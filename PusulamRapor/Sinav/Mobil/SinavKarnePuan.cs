using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Mobil
{
    public partial class SinavKarnePuan : DevExpress.XtraReports.UI.XtraReport
    {
        public SinavKarnePuan(DataTable dt)
        {
            InitializeComponent();

            this.DataSource = dt;

            if (dt.Rows.Count > 0)
            {
                FillReportDataFields.Fill(Detail, dt);
            }

        }

    }
}
