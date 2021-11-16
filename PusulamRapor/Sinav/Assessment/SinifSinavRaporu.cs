using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;


namespace PusulamRapor.Sinav.Assessment
{
    public partial class SinifSinavRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public SinifSinavRaporu(string tc, string oturum, string donem)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            DONEM = donem;
        }

        private void SinifSinavRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1208);

                ds = b.SorguGetir("sp_AssessmentSinifSinavRaporu");
            }

            Baslik();
            Icerik();

            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[KAMPÜS],[GRUP],[SINIF]");
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);

        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "ID_SINIF")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    PageHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 50f;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "ID_SINIF")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }

        }
    }
}
