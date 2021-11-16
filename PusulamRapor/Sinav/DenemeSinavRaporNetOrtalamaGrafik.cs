using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavRaporNetOrtalamaGrafik : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        DataTable dt;
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_MENU { get; set; }
        public int ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINIF { get; set; }

        public DenemeSinavRaporNetOrtalamaGrafik(string tckimlikno, string oturum, string ID_SINAV, string ID_SUBE, string ID_KADEME3, string ID_SINIF)
        {
            InitializeComponent();
            this.TCKIMLIKNO = tckimlikno;
            this.OTURUM = oturum;
            this.ID_SINAV = Convert.ToInt32(ID_SINAV);
            this.ID_SUBE = Convert.ToInt32(ID_SUBE);
            this.ID_KADEME3 = Convert.ToInt32(ID_KADEME3);
            this.ID_SINIF = Convert.ToInt32(ID_SINIF);
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
                    b.ParametreEkle("@ISLEM", 5); // Rapor

                    ds = b.SorguGetir("sp_DenemeSinavRaporlari");

                    dt = ds.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Series s = new Series(dt.Rows[i][1].ToString(), ViewType.Bar);
                        var POINT = new SeriesPoint(dt.Rows[i][1].ToString(), new double[] { Convert.ToDouble(dt.Rows[i][2].ToString().Replace(".", ",")) });
                        //POINT.Color = Color.FromArgb(255, 0, 255, 128);
                        s.Points.Add(POINT);
                        s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                        GRAFIK.Series.Add(s);
                        //(s.View as SideBySideBarSeriesView).BarWidth = 600 / dt.Rows.Count;
                        //(s.View as SideBySideBarSeriesView).BarDistanceFixed = 600 / dt.Rows.Count;
                    }

                    if (this.ID_SUBE == 0)
                    {
                        GRAFIK.Titles[0].Text = "KAMPÜSLERİN " + GRAFIK.Titles[0].Text;
                    }
                    else
                    {
                        GRAFIK.Titles[0].Text = "SINIFLARIN " + GRAFIK.Titles[0].Text;
                    }

                    XYDiagram xyd = GRAFIK.Diagram as XYDiagram;
                    xyd.AxisX.Label.Angle = -45;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
