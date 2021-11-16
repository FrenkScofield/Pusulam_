using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.YYS
{
    public partial class YYSTopluKarne : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_YYSSINAVDOSYALIST { get; set; }

        public DataTable dt1 { get; set; }
        public DataTable dt2 { get; set; }

        public YYSTopluKarne(string tc, string oturum, string idYysSinavDosyaList)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_YYSSINAVDOSYALIST = idYysSinavDosyaList;

        }

        private void YYSTopluKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_YYSSINAVDOSYALIST", ID_YYSSINAVDOSYALIST);
                b.ParametreEkle("@ISLEM", 9); // Rapor
                b.ParametreEkle("@ID_MENU", 1328);

                DataSet ds = b.SorguGetir("sp_YYS");

                dt1 = ds.Tables[0]; // ÖĞRENCİ LİSTESİ
                dt2 = ds.Tables[1]; // PUAN LİSTESİ

                GroupField gr = new GroupField("ID_YYSOGRENCI");
                GroupHeader1.GroupFields.Add(gr);
                this.DataSource = dt1;
                FillReportDataFields.Fill(Detail, dt1);

            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int idYysOgrenci = Convert.ToInt32(GetCurrentColumnValue("ID_YYSOGRENCI").ToString());

            DataTable dt = dt2.Select(String.Format("ID_YYSOGRENCI={0}", idYysOgrenci)).CopyToDataTable();
            xrChart1.Series.Clear();
            Series srsYuzdeGenel = new Series("", ViewType.Bar);

            int maxPuan = 0;

            for (int i = 1; i < 5; i++)
            {
                DataRow dr = dt.Select(String.Format("DERSNO={0}", i)).CopyToDataTable().Rows[0];

                string Ders = dr["DERSAD"].ToString();
                string Duzey = dr["DUZEY"].ToString();
                string Aciklama = dr["ACIKLAMA"].ToString();

                switch (i)
                {
                    case 1:
                        lblDers1.Text = Ders;
                        lblDuzey1.Text = Duzey;
                        lblAciklama1.Text = Aciklama;
                        break;
                    case 2:
                        lblDers2.Text = Ders;
                        lblDuzey2.Text = Duzey;
                        lblAciklama2.Text = Aciklama;
                        break;
                    case 3:
                        lblDers3.Text = Ders;
                        lblDuzey3.Text = Duzey;
                        lblAciklama3.Text = Aciklama;
                        break;
                    case 4:
                        lblDers4.Text = Ders;
                        lblDuzey4.Text = Duzey;
                        lblAciklama4.Text = Aciklama;
                        break;
                    default:
                        break;
                }
                int puan = Convert.ToInt32(dr["PUAN"].ToString());
                maxPuan = maxPuan > puan ? maxPuan : puan;
                srsYuzdeGenel.Points.Add(new SeriesPoint(Ders, dr["PUAN"].ToString()));

            }

            xrChart1.Series.Add(srsYuzdeGenel);

            NumericScaleOptions numericScaleOptions = ((XYDiagram)xrChart1.Diagram).AxisY.NumericScaleOptions;

            numericScaleOptions.MeasureUnit = NumericMeasureUnit.Ones;
            numericScaleOptions.GridOffset = 5;
            numericScaleOptions.AggregateFunction = AggregateFunction.Average;
            numericScaleOptions.GridAlignment = NumericGridAlignment.Ones;
            numericScaleOptions.GridSpacing = 1;

        }
    }
}
