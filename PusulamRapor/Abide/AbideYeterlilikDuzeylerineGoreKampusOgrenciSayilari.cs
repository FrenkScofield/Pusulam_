using System;
using System.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Web;
using System.IO;
using DevExpress.Compression;

namespace PusulamRapor.Abide
{
    public partial class AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilari : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_ABIDESINAV;
        string ID_KADEME3;
        string DONEM;
        DataTable DTSONUC;
        DataTable DTDUZEY;
        DataTable DTKAMPUS;
        DataTable DTSINAV;
        DataTable DTDERS;

        public AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilari(string TCKIMLIKNO, string OTURUM, string ID_ABIDESINAV, string ID_KADEME3, string DONEM)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_ABIDESINAV = ID_ABIDESINAV;
            this.ID_KADEME3 = ID_KADEME3;
            this.DONEM = DONEM;
            InitializeComponent();
        }

        private void AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ABIDESINAV", ID_ABIDESINAV);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_MENU", 1169);
                b.ParametreEkle("@ISLEM", 24);
                DataSet ds = b.SorguGetir("sp_Abide");

                DTSONUC = ds.Tables[0];
                DTDUZEY = ds.Tables[1];
                DTKAMPUS = ds.Tables[2];
                DTSINAV = ds.Tables[3];
                DTDERS = ds.Tables[4];
                float Y = 0;

                foreach (DataRow DERSROW in DTDERS.Rows)
                {
                    string DERS = DERSROW["DERS"].ToString();

                    if (DTSONUC.Select("DERS='" + DERS + "'").Length > 0)
                    {
                        DataTable DTSONUCDERS = DTSONUC.Select("DERS='" + DERS + "'").CopyToDataTable();
                        AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilariSub subreport = new AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilariSub(DTSONUCDERS, DTDUZEY, DTKAMPUS, DTSINAV, OTURUM, DERS);
                        XRSubreport report = new XRSubreport();
                        report.ReportSource = subreport;
                        report.LocationF = new PointF(0, Y);
                        report.CanGrow = true;
                        Detail.Controls.Add(report);
                    }
                }
            }
        }

        private void AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilari_AfterPrint(object sender, EventArgs e)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));
            }

            string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "");

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            using (ZipArchive archive = new ZipArchive())
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    archive.AddFile(path + "/" + file.Name, "/");
                }
                archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""), true);
        }
    }
}
