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

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOMain : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string ID_MENU { get; set; }
        public string ID_SINIF { get; set; }
        public string TC_OGRENCI { get; set; }
        public string RAPORTURLIST { get; set; }

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

        public GelisimRaporuOOMain(string tckimlikno, string oturum, string tcogrenci, string idsinif, string raporturlist, string donem, string tur, string idmenu)
        {
            InitializeComponent();
            TC_OGRENCI = tcogrenci;
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            DONEM = donem;
            RAPORTURLIST = raporturlist;
            ID_MENU = idmenu;
            ID_SINIF = idsinif;
            TUR = tur.Equals("2") ? true : false;
        }

        private void GelisimRaporuOOMain_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                    b.ParametreEkle("@RAPORTURLIST", RAPORTURLIST);
                    b.ParametreEkle("@ISLEM", 2); // Rapor

                    ds = b.SorguGetir("sp_GelisimRaporuOO");

                    GroupField ogrenciField = new GroupField("TCKIMLIKNO");
                    GroupHeader1.GroupFields.Add(ogrenciField);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.DataSource = ds.Tables[0].DefaultView.ToTable(true, "TCKIMLIKNO");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();

            GelisimRaporuOO rapor = new GelisimRaporuOO(ds, TC, DONEM, RAPORTURLIST,OTURUM);
            xrGelisimRaporuOO.ReportSource = rapor;

        }

        //Dosya olarak kaydetme
        List<int> pages = new List<int>();
        List<string> names = new List<string>();
        int pagecount = -1;
        int os = 0;

        private void GroupHeader1_AfterPrint(object sender, EventArgs e)
        {
            if (TUR)
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
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            if (TUR)
            {
                pages[os - 1] = (this.Pages.Count) - pagecount;
                pagecount = this.Pages.Count;
            }
        }

        private void GelisimRaporuOOMain_AfterPrint(object sender, EventArgs e)
        {
            if (TUR)
            {
                string[] sourcefiles = new string[os];
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""));
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
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + "/" + name + ".pdf");
                    sourcefiles[i] = path;
                    newReport.ExportToPdf(path);
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/" + OTURUM + ".zip"));
                }

                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (string file in sourcefiles)
                    {
                        archive.AddFile(file, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/" + OTURUM + ".zip"));
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""), true);
            }
        }
    }
}
