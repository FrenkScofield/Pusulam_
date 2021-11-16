using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
namespace Pusulam.Helper
{
    /// <summary>
    /// Summary description for ExcelVeriYukle
    /// </summary>
    public class ExcelVeriYukle : IHttpHandler
    {
        #region CLASS
        public class cls_BurslulukOgrenci
        {
            public string TCKIMLIKNO { get; set; }
            public string OGRENCI_ADSOYAD { get; set; }
            public string OKULADI { get; set; }
            public string SINIFDUZEYI { get; set; }
            public string SUBEAD { get; set; }
            public string VELI_ADSOYAD { get; set; }
            public string MAIL { get; set; }
            public string TELEFON_EV { get; set; }
            public string TELEFON_CEP { get; set; }
            public string BASVURUTARIH { get; set; }
            public string SINAVTARIH { get; set; }
            public string SEANS { get; set; }
        }
        public class Unite
        {
            public string KOD { get; set; }
            public string AD { get; set; }
        }
        public class CurpusStudy
        {
            public string TCKIMLIKNO { get; set; }
            public string TOKEN { get; set; }
            public int ID_KULLANICITIPI { get; set; }
            public bool YONETIM { get; set; }
        }
        public class RehberlikOnline
        {
            public string TCKIMLIKNO { get; set; }
            public string TESTADI { get; set; }
            public string LINK { get; set; }
        }
        public class UniteLink
        {
            public string KOD { get; set; }
            public string LINK { get; set; }
        }
        public class MorpaKazanim
        {
            public string KOD { get; set; }
            public string AD { get; set; }
            public string KOD_OKY { get; set; }
        }
        public class ZumreCalismaNot
        {
            public string TCKIMLIKNO { get; set; }
            public string YAZILI_NOT { get; set; }
        }
        public class ViuTopluMesaj
        {
            public string TC_GONDEREN { get; set; }
            public string TC_ALAN { get; set; }
            public string MESAJ { get; set; }
            public string LINK_ETIKET { get; set; }
            public string LINK { get; set; }
        }
        public class AkilliOgretimBarkod
        {
            public string ALTKONUKOD { get; set; }
            public string AKILLIOGRETIMBARKODKOD { get; set; }
        }

        #region YYS

        public class YYSDers
        {
            public int DERSNO { get; set; }
            public string DERSAD { get; set; }
            public List<YYSSoru> SORULIST { get; set; }
            public List<YYSDuzey> DUZEYLIST { get; set; }
        }

        public class YYSSoru
        {
            public int DERSNO { get; set; }
            public int SORUNO { get; set; }
            public string SORU { get; set; }
            public string CEVAP { get; set; }
        }

        public class YYSDuzey
        {
            public int DERSNO { get; set; }
            public int DUZEY { get; set; }
            public string ACIKLAMA { get; set; }
        }

        public class YYS
        {
            public List<YYSDers> DERSLIST { get; set; }
        }


        #endregion

        #endregion

        public DataTable dtOkunanDosya = new DataTable();
        private HttpContext context;
        public string Tur { get; set; }


        DataTable tablolar;

        public void ProcessRequest(HttpContext context)
        {
            Tur = context.Request.Params.Get(0);
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;
            string DosyaAd = Guid.NewGuid().ToString();
            string yol = "~/Dosyalar/ExcelTaslak/";

            if (Tur == "ZumreCalismaNot")
            {
                yol = "~/Dosyalar/ZumreCalisma/Dosyalar/";
                DosyaAd = "ID_ZUMRECALISMA_" + context.Request.Params.Get(1);
            }
            else if (Tur == "YYS")
            {
                yol = "~/Dosyalar/YYSExcel/";
                DosyaAd = "ID_YYSSINAV_" + context.Request["id"];
            }

            string extension = System.IO.Path.GetExtension(context.Request.Files[0].FileName);

            if ((extension == ".xls" || extension == ".xlsx"))
            {
                #region Dosya

                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var path = Path.Combine(Path.Combine(context.Server.MapPath(yol), DosyaAd + extension));
                            if (File.Exists(path))                            
                                File.Delete(path);                            
                            file.SaveAs(path);
                        }
                    }
                }

                #endregion Dosya

