using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav
{
    public partial class DersUniteListe : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_DERS { get; set; }
        public int ID_KADEME3 { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 80;
        float boy = 25;
        public DersUniteListe(string tc, string oturum, string idKademe3, string idDers)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_DERS = Convert.ToInt32(idDers);
            ID_KADEME3 = Convert.ToInt32(idKademe3);
        }

        private void DersUniteListe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_DERS", ID_DERS);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ISLEM", 11); // Rapor
                b.ParametreEkle("@ID_MENU", 19);

                ds = b.SorguGetir("sp_DersUnite");
            }


            Baslik();
            Icerik();
            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[Alt Konu Kodu]");
            this.DataSource = dt;
            FillReportDataFields.FillPanel(Detail, dt);

        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;

            float sayfaEn = 1149f - 40f;
            en = sayfaEn / 10;

            lbl = PublicMetods.lblEkle(ds.Tables[0].Rows[0]["Sınıf Düzeyi"]+ " " + ds.Tables[0].Rows[0]["Ders Adı"] +" Konuları" , LX, LY, en * 10, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            ReportHeader.Controls.Add(lbl);


            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "ID_DERS")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, (dc.ToString().Contains("Alt Konu Adı") ? en * 3 : en), boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    PageHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;

                }
            }
        }
        private void Icerik()
        {
            XRPanel pnl = new XRPanel();

            pnl.SizeF = new SizeF(LX, boy);
            pnl.Tag = "1";
            Detail.Controls.Add(pnl);
            LX = 0;
            LY = 0;
            boy = 50f;



            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "ID_DERS")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, (dc.ToString().Contains("Alt Konu Adı") ? en * 3 : en), boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                    pnl.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }

        }
    }
}