using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav.Bursluluk
{
    public partial class KatilimRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_BURSLULUKDOSYA { get; set; }
        public bool RAPORGENEL { get; set; }
        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float uzunluk = 150;
        float boy = 40;


        public KatilimRaporu(string tc, string oturum, string idBurslulukDosya, string raporGenel)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_BURSLULUKDOSYA = Convert.ToInt32(idBurslulukDosya);
            RAPORGENEL = Convert.ToInt32(raporGenel) > 0;

        }

        private void KatilimRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_BURSLULUKDOSYA", ID_BURSLULUKDOSYA);
                b.ParametreEkle("@RAPORGENEL", RAPORGENEL);
                b.ParametreEkle("@ISLEM", 8); // Rapor
                b.ParametreEkle("@ID_MENU", 1176);

                ds = b.SorguGetir("sp_BurslulukOgrenciIslemleri");
            }

            GroupField SINAVTARIHField = new GroupField("SINAVTARIH");
            GroupField SEANSField = new GroupField("SEANS");
            GroupHeader1.GroupFields.Add(SINAVTARIHField);
            GroupHeader1.GroupFields.Add(SEANSField);

            this.DataSource = ds.Tables[4];
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string baslik = "TÜM SEANSLARIN KATILIM RAPORU";
            string sinavTarih = GetCurrentColumnValue("SINAVTARIH").ToString();
            string sinavGun = GetCurrentColumnValue("SINAVGUN").ToString();
            string seans = GetCurrentColumnValue("SEANS").ToString();
            if (!RAPORGENEL)
            {
                baslik = string.Format("{0} {1}, Saat {2}",sinavTarih, sinavGun, seans);
            }

            GroupHeader1.Controls.Clear();
            Detail.Controls.Clear();
            LX = 0;
            LY = 0;
            lbl = PublicMetods.lblEkle(baslik, LX, LY, ((uzunluk * 2 * (ds.Tables[3].Rows.Count + 1))), boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            GroupHeader1.Controls.Add(lbl);



            lbl = PublicMetods.lblEkle("SINIF", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            Detail.Controls.Add(lbl);
            LX += lbl.WidthF;

            foreach (DataRow dr in ds.Tables[3].Rows)
            {
                lbl = PublicMetods.lblEkle(dr["KADEMEAD"].ToString(), LX, LY, uzunluk * 2, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;
            }

            lbl = PublicMetods.lblEkle("KATILIM", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            Detail.Controls.Add(lbl);
            LX += lbl.WidthF;

            LY += lbl.HeightF;


            LX = 0;

            lbl = PublicMetods.lblEkle("ŞUBE", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            Detail.Controls.Add(lbl);
            LX += lbl.WidthF;
            for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
            {
                lbl = PublicMetods.lblEkle("BAŞVURAN"+System.Environment.NewLine+" ÖĞR. SAY.", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;
                lbl = PublicMetods.lblEkle("KATILAN" + System.Environment.NewLine + " ÖĞR. SAY.", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;
            }
            lbl = PublicMetods.lblEkle("%", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            Detail.Controls.Add(lbl);
            LX += lbl.WidthF;

            LY += lbl.HeightF;

            int idSube = 9999;
            foreach (DataRow drSube in ds.Tables[2].Rows)
            {
                LX = 0;
                idSube = Convert.ToInt32(drSube["ID_SUBE"]);

                lbl = PublicMetods.lblEkle(drSube["SUBEAD"].ToString(), LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                Detail.Controls.Add(lbl);
                LX += lbl.WidthF;

                foreach (DataRow drKademe3 in ds.Tables[3].Rows)
                {

                    int idKademe3 = Convert.ToInt32(drKademe3["ID_KADEME3"]);
                    try
                    {
                        foreach (DataRow drVeri in ds.Tables[0].Select(String.Format("((0={4} AND SINAVTARIH='{0}' AND SEANS='{1}') OR 1={4}) AND ID_SUBE={2} AND ID_KADEME3={3}", sinavTarih, seans, idSube, idKademe3, RAPORGENEL)).CopyToDataTable().Rows)
                        {
                            lbl = PublicMetods.lblEkle(drVeri["BASVURAN_OGRSAY"].ToString(), LX, LY, uzunluk, boy, Color.White, Color.MidnightBlue, Color.SkyBlue);
                            Detail.Controls.Add(lbl);
                            LX += lbl.WidthF;

                            lbl = PublicMetods.lblEkle(drVeri["KATILAN_OGRSAY"].ToString(), LX, LY, uzunluk, boy, Color.White, Color.MidnightBlue, Color.SkyBlue);
                            Detail.Controls.Add(lbl);
                            LX += lbl.WidthF;
                        }
                    }
                    catch (Exception)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            lbl = PublicMetods.lblEkle("-", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                            Detail.Controls.Add(lbl);
                            LX += lbl.WidthF;
                        }

                    }
                }
                // KATILIM YUZDESI
                try
                {
                    foreach (DataRow drVeri in ds.Tables[1].Select(String.Format("((0={3} AND SINAVTARIH='{0}' AND SEANS='{1}') OR 1={3}) AND  ID_SUBE={2}", sinavTarih, seans, idSube,RAPORGENEL)).CopyToDataTable().Rows)
                    {
                        lbl = PublicMetods.lblEkle("% "+drVeri["KATILIMYUZDESI"].ToString(), LX, LY, uzunluk, boy, Color.White, Color.MidnightBlue, Color.SkyBlue);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;

                    }
                }
                catch (Exception)
                {
                    lbl = PublicMetods.lblEkle("-", LX, LY, uzunluk, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    Detail.Controls.Add(lbl);
                }

                LY += boy;
            }


            LX = 0;
            idSube = 9999;

            lbl = PublicMetods.lblEkle("GENEL", LX, LY, uzunluk, boy, Color.Red, Color.White, Color.White);
            Detail.Controls.Add(lbl);
            LX += lbl.WidthF;

            foreach (DataRow drKademe3 in ds.Tables[3].Rows)
            {

                int idKademe3 = Convert.ToInt32(drKademe3["ID_KADEME3"]);
                try
                {
                    foreach (DataRow drVeri in ds.Tables[0].Select(String.Format("((0={4} AND SINAVTARIH='{0}' AND SEANS='{1}') OR 1={4}) AND ID_SUBE={2} AND ID_KADEME3={3}", sinavTarih, seans, idSube, idKademe3, RAPORGENEL)).CopyToDataTable().Rows)
                    {
                        lbl = PublicMetods.lblEkle(drVeri["BASVURAN_OGRSAY"].ToString(), LX, LY, uzunluk, boy, Color.Red, Color.White, Color.SkyBlue);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;

                        lbl = PublicMetods.lblEkle(drVeri["KATILAN_OGRSAY"].ToString(), LX, LY, uzunluk, boy, Color.Red, Color.White, Color.SkyBlue);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }
                }
                catch (Exception)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        lbl = PublicMetods.lblEkle("-", LX, LY, uzunluk, boy, Color.Red, Color.White, Color.White);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }

                }
            }
            // KATILIM YUZDESI
            try
            {
                foreach (DataRow drVeri in ds.Tables[1].Select(String.Format("((0={3} AND SINAVTARIH='{0}' AND SEANS='{1}') OR 1={3}) AND  ID_SUBE={2}", sinavTarih, seans, idSube,RAPORGENEL)).CopyToDataTable().Rows)
                {
                    lbl = PublicMetods.lblEkle("% " + drVeri["KATILIMYUZDESI"].ToString(), LX, LY, uzunluk, boy, Color.Red, Color.White, Color.SkyBlue);
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;

                }
            }
            catch (Exception)
            {
                lbl = PublicMetods.lblEkle("-", LX, LY, uzunluk, boy, Color.Red, Color.White, Color.White);
                Detail.Controls.Add(lbl);
            }

            LY += boy;

            LX = 0;
            lbl = PublicMetods.lblEkle("", LX, LY, ((uzunluk * 2 * (ds.Tables[3].Rows.Count + 1)) + (uzunluk * 2)), boy, Color.White, Color.MidnightBlue, Color.White);
            Detail.Controls.Add(lbl);

            LY += boy;
        }
    }
}
