using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.IO;

namespace PusulamRapor.Frekans
{
    public partial class SinavOgrenciFrekansRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public DataTable OGRLIST { get; set; }
        public DataTable SINAVLIST { get; set; }
        public DataTable DERSLIST { get; set; }

        public SinavOgrenciFrekansRapor(string TCKIMLIKNO, string OTURUM, string ID_SUBELIST, string DONEM, string ID_KADEME3, string ID_SINAVTURU, string _DERSLIST)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);
                b.ParametreEkle("@DERSLIST", _DERSLIST);
                b.ParametreEkle("@ISJSON", false);
                b.ParametreEkle("@ID_MENU", 1296);
                ds = b.SorguGetir("sp_FrekansSinavOgrenci");
                //this.DataSource = ds.Tables[0];
            }

            OGRLIST = PublicMetods.orderBYtoTable(ds.Tables[0], "[KAMPÜS],[SINIF],[AD],[SOYAD]");
            SINAVLIST = PublicMetods.orderBYtoTable(ds.Tables[1], "[RN]");
            DERSLIST = PublicMetods.orderBYtoTable(ds.Tables[2], "[RN],[TAKMAAD]");

            Baslik();
            Icerik();

            this.DataSource = OGRLIST;
            FillReportDataFields.Fill(Detail, OGRLIST);
        }


        private void Baslik()
        {
            LX = 0;
            LY = 0;

            List<string> listeBaslik = new List<string>() { "Kampüs", "Sınıf", "TC No", "Adı", "Soyadı" };

            foreach (string item in listeBaslik)
            {
                lbl = PublicMetods.lblBaslik(item, LX, LY, en, boy * 2);
                PageHeader.Controls.Add(lbl);
                LX += en;
            }

            foreach (DataRow drDers in DERSLIST.Rows)
            {
                lbl = PublicMetods.lblBaslik(drDers["TAKMAAD"].ToString() + Environment.NewLine + "Soru Sayısı:" + drDers["SS"].ToString(), LX, LY, en * SINAVLIST.Rows.Count, boy);
                PageHeader.Controls.Add(lbl);

                foreach (DataRow drSinav in SINAVLIST.Rows)
                {
                    lbl = PublicMetods.lblBaslik(drSinav["SINAVAD"].ToString() + Environment.NewLine + "FREKANS", LX, LY + boy, en, boy);
                    PageHeader.Controls.Add(lbl);
                    LX += en;
                }
            }

            //foreach (DataColumn dc in ds.Tables[0].Columns)
            //{
            //    if (dc.ToString() != "TCKIMLIKNO")
            //    {
            //        lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            //        PageHeader.Controls.Add(lbl);
            //        LX += lbl.WidthF;
            //    }
            //}
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 50f;


            List<string> listeBaslik = new List<string>() { "SUBEAD", "SINIF", "TCKIMLIKNO", "AD", "SOYAD" };

            foreach (string item in listeBaslik)
            {
                lbl = PublicMetods.lblDetay(item, LX, LY, en, boy,"1");
                Detail.Controls.Add(lbl);
                LX += en;
            }


            foreach (DataRow drDers in DERSLIST.Rows)
            {
                foreach (DataRow drSinav in SINAVLIST.Rows)
                {
                    lbl = PublicMetods.lblDetay(drDers["TAKMAAD"].ToString() + "-" + drSinav["ID_SINAV"].ToString(), LX, LY, en, boy, "1");
                    Detail.Controls.Add(lbl);
                    LX += en;
                }
            }


            //foreach (DataColumn dc in ds.Tables[0].Columns)
            //{
            //    if (dc.ToString() != "TCKIMLIKNO")
            //    {
            //        lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue, "1");
            //        Detail.Controls.Add(lbl);
            //        LX += lbl.WidthF;
            //    }
            //}

        }
    }
}
