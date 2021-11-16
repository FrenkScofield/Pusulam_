using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class KarmaListeBilgi : DevExpress.XtraReports.UI.XtraReport
    {
        public KarmaListeBilgi(DataTable dt)
        {
            InitializeComponent();
            if (PublicMetods.orderBYtoTable(dt, "SINAVAD").Rows.Count > 0)
            {
                this.DataSource = dt;
                xrLabel_KagitAdedi.DataBindings.Add("Text", this.DataSource, "KAGITSAYISI");
                xrLabel_SinavKodu.DataBindings.Add("Text", this.DataSource, "SINAVAD");
                xrLabel_SinavTarih.DataBindings.Add("Text", this.DataSource, "TARIH");
                xrLabel_Kur.DataBindings.Add("Text", this.DataSource, "DERSSEVIYE");
                xrLabel_SinavGunu.DataBindings.Add("Text", this.DataSource, "GUN");
                xrLabel_SinavSaati.DataBindings.Add("Text", this.DataSource, "SEANS");

            }
        }

    }
}
