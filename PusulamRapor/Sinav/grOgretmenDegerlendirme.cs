using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class grOgretmenDegerlendirme : DevExpress.XtraReports.UI.XtraReport
    {
        public grOgretmenDegerlendirme(DataTable dt)
        {
            InitializeComponent();

            Font font9b = new System.Drawing.Font(new FontFamily("Tahoma"), 9, FontStyle.Bold);
            Font font16b = new System.Drawing.Font(new FontFamily("Tahoma"), 16, FontStyle.Bold);
            Font font9r = new System.Drawing.Font(new FontFamily("Tahoma"), 9, FontStyle.Regular);
            //Color titleblue = Color.FromArgb(68, 114, 196);
            //XRLabel xrBaslik = new XRLabel()
            //{
            //    WidthF = PageWidth,
            //    HeightF = 102,
            //    Text = "ÖĞRETMEN ÖĞRENCİ DEĞERLENDİRME",
            //    Font = font16b,
            //    ForeColor = titleblue,
            //    LocationF = new PointF(0, 0),
            //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            //    Multiline = true
            //};
            //ReportHeader.Controls.Add(xrBaslik);

            DataTable distinctValues = dt.DefaultView.ToTable(true, "PERIYOT");
            float x = 138f;
            float width = (827F - 40 - x) / (float)distinctValues.Rows.Count;
            DataTable table1 = new DataTable();
            table1.Columns.Add("BOLUM", typeof(string));
            foreach (DataRow item in distinctValues.Rows)
            {
                XRLabel xrPeriyotBaslik = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 33,
                    Text = item["PERIYOT"].ToString(),
                    Font = font9b,
                    ForeColor = Color.Black,
                    LocationF = new PointF(x, 0),
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.SkyBlue,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Multiline = true
                };
                PageHeader.Controls.Add(xrPeriyotBaslik);

                XRLabel xrPeriyot = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 50,
                    Text = item["PERIYOT"].ToString(),
                    Font = font9r,
                    ForeColor = Color.Black,
                    LocationF = new PointF(x, 0),
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    Tag = 1,
                    BorderColor = Color.SkyBlue,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Multiline = true
                };
                Detail.Controls.Add(xrPeriyot);

                x += width;

                table1.Columns.Add(item["PERIYOT"].ToString());
            }

            string TEMPBOLUM = "";
            DataRow newdr = table1.NewRow();
            foreach (DataRow item in dt.Rows)
            {
                if (TEMPBOLUM.Length == 0)
                {
                    TEMPBOLUM = item["BOLUM"].ToString();
                    newdr["BOLUM"] = TEMPBOLUM;
                }
                if (!item["BOLUM"].Equals(TEMPBOLUM) && !TEMPBOLUM.Equals(""))
                {
                    table1.Rows.Add(newdr);
                    newdr = table1.NewRow();
                    TEMPBOLUM = item["BOLUM"].ToString();
                    newdr["BOLUM"] = TEMPBOLUM;
                }
                newdr[item["PERIYOT"].ToString()] = item["KENDI"];
            }

            table1.Rows.Add(newdr);

            this.DataSource = table1;
            FillReportDataFields.Fill(Detail, table1);
        }
    }
}
