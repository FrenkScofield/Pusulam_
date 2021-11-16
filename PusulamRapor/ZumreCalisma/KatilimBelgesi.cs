using System.Data;

namespace PusulamRapor.ZumreCalisma
{
    public partial class KatilimBelgesi : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public KatilimBelgesi(string TCKIMLIKNO, string OTURUM, string ID_ZUMRECALISMA, string ID_MENU, string ID_ZUMRECALISMAKATILIMCI)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 27);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_MENU", ID_MENU);

                b.ParametreEkle("@ID_ZUMRECALISMA", ID_ZUMRECALISMA);
                b.ParametreEkle("@ID_ZUMRECALISMAKATILIMCI", ID_ZUMRECALISMAKATILIMCI);
                ds = b.SorguGetir("sp_ZumreCalisma");
                this.DataSource = ds.Tables[0];
            }
        }

        private void KatilimBelgesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }
    }
}
