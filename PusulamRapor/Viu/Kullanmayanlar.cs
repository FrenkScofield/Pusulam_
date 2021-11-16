using System.Data;

namespace PusulamRapor.Viu
{
    public partial class Kullanmayanlar : DevExpress.XtraReports.UI.XtraReport
    {
        public Kullanmayanlar(string TCKIMLIKNO, string OTURUM, string ID_SUBELIST, string BASTARIH, string BITTARIH)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 2);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@BASTARIH", BASTARIH);
                b.ParametreEkle("@BITTARIH", BITTARIH);
                b.ParametreEkle("@ID_MENU", 1236);

                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);

                DataSet ds = b.SorguGetir("sp_VIU");

                this.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
                    //xrSube.DataBindings.Add("Text", this.DataSource, "SUBE");
                    //xrTCKimlikNo.DataBindings.Add("Text", this.DataSource, "TCKIMLIKNO");
                    //xrAd.DataBindings.Add("Text", this.DataSource, "AD");
                    //xrSoyad.DataBindings.Add("Text", this.DataSource, "SOYAD");

                    //xrKullaniciAd.DataBindings.Add("Text", this.DataSource, "KULLANICIAD");
                    //xrSifre.DataBindings.Add("Text", this.DataSource, "SIFRE");
                    //xrSinif.DataBindings.Add("Text", this.DataSource, "SINIF");
                    //xrKademe.DataBindings.Add("Text", this.DataSource, "KADEME");
                }

            }
        }

    }
}
