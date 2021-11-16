using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PusulamRapor.Yazili
{
    public partial class KazanimAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string ID_SINAVLAR { get; set; }
        public string ID_SINIFLAR { get; set; }

        DataSet ds;

        DataTable T1;
        DataTable T2;

        DataTable datalar = new DataTable();

        public KazanimAnalizi(string TCKIMLIKNO, string OTURUM, string ID_SINAVLAR, string ID_SINIFLAR, string DONEM)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_SINAVLAR = ID_SINAVLAR;
            this.ID_SINIFLAR = ID_SINIFLAR;
            this.DONEM = DONEM;
            InitializeComponent();
        }

        private void KazanimAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SINAVLAR", ID_SINAVLAR);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_MENU", 1096);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                ds = b.SorguGetir("sp_KazanimAnalizi");

                T1 = ds.Tables[0];
                T2 = ds.Tables[1];

                Baslik();
                Icerik();

                //this.DataSource = datalar;
                //FillReportDataFields.Fill(Detail, datalar);

            }
        }

        float LY = 0;
        float LX = 0;
        FontFamily ff = new FontFamily("Tahoma");
        float lblEn = 200;
        float lblBoy = 40;
        DataTable dtsube;
        DataTable dtsinif;
        DataTable dtsinav;
        DataTable dtkod;

        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;


            DataView DV = T2.DefaultView.ToTable(true, "SUBE").DefaultView;
            DV.Sort = "SUBE";
            dtsube = DV.ToTable();


            DataView DV2 = T2.DefaultView.ToTable(true, "SUBE", "SINIF", "SINAVAD", "YAZILITARIH").DefaultView;
            DV2.Sort = "SUBE, SINIF, YAZILITARIH";
            dtsinif = DV2.ToTable();


            DataView DV3 = T2.DefaultView.ToTable(true, "SINAVAD", "YAZILITARIH").DefaultView;
            DV3.Sort = "YAZILITARIH desc";
            dtsinav = DV3.ToTable();


            DataView DV4 = T2.DefaultView.ToTable(true, "KOD").DefaultView;
            DV4.Sort = "KOD";
            dtkod = DV4.ToTable();


            ReportHeader.Controls.Add(lblEkle("DERS KAZANIMKODU", LX, LY, lblEn, lblBoy * 3, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("KAZANIM", LX, LY, lblEn, lblBoy * 3, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SORU SAYISI", LX, LY, lblEn, lblBoy * 3, backColor, foreColor, borderColor));


            foreach (DataRow SUBE in dtsube.Rows)
            {
                int SUBESAY = T1.Select("SUBE='" + SUBE["SUBE"] + "'").Count() + (T1.Select("SUBE='" + SUBE["SUBE"] + "'").CopyToDataTable().DefaultView.ToTable(true, "SINAVAD").Rows.Count * 2);
                ReportHeader.Controls.Add(lblEkle(SUBE["SUBE"].ToString(), LX, LY, lblEn * SUBESAY, lblBoy, backColor, foreColor, borderColor));
            }

            LY += lblBoy;
            LX = lblEn * 3;

            foreach (DataRow SUBE in dtsube.Rows)
            {
                foreach (DataRow SINAV in dtsinav.Rows)
                {
                    int SINAVSAY = T1.Select("SUBE='" + SUBE["SUBE"] + "' AND SINAVAD='" + SINAV["SINAVAD"] + "'").Count() + 2;
                    if (SINAVSAY > 2)
                    {
                        ReportHeader.Controls.Add(lblEkle(SINAV["SINAVAD"].ToString(), LX, LY, lblEn * SINAVSAY, lblBoy, backColor, foreColor, borderColor));
                    }
                }
            }

            LY += lblBoy;
            LX = lblEn * 3;

            foreach (DataRow SUBE in dtsube.Rows)
            {
                foreach (DataRow SINAV in dtsinav.Rows)
                {
                    bool cont = false;
                    foreach (DataRow SINIF in dtsinif.Select("SUBE='" + SUBE["SUBE"] + "' AND SINAVAD='" + SINAV["SINAVAD"] + "'"))
                    {
                        ReportHeader.Controls.Add(lblEkle(SINIF["SINIF"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                        cont = true;
                    }
                    if (cont)
                    {
                        ReportHeader.Controls.Add(lblEkle("KAMPÜS ORTALAMA", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                        ReportHeader.Controls.Add(lblEkle("GENEL ORTALAMA", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                    }
                }
            }
        }

        public void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LX = 0;
            LY = 0;


            DataView DV4 = T2.DefaultView.ToTable("KOD").DefaultView;
            DV4.Sort = "KOD";
            DataTable dtVERI = DV4.ToTable();

            foreach (DataRow kod in dtkod.Rows)
            {
                DataTable dtDISTINCT = dtVERI.Select("KOD='" + kod["KOD"] + "'").CopyToDataTable().DefaultView.ToTable(true, "KOD", "DERSAD", "KAZANIM", "SORUSAYISI");
                Detail.Controls.Add(lblEkle(dtDISTINCT.Rows[0]["DERSAD"].ToString() + " " + dtDISTINCT.Rows[0]["KOD"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dtDISTINCT.Rows[0]["KAZANIM"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle(dtDISTINCT.Rows[0]["SORUSAYISI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

                foreach (DataRow SUBE in dtsube.Rows)
                {
                    foreach (DataRow SINAV in dtsinav.Rows)
                    {
                        string query = "KOD='" + kod["KOD"] + "' AND SUBE = '" + SUBE["SUBE"] + "' AND SINAVAD = '" + SINAV["SINAVAD"] + "'";
                        try
                        {
                            DataTable dtORT = dtVERI.Select(query).CopyToDataTable().DefaultView.ToTable(true, "GENELYUZDE", "SUBEYUZDE");
                            foreach (DataRow SINIF in dtsinif.Select("SUBE='" + SUBE["SUBE"] + "' AND SINAVAD='" + SINAV["SINAVAD"] + "'"))
                            {
                                DataTable dtDISTINCTX = dtVERI.Select("KOD='" + kod["KOD"] + "' AND SUBE = '" + SUBE["SUBE"] + "' AND SINAVAD = '" + SINAV["SINAVAD"] + "' AND SINIF = '" + SINIF["SINIF"] + "'").CopyToDataTable().DefaultView.ToTable(true, "SINIFYUZDE");
                                Detail.Controls.Add(lblEkle(Convert.ToDecimal(dtDISTINCTX.Rows[0]["SINIFYUZDE"]).ToString("N2") + "%", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                            }
                            Detail.Controls.Add(lblEkle(Convert.ToDecimal(dtORT.Rows[0]["SUBEYUZDE"]).ToString("N2") + "%", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                            Detail.Controls.Add(lblEkle(Convert.ToDecimal(dtORT.Rows[0]["GENELYUZDE"]).ToString("N2") + "%", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                        }
                        catch (Exception)
                        {
                            bool cont = false;
                            foreach (DataRow SINIF in dtsinif.Select("SUBE='" + SUBE["SUBE"] + "' AND SINAVAD='" + SINAV["SINAVAD"] + "'"))
                            {
                                // DataTable dtDISTINCTX = dtVERI.Select("KOD='" + kod["KOD"] + "' AND SUBE = '" + SUBE["SUBE"] + "' AND SINAVAD = '" + SINAV["SINAVAD"] + "' AND SINIF = '" + SINIF["SINIF"] + "'").CopyToDataTable().DefaultView.ToTable(true, "SINIFYUZDE");
                                Detail.Controls.Add(lblEkle("-", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                                cont = true;
                            }
                            if (cont)
                            {
                                Detail.Controls.Add(lblEkle("-", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                                Detail.Controls.Add(lblEkle("-", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                            }
                        }
                    }
                }

                LY += lblBoy;
                LX = 0;
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
