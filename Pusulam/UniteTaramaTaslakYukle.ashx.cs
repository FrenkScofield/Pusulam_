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
    public class UniteTarama
    {
        public int ID_DERS { get; set; }
        public int SORUNO { get; set; }
        public string KAZANIMKOD { get; set; }
        public int PUAN { get; set; }
    }

    //public class UniteTaramaYorum
    //{
    //    public string DERS { get; set; }
    //    public int DUZEY { get; set; }
    //    public string YORUM { get; set; }
    //    public string ONERI { get; set; }
    //}

    //public class UniteTaramaPuanAralik
    //{
    //    public int MAXIMUMPUAN { get; set; }
    //    public int DUZEY { get; set; }
    //}

    //public class UniteTaramaBeceri
    //{
    //    public string DERS { get; set; }
    //    public string BECERI { get; set; }
    //    public string ACIKLAMA { get; set; }
    //}

    //public class UniteTarama
    //{
    //    public List<UniteTaramaSoru> SORULIST { get; set; }
    //    public List<UniteTaramaYorum> YORUMLIST { get; set; }
    //    public List<UniteTaramaPuanAralik> PUANARALIKLIST { get; set; }
    //    public List<UniteTaramaBeceri> BECERILIST { get; set; }
    //}

    public class UniteTaramaTaslakYukle : IHttpHandler
    {
        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;

            string DosyaAd = "ID_UNITETARAMASINAV_" + context.Request["ID_UNITETARAMASINAV"];
            string yol = "~/Dosyalar/UniteTarama/";

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
            List<UniteTarama> uniteTarama = new List<UniteTarama>();

            try
            {
                string sorguSO = "select * from [Soru Özellikleri$]";
                //string sorguY = "select * from [Yorumlar$]";
                //string sorguPA = "select * from [Puan Aralıkları$]";
                //string sorguB = "select * from [Beceriler$]";
                OleDbDataAdapter data_adaptorSO = new OleDbDataAdapter(sorguSO, baglanti);
                //OleDbDataAdapter data_adaptorY = new OleDbDataAdapter(sorguY, baglanti);
                //OleDbDataAdapter data_adaptorPA = new OleDbDataAdapter(sorguPA, baglanti);
                //OleDbDataAdapter data_adaptorB = new OleDbDataAdapter(sorguB, baglanti);
                baglanti.Close();

                DataTable dtSO = new DataTable();
                //DataTable dtY = new DataTable();
                //DataTable dtPA = new DataTable();
                //DataTable dtB = new DataTable();

                data_adaptorSO.Fill(dtSO);
                //data_adaptorY.Fill(dtY);
                //data_adaptorPA.Fill(dtPA);
                //data_adaptorB.Fill(dtB);

                UniteTarama soru = new UniteTarama();
                for (int i = 0; i < dtSO.Rows.Count; i++)
                {
                    soru = new UniteTarama();
                    soru.ID_DERS = Convert.ToInt32(dtSO.Rows[i]["Ders ID"]);
                    soru.SORUNO = Convert.ToInt32(dtSO.Rows[i]["Soru No"]);
                    soru.KAZANIMKOD = dtSO.Rows[i]["Kazanım ID"].ToString();
                   // soru.BECERI = dtSO.Rows[i]["Beceri"].ToString();
                    soru.PUAN = Convert.ToInt32(dtSO.Rows[i]["Puan Değeri"]);
                    uniteTarama.Add(soru);
                }

                //List<UniteTaramaYorum> yorumlist = new List<UniteTaramaYorum>();
                //UniteTaramaYorum yorum;
                //for (int i = 0; i < dtY.Rows.Count; i++)
                //{
                //    if (dtY.Rows[i]["Ders"].ToString() != "")
                //    {
                //        yorum = new UniteTaramaYorum();
                //        yorum.DERS = dtY.Rows[i]["Ders"].ToString();
                //        yorum.DUZEY = Convert.ToInt32(dtY.Rows[i]["Düzey"]);
                //        yorum.YORUM = dtY.Rows[i]["Yorum"].ToString();
                //        yorum.ONERI = dtY.Rows[i]["Öneri"].ToString();
                //        yorumlist.Add(yorum);
                //    }
                //}
                //uniteTarama.YORUMLIST = yorumlist;

                //List<UniteTaramaPuanAralik> puanaraliklist = new List<UniteTaramaPuanAralik>();
                //UniteTaramaPuanAralik puanaralik;
                //for (int i = 0; i < dtPA.Rows.Count; i++)
                //{
                //    if (dtPA.Rows[i]["Maximum Puan"].ToString() != "")
                //    {
                //        puanaralik = new UniteTaramaPuanAralik();
                //        puanaralik.MAXIMUMPUAN = Convert.ToInt32(dtPA.Rows[i]["Maximum Puan"]);
                //        puanaralik.DUZEY = Convert.ToInt32(dtPA.Rows[i]["Düzey"]);
                //        puanaraliklist.Add(puanaralik);
                //    }
                //}
                //uniteTarama.PUANARALIKLIST = puanaraliklist;

                //List<UniteTaramaBeceri> becerilist = new List<UniteTaramaBeceri>();
                //UniteTaramaBeceri beceri;
                //for (int i = 0; i < dtB.Rows.Count; i++)
                //{
                //    if (dtB.Rows[i]["Ders"].ToString() != "")
                //    {
                //        beceri = new UniteTaramaBeceri();
                //        beceri.DERS = dtB.Rows[i]["Ders"].ToString();
                //        beceri.BECERI = dtB.Rows[i]["Beceri"].ToString();
                //        beceri.ACIKLAMA = dtB.Rows[i]["Beceri Açıklaması"].ToString();
                //        becerilist.Add(beceri);
                //    }
                //}
                //uniteTarama.BECERILIST = becerilist;

            }
            catch (Exception ex)
            {
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(uniteTarama));

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
        }
    }
}