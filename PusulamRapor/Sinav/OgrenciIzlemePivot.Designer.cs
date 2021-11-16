namespace PusulamRapor.Sinav
{
    partial class OgrenciIzlemePivot
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
            this.pivotGrid = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.pivotGrid});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 254F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // pivotGrid
            // 
            this.pivotGrid.Dpi = 254F;
            this.pivotGrid.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.pivotGrid.Name = "pivotGrid";
            this.pivotGrid.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.pivotGrid.OptionsPrint.MergeRowFieldValues = false;
            this.pivotGrid.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.pivotGrid.OptionsView.ShowColumnGrandTotalHeader = false;
            this.pivotGrid.OptionsView.ShowColumnGrandTotals = false;
            this.pivotGrid.OptionsView.ShowColumnHeaders = false;
            this.pivotGrid.OptionsView.ShowColumnTotals = false;
            this.pivotGrid.OptionsView.ShowDataHeaders = false;
            this.pivotGrid.OptionsView.ShowFilterHeaders = false;
            this.pivotGrid.OptionsView.ShowRowGrandTotalHeader = false;
            this.pivotGrid.OptionsView.ShowRowGrandTotals = false;
            this.pivotGrid.OptionsView.ShowRowHeaders = false;
            this.pivotGrid.OptionsView.ShowRowTotals = false;
            this.pivotGrid.SizeF = new System.Drawing.SizeF(2870F, 254F);
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
            // OgrenciIzlemePivot
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.Dpi = 254F;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 2100;
            this.PageWidth = 2970;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRPivotGrid pivotGrid;
    }
}
