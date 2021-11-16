using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class GenelSoruAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string OGRENCIDONEM { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }

        DataSet ds = new DataSet();
        DataTable TblKitapcik = new DataTable();
        DataTable TblVeri = new DataTable();
        DataTable TblOgrSay = new DataTable();
        DataTable TblOrtalama = new DataTable();

        float LY = 0;
        float LX = 0;
        float lblEn = 200;
        float lblBoy = 40;

        float height = 30;
        float sayfaBoyu = 1130;
        FontFamily ff = new FontFamily("Tahoma");
        int islem = 1;
        #endregion
        public GenelSoruAnalizi(string tc, string oturum, string ogrenciDonem, string idSinavList, string idSubeList, string idSinifList, string idDersList)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            OGRENCIDONEM = ogrenciDonem;
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_SINIFs = idSinifList == "[0]" ? "[]" : idSinifList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;


            InitializeComponent();
        }

        private void GenelSoruAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1108);


                ds = b.SorguGetir("sp_GenelSoruAnalizi");


                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblVeri = ds.Tables[0];  // veri
                    TblKitapcik = ds.Tables[1];  // sube/sınıf list

                    lblEn = 200;// sayfaBoyu/(dt1.Rows.Count+3);

                    Baslik();
                    Icerik();
                    Footer();
                    //this.DataSource=dt2.Select("ID_DERS<>0").CopyToDataTable();
                    //FillReportDataFields.Fill(Detail,dt2.Select("ID_DERS<>0").CopyToDataTable());

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


            DataView dw = TblKitapcik.Select().CopyToDataTable().DefaultView.ToTable(true, "KITAPCIK").DefaultView;
            dw.Sort = "KITAPCIK";
            DataTable sortedKitapcik = dw.ToTable();



            ReportHeader.Controls.Add(lblEkle("SINAV ADI", LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblVeri.Rows[0]["SINAVAD"].ToString(), LX, LY, lblEn * 15, lblBoy, backColor, foreColor, borderColor));

            LY += lblBoy;
            LX = 0;

            ReportHeader.Controls.Add(lblEkle("SINAV TARİHİ", LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblVeri.Rows[0]["SINAVTARIH"].ToString(), LX, LY, lblEn * 15, lblBoy, backColor, foreColor, borderColor));

            LY += lblBoy;
            LX = 0;



            ReportHeader.Controls.Add(lblEkle("SORU", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("KAZANIM ADI", LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));

            ReportHeader.Controls.Add(lblEkle("A", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("B", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("C", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("D", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("E", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("BOŞ", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("DOĞRU", LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));

            LY += lblBoy;
            LX = 0;


            ReportHeader.Controls.Add(lblEkle("A", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow kitapcik in sortedKitapcik.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(kitapcik["KITAPCIK"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            LX += lblEn;
            for (int i = 0; i < 7; i++)
            {
                ReportHeader.Controls.Add(lblEkle("Cevap Sayısı", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("Yüzde %", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            ReportHeader.Controls.Add(lblEkle("CEVAP", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

        }
        public void Icerik()
        {

            height = 30;
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;

            DataView dv = TblVeri.Select("ID_DERS<>0").CopyToDataTable().DefaultView;
            dv.Sort = "BOLUMNO,SORUNO_A";
            DataTable sortedDT = dv.ToTable();

            DataView dw = TblKitapcik.Select().CopyToDataTable().DefaultView.ToTable(true, "KITAPCIK").DefaultView;
            dw.Sort = "KITAPCIK";
            DataTable sortedKitapcik = dw.ToTable();

            foreach (DataRow veri in sortedDT.Rows)
            {

                LX = 0;

                Detail.Controls.Add(lblEkle(veri["SORUNO_A"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                foreach (DataRow kitapcik in sortedKitapcik.Rows)
                {
                    DataTable dt = TblKitapcik.Select(String.Format("BOLUMNO='{0}' AND SORUNO_A='{1}' AND KITAPCIK='{2}'", veri["BOLUMNO"].ToString(), veri["SORUNO_A"].ToString(), kitapcik["KITAPCIK"].ToString())).CopyToDataTable();
                    Detail.Controls.Add(lblEkle(dt.Rows[0]["SORUNO"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                }

                Detail.Controls.Add(lblEkle(veri["KAZANIM"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                Detail.Controls.Add(lblEkle(veri["A"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["AYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["B"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["BYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["C"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["CYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["D"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["DYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["E"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["EYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["BOS"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor)); Detail.Controls.Add(lblEkle(veri["BOSYUZDE"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                decimal dogruYuzde = Convert.ToDecimal(veri[veri["DOGRUCEVAP"].ToString() + "YUZDE"].ToString());
                Color clr = Color.FromArgb(248, 105, 107); // kırmmızı
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



                Detail.Controls.Add(lblEkle(veri[veri["DOGRUCEVAP"].ToString()].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri[veri["DOGRUCEVAP"].ToString() + "YUZDE"].ToString(), LX, LY, lblEn, lblBoy, clr, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(veri["DOGRUCEVAP"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                LY += lblBoy;
            }
        }
        public void Footer()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;

            DataView dv = TblVeri.Select().CopyToDataTable().DefaultView.ToTable(true, "DERSAD", "BOLUMNO").DefaultView;
            dv.Sort = "BOLUMNO";
            DataTable sortedDersler = dv.ToTable();

            List<string> basari = new List<string>() {
                "00",
                "10",
                "30",
                "50",
                "70",
                "90",
                "100"
            };

            foreach (DataRow ders in sortedDersler.Rows)
            {
                LY += lblBoy;
                LX = 0;
                ReportFooter.Controls.Add(lblEkle(ders["DERSAD"] + " Yüzde Başarı Dağılımı", LX, LY, lblEn * 18, lblBoy, backColor, foreColor, borderColor));
                LY += lblBoy;
                


                for (int i = 0; i < basari.Count - 1; i++)
                {
                    DataTable dt = new DataTable();
                    string yaz = "";
                    LX = 0;


                    if (TblVeri.Select(String.Format("BOLUMNO='{0}' AND (DOGRUYUZDE>={1} AND DOGRUYUZDE<={2})", ders["BOLUMNO"].ToString(), basari[i], basari[(i + 1)])).Length > 0)
                    {
                        dt = TblVeri.Select(String.Format("BOLUMNO='{0}' AND (DOGRUYUZDE>={1} AND DOGRUYUZDE<={2})", ders["BOLUMNO"].ToString(), basari[i], basari[(i + 1)])).CopyToDataTable();
                        foreach (DataRow dr in dt.Rows)
                        {
                            yaz += dr["SORUNO_A"].ToString() + ", ";
                        }
                        yaz = yaz.Substring(0, yaz.Length - 2);
                    }
                    
                    ReportFooter.Controls.Add(lblEkle(String.Format("%{0} - {1} ({2})", basari[i], basari[(i + 1)], dt.Rows.Count), LX, LY, lblEn*3, lblBoy, backColor, foreColor, borderColor));                    
                    ReportFooter.Controls.Add(lblEkle(yaz, LX, LY, lblEn*15, lblBoy, Color.White, Color.MidnightBlue, Color.SkyBlue));
                    LY += lblBoy;
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
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }
    }
}
