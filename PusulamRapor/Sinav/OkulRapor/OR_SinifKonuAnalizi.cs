using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using System.Linq;
using System.Text.RegularExpressions;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_SinifKonuAnalizi : XtraReport
    {
        DataTable dt = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_SinifKonuAnalizi(DataTable _dt, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt = _dt;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;
            SINAVAD = _SINAVAD;

            InitializeComponent();
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string SINIF = GetCurrentColumnValue("SINIF").ToString();
            SINIFAD.Text = SINIF;
        }

        private void OR_OkulKonuAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;
            lbl_sinavAd.Text = SINAVAD;

            this.DataSource = dt;
            GroupField sinif = new GroupField("SINIF");
            GroupHeader1.GroupFields.Add(sinif);

            //DataTable table1 = dt.Select(string.Format("SINIF='{0}'", SINIF)).CopyToDataTable();
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
