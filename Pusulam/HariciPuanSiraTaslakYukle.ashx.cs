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
    /// Summary description for HariciPuanSiraTaslakYukle
    /// </summary>
    /// 

    public class HariciPuanSiraTaslak
    {
        public string TCKIMLIKNO { get; set; }
        public string ADSOYAD { get; set; }
        public List<HariciPuanSiraDetay> HariciList { get; set; }
        public string ILCEKATILIM { get; set; }
        public string ILKATILIM { get; set; }
        public string GENELKATILIM { get; set; }
    }

    public class HariciPuanSiraDetay
    {
        public string ID_SINAVPUANTURU { get; set; }
        public string PUAN { get; set; }
        public string SINIFSIRA { get; set; }
        public string OKULSIRA { get; set; }
        public string ILCESIRA { get; set; }
        public string ILSIRA { get; set; }
        public string GENELSIRA { get; set; }
    }

    public class HariciPuanSiraTaslakYukle : IHttpHandler
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
            List<HariciPuanSiraTaslak> list = new List<HariciPuanSiraTaslak>();
            try
            {
                string sorgu = "select * from [Sheet$]";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                int ptSayisi = (dt.Columns.Count - 5) / 6;

                foreach (DataRow item in dt.Rows)
                {
                    if (item["TCKIMLIKNO"].ToString() != "")
                    {

                        HariciPuanSiraTaslak t = (new HariciPuanSiraTaslak()
                        {
                            TCKIMLIKNO = item["TCKIMLIKNO"].ToString(),
                            ADSOYAD = item["AD SOYAD"].ToString(),
                            ILCEKATILIM = item["ILCE KATILIM SAYISI"].ToString(),
                            ILKATILIM = item["IL KATILIM SAYISI"].ToString(),
                            GENELKATILIM = item["GENEL KATILIM SAYISI"].ToString(),
                            HariciList = new List<HariciPuanSiraDetay>(),
                        });

                        for (int i = 0; i < ptSayisi; i++)
                        {
                            HariciPuanSiraDetay d = new HariciPuanSiraDetay();
                            int cIndex = (i * 6) + 2;

                            d.PUAN = item[cIndex].ToString();
                            d.SINIFSIRA = item[cIndex + 1].ToString();
                            d.OKULSIRA = item[cIndex + 2].ToString();
                            d.ILCESIRA = item[cIndex + 3].ToString();
                            d.ILSIRA = item[cIndex + 4].ToString();
                            d.GENELSIRA = item[cIndex + 5].ToString();
                            d.ID_SINAVPUANTURU = dt.Columns[ptSayisi * 6 + 5 + i].ToString();
                            t.HariciList.Add(d);
                        }

                        list.Add(t);

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