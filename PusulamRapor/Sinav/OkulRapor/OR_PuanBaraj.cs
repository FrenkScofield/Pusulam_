using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_PuanBaraj : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable _dt1;
        DataTable _dt2;
        List<string> listDersUzun;
        List<string> listPuanTur;
        string _SINAVAD;
        string _SINAVTARIH;
        string _SUBEAD;
        int _LIMIT = 0;
        int puantursay = 0;
        public Color[] colors = {  Color.FromArgb(182,221,232), Color.FromArgb(218,238,243), Color.FromArgb(178,161,199), Color.FromArgb(204,192,217),
                                   Color.FromArgb(229,223,236), Color.FromArgb(155,187,89), Color.FromArgb(194,214,155), Color.FromArgb(214,227,188)};
        public Color total = Color.FromArgb(63, 63, 63);

        public OR_PuanBaraj(DataTable _dt1, DataTable _dt2, string _SUBEAD, string _SINAVAD, string _SINAVTARIH, List<string> listDersUzun, string _LIMIT)
        {
            InitializeComponent();
            this._dt1 = _dt1;
            this._dt2 = _dt2;
            this._SUBEAD = _SUBEAD;
            this._SINAVAD = _SINAVAD;
            this._SINAVTARIH = _SINAVTARIH;
            this.listDersUzun = listDersUzun;
            this._LIMIT = Convert.ToInt32(_LIMIT);
        }

        private void OR_PuanBaraj_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("OKUL", typeof(string));
            table1.Columns.Add("SINIF", typeof(string));
            table1.Columns.Add("ADSOYAD", typeof(string));

            DataView viewBolumNo = new DataView(_dt2);
            DataTable distinctBolumNo = viewBolumNo.ToTable(true, "BOLUMNO", "TAKMAAD");
            foreach (DataRow item in distinctBolumNo.Rows)
            {
                table1.Columns.Add(item["TAKMAAD"].ToString().ToUpper(), typeof(string));
            }

            //TOPLAM İÇİN
            table1.Columns.Add("TOPLAM", typeof(string));

            string BASLIK = "";
            listPuanTur = new List<string>();

            if (_dt1.Rows.Count > 0)
            {
                DataView viewPuanTur = new DataView(_dt1);
                DataTable distinctPuanTur = viewPuanTur.ToTable(true, "PUANTURU");

                puantursay = distinctPuanTur.Rows.Count;

                foreach (DataRow item in distinctPuanTur.Rows)
                {
                    table1.Columns.Add(item["PUANTURU"].ToString(), typeof(string));
                    if (!BASLIK.Contains(item["PUANTURU"].ToString()))
                    {
                        BASLIK += "" + item["PUANTURU"].ToString() + ", ";
                        listPuanTur.Add(item["PUANTURU"].ToString());
                    }
                }
            }


            DataRow newdr = table1.NewRow();
            double toplam = 0d;

            SINAVADI.Text = _SINAVAD;
            SINAVTARIHI.Text = _SINAVTARIH;
            BASLIK = BASLIK.Substring(0, BASLIK.Length - 2) + " Puan Türünde " + _LIMIT + " Puan Barajını Aşamayan Öğrenciler";
            SINAVBASLIK.Text = BASLIK;

            foreach (DataRow NET in _dt2.Rows)
            {
                if (Convert.ToInt32(NET["BOLUMNO"]) == 1)
                {
                    if (newdr["OKUL"].ToString().Length > 0)
                    {
                        newdr["TOPLAM"] = string.Format("{0:0.00}", toplam);
                        table1.Rows.Add(newdr);
                        toplam = 0f;
                    }

                    newdr = table1.NewRow();
                    newdr["OKUL"] = _SUBEAD;
                    newdr["SINIF"] = NET["SINIF"].ToString();
                    if (_dt1.Rows.Count > 0)
                    {
                        foreach (DataRow PUAN in _dt1.Select("TCKIMLIKNO='" + NET["TCKIMLIKNO"] + "'").CopyToDataTable().Rows)
                        {
                            newdr["ADSOYAD"] = PUAN["AD"].ToString() + " " + PUAN["SOYAD"].ToString();
                            newdr[PUAN["PUANTURU"].ToString()] = PUAN["PUAN"].ToString();
                        }
                    }
                }
                newdr[NET["TAKMAAD"].ToString().ToUpper()] = NET["NET"].ToString();
                toplam += Convert.ToDouble(NET["NET"]);
            }

            newdr["TOPLAM"] = string.Format("{0:0.00}", toplam);
            table1.Rows.Add(newdr);

            float width = ((float)xrPanel_UST.WidthF - 300f) / ((float)listDersUzun.Count + 1f);
            float x = 0f;
            float y = 60f;
            int i = 0;
            {
                foreach (string ders in listDersUzun)
                {
                    XRLabel xrOgrenciAnahtarDers = new XRLabel()
                    {
                        WidthF = width,
                        HeightF = 50,
                        Text = ders,
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x, y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BackColor = colors[i % 8],
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x += xrOgrenciAnahtarDers.WidthF;
                    xrPanel_UST.Controls.Add(xrOgrenciAnahtarDers);
                    i++;
                }

                XRLabel xrToplam = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 50,
                    Text = "TOPLAM",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = colors[i % 8],
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrToplam.WidthF;
                xrPanel_UST.Controls.Add(xrToplam);
                i++;

                XRLabel xrPuan = new XRLabel()
                {
                    WidthF = 300f,
                    HeightF = 50,
                    Text = "PUAN",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = colors[i % 8],
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrPuan.WidthF;
                xrPanel_UST.Controls.Add(xrPuan);

                i = 0;
                x = 0f;
                y = 0f;

                XRLabel xrOkul = new XRLabel()
                {
                    WidthF = 125f,
                    HeightF = 50f,
                    Text = "OKUL",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrOkul.WidthF;
                xrPanel_UST2.Controls.Add(xrOkul);

                XRLabel xrSinif = new XRLabel()
                {
                    WidthF = 125f,
                    HeightF = 50f,
                    Text = "SINIF",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrSinif.WidthF;
                xrPanel_UST2.Controls.Add(xrSinif);

                XRLabel xrAdSoyad = new XRLabel()
                {
                    WidthF = 150f,
                    HeightF = 50f,
                    Text = "ADSOYAD",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrAdSoyad.WidthF;
                xrPanel_UST2.Controls.Add(xrAdSoyad);

                foreach (string ders in listDersUzun)
                {
                    XRLabel xrOgrenciAnahtarDers = new XRLabel()
                    {
                        WidthF = width,
                        HeightF = 50,
                        Text = "NET",
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x, y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BackColor = colors[i % 8],
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x += xrOgrenciAnahtarDers.WidthF;
                    xrPanel_UST2.Controls.Add(xrOgrenciAnahtarDers);
                    i++;
                }

                XRLabel xrToplam2 = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 50,
                    Text = "NET",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = colors[i % 8],
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                };
                x += xrToplam2.WidthF;
                xrPanel_UST2.Controls.Add(xrToplam2);
                i++;


                float widthpt = 300f / (float)listPuanTur.Count;
                foreach (string puantur in listPuanTur)
                {
                    XRLabel xrPuanTur = new XRLabel()
                    {
                        WidthF = widthpt,
                        HeightF = 50,
                        Text = puantur,
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x, y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BackColor = Color.White,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                    };
                    x += xrPuanTur.WidthF;
                    xrPanel_UST2.Controls.Add(xrPuanTur);
                    i++;
                }
            }

            x = 0f;
            y = 0f;
            i = 0;

            {
                XRLabel xrOkul = new XRLabel()
                {
                    WidthF = 125f,
                    HeightF = 50,
                    Text = "OKUL",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                    Tag = 1
                };
                x += xrOkul.WidthF;
                Detail.Controls.Add(xrOkul);

                XRLabel xrSinif = new XRLabel()
                {
                    WidthF = 125f,
                    HeightF = 50,
                    Text = "SINIF",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                    Tag = 1
                };
                x += xrSinif.WidthF;
                Detail.Controls.Add(xrSinif);

                XRLabel xrAdSoyad = new XRLabel()
                {
                    WidthF = 150f,
                    HeightF = 50,
                    Text = "ADSOYAD",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = Color.White,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                    Tag = 1
                };
                x += xrAdSoyad.WidthF;
                Detail.Controls.Add(xrAdSoyad);

                foreach (string ders in listDersUzun)
                {
                    XRLabel xrOgrenciAnahtarDers = new XRLabel()
                    {
                        WidthF = width,
                        HeightF = 50,
                        Text = ders,
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x, y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BackColor = colors[i % 8],
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                        Tag = 1
                    };
                    x += xrOgrenciAnahtarDers.WidthF;
                    Detail.Controls.Add(xrOgrenciAnahtarDers);
                    i++;
                }

                XRLabel xrToplam2 = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 50,
                    Text = "TOPLAM",
                    Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                    LocationF = new PointF(x, y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    BackColor = colors[i % 8],
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.Black,
                    Tag = 1
                };
                x += xrToplam2.WidthF;
                Detail.Controls.Add(xrToplam2);
                i++;

                float widthpt = 300f / (float)listPuanTur.Count;
                foreach (string puantur in listPuanTur)
                {
                    XRLabel xrPuanTur = new XRLabel()
                    {
                        WidthF = widthpt,
                        HeightF = 50,
                        Text = puantur,
                        Font = new System.Drawing.Font("Times New Roman", 8.0F, (FontStyle)(FontStyle.Bold)),
                        LocationF = new PointF(x, y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BackColor = Color.White,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        ForeColor = System.Drawing.Color.Black,
                        BorderColor = Color.Black,
                        Tag = 1
                    };
                    x += xrPuanTur.WidthF;
                    Detail.Controls.Add(xrPuanTur);
                }
            }
            this.DataSource = table1;
            FillReportDataFields.Fill(Detail, table1);
        }
    }
}
