using DevExpress.XtraCharts;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav
{
    public partial class skOkulKatilim:DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();

        readonly FontFamily familyArial = new FontFamily("Arial");

        public skOkulKatilim(DataTable dtt1,DataTable dtt2)
        {
            InitializeComponent();
            dt1=dtt1;
            dt2=dtt2;


        }

        private void skOkulKatilim_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource=dt1.Select("SIRA=0").CopyToDataTable(); // GENEL OLMAYANLARI AL
        }

        private void Detail_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                xrChart1.Series.Clear();
                Series srsYuzdeGenel = new Series("GENEL",ViewType.Bar);
                

                Font font = new Font(familyArial,10,FontStyle.Bold);
                srsYuzdeGenel.Label.Font=font;

                DataRowView k = (DataRowView)this.GetCurrentRow();
                //string a = k.Row["SUBEAD"].ToString();


                foreach(DataRow item in dt2.Rows)
                {
                    
                        double katilim = Convert.ToDouble(k.Row[item["ID_SINAV"].ToString()].ToString())/Convert.ToDouble(k.Row["OGRSAY"].ToString())*100;
                        srsYuzdeGenel.Points.Add(new SeriesPoint(item["SINAVAD"].ToString(),Math.Round(katilim,2)));
                    
                }

                
                srsYuzdeGenel.Label.TextOrientation=TextOrientation.Horizontal;
                ((BarSeriesLabel)srsYuzdeGenel.Label).Position=BarSeriesLabelPosition.Top;
                srsYuzdeGenel.Label.Border.Color=Color.Transparent;
                srsYuzdeGenel.Label.BackColor=Color.Transparent;
                srsYuzdeGenel.Label.TextColor=Color.DarkBlue;
                
                xrChart1.Series.Add(srsYuzdeGenel);
                ChartTitle title = new ChartTitle();
                title.Text= k.Row["SUBEAD"].ToString();
                title.WordWrap=true;
                title.Font=new Font(familyArial,20,FontStyle.Bold);
                xrChart1.Titles.Clear();
                xrChart1.Titles.Add(title);
                
            }
            catch(Exception)
            {

                throw;
            }
            

        }
    }
}
