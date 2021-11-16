using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class HariciPuanSiraTaslak : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public DataSet ds { get; set; }
        public HariciPuanSiraTaslak(string tc, string oturum, string idSinav)
        {
            InitializeComponent();

            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
        }
        public XRLabel lbl { get; set; }
        public float LX { get; set; }
        public float LY { get; set; }
        private void HariciPuanSiraTaslak_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            List<string> baslikList = new List<string>() { "PUAN", "Sınıf", "Okul", "İlçe", "İl", "Genel" };
            float uzunluk = 50;
            float boy = 40;


            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ISLEM", 1); // Rapor
                b.ParametreEkle("@ID_MENU", 17); // SinavListele

                ds = b.SorguGetir("sp_SinavHariciPuanSira");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[0], "ID_SINAVPUANTURU");
                    LX = 0;

                    lbl = PublicMetods.lblEkle("TCKIMLIKNO", LX, LY, uzunluk * 2, boy * 2, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    ReportHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                    lbl = PublicMetods.lblEkle("AD SOYAD", LX, LY, uzunluk * 2, boy * 2, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    ReportHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;

                    foreach (DataRow dr in dt.Rows)
                    {
                        LY = 0;
                        lbl = PublicMetods.lblEkle(dr["PUANTURU"].ToString(), LX, LY, baslikList.Count * uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                        ReportHeader.Controls.Add(lbl);

                        LY += lbl.HeightF;
                        foreach (string baslik in baslikList)
                        {
                            lbl = PublicMetods.lblEkle(baslik, LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                            ReportHeader.Controls.Add(lbl);
                            LX += lbl.WidthF;
                        }

                    }


                    LY = 0;
                    lbl = PublicMetods.lblEkle("ILCE KATILIM SAYISI", LX, LY, uzunluk * 2, boy * 2, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    ReportHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                    lbl = PublicMetods.lblEkle("IL KATILIM SAYISI", LX, LY, uzunluk * 2, boy * 2, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    ReportHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                    lbl = PublicMetods.lblEkle("GENEL KATILIM SAYISI", LX, LY, uzunluk * 2, boy * 2, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    ReportHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;

                    foreach (DataRow dr in dt.Rows)
                    {
                        lbl = PublicMetods.lblEkle(dr["ID_SINAVPUANTURU"].ToString(), LX, LY, 0, boy * 2, Color.White, Color.White, Color.White);
                        ReportHeader.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }

                }

            }
        }
    }
}
