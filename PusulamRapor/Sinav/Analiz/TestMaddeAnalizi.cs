using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class TestMaddeAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string OGRENCIDONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ICDISOGRENCI { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }
        public string SINIFALANLIST { get; set; }

        DataSet ds = new DataSet();
        DataTable TblVeri = new DataTable();
        DataTable TblDersList = new DataTable();
        DataTable TblUstList = new DataTable();
        DataTable TblAltList = new DataTable();
        DataTable TblSinavOzellik = new DataTable();
        DataTable TblGrup = new DataTable();
        DataTable TblBilgiBilisselBeceri = new DataTable();

        float LY = 0;
        float LX = 0;
        float lblEn = 100;
        float lblBoy = 40;
        FontFamily ff = new FontFamily("Tahoma");
        #endregion
        public TestMaddeAnalizi(string tc, string oturum, string ogrenciDonem, string idKademe3, string idSinavList, string idSubeList, string idDersList, string sinifAlanList, string icDisOgrenci)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            OGRENCIDONEM = ogrenciDonem;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;
            SINIFALANLIST= sinifAlanList == "[0]" ? "[]" : sinifAlanList;
            ICDISOGRENCI = Convert.ToInt32(icDisOgrenci); 
            InitializeComponent();
        }

        private void TestMaddeAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@OGRENCIDONEM", OGRENCIDONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@SINIFALANLIST", SINIFALANLIST);
                b.ParametreEkle("@ICDISOGRENCI", ICDISOGRENCI);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1111);

                ds = b.SorguGetir("sp_TestMaddeAnalizi");

                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblVeri = ds.Tables[0];  // veri
                    TblUstList = ds.Tables[1];
                    TblAltList = ds.Tables[2];
                    TblSinavOzellik = ds.Tables[3];
                    TblDersList = ds.Tables[4];  // ders
                    TblGrup = ds.Tables[5];  // tablo
                    TblBilgiBilisselBeceri = ds.Tables[6];  // tablo

                    Baslik();
                    Icerik();
                    Footer();
                }
            }
        }

        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 75;

            ReportHeader.Controls.Add(lblEkle("SINAV ADI", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblSinavOzellik.Rows[0]["SINAVAD"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            LY += lblBoy;

            LX = 0;
            ReportHeader.Controls.Add(lblEkle("SINAV TARİHİ", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblSinavOzellik.Rows[0]["SINAVTARIH"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            //LY += lblBoy;

            LY = 0;
            LX = 0;
            PageHeader.Controls.Add(lblEkle("SORU NO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("ÜST - ALT GRUP", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            PageHeader.Controls.Add(lblEkle("DOĞRU CEVAP", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("DOĞRU CEVAP SAYISI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            PageHeader.Controls.Add(lblEkle("A", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("B", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("C", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("D", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("E", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("BOŞ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("ZORLUK DERECESİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("ZORLUK AÇIKLAMASI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("AYIRICILIK GÜCÜ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("AYIRICILIK AÇIKLAMASI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("VARYANS", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("BİLGİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            PageHeader.Controls.Add(lblEkle("BİLİŞSEL SÜREÇ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

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
            dv.Sort = "BOLUMNO,SORUNO_A";
            DataTable sortedDT = dv.ToTable();

            DataView dw = TblDersList.Select().CopyToDataTable().DefaultView.ToTable(true, "BOLUMNO", "DERSAD", "SINAVKATILIM").DefaultView;
            dw.Sort = "BOLUMNO";
            DataTable sortedDersList = dw.ToTable();

            foreach (DataRow ders in sortedDersList.Rows)
            {
                int sira = 1;

                LX = 0;
                Detail.Controls.Add(lblEkle(ders["DERSAD"].ToString(), LX, LY, lblEn * 14, lblBoy, Color.SkyBlue, Color.MidnightBlue, Color.White));
                Detail.Controls.Add(lblEkle("Sınava Giren Öğrenci Sayısı :" + ders["SINAVKATILIM"].ToString(), LX, LY, lblEn * 3, lblBoy, Color.SkyBlue, Color.MidnightBlue, Color.White));
                LY += lblBoy;
                
                foreach (DataRow veri in sortedDT.Select(String.Format("BOLUMNO='{0}'", ders["BOLUMNO"].ToString())))
                {
                    LX = 0;

                    Detail.Controls.Add(lblEkle(sira.ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle("ÜST GRUP", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle("ALT GRUP", LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(veri["DOGRUCEVAP"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["DOGRUSAYISI"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));

                    DataTable dtUst = TblUstList.Select(String.Format("BOLUMNO='{0}' AND SORUNO_A='{1}'", ders["BOLUMNO"].ToString(), veri["SORUNO_A"].ToString())).CopyToDataTable();
                    DataTable dtAlt = TblAltList.Select(String.Format("BOLUMNO='{0}' AND SORUNO_A='{1}'", ders["BOLUMNO"].ToString(), veri["SORUNO_A"].ToString())).CopyToDataTable();

                    string dogruCevap = veri["DOGRUCEVAP"].ToString();

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["A"].ToString(), LX, LY, lblEn, lblBoy, backColor, dogruCevap == "A" ? Color.Red : foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["A"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, dogruCevap == "A" ? Color.Red : foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["B"].ToString(), LX, LY, lblEn, lblBoy, backColor, dogruCevap == "B" ? Color.Red : foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["B"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, dogruCevap == "B" ? Color.Red : foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["C"].ToString(), LX, LY, lblEn, lblBoy, backColor, dogruCevap == "C" ? Color.Red : foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["C"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, dogruCevap == "C" ? Color.Red : foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["D"].ToString(), LX, LY, lblEn, lblBoy, backColor, dogruCevap == "D" ? Color.Red : foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["D"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, dogruCevap == "D" ? Color.Red : foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["E"].ToString(), LX, LY, lblEn, lblBoy, backColor, dogruCevap == "E" ? Color.Red : foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["E"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, dogruCevap == "E" ? Color.Red : foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(dtUst.Rows[0]["BOS"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(dtAlt.Rows[0]["BOS"].ToString(), LX - lblEn, LY + lblBoy, lblEn, lblBoy, backColor, foreColor, borderColor));

                    Detail.Controls.Add(lblEkle(veri["ZORLUKDERECESI"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["ZORLUKACIKLAMA"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["AYIRICILIK"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["AYIRICILIKACIKLAMA"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["VARYANS"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["BILGI"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["BILISSELSUREC"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));

                    LY += lblBoy * 2;
                    sira++;
                }
            }

        }
        public void Footer()
        {

            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;

            #region ZORLUK DERECESİ
            float LYY = LY;

            LX = 0;
            LY += lblBoy;

            float en = PageWidth / 6 - 10;

            ReportFooter.Controls.Add(lblEkle("ZORLUK DERECESİ", LX, LY, en * 3, lblBoy, backColor, foreColor, borderColor));
            LX = 0;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.00 - 0.19", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Çok Zor", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["ZORLUK_CZ"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            LX = 0;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.20 - 0.34", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Zor", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["ZORLUK_Z"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            LX = 0;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.35 - 0.64", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Önerilen", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["ZORLUK_O"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            LX = 0;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.65 - 0.79", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Kolay", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["ZORLUK_K"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            LX = 0;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.80 - 1.00", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Çok Kolay", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["ZORLUK_CK"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            #endregion

            #region AYIRICILIK GÜCÜ
            LY = LYY;
            LX = en * 3 + 20;
            LY += lblBoy;

            ReportFooter.Controls.Add(lblEkle("AYIRICILIK GÜCÜ", LX, LY, en * 3, lblBoy, backColor, foreColor, borderColor));

            LX = en * 3 + 20;
            LY += lblBoy;   
            ReportFooter.Controls.Add(lblEkle("0.00 - 0.19", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Zayıf", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["AYIRICILIK_Z"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));

            LX = en * 3 + 20;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.20 - 0.29", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Sınırda", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["AYIRICILIK_S"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));

            LX = en * 3 + 20;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("0.30 - 0.39", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("İyi", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["AYIRICILIK_I"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));

            LX = en * 3 + 20;
            LY += lblBoy;
            ReportFooter.Controls.Add(lblEkle("> 0.40", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle("Çok İyi", LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(TblGrup.Rows[0]["AYIRICILIK_CI"].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            #endregion

            #region BİLGİ - BİLİŞSEL SÜREÇ 
            LX = 0;
            LY += 3 * lblBoy;
            en = PageWidth / 7 - 5.1f;

            foreach (DataColumn item in TblBilgiBilisselBeceri.Columns)
            {
                ReportFooter.Controls.Add(lblEkle(item.ColumnName, LX, LY, en, lblBoy, backColor, foreColor, borderColor));
            }

            foreach (DataRow row in TblBilgiBilisselBeceri.Rows)
            {
                LX = 0;
                LY += lblBoy;
                foreach (DataColumn column in TblBilgiBilisselBeceri.Columns)
                {
                    ReportFooter.Controls.Add(lblEkle(row[column.ColumnName].ToString(), LX, LY, en, lblBoy, backColor, foreColor, borderColor));
                }                
            }
            #endregion
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
                KeepTogether = true,
                BorderWidth = 1,
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
