using PusulamBusiness;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    /// <summary>
    /// Summary description for KarmaListeYukle
    /// </summary>
    /// 

    public class KarmaListeExcel
    {
        public string TCKIMLIKNO { get; set; }
        public string ADSOYAD { get; set; }
    }
    public class KarmaListeYukle : IHttpHandler
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string SINAVTARIH { get; set; }
        public void ProcessRequest(HttpContext context)
        {
            TCKIMLIKNO = context.Request.Params.Get(0);
            OTURUM = context.Request.Params.Get(1);
            SINAVTARIH = context.Request.Params.Get(2);
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;
            string DosyaAd = Guid.NewGuid().ToString();
            string yol = "~/Dosyalar/KarmaListe/";

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

        private void ExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<KarmaListeExcel> list = new List<KarmaListeExcel>();
            try
            {
                string sorgu = "select * from [KarmaListe$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    try
                    {
                        KarmaListeExcel e = new KarmaListeExcel();
                        e.TCKIMLIKNO = item["TCKIMLIKNO"].ToString();
                        e.ADSOYAD = item["AD SOYAD"].ToString();
                        list.Add(e);
                    }
                    catch (Exception)
                    {
                    }
                }




                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DKarmaListe.ID_MENU = 1147;
                        c.DKarmaListe.ExcelYukle(TCKIMLIKNO, OTURUM, SINAVTARIH, new JavaScriptSerializer().Serialize(list));
                    }
                }
                catch (Exception)
                {
                    throw;
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