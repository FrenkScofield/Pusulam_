using System;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav {
    public partial class SinavOzellikleri: DevExpress.XtraReports.UI.XtraReport {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }

        DataSet ds = new DataSet();

        public SinavOzellikleri(string tckimlikno, string oturum, string idSinav) {
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            InitializeComponent();
        }

        private void SinavOzellikleri_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
            //GroupField grpOturum = new GroupField("OTURUM");

            using (Baglanti b = new Baglanti()) {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ISLEM", 25);

                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_MENU", 17);

                ds = b.SorguGetir("sp_Sinav");

                this.DataSource = ds.Tables[0];

                GroupField grpSinavDers = new GroupField("ID_SINAVDERS");
                GroupHeader1.GroupFields.Add(grpSinavDers);
                GroupField grpOturum = new GroupField("OTURUM");
                GroupHeader2.GroupFields.Add(grpOturum);

                if (ds.Tables[0].Rows.Count > 0) {
                    FillReportDataFields.Fill(GroupHeader1, ds.Tables[0]);
                    FillReportDataFields.Fill(Detail, ds.Tables[0]);
                }

            }

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {
           
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {            

        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) {

            DataRow dr = ds.Tables[0].Rows[0];
            int oturum = Convert.ToInt32(GetCurrentColumnValue("OTURUM"));
            lblBaslik.Text = dr["DONEM"] + " " + dr["GRUP"] + " " + dr["SINAVAD"] +"("+oturum+" OTURUM)"+ " (Uygulama Tarihi : " + dr["SINAVTARIH"] + " )";
        }
    }
}
