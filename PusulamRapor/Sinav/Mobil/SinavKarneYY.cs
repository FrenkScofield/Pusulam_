using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Mobil
{
    public partial class SinavKarneYY : DevExpress.XtraReports.UI.XtraReport
    {
        public SinavKarneYY(DataTable dt)
        {
            InitializeComponent();

            this.DataSource = dt;

            lblSinavPuan.DataBindings.Add("Text", DataSource, "YYPUAN");

            if (dt.Rows.Count > 0)
            {
                FillReportDataFields.Fill(Detail, dt);
            }

        }

    }
}