using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.IO;
using DevExpress.Compression;
using System.Web;

namespace PusulamRapor.Abide
{
    public partial class AbidePuanaGoreGenelSonucListeSub : DevExpress.XtraReports.UI.XtraReport
    {
        string OTURUM = "";
        string DERS = "";
        public AbidePuanaGoreGenelSonucListeSub(DataTable table, string OTURUM, string DERS)
        {
            this.OTURUM = OTURUM;
            this.DERS = DERS;
            InitializeComponent();

            Font FONTHEADER = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Bold);
            Font FONTROW = new System.Drawing.Font(new FontFamily("VERDANA"), 10, FontStyle.Regular);

            float X = 0F;
            float Y = 0F;
            int i = 1;
            foreach (DataColumn COL in table.Columns)
            {
                int width = i < 5 ? 250 : 120;
                i++;
                XRLabel LABEL = PublicMetods.lblEkle(COL.ColumnName, X, Y, width, 50, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), FONTHEADER);
                GroupHeader1.Controls.Add(LABEL);

                XRLabel LABELDETAIL = PublicMetods.lblEkle(COL.ColumnName, X, Y, width, 23, Color.White, Color.FromArgb(255, 36, 64, 97), Color.FromArgb(255, 36, 64, 97), "1", FONTROW);
                Detail.Controls.Add(LABELDETAIL);

                X += LABEL.WidthF;
            }

            GroupHeader1.GroupFields.Add(new GroupField("TCKIMLIKNO"));
            this.DataSource = table;
            FillReportDataFields.Fill(Detail, table);
        }

        private void AbidePuanaGoreGenelSonucListeSub_AfterPrint(object sender, EventArgs e)
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""));

            string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "/" + DERS + ".XLS");
            this.ExportToXls(path);            
        }
    }
}
