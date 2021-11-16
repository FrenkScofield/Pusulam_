using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class CevapAnahtariTaslak
    {
        public string ID_SINAVDERS { get; set; }
        public string TAKMAAD { get; set; }
        public string SORUNO { get; set; }
        public string CEVAP { get; set; }
        public string B_KARSILIK { get; set; }
        public string C_KARSILIK { get; set; }
        public string D_KARSILIK { get; set; }
        public string KOD { get; set; }
        public string ID_BILGI { get; set; }
        public string ID_BILISSELSUREC { get; set; }
        public string ID_SORUBANKASI { get; set; }
    }

    /// <summary>
    /// Summary description for CevapAnahtariTaslakYukle
    /// </summary>
    public class CevapAnahtariTaslakYukle : IHttpHandler
    {
        public DataTable dtOkunanDosya = new DataTable();
        private HttpContext context;

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
                ExcelOku(baglanti, filepath);
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

        private void ExcelOku(OleDbConnection baglanti, string path)
        {
            bool success = true;
            List<CevapAnahtariTaslak> list = new List<CevapAnahtariTaslak>();
            try
            {
                string sorgu = "select * from [Sheet$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new CevapAnahtariTaslak()
                    {
                        ID_SINAVDERS = item["ID_SINAVDERS"].ToString(),
                        TAKMAAD = item["TAKMAAD"].ToString(),
                        SORUNO = item["SORU NO"].ToString(),
                        CEVAP = item["A Kitapçık Doğru Cevap"].ToString(),
                        B_KARSILIK = item["B Karşılık"].ToString(),
                        C_KARSILIK = item["C Karşılık"].ToString(),
                        D_KARSILIK = item["D Karşılık"].ToString(),
                        KOD = item["Unite Kod"].ToString(),
                        ID_BILGI = item["ID_BILGI"].ToString(),
                        ID_BILISSELSUREC = item["ID_BILISSELSUREC"].ToString(),
                        ID_SORUBANKASI = item["ID_SORUBANKASI"].ToString(),
                    });
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