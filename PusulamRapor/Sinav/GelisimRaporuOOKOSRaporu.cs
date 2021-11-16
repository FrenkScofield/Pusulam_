using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOOKOSRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dtTEK = new DataTable();
        DataTable dtCOK = new DataTable();

        Font fontrow1 = new System.Drawing.Font(new FontFamily("Tahoma"), 16, FontStyle.Regular);
        Font fontrow2 = new System.Drawing.Font(new FontFamily("Tahoma"), 25, FontStyle.Bold);
        public GelisimRaporuOOKOSRaporu(DataTable dt1, DataTable dt2)
        {
            dtTEK = dt1;
            dtCOK = dt2;
            InitializeComponent();

            this.DataSource = dtTEK;
            FillReportDataFields.Fill(Detail, dtTEK);


            pnl_soru.Controls.Clear();


            float X = 10;
            float Y = 10;
            foreach (DataRow SORU in dtCOK.Rows)
            {
                X = 15;
                XRLabel KAZANIM = PublicMetods.lblEkle(SORU["KAZANIM"].ToString(), X, Y, 820F, (pnl_soru.HeightF / 20), Color.Transparent, Color.Black, Color.Transparent, fontrow1);
                KAZANIM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                switch (SORU["DURUM"].ToString())
                {
                    case "4":
                        X += KAZANIM.WidthF + 45;
                        break;
                    case "3":
                        X += KAZANIM.WidthF + 165;
                        break;
                    case "2":
                        X += KAZANIM.WidthF + 280;
                        break;
                    case "1":
                        X += KAZANIM.WidthF + 395;
                        break;
                }
                XRLabel DURUM = PublicMetods.lblEkle("\u221A", X, Y, 70F, (pnl_soru.HeightF / 20) , Color.Transparent, Color.Green, Color.Transparent, fontrow2);
                DURUM.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                //Y += 86f;
                Y += (pnl_soru.HeightF / 20);

                pnl_soru.Controls.Add(KAZANIM);
                pnl_soru.Controls.Add(DURUM);
            }


        }

    }
}
