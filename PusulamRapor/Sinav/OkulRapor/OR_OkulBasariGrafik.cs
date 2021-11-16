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
    public partial class OR_OkulBasariGrafik : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_OkulBasariGrafik(DataTable _dt, string _SUBEAD, string _SUBEIL, string _SUBEILCE, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt = _dt;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;

            InitializeComponent();
        }

        private void OR_OkulBasariGrafik_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;

            xr_okulbasari.Series.Clear();

            DataView view = new DataView(dt);


            Series s = new Series("Dersler", ViewType.Line);
            s.Label.TextPattern = "{V:0.00}%";
            foreach (string ders in dersUzun)
            {
                DataTable ndt = dt.Select(string.Format("TAKMAAD='{0}'", ders)).CopyToDataTable();
                foreach (DataRow dr in ndt.Rows)
                {
                    Double value = Convert.ToDouble(ndt.Select("TAKMAAD='" + ders + "'").CopyToDataTable().Rows[0]["BASARIORAN"]);
                    s.Points.Add(new SeriesPoint(ders.Substring(0, 3) + Regex.Match(ders, @"\d+").Value, value));
                }
            }
            s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            s.Label.TextAlignment = StringAlignment.Center;
            s.ShowInLegend = false;
            xr_okulbasari.Series.Add(s);
            XYDiagram diagram = (XYDiagram)xr_okulbasari.Diagram;
            diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
        }
    }
}
