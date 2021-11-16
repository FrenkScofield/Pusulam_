using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class sdaSinav : DevExpress.XtraReports.UI.XtraReport
    {
        public sdaSinav(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
