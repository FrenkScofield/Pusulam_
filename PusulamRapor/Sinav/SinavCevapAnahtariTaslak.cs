using System;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class SinavCevapAnahtariTaslak : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SINAVDERSLIST { get; set; }

        DataSet ds = new DataSet();
        public SinavCevapAnahtariTaslak(string tckimlikno, string oturum, string idSinav,string idSinavDersList)
        {
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SINAVDERSLIST = idSinavDersList;
            InitializeComponent();
        }

        private void SinavCevapAnahtariTaslak_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ISLEM", 39);
                b.ParametreEkle("@ID_MENU", 17); // değişecek

                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SINAVDERSLIST", ID_SINAVDERSLIST);

                ds = b.SorguGetir("sp_Sinav");

                this.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count > 0)
                {                    
                    FillReportDataFields.Fill(Detail, ds.Tables[0]);
                }
            }
        }
    }
}
