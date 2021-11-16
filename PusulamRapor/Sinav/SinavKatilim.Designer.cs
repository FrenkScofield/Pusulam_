namespace PusulamRapor.Sinav
{
    partial class SinavKatilim
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
            if(disposing&&(components!=null))
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
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.srSinavKatilimOkul = new DevExpress.XtraReports.UI.XRSubreport();
            this.srSinavKatilimKatilmayan = new DevExpress.XtraReports.UI.XRSubreport();
            this.srSinavKatilimKatilan = new DevExpress.XtraReports.UI.XRSubreport();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.srSinavKatilimKatilan,
            this.srSinavKatilimKatilmayan,
            this.srSinavKatilimOkul});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 690.5625F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Dpi = 254F;
            this.GroupHeader1.HeightF = 254F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // srSinavKatilimOkul
            // 
            this.srSinavKatilimOkul.Dpi = 254F;
            this.srSinavKatilimOkul.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            this.srSinavKatilimOkul.Name = "srSinavKatilimOkul";
            this.srSinavKatilimOkul.SizeF = new System.Drawing.SizeF(2870F, 113.8766F);
            // 
            // srSinavKatilimKatilmayan
            // 
            this.srSinavKatilimKatilmayan.Dpi = 254F;
            this.srSinavKatilimKatilmayan.LocationFloat = new DevExpress.Utils.PointFloat(0F, 226.0833F);
            this.srSinavKatilimKatilmayan.Name = "srSinavKatilimKatilmayan";
            this.srSinavKatilimKatilmayan.SizeF = new System.Drawing.SizeF(2870F, 113.8766F);
            // 
            // srSinavKatilimKatilan
            // 
            this.srSinavKatilimKatilan.Dpi = 254F;
            this.srSinavKatilimKatilan.LocationFloat = new DevExpress.Utils.PointFloat(0F, 448.3333F);
            this.srSinavKatilimKatilan.Name = "srSinavKatilimKatilan";
            this.srSinavKatilimKatilan.SizeF = new System.Drawing.SizeF(2870F, 113.8766F);
            // 
            // SinavKatilim
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1});
            this.Dpi = 254F;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 2100;
            this.PageWidth = 2970;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.SinavKatilim_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRSubreport srSinavKatilimKatilan;
        private DevExpress.XtraReports.UI.XRSubreport srSinavKatilimKatilmayan;
        private DevExpress.XtraReports.UI.XRSubreport srSinavKatilimOkul;
    }
}
