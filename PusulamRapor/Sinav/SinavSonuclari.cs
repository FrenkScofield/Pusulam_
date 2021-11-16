using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class SinavSonuclari : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        private string TCKIMLIKNO;
        private string OTURUM;
        private string TC_OGRENCI;
        private int ID_SINAV;
        public int ID_MENU { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public bool LiseMi = false;
        public bool SIRALAMAGIZLE = false;
        public SinavSonuclari(string _tckimlikno, string _oturum, string _tcOgrenci, string _idSinav, string veli)
        {

            TCKIMLIKNO = _tckimlikno;
            OTURUM = _oturum;
            TC_OGRENCI = _tcOgrenci;
            ID_SINAV = Convert.ToInt32(_idSinav);
            ID_MENU = 1031;
            if (veli == "1")
            {
                ID_MENU = 1066;
            }
            if (veli == "2")
            {
                ID_MENU = 1045;
            }

            InitializeComponent();
        }

        private void SinavSonuclari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_MENU", ID_MENU);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                //b.ParametreEkle("@ID_MENU", 1031);
                ds = b.SorguGetir("sp_SinavSonuclari");
                dt1 = ds.Tables[0];
                dt2 = ds.Tables[1];
                dt3 = ds.Tables[2];
                dt4 = ds.Tables[3];
                dt5 = ds.Tables[4];
            }

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrLabel_AdSoyad.Text = dt1.Rows[0]["ADSOYAD"].ToString();
            xrLabel_Sinif.Text = dt1.Rows[0]["SINIF"].ToString();
            xrLabel_Sinav.Text = dt1.Rows[0]["SINAVAD"].ToString();
            xrLabel_SinavMain.Text = dt1.Rows[0]["SINAVTURU"].ToString();
            xrLabel_SinavTuru1.Text = dt1.Rows[0]["SINAVTURUUZUN"].ToString();
            xrLabel_Tarih.Text = dt1.Rows[0]["SINAVTARIH"].ToString();
            xrLabel_Kitapcik.Text = dt1.Rows[0]["KITAPCIK"].ToString();
            xrLabel_Okul.Text = dt1.Rows[0]["SUBEAD"].ToString();
            SIRALAMAGIZLE = Convert.ToBoolean(dt1.Rows[0]["SIRALAMAYIGIZLE"].ToString());

            if (SIRALAMAGIZLE ==false)
            {
                xrLabel_SinifKatilim.Text = dt1.Rows[0]["SINIFKATILIM"].ToString();
                xrLabel_OkulKatilim.Text = dt1.Rows[0]["SUBEKATILIM"].ToString();
                xrLabel_IlceKatilim.Text = dt1.Rows[0]["ILCEKATILIM"].ToString();
                xrLabel_IlKatilim.Text = dt1.Rows[0]["ILKATILIM"].ToString();
                xrLabel_GenelKatilim.Text = dt1.Rows[0]["GENELKATILIM"].ToString();
            }
            else
            {
                xrLabel2.Visible = false;
                xrLabel31.Visible = false;
                xrLabel_SinifKatilim.Visible = false;
                xrLabel39.Visible = false;
                xrLabel_OkulKatilim.Visible = false;
                xrLabel_IlceKatilim.Visible = false;
                xrLabel_IlKatilim.Visible = false;
                xrLabel_GenelKatilim.Visible = false;
                xrLabel9.Visible = false;
                xrLabel14.Visible = false;
                xrLabel41.Visible = false;

            }
            bool PUANGIZLE = Convert.ToBoolean(dt1.Rows[0]["PUANGIZLE"].ToString());
            if (PUANGIZLE == false)
            {
                if (SIRALAMAGIZLE == false)
                {
                    ssPuanSirala ssPS = new ssPuanSirala(dt1, true, true);
                    xrSubreport_ssPuanSirala.ReportSource = ssPS;
                }
                
            }
          

            if (Convert.ToInt32(dt1.Rows[0]["ID_KADEME"].ToString()) == 5) // lise
            {
                LiseMi = true;
                pbLise.Visible = true;
                pbOrtaOkul.Visible = false;
            }
            else
            {
                LiseMi = false;
                pbLise.Visible = false;
                pbOrtaOkul.Visible = true;
            }
            //lblDonem.Text=(dt1.Rows[0]["DONEM"].ToString()) + " Eğitim Öğretim Yılı ";
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int idSinavTuru = Convert.ToInt32(dt1.Rows[0]["ID_SINAVTURU"].ToString());
            ssOgrenciAnalizi ssOA = new Sinav.ssOgrenciAnalizi(dt2, LiseMi, idSinavTuru == 12,SIRALAMAGIZLE);
            xrSubreport_ssOgrenciAnaliz.ReportSource = ssOA;

            ssCevapAnahtari ssCA = new ssCevapAnahtari(dt3);
            xrSubreport_CevapAnahtari.ReportSource = ssCA;

            DataTable tableBolumler = new DataTable();
            tableBolumler = dt4.DefaultView.ToTable(true, "DERSAD");

            xr_dersbasari.Series.Clear();

            try
            {
                #region Series 
                Series srsYuzdeGenel = new Series("GENEL", ViewType.Bar);
                Series srsYuzdeSinif = new Series("SINIF", ViewType.Bar);
                Series srsYuzdeOgrenci = new Series("ÖĞRENCİ", ViewType.Bar);

                Series srsDogru = new Series("DOĞRU", ViewType.Bar);
                Series srsYanlis = new Series("YANLIŞ", ViewType.Bar);
                Series srsBos = new Series("BOŞ", ViewType.Bar);


                Font font = new Font(familyArial, 6, FontStyle.Bold);
                srsYuzdeGenel.Label.Font = font;
                srsYuzdeSinif.Label.Font = font;
                srsYuzdeOgrenci.Label.Font = font;
                srsDogru.Label.Font = font;
                srsYanlis.Label.Font = font;
                srsBos.Label.Font = font;

                foreach (DataRow item in dt4.Rows)
                {
                    srsYuzdeOgrenci.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["OGRENCI"].ToString())));
                    srsYuzdeSinif.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["SINIF"].ToString())));
                    srsYuzdeGenel.Points.Add(new SeriesPoint(item["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(item["GENEL"].ToString())));
                }


                srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
                ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
                srsYuzdeGenel.Label.Border.Color = Color.Transparent;
                srsYuzdeGenel.Label.BackColor = Color.Transparent;
                srsYuzdeGenel.Label.TextColor = Color.DarkBlue;


                srsYuzdeSinif.Label.TextOrientation = TextOrientation.Horizontal;
                ((BarSeriesLabel)srsYuzdeSinif.Label).Position = BarSeriesLabelPosition.Top;
                srsYuzdeSinif.Label.Border.Color = Color.Transparent;
                srsYuzdeSinif.Label.BackColor = Color.Transparent;
                srsYuzdeSinif.Label.TextColor = Color.DarkBlue;


                srsYuzdeOgrenci.Label.TextOrientation = TextOrientation.Horizontal;
                ((BarSeriesLabel)srsYuzdeOgrenci.Label).Position = BarSeriesLabelPosition.Top;
                srsYuzdeOgrenci.Label.Border.Color = Color.Transparent;
                srsYuzdeOgrenci.Label.BackColor = Color.Transparent;
                srsYuzdeOgrenci.Label.TextColor = Color.DarkBlue;



                xr_dersbasari.Series.Add(srsYuzdeGenel);
                xr_dersbasari.Series.Add(srsYuzdeSinif);
                xr_dersbasari.Series.Add(srsYuzdeOgrenci);


                #endregion

                float y = 0;
                XRPanel EklenecekPanel = new XRPanel();
                //  int DataRowDon = dt5.Rows.Count / 2;


                panel_unite.Controls.Clear();
                panel_unite1.Controls.Clear();

                int DataRowDon = dt5.Rows.Count;
                EklenecekPanel = panel_unite;
                bool GirdiBirkere = false;
                bool IkinciSayfa = false;
                int Yarisi = (dt5.Rows.Count / 2) - 1;

                for (int i = 0; i < DataRowDon; i++)
                {
                    if (Yarisi >= i)
                    {
                        EklenecekPanel = panel_unite;
                    }
                    else
                    {
                        EklenecekPanel = panel_unite1;
                        if (GirdiBirkere == false)
                        {
                            y = 0;
                            GirdiBirkere = true;
                        }
                    }

                    float x = 0;
                    //Color BackC = dt5.Rows[i]["KOD"].ToString()== "" ? Color.Orange : Color.Transparent;
                    Color BackC = i % 2 == 0 ? Color.FromArgb(217, 226, 243) : Color.White;
                    Color BorderC = Color.FromArgb(180, 198, 231);


                    XRLabel xr_KAZANIM = new XRLabel()
                    {
                        Text = dt5.Rows[i]["KONUAD"].ToString(),
                        LocationF = new PointF(x, y),
                        WidthF = dt5.Rows[i]["KOD"].ToString() != "" ? 235 : 235 + 172,
                        Font = font,
                        HeightF = 15,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderColor = BorderC,
                        BackColor = dt5.Rows[i]["KOD"].ToString() != "" ? BackC : Color.Orange,
                        Padding = 4,
                        KeepTogether = true,
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                        CanGrow = true
                    };
                    EklenecekPanel.Controls.Add(xr_KAZANIM);

                    if (dt5.Rows[i]["KOD"].ToString().Length > 0)
                    {
                        x += xr_KAZANIM.WidthF;
                        XRLabel xr_ss = new XRLabel()
                        {
                            Text = dt5.Rows[i]["SORU"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_ss);
                        x += xr_ss.WidthF;

                        XRLabel xr_d = new XRLabel()
                        {
                            Text = dt5.Rows[i]["DOGRU"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_d);
                        x += xr_d.WidthF;
                        XRLabel xr_y = new XRLabel()
                        {
                            Text = dt5.Rows[i]["YANLIS"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_y);
                        x += xr_y.WidthF;
                        XRLabel xr_b = new XRLabel()
                        {
                            Text = dt5.Rows[i]["BOS"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 30,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_b);
                        x += xr_y.WidthF;
                        XRLabel xr_o = new XRLabel()
                        {
                            Text = dt5.Rows[i]["YUZDE"].ToString(),
                            LocationF = new PointF(x, y),
                            WidthF = 52,
                            Font = font,
                            HeightF = xr_KAZANIM.HeightF,
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderColor = BorderC,
                            BackColor = BackC
                        };
                        EklenecekPanel.Controls.Add(xr_o);
                        x += xr_y.WidthF;
                    }

                    y += xr_KAZANIM.HeightF;
                }

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void xrSubreport_ssPuanSirala_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}