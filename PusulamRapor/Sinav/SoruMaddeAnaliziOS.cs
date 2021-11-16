using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class SoruMaddeAnaliziOS : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINAVTURU { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        float artim = 0;

        float LY = 0;
        float LX = 0;
        float height = 30;
        float sayfaBoyu = 1130;
        FontFamily ff = new FontFamily("Tahoma");
        int islem = 1;
        #endregion

        public SoruMaddeAnaliziOS(string tc, string oturum, string donem, string idKademe3, string idSinavturu, string idSinavList, string idSubeList, string idSinifList, string idDersList)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            DONEM = donem;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            ID_SINAVTURU = Convert.ToInt32(idSinavturu);
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_SINIFs = idSinifList == "[0]" ? "[]" : idSinifList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;

            if (idSinifList == "[]")
            {
                islem = 2; // okul
            }


            InitializeComponent();
        }
        //pusulam/Rapor.aspx?rapor=Sinav.SoruMaddeAnaliziOS&raporTur=pdf&p=32051057542;C4724139-35B6-4A9E-BB39-F9664D1DC90C;2017;18;2;[152];[11,444];[0,101677,101671,101679,101672,101676,101680];[942]

        private void SoruMaddeAnaliziOS_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //TCKIMLIKNO="32051057542";
            //OTURUM="C4724139-35B6-4A9E-BB39-F9664D1DC90C";
            //DONEM="2017";
            //ID_SINIFs="[0,101677,101671,101679,101672,101676,101680]";
            //ID_SUBEs="[11,444]";
            //ID_KADEME3=18;
            //ID_SINAVTURU=2;

            //ID_DERSs="[942]";
            //ID_SINAVs="[152]";



            using (Baglanti b = new Baglanti())
            {


                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@ISLEM", islem); // Rapor
                b.ParametreEkle("@ID_MENU", 1043);


                ds = b.SorguGetir("sp_SoruMaddeAnaliziOS");


                if (ds.Tables[1].Rows.Count > 0)
                {
                    dt1 = ds.Tables[0];  // sube/sınıf list
                    dt2 = ds.Tables[1];  // veri

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
            LY = 0;

            XRLabel xrSinavAd = new XRLabel()
            {
                WidthF = artim,
                HeightF = height,
                Text = "SINAV ADI",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrSinavAd.WidthF;
            ReportHeader.Controls.Add(xrSinavAd);

            XRLabel lblSinavAd = new XRLabel()
            {
                WidthF = artim * 2,
                HeightF = height,
                Text = dt2.Rows[0]["SINAVAD"].ToString(),
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += lblSinavAd.WidthF;
            ReportHeader.Controls.Add(lblSinavAd);

            LY += height;
            LX = 0;
            XRLabel xrSinavTarih = new XRLabel()
            {
                WidthF = artim,
                HeightF = height,
                Text = "SINAV TARİHİ",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrSinavTarih.WidthF;
            ReportHeader.Controls.Add(xrSinavTarih);

            XRLabel lblSinavTarih = new XRLabel()
            {
                WidthF = artim * 2,
                HeightF = height,
                Text = dt2.Rows[0]["SINAVTARIH"].ToString(),
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += lblSinavTarih.WidthF;
            ReportHeader.Controls.Add(lblSinavTarih);
            LY += height;
            LX = 0;




            height = 40;
            XRLabel xrDers = new XRLabel()
            {
                WidthF = artim,
                HeightF = height,
                Text = "DERS ADI",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrDers.WidthF;
            ReportHeader.Controls.Add(xrDers);

            XRLabel xrSN = new XRLabel()
            {
                WidthF = artim / 2,
                HeightF = height,
                Text = "SORU NO",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrSN.WidthF;
            ReportHeader.Controls.Add(xrSN);

            XRLabel xrKazanim = new XRLabel()
            {
                WidthF = artim + (artim / 2),
                HeightF = height,
                Text = "KAZANIM",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                BorderColor = borderColor,
            };
            LX += xrKazanim.WidthF;
            ReportHeader.Controls.Add(xrKazanim);

            foreach (DataRow dr in dt1.Rows)
            {
                string y = dr["SUBEAD"].ToString();
                if (islem == 1) //sınıf
                {
                    y += Environment.NewLine + dr["SINIFAD"].ToString();
                }
                XRLabel xrBaslik = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = height,
                    Text = y,
                    Multiline = true,
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = borderColor,
                };
                LX += xrBaslik.WidthF;
                ReportHeader.Controls.Add(xrBaslik);
            }
        }
        public void Icerik()
        {

            height = 30;
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LY = 0;
            DataTable TABLE = dt2.Select("ID_DERS<>0").CopyToDataTable();

            DataView dv = dt2.Select("ID_DERS<>0").CopyToDataTable().DefaultView;
            dv.Sort = "BOLUMNO,KOD";
            DataTable sortedDT = dv.ToTable();

            foreach (DataRow veri in sortedDT.Rows)
            {
                LX = 0;

                XRLabel xrDers = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = height,
                    Text = veri["DERSAD"].ToString(),
                    Tag = "1",
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    KeepTogether = true,
                    BorderColor = borderColor,
                };
                LX += xrDers.WidthF;
                Detail.Controls.Add(xrDers);

                XRLabel xrSN = new XRLabel()
                {
                    WidthF = artim / 2,
                    HeightF = height,
                    Text = veri["SORULIST"].ToString(),
                    //Text="SORUNO_A",
                    Tag = "1",
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    KeepTogether = true,
                    BorderColor = borderColor,
                };
                LX += xrSN.WidthF;
                Detail.Controls.Add(xrSN);

                XRLabel xrKazanim = new XRLabel()
                {
                    WidthF = artim + (artim / 2),
                    HeightF = height,
                    Text = veri["KAZANIM"].ToString(),
                    //Text="KAZANIM",
                    Tag = "1",
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    KeepTogether = true,
                    BorderColor = borderColor,
                };
                LX += xrKazanim.WidthF;
                Detail.Controls.Add(xrKazanim);

                foreach (DataRow dr in dt1.Rows)
                {

                    XRLabel xrBaslik = new XRLabel()
                    {
                        WidthF = artim,
                        HeightF = height,
                        Text = veri[dr["COL"].ToString()].ToString().Replace(".", ","),
                        //Text=dr["COL"].ToString(),
                        Tag = "1",
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = backColor,
                        ForeColor = foreColor,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        KeepTogether = true,
                        BorderColor = borderColor,
                    };
                    LX += xrBaslik.WidthF;
                    Detail.Controls.Add(xrBaslik);
                }
                LY += height;
            }
        }
        public void Footer()
        {
            height = 30;
            Color backColor = Color.Red;
            Color foreColor = Color.White;
            Color borderColor = Color.White;
            LX = 0;
            LY = 0;
            XRLabel xrDers = new XRLabel()
            {
                WidthF = artim * 3,
                HeightF = height,
                Text = "ORTALAMA",
                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                BackColor = backColor,
                ForeColor = foreColor,
                LocationF = new PointF(LX, LY),
                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders = DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth = 1,
                KeepTogether = true,
                BorderColor = borderColor,
            };
            LX += xrDers.WidthF;
            ReportFooter.Controls.Add(xrDers);
            DataRow row = dt2.Select("ID_DERS=0").CopyToDataTable().Rows[0];
            foreach (DataRow dr in dt1.Rows)
            {
                XRLabel xrDers2 = new XRLabel()
                {
                    WidthF = artim,
                    HeightF = height,
                    Text = row[dr["COL"].ToString()].ToString().Replace(".", ","),
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = backColor,
                    ForeColor = foreColor,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    KeepTogether = true,
                    BorderColor = borderColor,
                };
                LX += xrDers2.WidthF;
                ReportFooter.Controls.Add(xrDers2);
            }

        }
    }
}
