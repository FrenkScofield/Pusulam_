using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Viu
{
    public partial class Mesaj : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public Mesaj(string TCKIMLIKNO, string OTURUM, string ID_SUBELIST, string TCOGRETMENLIST, string BASTARIH, string BITTARIH)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@BASTARIH", BASTARIH);
                b.ParametreEkle("@BITTARIH", BITTARIH);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);
                b.ParametreEkle("@TCOGRETMENLIST", TCOGRETMENLIST);
                b.ParametreEkle("@ID_MENU", 1236);
                ds = b.SorguGetir("sp_VIU");
                this.DataSource = ds.Tables[0];
            }
        }

        private void Mesaj_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
        }

        private void ACIKLAMA_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ACIKLAMA.NavigateUrl = (ACIKLAMA.Text.Contains("https://okyanusdata.s3-eu-west-1.amazonaws.com/viu/")) ? ACIKLAMA.Text : "";
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Color clr = Color.FromName(GetCurrentColumnValue("RENK").ToString());
            xrTarih.ForeColor = clr;
            foreach (XRControl x in Detail.Controls)
                if (x.GetType().ToString() == "DevExpress.XtraReports.UI.XRPanel" && x.Tag.ToString() == "1")
                    foreach (XRControl y in x.Controls)
                        if (y.GetType().ToString() == "DevExpress.XtraReports.UI.XRLabel" && y.Tag.ToString() == "1")
                            y.ForeColor = clr;

        }
    }
}
