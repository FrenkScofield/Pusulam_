using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOOgretmenDegerlendirme : DevExpress.XtraReports.UI.XtraReport
    {
        public GelisimRaporuOOOgretmenDegerlendirme(DataTable dt)
        {
            InitializeComponent();

            Font font12b = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
            Font fontBaslik = new System.Drawing.Font(new FontFamily("Tahoma"), 20, FontStyle.Bold);
            Font font12r = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
            Color titleblue = Color.FromArgb(68, 114, 196);
            XRLabel xrBaslik = new XRLabel()
            {
                WidthF = PageWidth,
                HeightF = 150,
                Text = "ÖĞRETMEN ÖĞRENCİ DEĞERLENDİRME",
                Font = fontBaslik,
                ForeColor = titleblue,
                LocationF = new PointF(0, 0),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Multiline = true
            };
            ReportHeader.Controls.Add(xrBaslik);

            DataTable distinctValues = dt.DefaultView.ToTable(true, "PERIYOT");
            float width = 1442F / (float)distinctValues.Rows.Count;
            float x = 250f;
            DataTable table1 = new DataTable();
            table1.Columns.Add("BOLUM", typeof(string));
            foreach (DataRow item in distinctValues.Rows)
            {
                XRLabel xrPeriyotBaslik = new XRLabel()
                {
                    WidthF = width,
                    HeightF = 33,
                    Text = item["PERIYOT"].ToString(),
                    Font = font12b,
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
                    HeightF = 33,
                    Text = item["PERIYOT"].ToString(),
                    Font = font12r,
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
                if (TEMPBOLUM.Length==0)
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
