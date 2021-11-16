using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Linq;

namespace PusulamRapor.Sinav
{
    public partial class ssCevapAnahtari : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        DataTable tableBolumler = new DataTable();
        readonly FontFamily familyArial = new FontFamily("Arial");
        int max = 0;
        public ssCevapAnahtari(DataTable _dt)
        {
            dt = PublicMetods.orderBYtoTable( _dt,"BOLUMNO");

            max = Convert.ToInt32(dt.AsEnumerable().Max(row => row["SORUNO"]));
            tableBolumler = dt.DefaultView.ToTable(true, "DERSAD");

            InitializeComponent();
        }

        private void ssCevapAnahtari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //this.DataSource = dt;

            int ymarg = 2;
            int xmarg = 1;
            int hkwt = 15;
            float LY_OgrenciCevap = 2;
            float LY_CevapAnahtar = 17;
            int counter = 42;

            Font fontIcerik = new Font(familyArial, 6.5f);
            Font fontBold = new Font(familyArial, 6.5f, FontStyle.Bold);

            for (int i = 0; i < tableBolumler.Rows.Count; i++)
            {
                float LX_Anahtar = 2;
                float LX_OgrenciAnahtar = 0;

                XRLabel xrOgrenciAnahtarDers = new XRLabel()
                {
                    WidthF = 132,
                    HeightF = 2 * hkwt,
                    Text = tableBolumler.Rows[i]["DERSAD"].ToString().ToUpper(),
                    LocationF = new PointF(LX_OgrenciAnahtar, LY_OgrenciCevap),
                    Font=fontIcerik,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.None,
                    BackColor = Color.FromArgb(217, 226, 243),
                    ForeColor = System.Drawing.Color.Black,
                    BorderColor = Color.FromArgb(180, 198, 231)
                };
                LX_OgrenciAnahtar += xrOgrenciAnahtarDers.WidthF + xmarg + 3;
                Detail.Controls.Add(xrOgrenciAnahtarDers);
                counter = 42;
                for (int y = 0; y < dt.Rows.Count; y++)
                {
                    DataRow item = dt.Rows[y];
                    if (item["DERSAD"].ToString() == tableBolumler.Rows[i]["DERSAD"].ToString())
                    {
                        XRLabel xrOgrCvp = new XRLabel()
                        {
                            WidthF = 15,
                            HeightF = hkwt,
                            Text = item["OGRENCICEVAP"].ToString(),
                            LocationF = new PointF(LX_OgrenciAnahtar, LY_OgrenciCevap),
                            Font = (item["DURUM"].ToString() != "D") ? fontBold : fontIcerik,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BackColor = (item["DURUM"].ToString() == "Y") ? Color.Red : Color.FromArgb(217, 226, 243),
                            BorderColor = Color.FromArgb(180, 198, 231)
                        };

                        //if ((item["DURUM"].ToString() != "D")) xrOgrCvp.BackColor = Color.Red;

                        LX_OgrenciAnahtar += xrOgrCvp.WidthF + xmarg;
                        Detail.Controls.Add(xrOgrCvp);


                        XRLabel xrCvpAnah = new XRLabel()
                        {
                            WidthF = 15,
                            HeightF = hkwt,
                            Text = item["DOGRUCEVAP"].ToString(),
                            LocationF = new PointF(134 + LX_Anahtar, LY_CevapAnahtar),
                            Font = fontIcerik,
                            ForeColor = Color.Red,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BackColor = Color.White,
                            BorderColor = Color.FromArgb(180, 198, 231)
                        };
                        LX_Anahtar += xrCvpAnah.WidthF + xmarg;
                        Detail.Controls.Add(xrCvpAnah);
                        counter--;

                        try
                        {
                            DataRow item2 = dt.Rows[y + 1];
                            if (item2["DERSAD"].ToString() != tableBolumler.Rows[i]["DERSAD"].ToString())
                            {
                                for (int x = 0; x < counter; x++)
                                {
                                    xrOgrCvp = new XRLabel()
                                    {
                                        WidthF = 15,
                                        HeightF = hkwt,
                                        Text = "",
                                        LocationF = new PointF(LX_OgrenciAnahtar, LY_OgrenciCevap),
                                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                                        BackColor = Color.FromArgb(217, 226, 243),
                                        BorderColor = Color.FromArgb(180, 198, 231)
                                    };

                                    LX_OgrenciAnahtar += xrOgrCvp.WidthF + xmarg;
                                    Detail.Controls.Add(xrOgrCvp);

                                    xrCvpAnah = new XRLabel()
                                    {
                                        WidthF = 15,
                                        HeightF = hkwt,
                                        Text = "",
                                        LocationF = new PointF(134 + LX_Anahtar, LY_CevapAnahtar),
                                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                                        BackColor = Color.White,
                                        BorderColor = Color.FromArgb(180, 198, 231)
                                    };
                                    LX_Anahtar += xrCvpAnah.WidthF + xmarg;
                                    Detail.Controls.Add(xrCvpAnah);
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                if (i== tableBolumler.Rows.Count-1)
                {
                    for (int x = 0; x < counter; x++)
                    {
                        XRLabel xrOgrCvp = new XRLabel()
                        {
                            WidthF = 15,
                            HeightF = hkwt,
                            Text = "",
                            LocationF = new PointF(LX_OgrenciAnahtar, LY_OgrenciCevap),
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BackColor = Color.FromArgb(217, 226, 243),
                            BorderColor = Color.FromArgb(180, 198, 231)
                        };

                        LX_OgrenciAnahtar += xrOgrCvp.WidthF + xmarg;
                        Detail.Controls.Add(xrOgrCvp);

                        XRLabel xrCvpAnah = new XRLabel()
                        {
                            WidthF = 15,
                            HeightF = hkwt,
                            Text = "",
                            LocationF = new PointF(134 + LX_Anahtar, LY_CevapAnahtar),
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BackColor = Color.White,
                            BorderColor = Color.FromArgb(180, 198, 231)
                        };
                        LX_Anahtar += xrCvpAnah.WidthF + xmarg;
                        Detail.Controls.Add(xrCvpAnah);
                    }
                }

                LY_OgrenciCevap += 30 + ymarg;
                LY_CevapAnahtar += 30 + ymarg;
            }

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float LX_Anahtar = 136;
            int xmarg = 1;
            int hkwt = 15;
            float LY_CevapAnahtar = 19;
            Font fontIcerik = new Font(familyArial, 6.5f);
            Font fontBold = new Font(familyArial, 6.5f, FontStyle.Bold);

            int widthF = 15;

            for (int i = 0; i < 42; i++)
            {
                XRLabel xrSoruNo = new XRLabel()
                {
                    WidthF = widthF,
                    HeightF = hkwt,
                    Text = (i + 1).ToString(),
                    LocationF = new PointF(LX_Anahtar, LY_CevapAnahtar),
                    Font = fontIcerik,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BackColor = ColorTranslator.FromHtml("#0e3170"),
                    ForeColor = System.Drawing.Color.White,
                    BorderColor = ColorTranslator.FromHtml("#0e3170")
                };
                LX_Anahtar += xrSoruNo.WidthF + xmarg;
                GroupHeader1.Controls.Add(xrSoruNo);

                //LY_CevapAnahtar += 30 + ymarg;
            }


        }
    }
}
