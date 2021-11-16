using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.ZekaTest
{
    public partial class ZekaTestRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string TESTTARIHI1 { get; set; }
        public string TESTTARIHI2 { get; set; }
        public string SEARCH { get; set; }
        public string ORDERCOL { get; set; }
        public string ORDERDIR { get; set; }
        public string DURUM { get; set; }

        public ZekaTestRapor(string TCKIMLIKNO, string OTURUM, string TESTTARIHI1, string TESTTARIHI2, string SEARCH, string ORDERCOL, string ORDERDIR, string DURUM)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.TESTTARIHI1 = TESTTARIHI1;
            this.TESTTARIHI2 = TESTTARIHI2;
            this.SEARCH = SEARCH;
            this.ORDERCOL = ORDERCOL;
            this.ORDERDIR = ORDERDIR;
            this.DURUM = DURUM;
            InitializeComponent();
        }

        private void ZekaTestRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TESTTARIHI1", TESTTARIHI1);
                b.ParametreEkle("@TESTTARIHI2", TESTTARIHI2);
                b.ParametreEkle("@SEARCH", SEARCH);
                b.ParametreEkle("@ORDERCOLEXCEL", ORDERCOL);
                b.ParametreEkle("@ORDERDIREXCEL", ORDERDIR);
                b.ParametreEkle("@DURUM", DURUM);
                b.ParametreEkle("@ISLEM", 10);
                b.ParametreEkle("@ID_MENU", 1186);
                DataTable dt = b.SorguGetir("sp_ZekaTest").Tables[0];
                Font fontHeader = new Font(new FontFamily("Tahoma"), 10, FontStyle.Bold);
                Font fontRow = new Font(new FontFamily("Tahoma"), 8, FontStyle.Regular);

                if (dt.Rows.Count > 0)
                {
                    float lx = 0;
                    float ly = 0;
                    foreach (DataColumn col in dt.Columns)
                    {
                        XRLabel labelHeader = PublicMetods.lblEkle(col.ColumnName, lx, ly, 250, 50, Color.White, Color.Black, Color.Black, "0", fontHeader);
                        ReportHeader.Controls.Add(labelHeader);
                        XRLabel labelRow = PublicMetods.lblEkle(col.ColumnName, lx, ly, 250, 50, Color.White, Color.Black, Color.Black, "1", fontRow);
                        Detail.Controls.Add(labelRow);
                        lx += labelHeader.WidthF;
                    }
                    this.DataSource = dt;
                    FillReportDataFields.Fill(Detail, dt);
                }
            }
        }
    }
}
