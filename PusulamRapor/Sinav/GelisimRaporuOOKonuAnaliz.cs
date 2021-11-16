using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOKonuAnaliz : DevExpress.XtraReports.UI.XtraReport
    {
        public GelisimRaporuOOKonuAnaliz(DataTable dt)
        {
            InitializeComponent();

            DataView dv = dt.Select().CopyToDataTable().DefaultView;
            dv.Sort = "DERSAD, TIP, KOD";
            dt = dv.ToTable();

            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);

            Font font20b = new System.Drawing.Font(new FontFamily("Tahoma"), 20, FontStyle.Bold);
            Color titleblue = Color.FromArgb(68, 114, 196);
            XRLabel xrBaslik = new XRLabel()
            {
                WidthF = PageWidth,
                HeightF = 250,
                Text = "ULUSAL SINAVLARA HAZIRLIK DENEMELERİ" + Environment.NewLine + "SONUCUNDA ÖĞRENME ORANI %50’NİN" + Environment.NewLine + "ALTINDAKİ KAZANIMLAR TABLOSU",
                Font = font20b,
                ForeColor = titleblue,
                LocationF = new PointF(0, 0),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Multiline = true
            };
            ReportHeader.Controls.Add(xrBaslik);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string kod = GetCurrentColumnValue("KOD").ToString();
            if (kod.Equals(""))
            {
                KONUAD.BackColor = Color.Orange;
                SORU.BackColor = Color.Orange;
                DOGRU.BackColor = Color.Orange;
                YANLIS.BackColor = Color.Orange;
                BOS.BackColor = Color.Orange;
                YUZDE.BackColor = Color.Orange;
            }
            else
            {
                KONUAD.BackColor = Color.Transparent;
                SORU.BackColor = Color.Transparent;
                DOGRU.BackColor = Color.Transparent;
                YANLIS.BackColor = Color.Transparent;
                BOS.BackColor = Color.Transparent;
                YUZDE.BackColor = Color.Transparent;
            }
        }
    }
}
