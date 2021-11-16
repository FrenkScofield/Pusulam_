
using System.Data;

namespace PusulamRapor.DersCalismaProgrami
{
    public partial class OgretmenRapor : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public OgretmenRapor(string TCKIMLIKNO, string OTURUM, string idsiniflist, string idsubelist)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 7);
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

        private void OgretmenRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }
    }
}
