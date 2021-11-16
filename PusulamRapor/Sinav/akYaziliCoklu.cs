using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class akYaziliCoklu : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtDersListesi = new DataTable();
        DataTable dtYazili = new DataTable();
        string HANGI = "";
        int hangix = 0;
        int width = 696;
        public akYaziliCoklu(DataTable _dt1, DataTable _dt3, DataTable _dt4, string hangi)
        {
            InitializeComponent();
            dtDersListesi = _dt3;
            dtYazili = _dt4;
            HANGI = hangi;


            if (!HANGI.Contains("1. Dönem"))
            {
                b21.Visible = false;
                b22.Visible = false;
                b23.Visible = false;
                b24.Visible = false;
                b25.Visible = false;
                b26.Visible = false;
                b27.Visible = false;
                hangix = 2;
                b11.Text = "2. Dönem";
            }

            if (!HANGI.Contains("2. Dönem"))
            {
                b21.Visible = false;
                b22.Visible = false;
                b23.Visible = false;
                b24.Visible = false;
                b25.Visible = false;
                b26.Visible = false;
                b27.Visible = false;
                hangix = 1;
            }

            if (hangix != 0)
            {
                width = width * 2;
                b11.WidthF = width;
                b12.WidthF = width / 3;
                b13.WidthF = width / 3;
                b14.WidthF = width / 3;
                b13.LocationF = new PointF(b12.LocationF.X + width / 3, b12.LocationF.Y);
                b14.LocationF = new PointF(b13.LocationF.X + width / 3, b12.LocationF.Y);
            }

            this.DataSource = _dt1;
        }

        private void akYazili_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float LY = 0;
            float LX = 0;
            FontFamily ff = new FontFamily("Tahoma");
            foreach (DataRow ders in dtDersListesi.Rows)
            {
                try
                {
                    DataTable dt = dtYazili.Select("ID_DERS = " + ders["ID_DERS"].ToString()).CopyToDataTable();

                    LX = 0;
                    XRLabel xrSinavD = new XRLabel()
                    {
                        WidthF = 300,
                        HeightF = 20,
                        Text = ders["DERSAD"].ToString(),
                        Font = new Font(ff, 8, FontStyle.Bold),
                        BackColor = Color.Transparent,
                        ForeColor = Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.SkyBlue,
                        Tag = "Ogrt"
                    };
                    LX += xrSinavD.WidthF;
                    Detail.Controls.Add(xrSinavD);
                    int count = dt.Select("DONEMBILGI='1. Dönem'").Length;
                    int sinav = 0;
                    foreach (DataRow yazili in dt.Rows)
                    {
                        if (yazili["DONEMBILGI"].ToString().Equals("2. Dönem"))
                        {
                            if (hangix == 0)
                            {
                                if (sinav<3)
                                {
                                    for (int i = 0; i < 3 - count; i++)
                                    {
                                        XRLabel xrLabel = new XRLabel()
                                        {
                                            WidthF = width / 3,
                                            HeightF = 20,
                                            Text = "",
                                            Font = new Font(ff, 8, FontStyle.Bold),
                                            BackColor = Color.Transparent,
                                            ForeColor = Color.MidnightBlue,
                                            LocationF = new PointF(LX, LY),
                                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                                            BorderWidth = 1,
                                            BorderColor = Color.SkyBlue,
                                            Tag = "Ogrt"
                                        };
                                        LX += xrLabel.WidthF;
                                        Detail.Controls.Add(xrLabel);
                                        sinav++;
                                    }
                                }
                                
                            }

                            XRLabel xrSinav = new XRLabel()
                            {
                                WidthF = width / 3,
                                HeightF = 20,
                                Text = yazili["PUAN"].ToString(),
                                Font = new Font(ff, 8, FontStyle.Bold),
                                BackColor = Color.Transparent,
                                ForeColor = Color.MidnightBlue,
                                LocationF = new PointF(LX, LY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = Color.SkyBlue,
                                Tag = "Ogrt"
                            };
                            LX += xrSinav.WidthF;
                            Detail.Controls.Add(xrSinav);
                        }
                        else
                        {
                            XRLabel xrSinav = new XRLabel()
                            {
                                WidthF = width / 3,
                                HeightF = 20,
                                Text = yazili["PUAN"].ToString(),
                                Font = new Font(ff, 8, FontStyle.Bold),
                                BackColor = Color.Transparent,
                                ForeColor = Color.MidnightBlue,
                                LocationF = new PointF(LX, LY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = Color.SkyBlue,
                                Tag = "Ogrt"
                            };
                            LX += xrSinav.WidthF;
                            Detail.Controls.Add(xrSinav);
                        }
                        sinav++;
                    }
                    for (int i = sinav; i < (hangix != 0 ? 3 : 6); i++)
                    {

                        XRLabel xrSinav = new XRLabel()
                        {
                            WidthF = width / 3,
                            HeightF = 20,
                            Text = "",
                            Font = new Font(ff, 8, FontStyle.Bold),
                            BackColor = Color.Transparent,
                            ForeColor = Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.SkyBlue,
                            Tag = "Ogrt"
                        };
                        LX += xrSinav.WidthF;
                        Detail.Controls.Add(xrSinav);
                    }
                    LY += 20;
                }
                catch (Exception)
                {
                    LX += 232;
                }
            }
        }
    }
}
