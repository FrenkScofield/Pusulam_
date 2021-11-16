using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Yazili
{
    public partial class SinavAnaliz : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SINIFLAR { get; set; }
        public string ID_SUBELER { get; set; }
        public string DONEM { get; set; }
        DataSet ds;
        public SinavAnaliz(string tckimlikno, string oturum, string idSinav, string idSiniflar, string idSubeler, string DONEM)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SINIFLAR = idSiniflar;
            ID_SUBELER = idSubeler;
            this.DONEM = DONEM;
        }

        private void SinavAnaliz_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@ID_MENU", 1088);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 4);//2 ESKİ
                ds = b.SorguGetir("sp_SinavAnaliz");
            }

            xrChart1.Series.Clear();
            Series srsYuzdeGenel = new Series(ds.Tables[4].Rows[0][0].ToString(), ViewType.Bar);
            xrChart1.Titles[0].Text = ds.Tables[4].Rows[0][0].ToString();
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                srsYuzdeGenel.Points.Add(new SeriesPoint(item["DERS"].ToString() + "\nSORU " + item["SN"].ToString(), Convert.ToDouble(item["YUZDE"].ToString())));
            }

            #region Series Label
            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
            #endregion

            xrChart1.Series.Add(srsYuzdeGenel);

            EB.Text = ds.Tables[3].Rows[0]["ENYUKSEK"].ToString();
            ED.Text = ds.Tables[3].Rows[0]["ENDUSUK"].ToString();
            O.Text = ds.Tables[3].Rows[0]["ORTALAMA"].ToString();

            S1.Text = ds.Tables[1].Rows[0]["KISISAYISI"].ToString() + " KİŞİ";
            S2.Text = ds.Tables[1].Rows[1]["KISISAYISI"].ToString() + " KİŞİ";
            S3.Text = ds.Tables[1].Rows[2]["KISISAYISI"].ToString() + " KİŞİ";
            S4.Text = ds.Tables[1].Rows[3]["KISISAYISI"].ToString() + " KİŞİ";
            S5.Text = ds.Tables[1].Rows[4]["KISISAYISI"].ToString() + " KİŞİ";

            Y1.Text = ds.Tables[1].Rows[0]["YUZDE"].ToString();
            Y2.Text = ds.Tables[1].Rows[1]["YUZDE"].ToString();
            Y3.Text = ds.Tables[1].Rows[2]["YUZDE"].ToString();
            Y4.Text = ds.Tables[1].Rows[3]["YUZDE"].ToString();
            Y5.Text = ds.Tables[1].Rows[4]["YUZDE"].ToString();

            BS.Text = ds.Tables[2].Rows[0]["BASARILI"].ToString() + " KİŞİ";
            BSIZS.Text = ds.Tables[2].Rows[0]["BASARISIZ"].ToString() + " KİŞİ";

            BY.Text = ds.Tables[2].Rows[0]["BASARILIYUZDE"].ToString();
            BSIZY.Text = ds.Tables[2].Rows[0]["BASARISIZYUZDE"].ToString();

            xrChart2.Series.Clear();
            Series srsxrChart2 = new Series("SAYI BAZINDA DERECE DAĞILIMI", ViewType.Bar);

            xrChart3.Series.Clear();
            Series srsxrChart3 = new Series("YÜZDELİK BAZDA DERECE DAĞILIMI", ViewType.Pie);
            foreach (DataRow item in ds.Tables[1].Rows)
            {
                srsxrChart2.Points.Add(new SeriesPoint(item["DURUM"].ToString(), Convert.ToDouble(item["KISISAYISI"].ToString())));
                srsxrChart3.Points.Add(new SeriesPoint(item["DURUM"].ToString(), Convert.ToDouble(item["YUZDE"].ToString().Replace("%", ""))));
            }
            //srsxrChart2.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            #region Series Label
            srsxrChart2.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsxrChart2.Label).Position = BarSeriesLabelPosition.Top;
            srsxrChart2.Label.Border.Color = Color.Transparent;
            srsxrChart2.Label.BackColor = Color.Transparent;
            srsxrChart2.Label.TextColor = Color.DarkBlue;
            #endregion

            xrChart2.Series.Add(srsxrChart2);
            //srsxrChart3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsxrChart3.Label.TextPattern = "{A}: {VP:p2}";
            xrChart3.Series.Add(srsxrChart3);



            xrChart4.Series.Clear();
            Series srsxrChart4 = new Series("BAŞARI YÜZDELİK GÖSTERİM", ViewType.Pie);
            foreach (DataRow item in ds.Tables[2].Rows)
            {
                srsxrChart4.Points.Add(new SeriesPoint("BAŞARILI", Convert.ToDouble(item["BASARILIYUZDE"].ToString().Replace("%", ""))));
                srsxrChart4.Points.Add(new SeriesPoint("BAŞARISIZ", Convert.ToDouble(item["BASARISIZYUZDE"].ToString().Replace("%", ""))));
            }
            srsxrChart4.Label.TextPattern = "{A}: {VP:p2}";
            xrChart4.Series.Add(srsxrChart4);
        }
    }
}
