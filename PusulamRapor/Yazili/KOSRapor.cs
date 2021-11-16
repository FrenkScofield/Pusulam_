using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class KOSRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string SUBELER { get; set; }
        public string SINIFLAR { get; set; }
        public string TC_OGRENCI { get; set; }
        public int ID_YAZILI { get; set; }
        public int ID_MENU { get; set; }

        DataSet ds;
        DataTable dtTEK = new DataTable();
        DataTable dtCOK = new DataTable();

        Font fontrow1 = new System.Drawing.Font(new FontFamily("Tahoma"), 6, FontStyle.Regular);
        Font fontrow2 = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);

        public KOSRapor(string TCKIMLIKNO, string OTURUM, string SUBELER, string SINIFLAR, string TC_OGRENCI, string ID_YAZILI, string ID_MENU)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.SUBELER = SUBELER;
            this.SINIFLAR = SINIFLAR;
            this.TC_OGRENCI = TC_OGRENCI;
            this.ID_YAZILI = Convert.ToInt32(ID_YAZILI);
            this.ID_MENU = Convert.ToInt32(ID_MENU);
            InitializeComponent();
        }

        private void KOSRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAVRAPOR", ID_YAZILI);
                b.ParametreEkle("@SUBELER", SUBELER);
                b.ParametreEkle("@SINIFLAR", SINIFLAR);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_MENU", ID_MENU);
                b.ParametreEkle("@ISLEM", 8);
                ds = b.SorguGetir("sp_YaziliYoklamaSonuclari");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtTEK = ds.Tables[0];
                    dtCOK = ds.Tables[1];

                    GroupHeader1.GroupFields.Add(new GroupField("TCKIMLIKNO"));
                    this.DataSource = dtTEK;
                    FillReportDataFields.Fill(Detail, dtTEK);
                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string TC = GetCurrentColumnValue("TCKIMLIKNO").ToString();
            pnl_soru.Controls.Clear();
            if (dtCOK.Select("TCKIMLIKNO='" + TC + "'").Length > 0)
            {
                DataTable DTSORU = dtCOK.Select("TCKIMLIKNO='" + TC + "'").CopyToDataTable();
                float X = 8;
                float Y = 5;
                foreach (DataRow SORU in DTSORU.Rows)
                {
                    X = 8;
                    XRLabel KAZANIM = PublicMetods.lblEkle(SORU["KAZANIM"].ToString(), X, Y, 400F, 26, Color.Transparent, Color.Black, Color.Transparent, fontrow1);
                    KAZANIM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                    switch (SORU["DURUM"].ToString())
                    {
                        case "4":
                            X += KAZANIM.WidthF + 24;
                            break;
                        case "3":
                            X += KAZANIM.WidthF + 79;
                            break;
                        case "2":
                            X += KAZANIM.WidthF + 135;
                            break;
                        case "1":
                            X += KAZANIM.WidthF + 191;
                            break;
                    }
                    XRLabel DURUM = PublicMetods.lblEkle("\u221A", X, Y, 26, 26, Color.Transparent, Color.Green, Color.Transparent, fontrow2);
                    DURUM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    Y += 30.5f;

                    pnl_soru.Controls.Add(KAZANIM);
                    pnl_soru.Controls.Add(DURUM);
                }
            }
        }
    }
}
