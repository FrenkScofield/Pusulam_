using System.Data;
using System.Linq;

namespace PusulamRapor.UyariSistemi
{
    public partial class UyariSistemiKullanici : DevExpress.XtraReports.UI.XtraReport
    {
        public UyariSistemiKullanici(string TCKIMLIKNO, string OTURUM, string ID_UYARI)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 7);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_MENU", 1351);

                b.ParametreEkle("@ID_UYARI", ID_UYARI);


                DataSet ds = b.SorguGetir("sp_UyariSistemleri");

                this.DataSource = ds.Tables[0];

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    item["SUBELIST"] += item["SUBELIST"].ToString().Length == 20 ? "..." : "";
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FillReportDataFields.FillPanel(Detail, ds.Tables[0]);

                    lblUyariAd.Text = ds.Tables[1].Rows[0]["AD"].ToString();
                }



            }
        }

    }
}
