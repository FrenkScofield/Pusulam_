using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class grEtutRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public grEtutRaporu(DataTable dt)
        {
            InitializeComponent();

            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
