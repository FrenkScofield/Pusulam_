using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace PusulamRapor.Viu
{
    public partial class Arama : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public Arama(string TCKIMLIKNO, string OTURUM, string ID_SUBELIST, string TCOGRETMENLIST, string ID_ARAMASEBEPLIST, string ID_ARAMADURUMLIST, string BASTARIH, string BITTARIH)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 5);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@BASTARIH", BASTARIH);
                b.ParametreEkle("@BITTARIH", BITTARIH);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);
                b.ParametreEkle("@TCOGRETMENLIST", TCOGRETMENLIST);
                b.ParametreEkle("@ID_ARAMASEBEPLIST", ID_ARAMASEBEPLIST);
                b.ParametreEkle("@ID_ARAMADURUMLIST", ID_ARAMADURUMLIST);
                b.ParametreEkle("@JSON", 0);
                b.ParametreEkle("@ID_MENU", 1236);
                ds = b.SorguGetir("sp_VIU");
                this.DataSource = ds.Tables[0];
            }
        }

        private void Arama_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }
    }
}
