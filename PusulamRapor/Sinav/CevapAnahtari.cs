using System;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav
{
    public partial class CevapAnahtari: DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SINAV { get; set; }

        FontFamily ff = new FontFamily("Tahoma");
        DataSet ds = new DataSet();
        int tamSayfa = 1120;
        float artim = 15;


        public CevapAnahtari(string tckimlikno, string oturum, string idSinav)
        {
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINAV = Convert.ToInt32(idSinav);
            InitializeComponent();
        }

        private void CevapAnahtari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float yarim = tamSayfa / 2;
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ISLEM", 25);
                b.ParametreEkle("@ID_MENU", 17);

                b.ParametreEkle("@ID_SINAV", ID_SINAV);

                ds = b.SorguGetir("sp_Sinav");

                //this.DataSource = ds.Tables[0];

                //GroupField grpOturum = new GroupField("OTURUM");
                //GroupHeader1.GroupFields.Add(grpOturum);
                //GroupField grpSinavDers = new GroupField("ID_SINAVDERS");
                //GroupHeader2.GroupFields.Add(grpOturum);
                //GroupHeader2.GroupFields.Add(grpSinavDers);

                //if (ds.Tables[0].Rows.Count > 0) {
                //    FillReportDataFields.Fill(GroupHeader1, ds.Tables[0]);
                //    FillReportDataFields.Fill(Detail, ds.Tables[0]);
                //}

                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                DataTable dtOturum = ds.Tables[0].DefaultView.ToTable(true, "OTURUM");
                DataTable dtSinavDers = ds.Tables[0].DefaultView.ToTable(true, "TAKMAAD");
                float LX = (float)0; //yan
                float LY = (float)0;
                float LYYedek = 0;

                XRLabel xrBaslik = new XRLabel() { // başlık label
                    WidthF = tamSayfa,
                    HeightF = artim,
                    Text = dr["DONEM"] + " " + dr["GRUP"] + " " + dr["SINAVAD"] + " SINAVI CEVAP ANAHTARI ( " + dr["SINAVTARIH"] + " )",
                    Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                    BackColor = System.Drawing.Color.Transparent,
                    ForeColor = System.Drawing.Color.MidnightBlue,
                    LocationF = new PointF(LX, LY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = System.Drawing.Color.SkyBlue,
                };
                Detail.Controls.Add(xrBaslik);
                LY += artim;

                foreach (DataRow itemOturum in dtOturum.Rows)
                {

                    DataTable dtSD = dt.Select(string.Format("OTURUM={0}", itemOturum["OTURUM"])).CopyToDataTable().DefaultView.ToTable(true, "TAKMAAD");
                    XRLabel xrOturum = new XRLabel() { // oturum label
                        WidthF = tamSayfa, // /dtOturum.Rows.Count,
                        HeightF = artim,
                        Text = itemOturum["OTURUM"].ToString() + ". OTURUM",
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = System.Drawing.Color.PeachPuff,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.SkyBlue,
                    };
                    //LX += xrOturum.WidthF;
                    Detail.Controls.Add(xrOturum);
                    LY += artim;
                    XRLabel xra = new XRLabel() { // a label
                        WidthF = (float)tamSayfa / 2,
                        HeightF = artim,
                        Text = "A",
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = System.Drawing.Color.PeachPuff,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.SkyBlue,
                    };
                    LX += xra.WidthF;
                    Detail.Controls.Add(xra);
                    XRLabel xrb = new XRLabel() { // b label
                        WidthF = (float)tamSayfa / 2,
                        HeightF = artim,
                        Text = "B",
                        Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                        BackColor = System.Drawing.Color.PeachPuff,
                        ForeColor = System.Drawing.Color.MidnightBlue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = System.Drawing.Color.SkyBlue,
                    };
                    LX += xrb.WidthF;
                    Detail.Controls.Add(xrb);


                    int i = 0;
                    float LXDers = 0;
                    foreach (DataRow itemDers in dtSD.Rows)
                    { //ders adı label 
                        XRLabel xrSinavD = new XRLabel() {
                            WidthF = (float)tamSayfa / dtSD.Rows.Count / 2, //dtSinavDers
                            HeightF = artim,
                            Text = itemDers["TAKMAAD"].ToString(),
                            Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                            BackColor = System.Drawing.Color.Transparent,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LXDers, LY + artim),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = System.Drawing.Color.SkyBlue,
                        };
                        Detail.Controls.Add(xrSinavD);
                        XRLabel d = new XRLabel() {
                            WidthF = (float)tamSayfa / dtSD.Rows.Count / 2, //dtSinavDers
                            HeightF = artim,
                            Text = itemDers["TAKMAAD"].ToString(),
                            Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                            BackColor = System.Drawing.Color.Transparent,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LXDers + yarim, LY + artim),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = System.Drawing.Color.SkyBlue,
                        };
                        Detail.Controls.Add(d);
                        LXDers += xrSinavD.WidthF;


                        float LYY = (float)LY + (artim * 2);
                        float LXX = (float)i * artim;
                        i++;
                        int k = 0;
                        Color clr = Color.PeachPuff;
                        foreach (DataRow itemSoru in dt.Select(string.Format("TAKMAAD='{0}'", itemDers["TAKMAAD"])))
                        { //     
                            clr = (clr == Color.Transparent) ? Color.PeachPuff : Color.Transparent;
                            LXX = (float)(i - 1) * (tamSayfa / dtSD.Rows.Count / 2);//dtSinavDers
                            XRLabel xrSoruNo = new XRLabel() { //soru no 
                                WidthF = (float)(tamSayfa / dtSD.Rows.Count) / 4, //dtSinavDers
                                HeightF = artim,
                                Text = itemSoru["SORUNO_A"].ToString(),
                                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                                BackColor = clr,
                                ForeColor = System.Drawing.Color.MidnightBlue,
                                LocationF = new PointF(LXX, LYY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = System.Drawing.Color.SkyBlue,
                            };
                            LXX += xrSoruNo.WidthF;
                            Detail.Controls.Add(xrSoruNo);

                            XRLabel xrSoruCevap = new XRLabel() { // cevap
                                WidthF = (float)(tamSayfa / dtSD.Rows.Count) / 4, //dtSinavDers
                                HeightF = artim,
                                Text = itemSoru["CEVAP"].ToString(),
                                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                                BackColor = clr,
                                ForeColor = System.Drawing.Color.MidnightBlue,
                                LocationF = new PointF(LXX, LYY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = System.Drawing.Color.SkyBlue,
                            };
                            LXX += xrSoruCevap.WidthF;
                            Detail.Controls.Add(xrSoruCevap);
                            LYY += artim;
                        }


                        LYY = (float)LY + (artim * 2);
                        // LXX = i * artim;
                        clr = Color.PeachPuff;

                        DataTable dtSS = dt.Select(string.Format("TAKMAAD='{0}'", itemDers["TAKMAAD"])).CopyToDataTable();
                        dtSS.DefaultView.Sort = "SORUNO";
                        dtSS = dtSS.DefaultView.ToTable();


                        foreach (DataRow itemSoru in dtSS.Rows)
                        //foreach (DataRow itemSoru in dt.Select(string.Format("TAKMAAD='{0}'", itemDers["TAKMAAD"])))
                        { //       
                            clr = (clr == Color.Transparent) ? Color.PeachPuff : Color.Transparent;
                            k++;
                            LXX = (float)((i - 1) * (float)(tamSayfa / dtSD.Rows.Count / 2) + (yarim)); //dtSinavDers
                            XRLabel xrSoruNo = new XRLabel() { //soru no 
                                WidthF = (float)(tamSayfa / dtSD.Rows.Count / 4),//dtSinavDers
                                HeightF = artim,
                                Text = itemSoru["SORUNO"].ToString(),
                                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                                BackColor = clr,
                                ForeColor = System.Drawing.Color.MidnightBlue,
                                LocationF = new PointF(LXX, LYY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = System.Drawing.Color.SkyBlue,
                            };
                            LXX += xrSoruNo.WidthF;
                            Detail.Controls.Add(xrSoruNo);

                            XRLabel xrSoruCevap = new XRLabel() { // cevap
                                WidthF = (float)(tamSayfa / dtSD.Rows.Count / 4),//dtSinavDers
                                HeightF = artim,
                                Text = itemSoru["SORUNO"].ToString() == "" ? "" : itemSoru["CEVAP"].ToString(),
                                Font = new System.Drawing.Font(ff, 6, FontStyle.Bold),
                                BackColor = clr,
                                ForeColor = System.Drawing.Color.MidnightBlue,
                                LocationF = new PointF(LXX, LYY),
                                TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                                Borders = DevExpress.XtraPrinting.BorderSide.All,
                                BorderWidth = 1,
                                BorderColor = System.Drawing.Color.SkyBlue,
                            };
                            LXX += xrSoruCevap.WidthF;
                            Detail.Controls.Add(xrSoruCevap);
                            LYY += artim;
                        }
                        LYYedek = LYY> LYYedek ? LYY: LYYedek;
                        
                    }
                    LY = LYYedek+artim;
                    LX = 0;

                    Detail.Controls.Add(new XRPageBreak() {
                        LocationF= new PointF(0, LYYedek)
                    });
                }
            }
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
