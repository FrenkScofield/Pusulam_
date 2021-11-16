using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavOSRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINAVTURU { get; set; }
        public int ID_SINAV { get; set; }
        public string ID_SUBES { get; set; }
        public string ID_SINIFS { get; set; }
        public bool tcGorunsun { get; set; }
        public int siralama { get; set; }
        public bool Bursluluk { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        DataTable dt7 = new DataTable();
        DataTable dt8 = new DataTable();
        float lblEn = 0;
        float lblEn2 = 0;
        float lblEn3 = 0;
        float lblEn4 = 0;
        float lblEn5 = 0;


        float LY = 0;
        float LX = 0;
        float height = 30;
        float sayfaBoyu = 1130;
        FontFamily ff = new FontFamily("Tahoma");
        bool puanGizle = false;
        bool dersPuanGizle = false;


        float lblBoy = 0;
        #endregion
        public DenemeSinavOSRaporu(string tc, string oturum, string donem, string idKademe3, string idSinavturu, string idSinav, string idSubes, string idSinifs, string _tcGorunsun, string _siralama)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            DONEM = donem;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            ID_SINAVTURU = Convert.ToInt32(idSinavturu);
            ID_SINAV = Convert.ToInt32(idSinav);
            ID_SUBES = idSubes;
            ID_SINIFS = idSinifs;
            tcGorunsun = Convert.ToBoolean(_tcGorunsun);
            siralama = Convert.ToInt32(_siralama);

            Bursluluk = ID_SINAVTURU == 12 ? true : false;

            InitializeComponent();
        }
        private void DenemeSinavOSRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            //TCKIMLIKNO="32051057542";
            //OTURUM="F93FBC75-79CC-47CC-8755-3E233E2EAF60";
            //DONEM="2017"; 
            //ID_SINIFS="[101677]";
            //ID_SUBES="[11]";
            //ID_KADEME3=18;
            //ID_SINAV=179;

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                b.ParametreEkle("@ID_SINAV", ID_SINAV);
                b.ParametreEkle("@ID_SUBEs", ID_SUBES);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFS);
                b.ParametreEkle("@ISLEM", 3); // Rapor
                b.ParametreEkle("@ID_MENU", 1043);


                ds = b.SorguGetir("sp_GenelSinavRaporu");


                if (ds.Tables[0].Rows.Count > 0)
                {
                    //this.DataSource=ds.Tables[0];
                    dt1 = ds.Tables[0];  // öğrenci td
                    dt2 = ds.Tables[1];  // öğr ders list
                    dt3 = ds.Tables[2];  // öğr ders net
                    dt4 = ds.Tables[3];  // öğr puan tür
                    dt5 = ds.Tables[4];  // öğr puan list
                    dt6 = ds.Tables[5];  // baslik
                    dt7 = ds.Tables[6];  // ort
                    dt8 = ds.Tables[7];  // ort

                    puanGizle = Convert.ToBoolean(dt1.Rows[0]["PUANGIZLE"]);
                    puanGizle = ID_SINAVTURU == 12
                        ? false
                        : puanGizle;

                    dersPuanGizle = !Convert.ToBoolean(dt1.Rows[0]["OLCEKSINAVI"]);

                    if (puanGizle == true)
                    {
                        lblEn = sayfaBoyu / (dt2.Rows.Count + 3);
                        lblEn = lblEn < 250 ? 250 : lblEn;
                        sayfaBoyu = (dt2.Rows.Count + 3) * lblEn;
                    }
                    else
                    {
                        lblEn = sayfaBoyu / (dt2.Rows.Count + 3 + dt4.Rows.Count);
                        lblEn = lblEn < 250 ? 250 : lblEn;
                        sayfaBoyu = (dt2.Rows.Count + 3 + dt4.Rows.Count) * lblEn;
                    }

                    Baslik();
                    Icerik();

                }
            }
        }
        public void Baslik()
        {

            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;

            DataView dv = dt2.DefaultView;
            dv.Sort = "BOLUMNO";
            DataTable dv2 = dv.ToTable();

            lblBoy = height;

            #region ustBaslik
            LX = 0;

            ReportHeader.Controls.Add(lblEkle("SINAV ADI", LX, LY, lblEn + (tcGorunsun == true ? lblEn : 0), lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(dt6.Rows[0]["SINAVAD"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(dt6.Rows[0]["SINAVTURU"].ToString().ToUpper() + " SINAV LİSTESİ", LX, LY, sayfaBoyu - (lblEn * 2) + lblEn, lblBoy, backColor, foreColor, borderColor));

            if (Bursluluk) // bursluluk
            {
                ReportHeader.Controls.Add(lblEkle("BURS ÖĞRENCİ BİLGİLERİ", LX, LY, lblEn * 6, lblBoy * 2, backColor, foreColor, borderColor));
            }

            LY += height;

            #endregion

            #region dersAdi
            LX = 0;
            // tarih

            ReportHeader.Controls.Add(lblEkle("SINAV TARİHİ", LX, LY, lblEn + (tcGorunsun == true ? lblEn : 0), lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle(dt6.Rows[0]["SINAVTARIH"].ToString(), LX, LY, lblEn * 2, lblBoy, backColor, foreColor, borderColor));

            foreach (DataRow ders in dv2.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(ders["DERSAD"].ToString() + Environment.NewLine + " Soru Sayısı:" + ders["SORUSAYISI"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }

            ReportHeader.Controls.Add(lblEkle("TOPLAM" + Environment.NewLine + "Soru Sayısı:" + dt1.Rows[0]["TS"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("TOPLAM NET SIRALAMA" + Environment.NewLine + "Soru Sayısı:" + dt1.Rows[0]["TS"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            if (puanGizle == false)
            {
                foreach (DataRow pt in dt4.Rows)
                {
                    ReportHeader.Controls.Add(lblEkle(pt["SINAVPUANTURU"].ToString(), LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                }
            }



            LY += height;

            #endregion

            #region DYBN

            LX = 0;

            ReportHeader.Controls.Add(lblEkle("OKUL", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("SINIF", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            if (tcGorunsun)
            {
                ReportHeader.Controls.Add(lblEkle("TCKIMLIKNO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            ReportHeader.Controls.Add(lblEkle("AD SOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));

            foreach (DataRow ders in dv2.Rows)
            {
                lblEn2 = dersPuanGizle == false ? lblEn / 6 : lblEn / 5;

                ReportHeader.Controls.Add(lblEkle("D", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("Y", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("B", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("N", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("B %", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));

                if (!dersPuanGizle)
                {
                    ReportHeader.Controls.Add(lblEkle("P", LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                }
            }

            /////////////////// toplam
            lblEn5 = lblEn / 5;

            ReportHeader.Controls.Add(lblEkle("D", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("Y", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("B", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("N", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("B %", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));

            ////////////////
            lblEn3 = lblEn / 5;

            ReportHeader.Controls.Add(lblEkle("Sınıf", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("Okul", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("İlçe", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("İl", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            ReportHeader.Controls.Add(lblEkle("Genel", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));

            lblEn4 = lblEn / 6;
            if (!puanGizle)
            {
                foreach (DataRow pt in dt4.Rows)
                {
                    ReportHeader.Controls.Add(lblEkle("PUAN", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                    ReportHeader.Controls.Add(lblEkle("Sınıf", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                    ReportHeader.Controls.Add(lblEkle("Okul", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                    ReportHeader.Controls.Add(lblEkle("İlçe", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                    ReportHeader.Controls.Add(lblEkle("İl", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                    ReportHeader.Controls.Add(lblEkle("Genel", LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                }
            }


            if (Bursluluk) // bursluluk
            {

                ReportHeader.Controls.Add(lblEkle("BURS ORANI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("OKULU", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("VELİ ADI SOYADI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("TELEFON (EV)", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("TELEFON (CEP)", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                ReportHeader.Controls.Add(lblEkle("MAIL", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }


            #endregion

        }
        public void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;

            DataView dv = dt2.DefaultView;
            dv.Sort = "BOLUMNO";
            DataTable dv2 = dv.ToTable();

            DataTable sirali = new DataTable();

            dv = dt8.DefaultView;
            if (siralama == 0)
            {
                dv.Sort = "TN desc";
                sirali = dv.ToTable();
            }
            else
            {
                dv.Sort = "PT" + siralama + " desc";
                sirali = dv.ToTable();
            }


            #region row
            LX = 0;
            LY = 0;
            height = 30;

            lblEn2 = dersPuanGizle == false ? lblEn / 6 : lblEn / 5;


            Detail.Controls.Add(lblEkle("SUBEAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("SINIFAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            if (tcGorunsun)
            {
                Detail.Controls.Add(lblEkle("TCKIMLIKNO", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }
            Detail.Controls.Add(lblEkle("ADSOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            foreach (DataRow dr in dv2.Rows)
            {
                Detail.Controls.Add(lblEkle("D" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("Y" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("B" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("N" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));

                XRLabel lblDers = lblEkle("YN" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor, "YN" + dr["BOLUMNO"]);
                Detail.Controls.Add(lblDers);
                if (Bursluluk) // bursluluk
                {
                    lblDers.BeforePrint += LblDers_BeforePrint;
                }

                if (!dersPuanGizle)
                    Detail.Controls.Add(lblEkle("DP" + dr["BOLUMNO"], LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
            }

            Detail.Controls.Add(lblEkle("TD", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("TY", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("TB", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("TN", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("YN", LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));

            Detail.Controls.Add(lblEkle("SINIFSIRA", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("OKULSIRA", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("ILCESIRA", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("ILSIRA", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));
            Detail.Controls.Add(lblEkle("GENELSIRA", LX, LY, lblEn3, lblBoy, backColor, foreColor, borderColor));

            foreach (DataRow dr in dt4.Rows)
            {
                XRLabel l = lblEkle("PT" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor);
                Detail.Controls.Add(l);
                if (Bursluluk) // bursluluk
                {
                    l.BeforePrint += L_BeforePrint;
                }


                Detail.Controls.Add(lblEkle("SS" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("OS" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("CS" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("IL" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("GS" + dr["ID_SINAVPUANTURU"], LX, LY, lblEn4, lblBoy, backColor, foreColor, borderColor));
            }

            if (Bursluluk) // bursluluk
            {
                Detail.Controls.Add(lblEkle("BURSORANI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("OKULADI", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("VELI_ADSOYAD", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("TELEFON_EV", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("TELEFON_CEP", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                Detail.Controls.Add(lblEkle("MAIL", LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
            }

            this.DataSource = sirali;
            FillReportDataFields.Fill(Detail, sirali);

            #endregion

            #region Ort 
            LX = 0;

            backColor = Color.Red;
            foreColor = Color.White;
            borderColor = Color.White;

            ReportFooter.Controls.Add(lblEkle("ORTALAMALAR", LX, LY, (lblEn * 3) + (tcGorunsun == true ? lblEn : 0), lblBoy, backColor, foreColor, borderColor));

            lblEn2 = dersPuanGizle == false ? lblEn / 6 : lblEn / 5;
            foreach (DataRow ders in dv2.Rows)
            {
                foreach (DataRow veri in dt7.Select(String.Format("DERSAD='{0}'", ders[0].ToString())).CopyToDataTable().Rows)
                {

                    ReportFooter.Controls.Add(lblEkle(veri["DOGRUORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    ReportFooter.Controls.Add(lblEkle(veri["YANLISORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    ReportFooter.Controls.Add(lblEkle(veri["BOSORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    ReportFooter.Controls.Add(lblEkle(veri["NETORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    ReportFooter.Controls.Add(lblEkle(veri["YUZDENETORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    if (!dersPuanGizle)
                    {
                        ReportFooter.Controls.Add(lblEkle(veri["PUANORT"].ToString(), LX, LY, lblEn2, lblBoy, backColor, foreColor, borderColor));
                    }
                }
            }

            DataRow drToplam = dt7.Select("DERSAD='TOPLAM'").CopyToDataTable().Rows[0];

            // td 
            ReportFooter.Controls.Add(lblEkle(drToplam["DOGRUORT"].ToString(), LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(drToplam["YANLISORT"].ToString(), LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(drToplam["BOSORT"].ToString(), LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(drToplam["NETORT"].ToString(), LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            ReportFooter.Controls.Add(lblEkle(drToplam["YUZDENETORT"].ToString(), LX, LY, lblEn5, lblBoy, backColor, foreColor, borderColor));
            #endregion

        }

        private void LblDers_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int BURSTUR = GetCurrentColumnValue("BURSTUR") == null ? 0 : Convert.ToInt32(GetCurrentColumnValue("BURSTUR"));
            int BURS_BOLUMNO = GetCurrentColumnValue("BURS_BOLUMNO") == null ? 0 : Convert.ToInt32(GetCurrentColumnValue("BURS_BOLUMNO"));
            XRLabel x = ((XRLabel)sender);
            if (BURSTUR == 2 && x.Name == "YN" + BURS_BOLUMNO)
            {
                x.BackColor = Color.Red;
                x.ForeColor = Color.White;
            }
            else
            {
                x.BackColor = Color.White;
                x.ForeColor = Color.MidnightBlue;
            }
        }

        private void L_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int BURSTUR = GetCurrentColumnValue("BURSTUR") == null ? 0 : Convert.ToInt32(GetCurrentColumnValue("BURSTUR"));
            XRLabel x = ((XRLabel)sender);
            if (BURSTUR == 1)
            {
                x.BackColor = Color.Red;
                x.ForeColor = Color.White;
            }
            else
            {
                x.BackColor = Color.White;
                x.ForeColor = Color.MidnightBlue;
            }
        }


        public XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor, string _name = "")
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
                Tag = 1,
                Name = _name
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }

    }
}
