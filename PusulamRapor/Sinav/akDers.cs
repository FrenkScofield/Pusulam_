using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class akDers : DevExpress.XtraReports.UI.XtraReport
    {

        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        List<int> idSinavlar = new List<int>();
        List<string> adSinavlar = new List<string>();
        float artim = 0;
        float colcount = 0;
        bool olcek = false;
        int FontSize = 10;
        bool aayksydil = false;

        public akDers(DataTable _dt3, DataTable _dt4)
        {
            InitializeComponent();
            dt3 = _dt3;
            dt4 = _dt4;

            try
            {
                //FontSize = dt4.Rows[0]["ID_SINAVTURU"].ToString().Equals("6") || dt4.Rows[0]["ID_SINAVTURU"].ToString().Equals("7") ? 8 : 6;
                aayksydil = dt4.Rows[0]["ID_SINAVTURU"].ToString().Equals("3") || dt4.Rows[0]["ID_SINAVTURU"].ToString().Equals("5");
            }
            catch (Exception)
            {

            }

            olcek = dt4.Select("OLCEKSINAVI=1").Length > 0;
            if (olcek)
            {
                colcount = 5;
            }
            else
            {
                colcount = 4;
            }

            float LY = 24;
            float LX = 0;
            artim = (1692 - 60) / (dt3.Rows.Count + 1);
            FontFamily ff = new FontFamily("Tahoma");

            XRLabel xrSinav = new XRLabel()
            {
                WidthF = 60,
                HeightF = 60,
                Text = "SINAV",
                Font = new System.Drawing.Font(ff, FontSize-2, FontStyle.Bold),
                BackColor = System.Drawing.Color.SkyBlue,
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

            foreach (DataRow item in dt3.Rows)
            {//2970 w
                XRLabel xrAd = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = 30,
                    Text = item["DERSAD"].ToString(),
                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                    BackColor = System.Drawing.Color.SkyBlue,
                    ForeColor = System.Drawing.Color.MidnightBlue,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = System.Drawing.Color.White,
                    Tag = "Ogrt"
                };
                GroupHeader1.Controls.Add(xrAd);

                for (int i = 0; i < colcount; i++)
                {
                    string yaz = "D";
                    if (i == 1)
                    {
                        yaz = "Y";
                    }
                    else if (i == 2)
                    {
                        yaz = "B";
                    }
                    else if (i == 3)
                    {
                        yaz = "N";
                    }
                    else if (i == 4)
                    {
                        yaz = "P";
                    }
                    XRLabel xrAdD = new XRLabel()
                    {
                        WidthF = artim / colcount,
                        HeightF = 30,
                        Text = yaz,
                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                        BackColor = System.Drawing.Color.SkyBlue,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX + (i * (artim / colcount)), LY + 30),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.White,
                        Tag = "Ogrt",
                    };
                    GroupHeader1.Controls.Add(xrAdD);
                    //if (olcek && i == 4)
                    //{
                    //    GroupHeader1.Controls.Add(xrAdD);
                    //}else
                    //{
                    //    GroupHeader1.Controls.Add(xrAdD);
                    //}
                }
                LX += artim;
            }


            XRLabel xrToplam = new XRLabel()
            {
                WidthF = artim,
                HeightF = 30,
                Text = "TOPLAM",
                Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                BackColor = System.Drawing.Color.SkyBlue,
                ForeColor = System.Drawing.Color.MidnightBlue,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = System.Drawing.Color.White,
                Tag = "Ogrt"
            };
            GroupHeader1.Controls.Add(xrToplam);

            for (int i = 0; i < 4; i++)
            {
                string yaz = "TD";
                if (i == 1)
                {
                    yaz = "TY";
                }
                else if (i == 2)
                {
                    yaz = "TB";
                }
                else if (i == 3)
                {
                    yaz = "TN";
                }
                XRLabel xrAdD = new XRLabel()
                {
                    WidthF = artim / 4,
                    HeightF = 30,
                    Text = yaz,
                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                    BackColor = System.Drawing.Color.SkyBlue,
                    ForeColor = System.Drawing.Color.MidnightBlue,
                    LocationF = new PointF(LX + (i * (artim / 4)), LY + 30),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = System.Drawing.Color.White,
                    Tag = "Ogrt",
                    Name = "T-" + i.ToString()
                };
                GroupHeader1.Controls.Add(xrAdD);
            }

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            try
            {
                float LY = 0;
                float LX = 0;

                foreach (DataRow item2 in dt4.Rows)
                {
                    string idSinav = item2["ID_SINAV"].ToString();
                    string adSinav = item2["SINAVAD"].ToString();

                    if (idSinavlar.IndexOf(Convert.ToInt32(idSinav)) == -1)
                    {
                        idSinavlar.Add(Convert.ToInt32(idSinav));
                        adSinavlar.Add(adSinav);
                    }
                }
                int index = -1;
                foreach (string sinav in adSinavlar)
                {
                    index++;
                    LX = 0;

                    FontFamily ff = new FontFamily("Tahoma");

                    XRLabel xrSinavD = new XRLabel()
                    {
                        WidthF = 60,
                        HeightF = 30,
                        Text = sinav,
                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Bold),
                        BackColor = System.Drawing.Color.Transparent,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.SkyBlue,
                        Tag = "Ogrt",
                        Name = idSinavlar[index].ToString()
                    };
                    LX += xrSinavD.WidthF;
                    Detail.Controls.Add(xrSinavD);

                    foreach (DataRow item in dt3.Rows)
                    {//2970 w
                        bool girdi = false;
                        foreach (DataRow veri in dt4.Rows)
                        {
                            if (veri["ID_SINAV"].ToString() == idSinavlar[index].ToString() && veri["DERSAD"].ToString() == item["DERSAD"].ToString())
                            {
                                girdi = true;

                                for (int i = 0; i < colcount; i++)
                                {
                                    string yaz = "";

                                    if (i == 0)
                                        yaz = veri["DOGRU"].ToString();
                                    if (i == 1)
                                        yaz = veri["YANLIS"].ToString();
                                    if (i == 2)
                                        yaz = veri["BOS"].ToString();
                                    if (i == 3)
                                        yaz = veri["NET"].ToString();
                                    if (i == 4 && Convert.ToBoolean(veri["OLCEKSINAVI"]) == true)
                                        yaz = veri["DERSPUAN"].ToString();

                                    XRLabel xrAdD = new XRLabel()
                                    {
                                        WidthF = artim / colcount,
                                        HeightF = 30,
                                        Text = yaz,
                                        Font = new System.Drawing.Font(ff, FontSize, FontStyle.Regular),
                                        BackColor = System.Drawing.Color.Transparent,
                                        ForeColor = System.Drawing.Color.MidnightBlue,
                                        LocationF = new PointF(LX + (i * (artim / colcount)), LY),
                                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                                        BorderWidth = 1,
                                        BorderColor = System.Drawing.Color.SkyBlue,
                                        Tag = "Ogrt",
                                        Name = item["DERSAD"].ToString() + i.ToString()
                                    };
                                    Detail.Controls.Add(xrAdD);
                                    //if (olcek && i == 4)
                                    //{
                                    //    Detail.Controls.Add(xrAdD);
                                    //}
                                    //else
                                    //{
                                    //    Detail.Controls.Add(xrAdD);
                                    //}
                                }
                                LX += artim;

                            }
                        }
                        if (girdi == false)
                        {
                            for (int i = 0; i < colcount; i++)
                            {
                                XRLabel xrAdD = new XRLabel()
                                {
                                    WidthF = artim / colcount,
                                    HeightF = 30,
                                    Text = "-",
                                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Regular),
                                    BackColor = System.Drawing.Color.Transparent,
                                    ForeColor = System.Drawing.Color.MidnightBlue,
                                    LocationF = new PointF(LX + (i * (artim / colcount)), LY),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    BorderColor = System.Drawing.Color.SkyBlue,
                                    Tag = "Ogrt",
                                    Name = item["DERSAD"].ToString() + i.ToString()
                                };
                                Detail.Controls.Add(xrAdD);
                                //if (olcek && i == 4)
                                //{
                                //    Detail.Controls.Add(xrAdD);
                                //}
                                //else
                                //{
                                //    Detail.Controls.Add(xrAdD);
                                //}
                            }
                            LX += artim;
                        }

                        //LX += xrAd.WidthF;
                        //LX+=artim;
                    }

                    foreach (DataRow item in dt4.Rows)
                    {
                        if (item["ID_SINAV"].ToString() == idSinavlar[index].ToString())
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                string yaz = "";
                                if (i == 0)
                                    yaz = item["TD"].ToString();
                                if (i == 1)
                                    yaz = item["TY"].ToString();
                                if (i == 2)
                                    yaz = item["TB"].ToString();
                                if (i == 3)
                                    yaz = item["TN"].ToString();
                                XRLabel xrAdD = new XRLabel()
                                {
                                    WidthF = artim / 4,
                                    HeightF = 30,
                                    Text = ((i == 2 && aayksydil) ? "-" : yaz),
                                    Font = new System.Drawing.Font(ff, FontSize, FontStyle.Regular),
                                    BackColor = System.Drawing.Color.Transparent,
                                    ForeColor = System.Drawing.Color.MidnightBlue,
                                    LocationF = new PointF(LX + (i * (artim / 4)), LY),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    BorderColor = System.Drawing.Color.SkyBlue,
                                    Tag = "Ogrt",
                                    Name = "T-" + i.ToString()
                                };
                                Detail.Controls.Add(xrAdD);
                            }
                            break;
                        }
                    }

                    LY += 30;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblSinavTuru.Text = dt3.Rows[0]["SINAVTURU"].ToString();
        }
    }
}
