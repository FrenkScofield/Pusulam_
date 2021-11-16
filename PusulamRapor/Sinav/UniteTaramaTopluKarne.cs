using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;

namespace PusulamRapor.Sinav
{
    public partial class UniteTaramaTopluKarne : DevExpress.XtraReports.UI.XtraReport
    {
        Font font12b = new Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
        Font font12r = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
        DataSet ds;
        public UniteTaramaTopluKarne(string tc, string oturum, string idsiniflist, string id)
        {
            InitializeComponent();
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", tc);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_SINIFLIST", idsiniflist);
                b.ParametreEkle("@ID_UNITETARAMASINAV", id);
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@ID_MENU", 1299);

                ds = b.SorguGetir("sp_UniteTaramaTopluKarne");

                GroupField gf = new GroupField("TCKIMLIKNO");
                GroupField gf2 = new GroupField("ADSOYAD");
                GroupHeader1.GroupFields.Add(gf2);
                GroupHeader1.GroupFields.Add(gf);

                //gf.SortOrder = XRColumnSortOrder.Descending;
                //Detail.SortFields.Add(gf);




                DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[1], "ADSOYAD");

                this.DataSource = dt;
                FillReportDataFields.Fill(GroupHeader1, dt);
                FillReportDataFields.Fill(Detail, dt);

            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string tc = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            if (ds.Tables[1].Select("TCKIMLIKNO='" + tc + "'").Length > 0)
            {
                DataTable dt = ds.Tables[1].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable();

                string base64String = dt.Rows[0]["FOTOGRAF"].ToString();
                if (base64String != "")
                {
                    try
                    {
                        Image img = PublicMetods.ByteArrayToImage((byte[])dt.Rows[0]["FOTOGRAF"]);
                        xrPictureBox1.Image = img;
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string tc = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            xrChart1.Series.Clear();

            Series srsYuzdeGenel = new Series("", ViewType.Bar);


            BarSeriesView sv = (BarSeriesView)srsYuzdeGenel.View;
            sv.BarWidth = 0.10;

            foreach (DataRow item in ds.Tables[2].Select("TCKIMLIKNO='" + tc + "'").CopyToDataTable().Rows)
            {
                SeriesPoint point = new SeriesPoint(item["SINAVAD"].ToString(), Convert.ToDouble(item["YUZDE"]));
                srsYuzdeGenel.Points.Add(point);
            }

            #region Series Label
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
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
        }

    }
}
