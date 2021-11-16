using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class Sinav
    {
        public string TUR { get; set; }
        public string AD { get; set; }
        public string KOD { get; set; }
        public string RAPORBASLIK { get; set; }
        public string TERCIH { get; set; }
        public string GRUP { get; set; }
        public string KITAPCIK { get; set; }
        public string YANLIS { get; set; }
        public string TARIH { get; set; }
        public string DERSPUANI { get; set; }
        public List<Ders> DERSLIST { get; set; }
    }

    public class Ders
    {
        public string id { get; set; }
        public string text { get; set; }
        public int SORUSAYISI { get; set; }
        public int SIRA { get; set; }
        public List<Soru> SORULIST { get; set; }
    }

    public class Soru
    {
        public string TAKMAAD { get; set; }
        public int SORUTURU { get; set; }
        public int SORUNO { get; set; }
        public int KARAKTERSAYISI { get; set; }
        public string ACEVAP { get; set; }
        public string KOD { get; set; }
        public double KATSAYI { get; set; }
        public List<Karsilik> KARSILIKLIST { get; set; }
        public int? ID_BILGI { get; set; }
        public int? ID_BILISSELSUREC { get; set; }
    }

    public class Karsilik
    {
        public char KITAPCIK { get; set; }
        public int SIRA { get; set; }
    }

    public class SinavTaslakYukle : IHttpHandler
    {
        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;
            string DosyaAd = Guid.NewGuid().ToString();
            string yol = "~/Dosyalar/SinavTemplate/";

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
                            file.SaveAs(path);
                        }
                    }
                }
                #endregion

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
                ExcelOku(baglanti, filepath);
            }
            else
            {
                context.Response.Write("Yalnızca xls ve xlsx dosyaları yükleyebilirsiniz.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void ExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            Sinav sinav = new Sinav();
            try
            {
                string sorgu = "select * from [SINAV$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                int rn = 1;
                List<Ders> dersList = new List<Ders>();
                List<Soru> soruList = new List<Soru>();
                String tempTakmaAd = "";
                foreach (DataRow item in dt.Rows)
                {
                    if (rn == 1)
                    {
                        sinav.TUR = item["TÜR"].ToString();
                        sinav.AD = item["AD"].ToString();
                        sinav.KOD = item["KOD"].ToString();
                        sinav.RAPORBASLIK = item["RAPOR BAŞLIK"].ToString();
                        sinav.TERCIH = item["TERCİHLER"].ToString();
                        sinav.GRUP = item["GRUP"].ToString();
                        sinav.KITAPCIK = item["KİTAPÇIK SAYISI"].ToString();
                        sinav.YANLIS = item["YANLIŞ SAYISI"].ToString();
                        sinav.TARIH = item["TARİH"].ToString();
                        sinav.DERSPUANI = item["DERS PUANI HESAPLA"].ToString();
                    }
                    if (!item.IsNull("ID_DERS"))
                    {
                        Ders ders = new Ders();
                        ders.id = item["ID_DERS"].ToString();
                        ders.text = item["GÖRÜNECEK İSİM 1"].ToString().Trim();
                        ders.SORUSAYISI = Convert.ToInt32(item["SORU SAYISI"]);
                        ders.SIRA = Convert.ToInt32(item["OPTİK SIRA"]);
                        dersList.Add(ders);
                    }
                    if (!tempTakmaAd.Equals(item["GÖRÜNECEK İSİM 2"].ToString().Trim()))
                    {
                        int index = getIndex(dersList, tempTakmaAd);
                        if (index > -1)
                        {
                            dersList[index].SORULIST = soruList;
                            soruList = new List<Soru>();
                        }
                    }
                    if (!item.IsNull("GÖRÜNECEK İSİM 2"))
                    {
                        Soru soru = new Soru();
                        soru.TAKMAAD = item["GÖRÜNECEK İSİM 2"].ToString().Trim();
                        soru.SORUTURU = Convert.ToInt32(item["SORU TÜRÜ"]);
                        soru.SORUNO = Convert.ToInt32(item["SORU NO"]);
                        soru.ACEVAP = item["A CEVAP"].ToString().Replace(" ","");
                        soru.KOD = item.IsNull("ÜNİTE KOD") ? "" : item["ÜNİTE KOD"].ToString();
                        soru.KATSAYI = item.IsNull("KATSAYI") ? 0d : Convert.ToDouble(item["KATSAYI"]);
                        string k = item.IsNull("KARŞILIK LİSTESİ") ? "" : item["KARŞILIK LİSTESİ"].ToString();
                        List<Karsilik> karsilikList = new List<Karsilik>();
                        for (int x = 0; x < k.Split(',').Length; x++)
                        {
                            Karsilik karsilik = new Karsilik();
                            karsilik.SIRA = Convert.ToInt32(k.Split(',')[x]);
                            karsilik.KITAPCIK = (char)(x + 66);
                            karsilikList.Add(karsilik);
                        }
                        soru.KARSILIKLIST = karsilikList;
                        soru.KARAKTERSAYISI = item.IsNull("KARAKTER SAYISI") ? 0 : Convert.ToInt32(item["KARAKTER SAYISI"]);

                        if (!item.IsNull("ID_BILGI"))
                        {
                            soru.ID_BILGI = Convert.ToInt32(item["ID_BILGI"]);
                        }
                        else
                        {
                            soru.ID_BILGI = 0;
                        }
                        if (!item.IsNull("ID_BILISSELSUREC"))
                        {
                            soru.ID_BILISSELSUREC = Convert.ToInt32(item["ID_BILISSELSUREC"]);
                        }
                        else
                        {
                            soru.ID_BILISSELSUREC = 0;
                        }

                        soruList.Add(soru);
                        tempTakmaAd = soru.TAKMAAD.Trim();
                    }

                    if (dt.Rows.Count == rn)
                    {
                        int index = getIndex(dersList, tempTakmaAd);
                        if (index > -1)
                        {
                            dersList[index].SORULIST = soruList;
                        }
                    }

                    rn++;
                }

                sinav.DERSLIST = dersList;
            }
            catch (Exception ex)
            {
                success = false;
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(sinav));

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private int getIndex(List<Ders> dersList, string TAKMAAD)
        {
            for (int i = 0; i < dersList.Count; i++)
            {
                if (dersList[i].text.Equals(TAKMAAD))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}