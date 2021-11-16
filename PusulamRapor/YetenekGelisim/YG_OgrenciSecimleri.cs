using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_OgrenciSecimleri : DevExpress.XtraReports.UI.XtraReport
    {
        private string TCKIMLIKNO { get; set; }
        private string oturum { get; set; }
        private string ID_SUBEs { get; set; }
        private string DONEM { get; set; }
        DataSet ds;

        FontFamily ff = new FontFamily("Tahoma");

        public YG_OgrenciSecimleri(string TCKIMLIKNO, string oturum, string ID_SUBEs, string DONEM)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.oturum = oturum;
            this.ID_SUBEs = ID_SUBEs;
            this.DONEM = DONEM;
        }

        private void YG_OgrenciSecimleri_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_MENU", 1216);
                b.ParametreEkle("@ISLEM", 1);
                ds = b.SorguGetir("sp_YG_OgrenciSecimRaporu");
            }

            this.DataSource = ds.Tables[0];
            FillReportDataFields.Fill(Detail, ds.Tables[0]);
        }
    }
}
