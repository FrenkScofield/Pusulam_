using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOAbideDerslerGrafik : DevExpress.XtraReports.UI.XtraReport
    {

        Font fontrow = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
        Font fontrownumeric = new System.Drawing.Font(new FontFamily("Tahoma"), 10, FontStyle.Regular);
        Font dersad = new System.Drawing.Font(new FontFamily("Tahoma"), 14, FontStyle.Bold);


        public GelisimRaporuOOAbideDerslerGrafik(DataTable dt1)
        {
            InitializeComponent();

            float x = 0;
            float y = 0;

            #region Grafiksel Gösterim (Tek grafikte tüm dersler)
            x = 0;
            DataTable DTGRAFIK = dt1;

            Font fontBaslik = new System.Drawing.Font(new FontFamily("Tahoma"), 20, FontStyle.Bold);
            Color titleblue = Color.FromArgb(68, 114, 196);

            XRLabel xrBaslik = new XRLabel()
            {
                WidthF = PageWidth,
                HeightF = 150,
                //Text = "AKADEMİK BECERİ DEĞERLENDİRME DERSLERİN DÜZEYLERİ",
                Text = "Up Grade-mid Sonuçlarına Göre Öğrenci Düzey Grafiği",
                Font = fontBaslik,
                ForeColor = titleblue,
                LocationF = new PointF(0, 0),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Multiline = true
            };

            ReportHeader.Controls.Add(xrBaslik);

            //y += 200f;

            XRPanel PANEL = new XRPanel();
            PANEL.LocationF = new PointF(50, y);
            PANEL.SizeF = new SizeF(1600f, 260f);
            //PANEL.SizeF = new SizeF(696f, 260f);
            PANEL.BorderColor = Color.Black;
            PANEL.BorderWidth = 1;
            PANEL.Borders = DevExpress.XtraPrinting.BorderSide.All;
            PANEL.CanGrow = true;

            DataView view = new DataView(DTGRAFIK);
            DataTable distinctValuesDers = view.ToTable(true, "DERS");
            XRChart CHART = new XRChart();
            CHART.SizeF = new SizeF(1500f, 220f);
            //CHART.SizeF = new SizeF(600f, 220f);
            CHART.BackColor = Color.White;
            double max = 0d;
            float y2 = 0f;

            y += 260f;

            if (DTGRAFIK.Columns.Contains("SINAV"))
            {
                DataTable distinctValues = view.ToTable(true, "SINAV");
                for (int J = 0; J < distinctValues.Rows.Count; J++)
                {
                    DataTable DTGRAFIKSINAV = DTGRAFIK.Select("SINAV= '" + distinctValues.Rows[J]["SINAV"] + "'").CopyToDataTable();

                    Series srs = new Series(distinctValues.Rows[J]["SINAV"].ToString(), ViewType.Bar);
                    for (int K = 0; K < DTGRAFIKSINAV.Rows.Count; K++)
                    {
                        srs.Points.Add(new SeriesPoint(DTGRAFIKSINAV.Rows[K]["DERS"].ToString(), Convert.ToDouble(DTGRAFIKSINAV.Rows[K]["DUZEY"].ToString())));

                        if (Convert.ToDouble(DTGRAFIKSINAV.Rows[K]["MAXDUZEY"].ToString()) > max)
                        {
                            max = Convert.ToDouble(DTGRAFIKSINAV.Rows[K]["MAXDUZEY"].ToString());
                        }
                    }

                    CHART.Series.Add(srs);
                }


                //float width = 600f / (distinctValuesDers.Rows.Count + 1);
                //float x2 = (696 - (distinctValuesDers.Rows.Count + 1) * width) / 2;

                float width = 1500f / (distinctValuesDers.Rows.Count + 1);
                float x2 = (1600f - (distinctValuesDers.Rows.Count + 1) * width) / 2;

                y2 = CHART.HeightF + 90;
                List<string> dersList = new List<string>();

                for (int J = 0; J < distinctValues.Rows.Count; J++)
                {
                    XRLabel lblSinav = new XRLabel();
                    lblSinav.Text = distinctValues.Rows[J]["SINAV"].ToString();
                    lblSinav.LocationF = new PointF((1600f - (distinctValuesDers.Rows.Count + 1) * width) / 2, y2);
                    //lblSinav.LocationF = new PointF((696 - (distinctValuesDers.Rows.Count + 1) * width) / 2, y2);
                    lblSinav.SizeF = new SizeF(width, 30);
                    lblSinav.Font = fontrownumeric;
                    lblSinav.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lblSinav.BorderWidth = 0.5f;
                    PANEL.Controls.Add(lblSinav);

                    int sira = 1;
                    for (int K = 0; K < distinctValuesDers.Rows.Count; K++)
                    {
                        string ders = distinctValuesDers.Rows[K]["DERS"].ToString();

                        for (int L = 0; L < dersList.Count; L++)
                        {
                            if (ders == dersList[L])
                            {
                                sira = L + 1;
                            }
                        }
                        var DTGRAFIKSINAVX = DTGRAFIK.Select("SINAV= '" + distinctValues.Rows[J]["SINAV"] + "' AND DERS='" + distinctValuesDers.Rows[K]["DERS"] + "'");
                        if (DTGRAFIKSINAVX.Length > 0)
                        {
                            DataTable DTGRAFIKSINAV = DTGRAFIKSINAVX.CopyToDataTable();

                            XRLabel lblDersDuzey = new XRLabel();
                            lblDersDuzey.Text = DTGRAFIKSINAV.Rows[0]["DUZEY"].ToString();
                            lblDersDuzey.LocationF = new PointF(x2 + (width * sira), y2);
                            lblDersDuzey.SizeF = new SizeF(width, 30);
                            lblDersDuzey.Font = fontrownumeric;
                            lblDersDuzey.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            lblDersDuzey.BorderWidth = 0.5f;
                            lblDersDuzey.Borders = DevExpress.XtraPrinting.BorderSide.All;
                            PANEL.Controls.Add(lblDersDuzey);

                            XRLabel lblDers = new XRLabel();
                            lblDers.Text = ders;
                            lblDers.LocationF = new PointF(x2 + (width * sira), CHART.HeightF + 60);
                            lblDers.SizeF = new SizeF(width, 30);
                            lblDers.Font = fontrownumeric;
                            lblDers.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            lblDers.BorderWidth = 0.5f;
                            lblDers.Borders = DevExpress.XtraPrinting.BorderSide.All;
                            PANEL.Controls.Add(lblDers);

                        }
                        else
                        {
                            XRLabel lblDersDuzey = new XRLabel();
                            lblDersDuzey.Text = "";
                            lblDersDuzey.LocationF = new PointF(x2 + (width * sira), y2);
                            lblDersDuzey.SizeF = new SizeF(width, 30);
                            lblDersDuzey.Font = fontrownumeric;
                            lblDersDuzey.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            lblDersDuzey.BorderWidth = 0.5f;
                            lblDersDuzey.Borders = DevExpress.XtraPrinting.BorderSide.All;
                            PANEL.Controls.Add(lblDersDuzey);
                        }
                        dersList.Add(ders);
                        sira++;
                    }

                    y2 += 30;
                }
                //string title = "DERSLERİN DÜZEYLERİ";
                //SizeF sizex = PrintingSystem.Graph.MeasureString(title, dersad);
                //
                //XRLabel lblTitle = new XRLabel();
                //lblTitle.Text = title;
                //lblTitle.LocationF = new PointF((1600f - sizex.Width) / 2, 30);
                ////lblTitle.LocationF = new PointF((696f - sizex.Width) / 2, 30);
                //lblTitle.SizeF = new SizeF(sizex.Width, 30);
                //lblTitle.Font = fontrow;
                //lblTitle.Multiline = false;
                //lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //lblTitle.BorderWidth = 0;
                //PANEL.Controls.Add(lblTitle);


                CHART.LocationF = new PointF((1600f - 1500f) / 2, 60);
                //CHART.LocationF = new PointF((696f - 600f) / 2, 60);
                PANEL.Controls.Add(CHART);
                XYDiagram xyd = CHART.Diagram as XYDiagram;
                xyd.DefaultPane.BackColor = Color.FromArgb(0, 0, 0, 0);
                xyd.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;

                CHART.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                CHART.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                CHART.Legend.Direction = LegendDirection.LeftToRight;

                xyd.AxisY.Title.Text = "Düzeyler";
                xyd.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                xyd.AxisY.WholeRange.SetMinMaxValues(0, 6);
                xyd.AxisY.NumericScaleOptions.AutoGrid = false;
                xyd.AxisY.NumericScaleOptions.GridSpacing = 1;
                xyd.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;


                XRLabel lblBos = new XRLabel();
                lblBos.Text = "";
                lblBos.LocationF = new PointF(0, y2);
                lblBos.SizeF = new SizeF(1600f, 30f);
                //lblBos.SizeF = new SizeF(696, 30);
                lblBos.Font = fontrow;
                lblBos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                lblBos.BorderWidth = 0;
                PANEL.Controls.Add(lblBos);

                Detail.Controls.Add(PANEL);

                y += PANEL.HeightF;

                y += 50;
            }

            XRPageBreak br = new XRPageBreak();
            br.LocationF = new PointF(0, y);
            Detail.Controls.Add(br);

            #endregion

        }

    }
}
