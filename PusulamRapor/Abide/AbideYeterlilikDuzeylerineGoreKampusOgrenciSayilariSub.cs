using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Web;
using System.IO;

namespace PusulamRapor.Abide
{
    public partial class AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilariSub : DevExpress.XtraReports.UI.XtraReport
    {
        string OTURUM = "";
        string DERS = "";
        public AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilariSub(DataTable DTSONUCDERS, DataTable DTDUZEY, DataTable DTKAMPUS, DataTable DTSINAV, string OTURUM, string DERS)
        {
            this.OTURUM = OTURUM;
            this.DERS = DERS;
            InitializeComponent();

            Font FONTHEADERX = new System.Drawing.Font(new FontFamily("VERDANA"), 12, FontStyle.Bold);
            Font FONTHEADER = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Bold);
            Font FONTROW = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Regular);

            float X = 0F;
            float Y = 0F;
            float TEMPX = 0F;

            #region HEADER

            ReportHeader.Controls.Add(PublicMetods.lblEkle("YETERLİK DÜZEYLERİNE GÖRE KAMPÜS ÖĞRENCİ SAYILARI - " + DERS, X, Y, 3000, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADERX));

            Y += 23;
            X = 0;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("KAMPÜS ADI", X, Y, 300, 23 * 5, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
            X = 300;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("TOPLAM" + Environment.NewLine + "ÖĞRENCİ" + Environment.NewLine + "SAYISI", X, Y, 150, 23 * 5, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
            X += 150;

            float DUZEYWIDTH = 2550f / (float)DTDUZEY.Rows.Count;
            TEMPX = X;
            foreach (DataRow DUZEY in DTDUZEY.Rows)
            {
                ReportHeader.Controls.Add(PublicMetods.lblEkle(DUZEY["DUZEY"] + ". DÜZEY", TEMPX, Y, DUZEYWIDTH, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                TEMPX += DUZEYWIDTH;
            }
            TEMPX = X;
            Y += 23;

            foreach (DataRow DUZEY in DTDUZEY.Rows)
            {
                foreach (DataRow SINAV in DTSINAV.Rows)
                {
                    ReportHeader.Controls.Add(PublicMetods.lblEkle(SINAV["SINAVAD"].ToString(), TEMPX, Y, (DUZEYWIDTH) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                    TEMPX += (DUZEYWIDTH) / (float)DTSINAV.Rows.Count;
                }
            }
            TEMPX = X;
            Y += 23;

            foreach (DataRow DUZEY in DTDUZEY.Rows)
            {
                foreach (DataRow SINAV in DTSINAV.Rows)
                {
                    ReportHeader.Controls.Add(PublicMetods.lblEkle("Öğrenci Sayısı", TEMPX, Y, (((DUZEYWIDTH) / (float)DTSINAV.Rows.Count) / 2f), 23 * 3, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                    TEMPX += ((DUZEYWIDTH) / (float)DTSINAV.Rows.Count) / 2f;
                    ReportHeader.Controls.Add(PublicMetods.lblEkle("Toplam Öğrenciye" + Environment.NewLine + "Oranı (%)", TEMPX, Y, (((DUZEYWIDTH) / (float)DTSINAV.Rows.Count) / 2f), 23 * 3, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                    TEMPX += ((DUZEYWIDTH) / (float)DTSINAV.Rows.Count) / 2f;
                }
            }
            TEMPX = X;
            Y += 23 * 3;

            #endregion

            X = 0F;
            Y = 0F;
            TEMPX = 0F;

            #region ROWS
            foreach (DataRow KAMPUS in DTKAMPUS.Rows)
            {
                X = 0F;
                string KAMPUSAD = KAMPUS["KAMPUS"].ToString();
                Detail.Controls.Add(PublicMetods.lblEkle(KAMPUSAD, X, Y, 300, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                X += 300;

                if (DTSONUCDERS.Select("KAMPUS='" + KAMPUSAD + "'").Length > 0)//TOPLAM ÖĞRENCİ SAYISI
                {
                    DataTable DTKAMPUSTEK = DTSONUCDERS.Select("KAMPUS='" + KAMPUSAD + "'").CopyToDataTable();
                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSTEK.Rows[0]["TOPLAMOGRENCISAYISI"].ToString(), X, Y, 150, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                    X += 150;

                    foreach (DataRow DUZEY in DTDUZEY.Rows)
                    {
                        if (DTKAMPUSTEK.Select("DUZEY='" + DUZEY["DUZEY"]+"'").Length > 0)
                        {
                            DataTable DTKAMPUSDUZEYTEK = DTKAMPUSTEK.Select("DUZEY='" + DUZEY["DUZEY"]+ "'").CopyToDataTable();
                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                if (DTKAMPUSDUZEYTEK.Select("SINAVAD='" + SINAV["SINAVAD"] + "'").Length > 0)
                                {
                                    DataTable DTKAMPUSDUZEYSINAVTEK = DTKAMPUSDUZEYTEK.Select("SINAVAD='" + SINAV["SINAVAD"] + "'").CopyToDataTable();
                                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSDUZEYSINAVTEK.Rows[0]["DUZEYOGRENCISAYISI"].ToString(), X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;

                                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSDUZEYSINAVTEK.Rows[0]["ORAN"].ToString() + "%", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                                }
                                else
                                {
                                    Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;

                                    Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                                X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                            }

                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                                X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                            }
                        }
                    }
                }

                Y += 23;
            }
            #endregion

            X = 0F;
            #region TOTALS
            {
                Detail.Controls.Add(PublicMetods.lblEkle("TOPLAM", X, Y, 300, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                X += 300;

                if (DTSONUCDERS.Select("KAMPUS='" + "TOPLAM" + "'").Length > 0)//TOPLAM ÖĞRENCİ SAYISI
                {
                    DataTable DTKAMPUSTEK = DTSONUCDERS.Select("KAMPUS='" + "TOPLAM" + "'").CopyToDataTable();
                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSTEK.Rows[0]["TOPLAMOGRENCISAYISI"].ToString(), X, Y, 150, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                    X += 150;

                    foreach (DataRow DUZEY in DTDUZEY.Rows)
                    {
                        if (DTKAMPUSTEK.Select("DUZEY='" + DUZEY["DUZEY"]+"'").Length > 0)
                        {
                            DataTable DTKAMPUSDUZEYTEK = DTKAMPUSTEK.Select("DUZEY='" + DUZEY["DUZEY"]+"'").CopyToDataTable();
                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                if (DTKAMPUSDUZEYTEK.Select("SINAVAD='" + SINAV["SINAVAD"] + "'").Length > 0)
                                {
                                    DataTable DTKAMPUSDUZEYSINAVTEK = DTKAMPUSDUZEYTEK.Select("SINAVAD='" + SINAV["SINAVAD"] + "'").CopyToDataTable();
                                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSDUZEYSINAVTEK.Rows[0]["DUZEYOGRENCISAYISI"].ToString(), X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;

                                    Detail.Controls.Add(PublicMetods.lblEkle(DTKAMPUSDUZEYSINAVTEK.Rows[0]["ORAN"].ToString() + "%", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                                }
                                else
                                {
                                    Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;

                                    Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW));
                                    X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                                }
                            }
                        }
                        else
                        {
                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                                X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                            }

                            foreach (DataRow SINAV in DTSINAV.Rows)
                            {
                                Detail.Controls.Add(PublicMetods.lblEkle("-", X, Y, (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count, 23, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER));
                                X += (DUZEYWIDTH / 2f) / (float)DTSINAV.Rows.Count;
                            }
                        }
                    }
                }

                Y += 23;
            }
            #endregion

        }

        private void AbideYeterlilikDuzeylerineGoreKampusOgrenciSayilariSub_AfterPrint(object sender, EventArgs e)
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""));

            string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "/" + DERS + ".XLS");
            this.ExportToXls(path);
        }
    }
}
