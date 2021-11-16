using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class SinavKonuAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string OGRENCIDONEM { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }
        public string GRUPLAMATURU { get; set; }

        DataSet ds = new DataSet();
        DataTable TblDersList = new DataTable();
        DataTable TblVeri = new DataTable();
        DataTable TblOgrSay = new DataTable();
        DataTable TblSinavList = new DataTable();

        float LY = 0;
        float LX = 0;
        float lblEn = 163.63F;
        float lblBoy = 50;

        FontFamily ff = new FontFamily("Tahoma");
        #endregion
        public SinavKonuAnalizi(string tc, string oturum, string ogrenciDonem, string idSinavList, string idSubeList, string idSinifList, string idDersList, string GRUPLAMATURU)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            OGRENCIDONEM = ogrenciDonem;
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_SINIFs = idSinifList == "[0]" ? "[]" : idSinifList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;
            this.GRUPLAMATURU = GRUPLAMATURU;

            InitializeComponent();
        }

        private void SinavKonuAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@OGRENCIDONEM", OGRENCIDONEM);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@GRUPLAMATURU", GRUPLAMATURU);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1109);

                ds = b.SorguGetir("sp_SinavKonuAnalizi");

                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblVeri = ds.Tables[0];
                    TblDersList = ds.Tables[1];
                    TblSinavList = ds.Tables[2];
                    Baslik();
                    Icerik();

                    skaSinav sinav = new skaSinav(TblSinavList);
                    srSinav.ReportSource = sinav;
                }
            }
        }

        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;

            DataView dw = TblDersList.Select().CopyToDataTable().DefaultView.ToTable(true, "DERSAD").DefaultView;
            dw.Sort = "DERSAD";
            DataTable sortedDersList = dw.ToTable();

            xrPnl_Header.Controls.Add(lblEkle("SIRA", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            xrPnl_Header.Controls.Add(lblEkle("ÜNİTE ADI", LX, LY, (GRUPLAMATURU == "1" ? lblEn * 2 : GRUPLAMATURU == "2" ? lblEn * 3 : lblEn * 6), lblBoy, backColor, foreColor, borderColor));

            if (GRUPLAMATURU == "1" || GRUPLAMATURU == "2")
            {
                xrPnl_Header.Controls.Add(lblEkle("KONU ADI", LX, LY, (GRUPLAMATURU == "1" ? lblEn * 2 : GRUPLAMATURU == "2" ? lblEn * 3 : lblEn * 6), lblBoy, backColor, foreColor, borderColor));
            }
            if (GRUPLAMATURU == "1")
            {
                xrPnl_Header.Controls.Add(lblEkle("ALTKONU (KAZANIM)", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            }

            xrPnl_Header.Controls.Add(lblEkle("SORU SAYISI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            xrPnl_Header.Controls.Add(lblEkle("DOĞRU YÜZDESİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            xrPnl_Header.Controls.Add(lblEkle("YANLIŞ YÜZDESİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            xrPnl_Header.Controls.Add(lblEkle("BOŞ YÜZDESİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            LY += lblBoy;
            LX = 0;
        }

        public void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;

            DataView dv = TblVeri.DefaultView;
            dv.Sort = "DERSAD,KOD";
            DataTable sortedDT = dv.ToTable();

            DataView dw = TblDersList.Select().CopyToDataTable().DefaultView.ToTable(true, "DERSAD").DefaultView;
            dw.Sort = "DERSAD";
            DataTable sortedDersList = dw.ToTable();

            foreach (DataRow ders in sortedDersList.Rows)
            {
                int sira = 1;

                LX = 0;
                Detail.Controls.Add(lblEkle(ders["DERSAD"].ToString(), LX, LY, lblEn * 11, lblBoy, Color.SkyBlue, Color.MidnightBlue, Color.White));
                LY += lblBoy;

                foreach (DataRow veri in sortedDT.Select(String.Format("DERSAD='{0}'", ders["DERSAD"].ToString())))
                {
                    LX = 0;
                    decimal dogruYuzde = Convert.ToDecimal(veri["DOGRUYUZDESI"].ToString());
                    Color clr = Color.FromArgb(248, 105, 107); // kırmızı
                    if (dogruYuzde == 0) { clr = Color.FromArgb(248, 105, 107); }
                    else if (dogruYuzde < 11) { clr = Color.FromArgb(249, 131, 112); }
                    else if (dogruYuzde < 21) { clr = Color.FromArgb(250, 157, 117); }
                    else if (dogruYuzde < 31) { clr = Color.FromArgb(252, 183, 122); }
                    else if (dogruYuzde < 41) { clr = Color.FromArgb(253, 209, 127); }
                    else if (dogruYuzde < 51) { clr = Color.FromArgb(255, 235, 132); }
                    else if (dogruYuzde < 61) { clr = Color.FromArgb(224, 227, 131); }
                    else if (dogruYuzde < 71) { clr = Color.FromArgb(193, 218, 129); }
                    else if (dogruYuzde < 81) { clr = Color.FromArgb(162, 208, 127); }
                    else if (dogruYuzde < 91) { clr = Color.FromArgb(131, 199, 125); }
                    else if (dogruYuzde < 101) { clr = Color.FromArgb(99, 190, 123); }

                    Detail.Controls.Add(lblEkle(sira.ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["UNITE"].ToString(), LX, LY, (GRUPLAMATURU == "1" ? lblEn * 2 : GRUPLAMATURU == "2" ? lblEn * 3 : lblEn * 6), lblBoy, backColor, foreColor, borderColor));

                    if (GRUPLAMATURU == "1" || GRUPLAMATURU == "2")
                    {
                        Detail.Controls.Add(lblEkle(veri["KONU"].ToString(), LX, LY, (GRUPLAMATURU == "1" ? lblEn * 2 : GRUPLAMATURU == "2" ? lblEn * 3 : lblEn * 6), lblBoy, backColor, foreColor, borderColor));
                    }

                    if (GRUPLAMATURU == "1")
                    {
                        Detail.Controls.Add(lblEkle(veri["KAZANIM"].ToString(), LX, LY, (GRUPLAMATURU == "1" ? lblEn * 2 : GRUPLAMATURU == "2" ? lblEn * 3 : lblEn * 6), lblBoy, backColor, foreColor, borderColor));
                    }

                    Detail.Controls.Add(lblEkle(veri["SORUSAYISI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["DOGRUYUZDESI"].ToString(), LX, LY, lblEn, lblBoy, clr, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["YANLISYUZDESI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["BOSYUZDESI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                    LY += lblBoy;
                    sira++;
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
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                KeepTogether = true
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }
    }
}
