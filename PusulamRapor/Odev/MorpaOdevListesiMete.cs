using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class MorpaOdevListesiMete : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public MorpaOdevListesiMete(string TCKIMLIKNO, string OTURUM)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 20);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_MENU", 1237);

                ds = b.SorguGetir("sp_Odev");
                this.DataSource = ds.Tables[0];
            }
        }

        private void MorpaOdevListesiMete_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }
    }
}
