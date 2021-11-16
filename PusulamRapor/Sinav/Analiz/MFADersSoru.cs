using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class MFADersSoru : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable TblVeri = new DataTable();
        DataTable TblBolumNo = new DataTable();
        public MFADersSoru(DataTable dt1,DataTable dt2)
        {
            TblVeri = dt1;
            TblBolumNo = dt2;
            InitializeComponent();

        }

        private void MFADersSoru_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = TblVeri;
            //GroupField grpField = new GroupField("SORUNO_A");
            //GroupHeader1.GroupFields.Add(grpField);


            FillReportDataFields.Fill(GroupHeader1, TblBolumNo);
            FillReportDataFields.Fill(Detail, TblVeri);
        }
        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //int bolumNo = Convert.ToInt32(GetCurrentColumnValue("BOLUMNO"));
            int soruNo = Convert.ToInt32(GetCurrentColumnValue("SORUNO_A"));
            DataTable dt = TblVeri.Select("SORUNO_A=" + soruNo).CopyToDataTable();

            xr_dersSoru.Series.Clear();
            Series srsYuzdeGenel = new Series("", ViewType.Bar);
            srsYuzdeGenel.Points.Add(new SeriesPoint("A", Convert.ToDouble(dt.Rows[0]["ASAYISI"].ToString())));
            srsYuzdeGenel.Points.Add(new SeriesPoint("B", Convert.ToDouble(dt.Rows[0]["BSAYISI"].ToString())));
            srsYuzdeGenel.Points.Add(new SeriesPoint("C", Convert.ToDouble(dt.Rows[0]["CSAYISI"].ToString())));
            srsYuzdeGenel.Points.Add(new SeriesPoint("D", Convert.ToDouble(dt.Rows[0]["DSAYISI"].ToString())));
            srsYuzdeGenel.Points.Add(new SeriesPoint("E", Convert.ToDouble(dt.Rows[0]["ESAYISI"].ToString())));


            #region Series Label


            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;


            #endregion
            xr_dersSoru.Series.Add(srsYuzdeGenel);
        }

    }
}
