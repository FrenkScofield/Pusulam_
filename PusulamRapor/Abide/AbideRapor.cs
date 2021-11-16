using DevExpress.Compression;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;

namespace PusulamRapor.Abide
{
    public partial class AbideRapor : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string O_TCKIMLIKNO;
        string ID_ABIDESINAV;
        string DONEM;
        string ID_SUBE;
        string ID_SINIF;
        bool TUR;
        Dictionary<string, int> pages;

        DataSet ds;

        public AbideRapor(string TCKIMLIKNO, string OTURUM, string O_TCKIMLIKNO, string ID_ABIDESINAV, string DONEM, string ID_SUBE, string ID_SINIF, string TUR)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.O_TCKIMLIKNO = O_TCKIMLIKNO;
            this.ID_ABIDESINAV = ID_ABIDESINAV;
            this.DONEM = DONEM;
            this.ID_SUBE = ID_SUBE;
            this.ID_SINIF = ID_SINIF;
            this.TUR = TUR.Equals("2") ? true : false;
            InitializeComponent();
        }

        private void AbideRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@O_TCKIMLIKNO", O_TCKIMLIKNO);
                b.ParametreEkle("@ID_ABIDESINAV", ID_ABIDESINAV);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);
                b.ParametreEkle("@ID_SINIF", ID_SINIF);
                b.ParametreEkle("@ID_MENU", 1134);
                b.ParametreEkle("@ISLEM", 16);
                ds = b.SorguGetir("sp_Abide");

                DataTable DTKISISEL = ds.Tables[0];
                DataTable DTDUZEN = ds.Tables[1];
                float x = 0;
                float y = 0;

                bool LiseMi = ds.Tables[0].Rows[0]["ID_KADEME"].ToString() == "5";

                Font fontrow = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
                Font fontkapak = new System.Drawing.Font(new FontFamily("Century Gothic"), 16, FontStyle.Bold);
                Font fontkapakbuyuk = new System.Drawing.Font(new FontFamily("Century Gothic"), 25, FontStyle.Bold);
                Font fontrownumeric = new System.Drawing.Font(new FontFamily("Tahoma"), 10, FontStyle.Regular);
                Font fontdersad = new System.Drawing.Font(new FontFamily("Tahoma"), 14, FontStyle.Bold);
                pages = new Dictionary<string, int>();
                foreach (DataRow OGRENCI in DTKISISEL.Rows)
                {
                    string TCOGRENCI = OGRENCI["TCKIMLIKNO"].ToString();
                    List<int> dersSayfaTur = new List<int>();
                    foreach (DataRow dr in DTDUZEN.Rows)
                    {
                        int sayfaTur = Convert.ToInt32(dr["ID_ABIDESAYFATUR"].ToString());

                        if (sayfaTur == 5 || sayfaTur == 6 || sayfaTur == 7 || sayfaTur == 8)
                        {
                            dersSayfaTur.Add(Convert.ToInt32(dr["ID_ABIDESAYFATUR"].ToString()));
                        }
                    }

                    DataTable dtDers = PublicMetods.orderBYtoTable(ds.Tables[10], "BOLUMNO,DERS", new string[] { "DERS", "BOLUMNO" });


                    bool eklendi = false;
                    //int dersSira = 0;

                    for (int i = 0; i < DTDUZEN.Rows.Count; i++)
                    {

                        int ID_ABIDESAYFATUR = Convert.ToInt32(DTDUZEN.Rows[i]["ID_ABIDESAYFATUR"]);
                        int SIRA = Convert.ToInt32(DTDUZEN.Rows[i]["SIRA"]);

                        if (!eklendi && dersSayfaTur.Contains(ID_ABIDESAYFATUR))
                        {
                            foreach (DataRow itemDers in dtDers.Rows)
                            {
                                string dersAd = itemDers["DERS"].ToString();


                                foreach (int dersST in dersSayfaTur)
                                {

                                    //ID_ABIDESAYFATUR = Convert.ToInt32(DTDUZEN.Rows[i]["ID_ABIDESAYFATUR"]);

                                    switch (dersST)
                                    {
                                        case 5: //Beceri Adı ve Açıklama
                                            {
                                                #region BECERİ ADI VE ACIKLAMA
                                                x = 0;

                                                //DataTable DTBECERI = PublicMetods.orderBYtoTable( ds.Tables[6],"BOLUMNO");
                                                DataTable DTBECERI = ds.Tables[6].Select(String.Format("DERS='{0}'", dersAd)).CopyToDataTable();
                                                DataTable DTBECERIKAPAK = new DataTable();
                                                if (ds.Tables[7].Select(String.Format("DERS='{0}'", dersAd)).Length > 0)
                                                {
                                                    DTBECERIKAPAK = ds.Tables[7].Select(String.Format("DERS='{0}'", dersAd)).CopyToDataTable();
                                                }
                                                AbideRaporBeceriAdiAciklama AbideRaporBeceriAdiAciklama = new AbideRaporBeceriAdiAciklama(DTBECERI, DTBECERIKAPAK);
                                                XRSubreport report = new XRSubreport();
                                                report.ReportSource = AbideRaporBeceriAdiAciklama;
                                                report.LocationF = new PointF(0, y);
                                                report.CanGrow = true;
                                                Detail.Controls.Add(report);

                                                y += 50;

                                                XRPageBreak br = new XRPageBreak();
                                                br.LocationF = new PointF(0, y);
                                                Detail.Controls.Add(br);

                                                #endregion
                                            }
                                            break;
                                        case 6: //Kazanım ve Alt Becerileri
                                            {
                                                #region KAZANIM VE ALT BECERİLERİ
                                                x = 0;
                                                DataTable DTKAZANIM = null;
                                                if (ds.Tables[8].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).Length > 0)
                                                {
                                                    DTKAZANIM = ds.Tables[8].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).CopyToDataTable();
                                                }
                                                else
                                                {
                                                    DTKAZANIM = ds.Tables[8].Select("TCKIMLIKNO=''").CopyToDataTable();
                                                }

                                                //DTKAZANIM = PublicMetods.orderBYtoTable(DTKAZANIM, "BOLUMNO");

                                                DataView view2 = new DataView(DTKAZANIM);
                                                DataTable distinctValues2 = view2.ToTable(true, "DERS");
                                                for (int J = 0; J < distinctValues2.Rows.Count; J++)
                                                {
                                                    XRLabel lblxx = new XRLabel();
                                                    lblxx.LocationF = new PointF(740f, y);
                                                    lblxx.SizeF = new SizeF(0, 0);
                                                    Detail.Controls.Add(lblxx);

                                                    //y += 70;

                                                    //XRPictureBox pb = new XRPictureBox();
                                                    //string yol = AppDomain.CurrentDomain.BaseDirectory;
                                                    ////pb.Image = Image.FromFile(yol + "img\\Abide\\beceriders.png");
                                                    //pb.BackColor = Color.FromArgb(244, 121, 32);
                                                    //pb.SizeF = new SizeF(696f, 100f);
                                                    //pb.LocationF = new PointF(x + 50, y);
                                                    //Detail.Controls.Add(pb);

                                                    //SizeF size = PrintingSystem.Graph.MeasureString(distinctValues2.Rows[J]["DERS"].ToString(), fontdersad);
                                                    //XRLabel lbl = new XRLabel();
                                                    //lbl.Text = distinctValues2.Rows[J]["DERS"].ToString();
                                                    //lbl.LocationF = new PointF(740f - size.Width, y + 50f);
                                                    //lbl.ForeColor = Color.White;
                                                    //lbl.SizeF = size;
                                                    //lbl.Font = fontdersad;
                                                    //Detail.Controls.Add(lbl);
                                                    //lbl.BringToFront();

                                                    y += 50f;

                                                    XRLabel lblAdi = new XRLabel();
                                                    lblAdi.Text = distinctValues2.Rows[J]["DERS"].ToString() + " Soruların Çözülme Performansı";
                                                    lblAdi.LocationF = new PointF(50, y);
                                                    lblAdi.ForeColor = Color.White;
                                                    lblAdi.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblAdi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblAdi.SizeF = new SizeF(696f, 50f);
                                                    lblAdi.Font = fontdersad;
                                                    lblAdi.BorderColor = Color.White;
                                                    lblAdi.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblAdi.BorderWidth = 2;
                                                    Detail.Controls.Add(lblAdi);

                                                    //XRLabel lblSekilSol = new XRLabel();
                                                    //lblSekilSol.Text = "";
                                                    //lblSekilSol.LocationF = new PointF(50 + lblAdi.WidthF, y);
                                                    //lblSekilSol.ForeColor = Color.White;
                                                    //lblSekilSol.BackColor = Color.FromArgb(0, 91, 158);
                                                    //lblSekilSol.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                    //lblSekilSol.SizeF = new SizeF(84f, 100f);
                                                    //lblSekilSol.Font = fontrow;
                                                    //lblSekilSol.BorderColor = Color.White;
                                                    //lblSekilSol.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    //lblSekilSol.BorderWidth = 2;
                                                    //lblSekilSol.Multiline = true;
                                                    //Detail.Controls.Add(lblSekilSol);

                                                    //XRLabel lblAciklama = new XRLabel();
                                                    //lblAciklama.Text = "Tam Çözüldü" + Environment.NewLine +
                                                    //                    "Kısmi Çözüldü" + Environment.NewLine +
                                                    //                    "Çözülemedi";
                                                    //lblAciklama.LocationF = new PointF(50 + lblSekilSol.WidthF + lblAdi.WidthF, y);
                                                    //lblAciklama.ForeColor = Color.White;
                                                    //lblAciklama.BackColor = Color.FromArgb(0, 91, 158);
                                                    //lblAciklama.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    //lblAciklama.SizeF = new SizeF(126f, 100f);
                                                    //lblAciklama.Font = fontrow;
                                                    //lblAciklama.BorderColor = Color.White;
                                                    //lblAciklama.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    //lblAciklama.BorderWidth = 2;
                                                    //lblAciklama.Multiline = true;
                                                    //Detail.Controls.Add(lblAciklama);

                                                    ////XRLabel lblSekil = new XRLabel();
                                                    //XRPictureBox lblSekil = new XRPictureBox();
                                                    ////lblSekil.ImageUrl = "/img/icon.png";                                      
                                                    //lblSekil.LocationF = new PointF(50 + lblSekilSol.WidthF + lblAdi.WidthF + lblAciklama.WidthF, y);
                                                    //lblSekil.ForeColor = Color.White;
                                                    //lblSekil.BackColor = Color.FromArgb(0, 91, 158);
                                                    //lblSekil.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                    //lblSekil.SizeF = new SizeF(84f, 100f);
                                                    //lblSekil.Font = fontrow;
                                                    //lblSekil.BorderColor = Color.White;
                                                    //lblSekil.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    //lblSekil.BorderWidth = 2;
                                                    ////lblSekil.Multiline = true;                                      
                                                    //Detail.Controls.Add(lblSekil);

                                                    y += lblAdi.HeightF;

                                                    XRLabel lblNo = new XRLabel();
                                                    lblNo.Text = "Soru" + Environment.NewLine + "No";
                                                    lblNo.LocationF = new PointF(50, y);
                                                    lblNo.ForeColor = Color.White;
                                                    lblNo.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblNo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblNo.SizeF = new SizeF(50f, 50f);
                                                    lblNo.Font = fontrow;
                                                    lblNo.BorderColor = Color.White;
                                                    lblNo.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                                    lblNo.BorderWidth = 2;
                                                    lblNo.Multiline = true;
                                                    Detail.Controls.Add(lblNo);

                                                    XRLabel lblKazanim = new XRLabel();
                                                    lblKazanim.Text = "Sorunun Kazanımı";
                                                    lblKazanim.LocationF = new PointF(50 + lblNo.WidthF, y);
                                                    lblKazanim.ForeColor = Color.White;
                                                    lblKazanim.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblKazanim.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblKazanim.BorderColor = Color.White;
                                                    lblKazanim.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                                    lblKazanim.BorderWidth = 2;
                                                    lblKazanim.SizeF = new SizeF(352f, 50f);
                                                    lblKazanim.Font = fontrow;
                                                    Detail.Controls.Add(lblKazanim);

                                                    XRLabel lblBeceri = new XRLabel();
                                                    lblBeceri.Text = "Sorulara Ait Alt Beceriler";
                                                    lblBeceri.LocationF = new PointF(50 + lblNo.WidthF + lblKazanim.WidthF, y);
                                                    lblBeceri.ForeColor = Color.White;
                                                    lblBeceri.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblBeceri.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblBeceri.SizeF = new SizeF(294f, 50f);
                                                    lblBeceri.Font = fontrow;
                                                    Detail.Controls.Add(lblBeceri);

                                                    DataTable DTBECERIDERS = DTKAZANIM.Select("DERS= '" + distinctValues2.Rows[J]["DERS"] + "'").CopyToDataTable();

                                                    y += 50f;
                                                    for (int k = 0; k < DTBECERIDERS.Rows.Count; k++)
                                                    {
                                                        float rowheight1 = PrintingSystem.Graph.MeasureString(DTBECERIDERS.Rows[k]["KAZANIM"].ToString(), fontrownumeric, 350, StringFormat.GenericDefault).Height;
                                                        float rowheight2 = PrintingSystem.Graph.MeasureString(DTBECERIDERS.Rows[k]["BECERI"].ToString(), fontrownumeric, 244, StringFormat.GenericDefault).Height;

                                                        float rowheight = rowheight1 > rowheight2 ? rowheight1 : rowheight2;
                                                        rowheight += 25;


                                                        XRPanel PANEL2 = new XRPanel();
                                                        PANEL2.LocationF = new PointF(50, y);
                                                        PANEL2.SizeF = new SizeF(746f - 50f , rowheight);
                                                        PANEL2.BorderColor = Color.FromArgb(0, 91, 158);
                                                        PANEL2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                        PANEL2.BorderWidth = 2;
                                                        //PANEL.CanGrow = true;
                                                        PANEL2.KeepTogether = true;

                                                        XRLabel lblNoRow = new XRLabel();
                                                        lblNoRow.Text = DTBECERIDERS.Rows[k]["SORUNO"].ToString();
                                                        lblNoRow.LocationF = new PointF(0, 0);
                                                        lblNoRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblNoRow.BackColor = Color.White;
                                                        lblNoRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblNoRow.SizeF = new SizeF(50f, rowheight);
                                                        lblNoRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblNoRow.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                                        lblNoRow.BorderWidth = 2;
                                                        lblNoRow.CanGrow = false;
                                                        lblNoRow.Font = fontrow;
                                                        PANEL2.Controls.Add(lblNoRow);

                                                        XRLabel lblAdiRow = new XRLabel();
                                                        lblAdiRow.Text = DTBECERIDERS.Rows[k]["KAZANIM"].ToString();
                                                        lblAdiRow.LocationF = new PointF( lblNoRow.WidthF, 0);
                                                        lblAdiRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblAdiRow.BackColor = Color.White;
                                                        lblAdiRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblAdiRow.SizeF = new SizeF(352f, rowheight);
                                                        lblAdiRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblAdiRow.Borders = DevExpress.XtraPrinting.BorderSide.Right ;
                                                        lblAdiRow.BorderWidth = 2;
                                                        lblAdiRow.CanGrow = false;
                                                        lblAdiRow.Font = fontrownumeric;
                                                        PANEL2.Controls.Add(lblAdiRow);

                                                        XRLabel lblAciklamaRow = new XRLabel();
                                                        lblAciklamaRow.Text = DTBECERIDERS.Rows[k]["BECERI"].ToString();
                                                        lblAciklamaRow.LocationF = new PointF( lblNoRow.WidthF + lblAdiRow.WidthF, 0);
                                                        lblAciklamaRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblAciklamaRow.BackColor = Color.White;
                                                        lblAciklamaRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblAciklamaRow.SizeF = new SizeF(244f, rowheight);
                                                        lblAciklamaRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblAciklamaRow.Borders = DevExpress.XtraPrinting.BorderSide.Right;
                                                        lblAciklamaRow.BorderWidth = 2;
                                                        lblAciklamaRow.CanGrow = false;
                                                        lblAciklamaRow.Font = fontrownumeric;
                                                        PANEL2.Controls.Add(lblAciklamaRow);

                                                     

                                                        //XRLabel lblDurumRow1 = new XRLabel();
                                                        XRPictureBox lblDurumRow = new XRPictureBox
                                                        {
                                                            //Sizing = ImageSizeMode.AutoSize,
                                                            Dpi = 100F,
                                                            LocationFloat = new DevExpress.Utils.PointFloat(x, 0),
                                                            Name = "lblDurumRow",
                                                        };
                                                        if (DTBECERIDERS.Rows[k]["DURUM"].ToString().Contains(":)"))
                                                            lblDurumRow.ImageUrl = "/img/Abide/TamCozuldu" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                        else if (DTBECERIDERS.Rows[k]["DURUM"].ToString().Contains(":("))
                                                            lblDurumRow.ImageUrl = "/img/Abide/Cozulemedi" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                        else
                                                            lblDurumRow.ImageUrl = "/img/Abide/KismiCozuldu" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                        lblDurumRow.LocationF = new PointF( 5 +lblNoRow.WidthF + lblAdiRow.WidthF + lblAciklamaRow.WidthF, 5);
                                                        lblDurumRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblDurumRow.BackColor = Color.White;
                                                        //lblDurumRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblDurumRow.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                                        lblDurumRow.SizeF = new SizeF(50f - 10, rowheight - 10);
                                                        //lblDurumRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        //lblDurumRow.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                        lblDurumRow.BorderWidth = 0;
                                                        lblDurumRow.Font = fontrow;
                                                        lblDurumRow.CanGrow = false;
                                                        //lblDurumRow.AnchorHorizontal = HorizontalAnchorStyles.Both;
                                                        //lblDurumRow.AnchorVertical = VerticalAnchorStyles.Both;

                                                        PANEL2.Controls.Add(lblDurumRow);
                                                        Detail.Controls.Add(PANEL2);

                                                        y += rowheight;
                                                    }

                                                    y += 50f;



                                                    XRPanel PANEL = new XRPanel();
                                                    PANEL.LocationF = new PointF(0, y);
                                                    PANEL.SizeF = new SizeF(746f, 200f);
                                                    //PANEL.CanGrow = true;
                                                    PANEL.KeepTogether = true;

                                                    float yTemp = 0;

                                                    XRLabel lblBaslik = new XRLabel();
                                                    lblBaslik.Text = "Soruların Çözülme Durumu";
                                                    lblBaslik.LocationF = new PointF(50, yTemp);
                                                    lblBaslik.ForeColor = Color.White;
                                                    lblBaslik.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblBaslik.SizeF = new SizeF(696f, 50f);
                                                    lblBaslik.Font = fontdersad;
                                                    lblBaslik.BorderColor = Color.White;
                                                    lblBaslik.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblBaslik.BorderWidth = 2;
                                                    PANEL.Controls.Add(lblBaslik);

                                                    yTemp += lblBaslik.HeightF;



                                                    XRPictureBox lblDurumRow2 = new XRPictureBox
                                                    {
                                                        //Sizing = ImageSizeMode.AutoSize,
                                                        Dpi = 100F,
                                                        LocationFloat = new DevExpress.Utils.PointFloat(x, 0),
                                                        Name = "lblDurumRow",

                                                    };
                                                    lblDurumRow2.ImageUrl = "/img/Abide/TamCozuldu" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                    lblDurumRow2.LocationF = new PointF(50, yTemp);
                                                    lblDurumRow2.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow2.BackColor = Color.White;
                                                    lblDurumRow2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDurumRow2.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                                    lblDurumRow2.SizeF = new SizeF(50f, 50f);
                                                    lblDurumRow2.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow2.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblDurumRow2.BorderWidth = 2;
                                                    lblDurumRow2.Font = fontrow;
                                                    lblDurumRow2.CanGrow = false;
                                                    PANEL.Controls.Add(lblDurumRow2);



                                                    XRLabel lblBaslik2 = new XRLabel();
                                                    lblBaslik2.Text = "Tam Çözüldü";
                                                    lblBaslik2.LocationF = new PointF(100, yTemp);
                                                    lblBaslik2.BackColor = Color.White;
                                                    lblBaslik2.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                    lblBaslik2.SizeF = new SizeF(646f, 50f);
                                                    lblBaslik2.Font = fontrow;
                                                    lblBaslik2.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lblBaslik2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblBaslik2.BorderWidth = 2;
                                                    PANEL.Controls.Add(lblBaslik2);

                                                    yTemp += lblBaslik2.HeightF;




                                                    XRPictureBox lblDurumRow3 = new XRPictureBox
                                                    {
                                                        //Sizing = ImageSizeMode.AutoSize,
                                                        Dpi = 100F,
                                                        LocationFloat = new DevExpress.Utils.PointFloat(x, 0),
                                                        Name = "lblDurumRow",

                                                    };
                                                    lblDurumRow3.ImageUrl = "/img/Abide/KismiCozuldu" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                    lblDurumRow3.LocationF = new PointF(50, yTemp);
                                                    lblDurumRow3.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow3.BackColor = Color.White;
                                                    lblDurumRow3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDurumRow3.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                                    lblDurumRow3.SizeF = new SizeF(50f, 50f);
                                                    lblDurumRow3.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow3.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblDurumRow3.BorderWidth = 2;
                                                    lblDurumRow3.Font = fontrow;
                                                    lblDurumRow3.CanGrow = false;
                                                    PANEL.Controls.Add(lblDurumRow3);

                                                    XRLabel lblBaslik3 = new XRLabel();
                                                    lblBaslik3.Text = "Kısmi Çözüldü";
                                                    lblBaslik3.LocationF = new PointF(100, yTemp);
                                                    lblBaslik3.BackColor = Color.White;
                                                    lblBaslik3.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                    lblBaslik3.SizeF = new SizeF(646f, 50f);
                                                    lblBaslik3.Font = fontrow;
                                                    lblBaslik3.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik3.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lblBaslik3.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblBaslik3.BorderWidth = 2;
                                                    PANEL.Controls.Add(lblBaslik3);

                                                    yTemp += lblBaslik3.HeightF;




                                                    XRPictureBox lblDurumRow4 = new XRPictureBox
                                                    {
                                                        //Sizing = ImageSizeMode.AutoSize,
                                                        Dpi = 100F,
                                                        LocationFloat = new DevExpress.Utils.PointFloat(x, 0),
                                                        Name = "lblDurumRow",

                                                    };
                                                    lblDurumRow4.ImageUrl = "/img/Abide/Cozulemedi" + (LiseMi ? "_Lise" : "") + ".png" + Environment.NewLine;
                                                    lblDurumRow4.LocationF = new PointF(50, yTemp);
                                                    lblDurumRow4.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow4.BackColor = Color.White;
                                                    lblDurumRow4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDurumRow4.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                                    lblDurumRow4.SizeF = new SizeF(50f, 50f);
                                                    lblDurumRow4.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblDurumRow4.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblDurumRow4.BorderWidth = 2;
                                                    lblDurumRow4.Font = fontrow;
                                                    lblDurumRow4.CanGrow = false;
                                                    PANEL.Controls.Add(lblDurumRow4);


                                                    XRLabel lblBaslik4 = new XRLabel();
                                                    lblBaslik4.Text = "Çözülemedi";
                                                    lblBaslik4.LocationF = new PointF(100, yTemp);
                                                    lblBaslik4.BackColor = Color.White;
                                                    lblBaslik4.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                                    lblBaslik4.SizeF = new SizeF(646f, 50f);
                                                    lblBaslik4.Font = fontrow;
                                                    lblBaslik4.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lblBaslik4.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lblBaslik4.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    lblBaslik4.BorderWidth = 2;
                                                    PANEL.Controls.Add(lblBaslik4);



                                                    Detail.Controls.Add(PANEL);
                                                    y += PANEL.HeightF;



                                                    XRPageBreak br = new XRPageBreak();
                                                    br.LocationF = new PointF(0, y);
                                                    Detail.Controls.Add(br);
                                                }
                                                #endregion
                                            }
                                            break;
                                        case 7: //Soruların Çözülme Oranı
                                            {
                                                #region SORULARIN ÇÖZÜLME ORANI
                                                x = 0;
                                                DataTable DTCOZULME = null;

                                                if (ds.Tables[9].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).Length > 0)
                                                {
                                                    DTCOZULME = ds.Tables[9].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).CopyToDataTable();
                                                }
                                                else
                                                {
                                                    DTCOZULME = ds.Tables[14];
                                                }
                                                DTCOZULME = PublicMetods.orderBYtoTable(DTCOZULME, "BOLUMNO");

                                                DataView view3 = new DataView(DTCOZULME);
                                                DataTable distinctValues3 = view3.ToTable(true, "DERS");

                                                XRLabel lblxx = new XRLabel();
                                                lblxx.LocationF = new PointF(740f, y);
                                                lblxx.SizeF = new SizeF(0, 0);
                                                Detail.Controls.Add(lblxx);

                                                y += 70;

                                                XRPictureBox pb = new XRPictureBox();
                                                string yol = AppDomain.CurrentDomain.BaseDirectory;
                                                //pb.Image = Image.FromFile(yol + "img\\Abide\\beceriders.png");
                                                pb.BackColor = Color.FromArgb(244, 121, 32);
                                                pb.SizeF = new SizeF(696f, 100f);
                                                pb.LocationF = new PointF(x + 50, y);
                                                Detail.Controls.Add(pb);

                                                string BASLIK1 = "BÖLÜMLERE GÖRE" + Environment.NewLine + "SORULARIN ÇÖZÜLME ORANLARI";
                                                SizeF size = PrintingSystem.Graph.MeasureString(BASLIK1, fontdersad);
                                                XRLabel lbl = new XRLabel();
                                                lbl.Text = BASLIK1;
                                                lbl.LocationF = new PointF(730f - size.Width, y + 50f);
                                                lbl.ForeColor = Color.White;
                                                lbl.SizeF = size;
                                                lbl.Font = fontdersad;
                                                lbl.Multiline = true;
                                                lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                                                Detail.Controls.Add(lbl);
                                                lbl.BringToFront();

                                                y += 100f;

                                                for (int J = 0; J < distinctValues3.Rows.Count; J++)
                                                {
                                                    XRPanel PANEL = new XRPanel();
                                                    PANEL.LocationF = new PointF(0, y);
                                                    PANEL.SizeF = new SizeF(746f, 50f);
                                                    PANEL.CanGrow = true;
                                                    PANEL.KeepTogether = true;

                                                    float ytemp = 0;

                                                    XRLabel lblBos = new XRLabel();
                                                    lblBos.LocationF = new PointF(50, ytemp);
                                                    lblBos.SizeF = new SizeF(696f, 50f);
                                                    PANEL.Controls.Add(lblBos);

                                                    ytemp += 60;

                                                    XRLabel lblAdi = new XRLabel();
                                                    lblAdi.Text = distinctValues3.Rows[J]["DERS"].ToString() + " Sorulara Göre Yapılma Oranları Tablosu";
                                                    lblAdi.LocationF = new PointF(50, ytemp);
                                                    lblAdi.ForeColor = Color.White;
                                                    lblAdi.BackColor = Color.FromArgb(0, 91, 158);
                                                    lblAdi.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblAdi.SizeF = new SizeF(696f, 50f);
                                                    lblAdi.Font = fontrow;
                                                    PANEL.Controls.Add(lblAdi);

                                                    ytemp += 60;

                                                    XRLabel lbl1 = new XRLabel();
                                                    lbl1.Text = "Soruların" + Environment.NewLine + "% oranı";
                                                    lbl1.LocationF = new PointF(50, ytemp);
                                                    lbl1.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl1.BackColor = Color.White;
                                                    lbl1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl1.SizeF = new SizeF(120f, 50f);
                                                    lbl1.Font = fontrow;
                                                    lbl1.Multiline = true;
                                                    lbl1.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl1.BorderWidth = 2;
                                                    lbl1.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lbl1);

                                                    ytemp += 50;

                                                    XRLabel lbl2 = new XRLabel();
                                                    lbl2.Text = "Öğrenci";
                                                    lbl2.LocationF = new PointF(50, ytemp);
                                                    lbl2.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl2.BackColor = Color.White;
                                                    lbl2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl2.SizeF = new SizeF(120f, 50f);
                                                    lbl2.Font = fontrow;
                                                    lbl2.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl2.BorderWidth = 2;
                                                    lbl2.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lbl2);

                                                    ytemp += 50;

                                                    XRLabel lbl3 = new XRLabel();
                                                    lbl3.Text = "Sınıf";
                                                    lbl3.LocationF = new PointF(50, ytemp);
                                                    lbl3.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl3.BackColor = Color.White;
                                                    lbl3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl3.SizeF = new SizeF(120f, 50f);
                                                    lbl3.Font = fontrow;
                                                    lbl3.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl3.BorderWidth = 2;
                                                    lbl3.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lbl3);

                                                    ytemp += 50;

                                                    XRLabel lbl4 = new XRLabel();
                                                    lbl4.Text = "Okul";
                                                    lbl4.LocationF = new PointF(50, ytemp);
                                                    lbl4.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl4.BackColor = Color.White;
                                                    lbl4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl4.SizeF = new SizeF(120f, 50f);
                                                    lbl4.Font = fontrow;
                                                    lbl4.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl4.BorderWidth = 2;
                                                    lbl4.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lbl4);

                                                    ytemp += 50;

                                                    XRLabel lbl5 = new XRLabel();
                                                    lbl5.Text = "Genel";
                                                    lbl5.LocationF = new PointF(50, ytemp);
                                                    lbl5.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl5.BackColor = Color.White;
                                                    lbl5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl5.SizeF = new SizeF(120f, 50f);
                                                    lbl5.Font = fontrow;
                                                    lbl5.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl5.BorderWidth = 2;
                                                    lbl5.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lbl5);

                                                    ytemp += 50;

                                                    DataTable DTBECERIDERS = DTCOZULME.Select("DERS= '" + distinctValues3.Rows[J]["DERS"] + "'").CopyToDataTable();

                                                    float width = 576f / DTBECERIDERS.Rows.Count;

                                                    for (int k = 0; k < DTBECERIDERS.Rows.Count; k++)
                                                    {
                                                        XRLabel lblNoRow = new XRLabel();
                                                        lblNoRow.Text = "Soru" + Environment.NewLine + DTBECERIDERS.Rows[k]["SORUNO"].ToString();
                                                        lblNoRow.LocationF = new PointF(170 + width * k, lbl1.LocationF.Y);
                                                        lblNoRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblNoRow.BackColor = Color.White;
                                                        lblNoRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblNoRow.SizeF = new SizeF(width, 50f);
                                                        lblNoRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblNoRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                        lblNoRow.BorderWidth = 2;
                                                        lblNoRow.CanGrow = false;
                                                        lblNoRow.Font = fontrow;
                                                        lblNoRow.Multiline = true;
                                                        PANEL.Controls.Add(lblNoRow);

                                                        string OGRENCIDEGER = DTBECERIDERS.Columns.Contains("OGRENCI") ? DTBECERIDERS.Rows[k]["OGRENCI"].ToString() + "%" : "-";
                                                        XRLabel lblOgrenciRow = new XRLabel();
                                                        lblOgrenciRow.Text = OGRENCIDEGER.Equals("-%") ? "-" : OGRENCIDEGER;
                                                        lblOgrenciRow.LocationF = new PointF(170 + width * k, lbl1.LocationF.Y + 50f);
                                                        lblOgrenciRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblOgrenciRow.BackColor = Color.White;
                                                        lblOgrenciRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblOgrenciRow.SizeF = new SizeF(width, 50f);
                                                        lblOgrenciRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblOgrenciRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                        lblOgrenciRow.BorderWidth = 2;
                                                        lblOgrenciRow.CanGrow = false;
                                                        lblOgrenciRow.Font = fontrownumeric;
                                                        PANEL.Controls.Add(lblOgrenciRow);

                                                        string SINIFDEGER = DTBECERIDERS.Columns.Contains("SINIF") ? DTBECERIDERS.Rows[k]["SINIF"].ToString() + "%" : "-";
                                                        XRLabel lblSinifRow = new XRLabel();
                                                        lblSinifRow.Text = SINIFDEGER.Equals("%") ? "-" : SINIFDEGER;
                                                        lblSinifRow.LocationF = new PointF(170 + width * k, lbl1.LocationF.Y + 100f);
                                                        lblSinifRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblSinifRow.BackColor = Color.White;
                                                        lblSinifRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblSinifRow.SizeF = new SizeF(width, 50f);
                                                        lblSinifRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblSinifRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                        lblSinifRow.BorderWidth = 2;
                                                        lblSinifRow.CanGrow = false;
                                                        lblSinifRow.Font = fontrownumeric;
                                                        PANEL.Controls.Add(lblSinifRow);

                                                        string OKULDEGER = DTBECERIDERS.Columns.Contains("OKUL") ? DTBECERIDERS.Rows[k]["OKUL"].ToString() + "%" : "-";
                                                        XRLabel lblOkulRow = new XRLabel();
                                                        lblOkulRow.Text = OKULDEGER.Equals("%") ? "-" : OKULDEGER;
                                                        lblOkulRow.LocationF = new PointF(170 + width * k, lbl1.LocationF.Y + 150f);
                                                        lblOkulRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblOkulRow.BackColor = Color.White;
                                                        lblOkulRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblOkulRow.SizeF = new SizeF(width, 50f);
                                                        lblOkulRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblOkulRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                        lblOkulRow.BorderWidth = 2;
                                                        lblOkulRow.CanGrow = false;
                                                        lblOkulRow.Font = fontrownumeric;
                                                        PANEL.Controls.Add(lblOkulRow);

                                                        string GENELDEGER = DTBECERIDERS.Columns.Contains("GENEL") ? DTBECERIDERS.Rows[k]["GENEL"].ToString() + "%" : "-";
                                                        XRLabel lblGenelRow = new XRLabel();
                                                        lblGenelRow.Text = GENELDEGER.Equals("%") ? "-" : GENELDEGER;
                                                        lblGenelRow.LocationF = new PointF(170 + width * k, lbl1.LocationF.Y + 200f);
                                                        lblGenelRow.ForeColor = Color.FromArgb(0, 91, 158);
                                                        lblGenelRow.BackColor = Color.White;
                                                        lblGenelRow.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                        lblGenelRow.SizeF = new SizeF(width, 50f);
                                                        lblGenelRow.BorderColor = Color.FromArgb(0, 91, 158);
                                                        lblGenelRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                        lblGenelRow.BorderWidth = 2;
                                                        lblGenelRow.CanGrow = false;
                                                        lblGenelRow.Font = fontrownumeric;
                                                        PANEL.Controls.Add(lblGenelRow);
                                                    }
                                                    y += 50;
                                                    Detail.Controls.Add(PANEL);
                                                }

                                                y += 50;

                                                XRPageBreak br = new XRPageBreak();
                                                br.LocationF = new PointF(0, y);
                                                Detail.Controls.Add(br);


                                                #endregion
                                            }
                                            break;
                                        case 8: //Öğrenci Performansı
                                            {
                                                #region ÖĞRENCİ PERFORMANSI
                                                x = 0;
                                                DataTable DTPERFORMANS = new DataTable();
                                                DataTable dtOgrPerformans = new DataTable();

                                                if (ds.Tables[10].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).Length > 0)
                                                {
                                                    DTPERFORMANS = PublicMetods.orderBYtoTable(ds.Tables[10].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).CopyToDataTable(), "BOLUMNO");
                                                    if (ds.Tables[11].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).Length > 0)
                                                    {
                                                        dtOgrPerformans = PublicMetods.orderBYtoTable(ds.Tables[11].Select(String.Format("DERS='{0}' AND TCKIMLIKNO='{1}'", dersAd, TCOGRENCI)).CopyToDataTable(), "BOLUMNO");
                                                    }
                                                }
                                                else
                                                {
                                                    DataView view2 = new DataView(PublicMetods.orderBYtoTable(ds.Tables[14], "BOLUMNO"));
                                                    DataTable distinctValues2 = view2.ToTable(true, "DERS");
                                                    DTPERFORMANS = distinctValues2;
                                                    //dtOgrPerformans = distinctValues2;
                                                }
                                                //XRLabel lblxx = new XRLabel();
                                                //lblxx.LocationF = new PointF(740f, y);
                                                //lblxx.SizeF = new SizeF(0, 0);
                                                //Detail.Controls.Add(lblxx);

                                                //y += 70;

                                              

                                                //string BASLIK1 = dersAd.ToUpper() + " OKURYAZARLIĞI ALANI SONUÇ RAPORU";
                                                //SizeF size = PrintingSystem.Graph.MeasureString(BASLIK1, fontkapak);
                                                //XRLabel lbl = new XRLabel();
                                                //lbl.Text = BASLIK1;
                                                //lbl.LocationF = new PointF((830f - size.Width) / 2f, y + 50F);
                                                //lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                                //lbl.BackColor = Color.White;
                                                //lbl.SizeF = size;
                                                //lbl.Font = fontkapak;
                                                //lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                                                //Detail.Controls.Add(lbl);
                                                //lbl.BringToFront();
                                                //y += 100f;

                                                for (int j = 0; j < DTPERFORMANS.Rows.Count; j++)
                                                {
                                                    x = 50;
                                                    string ders = DTPERFORMANS.Rows[j]["DERS"].ToString();

                                                    string duzey = DTPERFORMANS.Columns.Contains("DUZEY") ? DTPERFORMANS.Rows[j]["DUZEY"].ToString() : "-";
                                                    string yorum = DTPERFORMANS.Columns.Contains("YORUM") ? DTPERFORMANS.Rows[j]["YORUM"].ToString().Substring(0, 1) + DTPERFORMANS.Rows[j]["YORUM"].ToString().Substring(1).Replace("*", Environment.NewLine + "*") : "-";
                                                    string oneri = DTPERFORMANS.Columns.Contains("ONERI") ? DTPERFORMANS.Rows[j]["ONERI"].ToString().Substring(0, 1) + DTPERFORMANS.Rows[j]["ONERI"].ToString().Substring(1).Replace("*", Environment.NewLine + "*") : "-";

                                                    //List<float> heights = new List<float>();

                                                    ////float heigth1 = PrintingSystem.Graph.MeasureString(ders, fontrow, 110, StringFormat.GenericDefault).Height;
                                                    //float heigth2 = PrintingSystem.Graph.MeasureString(duzey, fontrow, 90, StringFormat.GenericDefault).Height;
                                                    //float heigth3 = PrintingSystem.Graph.MeasureString(yorum, fontrownumeric, 248, StringFormat.GenericDefault).Height;
                                                    //float heigth4 = PrintingSystem.Graph.MeasureString(oneri, fontrownumeric, 248, StringFormat.GenericDefault).Height;

                                                    ////heigth1 += 100f;
                                                    //heigth2 += 100f;
                                                    //heigth3 += 100f;
                                                    //heigth4 += 100f;

                                                    ////heights.Add(heigth1);
                                                    //heights.Add(heigth2);
                                                    //heights.Add(heigth3);
                                                    //heights.Add(heigth4);

                                                    //float rowheight = 0f;

                                                    //foreach (var item in heights)
                                                    //{
                                                    //    if (item > rowheight)
                                                    //    {
                                                    //        rowheight = item;
                                                    //    }
                                                    //}

                                                    float pEnSol = 200f;
                                                    float pEnSag = 496f;
                                                    float pBoy = 150f;


                                                    //float rowheight = (pBoy * 3) + pBoy + 100f;

                                                    XRPanel PANEL = new XRPanel();
                                                    PANEL.LocationF = new PointF(0, y);
                                                    PANEL.SizeF = new SizeF(746f, 0);
                                                    PANEL.KeepTogether = true;

                                                    float ytemp = 0;

                                                    //string BASLIK1 = dersAd.ToUpper()+ Environment.NewLine + " OKURYAZARLIĞI ALANI SONUÇ RAPORU";
                                                    //SizeF size = PrintingSystem.Graph.MeasureString(BASLIK1, fontkapak);
                                                    XRLabel lbl = new XRLabel();
                                                    lbl.Text = dersAd.ToUpper()+" " + Environment.NewLine + "OKURYAZARLIĞI ALANI SONUÇ RAPORU";
                                                    lbl.LocationF = new PointF(50f, ytemp + 50F);
                                                    lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl.BackColor = Color.White;
                                                    lbl.SizeF = new SizeF(696f,100f);
                                                    lbl.Font = fontkapak;
                                                    lbl.Multiline = true;
                                                    lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    PANEL.Controls.Add(lbl);
                                                    //lbl.BringToFront();
                                                    ytemp += 150f;



                                                    XRLabel lbl1 = new XRLabel();
                                                    lbl1.Text = "Soruların Çözülme Performansı Sonuçlarına Göre Öğrenci Beceri Düzeyi";
                                                    lbl1.LocationF = new PointF(x, ytemp);
                                                    lbl1.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl1.BackColor = Color.White;
                                                    lbl1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl1.SizeF = new SizeF(pEnSol, pBoy);
                                                    lbl1.Font = fontrow;
                                                    lbl1.Multiline = true;
                                                    lbl1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl1.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl1.BorderWidth = 2;
                                                    lbl1.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left;
                                                    PANEL.Controls.Add(lbl1);

                                                    x += lbl1.WidthF;
                                                    XRLabel lbl2 = new XRLabel();
                                                    lbl2.Text = duzey;
                                                    lbl2.LocationF = new PointF(x, ytemp);
                                                    lbl2.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl2.BackColor = Color.White;
                                                    lbl2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl2.SizeF = new SizeF(pEnSag, pBoy);
                                                    lbl2.Font = fontrow;
                                                    lbl2.Multiline = true;
                                                    lbl2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl2.BorderColor = Color.FromArgb(0, 91, 158);
                                                    lbl2.BorderWidth = 2;
                                                    lbl2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                                    PANEL.Controls.Add(lbl2);

                                                    ytemp += lbl2.HeightF;
                                                    x = 0;

                                                    XRPanel pnl2 = new XRPanel();
                                                    pnl2.LocationF = new PointF(50f, ytemp);
                                                    pnl2.SizeF = new SizeF(696f, 0);
                                                    pnl2.KeepTogether = true;

                                                    //SizeF size3 = PrintingSystem.Graph.MeasureString(yorum, Convert.ToInt32( pEnSag),StringFormat.GenericDefault);

                                                    XRLabel lbl3 = new XRLabel();
                                                    lbl3.Text = "Bu Düzeydeki Öğrencilerden Beklenen Beceriler";
                                                    lbl3.LocationF = new PointF(x, 0);
                                                    lbl3.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl3.BackColor = Color.White;
                                                    lbl3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl3.SizeF = new SizeF(pEnSol, pBoy);
                                                    lbl3.Font = fontrow;
                                                    lbl3.Multiline = true;
                                                    lbl3.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl3.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left;
                                                    pnl2.Controls.Add(lbl3);

                                                    x += lbl3.WidthF;
                                                    XRLabel lbl4 = new XRLabel();
                                                    lbl4.Text = yorum;
                                                    lbl4.LocationF = new PointF(x, 0);
                                                    lbl4.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl4.BackColor = Color.White;
                                                    lbl4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
                                                    lbl4.SizeF = new SizeF(pEnSag, pBoy);
                                                    lbl4.Font = fontrow;
                                                    lbl4.Multiline = true;
                                                    lbl4.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl4.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                                    pnl2.Controls.Add(lbl4);

                                                    pnl2.BorderColor = Color.FromArgb(0, 91, 158);
                                                    pnl2.BorderWidth = 2;
                                                    pnl2.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top;
                                                    PANEL.Controls.Add(pnl2);



                                                    ytemp += pnl2.HeightF;
                                                    x = 0;


                                                    XRPanel pnl3 = new XRPanel();
                                                    pnl3.LocationF = new PointF(50f, ytemp);
                                                    pnl3.SizeF = new SizeF(696f, 0);
                                                    pnl3.KeepTogether = true;

                                                    //SizeF size5 = PrintingSystem.Graph.MeasureString(oneri, Convert.ToInt32(pEnSag));

                                                    XRLabel lbl5 = new XRLabel();
                                                    lbl5.Text = "Öğrencinin Bir Üst Beceri Düzeyine Geçebilmesi İçin Öneriler";
                                                    lbl5.LocationF = new PointF(x, 0);
                                                    lbl5.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl5.BackColor = Color.White;
                                                    lbl5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lbl5.SizeF = new SizeF(pEnSol, pBoy);
                                                    lbl5.Font = fontrow;
                                                    lbl5.Multiline = true;
                                                    lbl5.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl5.Borders = DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Left;
                                                    pnl3.Controls.Add(lbl5);


                                                    x += lbl5.WidthF;
                                                    XRLabel lbl6 = new XRLabel();
                                                    lbl6.Text = oneri;
                                                    lbl6.LocationF = new PointF(x, 0);
                                                    lbl6.ForeColor = Color.FromArgb(0, 91, 158);
                                                    lbl6.BackColor = Color.White;
                                                    lbl6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
                                                    lbl6.SizeF = new SizeF(pEnSag, pBoy);
                                                    lbl6.Font = fontrow;
                                                    lbl6.Multiline = true;
                                                    lbl6.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 10, 10);
                                                    lbl6.Borders = DevExpress.XtraPrinting.BorderSide.Left;
                                                    pnl3.Controls.Add(lbl6);

                                                    pnl3.BorderColor = Color.FromArgb(0, 91, 158);
                                                    pnl3.BorderWidth = 2;
                                                    pnl3.Borders = DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom;
                                                    PANEL.Controls.Add(pnl3);


                                                    ytemp += pnl3.HeightF + 50;



                                                    SizeF size7 = PrintingSystem.Graph.MeasureString("Öğrenci Becerileri Performans Grafiği (% Başarı)", fontkapak);
                                                    XRLabel lbl7 = new XRLabel();
                                                    lbl7.Text = "Öğrenci Becerileri Performans Grafiği (% Başarı)";
                                                    lbl7.LocationF = new PointF((826f - size7.Width) / 2f, ytemp);
                                                    lbl7.ForeColor = Color.White;//.FromArgb(0, 91, 158);
                                                    lbl7.BackColor = Color.FromArgb(205, 87, 0);
                                                    lbl7.SizeF = size7;
                                                    lbl7.Font = fontkapak;
                                                    lbl7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    PANEL.Controls.Add(lbl7);


                                                    ytemp += size7.Height + 50f;


                                                    XRChart CHART = new XRChart();
                                                    Series srs = new Series("", ViewType.Bar);

                                                    foreach (DataRow dr in dtOgrPerformans.Rows)
                                                    {
                                                        srs.Points.Add(new SeriesPoint(dr["BECERI"].ToString(), dr["ORT"].ToString()));
                                                    }

                                                    CHART.SizeF = new SizeF(pEnSol + pEnSag, 220f);
                                                    CHART.LocationF = new PointF(50f, ytemp);
                                                    CHART.Series.Add(srs);
                                                    PANEL.Controls.Add(CHART);



                                                    Detail.Controls.Add(PANEL);
                                                    y += PANEL.HeightF;
                                                }

                                                y += 50;
                                                XRPageBreak brx = new XRPageBreak();
                                                brx.LocationF = new PointF(0, y);
                                                Detail.Controls.Add(brx);
                                                #endregion
                                            }
                                            break;
                                    }
                                }

                            }
                            eklendi = true;
                            //string dersAd = dtDers.Rows[dersSira]["DERS"].ToString();
                        }




                        switch (ID_ABIDESAYFATUR)
                        {
                            case 1: //Kapak Sayfa
                                {
                                    #region ÖN KAPAK
                                    x = 0;
                                    DataTable DTKAPAK = ds.Tables[2];
                                    for (int J = 0; J < DTKAPAK.Rows.Count; J++)
                                    {
                                        string RESIMAD = DTKAPAK.Rows[J]["AD"].ToString();
                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        pb.Image = Image.FromFile(yol + "Dosyalar\\AbideResim\\" + ID_ABIDESINAV + "\\" + ID_ABIDESAYFATUR + "\\" + RESIMAD + ".png");
                                        pb.SizeF = new SizeF(827f, 1169f);
                                        pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                        pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                                        pb.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(pb);

                                        if (J == 0)
                                        {
                                            //Century Gothic
                                            Font font = fontkapakbuyuk;
                                            XRLabel lbl = new XRLabel();
                                            SizeF size = PrintingSystem.Graph.MeasureString(OGRENCI["KADEME3"].ToString(), font);


                                            lbl.Text = OGRENCI["KADEME3"].ToString();
                                            lbl.SizeF = size;//new SizeF(155F, 15F);
                                            lbl.LocationF = new PointF((pb.WidthF / 2f) - ((size.Width) / 2f), y + 240f);
                                            lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                            lbl.BackColor = Color.White;
                                            lbl.Font = font;
                                            Detail.Controls.Add(lbl);


                                            lbl = new XRLabel();
                                            font = fontkapak;
                                            size = PrintingSystem.Graph.MeasureString('(' + OGRENCI["YARIYIL"].ToString() + ')', font);
                                            lbl.Text = '(' + OGRENCI["YARIYIL"].ToString() + ')';
                                            lbl.SizeF = size;//new SizeF(155F, 15F);
                                            lbl.LocationF = new PointF((pb.WidthF / 2f) - ((size.Width) / 2f), y + 485f);
                                            lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                            lbl.BackColor = Color.White;
                                            lbl.Font = font;
                                            lbl.Visible = false;
                                            Detail.Controls.Add(lbl);


                                            lbl = new XRLabel();
                                            font = fontrow;
                                            size = PrintingSystem.Graph.MeasureString(OGRENCI["AD"].ToString() + Environment.NewLine + OGRENCI["SUBE"].ToString() + Environment.NewLine + OGRENCI["SINIF"].ToString(), font);
                                            lbl.Text = OGRENCI["AD"].ToString() + Environment.NewLine + OGRENCI["SUBE"].ToString() + Environment.NewLine + OGRENCI["SINIF"].ToString();
                                            lbl.SizeF = size;//new SizeF(155F, 15F);
                                            lbl.LocationF = new PointF((pb.WidthF / 2f) - ((size.Width) / 2f), y + 520f);
                                            lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                            lbl.BackColor = Color.White;
                                            lbl.Multiline = true;
                                            lbl.CanGrow = true;
                                            lbl.WordWrap = false;
                                            lbl.AutoWidth = true;

                                            //lbl.Padding = new DevExpress.XtraPrinting.PaddingInfo(40, 40, 10, 10);
                                            lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                            lbl.Font = font;
                                            Detail.Controls.Add(lbl);
                                            lbl.BringToFront();

                                            //lbl.LocationF = new PointF((pb.WidthF / 2f) - (lbl.WidthF / 2f), y + 780f);

                                        }
                                        y += 1169f;
                                    }
                                    #endregion
                                }
                                break;
                            case 2: //Önsöz
                                {
                                    #region ÖN SÖZ
                                    x = 0;
                                    DataTable DTONSOZ = ds.Tables[3];
                                    for (int J = 0; J < DTONSOZ.Rows.Count; J++)
                                    {
                                        string RESIMAD = DTONSOZ.Rows[J]["AD"].ToString();
                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        pb.Image = Image.FromFile(yol + "Dosyalar\\AbideResim\\" + ID_ABIDESINAV + "\\" + ID_ABIDESAYFATUR + "\\" + RESIMAD + ".png");
                                        pb.SizeF = new SizeF(827f, 1169f);
                                        pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                        pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                                        pb.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(pb);
                                        XRPageBreak pbr = new XRPageBreak();

                                        if (J == 0)
                                        {
                                            XRLabel lbl = new XRLabel();
                                            lbl.Text = "Sevgili " + OGRENCI["AD"].ToString();
                                            lbl.LocationF = new PointF(55f, y + 80f);
                                            lbl.ForeColor = Color.FromArgb(0, 91, 158);
                                            lbl.SizeF = new SizeF(400f, 50f);
                                            lbl.Font = fontrow;
                                            Detail.Controls.Add(lbl);
                                            lbl.BringToFront();
                                        }
                                        y += 1169f;
                                    }
                                    #endregion
                                }
                                break;
                            case 3: //Rapor Neyi İçerir
                                {
                                    #region RAPOR NEYİ İÇERİR
                                    x = 0;
                                    DataTable DTRNI = ds.Tables[4];
                                    for (int J = 0; J < DTRNI.Rows.Count; J++)
                                    {
                                        string RESIMAD = DTRNI.Rows[J]["AD"].ToString();
                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        pb.Image = Image.FromFile(yol + "Dosyalar\\AbideResim\\" + ID_ABIDESINAV + "\\" + ID_ABIDESAYFATUR + "\\" + RESIMAD + ".png");
                                        pb.SizeF = new SizeF(827f, 1169f);
                                        pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                        pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                                        pb.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(pb);
                                        y += 1169f;
                                    }
                                    #endregion
                                }
                                break;
                            case 4: //Arka Kapak
                                {
                                    #region ARKA KAPAK
                                    x = 0;
                                    DataTable DTAK = ds.Tables[5];
                                    for (int J = 0; J < DTAK.Rows.Count; J++)
                                    {
                                        string RESIMAD = DTAK.Rows[J]["AD"].ToString();
                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        pb.Image = Image.FromFile(yol + "Dosyalar\\AbideResim\\" + ID_ABIDESINAV + "\\" + ID_ABIDESAYFATUR + "\\" + RESIMAD + ".png");
                                        pb.SizeF = new SizeF(827f, 1169f);
                                        pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                                        pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                                        pb.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(pb);
                                        y += 1169f;
                                    }
                                    #endregion
                                }
                                break;

                            case 9: //Grafiksel Gösterim (Her bir ders ayrı)
                                {
                                    #region Grafiksel Gösterim (Her bir ders ayrı)
                                    x = 0;
                                    DataTable DTGRAFIK = null;
                                    if (ds.Tables[12].Select("TCKIMLIKNO='" + TCOGRENCI + "'").Length > 0)
                                    {
                                        DTGRAFIK = ds.Tables[12].Select("TCKIMLIKNO='" + TCOGRENCI + "'").CopyToDataTable();
                                        x = 0;

                                        XRLabel lblxx = new XRLabel();
                                        lblxx.LocationF = new PointF(740f, y);
                                        lblxx.SizeF = new SizeF(0, 0);
                                        Detail.Controls.Add(lblxx);

                                        y += 70;

                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        pb.BackColor = Color.FromArgb(244, 121, 32);
                                        pb.SizeF = new SizeF(696f, 100f);
                                        pb.LocationF = new PointF(x + 50, y);
                                        Detail.Controls.Add(pb);

                                        string BASLIK1 = "BRANŞLARA GÖRE" + Environment.NewLine + "ÖĞRENCİ YETERLİLİK DÜZEYİ GEŞİLİM GRAFİĞİ";
                                        SizeF size = PrintingSystem.Graph.MeasureString(BASLIK1, fontdersad);
                                        XRLabel lbl = new XRLabel();
                                        lbl.Text = BASLIK1;
                                        lbl.LocationF = new PointF(730f - size.Width, y + 50f);
                                        lbl.ForeColor = Color.White;
                                        lbl.SizeF = size;
                                        lbl.Font = fontdersad;
                                        lbl.Multiline = true;
                                        lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                                        Detail.Controls.Add(lbl);
                                        lbl.BringToFront();

                                        y += 100f;

                                        DataView view = new DataView(DTGRAFIK);
                                        DataTable distinctValues = view.ToTable(true, "DERS");

                                        for (int J = 0; J < distinctValues.Rows.Count; J++)
                                        {
                                            DataTable DTGRAFIKDERS = DTGRAFIK.Select("DERS= '" + distinctValues.Rows[J]["DERS"] + "'").CopyToDataTable();

                                            Series srs = new Series("", ViewType.Bar);
                                            for (int K = 0; K < DTGRAFIKDERS.Rows.Count; K++)
                                            {
                                                if (DTGRAFIKDERS.Columns.Contains("SINAV") && DTGRAFIKDERS.Columns.Contains("DUZEY"))
                                                {

                                                    string dzy = DTGRAFIKDERS.Rows[K]["DUZEY"].ToString();

                                                    double dblDuzey = 1;

                                                    if (dzy == "1A")
                                                        dblDuzey = 1;
                                                    else if (dzy == "1B")
                                                        dblDuzey = 1.5;
                                                    else
                                                        dblDuzey = Convert.ToDouble(dzy);

                                                    SeriesPoint sp = new SeriesPoint(DTGRAFIKDERS.Rows[K]["SINAV"].ToString(), dblDuzey);
                                                    sp.Tag = dzy;
                                                    srs.Points.Add(sp);

                                                    //srs.Points.Add(new SeriesPoint(DTGRAFIKDERS.Rows[K]["SINAV"].ToString(), DTGRAFIKDERS.Rows[K]["DUZEY"].ToString()));
                                                }
                                            }

                                            string title = distinctValues.Rows[J]["DERS"].ToString().ToUpper() + " GELİŞİM GRAFİĞİ";
                                            SizeF sizex = PrintingSystem.Graph.MeasureString(title, fontdersad);

                                            XRPanel PANEL = new XRPanel();
                                            PANEL.LocationF = new PointF(0, y);
                                            PANEL.SizeF = new SizeF(827f, sizex.Height + 240f);
                                            PANEL.KeepTogether = true;

                                            XRLabel lblTitle = new XRLabel();
                                            lblTitle.Text = title;
                                            lblTitle.LocationF = new PointF((827f - sizex.Width) / 2, 0);
                                            lblTitle.ForeColor = Color.White;
                                            lblTitle.BackColor = Color.FromArgb(0, 91, 158);
                                            lblTitle.SizeF = new SizeF(sizex.Width, 30);
                                            lblTitle.Font = fontrow;
                                            lblTitle.Multiline = true;
                                            lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                            PANEL.Controls.Add(lblTitle);

                                            y += 40f;
                                            XRChart CHART = new XRChart();

                                            CHART.CustomDrawSeriesPoint += CHART_CustomDrawSeriesPoint;
                                            CHART.SizeF = new SizeF(450f, 220f);
                                            CHART.BackColor = Color.FromArgb(230, 185, 184);
                                            CHART.LocationF = new PointF((827 - 450f) / 2, 40);
                                            CHART.Series.Add(srs);
                                            PANEL.Controls.Add(CHART);

                                            XYDiagram xydDILKAVRAMI = CHART.Diagram as XYDiagram;
                                            xydDILKAVRAMI.Rotated = true;
                                            xydDILKAVRAMI.DefaultPane.BackColor = Color.FromArgb(0, 0, 0, 0);
                                            xydDILKAVRAMI.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;
                                            xydDILKAVRAMI.AxisY.NumericScaleOptions.AutoGrid = false;
                                            xydDILKAVRAMI.AxisY.NumericScaleOptions.GridSpacing = 1;
                                            xydDILKAVRAMI.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;

                                            xydDILKAVRAMI.AxisY.WholeRange.SetMinMaxValues(0, 6);
                                            y += 250f;

                                            Detail.Controls.Add(PANEL);
                                        }

                                        y += 50;
                                        XRPageBreak br = new XRPageBreak();
                                        br.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(br);
                                    }
                                    #endregion
                                }
                                break;
                            case 10: //Grafiksel Gösterim (Tek grafikte tüm dersler)
                                {
                                    #region Grafiksel Gösterim (Tek grafikte tüm dersler)
                                    x = 0;
                                    DataTable DTGRAFIK = null;
                                    if (ds.Tables[13].Select("TCKIMLIKNO='" + TCOGRENCI + "'").Length > 0)
                                    {
                                        DTGRAFIK = PublicMetods.orderBYtoTable(ds.Tables[13].Select("TCKIMLIKNO='" + TCOGRENCI + "'").CopyToDataTable(), "BOLUMNO");

                                        XRLabel lblxx = new XRLabel();
                                        lblxx.LocationF = new PointF(740f, y);
                                        lblxx.SizeF = new SizeF(0, 0);
                                        Detail.Controls.Add(lblxx);

                                        y += 70;

                                        x = 0;
                                        XRPictureBox pb = new XRPictureBox();
                                        string yol = AppDomain.CurrentDomain.BaseDirectory;
                                        //pb.Image = Image.FromFile(yol + "img\\Abide\\beceriders.png");
                                        pb.BackColor = Color.FromArgb(244, 121, 32);
                                        pb.SizeF = new SizeF(696f, 100f);
                                        pb.LocationF = new PointF(x + 50, y);
                                        Detail.Controls.Add(pb);

                                        string BASLIK1 = "DERSLERE GÖRE DÜZEYLER VE" + Environment.NewLine + "\"NERDEYİM\" GRAFİKSEL GÖSTERİM";
                                        SizeF size = PrintingSystem.Graph.MeasureString(BASLIK1, fontdersad);
                                        XRLabel lbl = new XRLabel();
                                        lbl.Text = BASLIK1;
                                        lbl.LocationF = new PointF(730f - size.Width, y + 50f);
                                        lbl.ForeColor = Color.White;
                                        lbl.SizeF = size;
                                        lbl.Font = fontdersad;
                                        lbl.Multiline = true;
                                        lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomRight;
                                        Detail.Controls.Add(lbl);
                                        lbl.BringToFront();

                                        y += 200f;

                                        XRPanel PANEL = new XRPanel();
                                        PANEL.LocationF = new PointF(50, y);
                                        PANEL.SizeF = new SizeF(696f, 260f);
                                        PANEL.BorderColor = Color.Black;
                                        PANEL.BorderWidth = 1;
                                        PANEL.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                        PANEL.CanGrow = true;

                                        DataView view = new DataView(DTGRAFIK);
                                        DataTable distinctValuesDers = view.ToTable(true, "DERS");
                                        XRChart CHART = new XRChart();
                                        CHART.SizeF = new SizeF(600f, 220f);
                                        CHART.BackColor = Color.White;
                                        double max = 0d;
                                        float y2 = 0f;

                                        y += 260f;

                                        if (DTGRAFIK.Columns.Contains("SINAV"))
                                        {
                                            DataTable distinctValues = view.ToTable(true, "SINAV");
                                            for (int J = 0; J < distinctValues.Rows.Count; J++)
                                            {
                                                DataTable DTGRAFIKSINAV = DTGRAFIK.Select("SINAV= '" + distinctValues.Rows[J]["SINAV"] + "'").CopyToDataTable();

                                                Series srs = new Series(distinctValues.Rows[J]["SINAV"].ToString(), ViewType.Bar);
                                                for (int K = 0; K < DTGRAFIKSINAV.Rows.Count; K++)
                                                {
                                                    string dzy = DTGRAFIKSINAV.Rows[K]["DUZEY"].ToString();

                                                    double dblDuzey = 1;

                                                    if (dzy == "1A")
                                                        dblDuzey = 1;
                                                    else if (dzy == "1B")
                                                        dblDuzey = 1.5;
                                                    else
                                                        dblDuzey = Convert.ToDouble(dzy);

                                                    SeriesPoint sp = new SeriesPoint(DTGRAFIKSINAV.Rows[K]["DERS"].ToString(), dblDuzey);
                                                    sp.Tag = dzy;
                                                    srs.Points.Add(sp);
                                                    //srs.Points.Add(new SeriesPoint(DTGRAFIKSINAV.Rows[K]["DERS"].ToString(), DTGRAFIKSINAV.Rows[K]["DUZEY"].ToString()));

                                                    if (Convert.ToDouble(DTGRAFIKSINAV.Rows[K]["MAXDUZEY"].ToString()) > max)
                                                    {
                                                        max = Convert.ToDouble(DTGRAFIKSINAV.Rows[K]["MAXDUZEY"].ToString());
                                                    }
                                                }

                                                CHART.Series.Add(srs);
                                                CHART.CustomDrawSeriesPoint += CHART_CustomDrawSeriesPoint;
                                            }
                                            float width = 600f / (distinctValuesDers.Rows.Count + 1);
                                            float x2 = (696 - (distinctValuesDers.Rows.Count + 1) * width) / 2;
                                            y2 = CHART.HeightF + 90;
                                            List<string> dersList = new List<string>();

                                            /*
                                        for (int J = 0; J < distinctValues.Rows.Count; J++)
                                        {
                                            XRLabel lblSinav = new XRLabel();
                                            lblSinav.Text = distinctValues.Rows[J]["SINAV"].ToString();
                                            lblSinav.LocationF = new PointF((696 - (distinctValuesDers.Rows.Count + 1) * width) / 2, y2);
                                            lblSinav.SizeF = new SizeF(width, 30);
                                            lblSinav.Font = fontrownumeric;
                                            lblSinav.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                            lblSinav.BorderWidth = 0.5f;
                                            PANEL.Controls.Add(lblSinav);

                                            int sira = 1;
                                            for (int K = 0; K < distinctValuesDers.Rows.Count; K++)
                                            {
                                                string ders = distinctValuesDers.Rows[K]["DERS"].ToString();

                                                for (int L = 0; L < dersList.Count; L++)
                                                {
                                                    if (ders == dersList[L])
                                                    {
                                                        sira = L + 1;
                                                        break;
                                                    }
                                                }
                                                var DTGRAFIKSINAVX = DTGRAFIK.Select("SINAV= '" + distinctValues.Rows[J]["SINAV"] + "' AND DERS='" + distinctValuesDers.Rows[K]["DERS"] + "'");
                                                if (DTGRAFIKSINAVX.Length > 0)
                                                {
                                                    DataTable DTGRAFIKSINAV = DTGRAFIKSINAVX.CopyToDataTable();

                                                    XRLabel lblDersDuzey = new XRLabel();
                                                    lblDersDuzey.Text = DTGRAFIKSINAV.Rows[0]["DUZEY"].ToString();
                                                    lblDersDuzey.LocationF = new PointF(x2 + (width * sira), y2);
                                                    lblDersDuzey.SizeF = new SizeF(width, 30);
                                                    lblDersDuzey.Font = fontrownumeric;
                                                    lblDersDuzey.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDersDuzey.BorderWidth = 0.5f;
                                                    lblDersDuzey.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lblDersDuzey);

                                                    XRLabel lblDers = new XRLabel();
                                                    lblDers.Text = ders;
                                                    lblDers.LocationF = new PointF(x2 + (width * sira), CHART.HeightF + 60);
                                                    lblDers.SizeF = new SizeF(width, 30);
                                                    lblDers.Font = fontrownumeric;
                                                    lblDers.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDers.BorderWidth = 0.5f;
                                                    lblDers.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lblDers);

                                                }
                                                else
                                                {
                                                    XRLabel lblDersDuzey = new XRLabel();
                                                    lblDersDuzey.Text = "";
                                                    lblDersDuzey.LocationF = new PointF(x2 + (width * sira), y2);
                                                    lblDersDuzey.SizeF = new SizeF(width, 30);
                                                    lblDersDuzey.Font = fontrownumeric;
                                                    lblDersDuzey.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                                    lblDersDuzey.BorderWidth = 0.5f;
                                                    lblDersDuzey.Borders = DevExpress.XtraPrinting.BorderSide.All;
                                                    PANEL.Controls.Add(lblDersDuzey);
                                                }

                                                if (!dersList.Contains(ders))
                                                {
                                                    dersList.Add(ders);
                                                }
                                                sira++;
                                            }

                                            y2 += 30;
                                        }
                                            */
                                            string title = "DERSLERİN DÜZEYLERİ";
                                            SizeF sizex = PrintingSystem.Graph.MeasureString(title, fontdersad);

                                            XRLabel lblTitle = new XRLabel();
                                            lblTitle.Text = title;
                                            lblTitle.LocationF = new PointF((696f - sizex.Width) / 2, 30);
                                            lblTitle.SizeF = new SizeF(sizex.Width, 30);
                                            lblTitle.Font = fontrow;
                                            lblTitle.Multiline = false;
                                            lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                            lblTitle.BorderWidth = 0;
                                            PANEL.Controls.Add(lblTitle);


                                            CHART.LocationF = new PointF((696f - 600f) / 2, 60);
                                            PANEL.Controls.Add(CHART);
                                            XYDiagram xyd = CHART.Diagram as XYDiagram;
                                            xyd.DefaultPane.BackColor = Color.FromArgb(0, 0, 0, 0);
                                            xyd.DefaultPane.FillStyle.FillMode = DevExpress.XtraCharts.FillMode.Solid;

                                            CHART.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                                            CHART.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                                            CHART.Legend.Direction = LegendDirection.LeftToRight;

                                            xyd.AxisY.Title.Text = "Düzeyler";
                                            xyd.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;

                                            xyd.AxisY.WholeRange.SetMinMaxValues(0, 6);
                                            xyd.AxisY.NumericScaleOptions.AutoGrid = false;
                                            xyd.AxisY.NumericScaleOptions.GridSpacing = 1;
                                            xyd.AxisY.NumericScaleOptions.GridAlignment = NumericGridAlignment.Ones;


                                            XRLabel lblBos = new XRLabel();
                                            lblBos.Text = "";
                                            lblBos.LocationF = new PointF(0, y2);
                                            lblBos.SizeF = new SizeF(696, 30);
                                            lblBos.Font = fontrow;
                                            lblBos.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                            lblBos.BorderWidth = 0;
                                            PANEL.Controls.Add(lblBos);

                                            Detail.Controls.Add(PANEL);

                                            y += PANEL.HeightF;

                                            y += 50;
                                        }

                                        XRPageBreak br = new XRPageBreak();
                                        br.LocationF = new PointF(0, y);
                                        Detail.Controls.Add(br);
                                    }
                                    #endregion
                                }
                                break;
                        }
                    }

                    if (TUR)
                    {
                        XRLabel lblevent = new XRLabel();
                        lblevent.Text = "";
                        lblevent.LocationF = new PointF(0, y + 20f);
                        lblevent.SizeF = new SizeF(0, 0);
                        lblevent.PrintOnPage += Lblevent_PrintOnPage;
                        Detail.Controls.Add(lblevent);
                    }
                }
            }
        }

        private void CHART_CustomDrawSeriesPoint(object sender, CustomDrawSeriesPointEventArgs e)
        {
            if (e.SeriesPoint.Tag != null)
            {
                e.LabelText = e.SeriesPoint.Tag.ToString();
            }
        }

        private void Lblevent_PrintOnPage(object sender, PrintOnPageEventArgs e)
        {
            string tc = ds.Tables[0].Rows[pages.Count]["TCKIMLIKNO"].ToString();
            if (!pages.ContainsKey(tc))
            {
                pages.Add(tc, e.PageIndex);
            }
        }

        private void Detail_AfterPrint(object sender, EventArgs e)
        {
        }

        private void AbideRapor_AfterPrint(object sender, EventArgs e)
        {
            if (TUR)
            {
                int tss = 0;
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""));
                DataTable DTKISISEL = ds.Tables[0];
                foreach (DataRow OGRENCI in DTKISISEL.Rows)
                {
                    string TCOGRENCI = OGRENCI["TCKIMLIKNO"].ToString();
                    XtraReport newReport = new XtraReport();
                    for (int j = tss; j < pages[TCOGRENCI]; j++)
                    {
                        newReport.Pages.Add(this.Pages[j]);
                    }
                    tss = pages[TCOGRENCI];

                    string name = /*OGRENCI["SUBE"].ToString() + "-" + */OGRENCI["SINIF"].ToString() + "-" + OGRENCI["AD"].ToString();
                    string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "/" + name + ".pdf");
                    newReport.ExportToPdf(path);
                }

                if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip")))
                {
                    File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));
                }

                string PATH = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "");
                System.IO.DirectoryInfo di = new DirectoryInfo(PATH);
                using (ZipArchive archive = new ZipArchive())
                {
                    foreach (FileInfo file in di.GetFiles())
                    {
                        archive.AddFile(PATH + "/" + file.Name, "/");
                    }
                    archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));
                }

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""), true);
            }
        }
    }
}
