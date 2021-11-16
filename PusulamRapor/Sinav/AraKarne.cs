using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.Collections.Generic;


namespace PusulamRapor.Sinav
{
    public partial class AraKarne : DevExpress.XtraReports.UI.XtraReport
    {

        //public int ID_SINAVTURU { get; set; }
        public string TC_OGRENCI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_MENU { get; set; }

        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataTable t3 = new DataTable();
        DataTable t4 = new DataTable();
        DataTable t5 = new DataTable();
        DataTable t6 = new DataTable();
        DataTable t7 = new DataTable();
        DataTable t8 = new DataTable();
        DataTable t9 = new DataTable();
        DataTable t10 = new DataTable();
        DataSet ds = new DataSet();
        DataSet ds2 = new DataSet();

        public AraKarne(string tckimlikno, string oturum, string tcogrenci, string donem, string veli)//,string idsinavTuru
        {
            InitializeComponent();
            //ID_SINAVTURU=Convert.ToInt32(idsinavTuru);            
            TC_OGRENCI = tcogrenci;
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            DONEM = donem;
            ID_MENU = 1047;
            if (veli == "1")
            {
                ID_MENU = 1069;
            }
            if (veli == "2")
            {
                ID_MENU = 1045;
            }
        }

        private void AraKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@DONEM", DONEM);
                    b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                    b.ParametreEkle("@ID_MENU", ID_MENU);
                    b.ParametreEkle("@ISLEM", 2); // Rapor

                    ds = b.SorguGetir("sp_AraKarne");

                    GroupField ogrenciField = new GroupField("TCKIMLIKNO");
                    GroupField grpField = new GroupField("ID_SINAVTURU");
                    GroupHeader3.GroupFields.Add(ogrenciField);
                    GroupHeader2.GroupFields.Add(grpField);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.DataSource = ds.Tables[0];
                        t1 = ds.Tables[0];
                        t2 = ds.Tables[1];
                        t3 = ds.Tables[2];
                        t4 = PublicMetods.orderBYtoTable(ds.Tables[3],"SINAVTARIH,DERSAD");
                        t5 = PublicMetods.orderBYtoTable(ds.Tables[4],"SINAVTARIH,ID_SINAV,PUANTURU");
                        t6 = PublicMetods.orderBYtoTable(ds.Tables[5],"PUANTURU");

                        t7 = ds.Tables[6];
                        t8 = ds.Tables[7];
                        t9 = PublicMetods.orderBYtoTable(ds.Tables[8],"DERSAD");
                        t10 = PublicMetods.orderBYtoTable( ds.Tables[9], "DERSAD,YAZILITARIH");

