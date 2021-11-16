using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class ssPuanSirala : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt = new DataTable();



        public ssPuanSirala(DataTable _dt, bool BURSPUAN, bool BURSSIRALAMA)
        {
            dt = _dt;
            InitializeComponent();
            if (!BURSPUAN)
            {
                lblPuan.Visible = false;
                xrLabel_PuanAd2.Visible = false;

                xrLabel_PuanAd1.WidthF += xrLabel_PuanAd2.WidthF;
                lblPuanTuru.WidthF += lblPuan.WidthF;
            }
            if (!BURSSIRALAMA)
            {
                xrLabel5.Visible = false;
                xrLabel_PuanAd3.Visible = false;
                xrLabel_PuanAd4.Visible = false;
                xrLabel_PuanAd5.Visible = false;
                xrLabel_PuanAd6.Visible = false;
                xrLabel_PuanAd7.Visible = false;


                lblSinifSira.Visible = false;
                lblOkulSira.Visible = false;
                lblIlceSira.Visible = false;
                lblIlSira.Visible = false;
                lblGenelSira.Visible = false;

            }
        }

        private void ssPuanSirala_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            this.DataSource = dt;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lblPuanTuru.DataBindings.Add("Text", this.DataSource, "PUANTURU");
            lblPuan.DataBindings.Add("Text", this.DataSource, "PUAN");
            lblSinifSira.DataBindings.Add("Text", this.DataSource, "SINIFSIRA");
            lblOkulSira.DataBindings.Add("Text", this.DataSource, "OKULSIRA");
            lblIlceSira.DataBindings.Add("Text", this.DataSource, "ILCESIRA");
            lblIlSira.DataBindings.Add("Text", this.DataSource, "ILSIRA");
            lblGenelSira.DataBindings.Add("Text", this.DataSource, "GENELSIRA");
        }
    }
}
