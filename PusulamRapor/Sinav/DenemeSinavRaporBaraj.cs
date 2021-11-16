using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavRaporBaraj : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_MENU { get; set; }

        public int ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINIF { get; set; }
        public int LIMIT { get; set; }
        public Color[] colors = {  Color.FromArgb(182,221,232), Color.FromArgb(218,238,243), Color.FromArgb(178,161,199), Color.FromArgb(204,192,217),
                                   Color.FromArgb(229,223,236), Color.FromArgb(155,187,89), Color.FromArgb(194,214,155), Color.FromArgb(214,227,188)};
        public Color total = Color.FromArgb(63, 63, 63);

        DataSet ds;

        public DenemeSinavRaporBaraj(string tckimlikno, string oturum, string ID_SINAV, string ID_SUBE, string ID_KADEME3, string ID_SINIF, string LIMIT)
        {
            InitializeComponent();
            this.TCKIMLIKNO = tckimlikno;
            this.OTURUM = oturum;
            this.ID_SINAV = Convert.ToInt32(ID_SINAV);
            this.ID_SUBE = Convert.ToInt32(ID_SUBE);
            this.ID_KADEME3 = Convert.ToInt32(ID_KADEME3);
            this.ID_SINIF = Convert.ToInt32(ID_SINIF);
            this.LIMIT = Convert.ToInt32(LIMIT);
            this.ID_MENU = 1074;

            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", this.TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", this.OTURUM);
                    b.ParametreEkle("@ID_SINAV", this.ID_SINAV);
                    b.ParametreEkle("@ID_SUBE", this.ID_SUBE);
                    b.ParametreEkle("@ID_KADEME3", this.ID_KADEME3);
                    b.ParametreEkle("@ID_SINIF", this.ID_SINIF);
                    b.ParametreEkle("@ID_MENU", this.ID_MENU);
                    b.ParametreEkle("@LIMIT", this.LIMIT);
                    b.ParametreEkle("@ISLEM", 3); // Rapor

                    ds = b.SorguGetir("sp_DenemeSinavRaporlari");
                    DataTable dt = ds.Tables[0];
                    SINAVADI.Text = ds.Tables[1].Rows[0][0].ToString();
                    SINAVTARIHI.Text = ds.Tables[1].Rows[0][1].ToString();
                    SINAVBASLIK.Text = ds.Tables[1].Rows[0][2].ToString().Replace("PT-", "");

                    float x = 2; float y = 61;
                    float x2 = 0;
                    float x3 = 0;
                    float xmarg = 2;
                    bool girme = false;
                    int ptcount = getPtCount(dt.Columns);

                    xrPanel_UST.WidthF = (dt.Columns.Count - 4) * 102;
                    xrPanel_UST2.WidthF = ((dt.Columns.Count - 4) * 102) + 530;
                    SINAVBASLIK.WidthF = xrPanel_UST.WidthF;

                    for (int i = 4; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ToString().Contains("DR-"))
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 100,
                                HeightF = 50,
                                Text = dt.Columns[i].ToString().ToUpper().Replace("DR-", ""),
                                Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                                LocationF = new PointF(x, y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                BackColor = dt.Columns[i].ToString().Equals("TOPLAM") ? total : colors[(i - 4) % 8],
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrPanel_UST.Controls.Add(xrOgrenciAnahtarDers);
                        }
                        else if (dt.Columns[i].ToString().Contains("PT-") && !girme)
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 102 * ptcount,
                                HeightF = 50,
                                Text = "PUAN",
                                Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                                LocationF = new PointF(x, y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                BackColor = Color.FromArgb(217, 226, 243),
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrPanel_UST.Controls.Add(xrOgrenciAnahtarDers);
                            girme = true;
                        }

                    }



                    XRLabel xrOgrenciAnahtarDers2 = new XRLabel()
                    {
                        WidthF = 60,
                        HeightF = 30,
                        Text = dt.Columns[0].ToString().ToUpper(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x2, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x2 += xrOgrenciAnahtarDers2.WidthF + xmarg;
                    xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers2);

                    xrOgrenciAnahtarDers2 = new XRLabel()
                    {
                        WidthF = 120,
                        HeightF = 30,
                        Text = dt.Columns[1].ToString().ToUpper(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x2, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x2 += xrOgrenciAnahtarDers2.WidthF + xmarg;
                    xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers2);

                    xrOgrenciAnahtarDers2 = new XRLabel()
                    {
                        WidthF = 120,
                        HeightF = 30,
                        Text = dt.Columns[2].ToString().ToUpper(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x2, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x2 += xrOgrenciAnahtarDers2.WidthF + xmarg;
                    xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers2);
                    xrOgrenciAnahtarDers2 = new XRLabel()
                    {
                        WidthF = 224,
                        HeightF = 30,
                        Text = dt.Columns[3].ToString().ToUpper(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x2, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x2 += xrOgrenciAnahtarDers2.WidthF + xmarg;
                    xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers2);

                    for (int i = 4; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ToString().Contains("DR-"))
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 100,
                                HeightF = 30,
                                Text = "NET",
                                Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                                LocationF = new PointF(x2, 0),
                                BackColor = dt.Columns[i].ToString().Equals("TOPLAM") ? total : colors[(i - 4) % 8],
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x2 += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers);
                        }
                        else if (dt.Columns[i].ToString().Contains("PT-"))
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 100,
                                HeightF = 30,
                                Text = dt.Columns[i].ToString().ToUpper().Replace("PT-", ""),
                                Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                                LocationF = new PointF(x2, 0),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x2 += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers);
                        }

                    }

                    XRLabel xrOgrenciAnahtarDers3 = new XRLabel()
                    {
                        WidthF = 60,
                        HeightF = 30,
                        Name = dt.Columns[0].ToString(),
                        Text = dt.Columns[0].ToString(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F),
                        LocationF = new PointF(x3, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x3 += xrOgrenciAnahtarDers3.WidthF + xmarg;
                    xrOgrenciAnahtarDers3.Tag = 1;
                    Detail.Controls.Add(xrOgrenciAnahtarDers3);

                    xrOgrenciAnahtarDers3 = new XRLabel()
                    {
                        WidthF = 120,
                        HeightF = 30,
                        Name = dt.Columns[1].ToString(),
                        Text = dt.Columns[1].ToString(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F),
                        LocationF = new PointF(x3, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x3 += xrOgrenciAnahtarDers3.WidthF + xmarg;
                    xrOgrenciAnahtarDers3.Tag = 1;
                    Detail.Controls.Add(xrOgrenciAnahtarDers3);

                    xrOgrenciAnahtarDers3 = new XRLabel()
                    {
                        WidthF = 120,
                        HeightF = 30,
                        Name = dt.Columns[2].ToString(),
                        Text = dt.Columns[2].ToString(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F),
                        LocationF = new PointF(x3, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x3 += xrOgrenciAnahtarDers3.WidthF + xmarg;
                    xrOgrenciAnahtarDers3.Tag = 1;
                    Detail.Controls.Add(xrOgrenciAnahtarDers3);
                    xrOgrenciAnahtarDers3 = new XRLabel()
                    {
                        WidthF = 224,
                        HeightF = 30,
                        Name = dt.Columns[3].ToString(),
                        Text = dt.Columns[3].ToString(),
                        Font = new System.Drawing.Font("Times New Roman", 8.0F),
                        LocationF = new PointF(x3, 0),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x3 += xrOgrenciAnahtarDers3.WidthF + xmarg;
                    xrOgrenciAnahtarDers3.Tag = 1;
                    Detail.Controls.Add(xrOgrenciAnahtarDers3);

                    for (int i = 4; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ToString().Contains("DR-"))
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 100,
                                HeightF = 30,
                                Name = dt.Columns[i].ToString(),
                                Text = dt.Columns[i].ToString(),
                                Font = new System.Drawing.Font("Times New Roman", 8.0F),
                                LocationF = new PointF(x3, 0),
                                BackColor = dt.Columns[i].ToString().Equals("TOPLAM") ? total : colors[(i - 4) % 8],
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x3 += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrOgrenciAnahtarDers.Tag = 1;
                            Detail.Controls.Add(xrOgrenciAnahtarDers);
                        }
                        else if (dt.Columns[i].ToString().Contains("PT-"))
                        {
                            XRLabel xrOgrenciAnahtarDers = new XRLabel()
                            {
                                WidthF = 100,
                                HeightF = 30,
                                Name = dt.Columns[i].ToString(),
                                Text = dt.Columns[i].ToString(),
                                Font = new System.Drawing.Font("Times New Roman", 8.0F),
                                LocationF = new PointF(x3, 0),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                ForeColor = System.Drawing.Color.Black,
                                BorderColor = Color.Black,
                            };
                            x3 += xrOgrenciAnahtarDers.WidthF + xmarg;
                            xrOgrenciAnahtarDers.Tag = 1;
                            Detail.Controls.Add(xrOgrenciAnahtarDers);
                        }

                    }
                    this.DataSource = dt;
                    FillReportDataFields.Fill(Detail, dt);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int getPtCount(DataColumnCollection columns)
        {
            int i = 0;
            for (int j = 0; j < columns.Count; j++)
            {
                if (columns[j].ToString().Contains("PT-"))
                {
                    i++;
                }
            }
            return i;
        }
    }
}
