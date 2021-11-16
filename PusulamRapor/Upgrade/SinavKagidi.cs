using DevExpress.Compression;
using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;

namespace PusulamRapor.Upgrade
{
    public partial class SinavKagidi : DevExpress.XtraReports.UI.XtraReport
    {
        private string TCKIMLIKNO { get; set; }
        private string oturum { get; set; }
        private int idTktSinav { get; set; }
        private string idTktSinavOrtalamaList { get; set; }
        private string TCKIMLIKNO_OGR { get; set; }
        private int idSube { get; set; }
        private int idSinavGrup { get; set; }
        private string idSinif { get; set; }
        private int idTKTTest { get; set; }
        private string DONEM { get; set; }
        private string TARIH { get; set; }
        private string HANGITARIH { get; set; }
        private string START { get; set; }
        private string END { get; set; }
        private string HANGITARIHSORU { get; set; }

        public SinavKagidi(string TCKIMLIKNO, string oturum, string idTktSinav, string idTktSinavOrtalamaList, string TCKIMLIKNO_OGR, string idSube, string idSinavGrup, string idSinif, string idTKTTest, string DONEM, string TARIH, string HANGITARIH, string START, string END, string HANGITARIHSORU)
        {
            InitializeComponent();
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.oturum = oturum;
            this.idTktSinav = Convert.ToInt32(idTktSinav);
            this.idTktSinavOrtalamaList = idTktSinavOrtalamaList;
            this.TCKIMLIKNO_OGR = TCKIMLIKNO_OGR;
            this.idSube = Convert.ToInt32(idSube);
            this.idSinavGrup = Convert.ToInt32(idSinavGrup);
            this.idSinif = idSinif;
            this.idTKTTest = Convert.ToInt32(idTKTTest);
            this.DONEM = DONEM;
            this.TARIH = TARIH;
            this.HANGITARIH = HANGITARIH;
            this.START = START;
            this.END = END;
            this.HANGITARIHSORU = HANGITARIHSORU;
        }

        DataSet ds;
        private void TktSinavKagidi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_TKTSINAV", idTktSinav);
                b.ParametreEkle("@ID_TKTSINAVORTALAMALIST", idTktSinavOrtalamaList);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@TARIH", TARIH);
                b.ParametreEkle("@HESAPTURU", HANGITARIH);
                b.ParametreEkle("@BASLANGIC", START);
                b.ParametreEkle("@BITIS", END);
                b.ParametreEkle("@HESAPTURUSORU", HANGITARIHSORU);

                b.ParametreEkle("@ID_TKTTEST", idTKTTest);
                b.ParametreEkle("@TCKIMLIKNO_OGR", TCKIMLIKNO_OGR);
                b.ParametreEkle("@ID_SUBE", idSube);
                b.ParametreEkle("@ID_SINAVGRUP", idSinavGrup);
                b.ParametreEkle("@ID_SINIFS", idSinif);
                b.ParametreEkle("@ID_MENU", 8);
                b.ParametreEkle("@ISLEM", 10);

                ds = b.SorguGetir("sp_UpgradeSoru");

                this.DataSource = ds.Tables[0];

                GroupHeader1.GroupFields.Add(new GroupField("ID_OGRENCI"));
                GroupHeader1.GroupFields.Add(new GroupField("ADSOYAD"));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillReportDataFields.Fill(GroupHeader1, ds.Tables[0]);
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            switch (idSinavGrup)
            {
                case 3:
                    xrPictureBox2.Image = Properties.Resources.TktAnaokulu_a;
                    break;
                case 4:
                    xrPictureBox2.Image = Properties.Resources.TktAnaokulu_b;
                    break;
                case 5:
                    xrPictureBox2.Image = Properties.Resources.TktAnaokulu_c;
                    break;
                case 6:
                    xrPictureBox2.Image = Properties.Resources.TktAnaokulu_a;
                    break;
                default:
                    xrPictureBox2.Image = Properties.Resources.Tkt1_Sinif;
                    break;
            }
            //if (idSinavGrup < 7) xrPictureBox2.Image = Properties.Resources.TktAnaokulu_a;
            //else if (idSinavGrup >= 7 || idSinavGrup < 11) xrPictureBox2.Image = Properties.Resources.Tkt1_Sinif;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                string sira = GetCurrentColumnValue("SIRA").ToString();
                xr_PageNum.Text = sira;
                string dosya = GetCurrentColumnValue("DOSYAAD").ToString() + "." + GetCurrentColumnValue("DOSYAUZANTI").ToString();
                string yol = AppDomain.CurrentDomain.BaseDirectory;
                imgSoru.Image = Image.FromFile(yol + "Dosyalar\\" + dosya);
            }
            catch (Exception ex)
            {

            }
        }

        List<int> pages = new List<int>();
        List<string> names = new List<string>();
        private void Detail_AfterPrint(object sender, EventArgs e)
        {
            pages[os - 1]++;
        }

        int os = 0;

        private void GroupHeader1_AfterPrint(object sender, EventArgs e)
        {
            os++;
            pages.Add(2);
            names.Add(GetCurrentColumnValue("TCKIMLIKNO").ToString() + " - " + GetCurrentColumnValue("ADSOYAD").ToString());
        }

        private void SinavKagidi_AfterPrint(object sender, EventArgs e)
        {
            string[] sourcefiles = new string[os];
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/Temp/" + oturum + ""));
            int tss = 0;
            for (int i = 0; i < os; i++)
            {
                XtraReport newReport = new XtraReport();
                for (int j = tss; j < tss + pages[i]; j++)
                {
                    if (tss == j)
                    {

                        //this.Pages[tss].AssignWatermark(CreateTextWatermark(idSinavGrup >= 7 && idSinavGrup < 11));
                    }
                    newReport.Pages.Add(this.Pages[j]);
                }
                tss += pages[i];

                string name = names[i];
                string path = HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/Temp/" + oturum + "/" + name + ".pdf");
                sourcefiles[i] = path;
                newReport.ExportToPdf(path);
            }

            if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/" + oturum + ".zip")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/" + oturum + ".zip"));
            }

            using (ZipArchive archive = new ZipArchive())
            {
                foreach (string file in sourcefiles)
                {
                    archive.AddFile(file, "/");
                }
                archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/" + oturum + ".zip"));
            }

            System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/Temp/" + oturum + ""));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/UpgradeSinavKagidi/Temp/" + oturum + ""), true);
        }

        private Watermark CreateTextWatermark(bool which)
        {
            Watermark textWatermark = new Watermark();
            if (which)
            {
                textWatermark.Image = Properties.Resources.Tkt1_Sinif;
            }
            else
            {
                textWatermark.Image = Properties.Resources.TktAnaokulu;
            }
            textWatermark.ImageAlign = ContentAlignment.MiddleCenter;
            textWatermark.ImageViewMode = ImageViewMode.Stretch;
            textWatermark.ShowBehind = true;

            if (os > 1)
            {
                lblOgrenciAdSoyad.LocationF = new Point(Convert.ToInt32(lblOgrenciAdSoyad.LocationF.X), Convert.ToInt32(lblOgrenciAdSoyad.LocationF.Y) + 100);
            }

            return textWatermark;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (idSinavGrup < 7) xrPictureBox1.Image = Properties.Resources.TktAnaokuluArka;
            else if (idSinavGrup >= 7 || idSinavGrup < 11) xrPictureBox1.Image = Properties.Resources.TktIlkokulArka;
        }
    }
}
