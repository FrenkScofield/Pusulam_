using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporuOODersSinavGrafikli : DevExpress.XtraReports.UI.XtraReport
    {
        public GelisimRaporuOODersSinavGrafikli(object rep, int tip,  XRChart ch)
        {
            InitializeComponent();

            if (tip==1)
            {
                xrSubreport1.ReportSource = (GelisimRaporuOODersSinav)rep;
            }
            else
            {
                xrSubreport1.ReportSource = (GelisimRaporuOOPuanSinav)rep;
            }

            //xrSubreport1.ReportSource = tip == 2 ? (GelisimRaporuOOPuanSinav)rep : (GelisimRaporuOODersSinav)rep;

            Detail.Controls.Add(ch);

        }

    }
}
