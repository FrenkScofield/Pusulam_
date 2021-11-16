using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav.Bursluluk
{
    public partial class OgrenciListesi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_BURSLULUKDOSYA { get; set; }
        public DataSet ds { get; set; }

        public OgrenciListesi(string tc, string oturum, string idBurslulukDosya)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_BURSLULUKDOSYA = Convert.ToInt32(idBurslulukDosya);
        }

        public XRLabel lbl { get; set; }
        public float LX { get; set; }
        public float LY { get; set; }

        private void OgrenciListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_BURSLULUKDOSYA", ID_BURSLULUKDOSYA);
                b.ParametreEkle("@ISLEM", 5); // Rapor
                b.ParametreEkle("@ID_MENU", 1176);

                ds = b.SorguGetir("sp_BurslulukOgrenciIslemleri");
            }
            float uzunluk = 150;
            float boy = 40;
            List<string> baslikList = new List<string>() { "TC Kimlik No", "Öğrenci Adı Soyadı", "Okulu", "Sınıf Düzeyi", "Başvurduğu Kampüs", "Veli Adı Soyadı", "Mail", "Telefon (Ev)", "Telefon (Cep)", "Başvuru Tarihi", "Sınav Tarihi", "Seans" };
            List<string> icerikList = new List<string>() { "TCKIMLIKNO", "OGRENCI_ADSOYAD", "OKULADI", "SINIFDUZEYI", "SUBEAD", "VELI_ADSOYAD", "MAIL", "TELEFON_EV", "TELEFON_CEP", "BASVURUTARIH", "SINAVTARIH", "SEANS" };

            LY = 0;
            LX = 0;

            foreach (string item in baslikList)
            {
                lbl = PublicMetods.lblEkle(item, LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                ReportHeader.Controls.Add(lbl);
                LX += lbl.WidthF;
            }
            LX = 0;
            foreach (string item in icerikList)
            {
                lbl = PublicMetods.lblEkle(item, LX, LY, uzunluk, boy, Color.White, Color.Black, Color.MidnightBlue, "1");
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;
            }

            this.DataSource = ds.Tables[0];
            FillReportDataFields.Fill(Detail, ds.Tables[0]);
        }
    }
}