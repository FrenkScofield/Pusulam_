using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraCharts;
using System.IO;

namespace PusulamRapor.Odev
{
    public partial class OdevOgrenciRapor : DevExpress.XtraReports.UI.XtraReport
    {
        Font font12b = new Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
        Font font12r = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
        public OdevOgrenciRapor(string tc, string oturum, string idodevtur, string idsinif, string tcogrenci, string baslangic, string bitis)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", tc);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_ODEVTUR", idodevtur);
                b.ParametreEkle("@ID_SINIF", idsinif);
                b.ParametreEkle("@TC_OGRENCI", tcogrenci);
                b.ParametreEkle("@BASLANGIC_TARIHI", baslangic);
                b.ParametreEkle("@BITIS_TARIHI", bitis);
                b.ParametreEkle("@ISLEM", 13);
                b.ParametreEkle("@ID_MENU", 1230);

                DataSet ds = b.SorguGetir("sp_Odev");

                this.DataSource = ds.Tables[2];
                FillReportDataFields.Fill(GroupHeader1, ds.Tables[0]);
                FillReportDataFields.Fill(Detail, ds.Tables[2]);

                //XRChart chart = new XRChart();
                //chart.LocationF = new PointF(600, 0);
                //chart.SizeF = new SizeF(300, 300);
                //chart.CanGrow = true;
                Series srsYuzdeGenel = new Series("", ViewType.Doughnut);

                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    SeriesPoint point = new SeriesPoint(item["ODEVDURUM"].ToString(), Convert.ToDouble(item["SAYI"]));

                    switch (Convert.ToInt32(item["ID_ODEVDURUM"]))
                    {
                        case 1:
                            point.Color = Color.LawnGreen;
                            break;
                        case 2:
                            point.Color = Color.Yellow;
                            break;
                        case 3:
                            point.Color = Color.Red;
                            break;
                        case 4:
                            point.Color = Color.LightGreen;
                            break;
                        case 5:
                            point.Color = Color.Purple;
                            break;
                    }

                    srsYuzdeGenel.Points.Add(point);
                }

                ((DoughnutSeriesLabel)srsYuzdeGenel.Label).Position = PieSeriesLabelPosition.TwoColumns;
                ((DoughnutSeriesLabel)srsYuzdeGenel.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
                ((DoughnutSeriesLabel)srsYuzdeGenel.Label).ResolveOverlappingMinIndent = 5;

                #region Series Label
                srsYuzdeGenel.Label.Border.Color = Color.Transparent;
                srsYuzdeGenel.Label.BackColor = Color.Transparent;
                srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
                srsYuzdeGenel.Label.TextPattern = "{A}: {VP:P2}";
                srsYuzdeGenel.LegendTextPattern = "{A}: {V:0}";
                #endregion

                xrChart1.Series.Add(srsYuzdeGenel);

                foreach (Series item in xrChart1.Series)
                {
                    item.Label.Font = font12b;
                }

                xrChart1.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
                xrChart1.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
                xrChart1.Legend.Font = font12b;
                //xrChart1.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;

                //Detail.Controls.Add(xrChart1);
                try
                {
                    string base64String = ds.Tables[0].Rows[0]["FOTOGRAF"].ToString();
                    if (base64String != "")
                    {
                        Image img = PublicMetods.ByteArrayToImage((byte[])ds.Tables[0].Rows[0]["FOTOGRAF"]);
                        xrPictureBox1.Image = img;
                    }

                }
                catch (Exception)
                {
                }
            }
        }
    }
}
