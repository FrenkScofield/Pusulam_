using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class grPuanSinav:DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public grPuanSinav(DataTable dtt)
        {
            dt=dtt;
            InitializeComponent();
        }

        private void grPuanSinav_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            GroupField grpField = new GroupField("ID_SINAVPUANTURU");
            GroupHeader1.GroupFields.Add(grpField);


            this.DataSource=dt;

            if(dt.Rows.Count>0)
            {
                FillReportDataFields.Fill(GroupHeader1,dt);
                FillReportDataFields.Fill(GroupFooter1,dt);
                FillReportDataFields.Fill(Detail,dt);
            }

        }

        private void GroupFooter1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void GroupHeader1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable d = dt.Select(String.Format("ID_SINAVPUANTURU = '{0}'",GetCurrentColumnValue("ID_SINAVPUANTURU"))).CopyToDataTable();

            xr_dersbasari.Series.Clear();
            //xr_dersbasari.BackColor=Color.PeachPuff;

            Series srsYuzdeGenel = new Series(GetCurrentColumnValue("PUANTURU").ToString(),ViewType.Bar);
            srsYuzdeGenel.View.Color=Color.PeachPuff;
            //srsYuzdeGenel

            foreach(DataRow item in d.Rows)
            {
                srsYuzdeGenel.Points.Add(new SeriesPoint(item["SINAV ADI"].ToString(),Convert.ToDouble(item["BAŞARI %"].ToString())));
            }

            #region Series Label


            srsYuzdeGenel.Label.TextOrientation=TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position=BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color=Color.Transparent;
            srsYuzdeGenel.Label.BackColor=Color.Transparent;
            srsYuzdeGenel.Label.TextColor=Color.DarkBlue;

            #endregion
            xr_dersbasari.Series.Add(srsYuzdeGenel);
        }
    }
}
