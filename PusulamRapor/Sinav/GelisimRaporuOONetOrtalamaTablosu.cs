using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOONetOrtalamaTablosu : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt;
        public GelisimRaporuOONetOrtalamaTablosu(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
            GroupField grpField = new GroupField("STKISAAD");
            GroupHeader1.GroupFields.Add(grpField);

            this.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                FillReportDataFields.Fill(GroupHeader1, dt);
                FillReportDataFields.Fill(Detail, dt);
            }
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            DataTable dtx = dt.Select("STKISAAD='" + GetCurrentColumnValue("STKISAAD") + "'").CopyToDataTable();
            double ortalama = 0;
            double genel = 0;
            double sube = 0;
            double sinif = 0;
            foreach (DataRow item in dtx.Rows)
            {
                ortalama += Convert.ToDouble(item["ORTALAMA"]);
                genel += Convert.ToDouble(item["GENEL"]);
                sube += Convert.ToDouble(item["SUBE"]);
                sinif += Convert.ToDouble(item["SINIF"]);
            }

            GrafikYaz(dtx);

            xrLabel_TOPLAM_Ortalama.Text = ortalama.ToString("#.##");
            xrLabel_TOPLAM_Genel.Text = genel.ToString("#.##");
            xrLabel_TOPLAM_Sube.Text = sube.ToString("#.##");
            xrLabel_TOPLAM_Sinif.Text = sinif.ToString("#.##");
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel_STKISAAD.Text = GetCurrentColumnValue("STKISAAD").ToString();
        }

        public void GrafikYaz(DataTable d)
        {
            XRChart chart = new XRChart();
            chart.SizeF = new SizeF(1692, 300);
            chart.CanGrow = true;

            chart.Series.Clear();

            

            Series srsORTALAMA = new Series("", ViewType.Bar);
            srsORTALAMA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsORTALAMA.Points.Clear();
            
            Series srsGENEL = new Series("", ViewType.Bar);
            srsGENEL.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsGENEL.Points.Clear();

            Series srsSUBE = new Series("", ViewType.Bar);
            srsSUBE.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsSUBE.Points.Clear();

            Series srsSINIF = new Series("", ViewType.Bar);
            srsSINIF.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsSINIF.Points.Clear();

            srsORTALAMA.View.Color = Color.FromArgb(1, 174, 240);
            srsGENEL.View.Color = Color.Red;
            srsSUBE.View.Color = Color.FromArgb(0, 192, 0);
            srsSINIF.View.Color = Color.Purple;

            srsORTALAMA.LegendText = "ÖĞRENCİ NET ORTALAMASI";
            srsSINIF.LegendText = "SINIF NET ORTALAMASI";
            srsSUBE.LegendText = "KAMPÜS NET ORTALAMASI";
            srsGENEL.LegendText = "GENEL NET ORTALAMASI";

            foreach (DataRow item in d.Rows)
            {
                srsORTALAMA.Points.Add(new SeriesPoint(item["DERSAD"], Convert.ToDouble(item["ORTALAMA"])));
                srsGENEL.Points.Add(new SeriesPoint(item["DERSAD"], Convert.ToDouble(item["GENEL"])));
                srsSUBE.Points.Add(new SeriesPoint(item["DERSAD"], Convert.ToDouble(item["SUBE"])));
                srsSINIF.Points.Add(new SeriesPoint(item["DERSAD"], Convert.ToDouble(item["SINIF"])));
            }

            chart.Series.Add(srsORTALAMA);
            chart.Series.Add(srsSINIF);
            chart.Series.Add(srsSUBE);
            chart.Series.Add(srsGENEL);

            Font font16b = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);

            foreach (var axis in ((XYDiagram)chart.Diagram).GetAllAxesX())
                axis.Label.Font = font16b;

            //chart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            //chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            chart.Legend.Font = new System.Drawing.Font(new FontFamily("Tahoma"), 10);

            xrPanel_Chart.Controls.Clear();
            xrPanel_Chart.Controls.Add(chart);
        }
    }
}
