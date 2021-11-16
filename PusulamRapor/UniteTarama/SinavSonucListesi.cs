using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.UniteTarama
{
    public partial class SinavSonucListesi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_UNITETARAMASINAV { get; set; }
        public string ID_SUBELIST { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public SinavSonucListesi(string tc, string oturum, string idSinav, string idSubeList)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_UNITETARAMASINAV = Convert.ToInt32(idSinav);
            ID_SUBELIST = idSubeList;
        }

        private void SinavSonucListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_UNITETARAMASINAV", ID_UNITETARAMASINAV);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);
                b.ParametreEkle("@ISLEM", 1); // Rapor
                b.ParametreEkle("@ID_MENU", 1307);

                ds = b.SorguGetir("sp_UniteTaramaSinavSonucListesi");
            }

            Baslik();
            Icerik();
            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "[KAMPÜS],[SINIF],[AD SOYAD]");
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                PageHeader.Controls.Add(lbl);
                LX += lbl.WidthF;
            }
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 50f;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;

            }

        }
    }
}
