using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace PusulamRapor.TKT
{
    public partial class KazanimAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        private string TCKIMLIKNO { get; set; }
        private string oturum { get; set; }
        private string ID_SUBE { get; set; }
        private string ID_SINAVGRUP { get; set; }
        private string ID_SINIF { get; set; }
        private string ID_TKTTEST { get; set; }
        private string DONEM { get; set; }
        DataSet ds;

        FontFamily ff = new FontFamily("Tahoma");

        public KazanimAnalizi(string TCKIMLIKNO, string oturum, string ID_SUBE, string ID_SINAVGRUP, string ID_SINIF, string ID_TKTTEST, string DONEM)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.oturum = oturum;
            this.ID_SUBE = ID_SUBE;
            this.ID_SINAVGRUP = ID_SINAVGRUP;
            this.ID_SINIF = ID_SINIF;
            this.ID_TKTTEST = ID_TKTTEST;
            this.DONEM = DONEM;
        }

        private void KazanimAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_SUBES", ID_SUBE);
                b.ParametreEkle("@ID_SINAVGRUP", ID_SINAVGRUP);
                b.ParametreEkle("@ID_SINIFS", ID_SINIF);
                b.ParametreEkle("@ID_TKTTEST", ID_TKTTEST);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_MENU", 1199);
                b.ParametreEkle("@ISLEM", 13);
                ds = b.SorguGetir("sp_TKTTest");
            }

            Font fontTitle = new Font(ff, 10, FontStyle.Bold);
            Font fontRow = new Font(ff, 10, FontStyle.Regular);

            float x = 250;
            float y = 0;
            int w = 130;
            int h = 24;
            for (int i = 50; i < 101; i++)
            {
                XRLabel LBLAY = PublicMetods.lblEkle(i + " Ay", x, y, w, h, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, fontTitle);
                XRLabel LBLAYDTITLE = PublicMetods.lblEkle("DOĞRU", x, y + h, w / 2, h * 2, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, fontTitle);
                XRLabel LBLAYYTITLE = PublicMetods.lblEkle("YANLIŞ", x + (w / 2), y + h, w / 2, h * 2, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, fontTitle);

                XRLabel LBLAYD = PublicMetods.lblEkle(i + "DS", x, 0, w / 2, h * 2, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, "1", fontRow);
                XRLabel LBLAYY = PublicMetods.lblEkle(i + "YS", x + (w / 2), 0, w / 2, h * 2, System.Drawing.Color.White, System.Drawing.Color.Black, System.Drawing.Color.Black, "1", fontRow);

                ReportHeader.Controls.Add(LBLAY);
                ReportHeader.Controls.Add(LBLAYDTITLE);
                ReportHeader.Controls.Add(LBLAYYTITLE);
                Detail.Controls.Add(LBLAYD);
                Detail.Controls.Add(LBLAYY);
                x += w;
            }

            this.DataSource = ds.Tables[0];
            FillReportDataFields.Fill(Detail, ds.Tables[0]);
        }
    }
}