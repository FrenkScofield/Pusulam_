using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavRaporKatilmayanlar : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_MENU { get; set; }

        public int ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINIF { get; set; }

        DataSet ds;

        public DenemeSinavRaporKatilmayanlar(string tckimlikno, string oturum, string ID_SINAV, string ID_SUBE, string ID_KADEME3, string ID_SINIF)
        {
            InitializeComponent();
            this.TCKIMLIKNO = tckimlikno;
            this.OTURUM = oturum;
            this.ID_SINAV = Convert.ToInt32(ID_SINAV);
            this.ID_SUBE = Convert.ToInt32(ID_SUBE);
            this.ID_KADEME3 = Convert.ToInt32(ID_KADEME3);
            this.ID_SINIF = Convert.ToInt32(ID_SINIF);
            this.ID_MENU = 1074;

            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", this.TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", this.OTURUM);
                    b.ParametreEkle("@ID_SINAV", this.ID_SINAV);
                    b.ParametreEkle("@ID_SUBE", this.ID_SUBE);
                    b.ParametreEkle("@ID_KADEME3", this.ID_KADEME3);
                    b.ParametreEkle("@ID_SINIF", this.ID_SINIF);
                    b.ParametreEkle("@ID_MENU", this.ID_MENU);
                    b.ParametreEkle("@ISLEM", 2); // Rapor

                    ds = b.SorguGetir("sp_DenemeSinavRaporlari");
                    DataTable dt = ds.Tables[0];
                    this.DataSource = dt;
                    TITLE.Text = ds.Tables[1].Rows[0][0].ToString();
                    FillReportDataFields.Fill(Detail, dt);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}
