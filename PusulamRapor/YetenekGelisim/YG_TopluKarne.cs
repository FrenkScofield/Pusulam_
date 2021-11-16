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

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_TopluKarne : DevExpress.XtraReports.UI.XtraReport
    {

        public string TC_OGRENCI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINIFs { get; set; }
        public int ID_MENU { get; set; }
        Boolean kaydetMi = false;
        Boolean ilkSinifMi = false;

        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataSet ds = new DataSet();

        public YG_TopluKarne(string tckimlikno, string oturum, string idSinifList, string _ilkSinifMi, string _kaydetMi)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINIFs = idSinifList;
            ID_MENU = 1188;

            kaydetMi = Convert.ToInt32(_kaydetMi) > 0;
            ilkSinifMi = Convert.ToInt32(_ilkSinifMi) > 0;
        }


        private void YG_TopluKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@ID_MENU", ID_MENU);
                    b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                    b.ParametreEkle("@ISLEM", 7); // Rapor

                    ds = b.SorguGetir("sp_YG_YetenekGelisim");

                    GroupField sinifField = new GroupField("SINIFAD");
                    GroupHeader1.GroupFields.Add(sinifField);
                    GroupField adsoyadField = new GroupField("ADSOYAD");
                    GroupHeader1.GroupFields.Add(adsoyadField);
                    GroupField ogrenciField = new GroupField("TCKIMLIKNO");
                    GroupHeader1.GroupFields.Add(ogrenciField);

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        t1 = PublicMetods.orderBYtoTable(ds.Tables[0], "SINIFAD, ADSOYAD");
                        t2 = ds.Tables[1];

                        this.DataSource = t1;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        List<int> pages = new List<int>();
        List<string> names = new List<string>();
        int pagecount = -1;
        int os = 0;

        YG_UstSinifKarne srUst = new YG_UstSinifKarne("", "", "");
        YG_IlkSinifKarne srIlk = new YG_IlkSinifKarne("", "", "");

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();

            DataTable dt1 = t1.Select("TCKIMLIKNO=" + TC).CopyToDataTable();
            DataTable dt2 = t2.Select("TCKIMLIKNO=" + TC).CopyToDataTable();


            if (ilkSinifMi)
            {
                srIlk.dt1 = dt1;
                srIlk.dt2 = dt2;
                srTopluKarne.ReportSource = srIlk;
            }
            else
            {
                srUst.dt1 = dt1;
                srUst.dt2 = dt2;
                srTopluKarne.ReportSource = srUst;

                this.PrintingSystem.Watermark.CopyFrom(srUst.Watermark);
                this.PrintingSystem.Pages.AddRange(srUst.Pages);
            }

        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            pages[os - 1] = (this.Pages.Count) - pagecount;
            pagecount = this.Pages.Count;
        }

        private void GroupHeader1_AfterPrint(object sender, EventArgs e)
        {
            os++;
            pages.Add(1);
            names.Add(GetCurrentColumnValue("TCKIMLIKNO").ToString());
        }

        private void YG_TopluKarne_AfterPrint(object sender, EventArgs e)
        {
            //string yol = this.PrintingSystem.Document.Name; //YG_TopluKarne
            string yol = this.Tag.ToString(); //YetenekGelisim.YG_TopluKarne

            if (kaydetMi)
            {
                string[] sourcefiles = new string[os];
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/Temp/" + OTURUM + ""));
                int tss = 0; // toplam sayfa sayısı
                for (int i = 0; i < os; i++)
                {
                    XtraReport newReport = new XtraReport();
                    for (int j = tss; j < tss + pages[i]; j++)
                    {
                        newReport.Pages.Add(this.Pages[j]);
                        if (!ilkSinifMi)
                        {
                            newReport.PrintingSystem.Watermark.CopyFrom(srUst.Watermark);
                            newReport.PrintingSystem.Pages.AddRange(srUst.Pages);
                        }
                    }
                    tss += pages[i];

                    string name = names[i];
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/Temp/" + OTURUM + "/" + name + ".pdf");
                    sourcefiles[i] = path;
                    newReport.ExportToPdf(path);
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/" + OTURUM + ".zip"));
                }

                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (string file in sourcefiles)
                    {
                        archive.AddFile(file, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/" + OTURUM + ".zip"));
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/Temp/" + OTURUM + ""));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/" + yol + "/Temp/" + OTURUM + ""), true);
            }
        }
    }
}
