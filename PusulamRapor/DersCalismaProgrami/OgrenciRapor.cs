
using System.Data;

namespace PusulamRapor.DersCalismaProgrami
{
    public partial class OgrenciRapor : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public OgrenciRapor(string TCKIMLIKNO, string OTURUM, string idsiniflist, string idsubelist)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 6);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);     
                b.ParametreEkle("@ID_SUBELER", idsubelist);
                b.ParametreEkle("@ID_SINIFLAR", idsiniflist);               
                b.ParametreEkle("@JSON", 0);
                b.ParametreEkle("@ID_MENU", 1236);
                ds = b.SorguGetir("sp_DersCalismaProgrami");
                this.DataSource = ds.Tables[0];
            }
        }

        private void OgrenciRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }
    }
}
