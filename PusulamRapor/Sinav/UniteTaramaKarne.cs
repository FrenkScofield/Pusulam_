using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;

namespace PusulamRapor.Sinav
{
    public partial class UniteTaramaKarne : DevExpress.XtraReports.UI.XtraReport
    {
        Font font12b = new Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
        Font font12r = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
        public UniteTaramaKarne(string tc, string oturum, string tcogrenci, string id)
        {
            InitializeComponent();
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", tc);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@TC_OGRENCI", tcogrenci);
                b.ParametreEkle("@ID_UNITETARAMASINAV", id);
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@ID_MENU", 1259);

                DataSet ds = b.SorguGetir("sp_VeliUniteTaramaRaporu");

                //GroupField gf = new GroupField("SORUNO");
                //gf.SortOrder = XRColumnSortOrder.Descending;
                //Detail.SortFields.Add(gf);



                DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "DERSAD, SORUNO, ID_UNITETARAMASORU");
                this.DataSource = dt;
                FillReportDataFields.Fill(GroupHeader1, dt);
                FillReportDataFields.Fill(Detail, dt);
                FillReportDataFields.Fill(GroupFooter1, dt);


                #region CHART_SINAV
                xrChart1.Series.Clear();

                Series srsYuzdeGenel = new Series("", ViewType.Bar);


                BarSeriesView sv = (BarSeriesView)srsYuzdeGenel.View;
                sv.BarWidth = 0.10;

                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    SeriesPoint point = new SeriesPoint(item["SINAVAD"].ToString(), Convert.ToDouble(item["YUZDE"]));
                    srsYuzdeGenel.Points.Add(point);
                }

                #region Series Label
                srsYuzdeGenel.Label.Border.Color = Color.Transparent;
                srsYuzdeGenel.Label.BackColor = Color.Transparent;
                srsYuzdeGenel.Label.TextColor = Color.White;
                #endregion

                xrChart1.Series.Add(srsYuzdeGenel);

                XYDiagram diagram = (XYDiagram)xrChart1.Diagram;
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);

                //foreach (Series item in xrChart1.Series)
                //{
                //    item.Label.Font = font12b;
                //}

                xrChart1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                xrChart1.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                xrChart1.Legend.Font = font12b;
                #endregion


                #region CHART_DERS

                xrChart2.Series.Clear();

                Series srsOgr = new Series("Öğrenci Ort.", ViewType.Bar);
                Series srsSnf = new Series("Sınıf Ort.", ViewType.Bar);
                Series srsSub = new Series("Şube Ort.", ViewType.Bar);
                Series srsGnl = new Series("Genel Ort.", ViewType.Bar);
                foreach (DataRow ders in ds.Tables[2].Rows)
                {
                    srsOgr.Points.Add(new SeriesPoint(ders["DERSAD"].ToString(), Convert.ToDouble(ders["OGRENCI"])));
                    srsSnf.Points.Add(new SeriesPoint(ders["DERSAD"].ToString(), Convert.ToDouble(ders["SINIF"])));
                    srsSub.Points.Add(new SeriesPoint(ders["DERSAD"].ToString(), Convert.ToDouble(ders["SUBE"])));
                    srsGnl.Points.Add(new SeriesPoint(ders["DERSAD"].ToString(), Convert.ToDouble(ders["GENEL"])));
                }

                #region Series_Label

                srsOgr.Label.Border.Color = Color.Transparent;
                srsOgr.Label.BackColor = Color.Transparent;
                srsOgr.Label.TextColor = Color.White;

                srsSnf.Label.Border.Color = Color.Transparent;
                srsSnf.Label.BackColor = Color.Transparent;
                srsSnf.Label.TextColor = Color.White;

                srsSub.Label.Border.Color = Color.Transparent;
                srsSub.Label.BackColor = Color.Transparent;
                srsSub.Label.TextColor = Color.White;

                srsGnl.Label.Border.Color = Color.Transparent;
                srsGnl.Label.BackColor = Color.Transparent;
                srsGnl.Label.TextColor = Color.White;

                #endregion

                xrChart2.Series.Add(srsOgr);
                xrChart2.Series.Add(srsSnf);
                xrChart2.Series.Add(srsSub);
                xrChart2.Series.Add(srsGnl);

                XYDiagram diagram2 = (XYDiagram)xrChart2.Diagram;
                diagram2.AxisY.WholeRange.SetMinMaxValues(0, 100);

                //foreach (Series item in xrChart1.Series){ item.Label.Font = font12b; }

                xrChart2.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                xrChart2.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                xrChart2.Legend.Font = new Font(new FontFamily("Tahoma"), 10, FontStyle.Regular);

                #endregion

                string base64String = ds.Tables[0].Rows[0]["FOTOGRAF"].ToString();
                if (base64String != "")
                {
                    try
                    {
                        Image img = PublicMetods.ByteArrayToImage((byte[])ds.Tables[0].Rows[0]["FOTOGRAF"]);
                        xrPictureBox1.Image = img;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
    }
}
