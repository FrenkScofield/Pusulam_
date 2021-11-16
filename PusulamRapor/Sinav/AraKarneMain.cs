using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.Web;
using System.Collections.Generic;
using DevExpress.Compression;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Drawing;

namespace PusulamRapor.Sinav
{
    public partial class AraKarneMain : DevExpress.XtraReports.UI.XtraReport
    {
        public string TC_OGRENCI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_MENU { get; set; }
        public int ID_SINIF { get; set; }
        public string ID_SINAVTURU { get; set; }
        public string YAZILI_DONEM { get; set; }

        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataTable t3 = new DataTable();
        DataTable t4 = new DataTable();
        DataTable t5 = new DataTable();
        DataTable t6 = new DataTable();
        DataTable t7 = new DataTable();
        DataTable t8 = new DataTable();
        DataTable t9 = new DataTable();
        DataTable t10 = new DataTable();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();
        Boolean TUR = false;

        public AraKarneMain(string tckimlikno, string oturum, string tcogrenci, string donem, string idsinif, string idsinavturu, string yazilidonem, string tur)
        {
            InitializeComponent();
            TC_OGRENCI = tcogrenci;
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            DONEM = donem;
            ID_SINAVTURU = idsinavturu;
            YAZILI_DONEM = yazilidonem;
            ID_MENU = 1080;
            ID_SINIF = Convert.ToInt32(idsinif);
            TUR = tur.Equals("2") ? true : false;
        }

        private void AraKarneMain_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@DONEM", DONEM);
                    b.ParametreEkle("@TC_OGRENCI", (TC_OGRENCI.Equals("0") ? "" : TC_OGRENCI));
                    b.ParametreEkle("@ID_MENU", ID_MENU);
                    b.ParametreEkle("@ID_SINIF", ID_SINIF);
                    b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                    b.ParametreEkle("@YAZILI_DONEM", YAZILI_DONEM);
                    b.ParametreEkle("@ISLEM", 2); // Rapor

                    ds = b.SorguGetir("sp_AraKarneCoklu");

                    GroupField ogrenciField = new GroupField("TCKIMLIKNO");
                    GroupHeader1.GroupFields.Add(ogrenciField);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.DataSource = ds.Tables[0].DefaultView.ToTable(true, "TCKIMLIKNO");

                        t1 = ds.Tables[0];
                        t2 = ds.Tables[1];
                        t3 = ds.Tables[2];
                        t4 = ds.Tables[3];
                        t5 = ds.Tables[4];
                        t6 = ds.Tables[5];
                        t7 = ds.Tables[6];
                        t8 = ds.Tables[7];
                        t9 = ds.Tables[8];
                        t10 = ds.Tables[9];
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        bool yeniSaya = false;
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt10 = new DataTable();
            try
            {
                dt7 = t7.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
            }
            catch (Exception)
            {

                throw;
            }
            try
            {
                dt1 = t1.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
                dt2 = t2;
                dt3 = t3.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
                if (t4.Select("TCKIMLIKNO=" + TC).Length > 0)
                {
                    dt4 = t4.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
                }
                if (t5.Select("TCKIMLIKNO=" + TC).Length > 0)
                {
                    dt5 = t5.Select("TCKIMLIKNO=" + TC).CopyToDataTable();

                }
                dt6 = t6;
                dt8 = t8;
                dt9 = t9;
                dt10 = t10.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
            }
            catch (Exception)
            {

            }

            AraKarneCoklu arakarne = new AraKarneCoklu(dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, YAZILI_DONEM, yeniSaya);
            xrAraKarne.ReportSource = arakarne;


        }

        //Dosya olarak kaydetme
        List<int> pages = new List<int>();
        List<string> names = new List<string>();
        int pagecount = -1;
        int os = 0;

        private void GroupHeader1_AfterPrint(object sender, EventArgs e)
        {
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            os++;
            pages.Add(1);
            if (ds.Tables[0].Select("TCKIMLIKNO='" + TC + "'").Length > 0)
            {
                names.Add(GetCurrentColumnValue("TCKIMLIKNO").ToString() + " - " + ds.Tables[0].Select("TCKIMLIKNO='" + TC + "'").CopyToDataTable().Rows[0]["ADSOYAD"]);
            }
            else
            {
                names.Add(GetCurrentColumnValue("TCKIMLIKNO").ToString());
            }
        }
        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            pages[os - 1] = (this.Pages.Count) - pagecount;
            pagecount = this.Pages.Count;

            if (yeniSaya)
            {
                pages[os - 1]--;
            }
            yeniSaya = false;

            if (pages[os - 1] % 2 == 1)
            {
                yeniSaya = true;
                pages[os - 1]++;
            }
        }

        private void AraKarneMain_AfterPrint(object sender, EventArgs e)
        {
            if (TUR)
            {
                string[] sourcefiles = new string[os];
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/Temp/" + OTURUM + ""));
                int tss = 0; // toplam sayfa sayısı
                for (int i = 0; i < os; i++)
                {
                    XtraReport newReport = new XtraReport();
                    if (os == i + 1)
                    {
                        if (tss + pages[i] == this.Pages.Count + 1)
                        {
                            pages[i]--;
                        }
                    }

                    for (int j = tss; j < tss + pages[i]; j++)
                    {
                        this.Pages[j].AssignWatermark(this.Watermark);
                        newReport.Pages.Add(this.Pages[j]);
                    }
                    tss += pages[i];

                    string name = names[i];
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/Temp/" + OTURUM + "/" + name + ".pdf");
                    sourcefiles[i] = path;
                    newReport.ExportToPdf(path, new PdfExportOptions());
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/" + OTURUM + ".zip"));
                }

                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (string file in sourcefiles)
                    {
                        archive.AddFile(file, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/" + OTURUM + ".zip"));
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/Temp/" + OTURUM + ""));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AraKarne/Temp/" + OTURUM + ""), true);
            }
        }
    }
}
