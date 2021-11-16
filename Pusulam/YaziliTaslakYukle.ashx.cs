using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class Yazili
    {
        public string AD { get; set; }
        public string KOD { get; set; }
        public string GRUP { get; set; }
        public string DERS { get; set; }
        public List<int> OKULTUR { get; set; }
        public string TARIH { get; set; }
        public string YARIYIL { get; set; }
        public List<YYSoru> SORULIST { get; set; }
        public string ID_YAZILITURU { get; set; }
    }

    public class YYSoru
    {
        public int SORUNO { get; set; }
        public string KOD { get; set; }
        public double PUAN { get; set; }
        public int? ID_BILGI { get; set; }
        public int? ID_BILISSELSUREC { get; set; }
    }

    public class YaziliTaslakYukle : IHttpHandler
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
            List<Yazili> YAZILILIST = new List<Yazili>();
            try
            {
                string sorgu = "select * from [YAZILI$A:H]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);

                DataTable dt = new DataTable();
                data_adaptor.Fill(dt);


                foreach (DataRow yazilirow in dt.Rows)
                {
                    if (yazilirow["AD"].ToString().Length > 0)
                    {
                        Yazili yazili = new Yazili();
                        yazili.AD = yazilirow["AD"].ToString();
                        yazili.KOD = yazilirow["KOD"].ToString();
                        yazili.GRUP = yazilirow["GRUP"].ToString();
                        yazili.DERS = yazilirow["ID_DERS"].ToString();
                        yazili.ID_YAZILITURU = yazilirow["ID_YAZILITURU"].ToString();
                        List<int> kademe2list = new List<int>();
                        for (int i = 0; i < yazilirow["OKUL TÜR"].ToString().Split(',').Length; i++)
                        {
                            kademe2list.Add(Convert.ToInt32(yazilirow["OKUL TÜR"].ToString().Split(',')[i]));
                        }
                        yazili.OKULTUR = kademe2list;
                        yazili.YARIYIL = yazilirow["YARIYIL"].ToString();
                        yazili.TARIH = yazilirow["TARİH"].ToString();

                        string sorgu2 = "select * from [YAZILI$J:O] where [SINAV AD]= '" + yazilirow["AD"].ToString() + "'";
                        OleDbDataAdapter data_adaptor2 = new OleDbDataAdapter(sorgu2, baglanti);
                        DataTable dt2 = new DataTable();
                        data_adaptor2.Fill(dt2);

                        List<YYSoru> soruList = new List<YYSoru>();
                        foreach (DataRow sorurow in dt2.Rows)
                        {
                            YYSoru soru = new YYSoru();
                            soru.SORUNO = Convert.ToInt32(sorurow["SORU NO"]);
                            soru.KOD = sorurow.IsNull("ÜNİTE KOD") ? "" : sorurow["ÜNİTE KOD"].ToString();
                            soru.PUAN = sorurow.IsNull("PUAN") ? 0d : Convert.ToDouble(sorurow["PUAN"]);

                            if (!sorurow.IsNull("ID_BILGI"))
                            {
                                soru.ID_BILGI = Convert.ToInt32(sorurow["ID_BILGI"]);
                            }

                            if (!sorurow.IsNull("ID_BILISSELSUREC"))
                            {
                                soru.ID_BILISSELSUREC = Convert.ToInt32(sorurow["ID_BILISSELSUREC"]);
                            }
                            soruList.Add(soru);
                        }
                        yazili.SORULIST = soruList;
                        YAZILILIST.Add(yazili);
                    }
                }

                baglanti.Close();
            }
            catch (Exception ex)
            {
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(YAZILILIST));

            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {

                }
            }
        }
    }
}