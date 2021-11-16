using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Abide
{
    public partial class AbidePuanRapor : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_ABIDESINAV;
        string DONEM;
        string ID_SUBE;
        string ID_SINIF;

        DataSet ds;

        public AbidePuanRapor(string TCKIMLIKNO, string OTURUM, string ID_ABIDESINAV, string DONEM, string ID_SUBE, string ID_SINIF)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_SUBE = ID_SUBE;
            this.ID_SINIF = ID_SINIF;
            this.ID_ABIDESINAV = ID_ABIDESINAV;
            this.DONEM = DONEM;
            InitializeComponent();
        }

        private void AbidePuanRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ABIDESINAV", ID_ABIDESINAV);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);
                b.ParametreEkle("@ID_SINIF", ID_SINIF);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_MENU", 1132);
                b.ParametreEkle("@ISLEM", 18);
                ds = b.SorguGetir("sp_Abide");

                DataTable DTDERS = ds.Tables[0];

                Font fontrow = new System.Drawing.Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
                Font fontrownumeric = new System.Drawing.Font(new FontFamily("Tahoma"), 10, FontStyle.Regular);
                Font dersad = new System.Drawing.Font(new FontFamily("Tahoma"), 14, FontStyle.Bold);

                float X = 0;

                #region SABITLER
                {
                    XRLabel lblkampus = new XRLabel();
                    lblkampus.SizeF = new SizeF(300, 30);
                    lblkampus.Text = "KAMPÜS";
                    lblkampus.LocationF = new PointF(X, 30);
                    lblkampus.ForeColor = Color.Red;
                    lblkampus.BackColor = Color.White;
                    lblkampus.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lblkampus.Font = dersad;
                    lblkampus.CanGrow = false;
                    lblkampus.BorderWidth = 1;
                    lblkampus.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    lblkampus.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    lblkampus.BorderColor = Color.Black;
                    GroupHeader1.Controls.Add(lblkampus);

                    X += 300;

                    XRLabel lblsinif = new XRLabel();
                    lblsinif.SizeF = new SizeF(100, 30);
                    lblsinif.Text = "SINIF";
                    lblsinif.LocationF = new PointF(X, 30);
                    lblsinif.ForeColor = Color.Red;
                    lblsinif.BackColor = Color.White;
                    lblsinif.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lblsinif.Font = dersad;
                    lblsinif.CanGrow = false;
                    lblsinif.BorderWidth = 1;
                    lblsinif.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    lblsinif.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    lblsinif.BorderColor = Color.Black;
                    GroupHeader1.Controls.Add(lblsinif);

                    X += 100;

                    XRLabel lbladsoyad = new XRLabel();
                    lbladsoyad.SizeF = new SizeF(300, 30);
                    lbladsoyad.Text = "AD - SOYAD";
                    lbladsoyad.LocationF = new PointF(X, 30);
                    lbladsoyad.ForeColor = Color.Red;
                    lbladsoyad.BackColor = Color.White;
                    lbladsoyad.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lbladsoyad.Font = dersad;
                    lbladsoyad.CanGrow = false;
                    lbladsoyad.BorderWidth = 1;
                    lbladsoyad.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    lbladsoyad.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    lbladsoyad.BorderColor = Color.Black;
                    GroupHeader1.Controls.Add(lbladsoyad);

                    X += 300;

                    XRLabel lblsube = new XRLabel();
                    lblsube.SizeF = new SizeF(200, 30);
                    lblsube.Text = "ŞUBE";
                    lblsube.LocationF = new PointF(X, 30);
                    lblsube.ForeColor = Color.Red;
                    lblsube.BackColor = Color.White;
                    lblsube.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lblsube.Font = dersad;
                    lblsube.CanGrow = false;
                    lblsube.BorderWidth = 1;
                    lblsube.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    lblsube.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    lblsube.BorderColor = Color.Black;
                    GroupHeader1.Controls.Add(lblsube);

                    X += 200;

                    XRLabel lbltc = new XRLabel();
                    lbltc.SizeF = new SizeF(160, 30);
                    lbltc.Text = "TCKİMLİKNO";
                    lbltc.LocationF = new PointF(X, 30);
                    lbltc.ForeColor = Color.Red;
                    lbltc.BackColor = Color.White;
                    lbltc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    lbltc.Font = dersad;
                    lbltc.CanGrow = false;
                    lbltc.BorderWidth = 1;
                    lbltc.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                    lbltc.Borders = DevExpress.XtraPrinting.BorderSide.All;
                    lbltc.BorderColor = Color.Black;
                    GroupHeader1.Controls.Add(lbltc);

                    X += 160;
                }
                #endregion

                foreach (DataRow item in DTDERS.Rows)
                {
                    bool ekle = false;
                    if (ds.Tables.Count > 1)
                    {
                        DataTable DT = ds.Tables[1];
                        foreach (DataColumn col in DT.Columns)
                        {
                            if (col.ColumnName.Contains(item["DERS"].ToString()))
                            {
                                ekle = true;
                            }
                        }
                    }
                    else
                    {
                        ekle = true;
                    }

                    if (ekle)
                    {
                        XRLabel lbltc = new XRLabel();
                        lbltc.SizeF = new SizeF(50 * Convert.ToInt32(item["SORUNO"]), 30);
                        lbltc.Text = item["DERS"].ToString();
                        lbltc.LocationF = new PointF(X, 0);
                        lbltc.ForeColor = Color.Red;
                        lbltc.BackColor = Color.White;
                        lbltc.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        lbltc.Font = dersad;
                        lbltc.CanGrow = false;
                        lbltc.BorderWidth = 1;
                        lbltc.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                        lbltc.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        lbltc.BorderColor = Color.Black;
                        GroupHeader1.Controls.Add(lbltc);
                        float tempx = X;

                        for (int i = 0; i < Convert.ToInt32(item["SORUNO"]); i++)
                        {
                            XRLabel lbl = new XRLabel();
                            lbl.SizeF = new SizeF(50, 30);
                            lbl.Text = "S" + (i + 1);
                            lbl.LocationF = new PointF(tempx, 30);
                            lbl.ForeColor = Color.Red;
                            lbl.BackColor = Color.White;

                            lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                            lbl.Font = dersad;
                            lbl.CanGrow = false;
                            lbl.BorderWidth = 1;
                            lbl.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                            lbl.Borders = DevExpress.XtraPrinting.BorderSide.All;
                            lbl.BorderColor = Color.Black;
                            GroupHeader1.Controls.Add(lbl);

                            tempx += 50;
                        }

                        X += 50 * Convert.ToInt32(item["SORUNO"]);
                    }                    
                }

                if (ds.Tables.Count > 1)
                {
                    DataTable DT = ds.Tables[1];
                    X = 0;
                    int i = 0;
                    float width = 0;
                    foreach (DataColumn item in DT.Columns)
                    {
                        if (i == 0)
                        {
                            width = 300;
                        }
                        else if (i == 1)
                        {
                            width = 100;
                        }
                        else if (i == 2)
                        {
                            width = 300;
                        }
                        else if (i == 3)
                        {
                            width = 200;
                        }
                        else if (i == 4)
                        {
                            width = 160;
                        }
                        else
                        {
                            width = 50;
                        }

                        i++;

                        XRLabel lbl = new XRLabel();
                        lbl.SizeF = new SizeF(width, 30);
                        lbl.Text = item.ColumnName;
                        lbl.LocationF = new PointF(X, 0);
                        lbl.ForeColor = Color.Black;
                        lbl.BackColor = Color.White;

                        lbl.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                        lbl.Font = fontrow;
                        lbl.Tag = 1;
                        lbl.CanGrow = false;
                        lbl.BorderWidth = 1;
                        lbl.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
                        lbl.Borders = DevExpress.XtraPrinting.BorderSide.All;
                        lbl.BorderColor = Color.Black;
                        Detail.Controls.Add(lbl);

                        X += width;
                    }

                    this.DataSource = DT;
                    FillReportDataFields.Fill(Detail, DT);
                }
            }
        }
    }
}