                string filepath = context.Server.MapPath(yol) + DosyaAd + extension;
                OleDbConnection baglanti;               

                try
                {
                    baglanti = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0;IMEX=1'", filepath));
                    baglanti.Open();
                }
                catch (Exception)
                {
                    baglanti = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0;'", filepath));                    
                    baglanti.Open();
                }

                tablolar = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                switch (Tur)
                {
                    case "BurslulukOgrenci":
                        BurslulukExcelOku(baglanti, filepath);
                        break;
                    case "AssessmentOgrenci":
                        AssessmentOgrenciExcelOku(baglanti, filepath);
                        break;
                    case "Unite":
                        UniteExcelOku(baglanti, filepath);
                        break;
                    case "UniteLink":
                        UniteLinkExcelOku(baglanti, filepath);
                        break;
                    case "MorpaKazanim":
                        MorpaKazanimOku(baglanti, filepath);
                        break;
                    case "CurpusStudy":
                        CurpusStudyExcelOku(baglanti, filepath);
                        break;
                    case "RehberlikOnline":
                        RehberlikOnlineExcelOku(baglanti, filepath);
                        break;
                    case "ZumreCalismaNot":
                        ZumreCalismaNotOku(baglanti, filepath);
                        break;
                    case "ViuTopluMesaj":
                        ViuTopluMesajOku(baglanti, filepath);
                        break;
                    case "AkilliOgretimBarkod":
                        AkilliOgretimBarkodOku(baglanti, filepath);
                        break;
                    case "YYS":
                        YYSOku(baglanti, filepath);
                        if (baglanti.State == ConnectionState.Open)
                            baglanti.Close();
                        break;
                    default:
                        break;
                }

            }
            else
            {
                context.Response.Write("Yalnızca xls ve xlsx dosyaları yükleyebilirsiniz.");
            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }

        private void BurslulukExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<cls_BurslulukOgrenci> list = new List<cls_BurslulukOgrenci>();
            try
            {
                //string sorgu = "select * from [Sheet$]";

                string sorgu = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);


                foreach (DataRow item in dt.Rows)
                {

                    list.Add(new cls_BurslulukOgrenci()
                    {
                        TCKIMLIKNO = item["TC Kimlik No"].ToString(),
                        OGRENCI_ADSOYAD = item["Öğrenci Adı Soyadı"].ToString(),
                        OKULADI = item["Okulu"].ToString(),
                        SINIFDUZEYI = item["Sınıf Düzeyi"].ToString(),
                        SUBEAD = item["Başvurduğu Kampüs"].ToString(),
                        VELI_ADSOYAD = item["Veli Adı Soyadı"].ToString(),
                        MAIL = item["Mail"].ToString(),
                        TELEFON_EV = item["Telefon (Ev)"].ToString(),
                        TELEFON_CEP = item["Telefon (Cep)"].ToString(),
                        BASVURUTARIH = item["Başvuru Tarihi"].ToString(),
                        SINAVTARIH = item["Sınav Tarihi"].ToString(),
                        SEANS = item["Seans"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                success = false;
            }





            context.Response.Write(new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void AssessmentOgrenciExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            //List<cls_BurslulukOgrenci> list = new List<cls_BurslulukOgrenci>();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();


            List<string> excelSutun = ExcelSutunListesi();

            try
            {

                string sorgu = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";

                // string sorgu = "select * from [Sheet$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);


                foreach (DataRow item in dt.Rows)
                {
                    Dictionary<string, string> list2 = new Dictionary<string, string>();
                    int index = 0;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        list2.Add(excelSutun[index].ToString(), item[dc.ColumnName].ToString());
                        index++;
                    }
                    list.Add(list2);
                }

            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer() { MaxJsonLength = int.MaxValue }.Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void UniteExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<Unite> list = new List<Unite>();
            try
            {
                string sorgu = "select * from [UNITELER$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        Unite e = new Unite();
                        e.KOD = item["KOD"].ToString();
                        e.AD = item["AD"].ToString();
                        list.Add(e);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void CurpusStudyExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<CurpusStudy> list = new List<CurpusStudy>();
            try
            {
                string sorgu = "select * from [CurpusStudy$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        CurpusStudy e = new CurpusStudy();
                        e.TCKIMLIKNO = item["TCKIMLIKNO"].ToString();
                        e.TOKEN = item["TOKEN"].ToString();
                        e.ID_KULLANICITIPI = Convert.ToInt32(item["ID_KULLANICITIPI"].ToString());
                        e.YONETIM = item["YONETIM"].ToString() == "1" || item["YONETIM"].ToString() == "true" ? true : false;
                        list.Add(e);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void RehberlikOnlineExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<RehberlikOnline> list = new List<RehberlikOnline>();
            try
            {
                //string sorgu = "select * from [RehberlikOnline$]";
                string sorgu = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        RehberlikOnline e = new RehberlikOnline();
                        e.TCKIMLIKNO = item["TCKIMLIKNO"].ToString();
                        e.TESTADI = item["TESTADI"].ToString();
                        e.LINK = item["LINK"].ToString();
                        list.Add(e);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void UniteLinkExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<UniteLink> list = new List<UniteLink>();
            try
            {
                string sorgu = "select * from [UNITELINK$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        UniteLink e = new UniteLink();
                        e.KOD = item["KOD"].ToString();
                        e.LINK = item["LINK"].ToString();
                        list.Add(e);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void MorpaKazanimOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<MorpaKazanim> list = new List<MorpaKazanim>();
            try
            {
                string sorgu = "select * from [MORPAKAZANIM$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        MorpaKazanim e = new MorpaKazanim();
                        e.KOD = item["Morpa Kazanım Kodu"].ToString();
                        e.AD = item["Morpa Kazanım Adı"].ToString();
                        e.KOD_OKY = item["Okyanus Kazanım Kodu"].ToString();
                        if (!e.KOD.Equals(""))
                        {
                            list.Add(e);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void ZumreCalismaNotOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<ZumreCalismaNot> list = new List<ZumreCalismaNot>();
            try
            {
                string sorgu = "select * from [ZUMRECALISMA$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        ZumreCalismaNot e = new ZumreCalismaNot();
                        e.TCKIMLIKNO = item["TCNO"].ToString();
                        e.YAZILI_NOT = item["NOT"].ToString();
                        if (!e.TCKIMLIKNO.Equals(""))
                        {
                            list.Add(e);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
        }

        private void ViuTopluMesajOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<ViuTopluMesaj> list = new List<ViuTopluMesaj>();
            try
            {
                string sorgu = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();
                DataTable dt = new DataTable();
                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        ViuTopluMesaj e = new ViuTopluMesaj();
                        e.TC_GONDEREN = item["TC_GONDEREN"].ToString();
                        e.TC_ALAN = item["TC_ALAN"].ToString();
                        e.MESAJ = item["MESAJ"].ToString();
                        e.LINK_ETIKET = item["LINK_ETIKET"].ToString();
                        e.LINK = item["LINK"].ToString();
                        if (!e.TC_ALAN.Equals("") && !e.MESAJ.Equals(""))
                        {
                            list.Add(e);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }
            JavaScriptSerializer sr= new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
            context.Response.Write(sr.Serialize(list));

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
        }

        private void AkilliOgretimBarkodOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<AkilliOgretimBarkod> list = new List<AkilliOgretimBarkod>();
            try
            {
                string sorgu = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();
                DataTable dt = new DataTable();
                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        AkilliOgretimBarkod e = new AkilliOgretimBarkod();
                        e.ALTKONUKOD = item["Alt_Konu_Kodu"].ToString();
                        e.AKILLIOGRETIMBARKODKOD = item["Akilli_Ogretim_Barkod_Kodu"].ToString();
                        if (!e.ALTKONUKOD.Equals("") && !e.AKILLIOGRETIMBARKODKOD.Equals(""))
                        {
                            list.Add(e);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(list));
        }

        private void YYSOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            bool soruDuzey = true;
            YYS cYys = new YYS();
            try
            {
                //string sorguDers = "SELECT * FROM [" + tablolar.Rows[0]["TABLE_NAME"].ToString() + "]";
                string sorguDers = "SELECT * FROM  [Ders$]";
                string sorguSoru = "SELECT * FROM  [Soru$]";
                string sorguDuzey = "SELECT * FROM [Duzey$]";
                OleDbDataAdapter daDers = new OleDbDataAdapter(sorguDers, baglanti);
                OleDbDataAdapter daSoru = new OleDbDataAdapter(sorguSoru, baglanti);
                OleDbDataAdapter daDuzey = new OleDbDataAdapter(sorguDuzey, baglanti);
                baglanti.Close();
                DataTable dtDers = new DataTable(); daDers.Fill(dtDers);
                DataTable dtSoru = new DataTable(); daSoru.Fill(dtSoru);
                DataTable dtDuzey = new DataTable(); daDuzey.Fill(dtDuzey);


                List<YYSDers> listDers = new List<YYSDers>();
                foreach (DataRow ders in dtDers.Rows)
                {
                    YYSDers cDers;
                    try
                    {
                        cDers = new YYSDers();
                        cDers.DERSNO = Convert.ToInt32(ders["DERSNO"].ToString());
                        cDers.DERSAD = ders["DERSAD"].ToString();
                        if (cDers.DERSNO > 0 && !cDers.DERSAD.Equals(""))
                        {
                            List<YYSSoru> listSoru = new List<YYSSoru>();
                            if (dtSoru.Select("DERSNO=" + cDers.DERSNO).Length>0)
                            {
                                foreach (DataRow soru in dtSoru.Select("DERSNO="+ cDers.DERSNO).CopyToDataTable().Rows)
                                {
                                    YYSSoru cSoru;
                                    try
                                    {
                                        cSoru = new YYSSoru();
                                        cSoru.DERSNO = Convert.ToInt32(soru["DERSNO"].ToString());
                                        cSoru.SORUNO = Convert.ToInt32(soru["SORUNO"].ToString());
                                        cSoru.SORU = soru["SORU"].ToString();
                                        cSoru.CEVAP = soru["CEVAP"].ToString();
                                        if (cSoru.DERSNO > 0 && cSoru.SORUNO > 0 && !cSoru.SORU.Equals(""))
                                        {
                                            listSoru.Add(cSoru);
                                        }
                                    }
                                    catch (Exception) { }
                                }
                            }
                            List<YYSDuzey> listDuzey = new List<YYSDuzey>();
                            if (dtDuzey.Select("DERSNO=" + cDers.DERSNO).Length > 0)
                            {
                                foreach (DataRow duzey in dtDuzey.Select("DERSNO=" + cDers.DERSNO).CopyToDataTable().Rows)
                                {
                                    YYSDuzey cDuzey;
                                    try
                                    {
                                        cDuzey = new YYSDuzey();
                                        cDuzey.DERSNO = Convert.ToInt32(duzey["DERSNO"].ToString());
                                        cDuzey.DUZEY = Convert.ToInt32(duzey["DUZEY"].ToString());
                                        cDuzey.ACIKLAMA = duzey["ACIKLAMA"].ToString();
                                        if (cDuzey.DERSNO > 0 && cDuzey.DUZEY > 0 && !cDuzey.ACIKLAMA.Equals(""))
                                        {
                                            listDuzey.Add(cDuzey);
                                        }
                                    }
                                    catch (Exception) { }
                                }
                            }

                            if (listDuzey.Count != listSoru.Count )
                            {
                                soruDuzey = false;
                                break;
                            }

                            cDers.DUZEYLIST = listDuzey;
                            cDers.SORULIST = listSoru;
                            listDers.Add(cDers);
                        }
                    }
                    catch (Exception) { }
                }
                cYys.DERSLIST = listDers;
            }
            catch (Exception ex)
            {
                success = false;
            }
            if (!soruDuzey)
                cYys = new YYS();
            context.Response.Write(new JavaScriptSerializer().Serialize(cYys));
        }


        private List<string> ExcelSutunListesi()
        {
            List<string> k = new List<string>();

            for (var i = 65; i <= 90; i++)
            {
                k.Add(char.ConvertFromUtf32(i));
            }
            for (var i = 65; i <= 68; i++)
            {
                for (var j = 65; j <= 90; j++)
                {
                    k.Add(char.ConvertFromUtf32(i) + char.ConvertFromUtf32(j));
                }
            }
            return k;


        }
    }
}