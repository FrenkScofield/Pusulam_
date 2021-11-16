using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_Katilmayanlar : DevExpress.XtraReports.UI.XtraReport
    {

        public OR_Katilmayanlar(DataTable dt)
        {
            InitializeComponent();
            TITLE.Text = dt.Rows[0]["BASLIK"].ToString();
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
