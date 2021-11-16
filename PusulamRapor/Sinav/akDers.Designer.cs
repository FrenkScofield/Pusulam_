namespace PusulamRapor.Sinav {
    partial class akDers {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.lblSinavTuru = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
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
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblSinavTuru});
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
            this.GroupHeader1.HeightF = 30F;
            this.GroupHeader1.KeepTogether = true;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.RepeatEveryPage = true;
            this.GroupHeader1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupHeader1_BeforePrint);
            // 
            // lblSinavTuru
            // 
            this.lblSinavTuru.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(114)))), ((int)(((byte)(196)))));
            this.lblSinavTuru.BorderColor = System.Drawing.Color.White;
            this.lblSinavTuru.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblSinavTuru.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSinavTuru.ForeColor = System.Drawing.Color.White;
            this.lblSinavTuru.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblSinavTuru.Name = "lblSinavTuru";
            this.lblSinavTuru.Padding = new DevExpress.XtraPrinting.PaddingInfo(8, 8, 8, 8, 100F);
            this.lblSinavTuru.SizeF = new System.Drawing.SizeF(1692F, 20.83333F);
            this.lblSinavTuru.StylePriority.UseBackColor = false;
            this.lblSinavTuru.StylePriority.UseBorderColor = false;
            this.lblSinavTuru.StylePriority.UseBorders = false;
            this.lblSinavTuru.StylePriority.UseFont = false;
            this.lblSinavTuru.StylePriority.UseForeColor = false;
            this.lblSinavTuru.StylePriority.UsePadding = false;
            this.lblSinavTuru.StylePriority.UseTextAlignment = false;
            this.lblSinavTuru.Tag = "1";
            this.lblSinavTuru.Text = "SINAVTURU";
            this.lblSinavTuru.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // akDers
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            this.PageHeight = 1700;
            this.PageWidth = 1692;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel lblSinavTuru;
    }
}
