using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_SecimYapmayanlar : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string ID_SUBELIST { get; set; }

        DataSet ds;
        public XRLabel lbl { get; set; }
        List<string> istisna = new List<string>();

        float LX = 0;
        float LY = 0;
        float en = 130;
        float boy = 25;
        public YG_SecimYapmayanlar(string tc, string oturum, string donem, string idSubeList)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            DONEM = donem;
            ID_SUBELIST = idSubeList;
        }

        private void YG_SecimYapmayanlar_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1234);

                ds = b.SorguGetir("sp_YG_SecimYapmayanlar");



                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Detail.Controls.Clear();
                    return;
                }

                DataTable ogrenciList = PublicMetods.orderBYtoTable(ds.Tables[0], "[KAMPÜS],KADEME3SIRA,SINIF,AD,SOYAD");

                //istisna.Add("TCKIMLIKNO");
                istisna.Add("KADEME3SIRA");

                Baslik();
                Icerik();

                this.DataSource = ogrenciList;

                FillReportDataFields.Fill(Detail, ogrenciList);
            }
        }
       
        private void Baslik()
        {
            LX = 0;
            LY = 0;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    lbl = PublicMetods.lblBaslik(dc.ToString(), LX, LY, en, boy);
                    PageHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 50f;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    lbl = PublicMetods.lblDetay(dc.ToString(), LX, LY, en, boy, "1");
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }

        }
    }
}