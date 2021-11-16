using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class akYazili : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtDersListesi = new DataTable();
        DataTable dtYazili = new DataTable();
        public XRLabel lbl { get; set; }
        public akYazili(DataTable _dt1, DataTable _dt3, DataTable _dt4)
        {
            InitializeComponent();
            dtDersListesi = _dt3;
            dtYazili = _dt4;
            this.DataSource = _dt1;
        }

        private void akYazili_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float LY = 0;
            float LX = 0;
            FontFamily ff = new FontFamily("Tahoma");
            foreach (DataRow ders in dtDersListesi.Rows)
            {
                try
                {
                    DataTable dt = dtYazili.Select("ID_DERS = " + ders["ID_DERS"].ToString()).CopyToDataTable();

                    LX = 0;


                    lbl = PublicMetods.lblEkle(ders["DERSAD"].ToString(), LX, LY, 300, 20, Color.Transparent, Color.MidnightBlue, Color.SkyBlue);
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;

                    int count = dt.Select("DONEMBILGI='1. Dönem'").Length;
                    int sinav = 0;
                    bool girdi = false;
                    foreach (DataRow yazili in dt.Rows)
                    {
                        if (yazili["DONEMBILGI"].ToString().Equals("2. Dönem"))
                        {
                            if (!girdi)
                            {
                                for (int i = 0; i < 3 - count; i++)
                                {
                                    girdi = true;

                                    lbl = PublicMetods.lblEkle("", LX, LY, 232, 20, Color.Transparent, Color.MidnightBlue, Color.SkyBlue);
                                    Detail.Controls.Add(lbl);
                                    LX += lbl.WidthF;

                                    sinav++;
                                }
                            }

                            lbl = PublicMetods.lblEkle(yazili["PUAN"].ToString(), LX, LY, 232, 20, Color.Transparent, Color.MidnightBlue, Color.SkyBlue);
                            Detail.Controls.Add(lbl);
                            LX += lbl.WidthF;
                        }
                        else
                        {

                            lbl = PublicMetods.lblEkle(yazili["PUAN"].ToString(), LX, LY, 232, 20, Color.Transparent, Color.MidnightBlue, Color.SkyBlue);
                            Detail.Controls.Add(lbl);
                            LX += lbl.WidthF;
                        }
                        sinav++;
                    }
                    for (int i = sinav; i < 6; i++)
                    {
                        lbl = PublicMetods.lblEkle("", LX, LY, 232, 20, Color.Transparent, Color.MidnightBlue, Color.SkyBlue);
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }
                    LY += 20;
                }
                catch (Exception)
                {
                    LX += 232;
                }
            }
        }
    }
}
