using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Upgrade
{
    public partial class TopluSonuc : DevExpress.XtraReports.UI.XtraReport
    {
        private string TCKIMLIKNO { get; set; }
        private string oturum { get; set; }
        private string ID_SUBE { get; set; }
        private string ID_SINAVGRUP { get; set; }
        private string ID_SINIF { get; set; }
        private string ID_TKTSINAV { get; set; }
        private string DONEM { get; set; }
        DataSet ds;

        public TopluSonuc(string TCKIMLIKNO, string oturum, string ID_SUBE, string ID_SINAVGRUP, string ID_SINIF, string DONEM, string ID_TKTSINAV)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.oturum = oturum;
            this.ID_SUBE = ID_SUBE;
            this.ID_SINAVGRUP = ID_SINAVGRUP;
            this.ID_SINIF = ID_SINIF;
            this.ID_TKTSINAV = ID_TKTSINAV;
            this.DONEM = DONEM;
        }

        private void TopluSonuc_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_SUBES", ID_SUBE);
                b.ParametreEkle("@ID_SINAVGRUPS", ID_SINAVGRUP);
                b.ParametreEkle("@ID_SINIFS", ID_SINIF);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_TKTSINAV", ID_TKTSINAV);
                b.ParametreEkle("@ID_MENU", 1195);
                b.ParametreEkle("@ISLEM", 21);
                ds = b.SorguGetir("sp_UpgradeSoru");
            }

            this.DataSource = ds.Tables[0];
            FillReportDataFields.Fill(Detail, ds.Tables[0]);
        }
    }
}
