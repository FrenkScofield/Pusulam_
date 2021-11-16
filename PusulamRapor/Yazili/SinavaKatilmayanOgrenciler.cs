using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class SinavaKatilmayanOgrenciler : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SINIFLAR { get; set; }
        public string ID_SUBELER { get; set; }
        DataSet ds;
        public SinavaKatilmayanOgrenciler(string tckimlikno, string oturum, string idSinav, string idSiniflar, string idSubeler)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SINIFLAR = idSiniflar;
            ID_SUBELER = idSubeler;
        }

        private void SinavaKatilmayanOgrenciler_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@ID_MENU", 1098);
                b.ParametreEkle("@ISLEM", 4); // 2ESKİ
                ds = b.SorguGetir("sp_SinavaKatilmayanOgrenciler");

                this.DataSource = ds.Tables[0];
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }

        int i = 0;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Color backcolorBody;
            if (i % 2 == 0)
            {
                backcolorBody = Color.White;
            }
            else
            {
                backcolorBody = Color.FromArgb(255, 212, 216, 249);
            }

            S_NO.BackColor = backcolorBody;
            SINIF.BackColor = backcolorBody;
            SUBE.BackColor = backcolorBody;
            AD.BackColor = backcolorBody;

            i++;
        }
    }
}
