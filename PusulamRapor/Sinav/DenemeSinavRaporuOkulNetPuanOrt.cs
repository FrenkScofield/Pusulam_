using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinavRaporuOkulNetPuanOrt:DevExpress.XtraReports.UI.XtraReport
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
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();

        DataTable s = new DataTable();
        float artim = 0;

        float LY = 0;
        float LX = 0;
        float height = 30;
        float sayfaBoyu = 1130;
        FontFamily ff = new FontFamily("Tahoma");
        int islem = 1;
        #endregion
        public DenemeSinavRaporuOkulNetPuanOrt(string tc,string oturum,string donem,string idKademe3,string idSinavturu,string idSinavList,string idSubeList)
        {
            TCKIMLIKNO=tc;
            OTURUM=oturum;

            DONEM=donem;
            ID_KADEME3=Convert.ToInt32(idKademe3);
            ID_SINAVTURU=Convert.ToInt32(idSinavturu);
            ID_SINAVs=idSinavList=="0" ? "[]" : idSinavList;
            ID_SUBEs=idSubeList=="0" ? "[]" : idSubeList;
            //ID_SINIFs=idSinifList=="[0]" ? "[]" : idSinifList;
            //ID_DERSs=idDersList=="0" ? "[]" : idDersList;

            InitializeComponent();
        }

        private void DenemeSinavRaporuOkulNetPuanOrt_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            //TCKIMLIKNO="32051057542";
            //OTURUM="15C4B03C-64CC-4E34-A90A-69FDA737F242";
            //DONEM="2017";
            ////ID_SINIFs="[0,101677,101671,101679,101672,101676,101680]";
            //ID_SUBEs="[427,652,11,385,236,690,411,134,123,598,580,702,581]";
            //ID_KADEME3=18;
            //ID_SINAVTURU=3;

            //ID_DERSs="[]";
            //ID_SINAVs="[163]";




            //pusulam/Rapor.aspx?rapor=Sinav.DenemeSinavRaporuOkulNetPuanOrt&raporTur=pdf&p=32051057542;15C4B03C-64CC-4E34-A90A-69FDA737F242;2017;18;2;[179];[11]
            using(Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@DONEM",DONEM);
                b.ParametreEkle("@ID_KADEME3",ID_KADEME3);
                b.ParametreEkle("@ID_SINAVTURU",ID_SINAVTURU);
                b.ParametreEkle("@ID_SINAVs",ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs",ID_SUBEs);

                b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                b.ParametreEkle("@OTURUM",OTURUM);
                b.ParametreEkle("@ISLEM",2); // Rapor
                b.ParametreEkle("@ID_MENU",1074);

                ds=b.SorguGetir("sp_DSNetPuanOrtalamalariOS");


                if(ds.Tables[0].Rows.Count>0)
                {
                    dt1=ds.Tables[0];   // ders sube veri
                    dt2=ds.Tables[1];   // genel ort
                    dt3=ds.Tables[2];   // baslik
                    dt4=ds.Tables[3];   // subeler
                    dt5=ds.Tables[4];   // dersler

                    s=dt1.DefaultView.ToTable(true,"DERSAD", "BOLUMNO");

                    artim=sayfaBoyu/(s.Rows.Count+1);

                    lblBaslik2.Text=dt3.Rows[0]["KADEME3"].ToString()+" "+dt3.Rows[0]["SINAVAD"].ToString()+" OKUL NET-PUAN ORTALAMALARI";

                    Baslik();
                    Icerik();
                    Footer();

                    //this.DataSource=dt1;
                    //FillReportDataFields.Fill(Detail,dt1);

                }
            }
        }


        public void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;
            LX=0;
            LY=0;
            height=40;

            XRLabel xrSinavAd = new XRLabel()
            {
                WidthF=artim,
                HeightF=height,
                Text="OKUL",
                Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                BackColor=backColor,
                ForeColor=foreColor,
                KeepTogether=true,
                LocationF=new PointF(LX,LY),
                TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders=DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth=1,
                BorderColor=borderColor,
            };
            LX+=xrSinavAd.WidthF;
            PageHeader.Controls.Add(xrSinavAd);

            foreach(DataRow item in s.Rows)
            {
                string yaz = "Puan Ort";
                if(Convert.ToInt32(item["BOLUMNO"].ToString())!=-1)
                {
                    yaz="Net Ort";
                }
                XRLabel xrDers = new XRLabel()
                {
                    WidthF=artim,
                    HeightF=height,
                    Text=item["DERSAD"].ToString()+Environment.NewLine+yaz,
                    Multiline=true,
                    KeepTogether=true,
                    Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                    BackColor=backColor,
                    ForeColor=foreColor,
                    LocationF=new PointF(LX,LY),
                    TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders=DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth=1,
                    BorderColor=borderColor,
                };
                LX+=xrDers.WidthF;
                PageHeader.Controls.Add(xrDers);
            }




        }
        public void Icerik()
        {

            height=30;
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;
            LY=0;

            string sinavAd = dt3.Rows[0]["SINAVAD"].ToString();

            foreach(DataRow sube in dt4.Rows)
            {
                LX=0;
                XRLabel lblSubeAd = new XRLabel()
                {
                    WidthF=artim,
                    HeightF=height,
                    Text=sube["SUBEAD"].ToString(),
                    Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                    KeepTogether=true,
                    BackColor=backColor,
                    ForeColor=foreColor,
                    LocationF=new PointF(LX,LY),
                    TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders=DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth=1,
                    BorderColor=borderColor,
                };
                LX+=lblSubeAd.WidthF;
                Detail.Controls.Add(lblSubeAd);

                foreach(DataRow ders in s.Rows)
                {
                    foreach(DataRow veri in dt1.Select(string.Format("ID_SUBE={0} AND DERSAD='{1}'",sube["ID_SUBE"].ToString(),ders["DERSAD"].ToString())).CopyToDataTable().Rows)
                    {

                        XRLabel lblDers = new XRLabel()
                        {
                            WidthF=artim,
                            HeightF=height,
                            Text=veri[sinavAd].ToString(),
                            KeepTogether=true,
                            Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                            BackColor=backColor,
                            ForeColor=foreColor,
                            LocationF=new PointF(LX,LY),
                            TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders=DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth=1,
                            BorderColor=borderColor,
                        };
                        LX+=lblDers.WidthF;
                        Detail.Controls.Add(lblDers);
                    }
                }
                LY+=height;
            }

        }
        public void Footer()
        {
            height=30;
            Color backColor = Color.Red;
            Color foreColor = Color.White;
            Color borderColor = Color.White;
            LX=0;
            //LY=0;
            string sinavAd = dt3.Rows[0]["SINAVAD"].ToString();


            XRLabel lblSubeAd = new XRLabel()
            {
                WidthF=artim,
                HeightF=height,
                Text="ORTALAMA",
                Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                BackColor=backColor,
                ForeColor=foreColor,
                KeepTogether=true,
                LocationF=new PointF(LX,LY),
                TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                Borders=DevExpress.XtraPrinting.BorderSide.All,
                BorderWidth=1,
                BorderColor=borderColor,
            };
            LX+=lblSubeAd.WidthF;
            Detail.Controls.Add(lblSubeAd);

            foreach(DataRow ders in s.Rows)
            {
                foreach(DataRow veri in dt2.Select(string.Format("DERSAD='{0}'",ders["DERSAD"].ToString())).CopyToDataTable().Rows)
                {
                    XRLabel lblDers = new XRLabel()
                    {
                        WidthF=artim,
                        HeightF=height,
                        Text=veri[sinavAd].ToString(),
                        Font=new System.Drawing.Font(ff,6,FontStyle.Bold),
                        KeepTogether=true,
                        BackColor=backColor,
                        ForeColor=foreColor,
                        LocationF=new PointF(LX,LY),
                        TextAlignment=DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders=DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth=1,
                        BorderColor=borderColor,
                    };
                    LX+=lblDers.WidthF;
                    Detail.Controls.Add(lblDers);
                }
            }
        }

    }
}
