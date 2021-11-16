using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.Office.Utils;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class ssOgrenciAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public bool LiseMi { get; set; }
        public bool BursMu { get; set; }
        public bool SiralamayiGizle { get; set; }
        int i = 1;
        public ssOgrenciAnalizi(DataTable _dt,bool _LiseMi, bool _BursMu, bool _SiralamayiGizle)
        {
            dt = PublicMetods.orderBYtoTable(_dt, "BOLUMNO");
            LiseMi = _LiseMi;
            BursMu = _BursMu;
            SiralamayiGizle = _SiralamayiGizle;
            InitializeComponent();
        }



        float artim = 0;

        float LY = 0;
        float LX = 0;
        float height = 30;
        float sayfaBoyu = 810;
        FontFamily ff = new FontFamily("Tahoma");
        private void ssOgrenciAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            artim = sayfaBoyu / (dt.Rows.Count + 1);

            Color backColor = Color.FromArgb(180, 198, 231);
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;
            height = 30;

            XRLabel xrDersBaslik = new XRLabel()
            {
                WidthF = artim,
                HeightF = height,
                Text = "DERSLER",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                KeepTogether = true,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrDersBaslik.WidthF;
            PageHeader.Controls.Add(xrDersBaslik);

            foreach (DataRow dr in dt.Rows)
            {
                XRLabel xrDersAd = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = height,
                    Text = dr["DERSAD"].ToString(),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = dr["DERSAD"].ToString() == "TOPLAM" ? Color.Red : backColor,
                    ForeColor = dr["DERSAD"].ToString() == "TOPLAM" ? Color.White : foreColor,
                    KeepTogether = true,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = borderColor,
                };
                LX += xrDersAd.WidthF;
                PageHeader.Controls.Add(xrDersAd);
            }
            List<string> listTB = new List<string>() { "Soru Sayısı", "Doğru", "Yanlış", "Boş", "Net", "Başarı %"};
            List<string> listDT = new List<string>() { "TOPLAMSORU", "DOGRU", "YANLIS", "BOS", "NET", "YUZDEBASARI" };

            if (!LiseMi && !BursMu)
            {
                if (!SiralamayiGizle)
                {
                    listTB = new List<string>() { "Soru Sayısı", "Doğru", "Yanlış", "Boş", "Net", "Başarı %", "Sınıf Sırası", "Okul Sırası", "Genel Sırası" };
                    listDT = new List<string>() { "TOPLAMSORU", "DOGRU", "YANLIS", "BOS", "NET", "YUZDEBASARI", "SINIFSIRA", "OKULSIRA", "GENELSIRA" };
                }
                else
                {
                    listTB = new List<string>() { "Soru Sayısı", "Doğru", "Yanlış", "Boş", "Net", "Başarı %"};
                    listDT = new List<string>() { "TOPLAMSORU", "DOGRU", "YANLIS", "BOS", "NET", "YUZDEBASARI"};
                }
              
            }

            LY = 0;
            LX = 0;


            for (int i = 0; i < listTB.Count; i++)
            {
                backColor = i % 2 == 1 ? Color.FromArgb(180, 198, 231) : Color.FromArgb(217, 226, 243);

                LX = 0;

                XRLabel xrDersBaslik2 = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = height,
                    Text = listTB[i].ToString(),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    KeepTogether = true,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = borderColor,
                };
                LX += xrDersBaslik2.WidthF;
                Detail.Controls.Add(xrDersBaslik2);

                foreach (DataRow dr in dt.Rows)
                {                    
                    XRLabel xrDersAd2 = new XRLabel()
                    {
                        WidthF = artim,
                        HeightF = height,
                        Text = dr[listDT[i]].ToString(),
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = backColor,
                        ForeColor = foreColor,
                        KeepTogether = true,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = borderColor,
                    };
                    LX += xrDersAd2.WidthF;
                    Detail.Controls.Add(xrDersAd2);
                }
                LY += height;
            }


            //this.DataSource = dt;
            //if (Convert.ToBoolean(dt.Rows[0]["OLCEKSINAVI"].ToString()) == false)
            //{
            //    lblPuan.Text = "";
            //    lblPuanBaslik.Text = "";
            //}
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //lblDers.DataBindings.Add("Text", this.DataSource, "DERSAD");
            //lblSoruSayisi.DataBindings.Add("Text", this.DataSource, "TOPLAMSORU");
            //lblDogru.DataBindings.Add("Text", this.DataSource, "DOGRU");
            //lblYanlis.DataBindings.Add("Text", this.DataSource, "YANLIS");
            //lblBos.DataBindings.Add("Text", this.DataSource, "BOS");
            //lblNet.DataBindings.Add("Text", this.DataSource, "NET");
            //lblSinifNetOrt.DataBindings.Add("Text", this.DataSource, "SINIFNETORTALAMA");
            //lblYuzde.DataBindings.Add("Text", this.DataSource, "YUZDEBASARI");
            //lblSinifDogruOrt.DataBindings.Add("Text", this.DataSource, "SINIFDOGRUORTALAMA");
            //lblGenelDogruOrt.DataBindings.Add("Text", this.DataSource, "GENELDOGRUORTALAMA");
            //lblSinifSira.DataBindings.Add("Text", this.DataSource, "SINIFSIRA");
            //lblOkulSira.DataBindings.Add("Text", this.DataSource, "OKULSIRA");
            //lblGenelSira.DataBindings.Add("Text", this.DataSource, "GENELSIRA");

            //if (i % 2 == 1)
            //{
            //    lblDers.BackColor = Color.White;
            //    lblSoruSayisi.BackColor = Color.White;
            //    lblDogru.BackColor = Color.White;
            //    lblYanlis.BackColor = Color.White;
            //    lblBos.BackColor = Color.White;
            //    lblNet.BackColor = Color.White;
            //    lblSinifNetOrt.BackColor = Color.White;
            //    lblYuzde.BackColor = Color.White;
            //    lblSinifDogruOrt.BackColor = Color.White;
            //    lblGenelDogruOrt.BackColor = Color.White;
            //    lblSinifSira.BackColor = Color.White;
            //    lblOkulSira.BackColor = Color.White;
            //    lblGenelSira.BackColor = Color.White;
            //    lblPuan.BackColor = Color.White;
            //}
            //else
            //{
            //    lblDers.BackColor = Color.FromArgb(217, 226, 243);
            //    lblSoruSayisi.BackColor = Color.FromArgb(217, 226, 243);
            //    lblDogru.BackColor = Color.FromArgb(217, 226, 243);
            //    lblYanlis.BackColor = Color.FromArgb(217, 226, 243);
            //    lblBos.BackColor = Color.FromArgb(217, 226, 243);
            //    lblNet.BackColor = Color.FromArgb(217, 226, 243);
            //    lblSinifNetOrt.BackColor = Color.FromArgb(217, 226, 243);
            //    lblYuzde.BackColor = Color.FromArgb(217, 226, 243);
            //    lblSinifDogruOrt.BackColor = Color.FromArgb(217, 226, 243);
            //    lblGenelDogruOrt.BackColor = Color.FromArgb(217, 226, 243);
            //    lblSinifSira.BackColor = Color.FromArgb(217, 226, 243);
            //    lblOkulSira.BackColor = Color.FromArgb(217, 226, 243);
            //    lblGenelSira.BackColor = Color.FromArgb(217, 226, 243);
            //    lblPuan.BackColor = Color.FromArgb(217, 226, 243);
            //}

            //i++;

            //bool OLCEKSINAVI = Convert.ToBoolean(GetCurrentColumnValue("OLCEKSINAVI").ToString());
            //if (OLCEKSINAVI == false)
            //{
            //    lblPuan.Text = "";
            //    //lblPuanBaslik.Visible = false;
            //}
            //else
            //{
            //    lblPuan.DataBindings.Add("Text", this.DataSource, "PUAN");
            //}
        }
    }
}
