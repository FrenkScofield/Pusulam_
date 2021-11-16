using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav
{
    public partial class grDersSinav:DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();
        public bool puanMi { get; set; }
        public grDersSinav(DataTable dtt,bool puan)
        {
            dt=dtt;
            puanMi=puan;

            InitializeComponent();
        }

        private void grDersSinav_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            if(puanMi)
            {
                lblSS.Text="SS";
                lblDers.Text="TOPLAM";
                lblDers.Tag="0";

                #region label color

                lblDers.BackColor=Color.PeachPuff;
                lblSS.BackColor=Color.PeachPuff;
                xrLabel1.BackColor=Color.PeachPuff;
                xrLabel3.BackColor=Color.PeachPuff;
                xrLabel5.BackColor=Color.PeachPuff;
                xrLabel6.BackColor=Color.PeachPuff;
                xrLabel7.BackColor=Color.PeachPuff;
                xrLabel8.BackColor=Color.PeachPuff;
                xrLabel9.BackColor=Color.PeachPuff;
                xrLabel10.BackColor=Color.PeachPuff;

                lblSS.BorderColor=Color.Salmon;
                lblDers.BorderColor=Color.Salmon;
                xrLabel1.BorderColor=Color.Salmon;
                xrLabel3.BorderColor=Color.Salmon;
                xrLabel5.BorderColor=Color.Salmon;
                xrLabel6.BorderColor=Color.Salmon;
                xrLabel7.BorderColor=Color.Salmon;
                xrLabel8.BorderColor=Color.Salmon;
                xrLabel9.BorderColor=Color.Salmon;
                xrLabel10.BorderColor=Color.Salmon;
                xrLabel11.BorderColor=Color.Salmon;
                xrLabel12.BorderColor=Color.Salmon;
                xrLabel13.BorderColor=Color.Salmon;
                xrLabel14.BorderColor=Color.Salmon;
                xrLabel15.BorderColor=Color.Salmon;
                xrLabel16.BorderColor=Color.Salmon;

                #endregion

                GroupField grpField = new GroupField("TCKIMLIKNO");
                GroupHeader1.GroupFields.Add(grpField);

                this.DataSource=dt;

                if(dt.Rows.Count>0)
                {
                    FillReportDataFields.Fill(GroupHeader1,dt);
                    FillReportDataFields.Fill(Detail,dt);
                }
            }
            else
            {
                lblSS.Text="SORUSAYISI";
                lblDers.Text="DERSAD";
                lblDers.Tag="1";
                GroupField grpField = new GroupField("STKISAAD");
                GroupHeader1.GroupFields.Add(grpField);

                this.DataSource=dt;

                if(dt.Rows.Count>0)
                {
                    FillReportDataFields.Fill(GroupHeader1,dt);
                    FillReportDataFields.Fill(Detail,dt);
                }

            }
        }
        public void GrafikYaz(DataTable d)
        {

            xr_dersbasari.Series.Clear();
            string baslik = "";
            Series srsYuzdeGenel = new Series(baslik,ViewType.Bar);
            if(puanMi)
            {
                srsYuzdeGenel.View.Color=Color.Salmon;
            }
            else
            {
                baslik=d.Rows[0]["STKISAAD"].ToString();
            }

            foreach(DataRow item in d.Rows)
            {
                srsYuzdeGenel.Points.Add(new SeriesPoint(item["SINAV ADI"].ToString(),Convert.ToDouble(item["YUZDE"].ToString())));
                
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

        private void GroupFooter1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable d = new DataTable();
            if(puanMi)
                d=dt;
            else
                d=dt.Select(String.Format("STKISAAD = '{0}'",GetCurrentColumnValue("STKISAAD"))).CopyToDataTable();

            GrafikYaz(d);
        }
    }

}
