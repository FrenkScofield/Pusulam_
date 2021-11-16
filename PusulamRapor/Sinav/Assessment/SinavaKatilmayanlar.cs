using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav.Assessment
{
    public partial class SinavaKatilmayanlar : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_ASSESSMENT { get; set; }
        public string ID_SUBEs { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 130;
        float boy = 25;
        public SinavaKatilmayanlar(string tc, string oturum, string idAssessment, string idSubes)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_ASSESSMENT = Convert.ToInt32(idAssessment);
            ID_SUBEs = idSubes;
        }

        private void SinavaKatilmayanlar_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ASSESSMENT", ID_ASSESSMENT);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1217);

                ds = b.SorguGetir("sp_AssessmentSinavaKatilmayanlar");
            }

            Baslik();
            Icerik();
            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[Kampüs],[Grup],[Sınıf],[Ad Soyad]");
            this.DataSource = dt;
            FillReportDataFields.FillPanel(Detail, dt);
        }
        private void Baslik()
        {

            lbl = PublicMetods.lblEkle("SINAVA KATILMAYAN ÖĞRENCİ LİSTESİ", LX, LY, en*6, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            ReportHeader.Controls.Add(lbl);

            LX = 0;
            LY = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "TCKIMLIKNO")
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

            XRPanel pnl = new XRPanel();

            pnl.SizeF = new SizeF(this.PageSize.Width - 40f, 25f);
            pnl.Tag = "1";
            Detail.Controls.Add(pnl);


            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "TCKIMLIKNO")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                    pnl.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }

        }
    }
}
