using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Hedef
{
    public partial class HedefBelirlemeRaporDetayli : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public bool HEDEF { get; set; }

        public HedefBelirlemeRaporDetayli(string tc, string oturum, string idSubeList, string idSinifList)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            ID_SUBEs = idSubeList;
            ID_SINIFs = idSinifList;

            InitializeComponent();
        }

        private void HedefBelirlemeRaporDetayli_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                b.ParametreEkle("@HEDEF", HEDEF);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1168);

                DataSet ds = b.SorguGetir("sp_HedefBelirlemeRapor");

                this.DataSource = ds.Tables[0];
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }

    }
}
