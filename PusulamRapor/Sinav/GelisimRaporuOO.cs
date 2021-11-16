using DevExpress.XtraCharts;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOO : XtraReport
    {
        #region tanımlamalar

        private DataSet ds;
        private string[] rAPORTURLIST;
        private string tC;
        private string dONEM;
        private string OTURUM;

        Color white = Color.White;
        Color blue = Color.FromArgb(1, 174, 240);
        Color titleblue = Color.FromArgb(68, 114, 196);

        XRPageBreak pb2 = new XRPageBreak();


        int index = 0;
        float DERSX = 146;

        float LY = 0f;
        float LX = 0f;

        Font font9b = new Font(new FontFamily("Tahoma"), 9, FontStyle.Bold);
        Font font9r = new Font(new FontFamily("Tahoma"), 9, FontStyle.Regular);

        Font font12b = new Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
        Font font12r = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);

        Font font16b = new Font(new FontFamily("Tahoma"), 16, FontStyle.Bold);
        Font fontBaslik = new Font(new FontFamily("Tahoma"), 20, FontStyle.Bold);
        #endregion

        public GelisimRaporuOO(DataSet ds, string tC, string dONEM, string rAPORTURLIST, string oTURUM)
        {
            InitializeComponent();

            //rAPORTURLIST = rAPORTURLIST.Replace(",", "");

            this.ds = ds;
            this.tC = tC;
            this.rAPORTURLIST = rAPORTURLIST.Replace("[", "").Replace("]", "").Split(',');//.Replace(",", "");
            this.dONEM = dONEM;
            this.OTURUM = oTURUM;

            DataTable dtKisiselBilgi = ds.Tables[0].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

            //LBL_DONEM.Text = dONEM + " - " + (Convert.ToInt32(dONEM) + 1);
            //xrLabel_TC.Text = dtKisiselBilgi.Rows[0]["TCKIMLIKNO"].ToString();
            xrLabel_Okul.Text = dtKisiselBilgi.Rows[0]["SUBE"].ToString();
            xrLabel_AdSoyad.Text = dtKisiselBilgi.Rows[0]["ADSOYAD"].ToString();
            xrLabel_Sinif.Text = dtKisiselBilgi.Rows[0]["SINIF"].ToString();

            pbOn.ImageUrl = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Kapak/onO.png");
            pbArka.ImageUrl = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Kapak/arkaO.png");

            //if (Convert.ToInt32( dtKisiselBilgi.Rows[0]["ID_KADEME"])==5) // LİSE
            //{
            //    pbOn.ImageUrl = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Kapak/onL.png");
            //    pbArka.ImageUrl = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Kapak/arkaL.png");
            //}

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float v = 30f;
            float sayfaBoyu = 2100f;

            #region Sayfa1(Ders Öğretmen + Yazılı Yoklama Sonuçları)

            XRPanel sayfa1 = new XRPanel();
            sayfa1.LocationF = new PointF(20f, 0);
            sayfa1.WidthF = PageWidth-20f;
            sayfa1.HeightF = PageHeight - 200;

            #region Ders Öğretmen
            if (Array.IndexOf(rAPORTURLIST, "1") >= 0)//Ders Öğretmen
            {
                index = Array.IndexOf(rAPORTURLIST, "1");
                index++;
                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {

                    float lx = 0;
                    float ly = 0;


                    DataTable dtDersOgretmen = ds.Tables[1].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    if (dtDersOgretmen.Rows.Count > 0)
                    {

                        XRPanel pnlDO = new XRPanel()
                        {
                            LocationF = new PointF(LX, LY),
                            HeightF = sayfaBoyu
                        };


                        XRLabel xrBaslik = new XRLabel()
                        {
                            WidthF = v,
                            HeightF = sayfaBoyu,
                            Text = "DERS ÖĞRETMENLERİ",
                            Font = font16b,
                            BackColor = titleblue,
                            ForeColor = white,
                            Angle = 90,
                            LocationF = new PointF(0, 0),
                            TextAlignment = TextAlignment.MiddleCenter
                        };
                        pnlDO.Controls.Add(xrBaslik);
                        lx += 40f;

                        int kacinciOgretmen = 0;

                        float hh = sayfaBoyu / 8;

                        foreach (DataRow rowOgretmen in dtDersOgretmen.Rows)
                        {
                            if (kacinciOgretmen % 4 == 0)
                            {
                                lx += 30;
                                ly = pnlDO.HeightF - hh;
                            }
                            XRLabel xrAd = new XRLabel()
                            {
                                WidthF = v,
                                HeightF = hh,
                                Text = rowOgretmen["DERSAD"].ToString(),
                                Font = font12b,
                                BackColor = blue,
                                Angle = 90,
                                ForeColor = white,
                                LocationF = new PointF(lx, ly),
                                TextAlignment = TextAlignment.MiddleCenter,
                                Borders = BorderSide.Top,
                                BorderWidth = 1,
                                BorderColor = white
                            };
                            pnlDO.Controls.Add(xrAd);
                            ly -= hh;

                            XRLabel xrSinavAd = new XRLabel()
                            {
                                WidthF = v,
                                HeightF = hh,
                                Padding = new PaddingInfo(10, 0, 0, 0),
                                Text = rowOgretmen["ADSOYAD"].ToString(),
                                Font = font12r,
                                Angle = 90,
                                ForeColor = titleblue,
                                LocationF = new PointF(lx, ly),
                                TextAlignment = TextAlignment.MiddleLeft,
                                Borders = BorderSide.None
                            };
                            pnlDO.Controls.Add(xrSinavAd);
                            ly -= hh;

                            kacinciOgretmen += 1;


                        }

                        sayfa1.Controls.Add(pnlDO);
                        LX = pnlDO.WidthF;
                    }
                }
            }
            #endregion

            //LX = 0f;
            #region Yazılı Yoklama Sonuçları
            if (Array.IndexOf(rAPORTURLIST, "2") >= 0 || Array.IndexOf(rAPORTURLIST, "3") >= 0)//Yazılı Yoklama Sonuçları
            {
                if (Array.IndexOf(rAPORTURLIST, "2") > -1)
                {
                    index = Array.IndexOf(rAPORTURLIST, "2");
                    index++;
                }
                else
                {
                    index = Array.IndexOf(rAPORTURLIST, "3");
                    index++;
                }

                if (ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {

                    LY = 0f;
                    DataTable dtDers = ds.Tables[index];
                    XRSubreport report = new XRSubreport();
                    report.LocationF = new PointF(0, 0);
                    report.WidthF = PageWidth;
                    report.CanGrow = true;


                    int YAZILI_DONEM = Array.IndexOf(rAPORTURLIST, "2") > -1 && Array.IndexOf(rAPORTURLIST, "3") > -1 ? 0 :
                                       Array.IndexOf(rAPORTURLIST, "2") > -1 ? 1 :
                                       Array.IndexOf(rAPORTURLIST, "3") > -1 ? 2 : -1;
                    DataTable dtYazili = ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    GelisimRaporOOYazili yazili = new GelisimRaporOOYazili(dtDers, dtYazili, YAZILI_DONEM, titleblue, blue);
                    report.ReportSource = yazili;

                    yazili.CreateDocument();

                    XtraReport newReport = new XtraReport();
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""));
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + "/" + "yazili-" + tC + ".png");
                    yazili.ExportToImage(path, ImageFormat.Png);



                    XRPictureBox pb = new XRPictureBox();
                    pb.SizeF = new SizeF(1600f - 50f - LX, sayfaBoyu);
                    pb.LocationF = new PointF(LX, LY);
                    pb.AnchorHorizontal = HorizontalAnchorStyles.Left;
                    pb.Sizing = ImageSizeMode.ZoomImage;
                    pb.ImageAlignment = ImageAlignment.MiddleLeft;

                    pb.ImageUrl = path;
                    pb.Image = RotateImage(pb.Image);

                    sayfa1.Controls.Add(pb);
                }
            }

            #endregion

            if ((Array.IndexOf(rAPORTURLIST, "1") > -1) || (Array.IndexOf(rAPORTURLIST, "2") > -1) || (Array.IndexOf(rAPORTURLIST, "3") > -1))
            {

                Detail.Controls.Add(sayfa1);


                //XRPageBreak pbb = new XRPageBreak();
                //pbb.LocationF = new PointF(0, LY+1);
                //Detail.Controls.Add(pbb);
                //LY += 2200 - (2200 > LY ? 2200 % LY : LY % 2200);
                LY = 2200f;

            }

            #endregion

            #region Deneme Sonuçları

            LX = 0;
            if (Array.IndexOf(rAPORTURLIST, "4") >= 0)//Deneme Sonuçları
            {
                index = Array.IndexOf(rAPORTURLIST, "4");
                index++;


                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }


                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {


                    XRPanel pnlDS = new XRPanel()
                    {
                        LocationF = new PointF(LX, LY),
                        HeightF = sayfaBoyu,
                        WidthF = PageWidth
                    };



                    float x = 20f;
                    float y = 0f;

                    XRLabel xrBaslik = new XRLabel()
                    {
                        WidthF = 30f,
                        HeightF = sayfaBoyu,
                        Text = "ULUSAL SINAVLARA HAZIRLIK DENEME SONUÇLARI",
                        Font = font16b,
                        Angle = 90,
                        BackColor = titleblue,
                        ForeColor = white,
                        LocationF = new PointF(x, y),
                        TextAlignment = TextAlignment.MiddleCenter
                    };
                    pnlDS.Controls.Add(xrBaslik);
                    x += 40f;

                    DataTable temptable = ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    foreach (DataRow item in temptable.DefaultView.ToTable(true, "ID_SINAVTURU").Rows)
                    {

                        XRSubreport report = new XRSubreport();
                        //report.LocationF = new PointF(0, 0);
                        report.WidthF = PageWidth;
                        report.CanGrow = true;
                        report.CanShrink = true;

                        int idSinavTuru = Convert.ToInt32(item["ID_SINAVTURU"]);

                        DataTable dt3 = ds.Tables[index].Select("TCKIMLIKNO = '" + tC + "' AND ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();
                        DataTable dt4 = ds.Tables[index + 1].Select("TCKIMLIKNO = '" + tC + "' AND ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();

                        akDers ders = new akDers(dt3, dt4);
                        report.ReportSource = ders;

                        ders.CreateDocument();

                        XtraReport newReport = new XtraReport();
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""));
                        string path = HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + "/" + item["ID_SINAVTURU"] + "-ds-" + tC + ".png");
                        ders.ExportToImage(path, ImageFormat.Png);

                        XRPictureBox pb2 = new XRPictureBox();
                        pb2.SizeF = new SizeF(1690, sayfaBoyu);
                        pb2.LocationF = new PointF(x, y);
                        pb2.AnchorHorizontal = HorizontalAnchorStyles.Left;
                        pb2.Sizing = ImageSizeMode.ZoomImage;
                        pb2.ImageAlignment = ImageAlignment.MiddleLeft;

                        pb2.ImageUrl = path;
                        pb2.Image = RotateImage(pb2.Image);

                        pb2.WidthF = sayfaBoyu * pb2.Image.Width / pb2.Image.Height;

                        pnlDS.Controls.Add(pb2);

                        //Detail.Controls.Add(report);

                        x += pb2.WidthF + 10f;
                    }

                    Detail.Controls.Add(pnlDS);

                    XRPageBreak pb = new XRPageBreak();
                    pb.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb);
                    LY += 2200 -  (LY == 0 ? 0 :  (2200 > LY ? 2200 % LY : LY % 2200));
                    LX = 0;
                }
            }
            #endregion

            #region Gelişim Analizleri
            if (Array.IndexOf(rAPORTURLIST, "5") >= 0)//Gelişim Analizleri
            {
                index = Array.IndexOf(rAPORTURLIST, "5");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }

                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRLabel xrBaslik = new XRLabel()
                    {
                        WidthF = PageWidth,
                        HeightF = 150,
                        Text = "ULUSAL SINAVLARA" + Environment.NewLine + "HAZIRLIK DENEMELERİ" + Environment.NewLine + "GELİŞİM ANALİZLERİ",
                        Font = fontBaslik,
                        //BackColor = titleblue,
                        ForeColor = titleblue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = TextAlignment.MiddleCenter,
                        Multiline = true
                    };
                    Detail.Controls.Add(xrBaslik);
                    LY += 170f;

                    DataTable temptable = ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    foreach (DataRow item in temptable.DefaultView.ToTable(true, "DERSAD", "TCKIMLIKNO", "STKISAAD").Rows)
                    {
                        XRSubreport report = new XRSubreport();
                        report.WidthF = PageWidth;
                        report.CanGrow = true;
                        report.LocationF = new PointF(LX, LY);
                        report.HeightF = 300f;
                        report.CanShrink = false;
                        
                        DataTable d = ds.Tables[index].Select(String.Format("TCKIMLIKNO = '{0}' AND DERSAD = '{1}' AND STKISAAD = '{2}'", item["TCKIMLIKNO"], item["DERSAD"], item["STKISAAD"])).CopyToDataTable();

                        GelisimRaporuOODersSinav ders = new GelisimRaporuOODersSinav(d, false);
                        //report.ReportSource = ders;
                        
                        float height = 120 + (d.Rows.Count * 23);
                        height = height < 300 ? 300 : height;
                                                
                        XRChart ch = GrafikYazSub(d, 820, 0, height);

                        GelisimRaporuOODersSinavGrafikli dersgrafik = new GelisimRaporuOODersSinavGrafikli(ders,1,ch);
                        report.ReportSource = dersgrafik;

                        Detail.Controls.Add(report);

                        LY += height;
                    }
                }
                if (2200 - (2200 > LY + 170f ? 2200 % LY + 170f : LY + 170f % 2200) < 400)
                {
                    LY += (2200 > LY ? 2200 % LY : LY % 2200);
                }
                if (ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRLabel xrBaslik2 = new XRLabel()
                    {
                        WidthF = PageWidth,
                        HeightF = 100,
                        Text = "ULUSAL SINAVLARA" + Environment.NewLine + "HAZIRLIK DENEMELERİ" + Environment.NewLine + "GELİŞİM ANALİZLERİ" + Environment.NewLine + "TOPLAM",
                        Font = fontBaslik,
                        //BackColor = titleblue,
                        ForeColor = titleblue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = TextAlignment.MiddleCenter,
                        Multiline = true
                    };


                    Detail.Controls.Add(xrBaslik2);
                    LY += 170f;



                    DataTable temptabletotal = ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    foreach (DataRow item in temptabletotal.DefaultView.ToTable(true, "STKISAAD", "TCKIMLIKNO").Rows)
                    {
                        XRSubreport report = new XRSubreport();
                        report.WidthF = PageWidth;
                        report.CanGrow = true;
                        report.LocationF = new PointF(LX, LY);
                        report.HeightF = 300f;
                        report.CanShrink = false;

                        DataTable d = ds.Tables[index + 1].Select(String.Format("TCKIMLIKNO = '{0}' AND STKISAAD = '{1}'", item["TCKIMLIKNO"], item["STKISAAD"])).CopyToDataTable();

                        GelisimRaporuOODersSinav ders = new GelisimRaporuOODersSinav(d, true);
                        //report.ReportSource = ders;
                        //Detail.Controls.Add(report);
                        //float height = 120 + (d.Rows.Count * 23);
                        //height = height < 300 ? 300 : height;
                        //GrafikYaz(d, 820, LY, height);

                        float height = 120 + (d.Rows.Count * 23);
                        height = height < 300 ? 300 : height;

                        XRChart ch = GrafikYazSub(d, 820, 0, height);

                        GelisimRaporuOODersSinavGrafikli dersgrafik = new GelisimRaporuOODersSinavGrafikli(ders, 1, ch);
                        report.ReportSource = dersgrafik;

                        Detail.Controls.Add(report);

                        LY += height;
                    }

                    //XRPageBreak pb = new XRPageBreak();
                    //pb.LocationF = new PointF(0, LY);
                    //Detail.Controls.Add(pb);
                    //LY += 2200 - (2200 > LY ? 2200 % LY : LY % 2200);
                }

                if (2200 - (2200 > LY + 170f ? 2200 % LY + 170f : LY + 170f % 2200) < 400)
                {
                    LY += (2200 > LY ? 2200 % LY : LY % 2200);
                }

                index++;

                if (ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRLabel xrBaslik2 = new XRLabel()
                    {
                        WidthF = PageWidth,
                        HeightF = 100,
                        Text = "ULUSAL SINAVLARA" + Environment.NewLine + "HAZIRLIK DENEMELERİ" + Environment.NewLine + "GELİŞİM ANALİZLERİ" + Environment.NewLine + "PUAN",
                        Font = fontBaslik,
                        //BackColor = titleblue,
                        ForeColor = titleblue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = TextAlignment.MiddleCenter,
                        Multiline = true
                    };
                    Detail.Controls.Add(xrBaslik2);
                    LY += 170f;

                    DataTable temptablepuan = ds.Tables[index + 1].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    foreach (DataRow item in temptablepuan.DefaultView.ToTable(true, "ID_SINAVPUANTURU", "TCKIMLIKNO").Rows)
                    {
                        XRSubreport report = new XRSubreport();
                        report.WidthF = PageWidth;
                        report.CanGrow = true;
                        report.LocationF = new PointF(LX, LY);
                        report.HeightF = 300f;
                        report.CanShrink = false;

                        DataTable d = ds.Tables[index + 1].Select(String.Format("TCKIMLIKNO = '{0}' AND ID_SINAVPUANTURU = '{1}'", item["TCKIMLIKNO"], item["ID_SINAVPUANTURU"])).CopyToDataTable();

                        GelisimRaporuOOPuanSinav ders = new GelisimRaporuOOPuanSinav(d);
                        //report.ReportSource = ders;
                        //
                        //Detail.Controls.Add(report);
                        //
                        //float height = 120 + (d.Rows.Count * 23);
                        //height = height < 300 ? 300 : height;
                        //
                        //GrafikYaz(d, 820, LY, height);


                        float height = 120 + (d.Rows.Count * 23);
                        height = height < 300 ? 300 : height;

                        XRChart ch = GrafikYazSub(d, 820, 0, height);

                        GelisimRaporuOODersSinavGrafikli dersgrafik = new GelisimRaporuOODersSinavGrafikli(ders, 2, ch);
                        report.ReportSource = dersgrafik;

                        Detail.Controls.Add(report);

                        LY += height;
                    }
                }

                XRPageBreak pb = new XRPageBreak();
                pb.LocationF = new PointF(0, LY);
                Detail.Controls.Add(pb);

                LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));

            }
            #endregion

            #region Net Ortalamaları Karşılaştırma Tablosu
            if (Array.IndexOf(rAPORTURLIST, "6") >= 0)//Net Ortalamaları Karşılaştırma Tablosu
            {

                index = Array.IndexOf(rAPORTURLIST, "6");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }

                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRLabel xrBaslik = new XRLabel()
                    {
                        WidthF = PageWidth,
                        HeightF = 150,
                        Text = "ULUSAL SINAVLARA HAZIRLIK DENEMELERİ NET" + Environment.NewLine + "ORTALAMALARI KARŞILAŞTIRMA TABLOSU",
                        Font = fontBaslik,
                        //BackColor = titleblue,
                        ForeColor = titleblue,
                        LocationF = new PointF(LX, LY),
                        TextAlignment = TextAlignment.MiddleCenter,
                        Multiline = true
                    };
                    Detail.Controls.Add(xrBaslik);
                    LY += 170f;

                    DataTable temptable = ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;


                    GelisimRaporuOONetOrtalamaTablosu net = new GelisimRaporuOONetOrtalamaTablosu(temptable);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);
                    LY += net.PageHeight;

                    XRPageBreak pb = new XRPageBreak();
                    pb.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb);

                    LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));
                }
            }
            #endregion

            #region % 50 Altındaki Kazanımlar Tablosu
            if (Array.IndexOf(rAPORTURLIST, "7") >= 0)//% 50 Altındaki Kazanımlar Tablosu
            {
                index = Array.IndexOf(rAPORTURLIST, "7");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }

                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    DataTable temptable = ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOKonuAnaliz net = new GelisimRaporuOOKonuAnaliz(temptable);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);

                    LY += 50;

                    XRPageBreak pb = new XRPageBreak();
                    pb.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb);
                    LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));
                }
            }
            #endregion

            #region Ödev Raporu
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "8") >= 0)//Ödev Raporu
            {
                index = Array.IndexOf(rAPORTURLIST, "8");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }

                if (ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRLabel xrBaslik = new XRLabel()
                    {
                        WidthF = PageWidth,
                        HeightF = 23,
                        Text = "ÖDEV DURUMU",
                        Font = fontBaslik,
                        ForeColor = titleblue,
                        LocationF = new PointF(0, LY),
                        TextAlignment = TextAlignment.MiddleCenter,
                        Multiline = true
                    };
                    Detail.Controls.Add(xrBaslik);
                    LY += 100f;

                    DataTable DT = ds.Tables[index].Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();

                    DataTable distinctValues = DT.DefaultView.ToTable(true, "DERS");

                    int i = 0;
                    foreach (DataRow DERS in distinctValues.Rows)
                    {
                        i++;
                        if (distinctValues.Rows.Count % 2 == 0)
                        {
                            GrafikYazOdev(DERS["DERS"].ToString(), DT.Select("DERS='" + DERS["DERS"].ToString() + "'").CopyToDataTable(), LY);
                            if (DERSX == 146)
                            {
                                DERSX += 700;
                            }
                            else
                            {
                                DERSX = 146;
                                LY += 400f;
                            }
                        }
                        else
                        {
                            if (i == distinctValues.Rows.Count)
                            {
                                DERSX = 469;
                                GrafikYazOdev(DERS["DERS"].ToString(), DT.Select("DERS='" + DERS["DERS"].ToString() + "'").CopyToDataTable(), LY);
                                LY += 420f;
                            }
                            else
                            {
                                GrafikYazOdev(DERS["DERS"].ToString(), DT.Select("DERS='" + DERS["DERS"].ToString() + "'").CopyToDataTable(), LY);
                                if (DERSX == 146)
                                {
                                    DERSX += 700;
                                }
                                else
                                {
                                    DERSX = 146;
                                    LY += 400f;
                                }
                            }
                        }

                    }
                    DERSX = 146;
                    XRPageBreak pb = new XRPageBreak();
                    pb.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb);
                    LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));
                }
            }
            #endregion

            #region Öğeretmen Öğrenci Değerlendirme (Yorumlu)
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "9") >= 0)//Öğeretmen Öğrenci Değerlendirme
            {
                index = Array.IndexOf(rAPORTURLIST, "9");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }

                DataTable DTPUAN = ds.Tables[index];
                DataTable DTYORUM = ds.Tables[++index];

                if (DTPUAN.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOOgretmenDegerlendirme net = new GelisimRaporuOOOgretmenDegerlendirme(DTPUAN.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable());
                    report.ReportSource = net;

                    Detail.Controls.Add(report);

                    DataTable distinctValues = DTPUAN.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable().DefaultView.ToTable(true, "BOLUM");
                    LY += (distinctValues.Rows.Count * 33);
                }

                if (DTYORUM.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOOgretmenYorum net = new GelisimRaporuOOOgretmenYorum(DTYORUM.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable());
                    report.ReportSource = net;

                    Detail.Controls.Add(report);

                    LY += (DTYORUM.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable().Rows.Count * 33);
                }
            }
            #endregion
                       
            #region Öğeretmen Öğrenci Değerlendirme (Yorumsuz)
            LX = 0f;
            if ((Array.IndexOf(rAPORTURLIST, "14") >= 0) && (Array.IndexOf(rAPORTURLIST, "9") == -1))
            {
                index = Array.IndexOf(rAPORTURLIST, "14");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "9") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "13") > -1)
                {
                    index++;
                }

                DataTable DTPUAN = ds.Tables[index];

                if (DTPUAN.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOOgretmenDegerlendirme net = new GelisimRaporuOOOgretmenDegerlendirme(DTPUAN.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable());
                    report.ReportSource = net;

                    Detail.Controls.Add(report);

                    DataTable distinctValues = DTPUAN.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable().DefaultView.ToTable(true, "BOLUM");
                    LY += (distinctValues.Rows.Count * 33);
                }
            }
            #endregion

            #region Etüt Raporu
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "10") >= 0)//Etüt Raporu
            {
                index = Array.IndexOf(rAPORTURLIST, "10");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "9") > -1)
                {
                    index++;
                }

                DataTable DTETUT = ds.Tables[index];

                if (DTETUT.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    DataTable TABLE = DTETUT.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOEtutRaporu net = new GelisimRaporuOOEtutRaporu(TABLE);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);
                    LY += (TABLE.Rows.Count * 33);
                }
            }
            #endregion

            #region Abide Dersler Grafiği(Tek)
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "11") >= 0)//Abide Dersler Grafiği(Tek)
            {
                index = Array.IndexOf(rAPORTURLIST, "11");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "9") > -1)
                {
                    index++;
                }

                DataTable DTABIDE = ds.Tables[index];

                if (DTABIDE.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {
                    DataTable TABLE = DTABIDE.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;

                    GelisimRaporuOOAbideDerslerGrafik net = new GelisimRaporuOOAbideDerslerGrafik(TABLE);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);
                    LY += (TABLE.Rows.Count * 33);
                }
            }
            #endregion

            #region LUS SINAV RAPORU
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "12") >= 0)
            {
                index = Array.IndexOf(rAPORTURLIST, "12");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "9") > -1)
                {
                    index++;
                }


                DataTable DTLUS = ds.Tables[index];

                if (DTLUS.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {

                    pb2.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb2);
                    LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));

                    DataTable TABLE = DTLUS.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;


                    GelisimRaporuOOLUSRaporu net = new GelisimRaporuOOLUSRaporu(TABLE);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);
                    LY += 2200;
                    //LY += (TABLE.Rows.Count * 33);
                }
            }
            #endregion

            #region KOS SINAV RAPORU
            LX = 0f;
            if (Array.IndexOf(rAPORTURLIST, "13") >= 0)
            {
                index = Array.IndexOf(rAPORTURLIST, "13");
                index++;

                if ((Array.IndexOf(rAPORTURLIST, "2") == -1 && Array.IndexOf(rAPORTURLIST, "3") > -1) || (Array.IndexOf(rAPORTURLIST, "3") == -1 && Array.IndexOf(rAPORTURLIST, "2") > -1))
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "4") > -1)
                {
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "5") > -1)
                {
                    index++;
                    index++;
                }
                if (Array.IndexOf(rAPORTURLIST, "9") > -1)
                {
                    index++;
                }

                DataTable DTKOSOGR = ds.Tables[index];
                DataTable DTKOSSORU = ds.Tables[index + 1];

                if (DTKOSOGR.Select("TCKIMLIKNO='" + tC + "'").Length > 0)
                {

                    pb2.LocationF = new PointF(0, LY);
                    Detail.Controls.Add(pb2);
                    LY += 2200 - (LY == 0 ? 0 : (2200 > LY ? 2200 % LY : LY % 2200));

                    DataTable TABLE1 = DTKOSOGR.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    DataTable TABLE2 = DTKOSSORU.Select("TCKIMLIKNO='" + tC + "'").CopyToDataTable();
                    XRSubreport report = new XRSubreport();
                    report.WidthF = PageWidth;
                    report.CanGrow = true;
                    report.LocationF = new PointF(LX, LY);
                    report.CanShrink = true;


                    GelisimRaporuOOKOSRaporu net = new GelisimRaporuOOKOSRaporu(TABLE1, TABLE2);
                    report.ReportSource = net;

                    Detail.Controls.Add(report);
                    LY += 2200;
                    //LY += (TABLE.Rows.Count * 33);
                }
            }
            #endregion

            try
            {
                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/GelisimRaporuOO/Temp/" + OTURUM + ""), true);
            }
            catch (Exception)
            {
            }

        }

        public Image RotateImage(Image img)
        {
            var bmp = new Bitmap(img);

            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.White);
                gfx.DrawImage(img, 0, 0, img.Width, img.Height);
            }

            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
            return bmp;
        }

        public void GrafikYaz(DataTable d, float X, float Y, float height)
        {
            XRChart chart = new XRChart();
            chart.LocationF = new PointF(X, Y);
            chart.SizeF = new SizeF(800, height - 50);
            chart.CanGrow = true;

            Series srsYuzdeGenel = new Series("", ViewType.Bar);
            ((SeriesViewColorEachSupportBase)srsYuzdeGenel.View).ColorEach = true;
            srsYuzdeGenel.ShowInLegend = false;
            srsYuzdeGenel.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            foreach (DataRow item in d.Rows)
            {
                if (!item["SINAV ADI"].ToString().Equals("ORTALAMA"))
                {
                    srsYuzdeGenel.Points.Add(new SeriesPoint(item["SINAV ADI"].ToString(), Convert.ToDouble(item["NET"].ToString())));
                }
            }

            #region Series Label
            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
            #endregion

            BarSeriesView sv = (BarSeriesView)srsYuzdeGenel.View;
            sv.BarWidth = 0.10;

            chart.Series.Add(srsYuzdeGenel);

            foreach (var axis in ((XYDiagram)chart.Diagram).GetAllAxesX())
                axis.Label.Font = font12b;

            Detail.Controls.Add(chart);

            XYDiagram diagram = chart.Diagram as XYDiagram;
            diagram.AxisX.Label.Angle = 90;
        }



        public XRChart GrafikYazSub(DataTable d, float X, float Y, float height)
        {
            XRChart chart = new XRChart();
            chart.LocationF = new PointF(X, Y);
            chart.SizeF = new SizeF(800, height - 50);
            chart.CanGrow = true;

            Series srsYuzdeGenel = new Series("", ViewType.Bar);
            ((SeriesViewColorEachSupportBase)srsYuzdeGenel.View).ColorEach = true;
            srsYuzdeGenel.ShowInLegend = false;
            srsYuzdeGenel.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;

            foreach (DataRow item in d.Rows)
            {
                if (!item["SINAV ADI"].ToString().Equals("ORTALAMA"))
                {
                    srsYuzdeGenel.Points.Add(new SeriesPoint(item["SINAV ADI"].ToString(), Convert.ToDouble(item["NET"].ToString())));
                }
            }

            #region Series Label
            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
            #endregion

            BarSeriesView sv = (BarSeriesView)srsYuzdeGenel.View;
            sv.BarWidth = 0.10;

            chart.Series.Add(srsYuzdeGenel);

            foreach (var axis in ((XYDiagram)chart.Diagram).GetAllAxesX())
                axis.Label.Font = font12b;

            //Detail.Controls.Add(chart);

            XYDiagram diagram = chart.Diagram as XYDiagram;
            diagram.AxisX.Label.Angle = 90;

            return chart;
        }

        public void GrafikYazOdev(string DERSAD, DataTable d, float DERSY)
        {

            XRLabel xrBaslik = new XRLabel()
            {
                WidthF = 700f,
                HeightF = 23,
                Text = DERSAD + " ÖDEV DURUMU",
                Font = font16b,
                ForeColor = titleblue,
                LocationF = new PointF(DERSX, DERSY),
                TextAlignment = TextAlignment.MiddleCenter,
                Multiline = true
            };
            Detail.Controls.Add(xrBaslik);
            DERSY += 23f;

            int TOPLAM = 0;
            foreach (DataRow item in d.Rows)
            {
                TOPLAM += Convert.ToInt32(item["SAYI"]);
            }

            XRLabel xrBaslik2 = new XRLabel()
            {
                WidthF = 700f,
                HeightF = 23,
                Text = "TOPLAM ÖDEV SAYISI: " + TOPLAM,
                Font = font12r,
                ForeColor = Color.Red,
                LocationF = new PointF(DERSX, DERSY),
                TextAlignment = TextAlignment.MiddleCenter,
                Multiline = true
            };
            Detail.Controls.Add(xrBaslik2);
            DERSY += 33f;

            XRChart chart = new XRChart();
            chart.LocationF = new PointF(DERSX, DERSY);
            chart.SizeF = new SizeF(700f, 300);
            chart.CanGrow = true;
            Series srsYuzdeGenel = new Series("", ViewType.Doughnut);

            foreach (DataRow item in d.Rows)
            {
                SeriesPoint point = new SeriesPoint(item["ODEVDURUM"].ToString(), Convert.ToDouble(item["SAYI"]));

                switch (item["ODEVDURUM"].ToString())
                {
                    case "Diğer":
                        point.Color = Color.Purple;
                        break;
                    case "Eksik Yaptı":
                        point.Color = Color.Yellow;
                        break;
                    case "Geç Yaptı":
                        point.Color = Color.LightGreen;
                        break;
                    case "Tam Yaptı":
                        point.Color = Color.LawnGreen;
                        break;
                    case "Yapmadı":
                        point.Color = Color.Red;
                        break;
                }

                srsYuzdeGenel.Points.Add(point);
            }

            ((DoughnutSeriesLabel)srsYuzdeGenel.Label).Position = PieSeriesLabelPosition.TwoColumns;
            ((DoughnutSeriesLabel)srsYuzdeGenel.Label).ResolveOverlappingMode = ResolveOverlappingMode.Default;
            ((DoughnutSeriesLabel)srsYuzdeGenel.Label).ResolveOverlappingMinIndent = 5;

            #region Series Label
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
            srsYuzdeGenel.Label.TextPattern = "{A}: {VP:P2}";
            srsYuzdeGenel.LegendTextPattern = "{A}: {V:0}";
            #endregion

            chart.Series.Add(srsYuzdeGenel);


            //foreach (var axis in ((XYDiagram)chart.Diagram).GetAllAxesX())
            //    axis.Label.Font = font12b;

            foreach (Series item in chart.Series)
            {
                item.Label.Font = font12b;
            }

            chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.RightOutside;
            chart.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            chart.Legend.Font = font12b;

            Detail.Controls.Add(chart);
        }

    }
}