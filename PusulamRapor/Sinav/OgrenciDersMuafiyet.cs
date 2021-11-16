using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class OgrenciDersMuafiyet : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public OgrenciDersMuafiyet(string TCKIMLIKNO, string OTURUM)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 3);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_MENU", 1156);

                ds = b.SorguGetir("sp_OgrenciDersMuaf");
                this.DataSource = ds.Tables[0];
            }
        }

        private void OgrenciDersMuafiyet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }
    }
}
