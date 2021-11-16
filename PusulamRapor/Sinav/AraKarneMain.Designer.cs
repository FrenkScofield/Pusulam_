namespace PusulamRapor.Sinav
{
    partial class AraKarneMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AraKarneMain));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrAraKarne = new DevExpress.XtraReports.UI.XRSubreport();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrAraKarne});
            this.Detail.HeightF = 100F;
            this.Detail.KeepTogether = true;
            this.Detail.KeepTogetherWithDetailReports = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrAraKarne
            // 
            this.xrAraKarne.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrAraKarne.Name = "xrAraKarne";
            this.xrAraKarne.SizeF = new System.Drawing.SizeF(1692F, 100F);
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
            // GroupHeader1
            // 
            this.GroupHeader1.HeightF = 100F;
            this.GroupHeader1.Level = 2;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            this.GroupHeader1.AfterPrint += new System.EventHandler(this.GroupHeader1_AfterPrint);
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 62.50002F;
            this.GroupFooter1.Level = 2;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.AfterPrint += new System.EventHandler(this.GroupFooter1_AfterPrint);
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.HeightF = 100F;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.HeightF = 155.3237F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // AraKarneMain
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1,
            this.GroupHeader2,
            this.GroupFooter2});
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 0, 0);
            this.PageHeight = 1200;
            this.PageWidth = 1732;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "15.1";
            //this.Watermark.Image = ((System.Drawing.Image)(resources.GetObject("AraKarneMain.Watermark.Image")));
            //this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.AraKarneMain_BeforePrint);
            this.AfterPrint += new System.EventHandler(this.AraKarneMain_AfterPrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRSubreport xrAraKarne;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
    }
}