                        if (Convert.ToInt32( t7.Rows[0]["ID_KADEME3"].ToString()) <=14) // ortaokul ise
                        {
                            xrPictureBox1.Visible = false;
                            pbOrtaOkul.Visible = true;
                        }
                    }
                }



                //  Yazılı
                //if(ID_SINAVTURU==-1)
                //{
                //    using(Baglanti b = new Baglanti())
                //    {
                //        b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                //        b.ParametreEkle("@OTURUM",OTURUM);
                //        b.ParametreEkle("@DONEM",DONEM);
                //        b.ParametreEkle("@TC_OGRENCI",TC_OGRENCI);
                //        //b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                //        b.ParametreEkle("@ISLEM",4); // Rapor

                //        ds=b.SorguGetir("sp_AraKarne");


                //        if(ds.Tables[0].Rows.Count>0)
                //        {
                //            this.DataSource=ds.Tables[0];
                //            t1=ds.Tables[0];
                //            t2=ds.Tables[1];
                //            t3=ds.Tables[2];
                //            t4=ds.Tables[3];
                //        }

                //        GroupField ogrenciField = new GroupField("ID_SINAVOGRENCI");
                //        GroupHeader1.GroupFields.Add(ogrenciField);
                //    }
                //}
                //else
                //{

                //    using(Baglanti b = new Baglanti())
                //    {
                //        b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                //        b.ParametreEkle("@OTURUM",OTURUM);
                //        b.ParametreEkle("@DONEM",DONEM);
                //        b.ParametreEkle("@TC_OGRENCI",TC_OGRENCI);
                //        b.ParametreEkle("@ID_SINAVTURU",ID_SINAVTURU);
                //        b.ParametreEkle("@ISLEM",2); // Rapor

                //        ds=b.SorguGetir("sp_AraKarne");


                //        if(ds.Tables[0].Rows.Count>0)
                //        {
                //            this.DataSource=ds.Tables[0];
                //            t1=ds.Tables[0];
                //            t2=ds.Tables[1];
                //            t3=ds.Tables[2];
                //            t4=ds.Tables[3];
                //            t5=ds.Tables[4];
                //            t6=ds.Tables[5];
                //        }

                //        GroupField ogrenciField = new GroupField("ID_SINAVOGRENCI");
                //        GroupHeader1.GroupFields.Add(ogrenciField);
                //    }
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (t2.Rows.Count > 0)
                {
                    float LY = 37;
                    float LX = 0;

                    FontFamily ff = new FontFamily("Tahoma");

                    int kacinciOgretmen = 0;

                    foreach (DataRow rowOgretmen in t2.Select(string.Format("ID_SINIF={0}", GetCurrentColumnValue("ID_SINIF"))))
                    {
                        XRLabel xrAd = new XRLabel()
                        {
                            WidthF = 166,
                            HeightF = 30,
                            Text = rowOgretmen["DERSAD"].ToString(),
                            Font = new System.Drawing.Font(ff, 9, FontStyle.Bold),
                            BackColor = System.Drawing.Color.SkyBlue,
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.Top,
                            BorderWidth = 1,
                            BorderColor = System.Drawing.Color.White,
                            Tag = "Ogrt"
                        };
                        LX += xrAd.WidthF;
                        GroupHeader1.Controls.Add(xrAd);

                        XRLabel xrSinavAd = new XRLabel()
                        {
                            WidthF = 166,
                            HeightF = 30,
                            Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0),
                            Text = rowOgretmen["ADSOYAD"].ToString(),
                            Font = new System.Drawing.Font(ff, 9, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.MidnightBlue,
                            LocationF = new PointF(LX, LY),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft,
                            Borders = DevExpress.XtraPrinting.BorderSide.None,
                            Tag = "Ogrt"
                        };
                        LX += xrSinavAd.WidthF;
                        GroupHeader1.Controls.Add(xrSinavAd);

                        kacinciOgretmen += 1;

                        if (kacinciOgretmen % 5 == 0)
                        {
                            LX = 0;
                            LY += 30;
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                int idSinavTuru = Convert.ToInt32(GetCurrentColumnValue("ID_SINAVTURU"));

                //DataView dw = t10.DefaultView;
                //dw.Sort = "SINAVADI,DONEMBILGI,ID_SINAVRAPOR";
                //DataTable dt = dw.ToTable();
                


                if (idSinavTuru == -1)
                {
                    akYazili yazili = new akYazili(t7, t9, t10);
                    srDers.ReportSource = yazili;
                }
                else
                {

                    //int idSinavTuru = Convert.ToInt32(GetCurrentColumnValue("ID_SINAVTURU"));

                    DataTable dt3 = t3.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();
                    DataTable dt4 = t4.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();



                    akDers ders = new akDers(dt3, dt4);
                    srDers.ReportSource = ders;

                    if (t5.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).Length > 0)
                    {
                        DataRow[] d = t5.Select("ID_SINAVTURU = " + idSinavTuru.ToString());
                        DataRow[] d2 = t6.Select("ID_SINAVTURU = " + idSinavTuru.ToString());

                        if (d.Length>0 && d2.Length>0)
                        {
                        DataTable dt5 = d.CopyToDataTable();
                        DataTable dt6 = d2.CopyToDataTable();

                        akPuan puan = new akPuan(dt6, dt5);
                        srPuan.ReportSource = puan;
                        srPuan.Visible = true;
                        }
                    }
                    else
                    {
                        srPuan.Visible = false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void GroupHeader3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (t1.Rows.Count > 0)
            {
                xrLabel_AdSoyad.DataBindings.Add("Text", this.DataSource, "ADSOYAD");
                xrLabel_Okul.DataBindings.Add("Text", this.DataSource, "SUBEAD");
                xrLabel_Sinif.DataBindings.Add("Text", this.DataSource, "SINIF");
                xrLabel_TC.DataBindings.Add("Text", this.DataSource, "TCKIMLIKNO");
                //xrPictureBoxFotograf.DataBindings.Add("Image",this.DataSource,"FOTOGRAF");
            }
        }
    }
}
