using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Viu
{
    public partial class MenuLog : DevExpress.XtraReports.UI.XtraReport
    { 
      

        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }

        public string ID_SUBELIST { get; set; }
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

        public MenuLog(string tc, string oturum, string id_subelist)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_SUBELIST = id_subelist;
            InitializeComponent();
        }
        private void MenuLog_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)

        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 2);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_MENU", 1236);

                b.ParametreEkle("@ID_SUBELIST", ID_SUBELIST);

                DataSet ds = b.SorguGetir("sp_VIU");

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Detail.Controls.Clear();
                    return;
                }

                DataTable dt = ds.Tables[0];

                istisna.Add("");

                en = sayfaEn / dt.Columns.Count;

                Baslik(dt);
                Icerik(dt);

                this.DataSource = dt;

                FillReportDataFields.Fill(Detail, dt);
            }
        }

        private void Baslik(DataTable dt)
        {
            LX = 0;
            LY = 0;

            foreach (DataColumn dc in dt.Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    lbl = PublicMetods.lblBaslik(dc.ToString(), LX, LY, en, boy);
                    PageHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }
        }

        private void Icerik(DataTable dt)
        {
            LX = 0;
            LY = 0;
            //boy = 150f;

            foreach (DataColumn dc in dt.Columns)
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
