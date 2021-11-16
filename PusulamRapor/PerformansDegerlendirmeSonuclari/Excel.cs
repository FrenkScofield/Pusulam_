using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;
using System;

namespace PusulamRapor.PerformansDegerlendirmeSonuclari
{
    public partial class Excel : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_SUBE;
        string ID_KADEME;
        string ID_KULLANICITIPI;
        string TCKIMLIKNOLIST;
        string ID_DEGERLENDIRMEPERIYOT;
        string DONEM;

        Font fontrow = new System.Drawing.Font(new FontFamily("Times New Roman"), 9.75f, FontStyle.Regular);
        DataSet ds;

        public Excel(string TCKIMLIKNO, string OTURUM, string ID_SUBE, string ID_KADEME, string ID_KULLANICITIPI, string TCKIMLIKNOLIST, string ID_DEGERLENDIRMEPERIYOT, string DONEM)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_SUBE = ID_SUBE;
            this.ID_KADEME = ID_KADEME;
            this.ID_KULLANICITIPI = ID_KULLANICITIPI;
            this.TCKIMLIKNOLIST = TCKIMLIKNOLIST;
            this.ID_DEGERLENDIRMEPERIYOT = ID_DEGERLENDIRMEPERIYOT;
            this.DONEM = DONEM;
        }

        private void Excel_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TCPERSONELLIST", TCKIMLIKNOLIST);
                b.ParametreEkle("@ID_DEGERLENDIRMEPERIYOT", ID_DEGERLENDIRMEPERIYOT);
                b.ParametreEkle("@ID_KULLANICITIPI", ID_KULLANICITIPI);
                b.ParametreEkle("@ID_KADEME", ID_KADEME);
                b.ParametreEkle("@ID_SUBELIST", ID_SUBE);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_MENU", 1158);
                b.ParametreEkle("@ISLEM", 24);
                ds = b.SorguGetir("sp_Degerlendirme");

                float Y = 0;
                float X = 0;
                #region HEADER
                {
                    //XRLabel lblSUBE = new XRLabel()
                    //{
                    //    Text = "KAMPÜS",
                    //    WidthF = 200f,
                    //    HeightF = 92f,
                    //    BackColor = Color.White,
                    //    ForeColor = Color.Black,
                    //    CanGrow = false,
                    //    LocationF = new PointF(X, Y),
                    //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                    //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    //    BorderWidth = 1,
                    //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                    //    BorderColor = Color.Black,
                    //    KeepTogether = true,
                    //    Tag = "0"
                    //};
                    //ReportHeader.Controls.Add(lblSUBE);
                    //X += lblSUBE.WidthF;

                    XRLabel lblKADEME = new XRLabel()
                    {
                        Text = "KADEME",
                        WidthF = 120f,
                        HeightF = 92f,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblKADEME);
                    X += lblKADEME.WidthF;

                    XRLabel lblKULLANICITIPI = new XRLabel()
                    {
                        Text = "GÖREV",
                        WidthF = 120f,
                        HeightF = 92f,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblKULLANICITIPI);
                    X += lblKULLANICITIPI.WidthF;

                    XRLabel lblADSOYAD = new XRLabel()
                    {
                        Text = "AD SOYAD",
                        WidthF = 150f,
                        HeightF = 92f,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblADSOYAD);
                    X += lblADSOYAD.WidthF;

                    XRLabel lblTCKIMLIKNO = new XRLabel()
                    {
                        Text = "TCNO",
                        WidthF = 150f,
                        HeightF = 92f,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblTCKIMLIKNO);
                    X += lblTCKIMLIKNO.WidthF;

                    XRLabel lblPERIYOT = new XRLabel()
                    {
                        Text = "PERİYOT",
                        WidthF = 150f,
                        HeightF = 92f,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblPERIYOT);
                    X += lblPERIYOT.WidthF;

                    XRLabel lblGENELGENELPUAN = new XRLabel()
                    {
                        Text = "GENEL PUAN",
                        WidthF = 160f,
                        HeightF = 92F,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblGENELGENELPUAN);
                    X += lblGENELGENELPUAN.WidthF;

                    XRLabel lblGENELGENELSIRA = new XRLabel()
                    {
                        Text = "GENEL SIRA",
                        WidthF = 160f,
                        HeightF = 92F,
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        CanGrow = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };
                    ReportHeader.Controls.Add(lblGENELGENELSIRA);
                    X += lblGENELGENELSIRA.WidthF;

                    DataTable distinctBolumNo = ds.Tables[1].DefaultView.ToTable(true, "BOLUM");
                    foreach (DataRow BOLUM in distinctBolumNo.Rows)
                    {
                        XRLabel lblBOLUM = new XRLabel()
                        {
                            Text = BOLUM["BOLUM"].ToString(),
                            WidthF = 240f,
                            HeightF = 46F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(X, Y),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblBOLUM);

                        float tempx = X;
                        float tempx2 = X;
                        float tempy = 46;
                        float tempy2 = 69;
                        XRLabel lblPUAN = new XRLabel()
                        {
                            Text = "PUAN",
                            WidthF = 160f,
                            HeightF = 23F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(tempx2, tempy),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblPUAN);
                        tempx2 += lblPUAN.WidthF;

                        XRLabel lblSIRA = new XRLabel()
                        {
                            Text = "SIRA",
                            WidthF = 80f,
                            HeightF = 23F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(tempx2, tempy),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblSIRA);
                        tempx2 += lblSIRA.WidthF;

                        XRLabel lblKENDI = new XRLabel()
                        {
                            Text = "KENDİ",
                            WidthF = 80f,
                            HeightF = 23F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(tempx, tempy2),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblKENDI);
                        tempx += lblKENDI.WidthF;

                        //XRLabel lblKAMPUS = new XRLabel()
                        //{
                        //    Text = "KAMPÜS",
                        //    WidthF = 80f,
                        //    HeightF = 23F,
                        //    BackColor = Color.White,
                        //    ForeColor = Color.Black,
                        //    CanGrow = false,
                        //    LocationF = new PointF(tempx, tempy2),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderWidth = 1,
                        //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        //    BorderColor = Color.Black,
                        //    KeepTogether = true,
                        //    Tag = "0"
                        //};
                        //ReportHeader.Controls.Add(lblKAMPUS);
                        //tempx += lblKAMPUS.WidthF;

                        XRLabel lblGENEL = new XRLabel()
                        {
                            Text = "GENEL",
                            WidthF = 80f,
                            HeightF = 23F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(tempx, tempy2),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblGENEL);
                        tempx += lblGENEL.WidthF;

                        //XRLabel lblKAMPUSSIRA = new XRLabel()
                        //{
                        //    Text = "KAMPÜS",
                        //    WidthF = 80f,
                        //    HeightF = 23F,
                        //    BackColor = Color.White,
                        //    ForeColor = Color.Black,
                        //    CanGrow = false,
                        //    LocationF = new PointF(tempx, tempy2),
                        //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                        //    BorderWidth = 1,
                        //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                        //    BorderColor = Color.Black,
                        //    KeepTogether = true,
                        //    Tag = "0"
                        //};
                        //ReportHeader.Controls.Add(lblKAMPUSSIRA);
                        //tempx += lblKAMPUSSIRA.WidthF;

                        XRLabel lblGENELSIRA = new XRLabel()
                        {
                            Text = "GENEL",
                            WidthF = 80f,
                            HeightF = 23F,
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            CanGrow = false,
                            LocationF = new PointF(tempx, tempy2),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };
                        ReportHeader.Controls.Add(lblGENELSIRA);
                        tempx += lblGENELSIRA.WidthF;

                        X += lblBOLUM.WidthF;
                    }
                }
                #endregion

                #region DETAIL
                {
                    Y = 0;

                    DataView dv = ds.Tables[2].DefaultView;
                    dv.Sort = "PERIYOT ASC, PUAN DESC";
                    DataTable distinctPersonel = dv.ToTable(true, "TCKIMLIKNO", "PERIYOT");

                    foreach (DataRow PERSONEL in distinctPersonel.Rows)
                    {
                        if (ds.Tables[0].Select("TCKIMLIKNO='" + PERSONEL["TCKIMLIKNO"] + "'").Length > 0)
                        {
                            X = 0;
                            DataRow PERSONELBILGI = ds.Tables[0].Select("TCKIMLIKNO='" + PERSONEL["TCKIMLIKNO"] + "'").CopyToDataTable().Rows[0];

                            //string SUBE = PERSONELBILGI["SUBE"].ToString();
                            string KADEME = PERSONELBILGI["KADEME"].ToString();
                            string KULLANICITIPI = PERSONELBILGI["KULLANICITIPI"].ToString();
                            string ADSOYAD = PERSONELBILGI["ADSOYAD"].ToString();
                            string TCKIMLIKNO = PERSONELBILGI["TCKIMLIKNO"].ToString();
                            string PERIYOT = PERSONEL["PERIYOT"].ToString();

                            //XRLabel lblSUBE = new XRLabel()
                            //{
                            //    Text = SUBE,
                            //    WidthF = 200f,
                            //    HeightF = 46F,
                            //    BackColor = Color.White,
                            //    ForeColor = Color.Black,
                            //    CanGrow = false,
                            //    LocationF = new PointF(X, Y),
                            //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                            //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                            //    BorderWidth = 1,
                            //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                            //    BorderColor = Color.Black,
                            //    KeepTogether = true,
                            //    Tag = "0"
                            //};
                            //Detail.Controls.Add(lblSUBE);
                            //X += lblSUBE.WidthF;

                            XRLabel lblKADEME = new XRLabel()
                            {
                                Text = KADEME,
                                WidthF = 120f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(X, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblKADEME);
                            X += lblKADEME.WidthF;

                            XRLabel lblKULLANICITIPI = new XRLabel()
                            {
                                Text = KULLANICITIPI,
                                WidthF = 120f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(X, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblKULLANICITIPI);
                            X += lblKULLANICITIPI.WidthF;

                            XRLabel lblADSOYAD = new XRLabel()
                            {
                                Text = ADSOYAD,
                                WidthF = 150f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(X, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblADSOYAD);
                            X += lblADSOYAD.WidthF;

                            XRLabel lblTCKIMLIKNO = new XRLabel()
                            {
                                Text = TCKIMLIKNO,
                                WidthF = 150f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(X, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblTCKIMLIKNO);
                            X += lblTCKIMLIKNO.WidthF;

                            DataTable TABLE = ds.Tables[1].Select("TCKIMLIKNO='" + TCKIMLIKNO + "' AND PERIYOT='" + PERIYOT + "'").CopyToDataTable();
                            XRLabel lblPERIYOT = new XRLabel()
                            {
                                Text = PERSONEL["PERIYOT"].ToString(),
                                WidthF = 150f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(X, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblPERIYOT);
                            float TEMPX = X + lblPERIYOT.WidthF;

                            XRLabel lblGENELGENELPUAN = new XRLabel()
                            {
                                Text = "GENEL PUAN",
                                WidthF = 160f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(TEMPX, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblGENELGENELPUAN);
                            TEMPX += lblGENELGENELPUAN.WidthF;

                            if (ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "' AND PERIYOT = '" + PERIYOT + "'").Length > 0)
                            {
                                DataTable DTGENELGENELPUAN = ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "' AND PERIYOT = '" + PERIYOT + "'").CopyToDataTable();
                                lblGENELGENELPUAN.Text = DTGENELGENELPUAN.Rows[0]["PUAN"].ToString();
                            }
                            else
                            {
                                lblGENELGENELPUAN.Text = "";
                            }

                            XRLabel lblGENELGENELSIRA = new XRLabel()
                            {
                                Text = "GENEL SIRA",
                                WidthF = 160f,
                                HeightF = 46F,
                                BackColor = Color.White,
                                ForeColor = Color.Black,
                                CanGrow = false,
                                LocationF = new PointF(TEMPX, Y),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                BorderColor = Color.Black,
                                KeepTogether = true,
                                Tag = "0"
                            };
                            Detail.Controls.Add(lblGENELGENELSIRA);
                            TEMPX += lblGENELGENELSIRA.WidthF;

                            if (ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "' AND PERIYOT = '" + PERIYOT + "'").Length > 0)
                            {
                                DataTable DTGENELGENELPUAN = ds.Tables[2].Select("TCKIMLIKNO='" + TCKIMLIKNO + "' AND PERIYOT = '" + PERIYOT + "'").CopyToDataTable();
                                lblGENELGENELSIRA.Text = DTGENELGENELPUAN.Rows[0]["SIRA"].ToString();
                            }
                            else
                            {
                                lblGENELGENELSIRA.Text = "";
                            }

                            foreach (DataRow ROW in TABLE.Rows)
                            {
                                XRLabel lblKENDI = new XRLabel()
                                {
                                    Text = ROW["KENDI"].ToString(),
                                    WidthF = 80f,
                                    HeightF = 46F,
                                    BackColor = Color.White,
                                    ForeColor = Color.Black,
                                    CanGrow = false,
                                    LocationF = new PointF(TEMPX, Y),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                    BorderColor = Color.Black,
                                    KeepTogether = true,
                                    Tag = "0"
                                };
                                Detail.Controls.Add(lblKENDI);
                                TEMPX += lblKENDI.WidthF;

                                //XRLabel lblKAMPUS = new XRLabel()
                                //{
                                //    Text = ROW["SUBE"].ToString(),
                                //    WidthF = 80f,
                                //    HeightF = 46F,
                                //    BackColor = Color.White,
                                //    ForeColor = Color.Black,
                                //    CanGrow = false,
                                //    LocationF = new PointF(TEMPX, Y),
                                //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                //    BorderWidth = 1,
                                //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                //    BorderColor = Color.Black,
                                //    KeepTogether = true,
                                //    Tag = "0"
                                //};
                                //Detail.Controls.Add(lblKAMPUS);
                                //TEMPX += lblKAMPUS.WidthF;

                                XRLabel lblGENEL = new XRLabel()
                                {
                                    Text = ROW["GENEL"].ToString(),
                                    WidthF = 80f,
                                    HeightF = 46F,
                                    BackColor = Color.White,
                                    ForeColor = Color.Black,
                                    CanGrow = false,
                                    LocationF = new PointF(TEMPX, Y),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                    BorderColor = Color.Black,
                                    KeepTogether = true,
                                    Tag = "0"
                                };
                                Detail.Controls.Add(lblGENEL);
                                TEMPX += lblGENEL.WidthF;

                                //XRLabel lblKAMPUSSIRA = new XRLabel()
                                //{
                                //    Text = ROW["SUBESIRA"].ToString(),
                                //    WidthF = 80f,
                                //    HeightF = 46F,
                                //    BackColor = Color.White,
                                //    ForeColor = Color.Black,
                                //    CanGrow = false,
                                //    LocationF = new PointF(TEMPX, Y),
                                //    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                //    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                //    BorderWidth = 1,
                                //    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                //    BorderColor = Color.Black,
                                //    KeepTogether = true,
                                //    Tag = "0"
                                //};
                                //Detail.Controls.Add(lblKAMPUSSIRA);
                                //TEMPX += lblKAMPUSSIRA.WidthF;

                                XRLabel lblGENELSIRA = new XRLabel()
                                {
                                    Text = ROW["GENELSIRA"].ToString(),
                                    WidthF = 80f,
                                    HeightF = 46F,
                                    BackColor = Color.White,
                                    ForeColor = Color.Black,
                                    CanGrow = false,
                                    LocationF = new PointF(TEMPX, Y),
                                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                                    BorderWidth = 1,
                                    Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 4, 4),
                                    BorderColor = Color.Black,
                                    KeepTogether = true,
                                    Tag = "0"
                                };
                                Detail.Controls.Add(lblGENELSIRA);

                                TEMPX += lblGENELSIRA.WidthF;
                            }

                            Y += 46F;
                        }
                    }
                }
                #endregion
            }
        }
    }
}
