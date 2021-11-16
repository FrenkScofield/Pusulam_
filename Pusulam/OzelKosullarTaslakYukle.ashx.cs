using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class OsymOzelKosullar
    {
        public int Kosul_No { get; set; }
       
        public string Aciklama { get; set; }
       
    }

    public class OzelKosullarTaslakYukle : IHttpHandler
    {
        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;

            string DosyaAd = "OsymOzelKosullar" +"-"+ System.Guid.NewGuid();
            //context.Request["ID_UNITETARAMASINAV"];
            string yol = "~/Dosyalar/OsymIslemleri/";

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
                            {
                                File.Delete(path);
                            }
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
            List<OsymOzelKosullar> osymOzelKosullar = new List<OsymOzelKosullar>();

            try
            {
                string sorguSO = "select * from [Table 1$]";
               
                OleDbDataAdapter data_adaptorSO = new OleDbDataAdapter(sorguSO, baglanti);
               
                baglanti.Close();

                DataTable dtSO = new DataTable();
               

                data_adaptorSO.Fill(dtSO);
                

                OsymOzelKosullar kosul = new OsymOzelKosullar();
                for (int i = 0; i < dtSO.Rows.Count; i++)
                {
                    kosul = new OsymOzelKosullar();
                    kosul.Kosul_No = Convert.ToInt32(dtSO.Rows[i]["Numara"]);
                    kosul.Aciklama = dtSO.Rows[i]["Aciklama"].ToString();
                    osymOzelKosullar.Add(kosul);
                }

               

            }
            catch (Exception ex)
            {
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(osymOzelKosullar));

           
        }
    }
}