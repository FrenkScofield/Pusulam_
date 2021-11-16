using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav.Assessment
{
    public partial class SinavOgrenciRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_ASSESSMENT { get; set; }
        public string ID_SUBEs { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public SinavOgrenciRaporu(string tc, string oturum, string idAssessment, string idSubes)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_ASSESSMENT = Convert.ToInt32(idAssessment);
            ID_SUBEs = idSubes;
        }

        private void SinavOgrenciRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ASSESSMENT", ID_ASSESSMENT);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1206);

                ds = b.SorguGetir("sp_AssessmentSinavOgrenciRaporu");
            }

            Baslik();
            Icerik();
            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[KAMPÜS],[GRUP],[SINIF],[AD SOYAD]");
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "ID_ASSESSMENTOGRENCI")
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
                if (dc.ToString() != "ID_ASSESSMENTOGRENCI")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }

        }
    }
}
