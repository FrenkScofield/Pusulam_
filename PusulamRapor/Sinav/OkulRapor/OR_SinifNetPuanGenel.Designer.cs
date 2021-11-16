namespace PusulamRapor.Sinav.OkulRapor
{
    partial class OR_SinifNetPuanGenel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrSubreport_SinifNetListesi = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrSubreport_SinifPuanListesi = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrPageBreak1 = new DevExpress.XtraReports.UI.XRPageBreak();
            this.xrPageBreak2 = new DevExpress.XtraReports.UI.XRPageBreak();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageBreak2,
            this.xrPageBreak1,
            this.xrSubreport_SinifPuanListesi,
            this.xrSubreport_SinifNetListesi});
            this.Detail.HeightF = 69.75926F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrSubreport_SinifNetListesi
            // 
            this.xrSubreport_SinifNetListesi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrSubreport_SinifNetListesi.Name = "xrSubreport_SinifNetListesi";
            this.xrSubreport_SinifNetListesi.SizeF = new System.Drawing.SizeF(1732F, 32.25926F);
            // 
            // xrSubreport_SinifPuanListesi
            // 
            this.xrSubreport_SinifPuanListesi.LocationFloat = new DevExpress.Utils.PointFloat(0F, 37.5F);
            this.xrSubreport_SinifPuanListesi.Name = "xrSubreport_SinifPuanListesi";
            this.xrSubreport_SinifPuanListesi.SizeF = new System.Drawing.SizeF(1732F, 32.25926F);
            // 
            // xrPageBreak1
            // 
            this.xrPageBreak1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 67.75925F);
            this.xrPageBreak1.Name = "xrPageBreak1";
            // 
            // xrPageBreak2
            // 
            this.xrPageBreak2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 35.5F);
            this.xrPageBreak2.Name = "xrPageBreak2";
            // 
            // OR_SinifNetPuanGenel
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 2200;
            this.PageWidth = 1732;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport_SinifNetListesi;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport_SinifPuanListesi;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak1;
        private DevExpress.XtraReports.UI.XRPageBreak xrPageBreak2;
    }
}
