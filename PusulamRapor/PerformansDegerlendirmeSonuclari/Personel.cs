using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;
using System;
using DevExpress.XtraCharts;

namespace PusulamRapor.PerformansDegerlendirmeSonuclari
{
    public partial class Personel : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_SUBE;
        string ID_KADEME;
        string ID_KULLANICITIPI;
        string TCKIMLIKNOLIST;
        string ID_DEGERLENDIRMEPERIYOT;

        Font fontrow = new System.Drawing.Font(new FontFamily("Times New Roman"), 9.75f, FontStyle.Regular);
        DataSet ds;

        public Personel(string TCKIMLIKNO, string OTURUM, string ID_SUBE, string ID_KADEME, string ID_KULLANICITIPI, string TCKIMLIKNOLIST, string ID_DEGERLENDIRMEPERIYOT)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_SUBE = ID_SUBE;
            this.ID_KADEME = ID_KADEME;
            this.ID_KULLANICITIPI = ID_KULLANICITIPI;
            this.TCKIMLIKNOLIST = TCKIMLIKNOLIST;
            this.ID_DEGERLENDIRMEPERIYOT = ID_DEGERLENDIRMEPERIYOT;
        }

        private void Personel_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TCPERSONELLIST", TCKIMLIKNOLIST);
                b.ParametreEkle("@ID_DEGERLENDIRMEPERIYOT", ID_DEGERLENDIRMEPERIYOT);
                b.ParametreEkle("@ID_KULLANICITIPI", ID_KULLANICITIPI);
                b.ParametreEkle("@ID_KADEME", ID_KADEME);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBE);
                b.ParametreEkle("@ID_MENU", 1158);
                b.ParametreEkle("@ISLEM", 23);
                ds = b.SorguGetir("sp_Degerlendirme");

                //GroupField ogrenciField = new GroupField("SIRA");
                //GroupHeader1.GroupFields.Add(ogrenciField);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataView dv = ds.Tables[0].DefaultView;
                    dv.Sort = "SIRA ASC";
                    DataTable distinctPersonel = dv.ToTable();
                    this.DataSource = distinctPersonel;
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TCKIMLIKNO = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            ADSOYAD.Text = GetCurrentColumnValue("ADSOYAD").ToString();
            TCNO.Text = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            KADEME.Text = GetCurrentColumnValue("KADEME").ToString();
            KULLANICITIPI.Text = GetCurrentColumnValue("KULLANICITIPI").ToString();
            if (ds.Tables[3].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").Length > 0)
            {
                DataTable DTGENELGENELPUAN = ds.Tables[3].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").CopyToDataTable();
                GENELSIRA.Text = DTGENELGENELPUAN.Rows[0]["SIRA"].ToString();
                GENELPUAN.Text = DTGENELGENELPUAN.Rows[0]["PUAN"].ToString();
            }
            else
            {
                GENELSIRA.Text = "";
            }
            xrChart1.Series.Clear();
            #region PUANSIRA
            {
                xrPanel2.Controls.Clear();
                if (ds.Tables[1].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").Length > 0)
                {
                    DataTable DTPUAN = ds.Tables[1].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").CopyToDataTable();
                    float Y = 0;

                    Series sKendi = new Series("KENDI", ViewType.Bar);
                    //Series sSube = new Series("SUBE", ViewType.Bar);
                    Series sGenel = new Series("GENEL", ViewType.Bar);

                    foreach (DataRow item in DTPUAN.Rows)
                    {
                        string bolum = item["BOLUM"].ToString();
                        float X = 0f;

                        float rowheight = ((bolum.Length / 75f) < 1 ? 1 : (bolum.Length / 75f)) * 40f;
                        Y += 1;
                        XRPanel panel = new XRPanel()
                        {
                            WidthF = 787,
                            HeightF = rowheight,
                            LocationF = new PointF(X, Y),
                            KeepTogether = true,
                        };

                        XRLabel lblBolum = new XRLabel()
                        {
                            Text = bolum,
                            WidthF = 496.26f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblBolum);
                        X += lblBolum.WidthF;

                        XRLabel lblKendi = new XRLabel()
                        {
                            Text = item["KENDI"].ToString(),
                            WidthF = 90f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblKendi);
                        X += lblKendi.WidthF;

                        //XRLabel lblKampus = new XRLabel()
                        //{
                        //    Text = item["SUBE"].ToString(),
                        //    WidthF = 65.4f,
                        //    HeightF = rowheight,
                        //    BackColor = Color.White,
                        //    ForeColor = Color.Black,
                        //    CanGrow = false,
                        //    LocationF = new PointF(X, 0),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderWidth = 1,
                        //    BorderColor = Color.Black,
                        //    KeepTogether = true,
                        //    Tag = "0"
                        //};
                        //panel.Controls.Add(lblKampus);
                        //X += lblKampus.WidthF;

                        XRLabel lblGenel = new XRLabel()
                        {
                            Text = item["GENEL"].ToString(),
                            WidthF = 81.5f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblGenel);
                        X += lblGenel.WidthF;

                        //XRLabel lblSubeSira = new XRLabel()
                        //{
                        //    Text = item["SUBESIRA"].ToString(),
                        //    WidthF = 65.4F,
                        //    HeightF = rowheight,
                        //    BackColor = Color.White,
                        //    ForeColor = Color.Black,
                        //    CanGrow = false,
                        //    LocationF = new PointF(X, 0),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderWidth = 1,
                        //    BorderColor = Color.Black,
                        //    KeepTogether = true,
                        //    Tag = "0"
                        //};
                        //panel.Controls.Add(lblSubeSira);
                        //X += lblSubeSira.WidthF;

                        XRLabel lblGenelSira = new XRLabel()
                        {
                            Text = item["GENELSIRA"].ToString(),
                            WidthF = 119.24F,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblGenelSira);
                        X += lblGenelSira.WidthF;

                        Y += rowheight;

                        xrPanel2.Controls.Add(panel);

                        double KENDI = Convert.ToSingle(item["KENDI"]);
                        //double SUBE = Convert.ToSingle(item["SUBE"]);
                        double GENEL = Convert.ToSingle(item["GENEL"]);

                        string bolumkisa = bolum.Length > 12 ? bolum.Substring(0, 12) + "..." : bolum;

                        sKendi.Points.Add(new SeriesPoint(bolumkisa, KENDI));
                        //sSube.Points.Add(new SeriesPoint(bolumkisa, SUBE));
                        sGenel.Points.Add(new SeriesPoint(bolumkisa, GENEL));
                    }

                    sKendi.Label.TextPattern = "{V:0.00}";
                    //sSube.Label.TextPattern = "{V:0.00}";
                    sGenel.Label.TextPattern = "{V:0.00}";

                    ((BarSeriesLabel)sKendi.Label).Position = BarSeriesLabelPosition.Top;
                    //((BarSeriesLabel)sSube.Label).Position = BarSeriesLabelPosition.Top;
                    ((BarSeriesLabel)sGenel.Label).Position = BarSeriesLabelPosition.Top;
                    xrChart1.Series.Add(sGenel);
                    //xrChart1.Series.Add(sSube);
                    xrChart1.Series.Add(sKendi);
                    xrChart1.HeightF = sKendi.Points.Count * 3 * 28 > 1073 ? 1073 : sKendi.Points.Count * 3 * 28;
                }

            }
            #endregion

            #region YORUM
            {
                xrPanel5.Controls.Clear();
                if (ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").Length > 0)
                {
                    DataTable DTYORUM = ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "'").CopyToDataTable();
                    float Y = 0;
                    foreach (DataRow item in DTYORUM.Rows)
                    {
                        string yorum = item["YORUM"].ToString();
                        float X = 0f;

                        float rowheight = ((yorum.Length / 75f) < 1 ? 1 : (yorum.Length / 75f)) * 40f;
                        Y += 1;
                        XRPanel panel = new XRPanel()
                        {
                            WidthF = 787,
                            HeightF = rowheight,
                            LocationF = new PointF(X, Y),
                            KeepTogether = true,
                        };

                        XRLabel lblAdSoyad = new XRLabel()
                        {
                            Text = item["ADSOYAD"].ToString(),
                            WidthF = 217.75f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblAdSoyad);
                        X += lblAdSoyad.WidthF;

                        XRLabel lblYorum = new XRLabel()
                        {
                            Text = yorum,
                            WidthF = 438.11f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblYorum);
                        X += lblYorum.WidthF;

                        XRLabel lblTarih = new XRLabel()
                        {
                            Text = item["TARIH"].ToString(),
                            WidthF = 131.14f,
                            HeightF = rowheight,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, 0),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        panel.Controls.Add(lblTarih);
                        X += lblTarih.WidthF;

                        Y += rowheight;

                        xrPanel5.Controls.Add(panel);
                    }
                }
            }
            #endregion
        }
    }
}
