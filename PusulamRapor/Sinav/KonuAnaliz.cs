using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.IO;
using DevExpress.Compression;

namespace PusulamRapor.Sinav
{
    public partial class KonuAnaliz : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string ID_SINAVTURU { get; set; }
        public string ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_SINIF { get; set; }
        public string TC_OGRENCI { get; set; }
        public int LIMIT { get; set; }
        public int ACIKLAMALIMIT { get; set; }
        public string ACIKLAMA { get; set; }
        public int DOWNLOAD { get; set; }

        DataSet ds = new DataSet();
        public KonuAnaliz(string tckimlikno, string oturum, string donem, string idSinavTuru, string idSinav, string idSube, string idSinif, string tcOgrenci, string limit, string aciklamaLimit, string aciklama, string download)
        {
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            DONEM = donem;
            ID_SINAVTURU = idSinavTuru;
            ID_SINAV = idSinav;
            ID_SUBE = Convert.ToInt32(idSube);
            ID_SINIF = Convert.ToInt32(idSinif);
            TC_OGRENCI = tcOgrenci;
            LIMIT = Convert.ToInt32(limit);
            ACIKLAMALIMIT = Convert.ToInt32(aciklamaLimit);
            ACIKLAMA = aciklama;
            DOWNLOAD = Convert.ToInt32(download);
            InitializeComponent();
        }

        private void KonuAnaliz_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                b.ParametreEkle("@LIMIT", LIMIT);
                b.ParametreEkle("@ID_MENU", 1084);
                b.ParametreEkle("@ISLEM", 1); // Rapor
                ds = b.SorguGetir("sp_KonuAnaliziCoklu");
            }

            this.DataSource = ds.Tables[0].DefaultView.ToTable(true, "TCKIMLIKNO");
            GroupField ogrenciField = new GroupField("TCKIMLIKNO");
            GroupHeader1.GroupFields.Add(ogrenciField);
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Font font = new Font(familyArial, 8, FontStyle.Bold);
            adsoyad.Font = font;
            sinif.Font = font;
            okul.Font = font;
            tc.Font = font;

            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            DataTable dt2 = ds.Tables[1].Select("TCKIMLIKNO=" + TC).CopyToDataTable();

            adsoyad.Text = dt2.Rows[0][0].ToString();
            sinif.Text = dt2.Rows[0][1].ToString();
            okul.Text = dt2.Rows[0][2].ToString();
            tc.Text = dt2.Rows[0][3].ToString();

            DataTable dt1 = ds.Tables[0].Select("TCKIMLIKNO=" + TC).CopyToDataTable();
            if (dt1.Select("YUZDE<=" + ACIKLAMALIMIT).Length > 0)
            {
                aciklama.Visible = true;
                aciklama.Text = ACIKLAMA;
            }
            else
            {
                aciklama.Visible = false;
            }
        }

        readonly FontFamily familyArial = new FontFamily("Arial");
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Font font = new Font(familyArial, 8, FontStyle.Bold);
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();

            DataTable dt1 = ds.Tables[0].Select("TCKIMLIKNO=" + TC).CopyToDataTable();
            panel_unite.Controls.Clear();

            float y = 0;

            int DataRowDon = dt1.Rows.Count;
            int Yarisi = (dt1.Rows.Count / 2) - 1;

            for (int i = 0; i < DataRowDon; i++)
            {
                float x = 0;
                //Color BackC = dt5.Rows[i]["KOD"].ToString()== "" ? Color.Orange : Color.Transparent;
                Color BackC = i % 2 == 0 ? Color.FromArgb(217, 226, 243) : Color.White;
                Color BorderC = Color.FromArgb(180, 198, 231);

                XRLabel xr_KAZANIM = new XRLabel()
                {
                    Text = dt1.Rows[i]["KONUAD"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 410,
                    Font = font,
                    HeightF = 30,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange,
                    Padding = 4,
                    KeepTogether = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                    CanGrow = true
                };
                panel_unite.Controls.Add(xr_KAZANIM);

                //if (dt1.Rows[i]["KOD"].ToString().Length > 0)
                //{
                x += xr_KAZANIM.WidthF;
                XRLabel xr_ss = new XRLabel()
                {
                    Text = dt1.Rows[i]["SORU"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 80,
                    Font = font,
                    HeightF = xr_KAZANIM.HeightF,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    KeepTogether = true,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange
                };
                panel_unite.Controls.Add(xr_ss);
                x += xr_ss.WidthF;

                XRLabel xr_d = new XRLabel()
                {
                    Text = dt1.Rows[i]["DOGRU"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 80,
                    Font = font,
                    HeightF = xr_KAZANIM.HeightF,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    KeepTogether = true,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange
                };
                panel_unite.Controls.Add(xr_d);
                x += xr_d.WidthF;
                XRLabel xr_y = new XRLabel()
                {
                    Text = dt1.Rows[i]["YANLIS"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 80,
                    Font = font,
                    HeightF = xr_KAZANIM.HeightF,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    KeepTogether = true,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange
                };
                panel_unite.Controls.Add(xr_y);
                x += xr_y.WidthF;
                XRLabel xr_b = new XRLabel()
                {
                    Text = dt1.Rows[i]["BOS"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 80,
                    Font = font,
                    HeightF = xr_KAZANIM.HeightF,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    KeepTogether = true,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange
                };
                panel_unite.Controls.Add(xr_b);
                x += xr_y.WidthF;
                XRLabel xr_o = new XRLabel()
                {
                    Text = dt1.Rows[i]["YUZDE"].ToString(),
                    LocationF = new PointF(x, y),
                    WidthF = 80,
                    Font = font,
                    HeightF = xr_KAZANIM.HeightF,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderColor = BorderC,
                    KeepTogether = true,
                    BackColor = dt1.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange
                };
                panel_unite.Controls.Add(xr_o);
                x += xr_y.WidthF;
                //}

                y += xr_KAZANIM.HeightF;
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
            }
        }

        private void GroupFooter1_AfterPrint(object sender, EventArgs e)
        {
            if (DOWNLOAD == 1)
            {
                pages[os - 1] = (this.Pages.Count) - pagecount;
                pagecount = this.Pages.Count;
            }
        }

        private void KonuAnaliz_AfterPrint(object sender, EventArgs e)
        {
            if (DOWNLOAD == 1)
            {
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
