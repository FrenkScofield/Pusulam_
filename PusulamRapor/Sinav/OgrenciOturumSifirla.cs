using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class OgrenciOturumSifirla : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public bool DIS_OGRENCI { get; set; }

        DataSet ds;
        List<string> istisna = new List<string>();
        List<DataRow> list = new List<DataRow>();
        public XRLabel lbl { get; set; }
        public XRRichText rt { get; set; }

        float LX = 0f;
        float LY = 0f;
        float en = 130f;
        float boy = 50f;

        float sayfaEn = 1169F - 20F;

        FontFamily ff = new FontFamily("Tahoma");
        
        public OgrenciOturumSifirla(string tc, string oturum, string id_sinav, string dis_ogrenci)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(id_sinav);
            DIS_OGRENCI = Convert.ToBoolean(dis_ogrenci);
            InitializeComponent();
        }
        private void OgrenciOturumSifirla_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@DIS_OGRENCI", DIS_OGRENCI);
                b.ParametreEkle("@ISLEM", 32);
                b.ParametreEkle("@ID_MENU", 1358);
                ds = b.SorguGetir("sp_OnlineSinav");

             
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
