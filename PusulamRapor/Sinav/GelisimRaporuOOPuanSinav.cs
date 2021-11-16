using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOPuanSinav : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public GelisimRaporuOOPuanSinav(DataTable dtt)
        {
            InitializeComponent();
            dt = dtt;
        }

        private void GelisimRaporuOOPuanSinav_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            GroupField grpField = new GroupField("ID_SINAVPUANTURU");
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
