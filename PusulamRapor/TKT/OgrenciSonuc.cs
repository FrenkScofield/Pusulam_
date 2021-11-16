using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.Utils;

namespace PusulamRapor.TKT
{
    public partial class OgrenciSonuc : DevExpress.XtraReports.UI.XtraReport
    {
        private string TCKIMLIKNO { get; set; }
        private string oturum { get; set; }
        private int ID_TKTTEST { get; set; }
        private int GRAFIK { get; set; }
        private string SECILI_OGRENCI { get; set; }
        private string DONEM { get; set; }
        private string TARIH { get; set; }
        private string HESAPTURU { get; set; }

        public OgrenciSonuc(string TCKIMLIKNO, string oturum, string ID_TKTTEST, string SECILI_OGRENCI, string GRAFIK, string DONEM, string TARIH, string HESAPTURU)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.oturum = oturum;
            this.ID_TKTTEST = Convert.ToInt32(ID_TKTTEST);
            this.GRAFIK = Convert.ToInt32(GRAFIK);
            this.SECILI_OGRENCI = SECILI_OGRENCI;
            this.DONEM = DONEM;
            this.TARIH = TARIH;
            this.HESAPTURU = HESAPTURU;
        }

        DataSet ds;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_TKTTEST", ID_TKTTEST);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@TARIH", TARIH);
                b.ParametreEkle("@HESAPTURU", HESAPTURU);
                b.ParametreEkle("@TCKIMLIKNO_OGR", SECILI_OGRENCI);
                b.ParametreEkle("@ID_MENU", 12);
                b.ParametreEkle("@ISLEM", (GRAFIK == 0 ? 7 : 11));

                ds = b.SorguGetir("sp_TKTTest");
                xrLabel_ADSOYAD.Text = ds.Tables[0].Rows[0]["ADSOYAD"].ToString();
                xrLabel_ADSOYAD2.Text = ds.Tables[0].Rows[0]["ADSOYAD"].ToString();

                int ID_KADEME3 = Convert.ToInt32(ds.Tables[0].Rows[0]["ID_KADEME3"]);
                if (ID_KADEME3 == 4)
                {
                    xrLabel2.Text = "B GRUBU" + Environment.NewLine + "UPGRADE BİLİŞSEL DEĞERLENDİRME TESTİ" + Environment.NewLine + "RAPORU";
                    xrRichText1.Html = @"<ul style='text-align:justify; font-size:15.75pt;'><li>Bilişsel değerlendirme testi çocukların bilişsel gelişimlerini takip etmek amacıyla uygulanır."
                                        + "</li><br/><li>" +
                                        "Raporda öğrencinin sözcük dağarcığı, olay akış algılama becerisi, genel bilgisi, görsel dikkat, işitsel ayrımlaştırma, sayı bilgisi, görsel mekânsal işlevler ve kağıt-kalem becerileri performanslarına bağlı olarak gelişmiş ve gelişime açık yönleri ile ilgili bilgi verir."
                                        + "</li><br/><li>" +
                                        "Bu değerlendirme sonucunda öğrencilerin ihtiyacı olan destekleyici çalışmalar yapılır."
                                        + "</li><br/><li>" +
                                        "Raporda adı geçen ve uygulanan testler öğrenciye grupla beraber uygulanmış olup, öğrencinin o gün içerisindeki motivasyonunun, dikkatinin ve duygusal durumunun test sonuçlarına yansıyabileceği önemle belirtilmektedir."
                                        + "</li></ul>";
                    xrLabel6.Text = "Bilişsel Beceriler Testi Neyi Ölçer?";
                    xrRichText2.Html = @"<div style='text-align:justify; font-size:15.75pt; margin-left:18px;'><p>&nbsp&nbsp&nbsp&nbsp&nbsp Çocuğun bilişsel performansını belirlemek amacıyla kullanılan ve dört alt testten oluşan bir yetenek testidir.  Alt testler çocuğun; sözel algılamasını, işitsel dikkatini, görsel dikkatini, algısal hızını, sayı olgunluğunu, psikomotor becerilerini ve görsel mekansal işlevlerini değerlendirmektedir.</p></div>";
                }
                else if (ID_KADEME3 == 5)
                {
                    xrLabel2.Text = "C GRUBU" + Environment.NewLine + "UPGRADE BİLİŞSEL DEĞERLENDİRME TESTİ" + Environment.NewLine + "RAPORU";
                    xrRichText1.Html = @"<ul style='text-align:justify; font-size:15.75pt;'><li>Bilişsel değerlendirme testi çocukların bilişsel gelişimlerini takip etmek amacıyla uygulanır."
                                        + "</li><br/><li>" +
                                        "Raporda öğrencinin sözcük dağarcığı, olay akış algılama becerisi, genel bilgisi, görsel dikkat, işitsel ayrımlaştırma, sayı bilgisi, görsel mekânsal işlevler ve kağıt-kalem becerileri performanslarına bağlı olarak gelişmiş ve gelişime açık yönleri ile ilgili bilgi verir."
                                        + "</li><br/><li>" +
                                        "Bu değerlendirme sonucunda öğrencilerin ihtiyacı olan destekleyici çalışmalar yapılır."
                                        + "</li><br/><li>" +
                                        "Raporda adı geçen ve uygulanan testler öğrenciye grupla beraber uygulanmış olup, öğrencinin o gün içerisindeki motivasyonunun, dikkatinin ve duygusal durumunun test sonuçlarına yansıyabileceği önemle belirtilmektedir."
                                        + "</li></ul>";
                    xrLabel6.Text = "Bilişsel Beceriler Testi Neyi Ölçer?";
                    xrRichText2.Html = @"<div style='text-align:justify; font-size:15.75pt; margin-left:18px;'><p>&nbsp&nbsp&nbsp&nbsp&nbsp Çocuğun bilişsel performansını belirlemek amacıyla kullanılan ve dört alt testten oluşan bir yetenek testidir.  Alt testler çocuğun; sözel algılamasını, işitsel dikkatini, görsel dikkatini, algısal hızını, sayı olgunluğunu, psikomotor becerilerini ve görsel mekansal işlevlerini değerlendirmektedir.</p></div>";
                }

                xrLabel_TARIH.Text = ds.Tables[1].Rows[0]["TARIH"].ToString();

                try
                {
                    xrLabel_KATEGORI1_AD.Text = ds.Tables[1].Rows[0]["KATEGORI"].ToString();
                    xrLabel_KATEGORI1_ACIKLAMA.Text = ds.Tables[1].Rows[0]["ACIKLAMA"].ToString();
                    xrLabel_KATEGORI1_SEVIYE.Text = ds.Tables[1].Rows[0]["SEVIYE"].ToString();
                }
                catch (Exception ex)
                {

                }

                try
                {
                    xrLabel_KATEGORI2_AD.Text = ds.Tables[1].Rows[1]["KATEGORI"].ToString();
                    xrLabel_KATEGORI2_ACIKLAMA.Text = ds.Tables[1].Rows[1]["ACIKLAMA"].ToString();
                    xrLabel_KATEGORI2_SEVIYE.Text = ds.Tables[1].Rows[1]["SEVIYE"].ToString();
                }
                catch (Exception ex)
                {

                }

                try
                {
                    xrLabel_KATEGORI3_AD.Text = ds.Tables[1].Rows[2]["KATEGORI"].ToString();
                    xrLabel_KATEGORI3_ACIKLAMA.Text = ds.Tables[1].Rows[2]["ACIKLAMA"].ToString();
                    xrLabel_KATEGORI3_SEVIYE.Text = ds.Tables[1].Rows[2]["SEVIYE"].ToString();
                }
                catch (Exception ex)
                {

                }

                try
                {
                    xrLabel_KATEGORI4_AD.Text = ds.Tables[1].Rows[3]["KATEGORI"].ToString();
                    xrLabel_KATEGORI4_ACIKLAMA.Text = ds.Tables[1].Rows[3]["ACIKLAMA"].ToString();
                    xrLabel_KATEGORI4_SEVIYE.Text = ds.Tables[1].Rows[3]["SEVIYE"].ToString();
                }
                catch (Exception ex)
                {

                }

                String html = "";
                html = "<div style='color:#0066CC; text-align:justify;'>";
                if (ds.Tables[1].Select("SEVIYE='Ortanın Üstü' or SEVIYE='Üstün'").Length > 0)
                {
                    html += "<div><b><u>Öğrencinin Güçlü Olduğu Yönleri:</u> </b> ";
                    try { html += (ds.Tables[1].Rows[0]["SEVIYE"].ToString().Equals("Ortanın Üstü") || ds.Tables[1].Rows[0]["SEVIYE"].ToString().Equals("Üstün") ? ds.Tables[1].Rows[0]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[1]["SEVIYE"].ToString().Equals("Ortanın Üstü") || ds.Tables[1].Rows[1]["SEVIYE"].ToString().Equals("Üstün") ? ds.Tables[1].Rows[1]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[2]["SEVIYE"].ToString().Equals("Ortanın Üstü") || ds.Tables[1].Rows[2]["SEVIYE"].ToString().Equals("Üstün") ? ds.Tables[1].Rows[2]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[3]["SEVIYE"].ToString().Equals("Ortanın Üstü") || ds.Tables[1].Rows[3]["SEVIYE"].ToString().Equals("Üstün") ? ds.Tables[1].Rows[3]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    html += "</div>";
                }
                if (ds.Tables[1].Select("SEVIYE='Desteklenmeli' or SEVIYE='Ortanın Altı' or SEVIYE='Orta'").Length > 0)
                {
                    html += "<div style='margin-top:10px;'>";
                    html += "<b><u>Öğrencinin Gelişime Açık Yönleri:</u> </b> ";
                    try { html += (ds.Tables[1].Rows[0]["SEVIYE"].ToString().Equals("Desteklenmeli") || ds.Tables[1].Rows[0]["SEVIYE"].ToString().Equals("Ortanın Altı") || ds.Tables[1].Rows[0]["SEVIYE"].ToString().Equals("Orta") ? ds.Tables[1].Rows[0]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[1]["SEVIYE"].ToString().Equals("Desteklenmeli") || ds.Tables[1].Rows[1]["SEVIYE"].ToString().Equals("Ortanın Altı") || ds.Tables[1].Rows[1]["SEVIYE"].ToString().Equals("Orta") ? ds.Tables[1].Rows[1]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[2]["SEVIYE"].ToString().Equals("Desteklenmeli") || ds.Tables[1].Rows[2]["SEVIYE"].ToString().Equals("Ortanın Altı") || ds.Tables[1].Rows[2]["SEVIYE"].ToString().Equals("Orta") ? ds.Tables[1].Rows[2]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    try { html += (ds.Tables[1].Rows[3]["SEVIYE"].ToString().Equals("Desteklenmeli") || ds.Tables[1].Rows[3]["SEVIYE"].ToString().Equals("Ortanın Altı") || ds.Tables[1].Rows[3]["SEVIYE"].ToString().Equals("Orta") ? ds.Tables[1].Rows[3]["KATEGORI"].ToString() + ", " : ""); } catch (Exception) { }
                    html += "</div>";
                }
                html += "Öğrencinin gelişime açık yönleri <i>Upgrade Programı</i> kapsamında bireyselleştirilmiş çalışmalar ile desteklenecektir.";
                html += "</div>";
                xrRichText_GENELSONUC.Html = html;
            }

            if (GRAFIK == 1)
            {
                Series DILKAVRAMI_SERIES1 = new Series("", ViewType.Bar);
                Series AYIRTETMEHIZI_SERIES1 = new Series("", ViewType.Bar);
                Series SAYIKAVRAMI_SERIES1 = new Series("", ViewType.Bar);
                Series YERKAVRAMI_SERIES1 = new Series("", ViewType.Bar);


                Series DILKAVRAMI_SERIES2 = new Series("", ViewType.Bar);
                Series AYIRTETMEHIZI_SERIES2 = new Series("", ViewType.Bar);
                Series SAYIKAVRAMI_SERIES2 = new Series("", ViewType.Bar);
                Series YERKAVRAMI_SERIES2 = new Series("", ViewType.Bar);


                Series DILKAVRAMI_SERIES3 = new Series("", ViewType.Bar);
                Series AYIRTETMEHIZI_SERIES3 = new Series("", ViewType.Bar);
                Series SAYIKAVRAMI_SERIES3 = new Series("", ViewType.Bar);
                Series YERKAVRAMI_SERIES3 = new Series("", ViewType.Bar);

                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        SeriesPoint point1 = new SeriesPoint(SplitText(Convert.ToString(dr["ALTKATEGORI"])), Convert.ToDecimal(dr["ONTEST"]));
                        SeriesPoint point2 = new SeriesPoint(SplitText(Convert.ToString(dr["ALTKATEGORI"])), Convert.ToDecimal(dr["ARATEST"]));
                        SeriesPoint point3 = new SeriesPoint(SplitText(Convert.ToString(dr["ALTKATEGORI"])), Convert.ToDecimal(dr["SONTEST"]));
                        if (dr["ID_KATEGORI"].ToString().Equals("1"))
                        {
                            DILKAVRAMI_SERIES1.Points.Add(point1);
                            DILKAVRAMI_SERIES2.Points.Add(point2);
                            DILKAVRAMI_SERIES3.Points.Add(point3);
                        }
                        else if (dr["ID_KATEGORI"].ToString().Equals("2"))
                        {
                            AYIRTETMEHIZI_SERIES1.Points.Add(point1);
                            AYIRTETMEHIZI_SERIES2.Points.Add(point2);
                            AYIRTETMEHIZI_SERIES3.Points.Add(point3);
                        }
                        else if (dr["ID_KATEGORI"].ToString().Equals("3"))
                        {
                            SAYIKAVRAMI_SERIES1.Points.Add(point1);
                            SAYIKAVRAMI_SERIES2.Points.Add(point2);
                            SAYIKAVRAMI_SERIES3.Points.Add(point3);
                        }
                        else if (dr["ID_KATEGORI"].ToString().Equals("4"))
                        {
                            YERKAVRAMI_SERIES1.Points.Add(point1);
                            YERKAVRAMI_SERIES2.Points.Add(point2);
                            YERKAVRAMI_SERIES3.Points.Add(point3);
                        }
                    }
                }

                DILKAVRAMI.Titles.Add(new ChartTitle());
                DILKAVRAMI.Titles[0].Text = "DİL KAVRAMI";
                DILKAVRAMI.Titles[0].WordWrap = true;
                DILKAVRAMI.Titles[0].TextColor = Color.FromArgb(31, 73, 125);
                DILKAVRAMI_SERIES1.LabelsVisibility = DefaultBoolean.False;
                DILKAVRAMI_SERIES2.LabelsVisibility = DefaultBoolean.False;
                DILKAVRAMI_SERIES3.LabelsVisibility = DefaultBoolean.False;

                DILKAVRAMI_SERIES1.View.Color = Color.FromArgb(80, 123, 191);
                DILKAVRAMI_SERIES2.View.Color = Color.FromArgb(31, 73, 125);
                DILKAVRAMI_SERIES3.View.Color = Color.FromArgb(89, 147, 219);

                ((BarSeriesView)DILKAVRAMI_SERIES1.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)DILKAVRAMI_SERIES2.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)DILKAVRAMI_SERIES3.View).FillStyle.FillMode = FillMode.Solid;

                DILKAVRAMI_SERIES1.LegendText = "Ön Test";
                DILKAVRAMI_SERIES2.LegendText = "Ara Test";
                DILKAVRAMI_SERIES3.LegendText = "Son Test";

                if (!IsZero(DILKAVRAMI_SERIES1))
                {
                    DILKAVRAMI.Series.Add(DILKAVRAMI_SERIES1);
                }
                if (!IsZero(DILKAVRAMI_SERIES2))
                {
                    DILKAVRAMI.Series.Add(DILKAVRAMI_SERIES2);
                }
                if (!IsZero(DILKAVRAMI_SERIES3))
                {
                    DILKAVRAMI.Series.Add(DILKAVRAMI_SERIES3);
                }


                AYIRTETMEHIZI.Titles.Add(new ChartTitle());
                AYIRTETMEHIZI.Titles[0].Text = "AYIRT ETME HIZI";
                AYIRTETMEHIZI.Titles[0].WordWrap = true;
                AYIRTETMEHIZI.Titles[0].TextColor = Color.FromArgb(31, 73, 125);
                AYIRTETMEHIZI_SERIES1.LabelsVisibility = DefaultBoolean.False;
                AYIRTETMEHIZI_SERIES2.LabelsVisibility = DefaultBoolean.False;
                AYIRTETMEHIZI_SERIES3.LabelsVisibility = DefaultBoolean.False;

                AYIRTETMEHIZI_SERIES1.View.Color = Color.FromArgb(80, 123, 191);
                AYIRTETMEHIZI_SERIES2.View.Color = Color.FromArgb(31, 73, 125);
                AYIRTETMEHIZI_SERIES3.View.Color = Color.FromArgb(89, 147, 219);

                ((BarSeriesView)AYIRTETMEHIZI_SERIES1.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)AYIRTETMEHIZI_SERIES2.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)AYIRTETMEHIZI_SERIES3.View).FillStyle.FillMode = FillMode.Solid;

                AYIRTETMEHIZI_SERIES1.LegendText = "Ön Test";
                AYIRTETMEHIZI_SERIES2.LegendText = "Ara Test";
                AYIRTETMEHIZI_SERIES3.LegendText = "Son Test";

                if (!IsZero(AYIRTETMEHIZI_SERIES1))
                {
                    AYIRTETMEHIZI.Series.Add(AYIRTETMEHIZI_SERIES1);
                }
                if (!IsZero(AYIRTETMEHIZI_SERIES2))
                {
                    AYIRTETMEHIZI.Series.Add(AYIRTETMEHIZI_SERIES2);
                }
                if (!IsZero(AYIRTETMEHIZI_SERIES3))
                {
                    AYIRTETMEHIZI.Series.Add(AYIRTETMEHIZI_SERIES3);
                }

                SAYIKAVRAMI.Titles.Add(new ChartTitle());
                SAYIKAVRAMI.Titles[0].Text = "SAYI KAVRAMI";
                SAYIKAVRAMI.Titles[0].WordWrap = true;
                SAYIKAVRAMI.Titles[0].TextColor = Color.FromArgb(31, 73, 125);
                SAYIKAVRAMI_SERIES1.LabelsVisibility = DefaultBoolean.False;
                SAYIKAVRAMI_SERIES2.LabelsVisibility = DefaultBoolean.False;
                SAYIKAVRAMI_SERIES3.LabelsVisibility = DefaultBoolean.False;

                SAYIKAVRAMI_SERIES1.View.Color = Color.FromArgb(80, 123, 191);
                SAYIKAVRAMI_SERIES2.View.Color = Color.FromArgb(31, 73, 125);
                SAYIKAVRAMI_SERIES3.View.Color = Color.FromArgb(89, 147, 219);

                ((BarSeriesView)SAYIKAVRAMI_SERIES1.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)SAYIKAVRAMI_SERIES2.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)SAYIKAVRAMI_SERIES3.View).FillStyle.FillMode = FillMode.Solid;

                SAYIKAVRAMI_SERIES1.LegendText = "Ön Test";
                SAYIKAVRAMI_SERIES2.LegendText = "Ara Test";
                SAYIKAVRAMI_SERIES3.LegendText = "Son Test";

                if (!IsZero(SAYIKAVRAMI_SERIES1))
                {
                    SAYIKAVRAMI.Series.Add(SAYIKAVRAMI_SERIES1);
                }
                if (!IsZero(SAYIKAVRAMI_SERIES2))
                {
                    SAYIKAVRAMI.Series.Add(SAYIKAVRAMI_SERIES2);
                }
                if (!IsZero(SAYIKAVRAMI_SERIES3))
                {
                    SAYIKAVRAMI.Series.Add(SAYIKAVRAMI_SERIES3);
                }

                YERKAVRAMI.Titles.Add(new ChartTitle());
                YERKAVRAMI.Titles[0].Text = "YER KAVRAMI";
                YERKAVRAMI.Titles[0].WordWrap = true;
                YERKAVRAMI.Titles[0].TextColor = Color.FromArgb(31, 73, 125);
                YERKAVRAMI_SERIES1.LabelsVisibility = DefaultBoolean.False;
                YERKAVRAMI_SERIES2.LabelsVisibility = DefaultBoolean.False;
                YERKAVRAMI_SERIES3.LabelsVisibility = DefaultBoolean.False;

                YERKAVRAMI_SERIES1.View.Color = Color.FromArgb(80, 123, 191);
                YERKAVRAMI_SERIES2.View.Color = Color.FromArgb(31, 73, 125);
                YERKAVRAMI_SERIES3.View.Color = Color.FromArgb(89, 147, 219);

                ((BarSeriesView)SAYIKAVRAMI_SERIES1.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)YERKAVRAMI_SERIES2.View).FillStyle.FillMode = FillMode.Solid;
                ((BarSeriesView)YERKAVRAMI_SERIES3.View).FillStyle.FillMode = FillMode.Solid;

                YERKAVRAMI_SERIES1.LegendText = "Ön Test";
                YERKAVRAMI_SERIES2.LegendText = "Ara Test";
                YERKAVRAMI_SERIES3.LegendText = "Son Test";

                if (!IsZero(YERKAVRAMI_SERIES1))
                {
                    YERKAVRAMI.Series.Add(YERKAVRAMI_SERIES1);
                }
                if (!IsZero(YERKAVRAMI_SERIES2))
                {
                    YERKAVRAMI.Series.Add(YERKAVRAMI_SERIES2);
                }
                if (!IsZero(YERKAVRAMI_SERIES3))
                {
                    YERKAVRAMI.Series.Add(YERKAVRAMI_SERIES3);
                }

                XYDiagram xydDILKAVRAMI = DILKAVRAMI.Diagram as XYDiagram;
                if (xydDILKAVRAMI != null)
                {
                    xydDILKAVRAMI.AxisX.Label.Angle = 0;
                    xydDILKAVRAMI.AxisY.Title.Text = "GELİŞİM YÜZDESİ";
                    xydDILKAVRAMI.AxisY.Title.Visibility = DefaultBoolean.True;
                }
                XYDiagram xydAYIRTETMEHIZI = AYIRTETMEHIZI.Diagram as XYDiagram;
                if (xydAYIRTETMEHIZI != null)
                {
                    xydAYIRTETMEHIZI.AxisX.Label.Angle = 0;
                    xydAYIRTETMEHIZI.AxisY.Title.Text = "GELİŞİM YÜZDESİ";
                    xydAYIRTETMEHIZI.AxisY.Title.Visibility = DefaultBoolean.True;
                }
                XYDiagram xydSAYIKAVRAMI = SAYIKAVRAMI.Diagram as XYDiagram;
                if (xydSAYIKAVRAMI != null)
                {
                    xydSAYIKAVRAMI.AxisX.Label.Angle = 0;
                    xydSAYIKAVRAMI.AxisY.Title.Text = "GELİŞİM YÜZDESİ";
                    xydSAYIKAVRAMI.AxisY.Title.Visibility = DefaultBoolean.True;
                }
                XYDiagram xydYERKAVRAMI = YERKAVRAMI.Diagram as XYDiagram;
                if (xydYERKAVRAMI != null)
                {
                    xydYERKAVRAMI.AxisX.Label.Angle = 0;
                    xydYERKAVRAMI.AxisY.Title.Text = "GELİŞİM YÜZDESİ";
                    xydYERKAVRAMI.AxisY.Title.Visibility = DefaultBoolean.True;
                }
            }
            else
            {
                DILKAVRAMI.Visible = false;
                AYIRTETMEHIZI.Visible = false;
                SAYIKAVRAMI.Visible = false;
                YERKAVRAMI.Visible = false;
                xrPageBreak1.Visible = false;
            }
        }

        public bool IsZero(Series s)
        {
            for (int i = 0; i < s.Points.Count; i++)
            {
                double value = s.Points[i].Values[0];
                if (value > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public string SplitText(string data)
        {
            string[] array = data.Split(' ');
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i % 2 == 1)
                {
                    s = s + array[i] + "\n";
                }
                else
                {
                    s = s + array[i] + " ";
                }
            }

            return s;
        }
    }
}
