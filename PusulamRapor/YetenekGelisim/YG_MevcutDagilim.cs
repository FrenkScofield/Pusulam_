using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_MevcutDagilim : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 100;
        float boy = 25;
        public YG_MevcutDagilim(string tc, string oturum, string donem)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            DONEM = donem;
        }

        private void YG_MevcutDagilim_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1215);

                ds = b.SorguGetir("sp_YG_MevcutDagilim");
            }

            en = (1169f - 40f) / (ds.Tables[0].Columns.Count - 1);
            
            GroupHeader1.GroupFields.Add(new GroupField("KATEGORI"));
            this.DataSource = ds.Tables[1];              
        }
        public string kategoriAd { get; set; }
        DataTable d = new DataTable();
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            kategoriAd = GetCurrentColumnValue("KATEGORI").ToString();

            GroupHeader1.Controls.Clear();
            Detail.Controls.Clear();

            Baslik();
            Icerik();
        }
        private void Baslik()
        {
            LX = 0;
            LY = 0;

            lbl = PublicMetods.lblEkle(kategoriAd, LX, LY, en * (ds.Tables[0].Columns.Count - 1), boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
            GroupHeader1.Controls.Add(lbl);
            LY += lbl.HeightF;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (dc.ToString() != "KATEGORI")
                {
                    lbl = PublicMetods.lblEkle(dc.ToString(), LX, LY, en, boy, Color.SkyBlue, Color.MidnightBlue, Color.White);
                    GroupHeader1.Controls.Add(lbl);
                    LX += lbl.WidthF;

                }
            }
        }
        private void Icerik()
        {
            LX = 0;
            LY = 0;

            d = ds.Tables[0].Select(String.Format("KATEGORI = '{0}'", kategoriAd)).CopyToDataTable();
            DataTable dt = PublicMetods.orderBYtoTable(d, "[KATEGORI],[SUBEAD]");

            foreach (DataRow dr in dt.Rows)
            {
                LX = 0;
                foreach (DataColumn dc in dt.Columns)
                {

                    if (dc.ToString() != "KATEGORI")
                    {
                        lbl = PublicMetods.lblEkle(dr[dc.ToString()].ToString(), LX, LY, en, boy, Color.White, Color.MidnightBlue, Color.SkyBlue);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }

                }
                LY += lbl.HeightF;
            }

        }
    }
}