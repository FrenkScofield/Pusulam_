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
using System.Text.RegularExpressions;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_OkulSoruFrekans : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtKATILIM = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");
        public OR_OkulSoruFrekans(DataTable _dt1, DataTable _dtKATILIM, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dtKATILIM = _dtKATILIM;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;
            SINAVAD = _SINAVAD;

            InitializeComponent();
            this.DataSource = dt1;
            FillReportDataFields.Fill(Detail, dt1);
        }

        private void PageHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;
            lbl_sinavad.Text = SINAVAD;
            lbl_katilimokul.Text = dtKATILIM.Rows[0]["SUBE"].ToString();
            lbl_katilimilce.Text = dtKATILIM.Rows[0]["ILCE"].ToString();
            lbl_katilimil.Text = dtKATILIM.Rows[0]["IL"].ToString();
            lbl_katilimgenel.Text = dtKATILIM.Rows[0]["GENEL"].ToString();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            A_ORAN.ForeColor = Color.Black;
            B_ORAN.ForeColor = Color.Black;
            C_ORAN.ForeColor = Color.Black;
            D_ORAN.ForeColor = Color.Black;
            E_ORAN.ForeColor = Color.Black;

            string dc = GetCurrentColumnValue("DC").ToString();
            switch (dc)
            {
                case "A":
                    if(Convert.ToDouble(GetCurrentColumnValue("A_ORAN"))<50)
                    {
                        A_ORAN.ForeColor = Color.Red;
                    }
                    break;
                case "B":
                    if (Convert.ToDouble(GetCurrentColumnValue("B_ORAN")) < 50)
                    {
                        B_ORAN.ForeColor = Color.Red;
                    }
                    break;
                case "C":
                    if (Convert.ToDouble(GetCurrentColumnValue("C_ORAN")) < 50)
                    {
                        C_ORAN.ForeColor = Color.Red;
                    }
                    break;
                case "D":
                    if (Convert.ToDouble(GetCurrentColumnValue("D_ORAN")) < 50)
                    {
                        D_ORAN.ForeColor = Color.Red;
                    }
                    break;
                case "E":
                    if (Convert.ToDouble(GetCurrentColumnValue("E_ORAN")) < 50)
                    {
                        E_ORAN.ForeColor = Color.Red;
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
