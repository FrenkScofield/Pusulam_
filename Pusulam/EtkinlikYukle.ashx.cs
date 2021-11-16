using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class Etkinlik
    {
        public string AD { get; set; }
        public string ACIKLAMA { get; set; }
        public string BASLANGIC { get; set; }
        public string BITIS { get; set; }
    }

    public class EtkinlikYukle : IHttpHandler
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
            List<Etkinlik> list = new List<Etkinlik>();
            try
            {
                string sorgu = "select * from [ETKİNLİKLER$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        Etkinlik e = new Etkinlik();
                        e.AD = item["AD"].ToString();
                        e.ACIKLAMA = item["AÇIKLAMA"].ToString();
                        e.BASLANGIC = item["BAŞLANGIÇ"].ToString();
                        e.BITIS = item["BİTİŞ"].ToString();
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
    }
}