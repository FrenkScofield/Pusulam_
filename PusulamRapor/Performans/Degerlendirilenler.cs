using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Performans
{
    public partial class Degerlendirilenler : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_PERIYOT { get; set; }


        DataSet ds;
        List<string> istisna = new List<string>();
        List<DataRow> list = new List<DataRow>();
        public XRLabel lbl { get; set; }
        public XRRichText rt { get; set; }

        float LX = 0f;
        float LY = 0f;
        float en = 20f;
        float boy = 20f;

        float sayfaEn = 800F - 20F;

        FontFamily ff = new FontFamily("Tahoma");
        
        public Degerlendirilenler(string tc, string oturum,string id_Periyot)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_PERIYOT = id_Periyot;
            InitializeComponent();
        }
        private void Degerlendirilenler_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("ID_PERIYOT", ID_PERIYOT);
                b.ParametreEkle("@ISLEM", 12);
                b.ParametreEkle("@ID_MENU", 1392);
                ds = b.SorguGetir("sp_Performans");

             
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Detail.Controls.Clear();
                    return;
                }

                DataTable dt = ds.Tables[0];

                istisna.Add("");

                en = sayfaEn / dt.Columns.Count;

                Baslik();
                Icerik();

                //dt = PublicMetods.orderBYtoTable(dt, "[Soru No]");

                this.DataSource = dt;

                FillReportDataFields.Fill(Detail, dt);
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
            //boy = 150f;

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
