using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporOOYazili : DevExpress.XtraReports.UI.XtraReport
    {
        public GelisimRaporOOYazili(DataTable dt1, DataTable dt2, int donem, Color titleblue, Color blue)
        {
            InitializeComponent();
            xrLabel1.BackColor = titleblue;
            xrLabel2.BackColor = titleblue;
            xrLabel3.BackColor = titleblue;
            xrLabel4.BackColor = titleblue;
            xrLabel5.BackColor = titleblue;
            xrLabel6.BackColor = titleblue;
            //xrLabel7.BackColor = titleblue;
            xrLabel8.BackColor = titleblue;
            xrLabel9.BackColor = titleblue;
            //xrLabel10.BackColor = titleblue;

            if (dt2.Rows.Count > 0)
            {

                float LY = 0;
                float LX = 0;

                Font font12r = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
                Font font12b = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
                FontFamily ff = new FontFamily("Tahoma");



                if (donem == 0)
                {
                    foreach (DataRow rowDersler in dt1.Rows)
                    {
                        LX = 0;
                        XRLabel xrAd = new XRLabel()
                        {
                            WidthF = 310.31f,
                            HeightF = 36f,
                            Text = rowDersler["DERSAD"].ToString(),
                            Font = font12b,
                            ForeColor = System.Drawing.Color.White,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.White,
                            BackColor = blue
                        };
                        LX += xrAd.WidthF;
                        Detail.Controls.Add(xrAd);

                        XRLabel xrNot101 = new XRLabel()
                        {
                            WidthF = 345,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot101.WidthF;
                        Detail.Controls.Add(xrNot101);
                        XRLabel xrNot102 = new XRLabel()
                        {
                            WidthF = 345,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot102.WidthF;
                        Detail.Controls.Add(xrNot102);
                        //XRLabel xrNot230 = new XRLabel()
                        //{
                        //    WidthF = 230,
                        //    HeightF = 36,
                        //    Text = "",
                        //    Font = font12r,
                        //    ForeColor = System.Drawing.Color.MidnightBlue,
                        //    LocationF = new PointF(LX, LY),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderColor = Color.DeepSkyBlue,
                        //    BackColor = Color.White
                        //};
                        //LX += xrNot230.WidthF;
                        //Detail.Controls.Add(xrNot230);
                        XRLabel xrNot201 = new XRLabel()
                        {
                            WidthF = 345,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot201.WidthF;
                        Detail.Controls.Add(xrNot201);
                        XRLabel xrNot202 = new XRLabel()
                        {
                            WidthF = 345,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot202.WidthF;
                        Detail.Controls.Add(xrNot202);
                        //XRLabel xrNot203 = new XRLabel()
                        //{
                        //    WidthF = 230,
                        //    HeightF = 36,
                        //    Text = "",
                        //    Font = font12r,
                        //    ForeColor = System.Drawing.Color.MidnightBlue,
                        //    LocationF = new PointF(LX, LY),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderColor = Color.DeepSkyBlue,
                        //    BackColor = Color.White
                        //};
                        //LX += xrNot203.WidthF;
                        //Detail.Controls.Add(xrNot203);
                        xrNot101.Text = "G";
                        if (dt2.Rows[0]["DERSAD"].ToString().Length > 2)
                        {
                            try
                            {
                                foreach (DataRow row2 in dt2.Select(String.Format("DERSAD='{0}'", rowDersler["DERSAD"])).CopyToDataTable().Rows)
                                {
                                    if (row2["SINAVADI"].ToString().Contains("101"))
                                    {
                                        xrNot101.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    else if (row2["SINAVADI"].ToString().Contains("102"))
                                    {
                                        xrNot102.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    //else if (row2["SINAVADI"].ToString().Contains("230")) // 103 olması lazım idi
                                    //{
                                    //    xrNot230.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    //}
                                    else if (row2["SINAVADI"].ToString().Contains("201"))
                                    {
                                        xrNot201.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    else if (row2["SINAVADI"].ToString().Contains("202"))
                                    {
                                        xrNot202.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    //else if (row2["SINAVADI"].ToString().Contains("203"))
                                    //{
                                    //    xrNot203.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    //}
                                }
                            }
                            catch (Exception)
                            {

                            }

                            LY += 36;
                        }


                    }
                }



                //Sadece 1. dönem seçilince (Ali Bolu)
                else if (donem == 1)
                {
                    xrLabel3.WidthF = 1380f;
                    xrLabel5.WidthF = 1380f / 2f;
                    xrLabel6.WidthF = 1380f / 2f;
                    //xrLabel7.WidthF = 1380f / 3f;

                    xrLabel6.LocationF = new PointF(xrLabel6.LocationF.X + 345f, xrLabel6.LocationF.Y);
                    //xrLabel7.LocationF = new PointF(xrLabel7.LocationF.X + 230f * 2, xrLabel7.LocationF.Y);

                    xrLabel8.Visible = false;
                    //xrLabel10.Visible = false;
                    xrLabel9.Visible = false;
                    xrLabel4.Visible = false;
                    foreach (DataRow rowDersler in dt1.Rows)
                    {
                        LX = 0;
                        XRLabel xrAd = new XRLabel()
                        {
                            WidthF = 310.31f,
                            HeightF = 36,
                            Text = rowDersler["DERSAD"].ToString(),
                            Font = font12b,
                            ForeColor = System.Drawing.Color.White,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.White,
                            BackColor = blue
                        };
                        LX += xrAd.WidthF;
                        Detail.Controls.Add(xrAd);

                        XRLabel xrNot101 = new XRLabel()
                        {
                            WidthF = 690f,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot101.WidthF;
                        Detail.Controls.Add(xrNot101);
                        XRLabel xrNot102 = new XRLabel()
                        {
                            WidthF = 690f,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot102.WidthF;
                        Detail.Controls.Add(xrNot102);
                        //XRLabel xrNot230 = new XRLabel()
                        //{
                        //    WidthF = 460f,
                        //    HeightF = 36,
                        //    Text = "",
                        //    Font = font12r,
                        //    ForeColor = System.Drawing.Color.MidnightBlue,
                        //    LocationF = new PointF(LX, LY),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderColor = Color.DeepSkyBlue,
                        //    BackColor = Color.White
                        //};
                        //LX += xrNot230.WidthF;
                        //Detail.Controls.Add(xrNot230);

                        xrNot101.Text = "G";
                        if (dt2.Rows[0]["DERSAD"].ToString().Length > 2)
                        {
                            try
                            {
                                foreach (DataRow row2 in dt2.Select(String.Format("DERSAD='{0}'", rowDersler["DERSAD"])).CopyToDataTable().Rows)
                                {
                                    if (row2["SINAVADI"].ToString().Contains("101"))
                                    {
                                        xrNot101.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    else if (row2["SINAVADI"].ToString().Contains("102"))
                                    {
                                        xrNot102.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    //else if (row2["SINAVADI"].ToString().Contains("230"))
                                    //{
                                    //    xrNot230.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    //}
                                }
                            }
                            catch (Exception)
                            {

                            }

                            LY += 36;
                        }


                    }
                }




                //2. dönem seçilince

                else if (donem == 2)
                {
                    xrLabel3.Text = "2. DÖNEM YAZILI YOKLAMA SONUÇLARI";
                    xrLabel3.WidthF = 1380f;
                    xrLabel5.WidthF = 1380f / 2f;
                    xrLabel6.WidthF = 1380f / 2f;
                    //xrLabel7.WidthF = 1380f / 3f;

                    xrLabel6.LocationF = new PointF(xrLabel6.LocationF.X + 345f, xrLabel6.LocationF.Y);
                    //xrLabel7.LocationF = new PointF(xrLabel7.LocationF.X + 230f * 2, xrLabel7.LocationF.Y);

                    xrLabel8.Visible = false;
                    //xrLabel10.Visible = false;
                    xrLabel9.Visible = false;
                    xrLabel4.Visible = false;

                    foreach (DataRow rowDersler in dt1.Rows)
                    {
                        LX = 0;
                        XRLabel xrAd = new XRLabel()
                        {
                            WidthF = 310.31f,
                            HeightF = 36,
                            Text = rowDersler["DERSAD"].ToString(),
                            Font = font12b,
                            ForeColor = System.Drawing.Color.White,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.White,
                            BackColor = blue
                        };
                        LX += xrAd.WidthF;
                        Detail.Controls.Add(xrAd);

                        XRLabel xrNot101 = new XRLabel()
                        {
                            WidthF = 690f,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot101.WidthF;
                        Detail.Controls.Add(xrNot101);
                        XRLabel xrNot102 = new XRLabel()
                        {
                            WidthF = 690f,
                            HeightF = 36,
                            Text = "",
                            Font = font12r,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = Color.DeepSkyBlue,
                            BackColor = Color.White
                        };
                        LX += xrNot102.WidthF;
                        Detail.Controls.Add(xrNot102);
                        //XRLabel xrNot230 = new XRLabel()
                        //{
                        //    WidthF = 460f,
                        //    HeightF = 36,
                        //    Text = "",
                        //    Font = font12r,
                        //    ForeColor = System.Drawing.Color.MidnightBlue,
                        //    LocationF = new PointF(LX, LY),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderColor = Color.DeepSkyBlue,
                        //    BackColor = Color.White
                        //};
                        //LX += xrNot230.WidthF;
                        //Detail.Controls.Add(xrNot230);

                        //xrNot101.Text = "G";
                        if (dt2.Rows[0]["DERSAD"].ToString().Length > 2)
                        {
                            try
                            {
                                foreach (DataRow row2 in dt2.Select(String.Format("DERSAD='{0}'", rowDersler["DERSAD"])).CopyToDataTable().Rows)
                                {
                                    if (row2["SINAVADI"].ToString().Contains("201"))
                                    {
                                        xrNot101.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    else if (row2["SINAVADI"].ToString().Contains("202"))
                                    {
                                        xrNot102.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    }
                                    //else if (row2["SINAVADI"].ToString().Contains("203"))
                                    //{
                                    //    xrNot230.Text = row2["PUAN"].ToString() == "0" ? "G" : row2["PUAN"].ToString();
                                    //}
                                }
                            }
                            catch (Exception)
                            {

                            }

                            LY += 36;
                        }


                    }
                }



            }
        }

    }
}
