using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class skaSinav : DevExpress.XtraReports.UI.XtraReport
    {
        public skaSinav(DataTable dt)
        {
            InitializeComponent();
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
