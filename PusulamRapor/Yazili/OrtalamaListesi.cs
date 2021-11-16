using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class OrtalamaListesi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SINIFLAR { get; set; }
        public string ID_SUBELER { get; set; }
        public string DONEM { get; set; }
        public string RAPORTUR { get; set; }
        DataSet ds;
        public OrtalamaListesi(string tckimlikno, string oturum, string idSinav, string idSiniflar, string idSubeler, string DONEM, string RAPORTUR)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SINIFLAR = idSiniflar;
            ID_SUBELER = idSubeler;
            this.RAPORTUR = RAPORTUR;
            this.DONEM = DONEM;
        }

        private void OrtalamaListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@ID_MENU", 1090);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@RAPORTUR", RAPORTUR);
                b.ParametreEkle("@ISLEM", 4); //2 ESKİ
                ds = b.SorguGetir("sp_OrtalamaListesi");
            }

            lblDONEMKOD.Text = ds.Tables[0].Rows[0]["DONEMKOD"].ToString()+ " EĞİTİM-ÖĞRETİM YILI";
            lblSINAVAD.Text = ds.Tables[0].Rows[0]["SINAVAD"].ToString();

            //Headers
            if (RAPORTUR.Equals("0"))
            //if (ID_SUBELER.IndexOf("[0,") > -1)
            {
                xrPanel_Headers.WidthF = xrPanel_Headers.WidthF + 100;
                xrPanel_Headers.LocationF = new PointF(xrPanel_Headers.LocationF.X - 100, 0);
            }



            float width = (xrPanel_Headers.WidthF / (ds.Tables[1].Rows.Count + 1));
            float height = xrPanel_Headers.HeightF / 2;
            float x = 0f;

            FontFamily familyArial = new FontFamily("TAHOMA");
            Font font = new Font(familyArial, 9, FontStyle.Bold);
            int ToplamPuan = 0;
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[1].Rows[i];

                XRLabel xr_Header_Puan = new XRLabel()
                {
                    Text = dr["PUANDEGERI"].ToString(),
                    LocationF = new PointF(x, 0),
                    WidthF = width,
                    Font = font,
                    HeightF = height,
                    Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom,
                    BorderColor = Color.White,
                    BackColor = Color.FromArgb(255, 68, 71, 92),
                    ForeColor = Color.White,
                    KeepTogether = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    CanGrow = true
                };
                xrPanel_Headers.Controls.Add(xr_Header_Puan);

                XRLabel xr_Header_Soru = new XRLabel()
                {
                    Text = dr["SORUNO"].ToString() + ". SORU",
                    LocationF = new PointF(x, height),
                    WidthF = width,
                    Font = font,
                    HeightF = height,
                    Borders = DevExpress.XtraPrinting.BorderSide.Right,
                    BorderColor = Color.White,
                    BackColor = Color.FromArgb(255, 68, 71, 92),
                    ForeColor = Color.White,
                    KeepTogether = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    CanGrow = true
                };

                xrPanel_Headers.Controls.Add(xr_Header_Soru);
                ToplamPuan += Convert.ToInt32(dr["PUANDEGERI"].ToString());
                x += width;
            }

            XRLabel xr_Header_Puan_Toplam = new XRLabel()
            {
                Text = ToplamPuan.ToString(),
                LocationF = new PointF(x, 0),
                WidthF = width,
                Font = font,
                HeightF = height,
                Borders = DevExpress.XtraPrinting.BorderSide.Bottom,
                BorderColor = Color.White,
                BackColor = Color.FromArgb(255, 68, 71, 92),
                ForeColor = Color.White,
                KeepTogether = true,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanGrow = true
            };
            xrPanel_Headers.Controls.Add(xr_Header_Puan_Toplam);

            XRLabel xr_Header_Soru_Toplam = new XRLabel()
            {
                Text = "TOPLAM",
                LocationF = new PointF(x, height),
                WidthF = width,
                Font = font,
                HeightF = height,
                BackColor = Color.FromArgb(255, 68, 71, 92),
                ForeColor = Color.White,
                KeepTogether = true,
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                CanGrow = true
            };
            xrPanel_Headers.Controls.Add(xr_Header_Soru_Toplam);
            //Headers


            Font fontBody = new Font(familyArial, 8, FontStyle.Regular);
            float xbody = 0;
            float ybody = -36;
            String tempSinif = "";
            String tempSube = "";
            int SIRA = 0;
            Color backcolorBody = Color.White;
            float heightBody = 36;
            float toplam = 0;
            bool cont = false;

            if (RAPORTUR.Equals("0"))
            //if (ID_SUBELER.IndexOf("[0,") > -1)
            {
                xrLabel_Sinif.Visible = false;
                xrLabel6.Visible = false;
                xrLabel5.Text = "PUAN DEĞERİ";
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];

                if (((!RAPORTUR.Equals("0")) && (!tempSinif.Equals(dr["SINIF"].ToString()) || tempSinif.Equals("")))
                    ||
                    ((RAPORTUR.Equals("0")) && (!tempSube.Equals(dr["SUBE"].ToString()) || tempSube.Equals("")))
                   )
                {
                    if (cont)
                    {
                        XRLabel xr_Body_Toplam_Ortalama = new XRLabel()
                        {
                            Text = toplam.ToString("0.00"),
                            LocationF = new PointF(xbody, ybody),
                            WidthF = width,
                            Font = fontBody,
                            HeightF = heightBody,
                            Borders = DevExpress.XtraPrinting.BorderSide.Right,
                            BorderColor = Color.FromArgb(255, 68, 71, 92),
                            BackColor = backcolorBody,
                            ForeColor = Color.FromArgb(255, 68, 71, 92),
                            KeepTogether = true,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            CanGrow = true
                        };
                        xrPanel_Body.Controls.Add(xr_Body_Toplam_Ortalama);
                    }
                    toplam = 0;
                    tempSinif = dr["SINIF"].ToString();
                    tempSube = dr["SUBE"].ToString();
                    SIRA++;
                    xbody = 0;
                    ybody += heightBody;

                    if (SIRA % 2 == 1)
                    {
                        backcolorBody = Color.White;
                    }
                    else
                    {
                        backcolorBody = Color.FromArgb(255, 212, 216, 249);
                    }

                    XRLabel xr_Body_Sira = new XRLabel()
                    {
                        Text = SIRA.ToString(),
                        LocationF = new PointF(xbody, ybody),
                        WidthF = 60,
                        Font = fontBody,
                        HeightF = heightBody,
                        Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left,
                        BorderColor = Color.FromArgb(255, 68, 71, 92),
                        BackColor = backcolorBody,
                        ForeColor = Color.FromArgb(255, 68, 71, 92),
                        KeepTogether = true,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        CanGrow = true
                    };
                    xrPanel_Body.Controls.Add(xr_Body_Sira);
                    xbody += 60;

                    XRLabel xr_Body_Kampus = new XRLabel()
                    {
                        Text = dr["SUBE"].ToString(),
                        LocationF = new PointF(xbody, ybody),
                        WidthF = 100,
                        Font = fontBody,
                        HeightF = heightBody,
                        Borders = DevExpress.XtraPrinting.BorderSide.Right,
                        BorderColor = Color.FromArgb(255, 68, 71, 92),
                        BackColor = backcolorBody,
                        ForeColor = Color.FromArgb(255, 68, 71, 92),
                        KeepTogether = true,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        CanGrow = true
                    };
                    xrPanel_Body.Controls.Add(xr_Body_Kampus);
                    xbody += 100;


                    if (!RAPORTUR.Equals("0"))
                    //if (ID_SUBELER.IndexOf("[0,") == -1)
                    {
                        XRLabel xr_Body_Sinif = new XRLabel()
                        {
                            Text = dr["SINIF"].ToString(),
                            LocationF = new PointF(xbody, ybody),
                            WidthF = 100,
                            Font = fontBody,
                            HeightF = heightBody,
                            Borders = DevExpress.XtraPrinting.BorderSide.Right,
                            BorderColor = Color.FromArgb(255, 68, 71, 92),
                            BackColor = backcolorBody,
                            ForeColor = Color.FromArgb(255, 68, 71, 92),
                            KeepTogether = true,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            CanGrow = true
                        };
                        xrPanel_Body.Controls.Add(xr_Body_Sinif);
                        xbody += 100;
                    }
                }

                XRLabel xr_Body_Ortalama = new XRLabel()
                {
                    Text = dr["ORTALAMA"].ToString(),
                    LocationF = new PointF(xbody, ybody),
                    WidthF = width,
                    Font = fontBody,
                    HeightF = heightBody,
                    Borders = DevExpress.XtraPrinting.BorderSide.Right,
                    BorderColor = Color.FromArgb(255, 68, 71, 92),
                    BackColor = backcolorBody,
                    ForeColor = Color.FromArgb(255, 68, 71, 92),
                    KeepTogether = true,
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    CanGrow = true
                };
                xrPanel_Body.Controls.Add(xr_Body_Ortalama);
                xbody += width;
                toplam += Convert.ToSingle(dr["ORTALAMA"].ToString());
                cont = true;

                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    XRLabel xr_Body_Toplam_Ortalama_Son = new XRLabel()
                    {
                        Text = toplam.ToString("0.00"),
                        LocationF = new PointF(xbody, ybody),
                        WidthF = width,
                        Font = fontBody,
                        HeightF = heightBody,
                        Borders = DevExpress.XtraPrinting.BorderSide.Right,
                        BorderColor = Color.FromArgb(255, 68, 71, 92),
                        BackColor = backcolorBody,
                        ForeColor = Color.FromArgb(255, 68, 71, 92),
                        KeepTogether = true,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        CanGrow = true
                    };
                    xrPanel_Body.Controls.Add(xr_Body_Toplam_Ortalama_Son);
                }
            }
        }
    }
}
