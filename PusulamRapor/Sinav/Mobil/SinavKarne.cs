using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav.Mobil
{
    public partial class SinavKarne : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;

        public SinavKarne(string TCKIMLIKNO, string OTURUM, string TCKIMLIKNO_OGR, string ID_SINAVTUR, string ID_SINAV)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TCKIMLIKNO_OGR", TCKIMLIKNO_OGR);
                b.ParametreEkle("@ID_SINAVTUR", ID_SINAVTUR);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                
                ds = b.SorguGetir("sp_MobilSinavKarne");

                this.DataSource = ds.Tables[0];

                FillReportDataFields.Fill(PageHeader, ds.Tables[0]);

                // YAZILI
                try
                {
                    SinavKarneYY rapor = new SinavKarneYY(ds.Tables[1]);
                    SubReportYY.ReportSource = rapor;

                    if (ds.Tables[1].Rows.Count == 0)
                        SubReportYY.Visible = false;
                }
                catch (Exception ex)
                {
                    SubReportYY.ReportSource = null;
                    SubReportYY.Visible = false;
                }

                // SINAV DERSLER
                try
                {
                    SinavKarneDers rapor = new SinavKarneDers(ds.Tables[2]);
                    SubReportSinavDers.ReportSource = rapor;

                    if (ds.Tables[2].Rows.Count == 0)
                        SubReportSinavDers.Visible = false;
                }
                catch (Exception ex)
                {
                    SubReportSinavDers.ReportSource = null;
                    SubReportSinavDers.Visible = false;
                }

                // SINAV PUANLAR
                try
                {
                    SinavKarnePuan rapor = new SinavKarnePuan(ds.Tables[3]);
                    SubReportSinavPuan.ReportSource = rapor;

                    if (ds.Tables[3].Rows.Count == 0)
                        SubReportSinavPuan.Visible = false;
                }
                catch (Exception ex)
                {
                    SubReportSinavPuan.ReportSource = null;
                    SubReportSinavPuan.Visible = false;
                }

            }

        }

    }
}
