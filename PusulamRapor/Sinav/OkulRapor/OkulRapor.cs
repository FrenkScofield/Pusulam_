using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using PusulamRapor.Sinav.OkulRapor;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OkulRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_SINIF { get; set; }


        DataSet ds = new DataSet();
        DataTable dt0 = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        DataTable dt9 = new DataTable();
        DataTable dt10 = new DataTable();
        DataTable dt11 = new DataTable();
        DataTable dt12 = new DataTable();
        DataTable dt13 = new DataTable();
        DataTable dt14 = new DataTable();
        DataTable dt15 = new DataTable();
        DataTable dt16 = new DataTable();
        DataTable dtKATILIM = new DataTable();
        DataTable dt18 = new DataTable();
        DataTable dt19 = new DataTable();
        DataTable dt20 = new DataTable();
        DataTable dt21 = new DataTable();
        DataTable dt22 = new DataTable();
        DataTable dt23 = new DataTable();
        DataTable dt24 = new DataTable();
        DataTable dt25 = new DataTable();

        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public string SINAVTARIH { get; set; }

        public static int dersSayisi { get; set; }

        //pusulam/Rapor.aspx?rapor=Sinav.OkulRapor.OkulRapor&raporTur=PDF&p=32051057542;958AA090-EAE3-461C-80AA-C9573D246A8A;11;161
        public OkulRapor(string tc, string oturum, string idSube, string idSinav)//, string idSinif
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SUBE = Convert.ToInt32(idSube);
            //ID_SINIF = Convert.ToInt32(idSinif);
        }

        private void OkulRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);//161
                b.ParametreEkle("@ID_SUBE", ID_SUBE);//11
                //b.ParametreEkle("@ID_SINIF", ID_SINIF);//11
                b.ParametreEkle("@ID_MENU", 1074);//?
                b.ParametreEkle("@ISLEM", 1);
                ds = b.SorguGetir("sp_OkulRapor");
            }

            dt0 = ds.Tables[0];
            dt1 = ds.Tables[1];
            dt2 = ds.Tables[2];
            dt3 = ds.Tables[3];
            dt4 = ds.Tables[4];
            dt5 = ds.Tables[5];
            dt6 = ds.Tables[6];
            dt7 = ds.Tables[7];
            dt8 = ds.Tables[8];
            dt9 = ds.Tables[9];
            dt10 = ds.Tables[10];
            dt11 = ds.Tables[11];
            dt12 = ds.Tables[12];
            dt13 = ds.Tables[13];
            dt14 = ds.Tables[14];
            dt15 = ds.Tables[15];
            dt16 = ds.Tables[16];
            dtKATILIM = ds.Tables[17];
            dt18 = ds.Tables[18];
            dt19 = ds.Tables[19];
            dt20 = ds.Tables[20];
            dt21 = ds.Tables[21];
            dt22 = ds.Tables[22];
            dt23 = ds.Tables[23];
            dt24 = ds.Tables[24];
            dt25 = ds.Tables[25];

            List<string> listDersKisa = new List<string>();
            List<string> listDersUzun = new List<string>();
            dersSayisi = dt1.Rows.Count;
            foreach (DataRow dr in dt1.Rows)
            {
                listDersUzun.Add(dr["TAKMAAD"].ToString().ToUpper());
                listDersKisa.Add(dr["TAKMAAD"].ToString().ToUpper().Substring(0, 3));
            }

            SUBEAD = dt0.Rows[0]["SUBEAD"].ToString();
            SUBEIL = dt0.Rows[0]["SUBEIL"].ToString();
            SUBEILCE = dt0.Rows[0]["SUBEILCE"].ToString();
            SINAVAD = dt0.Rows[0]["SINAVAD"].ToString();
            SINAVTARIH = dt0.Rows[0]["SINAVTARIH"].ToString();

            lbl_SinavAdi.Text = SINAVAD;
            lbl_SinavTarihi.Text = SINAVTARIH;
            lbl_subeAdi.Text = SUBEAD;

            if (Convert.ToBoolean(dt18.Rows[0][0]))
            {
                //Sayfa 2 Üst
                OR_OkulNetPuanOrt OkulNetPuanOrt = new OR_OkulNetPuanOrt(dt2, dt3, SUBEAD, SUBEIL, SUBEILCE, listDersKisa, listDersUzun);
                xrSubreport_OkulNet.ReportSource = OkulNetPuanOrt;

                //Sayfa 2 Alt
                OR_SinifNetPuanOrt SinifNetPuanOrt = new OR_SinifNetPuanOrt(dt4, dt5, SUBEAD, SUBEIL, SUBEILCE, listDersKisa, listDersUzun);
                xrSubreport_SinifOrt.ReportSource = SinifNetPuanOrt;

                ////Sayfa 6
                if (dt9.Rows.Count > 0 && dt10.Rows.Count > 0 && dt11.Rows.Count > 0 && dtKATILIM.Rows.Count > 0)
                {
                    OR_OkulPuanListesi OkulPuanListesi = new OR_OkulPuanListesi(dt9, dt10, dt11, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
                    xrSubreport_OkulPuanList.ReportSource = OkulPuanListesi;
                }

                ////Sayfa 10
                //if (dt9.Rows.Count>0&& dt10.Rows.Count > 0 && dt11.Rows.Count > 0 && dtKATILIM.Rows.Count > 0)
                //{
                //    OR_SinifPuanListesi SinifPuanListesi = new OR_SinifPuanListesi(dt9, dt10, dt11, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
                //    xrSubreport_SinifPuanListesi.ReportSource = SinifPuanListesi;
                //}

                //Sayfa 13
                if (dt15.Rows.Count > 0 && dt16.Rows.Count > 0)
                {
                    OR_OkulSinavKarsilastirma OkulSinavKarsilastirma = new OR_OkulSinavKarsilastirma(dt15, dt16, SUBEAD, SUBEIL, SUBEILCE, listDersKisa, listDersUzun);
                    xrSubreport_OkulSinavKarsilastirma.ReportSource = OkulSinavKarsilastirma;
                }

                if (dt21.Rows.Count > 0)
                {
                    OR_PuanBaraj PuanBaraj = new OR_PuanBaraj(dt21, dt22, SUBEAD, SINAVAD, SINAVTARIH, listDersUzun, "150");
                    xrSubreport_150Baraj.ReportSource = PuanBaraj;
                }

                if (dt22.Rows.Count > 0)
                {
                    OR_PuanBaraj PuanBaraj = new OR_PuanBaraj(dt23, dt24, SUBEAD, SINAVAD, SINAVTARIH, listDersUzun, "180");
                    xrSubreport_180Baraj.ReportSource = PuanBaraj;
                }
            }
            else
            {
                SubBand1.Visible = false;
                SubBand4.Visible = false;
                SubBand7.Visible = false;
                SubBand10.Visible = false;
                xrSubreport_OkulSinavKarsilastirma.Visible = false;
            }           

            //Sayfa 3
            OR_OkulBasariGrafik OkulBasariGrafik = new OR_OkulBasariGrafik(dt6, SUBEAD, SUBEIL, SUBEILCE, listDersKisa, listDersUzun);
            xrSubreport_OkulBasariGrafik.ReportSource = OkulBasariGrafik;

            //Sayfa 5
            OR_OkulDersBasariSirali OkulDersBasariSirali = new OR_OkulDersBasariSirali(dt7, dt8, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
            xrSubreport_okulDersBasariSirali.ReportSource = OkulDersBasariSirali;            

            ////Sayfa 7
            OR_OkulKonuAnalizi OkulKonuAnalizi = new OR_OkulKonuAnalizi(dt12, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
            xrSubreport_OkulKonuAnaliz.ReportSource = OkulKonuAnalizi;

            //Sayfa 9
            OR_SinifKonuAnalizi SinifKonuAnalizi = new OR_SinifKonuAnalizi(dt13, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
            xrSubreport_SinifKonuAnaliz.ReportSource = SinifKonuAnalizi;


            ////Sayfa 11
            //OR_SinifNetListesi SinifNetListesi = new OR_SinifNetListesi(dt7, dt8, dt2, dt4, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
            //xrSubreport_SinifNetListesi.ReportSource = SinifNetListesi;

            //Sayfa 11
            OR_SinifNetPuanGenel SinifNetListesi = new OR_SinifNetPuanGenel(Convert.ToBoolean(dt18.Rows[0][0]), dt2, dt4, dt7, dt8, dt9, dt10, dt11, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, listDersKisa, listDersUzun);
            xrSubreport_SinifNetPuanGenel.ReportSource = SinifNetListesi;

            //Sayfa 12
            OR_OkulSoruFrekans OkulSoruFrekans = new OR_OkulSoruFrekans(dt14, dtKATILIM, SUBEAD,SUBEIL,SUBEILCE,SINAVAD, listDersKisa, listDersUzun);
            xrSubreport_OkulSoruFrekans.ReportSource=OkulSoruFrekans;

            //Sayfa 13
            OR_NetPuanOrtalamalari NetPuanOrtalamalari = new OR_NetPuanOrtalamalari(dt19, dt20, SINAVAD);
            xrSubreport_NetPuanOrtalamalari.ReportSource = NetPuanOrtalamalari;

            if (dt25.Rows.Count>0)
            {
                OR_Katilmayanlar Katilmayanlar = new OR_Katilmayanlar(dt25);
                xrSubreport_Katilmayanlar.ReportSource = Katilmayanlar;
            }
            
        }
    }
}
