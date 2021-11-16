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
    public partial class OgrenciMaddeAnalizi : DevExpress.XtraReports.UI.XtraReport
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
        DataTable TblVeri = new DataTable();
        DataTable TblDersList = new DataTable();
        DataTable TblOgrenciList = new DataTable();
        DataTable TblSoruList = new DataTable();
        DataTable TblSinavOzellik = new DataTable();

        float LY = 0;
        float LX = 0;
        float lblEn = 100;
        float lblBoy = 40;
        FontFamily ff = new FontFamily("Tahoma");
        #endregion
        public OgrenciMaddeAnalizi(string tc, string oturum, string ogrenciDonem, string idSinavList, string idSubeList, string idSinifList, string idDersList)
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

        private void OgrenciMaddeAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                b.ParametreEkle("@ID_MENU", 1112);


                ds = b.SorguGetir("sp_OgrenciMaddeAnalizi");


                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblVeri = ds.Tables[0];  // veri
                    TblOgrenciList = ds.Tables[1];
                    TblSoruList = ds.Tables[2];
                    TblSinavOzellik = ds.Tables[3];

                    Baslik();
                    Icerik();
                    //Footer();
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

            DataView dv = TblSoruList.DefaultView.ToTable(true, "BOLUMNO", "DERSAD").DefaultView;
            dv.Sort = "BOLUMNO";
            DataTable sortedBolumNoList = dv.ToTable();


            DataView dv2 = TblSoruList.DefaultView;
            dv2.Sort = "BOLUMNO,SORUNO_A";
            DataTable sortedSoruList = dv2.ToTable();

            ReportHeader.Controls.Add(lblEkle("SINAV ADI", LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblSinavOzellik.Rows[0]["SINAVAD"].ToString(), LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            LY += lblBoy;

            LX = 0;
            ReportHeader.Controls.Add(lblEkle("SINAV TARİHİ", LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(TblSinavOzellik.Rows[0]["SINAVTARIH"].ToString(), LX, LY, lblEn * 3, lblBoy, backColor, foreColor, borderColor));
            LY += lblBoy;

            LX = 0;
            ReportHeader.Controls.Add(lblEkle("ŞUBE", LX, LY, lblEn, lblBoy * 4, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SINIF", LX, LY, lblEn, lblBoy * 4, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("AD SOYAD", LX, LY, lblEn, lblBoy * 4, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("TCKIMLIKNO", LX, LY, lblEn, lblBoy * 4, backColor, foreColor, borderColor));
            foreach (DataRow bolumNo in sortedBolumNoList.Rows)
            {
                int kolonSayisi = TblSoruList.Select(String.Format("BOLUMNO='{0}'", bolumNo["BOLUMNO"].ToString())).Length;
                ReportHeader.Controls.Add(lblEkle(bolumNo["DERSAD"].ToString(), LX, LY, (lblEn * kolonSayisi), lblBoy, backColor, foreColor, borderColor));
            }
            LY += lblBoy;

            LX = lblEn * 3;
            foreach (DataRow soru in sortedSoruList.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(soru["KAZANIM"].ToString(), LX, LY, lblEn, lblBoy * 2, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle(soru["SORUNO_A"].ToString(), LX - lblEn, LY + (lblBoy * 2), lblEn, lblBoy, backColor, foreColor, borderColor));
            }
        }
        public void Icerik()
        {

            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;


            DataView dv = TblSoruList.DefaultView.ToTable(true, "BOLUMNO", "DERSAD").DefaultView;
            dv.Sort = "BOLUMNO";
            DataTable sortedBolumNoList = dv.ToTable();


            DataView dv2 = TblSoruList.DefaultView;
            dv2.Sort = "BOLUMNO,SORUNO_A";
            DataTable sortedSoruList = dv2.ToTable();

            DataView dv3 = TblOgrenciList.DefaultView;
            dv3.Sort = "SUBEAD,SINIFAD,ADSOYAD";
            DataTable sortedOgrenciList = dv3.ToTable();

            foreach (DataRow ogrenci in sortedOgrenciList.Rows)
            {
                LX = 0;
                Detail.Controls.Add(lblEkle(ogrenci["SUBEAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(ogrenci["SINIFAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(ogrenci["ADSOYAD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(ogrenci["TCKIMLIKNO"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));


                foreach (DataRow soru in sortedSoruList.Rows)
                {
                    try
                    {
                        string yaz = "";
                        yaz = TblVeri.Select(String.Format("TCKIMLIKNO='{0}' AND BOLUMNO='{1}' AND SORUNO_A='{2}'", ogrenci["TCKIMLIKNO"].ToString(), soru["BOLUMNO"].ToString(), soru["SORUNO_A"].ToString())).CopyToDataTable().Rows[0]["OGRENCICEVAP"].ToString();
                        Detail.Controls.Add(lblEkle(yaz, LX, LY, lblEn, lblBoy, (yaz == "+" ? backColor : Color.Red), (yaz == "+" ? ForeColor : Color.White), borderColor));
                    }
                    catch (Exception)
                    {
                        Detail.Controls.Add(lblEkle("", LX, LY, lblEn, lblBoy, Color.White, ForeColor , borderColor));
                    }
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