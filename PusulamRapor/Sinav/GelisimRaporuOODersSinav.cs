using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOODersSinav : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public bool puanMi { get; set; }
        public GelisimRaporuOODersSinav(DataTable dtt, bool puan)
        {
            dt = dtt;
            puanMi = puan;

            InitializeComponent();
        }

        private void GelisimRaporuOODersSinav_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (puanMi)
            {
                lblSS.Text = "SS";

                GroupField grpField = new GroupField("STKISAAD");
                GroupHeader1.GroupFields.Add(grpField);

                this.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    FillReportDataFields.Fill(GroupHeader1, dt);
                    FillReportDataFields.Fill(Detail, dt);
                }
            }
            else
            {
                lblSS.Text = "SORUSAYISI";
                lblDers.Text = "DERSAD";
                lblDers.Tag = "1";
                GroupField grpField = new GroupField("STKISAAD");
                GroupHeader1.GroupFields.Add(grpField);

                this.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                    FillReportDataFields.Fill(GroupHeader1, dt);
                    FillReportDataFields.Fill(Detail, dt);
                }

            }
        }
        
    }
}
