using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPrinting;
using System.Web;
using System.IO;

namespace PusulamRapor.OnlineDers
{
    public partial class DersProgrami : DevExpress.XtraReports.UI.XtraReport
    {
        public DersProgrami()
        {
            InitializeComponent();
        }

        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string TC_OGRENCI { get; set; }
        public string ID_SINIF { get; set; }
        public string DOSYAAD { get; set; }

        public DersProgrami(string TCKIMLIKNO, string OTURUM, string TC_OGRENCI, string ID_SINIF, string DOSYAAD)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.TC_OGRENCI = TC_OGRENCI;
            this.ID_SINIF = ID_SINIF;
            this.DOSYAAD = DOSYAAD;
            InitializeComponent();
        }

        private void DersProgrami_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_SINIF", ID_SINIF);
                b.ParametreEkle("@ISLEM", 8);
                b.ParametreEkle("@ID_MENU", 1338);
                DataTable dt = b.SorguGetir("sp_OnlineDers").Tables[0];
                this.DataSource = dt;
                FillReportDataFields.FillPanel(Detail, dt);
            }
        }

        private void DersProgrami_AfterPrint(object sender, EventArgs e)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/OnlineDers/Temp/" + DOSYAAD + ".pdf")))            
                File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/OnlineDers/Temp/" + DOSYAAD + ".pdf"));
            
            string path = HttpContext.Current.Server.MapPath("/Dosyalar/OnlineDers/Temp/" + DOSYAAD + ".pdf");
            this.ExportToPdf(path, new PdfExportOptions());


        }
    }
}
