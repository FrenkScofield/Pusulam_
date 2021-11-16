using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOLUSRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public GelisimRaporuOOLUSRaporu(DataTable dt)
        {
            InitializeComponent();

            if (dt.Rows.Count > 0)
            {

                this.DataSource = dt;
                FillReportDataFields.Fill(Detail, dt);
            }
        }

    }
}
