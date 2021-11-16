using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;
using System.Collections.Generic;
using System.Web;
using System.IO;
using DevExpress.Compression;

namespace PusulamRapor.Sinav
{
    public partial class TopluSinavSonuclari : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SUBE { get; set; }
        public string ID_SINIF { get; set; }
        public string TC_OGRENCI { get; set; }
        public int KONUANALIZ { get; set; }
        public int DOWNLOAD { get; set; }
        public bool BURSPUAN { get; set; }
        public bool BURSSIRALAMA { get; set; }
        public bool BURSORANI { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        readonly FontFamily familyArial = new FontFamily("Arial");
        public TopluSinavSonuclari(string tckimlikno, string oturum, string idSinav, string idSube, string idSinif, string tcOgrenci, string konuAnaliz, string download, string bursPuanBilgileri, string bursSiralamaBilgileri, string bursBursOrani)
        {
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SUBE = idSube;
            ID_SINIF = idSinif;
            TC_OGRENCI = tcOgrenci;
            KONUANALIZ = Convert.ToInt32(konuAnaliz);
            DOWNLOAD = Convert.ToInt32(download);

            BURSPUAN = Convert.ToInt16(bursPuanBilgileri) > 0;
            BURSSIRALAMA = Convert.ToInt16(bursSiralamaBilgileri) > 0;
            BURSORANI = Convert.ToInt16(bursBursOrani) > 0;

            InitializeComponent();
        }

        private void TopluSinavSonuclari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);
                b.ParametreEkle("@ID_SINIF", ID_SINIF);
                b.ParametreEkle("@ID_MENU", 1046);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                ds = b.SorguGetir("sp_TopluSinavSonuclari");
            }

            this.DataSource = ds.Tables[5];
            GroupField sinifField = new GroupField("SINIF");
            GroupField ogrenciField = new GroupField("ADSOYAD");
            GroupHeader1.GroupFields.Add(sinifField);
            GroupHeader1.GroupFields.Add(ogrenciField);

            xrLabel_AdSoyad.DataBindings.Add("Text", this.DataSource, "ADSOYAD");
            xrLabel_Sinif.DataBindings.Add("Text", this.DataSource, "SINIF");
            xrLabel_Sinav.DataBindings.Add("Text", this.DataSource, "SINAVAD");
            xrLabel_Tarih.DataBindings.Add("Text", this.DataSource, "SINAVTARIH");
            xrLabel_Kitapcik.DataBindings.Add("Text", this.DataSource, "KITAPCIK");
            xrLabel_Okul.DataBindings.Add("Text", this.DataSource, "SUBEAD");
            xrLabel_SinifKatilim.DataBindings.Add("Text", this.DataSource, "SINIFKATILIM");
            xrLabel_OkulKatilim.DataBindings.Add("Text", this.DataSource, "SUBEKATILIM");
            xrLabel_GenelKatilim.DataBindings.Add("Text", this.DataSource, "GENELKATILIM");
            xrLabel_SinavMain.DataBindings.Add("Text", this.DataSource, "SINAVTURU");
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string tc = GetCurrentColumnValue("TCKIMLIKNO").ToString();



            dt1 = ds.Tables[0].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();
            dt2 = ds.Tables[1].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();
            dt3 = ds.Tables[2].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();
            dt4 = ds.Tables[3].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();
            dt5 = ds.Tables[4].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();
            if (ds.Tables[6].Rows.Count > 0)
            {
                dt6 = ds.Tables[6].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();

                lblBursOrani.Visible = false;
                if (BURSORANI)
                {
                    int burstur = Convert.ToInt32(dt6.Rows[0]["BURSTUR"].ToString());
                    if (burstur == 1)
                    {
                        lblBursOrani.Visible = true;
                        lblBursOrani.Text = "Sınavda göstermiş olduğunuz başarıdan dolayı % " + dt6.Rows[0]["BURSORANI"].ToString() + " eğitim bursu almaya hak kazandınız.";
                    }
                    else if (burstur == 2)
                    {
                        lblBursOrani.Visible = true;
                        lblBursOrani.Text = dt6.Rows[0]["BURS_DERSAD"].ToString() + " Dersinde göstermiş olduğunuz başarıdan dolayı % " + dt6.Rows[0]["BURSORANI"].ToString() + " eğitim bursu almaya hak kazandınız.";
                    }
                }
            }



            dt5 = PublicMetods.orderBYtoTable(dt5, "BOLUMNO");

            xrLabel_SinavTuru1.Text = dt1.Rows[0]["SINAVTURUUZUN"].ToString();

            int idKademe = Convert.ToInt32(GetCurrentColumnValue("ID_KADEME").ToString());
            int idSinavTuru = Convert.ToInt32(dt1.Rows[0]["ID_SINAVTURU"].ToString());
            bool PUANGIZLE = Convert.ToBoolean(GetCurrentColumnValue("PUANGIZLE").ToString());
            if (!PUANGIZLE && (BURSPUAN || BURSSIRALAMA))
            {
                ssPuanSirala ssPS = new ssPuanSirala(dt1, BURSPUAN, BURSSIRALAMA);
                xrSubreport_ssPuanSirala.ReportSource = ssPS;
            }
            ssOgrenciAnalizi ssOA = new Sinav.ssOgrenciAnalizi(dt2, idKademe == 5, idSinavTuru == 12,false);
            xrSubreport_ssOgrenciAnaliz.ReportSource = ssOA;

            ssCevapAnahtari ssCA = new ssCevapAnahtari(dt3);
            xrSubreport_CevapAnahtari.ReportSource = ssCA;

            DataTable tableBolumler = new DataTable();
            tableBolumler = PublicMetods.orderBYtoTable(dt4, "BOLUMNO").DefaultView.ToTable(true, "DERSAD");

            xr_dersbasari.Series.Clear();


            Series srsYuzdeGenel = new Series("GENEL", ViewType.Bar);
            Series srsYuzdeSinif = new Series("SINIF", ViewType.Bar);
            Series srsYuzdeOgrenci = new Series("ÖĞRENCİ", ViewType.Bar);

            Series srsDogru = new Series("DOĞRU", ViewType.Bar);
            Series srsYanlis = new Series("YANLIŞ", ViewType.Bar);
            Series srsBos = new Series("BOŞ", ViewType.Bar);


            Font font = new Font(familyArial, 6, FontStyle.Bold);
            srsYuzdeGenel.Label.Font = font;
            srsYuzdeSinif.Label.Font = font;
            srsYuzdeOgrenci.Label.Font = font;
            srsDogru.Label.Font = font;
            srsYanlis.Label.Font = font;
            srsBos.Label.Font = font;

            foreach (DataRow item in PublicMetods.orderBYtoTable(dt4, "BOLUMNO").Rows)
            {
                srsYuzdeOgrenci.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["OGRENCI"].ToString())));
                srsYuzdeSinif.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["SINIF"].ToString())));
                srsYuzdeGenel.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["GENEL"].ToString())));
            }

            #region Series Label


            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;


            srsYuzdeSinif.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeSinif.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeSinif.Label.Border.Color = Color.Transparent;
            srsYuzdeSinif.Label.BackColor = Color.Transparent;
            srsYuzdeSinif.Label.TextColor = Color.DarkBlue;


            srsYuzdeOgrenci.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeOgrenci.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeOgrenci.Label.Border.Color = Color.Transparent;
            srsYuzdeOgrenci.Label.BackColor = Color.Transparent;
            srsYuzdeOgrenci.Label.TextColor = Color.DarkBlue;

            #endregion

            xr_dersbasari.Series.Add(srsYuzdeGenel);
            xr_dersbasari.Series.Add(srsYuzdeSinif);
            xr_dersbasari.Series.Add(srsYuzdeOgrenci);

            if (KONUANALIZ == 1)
            {
                float y = 0;
                XRPanel EklenecekPanel = new XRPanel();

                panel_unite.Controls.Clear();
                panel_unite1.Controls.Clear();

                int DataRowDon = dt5.Rows.Count;
                EklenecekPanel = panel_unite;
                bool GirdiBirkere = false;
                int Yarisi = (dt5.Rows.Count / 2) - 1;

                for (int i = 0; i < DataRowDon; i++)
                {
                    if (Yarisi >= i)
                    {
                        EklenecekPanel = panel_unite;
                    }
                    else
                    {
                        EklenecekPanel = panel_unite1;
                        if (GirdiBirkere == false)
                        {
                            y = 0;
                            GirdiBirkere = true;
                        }
                    }

                    float x = 0;
                    //Color BackC = dt5.Rows[i]["KOD"].ToString()== "" ? Color.Orange : Color.Transparent;
                    Color BackC = i % 2 == 0 ? Color.FromArgb(217, 226, 243) : Color.White;
                    Color BorderC = Color.FromArgb(180, 198, 231);


                    XRLabel xr_KAZANIM = new XRLabel()
                    {
                        Text = dt5.Rows[i]["KONUAD"].ToString(),
                        LocationF = new PointF(x, y),
                        WidthF = dt5.Rows[i]["KOD"].ToString() != "" ? 235 : 235 + 172,
                        Font = font,
                        HeightF = 15,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderColor = BorderC,
                        BackColor = dt5.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange,
                        Padding = 4,
                        KeepTogether = true,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                        CanGrow = true
                    };
                    EklenecekPanel.Controls.Add(xr_KAZANIM);

                    if (dt5.Rows[i]["KOD"].ToString().Length > 0)
                    {
                        x += xr_KAZANIM.WidthF;
                        XRLabel xr_ss = new XRLabel()
                        {
                            Text = dt5.Rows[i]["SORU"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_ss);
                        x += xr_ss.WidthF;

                        XRLabel xr_d = new XRLabel()
                        {
                            Text = dt5.Rows[i]["DOGRU"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_d);
                        x += xr_d.WidthF;
                        XRLabel xr_y = new XRLabel()
                        {
                            Text = dt5.Rows[i]["YANLIS"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_y);
                        x += xr_y.WidthF;
                        XRLabel xr_b = new XRLabel()
                        {
                            Text = dt5.Rows[i]["BOS"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_b);
                        x += xr_y.WidthF;
                        XRLabel xr_o = new XRLabel()
                        {
                            Text = dt5.Rows[i]["YUZDE"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 52,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_o);
                        x += xr_y.WidthF;
                    }

                    y += xr_KAZANIM.HeightF;
                }
                pnl_KanuAnaliz.Visible = true;
            }
            else
            {
                pnl_KanuAnaliz.Visible = false;
            }
        }


        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int idKademe = Convert.ToInt32(GetCurrentColumnValue("ID_KADEME").ToString());

            if (idKademe == 4) // ortaokul
            {
                pbOrtaOkul.Visible = true;
                pbLise.Visible = false;
            }
            else
            {
                pbOrtaOkul.Visible = false;
                pbLise.Visible = true;
            }
        }

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

        private void TopluSinavSonuclari_AfterPrint(object sender, EventArgs e)
        {
            if (DOWNLOAD == 1)
            {
                string[] sourcefiles = new string[os];
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/Temp/" + OTURUM + ""));
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
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/Temp/" + OTURUM + "/" + name + ".pdf");
                    sourcefiles[i] = path;
                    newReport.ExportToPdf(path);
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/" + OTURUM + ".zip"));
                }

                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (string file in sourcefiles)
                    {
                        archive.AddFile(file, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/" + OTURUM + ".zip"));
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/Temp/" + OTURUM + ""));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/SinavSonuc/Temp/" + OTURUM + ""), true);
            }
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
        }
    }
}
