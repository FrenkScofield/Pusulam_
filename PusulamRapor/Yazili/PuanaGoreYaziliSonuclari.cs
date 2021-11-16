using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class PuanaGoreYaziliSonuclari : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNOX { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINAVLAR { get; set; }
        public string ID_SINIFLAR { get; set; }
        public string ID_SUBELER { get; set; }
        public string ID_DERSLER { get; set; }
        public string YARIYIL { get; set; }
        public int ID_KADEME3 { get; set; }
        public string PUANARALIGI { get; set; }
        public bool TC { get; set; }
        public string DONEM { get; set; }
        DataSet ds;
        public PuanaGoreYaziliSonuclari(string tckimlikno, string oturum, string idSinavlar, string idSiniflar, string idSubeler, string idDersler, string DONEM, string yariyil, string idKademe3, string puanAraliği, string tc)
        {
            InitializeComponent();
            TCKIMLIKNOX = tckimlikno;
            OTURUM = oturum;
            ID_SINAVLAR = idSinavlar;
            ID_SINIFLAR = idSiniflar;
            ID_SUBELER = idSubeler;
            ID_DERSLER = idDersler;
            YARIYIL = yariyil;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            PUANARALIGI = puanAraliği;
            TC = Convert.ToBoolean(tc);
            this.DONEM = DONEM;
        }

        private void PuanaGoreYaziliSonuclari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNOX);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAVLAR", ID_SINAVLAR);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@YARIYIL", YARIYIL);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_DERSLER", ID_DERSLER);
                b.ParametreEkle("@PUANARALIGI", PUANARALIGI);
                b.ParametreEkle("@TC", TC);
                b.ParametreEkle("@ID_MENU", 1092);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 4); //2 ESKİ
                ds = b.SorguGetir("sp_PuanaGoreYaziliSonuclari");

                if (!TC)
                {
                    xrLabel4.Visible = false;
                    TCKIMLIKNO.Tag = 0;
                    TCKIMLIKNO.Visible = false;
                    SINAVADI.WidthF += TCKIMLIKNO.WidthF / 2.0f;
                    ADSOYAD.WidthF += TCKIMLIKNO.WidthF / 2.0f;
                    ADSOYAD.LocationF = new PointF((ADSOYAD.LocationF.X - (TCKIMLIKNO.WidthF / 2.0f)), ADSOYAD.LocationF.Y);
                    xrLabel3.WidthF += TCKIMLIKNO.WidthF / 2.0f;
                    xrLabel5.WidthF += TCKIMLIKNO.WidthF / 2.0f;
                    xrLabel5.LocationF = new PointF((xrLabel5.LocationF.X - (TCKIMLIKNO.WidthF / 2.0f)), xrLabel5.LocationF.Y);
                }

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

            KAMPUS.BackColor = backcolorBody;
            SINIF.BackColor = backcolorBody;
            SINAVADI.BackColor = backcolorBody;
            ADSOYAD.BackColor = backcolorBody;
            TCKIMLIKNO.BackColor = backcolorBody;
            PUAN.BackColor = backcolorBody;

            i++;
        }
    }
}
