using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    public class AbideSoru
    {
        public string DERS { get; set; }
        public int SORUNO { get; set; }
        public string SORUNOTAKMA { get; set; }
        public string KAZANIM { get; set; }
        public string BECERI { get; set; }
        public int PUAN { get; set; }
        public int? ID_BILGI { get; set; }
        public int? ID_BILISSELSUREC { get; set; }
    }

    public class AbideYorum
    {
        public string DERS { get; set; }
        public string DUZEY { get; set; }
        public string YORUM { get; set; }
        public string ONERI { get; set; }
    }

    public class AbidePuanAralik
    {
        public int MAXIMUMPUAN { get; set; }
        public string DUZEY { get; set; }
    }

    public class AbideBeceri
    {
        public string DERS { get; set; }
        public string BECERI { get; set; }
        public string ACIKLAMA { get; set; }
    }

    public class Abide
    {
        public List<AbideSoru> SORULIST { get; set; }
        public List<AbideYorum> YORUMLIST { get; set; }
        public List<AbidePuanAralik> PUANARALIKLIST { get; set; }
        public List<AbideBeceri> BECERILIST { get; set; }
    }

    public class AbideTaslakYukle : IHttpHandler
    {
        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;

            string DosyaAd = "ID_ABIDESINAV_" + context.Request["ID_ABIDESINAV"];
            string yol = "~/Dosyalar/AbideExcel/";

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
            Abide abide = new Abide();

            try
            {
                string sorguSO = "select * from [Soru Özellikleri$]";
                string sorguY = "select * from [Yorumlar$]";
                string sorguPA = "select * from [Puan Aralıkları$]";
                string sorguB = "select * from [Beceriler$]";
                OleDbDataAdapter data_adaptorSO = new OleDbDataAdapter(sorguSO, baglanti);
                OleDbDataAdapter data_adaptorY = new OleDbDataAdapter(sorguY, baglanti);
                OleDbDataAdapter data_adaptorPA = new OleDbDataAdapter(sorguPA, baglanti);
                OleDbDataAdapter data_adaptorB = new OleDbDataAdapter(sorguB, baglanti);
                baglanti.Close();

                DataTable dtSO = new DataTable();
                DataTable dtY = new DataTable();
                DataTable dtPA = new DataTable();
                DataTable dtB = new DataTable();

                data_adaptorSO.Fill(dtSO);
                data_adaptorY.Fill(dtY);
                data_adaptorPA.Fill(dtPA);
                data_adaptorB.Fill(dtB);

                List<AbideSoru> sorulist = new List<AbideSoru>();
                AbideSoru soru;
                for (int i = 0; i < dtSO.Rows.Count; i++)
                {
                    soru = new AbideSoru();
                    soru.DERS = dtSO.Rows[i]["Ders"].ToString();
                    soru.SORUNO = Convert.ToInt32(dtSO.Rows[i]["Soru No"]);
                    soru.SORUNOTAKMA = dtSO.Rows[i]["Soru No (Takma)"].ToString();
                    soru.KAZANIM = dtSO.Rows[i]["Kazanım"].ToString();
                    soru.BECERI = dtSO.Rows[i]["Beceri"].ToString();
                    soru.PUAN = Convert.ToInt32(dtSO.Rows[i]["Puan"]);
                    if (!dtSO.Rows[i].IsNull("ID_BILGI"))
                    {
                        soru.ID_BILGI = Convert.ToInt32(dtSO.Rows[i]["ID_BILGI"]);
                    }
                    if (!dtSO.Rows[i].IsNull("ID_BILISSELSUREC"))
                    {
                        soru.ID_BILISSELSUREC = Convert.ToInt32(dtSO.Rows[i]["ID_BILISSELSUREC"]);
                    }
                    sorulist.Add(soru);
                }
                abide.SORULIST = sorulist;

                List<AbideYorum> yorumlist = new List<AbideYorum>();
                AbideYorum yorum;
                for (int i = 0; i < dtY.Rows.Count; i++)
                {
                    if (dtY.Rows[i]["Ders"].ToString() != "")
                    {
                        yorum = new AbideYorum();
                        yorum.DERS = dtY.Rows[i]["Ders"].ToString();
                        yorum.DUZEY = dtY.Rows[i]["Düzey"].ToString();
                        yorum.YORUM = dtY.Rows[i]["Yorum"].ToString();
                        yorum.ONERI = dtY.Rows[i]["Öneri"].ToString();
                        yorumlist.Add(yorum);
                    }
                }
                abide.YORUMLIST = yorumlist;

                List<AbidePuanAralik> puanaraliklist = new List<AbidePuanAralik>();
                AbidePuanAralik puanaralik;
                for (int i = 0; i < dtPA.Rows.Count; i++)
                {
                    if (dtPA.Rows[i]["Maximum Puan"].ToString() != "")
                    {
                        puanaralik = new AbidePuanAralik();
                        puanaralik.MAXIMUMPUAN = Convert.ToInt32(dtPA.Rows[i]["Maximum Puan"]);
                        puanaralik.DUZEY = dtPA.Rows[i]["Düzey"].ToString();
                        puanaraliklist.Add(puanaralik);
                    }
                }
                abide.PUANARALIKLIST = puanaraliklist;

                List<AbideBeceri> becerilist = new List<AbideBeceri>();
                AbideBeceri beceri;
                for (int i = 0; i < dtB.Rows.Count; i++)
                {
                    if (dtB.Rows[i]["Ders"].ToString() != "")
                    {
                        beceri = new AbideBeceri();
                        beceri.DERS = dtB.Rows[i]["Ders"].ToString();
                        beceri.BECERI = dtB.Rows[i]["Beceri"].ToString();
                        beceri.ACIKLAMA = dtB.Rows[i]["Beceri Açıklaması"].ToString();
                        becerilist.Add(beceri);
                    }
                }
                abide.BECERILIST = becerilist;

            }
            catch (Exception ex)
            {
            }

            context.Response.Write(new JavaScriptSerializer().Serialize(abide));

            //if (File.Exists(path))
            //{
            //    File.Delete(path);
            //}
        }
    }
}