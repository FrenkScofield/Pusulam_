using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Viu
{
    public partial class Bildirim : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string TC_KULLANICI { get; set; }
        public string BASTARIH { get; set; }
        public string BITTARIH { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public Bildirim(string tc, string oturum, string tcKullanici, string basTarih, string bitTarih)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            TC_KULLANICI = tcKullanici;
            BASTARIH = basTarih;
            BITTARIH = bitTarih;
        }

        private void Bildirim_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TC_KULLANICI", TC_KULLANICI);
                b.ParametreEkle("@BASTARIH", BASTARIH);
                b.ParametreEkle("@BITTARIH", BITTARIH);
                b.ParametreEkle("@DATATABLE", 1);
                b.ParametreEkle("@ISLEM", 1); // Rapor
                b.ParametreEkle("@ID_MENU", 1236);

                ds = b.SorguGetir("sp_ViuBildirimRapor");
            }

            Baslik();
            Icerik();
            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[KYE_TARIH]");
            this.DataSource = dt;
            FillReportDataFields.FillPanel(Detail, dt);
        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                en = (dc.ToString() == "MESAJ TAM")
                    ? 300
                    : 150;

                lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                PageHeader.Controls.Add(lbl);
                LX += lbl.WidthF;
            }
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 75f;

            XRPanel pnl = new XRPanel();
            pnl.WidthF = PageWidth - 40F;
            pnl.HeightF = boy;
            pnl.Tag = "1";
            Detail.Controls.Add(pnl);
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                en = (dc.ToString() == "MESAJ TAM")
                    ? 300
                    : 150;
                lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                pnl.Controls.Add(lbl);
                LX += lbl.WidthF;

            }

        }
    }
}
