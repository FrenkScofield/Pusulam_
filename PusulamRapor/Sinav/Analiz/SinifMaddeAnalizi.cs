using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class SinifMaddeAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string OGRENCIDONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }

        DataSet ds = new DataSet();
        DataTable TblSinif = new DataTable();
        DataTable TblVeri = new DataTable();
        DataTable TblOgrSay = new DataTable();
        DataTable TblOrtalama = new DataTable();
        float artim = 0;

        float LY = 0;
        float LX = 0;
        float lblEn = 200;
        float lblBoy = 40;

        float height = 30;
        float sayfaBoyu = 1130;
        FontFamily ff = new FontFamily("Tahoma");
        int islem = 1;
        #endregion
        public SinifMaddeAnalizi(string tc, string oturum, string donem,string idKademe3,string idSinavList, string idSubeList, string idDersList)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            OGRENCIDONEM = donem;
            ID_KADEME3 = Convert.ToInt32( idKademe3);
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;


            InitializeComponent();
        }

        private void SinifMaddeAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            using (Baglanti b = new Baglanti())
            {


                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@OGRENCIDONEM", OGRENCIDONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@ISLEM", 1); // Rapor
                b.ParametreEkle("@ID_MENU", 1108);


                ds = b.SorguGetir("sp_SinifMaddeAnalizi");


                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblSinif = ds.Tables[0];  // sube/sınıf list
                    TblVeri = ds.Tables[1];  // veri
                    TblOgrSay = ds.Tables[2];  // ogrsay
                    TblOrtalama = ds.Tables[3];  // ortalama

                    artim = 160;// sayfaBoyu/(dt1.Rows.Count+3);

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

            ReportHeader.Controls.Add(lblEkle("SINAV ADI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblVeri.Rows[0]["SINAVAD"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow dr in TblSinif.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(dr["SUBEAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            LY += lblBoy;
            LX = 0;

            ReportHeader.Controls.Add(lblEkle("SINAV TARİHİ", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblVeri.Rows[0]["SINAVTARIH"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow dr in TblSinif.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(dr["SINIFAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            LY += lblBoy;
            LX = 0;


            height = 40;


            ReportHeader.Controls.Add(lblEkle("DERS ADI", LX, LY, lblEn, height, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SORU NO", LX, LY, lblEn, height, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("KAZANIM", LX, LY, lblEn, height, backColor, foreColor, borderColor));


            foreach (DataRow dr in TblSinif.Rows)
            {
                DataRow d = ds.Tables[2].Select("ID_SINIF=" + dr["ID_SINIF"]).CopyToDataTable().Rows[0];
                ReportHeader.Controls.Add(lblEkle("Ö.S." + d["OGRSAY"].ToString(), LX, LY, lblEn, height, backColor, foreColor, borderColor));
            }
        }
        public void Icerik()
        {

            height = 30;
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;
            DataTable TABLE = TblVeri.Select("ID_DERS<>0").CopyToDataTable();

            DataView dv = TblVeri.Select("ID_DERS<>0").CopyToDataTable().DefaultView;
            dv.Sort = "BOLUMNO,SORUNO_A";
            DataTable sortedDT = dv.ToTable();

            foreach (DataRow veri in sortedDT.Rows)
            {

                LX = 0;




                if (veri["SORUNO_A"].ToString() == "0")
                {
                    Detail.Controls.Add(lblEkle(veri["DERSAD"].ToString(), LX, LY, lblEn, lblBoy, Color.Yellow, Color.Black, borderColor));
                    Detail.Controls.Add(lblEkle("SINIF ORTALAMASI", LX, LY, lblEn * 2, lblBoy, Color.Yellow, Color.Black, borderColor));
                }
                else
                {
                    Detail.Controls.Add(lblEkle(veri["DERSAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["SORUNO_A"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    Detail.Controls.Add(lblEkle(veri["KAZANIM"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                }


                foreach (DataRow dr in TblSinif.Rows)
                {

                    decimal dogruYuzde = Convert.ToDecimal(veri[dr["COL"].ToString()].ToString().Replace("-", "-1").Replace(".", ","));
                    Color clr = Color.FromArgb(248, 105, 107); // kırmmızı
                    if (dogruYuzde == -1) { clr = Color.White; }
                    else if (dogruYuzde == 0) { Color.FromArgb(248, 105, 107); }
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

                    if (veri["SORUNO_A"].ToString() == "0") clr = Color.Yellow;

                    //if (veri["SORUNO_A"].ToString() == "0")
                    //{
                    //    Detail.Controls.Add(lblEkle(veri[dr["COL"].ToString()].ToString().Replace(".", ","), LX, LY, lblEn, lblBoy, Color.Yellow, Color.Black, borderColor));
                    //}
                    //else
                    //{
                    Detail.Controls.Add(lblEkle(veri[dr["COL"].ToString()].ToString().Replace(".", ","), LX, LY, lblEn, lblBoy, clr, foreColor, borderColor));
                    //}
                }
                LY += lblBoy;
            }
        }
        public void Footer()
        {
            height = 30;
            Color backColor = Color.Yellow;
            Color foreColor = Color.Black;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;

            DataTable dv = TblOrtalama.Select().CopyToDataTable().DefaultView.ToTable(true, "DERSAD");


            foreach (DataRow dr in dv.Rows) // dersler
            {
                LX = 0;

                ReportFooter.Controls.Add(lblEkle(dr["DERSAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportFooter.Controls.Add(lblEkle("OKUL ORTALAMASI", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));

                foreach (DataRow sube in TblSinif.Select().CopyToDataTable().DefaultView.ToTable(true, "SUBEAD").Rows) // 
                {
                    foreach (DataRow veri in TblOrtalama.Select(String.Format("DERSAD='{0}' AND SUBEAD='{1}'", dr["DERSAD"].ToString(), sube["SUBEAD"].ToString())))
                    {
                        int sinifSayisi = TblSinif.Select(String.Format("SUBEAD='{0}'", sube["SUBEAD"].ToString())).Length;
                        ReportFooter.Controls.Add(lblEkle(veri["YUZDE"].ToString(), LX, LY, lblEn * sinifSayisi, lblBoy, backColor, foreColor, borderColor));
                    }
                }
                LY += lblBoy;
            }


            foreach (DataRow dr in dv.Rows) // dersler
            {
                LX = 0;

                ReportFooter.Controls.Add(lblEkle(dr["DERSAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportFooter.Controls.Add(lblEkle("GENEL ORTALAMASI", LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));

                foreach (DataRow veri in TblOrtalama.Select(String.Format("DERSAD='{0}' AND SUBEAD=''", dr["DERSAD"].ToString())))
                {
                    ReportFooter.Controls.Add(lblEkle(veri["YUZDE"].ToString(), LX, LY, lblEn * (TblSinif.Select().Length), lblBoy, backColor, foreColor, borderColor));
                }

                LY += lblBoy;
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
