using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;

namespace PusulamRapor.Sinav
{
    public partial class AraKarneCoklu : DevExpress.XtraReports.UI.XtraReport
    {
        public string TC_OGRENCI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_MENU { get; set; }
        public int ID_SINIF { get; set; }

        public int SayfaSayisi { get; set; }

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
        string YAZILI_DONEM = "";
        bool yeniSayfa = false;

        public AraKarneCoklu(DataTable _dt1, DataTable _dt2, DataTable _dt3, DataTable _dt4, DataTable _dt5, DataTable _dt6, DataTable _dt7, DataTable _dt8, DataTable _dt9, DataTable _dt10, string YAZILI_DONEM,bool ys)
        {
            InitializeComponent();
            t1 = _dt1;
            t2 = _dt2;
            t3 = _dt3;
            t4 = _dt4;
            t5 = _dt5;
            t6 = _dt6;
            t7 = _dt7;
            t8 = _dt8;
            t9 = _dt9;
            t10 = _dt10;
            this.YAZILI_DONEM = YAZILI_DONEM;
            yeniSayfa = ys;

            GroupField ogrenciField = new GroupField("TCKIMLIKNO");
            GroupField grpField = new GroupField("ID_SINAVTURU");
            GroupHeader3.GroupFields.Add(ogrenciField);
            GroupHeader2.GroupFields.Add(grpField);
            try
            {

                if (Convert.ToInt32(t1.Rows[0]["ID_KADEME"]) == 4)
                {
                    pbBaslik.Visible = false;
                    pbBaslikOO.Visible = true;
                }

                if (t1.Rows.Count > 0)
                {
                    this.DataSource = t1;
                }
            }
            catch (Exception)
            {

                throw;
            }
            if (yeniSayfa)
            {

                XRPageBreak br = new XRPageBreak();
                br.LocationF = new PointF(0, 1);
                this.GroupHeader3.Controls.Add(br);

            }
        }

        private void AraKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
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
                if (idSinavTuru == -1)
                {
                    if (!YAZILI_DONEM.Equals("[]"))
                    {
                        akYaziliCoklu yazili = new akYaziliCoklu(t7, t9, t10, YAZILI_DONEM);
                        srDers.ReportSource = yazili;
                    }
                }
                else
                {  
                    DataTable dt3 = t3.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();
                    DataTable dt4 = t4.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();

                    akDers ders = new akDers(dt3, dt4);
                    srDers.ReportSource = ders;
                    if (t5.Rows.Count > 0)
                    {

                        if (t5.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).Length > 0)
                        {
                            DataTable dt5 = t5.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();
                            DataTable dt6 = t6.Select("ID_SINAVTURU = " + idSinavTuru.ToString()).CopyToDataTable();
                            bool puangizle = Convert.ToBoolean(dt5.Rows[0]["PUANGIZLE"]);
                            if (!puangizle)
                            {
                                akPuan puan = new akPuan(dt6, dt5);
                                srPuan.ReportSource = puan;
                                srPuan.Visible = true;
                            }
                            else
                            {
                                srPuan.Visible = false;
                            }
                        }
                        
                    }else
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
