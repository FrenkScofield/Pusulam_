using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class KarmaListe : DevExpress.XtraReports.UI.XtraReport
    {

        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string SQLJSON { get; set; }
        public int SECENEK { get; set; }
        public bool TCGOZUKSUNMU { get; set; }
        public bool KENDISINIF { get; set; }
        public bool BURSLULUK { get; set; }
        public string JSEXCEL { get; set; }
        public string SINAVTARIH { get; set; }
        public bool YENIDAGIT { get; set; }
        public bool SINIFKAPASITE { get; set; }
        public int ID_BURSLULUKDOSYA { get; set; }
        public string BURS_SINAVTARIH { get; set; }
        public string BURS_SEANS { get; set; }
        public string DONEM { get; set; }

        DataSet ds = new DataSet();

        public KarmaListe(string tc, string oturum, string sqlJSON, string secenek, string kendiSinif, string TCGozuksunMu, string bursluluk, string sinavTarih, string yeniDagit, string sinifKapasite, string idBurslulukDosya, string bursSinavTarih, string bursSeans,string donem)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            SQLJSON = sqlJSON;
            SINAVTARIH = sinavTarih;
            SECENEK = Convert.ToInt32(secenek);
            KENDISINIF = Convert.ToBoolean(kendiSinif);
            TCGOZUKSUNMU = Convert.ToBoolean(TCGozuksunMu);
            BURSLULUK = Convert.ToBoolean(bursluluk);
            YENIDAGIT = Convert.ToBoolean(yeniDagit);
            SINIFKAPASITE = Convert.ToBoolean(sinifKapasite);
            DONEM = donem;
            ID_BURSLULUKDOSYA = Convert.ToInt32(idBurslulukDosya);
            BURS_SINAVTARIH = bursSinavTarih;
            BURS_SEANS = bursSeans;
        }

        private void KarmaListe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (SECENEK == 1)
                {
                    xrLabel158.Text = "AD SOYAD SIRALI GENEL LİSTE"; // ad soyada göre sırala
                }
                else if (SECENEK == 2)
                {
                    xrLabel158.Text = "SINAV SINIFI YOKLAMA LİSTESİ";
                }
                else if (SECENEK == 3)
                {
                    xrLabel158.Text = "SINIF LİSTELERİ (KAPI LİSTELERİ)";
                }

                int islem = YENIDAGIT ? 1 : 3;

                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@SQLJSON", SQLJSON);
                    b.ParametreEkle("@BURSLULUK", BURSLULUK);
                    b.ParametreEkle("@KENDISINIF", KENDISINIF);
                    b.ParametreEkle("@SINIFKAPASITE", SINIFKAPASITE);

                    b.ParametreEkle("@SINAVTARIH", BURSLULUK ? BURS_SINAVTARIH : SINAVTARIH);

                    b.ParametreEkle("@ID_BURSLULUKDOSYA", ID_BURSLULUKDOSYA);
                    b.ParametreEkle("@SEANS", BURS_SEANS);
                    b.ParametreEkle("@DONEM", DONEM);

                    b.ParametreEkle("@ISLEM", islem); // Rapor
                    b.ParametreEkle("@ID_MENU", 1147);

                    ds = b.SorguGetir("sp_KarmaListe");

                    DataTable dt = ds.Tables[0];
                    if (SECENEK == 1)
                    {
                        this.DataSource = PublicMetods.orderBYtoTable(dt, "ADSOYAD");
                        GroupFooter1.Visible = false;
                    }
                    else if (SECENEK == 2)
                    {
                        this.DataSource = PublicMetods.orderBYtoTable(dt, "SINAVSINIFAD,SINIFAD,ADSOYAD");
                    }
                    else if (SECENEK == 3)
                    {
                        this.DataSource = PublicMetods.orderBYtoTable(dt, "SINIFAD,ADSOYAD");
                        GroupFooter1.Visible = false;
                        xrSubreport_SinavBilgi.Visible = false;
                        lbl1.Visible = false;
                        lbl2.Visible = false;
                        lbl3.Visible = false;
                        lbl4.Visible = false;
                        lbl5.Visible = false;
                        lbl6.Visible = false;
                    }

                }
                if (SECENEK == 1)
                {
                    GroupField bolumfield3 = new GroupField("SUBEAD");
                    GroupHeader1.GroupFields.Add(bolumfield3);
                }
                else if (SECENEK == 2)
                {
                    GroupField bolumfield2 = new GroupField("SUBEAD");
                    GroupHeader2.GroupFields.Add(bolumfield2);

                    GroupField bolumfield = new GroupField("SINAVSINIFSUBEAD");
                    GroupHeader1.GroupFields.Add(bolumfield);
                }
                else if (SECENEK == 3)
                {
                    GroupField bolumfield2 = new GroupField("SUBEAD");
                    GroupHeader2.GroupFields.Add(bolumfield2);

                    GroupField bolumfield = new GroupField("SINIFSUBEAD");
                    GroupHeader1.GroupFields.Add(bolumfield);
                }

                xrLabel_OgrSinif.DataBindings.Add("Text", this.DataSource, "SINIFAD");
                xrLabel_Okul.DataBindings.Add("Text", this.DataSource, "SUBEAD");
                xrLabel_OgrNo.DataBindings.Add("Text", this.DataSource, "OGRNO");
                xrLabel_AdSoyad.DataBindings.Add("Text", this.DataSource, "ADSOYAD");
                xrLabel_SinavAd.DataBindings.Add("Text", this.DataSource, "SINAVAD");
                xrLabel_Kur.DataBindings.Add("Text", this.DataSource, "DERSSEVIYE");
                xrLabel_SinavSinif.DataBindings.Add("Text", this.DataSource, "SINAVSINIFAD");

                if (TCGOZUKSUNMU == true)
                    xrLabel_TC.DataBindings.Add("Text", this.DataSource, "TCKIMLIKNO");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (SECENEK == 2)
            {
                KarmaListeBilgi dsBilgi = new KarmaListeBilgi(ds.Tables[1].Select(string.Format("SINIFAD='{0}'", GetCurrentColumnValue("SINAVSINIFSUBEAD"))).CopyToDataTable());
                xrSubreport_SinavBilgi.ReportSource = dsBilgi;
            }
            ogrenciSayisi = 1;
            if (SECENEK == 1)
            {
                KarmaListeBilgi dsBilgi = new KarmaListeBilgi(ds.Tables[2].Select(string.Format("SUBEAD='{0}'", GetCurrentColumnValue("SUBEAD"))).CopyToDataTable());
                xrSubreport_SinavBilgi.ReportSource = dsBilgi;
                ogrenciSayisi = 1;
            }
        }

        int ogrenciSayisi = 1;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel_Sira.Text = ogrenciSayisi.ToString();
            ogrenciSayisi++;
        }
    }
}
