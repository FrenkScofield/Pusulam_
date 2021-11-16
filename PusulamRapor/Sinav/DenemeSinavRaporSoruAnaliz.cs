using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavRaporSoruAnaliz : DevExpress.XtraReports.UI.XtraReport
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

        public DenemeSinavRaporSoruAnaliz(string tckimlikno, string oturum, string ID_SINAV, string ID_SUBE, string ID_KADEME3, string ID_SINIF)
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
                    b.ParametreEkle("@ISLEM", 4); // Rapor

                    ds = b.SorguGetir("sp_DenemeSinavRaporlari");

                    OKUL.Text = ds.Tables[1].Rows[0][0].ToString();
                    SINAV.Text = ds.Tables[1].Rows[0][1].ToString();

                    KATILIM_OKUL.Text = ds.Tables[2].Rows[0][0].ToString();
                    KATILIM_ILCE.Text = ds.Tables[2].Rows[0][1].ToString();
                    KATILIM_IL.Text = ds.Tables[2].Rows[0][2].ToString();
                    KATILIM_GENEL.Text = ds.Tables[2].Rows[0][3].ToString();

                    dt = ds.Tables[0];
                    this.DataSource = dt;
                    GroupField grpField = new GroupField("BOLUMNO");
                    GroupHeader1.GroupFields.Add(grpField);
                    FillReportDataFields.Fill(Detail, dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String DC = GetCurrentColumnValue("DC").ToString();
            Double DY = Convert.ToDouble(GetCurrentColumnValue(DC).ToString().Replace(".", ",").Substring(GetCurrentColumnValue(DC).ToString().IndexOf(" - %") + 4));
            Double BY = Convert.ToDouble(GetCurrentColumnValue("BOS").ToString().Replace(".", ",").Substring(GetCurrentColumnValue("BOS").ToString().IndexOf(" - %") + 4));
            Double YY = Math.Round((100.0 - (DY + BY)), 2);

            LEGEND_DOGRU.Text = "Doğru (" + DY + ")";
            LEGEND_YANLIS.Text = "Yanlış (" + YY + ")";
            LEGEND_BOS.Text = "Boş (" + BY + ")";
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            TAKMAAD.Text = GetCurrentColumnValue("TAKMAAD").ToString();
        }

        private void A_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String DC = GetCurrentColumnValue("DC").ToString();
            Boolean HATALI = Convert.ToBoolean(GetCurrentColumnValue("HATALI"));
            if (DC.Equals((sender as XRLabel).Name) && !HATALI)
            {
                (sender as XRLabel).BackColor = Color.FromArgb(0, 120, 0);
                (sender as XRLabel).ForeColor = Color.White;
            }
            else
            {
                (sender as XRLabel).BackColor = Color.White;
                (sender as XRLabel).ForeColor = Color.Black;
            }
            string VALUE = GetCurrentColumnValue((sender as XRLabel).Name).ToString().Replace(" - ", Environment.NewLine);
            (sender as XRLabel).Text = VALUE;
        }

        private void HATALI_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Boolean HATALIX = Convert.ToBoolean(GetCurrentColumnValue("HATALI"));
            if (HATALIX)
            {
                HATALI.BackColor = Color.FromArgb(0, 120, 0);
                HATALI.ForeColor = Color.White;
            }
            else
            {
                HATALI.BackColor = Color.White;
                HATALI.ForeColor = Color.Black;
            }
            HATALI.Text = "";
        }

        private void GRAFIK_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            String DC = GetCurrentColumnValue("DC").ToString();
            Double DY = Convert.ToDouble(GetCurrentColumnValue(DC).ToString().Replace(".", ",").Substring(GetCurrentColumnValue(DC).ToString().IndexOf(" - %") + 4));
            Double BY = Convert.ToDouble(GetCurrentColumnValue("BOS").ToString().Replace(".", ",").Substring(GetCurrentColumnValue("BOS").ToString().IndexOf(" - %") + 4));
            Double YY = Math.Round((100.0 - (DY + BY)), 2);
            GRAFIK.Series[0].Points.Clear();

            var PD = new DevExpress.XtraCharts.SeriesPoint("Doğru", new double[] { DY });
            PD.Color = Color.FromArgb(255, 0, 255, 128);
            var PY = new DevExpress.XtraCharts.SeriesPoint("Yanlış", new double[] { YY });
            PY.Color = Color.FromArgb(255, 255, 120, 120);
            var PB = new DevExpress.XtraCharts.SeriesPoint("Boş", new double[] { BY });
            PB.Color = Color.FromArgb(255, 255, 255, 255);

            GRAFIK.Series[0].Points.Add(PD);
            GRAFIK.Series[0].Points.Add(PY);
            GRAFIK.Series[0].Points.Add(PB);
        }

        private void GRAFIK_AfterPrint(object sender, EventArgs e)
        {

        }

        private void GRAFIK_CustomDrawSeriesPoint(object sender, DevExpress.XtraCharts.CustomDrawSeriesPointEventArgs e)
        {
            PieDrawOptions drawOptions = e.SeriesDrawOptions as PieDrawOptions;
            if (drawOptions == null)
                return;

            // Get the fill options for the series point.
            drawOptions.FillStyle.FillMode = FillMode.Solid;
            drawOptions.Border.Color = Color.FromArgb(255, 0, 0, 0);
        }
    }
}
