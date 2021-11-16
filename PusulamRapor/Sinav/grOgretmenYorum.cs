using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;


namespace PusulamRapor.Sinav
{
    public partial class grOgretmenYorum : DevExpress.XtraReports.UI.XtraReport
    {
        public grOgretmenYorum(DataTable dt)
        {
            InitializeComponent();

            Font font9b = new System.Drawing.Font(new FontFamily("Tahoma"), 9, FontStyle.Bold);
            Font font16b = new System.Drawing.Font(new FontFamily("Tahoma"), 16, FontStyle.Bold);
            Font font9r = new System.Drawing.Font(new FontFamily("Tahoma"), 9, FontStyle.Regular);
            //Color titleblue = Color.FromArgb(68, 114, 196);
            //XRLabel xrBaslik = new XRLabel()
            //{
            //    WidthF = PageWidth,
            //    HeightF = 40,
            //    Text = "ÖĞRETMEN ÖĞRENCİ YORUM",
            //    Font = font16b,
            //    ForeColor = titleblue,
            //    LocationF = new PointF(0, 0),
            //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            //    Multiline = true
            //};
            //ReportHeader.Controls.Add(xrBaslik);

            this.DataSource = dt;

            GroupField PERIYOT = new GroupField("PERIYOT");
            GroupHeader1.GroupFields.Add(PERIYOT);

            FillReportDataFields.Fill(GroupHeader1, dt);
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
