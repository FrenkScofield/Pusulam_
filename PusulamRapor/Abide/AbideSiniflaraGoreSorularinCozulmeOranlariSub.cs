using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using System.Web;

namespace PusulamRapor.Abide
{
    public partial class AbideSiniflaraGoreSorularinCozulmeOranlariSub : DevExpress.XtraReports.UI.XtraReport
    {
        string OTURUM = "";
        string DERS = "";
        DataTable table;

        int i;
        public AbideSiniflaraGoreSorularinCozulmeOranlariSub(DataTable table, string OTURUM, string DERS)
        {
            this.table = table;
            this.OTURUM = OTURUM;
            this.DERS = DERS;
            InitializeComponent();

            Font FONTHEADER = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Bold);
            Font FONTROW = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Regular);

            float X = 0F;
            float Y = 0F;
            foreach (DataColumn COL in table.Columns)
            {
                XRLabel LABEL = PublicMetods.lblEkle(COL.ColumnName, X, Y, 250, 150, Color.FromArgb(255, 61, 133, 198), Color.White, Color.White, FONTHEADER);
                GroupHeader1.Controls.Add(LABEL);

                X += LABEL.WidthF;
            }

            foreach (DataRow ROW in table.Rows)
            {
                int i = 0;
                X = 0;
                foreach (DataColumn COL in table.Columns)
                {
                    string value = ROW[COL.ColumnName].ToString();
                    Color BACKCOLOR = Color.White;

                    if (i > 1)
                    {
                        if (Convert.ToDouble(value) < 50)
                        {
                            BACKCOLOR = Color.FromArgb(255, 244, 204, 204);
                        }
                        value += "%";
                    }

                    XRLabel LABELDETAIL = PublicMetods.lblEkle(value, X, Y, 250, 23, BACKCOLOR, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTROW);
                    Detail.Controls.Add(LABELDETAIL);

                    X += LABELDETAIL.WidthF;
                    i++;
                }
                Y += 23;
            }
        }

        private void AbideSiniflaraGoreSorularinCozulmeOranlariSub_AfterPrint(object sender, EventArgs e)
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""));

            string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "/" + DERS + ".XLS");
            this.ExportToXls(path);
        }
    }
}
