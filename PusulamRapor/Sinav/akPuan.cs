using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class akPuan : DevExpress.XtraReports.UI.XtraReport
    {

        DataTable dt6 = new DataTable(); // PUAN TÜRLERİ
        DataTable dt5 = new DataTable(); // PUANLAR
        List<string> adSinavlar = new List<string>();
        List<int> idSinavlar = new List<int>();
        float artim = 0;

        int FontSize = 8;

        public akPuan(DataTable _dt6, DataTable _dt5)
        {
            InitializeComponent();
            dt6 = _dt6;
            dt5 = _dt5;

            //FontSize = dt6.Rows[0]["ID_SINAVTURU"].ToString().Equals("6") || dt6.Rows[0]["ID_SINAVTURU"].ToString().Equals("7") ? 8 : 6;

            float LY = 0;
            float LX = 0;
            artim = (1692 - 100) / (dt6.Rows.Count);
            //string idSinav = GetCurrentColumnValue("ID_SINAV").ToString();
            FontFamily ff = new FontFamily("Tahoma");



            XRLabel xrSinav = new XRLabel()
            {
                WidthF = 100,
                HeightF = 60,
                Text = "SINAV",
                Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                BackColor = System.Drawing.Color.LightSalmon,
                ForeColor = System.Drawing.Color.MidnightBlue,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = System.Drawing.Color.White,
                Tag = "Ogrt"
            };
            LX += xrSinav.WidthF;
            GroupHeader1.Controls.Add(xrSinav);

            foreach (DataRow item in dt6.Rows)
            {//2970 w
                XRLabel xrAd = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = 30,
                    Text = item["PUANTURU"].ToString(),
                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                    BackColor = System.Drawing.Color.LightSalmon,
                    ForeColor = System.Drawing.Color.MidnightBlue,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = System.Drawing.Color.White,
                    Tag = "Ogrt"
                };
                GroupHeader1.Controls.Add(xrAd);

                for (int i = 0; i < 6; i++)
                {
                    string yaz = "PUAN";
                    if (i == 1)
                    {
                        yaz = "SINIF";
                    }
                    else if (i == 2)
                    {
                        yaz = "OKUL";
                    }
                    else if (i == 3)
                    {
                        yaz = "ILCE";
                    }
                    else if (i == 4)
                    {
                        yaz = "IL";
                    }
                    else if (i == 5)
                    {
                        yaz = "GENEL";
                    }
                    XRLabel xrAdD = new XRLabel()
                    {
                        WidthF = artim / 6,
                        HeightF = 30,
                        Text = yaz,
                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                        BackColor = System.Drawing.Color.LightSalmon,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX + (i * (artim / 6)), LY + 30),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.White,
                        Tag = "Ogrt",
                    };
                    GroupHeader1.Controls.Add(xrAdD);
                }

                LX += artim;
            }
        }

        private void akPuan_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                float LY = 0;
                float LX = 0;

                foreach (DataRow item in dt5.Rows)
                {
                    string idSinav = item["ID_SINAV"].ToString();
                    string adSinav = item["SINAVAD"].ToString();

                    if (idSinavlar.IndexOf(Convert.ToInt32(idSinav)) == -1)
                    {
                        idSinavlar.Add(Convert.ToInt32(idSinav));
                        adSinavlar.Add(adSinav);
                    }
                }
                int index = -1;
                foreach (int idSinav in idSinavlar)
                {
                    index++;
                    LX = 0;

                    FontFamily ff = new FontFamily("Tahoma");

                    XRLabel xrSinavD = new XRLabel()
                    {
                        WidthF = 100,
                        HeightF = 30,
                        Text = adSinavlar[index],
                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                        BackColor = System.Drawing.Color.Transparent,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.LightSalmon,
                        Tag = "Ogrt",
                        KeepTogether = true,
                        Name = idSinav.ToString()
                    };
                    LX += xrSinavD.WidthF;
                    Detail.Controls.Add(xrSinavD);

                    foreach (DataRow item in dt6.Rows)
                    {//2970 w

                        bool girdi = false;
                        foreach (DataRow veri in dt5.Rows)
                        {
                            if (veri["ID_SINAV"].ToString() == idSinav.ToString() && veri["ID_SINAVPUANTURU"].ToString() == item["ID_SINAVPUANTURU"].ToString())
                            {

                                for (int i = 0; i < 6; i++)
                                {
                                    Color yaziRengi = System.Drawing.Color.MidnightBlue;
                                    string yaz = "";
                                    if (i == 0)
                                    {
                                        yaz = veri["PUAN"].ToString();
                                        yaziRengi = System.Drawing.Color.DarkBlue;
                                    }
                                    if (i == 1)
                                        yaz = veri["SINIFSIRA"].ToString();// +" / "+veri["SINIFKATILIM"].ToString();
                                    if (i == 2)
                                        yaz = veri["OKULSIRA"].ToString();// +" / "+veri["SUBEKATILIM"].ToString();
                                    if (i == 3)
                                        yaz = veri["ILCESIRA"].ToString();// +" / "+veri["ILCEKATILIM"].ToString();
                                    if (i == 4)
                                        yaz = veri["ILSIRA"].ToString();// +" / "+veri["ILKATILIM"].ToString();
                                    if (i == 5)
                                        yaz = veri["GENELSIRA"].ToString();// +" / "+veri["GENELKATILIM"].ToString();

                                    XRLabel xrAdD = new XRLabel()
                                    {
                                        WidthF = artim / 6,
                                        HeightF = 30,
                                        Text = yaz,
                                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                                        BackColor = (i != 0 ? System.Drawing.Color.Transparent : Color.LightSalmon),
                                        ForeColor = yaziRengi,
                                        LocationF = new PointF(LX + (i * (artim / 6)), LY),
                                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                                        BorderWidth = 1,
                                        BorderColor = (i != 0 ? System.Drawing.Color.LightSalmon : Color.White),
                                        Tag = "Ogrt",
                                        Name = item["ID_SINAVPUANTURU"].ToString() + i.ToString()
                                    };
                                    Detail.Controls.Add(xrAdD);
                                }
                            }
                        }
                        if (girdi == false)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Color yaziRengi = System.Drawing.Color.MidnightBlue;
                                string yaz = "";

                                XRLabel xrAdD = new XRLabel()
                                {
                                    WidthF = artim / 6,
                                    HeightF = 30,
                                    Text = yaz,
                                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                                    BackColor = System.Drawing.Color.Transparent,
                                    ForeColor = yaziRengi,
                                    LocationF = new PointF(LX + (i * (artim / 6)), LY),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    BorderColor = System.Drawing.Color.LightSalmon,
                                    Tag = "Ogrt",
                                    Name = item["ID_SINAVPUANTURU"].ToString() + i.ToString()
                                };
                                Detail.Controls.Add(xrAdD);
                            }
                        }

                        LX += artim;
                    }
                    LY += 30;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
