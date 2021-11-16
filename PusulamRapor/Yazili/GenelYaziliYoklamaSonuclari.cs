using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class GenelYaziliYoklamaSonuclari : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINAV { get; set; }
        public string SINIFLAR { get; set; }
        public string SUBELER { get; set; }
        public GenelYaziliYoklamaSonuclari(string _TCKIMLIKNO, string _OTURUM, string _ID_SINAV, string _SINIFLAR, string _SUBELER)
        {
            TCKIMLIKNO = _TCKIMLIKNO;
            OTURUM = _OTURUM;
            ID_SINAV = _ID_SINAV;
            SINIFLAR = _SINIFLAR;
            SUBELER = _SUBELER;
            InitializeComponent();
        }

        float LY = 0;
        float LX = 0;
        FontFamily ff = new FontFamily("Tahoma");
        float lblEn = 200;
        float lblBoy = 40;
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtOgrenci = new DataTable();
        private void GenelYaziliYoklamaSonuclari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@SINIFLAR", SINIFLAR);
                b.ParametreEkle("@SUBELER", SUBELER);
                b.ParametreEkle("@ISLEM", 7); //2 eski
                b.ParametreEkle("@ID_MENU", 1044);

                ds = b.SorguGetir("sp_YaziliYoklamaSonuclari");

                if (ds.Tables[1].Rows.Count > 0)
                {

                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                    dtOgrenci = dt1.AsDataView().ToTable(true, "ID_OGRENCI");

                    Baslik();
                    Icerik();

                }
            }
        }
        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;

            ReportHeader.Controls.Add(lblEkle("SORULARIN KONUSU", LX, LY, lblEn * 5, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow row in dt2.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(row["KONUKODU"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            ReportHeader.Controls.Add(lblEkle("", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            LY += lblBoy;
            LX = 0;
            //////

            ReportHeader.Controls.Add(lblEkle("SORULARIN PUAN DEĞERİ", LX, LY, lblEn * 5, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow row in dt2.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(row["PUANDEGERI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            ReportHeader.Controls.Add(lblEkle("", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            LY += lblBoy;
            LX = 0;
            ////////


            ReportHeader.Controls.Add(lblEkle("SIRA", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("OGR.NO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("KAMPÜS", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SINIF", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("ADSOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow row in dt2.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(row["SORUNO"].ToString() + ". SORU", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            ReportHeader.Controls.Add(lblEkle("TOPLAM PUAN", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("DERECE", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
        }

        public string PuanDurum(int puan)
        {
            if (puan > 84 && puan < 101)
            {
                return "Pekiyi";
            }
            else if (puan > 69 && puan < 85)
            {
                return "İyi";
            }
            else if (puan > 59 && puan < 70)
            {
                return "Orta";
            }
            else if (puan > 49 && puan < 60)
            {
                return "Geçer";
            }
            else if (puan < 50)
            {
                return "Geçmez";
            }

            return "-";
        }

        int sira = 1;
        public void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;


            foreach (DataRow dr in dtOgrenci.Rows)
            {
                DataTable dt = dt1.Select("ID_OGRENCI=" + dr["ID_OGRENCI"].ToString(), "SORUNO").CopyToDataTable();

                Detail.Controls.Add(lblEkle(sira.ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dt.Rows[0]["OGRNO"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dt.Rows[0]["SUBE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dt.Rows[0]["SINIF"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dt.Rows[0]["ADSOYAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                int toplamPuan = 0;
                foreach (DataRow row in dt.Rows)
                {
                    int puan = Convert.ToInt32(row["PUAN"].ToString());
                    Detail.Controls.Add(lblEkle(puan.ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    toplamPuan += puan;
                }
                Detail.Controls.Add(lblEkle(toplamPuan.ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(PuanDurum(toplamPuan).ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                LY += lblBoy;
                LX = 0;
                sira++;
            }
        }

        public XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }

    }
}
