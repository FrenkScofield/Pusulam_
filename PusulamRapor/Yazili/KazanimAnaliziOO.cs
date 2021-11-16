using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Yazili
{
    public partial class KazanimAnaliziOO : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        List<string> AltKonuList = new List<string>();
        Dictionary<string, int> AltKonuPuan = new Dictionary<string, int>();
        string SinavAd = string.Empty;
        public KazanimAnaliziOO(string tckimlikno, string oturum, string idSinav, string idSinif)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", tckimlikno);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_YAZILI", idSinav);
                b.ParametreEkle("@ID_SINIF", idSinif);
                b.ParametreEkle("@ID_MENU", 1116);
                b.ParametreEkle("@ISLEM", 1);
                ds = b.SorguGetir("sp_KazanimAnaliziOO");
                dt = ds.Tables[0];
                dt2 = ds.Tables[1];
                dt3 = ds.Tables[2];
            }

            lbl_snvbilgi.Text = dt3.Rows[0]["SINAVAD"].ToString();
            lbl_kampus.Text = dt3.Rows[0]["SUBE"].ToString();
            lbl_tarih.Text = DateTime.Now.ToShortDateString();
            lbl_sinif.Text = dt3.Rows[0]["SINIF"].ToString() + "   Dönem : " + dt3.Rows[0]["DONEM"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!AltKonuList.Contains(dt.Rows[i]["KOD"].ToString()))
                {
                    AltKonuList.Add(dt.Rows[i]["KOD"].ToString());
                }
            }
        }

        FontFamily fam = new FontFamily("Calibri");
        private void KazanimAnaliziOO_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float x = 0;
            float y = 0;
            Font font = new Font(fam, 8);

            XRLabel xr_SinavAd = new XRLabel()
            {
                Text = SinavAd,
                WidthF = 300,
                HeightF = 160,
                LocationF = new PointF(x, y),
                CanGrow = false,
                BorderColor = Color.DarkGray,
                BackColor = Color.WhiteSmoke,
            };
            Detail.Controls.Add(xr_SinavAd);
            y += 160;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                XRLabel xr_Konu = new XRLabel()
                {
                    Text = dt2.Rows[i]["KAZANIM"].ToString(),
                    WidthF = 300,
                    HeightF = 40,
                    LocationF = new PointF(x, y),
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    Tag = "1",
                    CanGrow = false,
                    BorderColor = Color.DarkGray,
                    BackColor = Color.WhiteSmoke,
                };
                Detail.Controls.Add(xr_Konu);
                y += 40;
            }

            y = 00;
            x += 260;
            int DonmeSayisi = 0;
            string adsoyad = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (adsoyad == "" || adsoyad != dt.Rows[i]["ADSOYAD"].ToString()) // Eğer ilk defa giriyorsa öğrenci adlarınıda ekleyeceksin
                {
                    adsoyad = dt.Rows[i]["ADSOYAD"].ToString();
                    x += 40;
                    y = 0;
                    XRLabel xr_OgrAd = new XRLabel()
                    {
                        Text = dt.Rows[i]["ADSOYAD"].ToString(),
                        WidthF = 40,
                        HeightF = 160,
                        LocationF = new PointF(x, y),
                        CanGrow = false,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderColor = Color.Gray,
                        BackColor = Color.Gainsboro,
                        Angle = 90F,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter
                    };
                    Detail.Controls.Add(xr_OgrAd);
                    y += 160;

                    XRLabel xr_Yuzde = new XRLabel()
                    {
                        Text = dt.Rows[i]["YUZDE"].ToString(),
                        WidthF = 40,
                        HeightF = 40,
                        LocationF = new PointF(x, y),
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BorderColor = Color.DarkGray,
                        BackColor = Color.WhiteSmoke,
                        CanGrow = false
                    };
                    Detail.Controls.Add(xr_Yuzde);
                    y += 40;
                }
                else //Öğrenci adları eklenmiş demektir burada eklenmez
                {
                    XRLabel xr_Yuzde = new XRLabel()
                    {
                        Text = dt.Rows[i]["YUZDE"].ToString(),
                        WidthF = 40,
                        HeightF = 40,
                        LocationF = new PointF(x, y),
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        BorderColor = Color.DarkGray,
                        BackColor = Color.WhiteSmoke,
                        CanGrow = false
                    };
                    Detail.Controls.Add(xr_Yuzde);
                    y += 40;
                }
            }



            #region GenelToplam

            y = 0;
            x += 40;
            int Donence = 0;
            foreach (string item in AltKonuList)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    if (item == dt2.Rows[i]["KOD"].ToString())
                    {
                        if (Donence == 0)
                        {
                            XRLabel xr_GenelToplam = new XRLabel()
                            {
                                Text = "GENEL TOPLAM",
                                WidthF = 40,
                                HeightF = 160,
                                LocationF = new PointF(x, y),
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter,
                                BorderColor = Color.DarkGray,
                                BackColor = Color.WhiteSmoke,
                                CanGrow = false,
                                Angle = 90F,

                            };
                            Detail.Controls.Add(xr_GenelToplam);
                            y += 160;

                            XRLabel xr_Yuzde = new XRLabel()
                            {
                                Text = dt2.Rows[i]["YUZDE"].ToString(),
                                WidthF = 40,
                                HeightF = 40,
                                LocationF = new PointF(x, y),
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                BorderColor = Color.DarkGray,
                                BackColor = Color.WhiteSmoke,
                                CanGrow = false
                            };
                            Detail.Controls.Add(xr_Yuzde);
                            y -= 120;
                        }
                        else
                        {
                            XRLabel xr_Yuzde = new XRLabel()
                            {
                                Text = dt2.Rows[i]["YUZDE"].ToString(),
                                WidthF = 40,
                                HeightF = 40,
                                LocationF = new PointF(x, y),
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                BorderColor = Color.DarkGray,
                                BackColor = Color.WhiteSmoke,
                                CanGrow = false
                            };
                            Detail.Controls.Add(xr_Yuzde);
                        }

                    }
                }
                if (Donence == 0) y += 160; else y += 40;
                Donence++;
            }
            #endregion
            int yx = Convert.ToInt32(x + 40);

            this.PageWidth = yx;

        }
    }
}
