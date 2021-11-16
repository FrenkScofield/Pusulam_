using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class TopluYaziliYoklamaSonuclari : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string SINAVLAR { get; set; }
        public string SINIFLAR { get; set; }
        public string SUBELER { get; set; }
        public string DERSLER { get; set; }
        public int ID_KADEME3 { get; set; }
        public int YARIYIL { get; set; }
        public string DONEM { get; set; }
        public TopluYaziliYoklamaSonuclari(string _TCKIMLIKNO, string _OTURUM, string _SINAVLAR, string _SINIFLAR, string _SUBELER, string _DERSLER, string _ID_KADEME3, string _YARIYIL, string _DONEM)
        {
            TCKIMLIKNO = _TCKIMLIKNO;
            OTURUM = _OTURUM;
            SINAVLAR = _SINAVLAR;
            SINIFLAR = _SINIFLAR;
            SUBELER = _SUBELER;
            DERSLER = _DERSLER;
            ID_KADEME3 = Convert.ToInt32(_ID_KADEME3);
            YARIYIL = Convert.ToInt32(_YARIYIL);
            DONEM = _DONEM;
            InitializeComponent();
        }

        float LY = 0;
        float LX = 0;
        FontFamily ff = new FontFamily("Tahoma");
        float lblEn = 200;
        float lblBoy = 40;
        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dtOgrenci = new DataTable();
        private void TopluYaziliYoklamaSonuclari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAVLAR", SINAVLAR);
                b.ParametreEkle("@ID_SINIFLAR", SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", SUBELER);
                b.ParametreEkle("@ID_DERSLER", DERSLER);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@YARIYIL", YARIYIL);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 6); //2 eski
                b.ParametreEkle("@ID_MENU", 1091);

                ds = b.SorguGetir("sp_TopluYaziliYoklamaSonuclari");
                dt1 = ds.Tables[0];
                Baslik();
                Icerik();

                this.DataSource = dt1;
                FillReportDataFields.Fill(Detail, dt1);
                int a = 0;
            }
        }
        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;

            ReportHeader.Controls.Add(lblEkle("KAMPUS", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SINIF", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("TCKIMLIKNO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("ADSOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            foreach (DataColumn column in dt1.Columns)
            {
                if (column.ColumnName != "KAMPUS" && column.ColumnName != "SINIF" && column.ColumnName != "TCKIMLIKNO" && column.ColumnName != "ADSOYAD")
                {
                    ReportHeader.Controls.Add(lblEkle(column.ColumnName, LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                }
            }
        }

        int sira = 1;
        public void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;

            Detail.Controls.Add(lblEkle("KAMPUS", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("SINIF", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("TCKIMLIKNO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("ADSOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            foreach (DataColumn column in dt1.Columns)
            {
                if (column.ColumnName != "KAMPUS" && column.ColumnName != "SINIF" && column.ColumnName != "TCKIMLIKNO" && column.ColumnName != "ADSOYAD")
                {
                    Detail.Controls.Add(lblEkle(column.ColumnName, LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                }
            }
        }

        public XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                Tag = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }
    }
}
