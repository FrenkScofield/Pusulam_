using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PusulamRapor.Sinav.Online
{
    public partial class Sinav : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public bool CEVAPANAHTARI { get; set; }

        public DataSet ds { get; set; }
        public XRLabel lbl { get; set; }

        float LX = 0;
        float LY = 0;
        float en = 150;
        float boy = 40;

        public Sinav(string tc, string oturum, string idSinav, string cevapAnahtari)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            CEVAPANAHTARI = Convert.ToBoolean(cevapAnahtari);
        }

        public class soru
        {
            public int BolumNo { get; set; }
            public int SoruNo { get; set; }
            public string SoruHtml { get; set; }
        }

        private void Sinav_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@CEVAPANAHTARI", CEVAPANAHTARI);
                b.ParametreEkle("@ISLEM", 17); // Sınav Detay Rapor
                b.ParametreEkle("@ID_MENU", 1358);

                ds = b.SorguGetir("sp_OnlineSinav");
            }

            lblSinavAd.Text = ds.Tables[0].Rows[0]["SINAVAD"].ToString();

            GroupField grpFieldBOLUMNO = new GroupField("BOLUMNO");
            GroupHeader1.GroupFields.Add(grpFieldBOLUMNO);
            GroupField grpFieldSORUNO = new GroupField("SORUNO");
            GroupHeader2.GroupFields.Add(grpFieldSORUNO);

            DataTable dt = PublicMetods.orderBYtoTable(ds.Tables[1], "[BOLUMNO],[SORUNO],[CEVAPNO]");
            this.DataSource = dt;

            List<soru> soruList = new List<soru>();

            foreach (DataRow item in dt.Rows)
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = System.Text.Encoding.UTF8;

                        soru tempSoru = new soru()
                        {
                            BolumNo = Convert.ToInt32(item["BOLUMNO"]),
                            SoruNo = Convert.ToInt32(item["SORUNO"]),
                            SoruHtml = item["SORUHTML"].ToString()
                        };

                        if (soruList.FindIndex(x => x.BolumNo == tempSoru.BolumNo && x.SoruNo == tempSoru.SoruNo) == -1)
                        {
                            item["SORUHTML"] = client.DownloadString("https://pusulam.okyanuskoleji.k12.tr/" + tempSoru.SoruHtml).Replace("\"/assets/ckeditor/plugins/ckeditor_wiris", "\"https://sorubankasi.okyanuskoleji.k12.tr/assets/ckeditor/plugins/ckeditor_wiris");

                            tempSoru.SoruHtml = item["SORUHTML"].ToString();
                            soruList.Add(tempSoru);
                        }
                        else
                        {
                            item["SORUHTML"] = soruList.Find(x => x.BolumNo == tempSoru.BolumNo && x.SoruNo == tempSoru.SoruNo).SoruHtml;
                        }
                        item["CEVAPHTML"] = client.DownloadString("https://pusulam.okyanuskoleji.k12.tr/" + item["CEVAPHTML"]).Replace("\"/assets/ckeditor/plugins/ckeditor_wiris", "\"https://sorubankasi.okyanuskoleji.k12.tr/assets/ckeditor/plugins/ckeditor_wiris");
                    }
                }
                catch (Exception ex)
                {

                    var kks = ex;
                    //item["SORUHTML"] = "";
                    //item["CEVAPHTML"] = "";
                }
            }

            FillReportDataFields.FillPanel(GroupHeader1, dt);
            FillReportDataFields.FillPanel(GroupHeader2, dt);
            FillReportDataFields.FillPanel(Detail, dt);

            if (CEVAPANAHTARI && ds.Tables[2].Rows.Count > 0)
                CevapAnahtari(PublicMetods.orderBYtoTable(ds.Tables[2], "[BOLUMNO],[SORUNO]"));
        }

        private void CevapAnahtari(DataTable dt)
        {
            LX = 0;
            LY = 0;
            boy = 50f;

            lbl = PublicMetods.lblBaslik("CEVAP ANAHTARI", LX, LY, PageWidth - 40f, boy);
            ReportFooter.Controls.Add(lbl);
            float baslikHeight = lbl.HeightF + 15f;


            DataTable dtDers = PublicMetods.orderBYtoTable(dt, "[BOLUMNO]", new string[] { "BOLUMNO", "TAKMAAD" });

            en = ((PageWidth - 40f) / dtDers.Rows.Count) - 10;

            foreach (DataRow ders in dtDers.Rows)
            {
                LY = baslikHeight;
                lbl = PublicMetods.lblBaslik(ders["TAKMAAD"].ToString(), LX, LY, en, boy);
                ReportFooter.Controls.Add(lbl);
                LY += lbl.HeightF;

                float lxx = LX;
                bool altSatir = false;

                foreach (DataRow soru in ((DataTable)dt.Select("BOLUMNO=" + ders["BOLUMNO"].ToString()).CopyToDataTable()).Rows)
                {

                    lbl = PublicMetods.lblEkle(soru["SORUNO"].ToString(), LX, LY, en / 4, boy / 2, Color.White, Color.FromArgb(76, 166, 213), Color.FromArgb(76, 166, 213), new Font(new FontFamily("Tahoma"), 8, FontStyle.Bold));
                    //bl = PublicMetods.lblDetay(soru["SORUNO"].ToString(), LX, LY, en / 4, boy / 2,"");
                    ReportFooter.Controls.Add(lbl);
                    LX += lbl.WidthF;


                    lbl = PublicMetods.lblEkle(soru["CEVAP"].ToString(), LX, LY, en / 4, boy / 2, Color.White, Color.Brown, Color.FromArgb(76, 166, 213), new Font(new FontFamily("Tahoma"), 8, FontStyle.Bold));
                    //lbl = PublicMetods.lblDetay(soru["CEVAP"].ToString(), LX, LY, en / 4, boy / 2, "");
                    ReportFooter.Controls.Add(lbl);
                    LX += lbl.WidthF;

                    if (altSatir)
                    {
                        LY += lbl.HeightF;
                        LX -= lbl.WidthF * 4;
                        altSatir = false;
                    }
                    else
                        altSatir = true;

                }
                LX = lxx + en + 10;
            }
        }

    }
}
