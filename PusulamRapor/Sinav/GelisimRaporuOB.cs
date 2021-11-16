using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Web;
using DevExpress.Compression;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOB : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string ID_SINAVTURU { get; set; }
        public string ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_SINIF { get; set; }
        public string TC_OGRENCI { get; set; }
        public string ACIKLAMA { get; set; }
        public int DOWNLOAD { get; set; }
        public int OOD { get; set; }
        public int ETUTRAPORU { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();

        public GelisimRaporuOB(string tckimlikno, string oturum, string donem, string idSinavTuru, string idSinav, string idSube, string idSinif, string tcOgrenci, string aciklama, string download, string OOD, string ETUTRAPORU)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            DONEM = donem;
            ID_SINAVTURU = idSinavTuru;
            ID_SINAV = idSinav;
            ID_SUBE = Convert.ToInt32(idSube);
            ID_SINIF = Convert.ToInt32(idSinif);
            TC_OGRENCI = tcOgrenci;
            ACIKLAMA = aciklama;
            DOWNLOAD = Convert.ToInt32(download);
            this.OOD = Convert.ToInt32(OOD);
            this.ETUTRAPORU = Convert.ToInt32(ETUTRAPORU);
        }

        private void GelisimRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                b.ParametreEkle("@ID_SINAVS", ID_SINAV);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);
                b.ParametreEkle("@ID_SINIF", ID_SINIF);
                b.ParametreEkle("@OOD", OOD);
                b.ParametreEkle("@ETUTRAPORU", ETUTRAPORU);
                b.ParametreEkle("@ID_MENU", 1083);

                ds = b.SorguGetir("sp_GelisimRaporuOgrenciBelge");

                if (ds.Tables[0].Rows.Count > 0)
                {

                    GroupField grpField = new GroupField("TCKIMLIKNO");
                    GroupHeader1.GroupFields.Add(grpField);

                    GroupField grpField2 = new GroupField("DERSAD");
                    GroupHeader2.GroupFields.Add(grpField2);


                    this.DataSource = ds.Tables[0].DefaultView.ToTable(true, "TCKIMLIKNO", "DERSAD");

                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                    dt3 = ds.Tables[2];
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        string tc = "";
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tc = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            DataTable d = dt1.Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
            //FillReportDataFields.Fill(GroupHeader1, d);
            TCKN.Text = tc;
            ADSOYAD.Text = d.Rows[0]["ADSOYAD"].ToString();
            SUBE.Text = d.Rows[0]["SUBEAD"].ToString();
            SINIF.Text = d.Rows[0]["SINIF"].ToString();
            aciklama.Text = ACIKLAMA;
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable d = dt1.Select(String.Format("TCKIMLIKNO = '{0}' AND DERSAD = '{1}'", tc, GetCurrentColumnValue("DERSAD").ToString())).CopyToDataTable();

            grDersSinav ders = new grDersSinav(d, false);
            srDersSinav.ReportSource = ders;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            tc = GetCurrentColumnValue("TCKIMLIKNO").ToString();

            if (dt2.Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
            {
                DataTable d2 = dt2.Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                if (d2.Rows.Count > 0)
                {
                    grDersSinav net = new grDersSinav(d2, true);
                    srPuanNet.ReportSource = net;
                }
            }

            if (dt3.Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
            {
                DataTable d3 = dt3.Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                if (d3.Rows.Count > 0)
                {
                    grPuanSinav puan = new grPuanSinav(d3);
                    srPuanSinav.ReportSource = puan;
                }
            }

            int index = 3;
            if (OOD == 1)
            {
                if (ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
                {
                    DataTable d4 = ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                    if (d4.Rows.Count > 0)
                    {
                        grOgretmenDegerlendirme degerlendirme = new grOgretmenDegerlendirme(d4);
                        srOgretmenDegerlendirme.ReportSource = degerlendirme;
                    }
                }

                index++;
                if (ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
                {
                    DataTable d5 = ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                    if (d5.Rows.Count > 0)
                    {
                        grOgretmenYorum yorum = new grOgretmenYorum(d5);
                        srOgretmenYorum.ReportSource = yorum;
                    }
                }
                index++;
            }

            if (ETUTRAPORU == 1)
            {
                if (ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
                {
                    DataTable d5 = ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                    if (d5.Rows.Count > 0)
                    {
                        grEtutRaporu gretutraporu = new grEtutRaporu(d5);
                        srEtutRaporu.ReportSource = gretutraporu;
                    }
                }
            }
        }

        //Dosya olarak kaydetme
        List<int> pages = new List<int>();
        List<string> names = new List<string>();
        int pagecount = -1;
        int os = 0;

        private void GroupHeader1_AfterPrint(object sender, EventArgs e)
        {
            if (DOWNLOAD == 1)
            {
                os++;
                pages.Add(1);
                names.Add(GetCurrentColumnValue("TCKIMLIKNO").ToString());
                if (this.Pages.Count > 1)
                {
                    pages[os - 2] = (this.Pages.Count) - pagecount;
                    pagecount = this.Pages.Count;
                }
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
        }

        private void GelisimRaporuOB_AfterPrint(object sender, EventArgs e)
        {
            if (DOWNLOAD == 1)
            {
                pages[os - 1] = (this.Pages.Count) - pagecount - 1;
                pagecount = this.Pages.Count;

                string[] sourcefiles = new string[os];
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/Temp/" + OTURUM + ""));
                int tss = 0;
                for (int i = 0; i < os; i++)
                {
                    XtraReport newReport = new XtraReport();
                    for (int j = tss; j < tss + pages[i]; j++)
                    {
                        newReport.Pages.Add(this.Pages[j]);
                    }
                    tss += pages[i];

                    string name = names[i];
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/Temp/" + OTURUM + "/" + name + ".pdf");
                    sourcefiles[i] = path;
                    newReport.ExportToPdf(path);
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/" + OTURUM + ".zip"));
                }

                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (string file in sourcefiles)
                    {
                        archive.AddFile(file, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/" + OTURUM + ".zip"));
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/Temp/" + OTURUM + ""));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/KonuAnaliz/Temp/" + OTURUM + ""), true);
            }
        }
    }
}
