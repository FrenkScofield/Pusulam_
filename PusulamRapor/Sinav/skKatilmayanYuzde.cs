using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class skKatilmayanYuzde : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        public bool katilim { get; set; }

        readonly FontFamily familyArial = new FontFamily("Arial");
        FontFamily ff = new FontFamily("Tahoma");
        float lblEn = 0;
        float lblBoy = 0;

        float LY = 2;
        float LX = 0;

        public skKatilmayanYuzde(DataTable dtt1, DataTable dtt2, bool _katilim)
        {
            InitializeComponent();
            dt1 = dtt1;
            dt2 = dtt2;
            katilim = _katilim;
            //dt=dt1.Select("SIRA=0").CopyToDataTable();

            //this.DataSource=dt1.Select("SIRA=0").CopyToDataTable(); // GENEL OLMAYANLARI AL
        }

        private void skKatilmayanYuzde_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            LY = 20;
            LX = 0;
            lblEn = 1130 / (dt2.Rows.Count + 2);
            lblBoy = 30;
            FontFamily ff = new FontFamily("Tahoma");

            XRLabel lblKampus = new XRLabel()
            {
                WidthF = lblEn,
                HeightF = lblBoy * 2,
                Text = "ŞUBE",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = System.Drawing.Color.SkyBlue,
                ForeColor = System.Drawing.Color.MidnightBlue,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = System.Drawing.Color.White,
                Tag = "Ogrt"
            };
            LX += lblKampus.WidthF;
            PageHeader.Controls.Add(lblKampus);


            XRLabel lblOS = new XRLabel()
            {
                WidthF = lblEn,
                HeightF = lblBoy * 2,
                Text = dt2.Rows[0]["KADEME3"].ToString() + " Öğrenci Sayısı",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = System.Drawing.Color.SkyBlue,
                ForeColor = System.Drawing.Color.MidnightBlue,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = System.Drawing.Color.White,
                Tag = "Ogrt"
            };
            LX += lblOS.WidthF;
            PageHeader.Controls.Add(lblOS);

            string yazi = dt2.Rows[0]["SINAVTURU"].ToString() + " DENEME SINAVLARINA KATILMAYAN ÖĞRENCİ SAYISI";
            if (katilim)
            {
                yazi = dt2.Rows[0]["SINAVTURU"].ToString() + " DENEME SINAVLARINA KATILAN ÖĞRENCİ SAYISI";
            }

            XRLabel lblBaslik = new XRLabel()
            {
                WidthF = lblEn * dt2.Rows.Count,
                HeightF = lblBoy,
                Text = yazi,
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = System.Drawing.Color.SkyBlue,
                ForeColor = System.Drawing.Color.MidnightBlue,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = System.Drawing.Color.White,
                Tag = "Ogrt"
            };
            //LX+=lblBaslik.WidthF;
            LY += lblBoy;
            PageHeader.Controls.Add(lblBaslik);

            foreach (DataRow item in dt2.Rows)
            {

                XRLabel lblSinav = new XRLabel()
                {
                    WidthF = lblEn,
                    HeightF = lblBoy,
                    Text = item["SINAVAD"].ToString(),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = System.Drawing.Color.SkyBlue,
                    ForeColor = System.Drawing.Color.MidnightBlue,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = System.Drawing.Color.White,
                    Tag = "Ogrt"
                };
                LX += lblSinav.WidthF;
                PageHeader.Controls.Add(lblSinav);
            }

            LY = 0;

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //DataRowView dtRow = (DataRowView)this.GetCurrentRow();
            //DataRow row = dtRow.Row;
            foreach (DataRow row in dt1.Rows)// dt1.Select("SIRA=0").CopyToDataTable().Rows
            {

                LX = 0;
                lblEn = 1130 / (dt2.Rows.Count + 2);
                lblBoy = 30;
                Color yaziRengi = System.Drawing.Color.MidnightBlue;
                Color divRengi = Color.White;
                Color borderRengi = System.Drawing.Color.SkyBlue;
                if (row["SIRA"].ToString() == "1")
                {
                    yaziRengi = Color.White;
                    borderRengi = System.Drawing.Color.SkyBlue;
                    divRengi = System.Drawing.Color.SteelBlue;
                    LY += 15;
                }

                XRLabel lblKampusYaz = new XRLabel()
                {
                    WidthF = lblEn,
                    HeightF = lblBoy,
                    Text = row["SUBEAD"].ToString(),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = divRengi,
                    ForeColor = yaziRengi,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = borderRengi,
                    Tag = "Ogrt"
                };
                LX += lblKampusYaz.WidthF;
                Detail.Controls.Add(lblKampusYaz);


                XRLabel lblOSYaz = new XRLabel()
                {
                    WidthF = lblEn,
                    HeightF = lblBoy,
                    Text = row["OGRSAY"].ToString(),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = divRengi,
                    ForeColor = yaziRengi,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = borderRengi,
                    Tag = "Ogrt"
                };
                LX += lblOSYaz.WidthF;
                Detail.Controls.Add(lblOSYaz);



                foreach (DataRow item in dt2.Rows)
                {
                    if (row["SIRA"].ToString() == "0")
                    {
                        yaziRengi = System.Drawing.Color.MidnightBlue;
                    }
                    else
                    {
                        yaziRengi = System.Drawing.Color.White;
                    }

                    XRLabel lblSinavYaz = new XRLabel()
                    {
                        WidthF = lblEn / 2,
                        HeightF = lblBoy,
                        Text = katilim ? row[item["ID_SINAV"].ToString()].ToString() : (Convert.ToInt32(row["OGRSAY"]) - Convert.ToInt32(row[item["ID_SINAV"].ToString()])).ToString(),
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = divRengi,
                        ForeColor = yaziRengi,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = borderRengi,
                        Tag = "Ogrt"
                    };
                    LX += lblSinavYaz.WidthF;
                    Detail.Controls.Add(lblSinavYaz);



                    double yuzde = Convert.ToDouble(row["YUZDEKATILMAYAN" + item["ID_SINAV"].ToString()].ToString());
                    if (katilim)
                    {
                        yuzde = Convert.ToDouble(row["YUZDEKATILAN" + item["ID_SINAV"].ToString()].ToString());
                    }

                    if ((yuzde >= 50 && !katilim && row["SIRA"].ToString() == "0") || (yuzde <= 50 && katilim && row["SIRA"].ToString() == "0"))
                    {
                        yaziRengi = Color.Red;
                    }

                    XRLabel lblSinavYuzdeYaz = new XRLabel()
                    {
                        WidthF = lblEn / 2,
                        HeightF = lblBoy,
                        Text = "% " + Math.Round(yuzde, 2).ToString(),
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = divRengi,
                        ForeColor = yaziRengi,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = borderRengi,
                        Tag = "Ogrt"
                    };
                    LX += lblSinavYuzdeYaz.WidthF;
                    Detail.Controls.Add(lblSinavYuzdeYaz);
                }
                LY += lblBoy;
            }
        }
    }
}
