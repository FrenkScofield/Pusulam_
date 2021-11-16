namespace PusulamRapor.Viu
{
    partial class Mesaj
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
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.xrTarih = new DevExpress.XtraReports.UI.XRLabel();
            this.GONDERENTIP = new DevExpress.XtraReports.UI.XRLabel();
            this.GONDERENSUBE = new DevExpress.XtraReports.UI.XRLabel();
            this.KYE_TARIH = new DevExpress.XtraReports.UI.XRLabel();
            this.ACIKLAMA = new DevExpress.XtraReports.UI.XRLabel();
            this.KIME = new DevExpress.XtraReports.UI.XRLabel();
            this.KIMICIN = new DevExpress.XtraReports.UI.XRLabel();
            this.GONDEREN = new DevExpress.XtraReports.UI.XRLabel();
            this.KIMETIP = new DevExpress.XtraReports.UI.XRLabel();
            this.GONDERENTC = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 60F;
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.Tag = "1";
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrPanel1
            // 
            this.xrPanel1.CanGrow = false;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTarih,
            this.GONDERENTIP,
            this.GONDERENSUBE,
            this.KYE_TARIH,
            this.ACIKLAMA,
            this.KIME,
            this.KIMICIN,
            this.GONDEREN,
            this.KIMETIP,
            this.GONDERENTC});
            this.xrPanel1.Dpi = 254F;
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(2845F, 60F);
            this.xrPanel1.Tag = "1";
            // 
            // xrTarih
            // 
            this.xrTarih.BackColor = System.Drawing.Color.LightGray;
            this.xrTarih.BorderColor = System.Drawing.Color.White;
            this.xrTarih.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTarih.Dpi = 254F;
            this.xrTarih.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTarih.ForeColor = System.Drawing.Color.Black;
            this.xrTarih.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTarih.Name = "xrTarih";
            this.xrTarih.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrTarih.SizeF = new System.Drawing.SizeF(80F, 60F);
            this.xrTarih.StylePriority.UseBackColor = false;
            this.xrTarih.StylePriority.UseBorderColor = false;
            this.xrTarih.StylePriority.UseBorders = false;
            this.xrTarih.StylePriority.UseFont = false;
            this.xrTarih.StylePriority.UseForeColor = false;
            this.xrTarih.StylePriority.UseTextAlignment = false;
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.RecordNumber;
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTarih.Summary = xrSummary1;
            this.xrTarih.Tag = "0";
            this.xrTarih.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GONDERENTIP
            // 
            this.GONDERENTIP.BackColor = System.Drawing.Color.LightGray;
            this.GONDERENTIP.BorderColor = System.Drawing.Color.White;
            this.GONDERENTIP.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.GONDERENTIP.Dpi = 254F;
            this.GONDERENTIP.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GONDERENTIP.ForeColor = System.Drawing.Color.Black;
            this.GONDERENTIP.LocationFloat = new DevExpress.Utils.PointFloat(880F, 0F);
            this.GONDERENTIP.Name = "GONDERENTIP";
            this.GONDERENTIP.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.GONDERENTIP.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.GONDERENTIP.StylePriority.UseBackColor = false;
            this.GONDERENTIP.StylePriority.UseBorderColor = false;
            this.GONDERENTIP.StylePriority.UseBorders = false;
            this.GONDERENTIP.StylePriority.UseFont = false;
            this.GONDERENTIP.StylePriority.UseForeColor = false;
            this.GONDERENTIP.StylePriority.UsePadding = false;
            this.GONDERENTIP.StylePriority.UseTextAlignment = false;
            this.GONDERENTIP.Tag = "1";
            this.GONDERENTIP.Text = "GONDERENTIP";
            this.GONDERENTIP.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GONDERENSUBE
            // 
            this.GONDERENSUBE.BackColor = System.Drawing.Color.LightGray;
            this.GONDERENSUBE.BorderColor = System.Drawing.Color.White;
            this.GONDERENSUBE.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.GONDERENSUBE.Dpi = 254F;
            this.GONDERENSUBE.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GONDERENSUBE.ForeColor = System.Drawing.Color.Black;
            this.GONDERENSUBE.LocationFloat = new DevExpress.Utils.PointFloat(579.9999F, 0F);
            this.GONDERENSUBE.Name = "GONDERENSUBE";
            this.GONDERENSUBE.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.GONDERENSUBE.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.GONDERENSUBE.StylePriority.UseBackColor = false;
            this.GONDERENSUBE.StylePriority.UseBorderColor = false;
            this.GONDERENSUBE.StylePriority.UseBorders = false;
            this.GONDERENSUBE.StylePriority.UseFont = false;
            this.GONDERENSUBE.StylePriority.UseForeColor = false;
            this.GONDERENSUBE.StylePriority.UsePadding = false;
            this.GONDERENSUBE.StylePriority.UseTextAlignment = false;
            this.GONDERENSUBE.Tag = "1";
            this.GONDERENSUBE.Text = "GONDERENSUBE";
            this.GONDERENSUBE.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // KYE_TARIH
            // 
            this.KYE_TARIH.BackColor = System.Drawing.Color.LightGray;
            this.KYE_TARIH.BorderColor = System.Drawing.Color.White;
            this.KYE_TARIH.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.KYE_TARIH.Dpi = 254F;
            this.KYE_TARIH.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KYE_TARIH.ForeColor = System.Drawing.Color.Black;
            this.KYE_TARIH.LocationFloat = new DevExpress.Utils.PointFloat(2545F, 0F);
            this.KYE_TARIH.Name = "KYE_TARIH";
            this.KYE_TARIH.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.KYE_TARIH.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.KYE_TARIH.StylePriority.UseBackColor = false;
            this.KYE_TARIH.StylePriority.UseBorderColor = false;
            this.KYE_TARIH.StylePriority.UseBorders = false;
            this.KYE_TARIH.StylePriority.UseFont = false;
            this.KYE_TARIH.StylePriority.UseForeColor = false;
            this.KYE_TARIH.StylePriority.UsePadding = false;
            this.KYE_TARIH.StylePriority.UseTextAlignment = false;
            this.KYE_TARIH.Tag = "1";
            this.KYE_TARIH.Text = "KYE_TARIH";
            this.KYE_TARIH.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // ACIKLAMA
            // 
            this.ACIKLAMA.BackColor = System.Drawing.Color.LightGray;
            this.ACIKLAMA.BorderColor = System.Drawing.Color.White;
            this.ACIKLAMA.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.ACIKLAMA.Dpi = 254F;
            this.ACIKLAMA.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ACIKLAMA.ForeColor = System.Drawing.Color.Black;
            this.ACIKLAMA.LocationFloat = new DevExpress.Utils.PointFloat(2080F, 0F);
            this.ACIKLAMA.Name = "ACIKLAMA";
            this.ACIKLAMA.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.ACIKLAMA.SizeF = new System.Drawing.SizeF(465F, 60F);
            this.ACIKLAMA.StylePriority.UseBackColor = false;
            this.ACIKLAMA.StylePriority.UseBorderColor = false;
            this.ACIKLAMA.StylePriority.UseBorders = false;
            this.ACIKLAMA.StylePriority.UseFont = false;
            this.ACIKLAMA.StylePriority.UseForeColor = false;
            this.ACIKLAMA.StylePriority.UsePadding = false;
            this.ACIKLAMA.StylePriority.UseTextAlignment = false;
            this.ACIKLAMA.Tag = "1";
            this.ACIKLAMA.Text = "ACIKLAMA";
            this.ACIKLAMA.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ACIKLAMA.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ACIKLAMA_BeforePrint);
            // 
            // KIME
            // 
            this.KIME.BackColor = System.Drawing.Color.LightGray;
            this.KIME.BorderColor = System.Drawing.Color.White;
            this.KIME.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.KIME.Dpi = 254F;
            this.KIME.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KIME.ForeColor = System.Drawing.Color.Black;
            this.KIME.LocationFloat = new DevExpress.Utils.PointFloat(1480F, 0F);
            this.KIME.Name = "KIME";
            this.KIME.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.KIME.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.KIME.StylePriority.UseBackColor = false;
            this.KIME.StylePriority.UseBorderColor = false;
            this.KIME.StylePriority.UseBorders = false;
            this.KIME.StylePriority.UseFont = false;
            this.KIME.StylePriority.UseForeColor = false;
            this.KIME.StylePriority.UsePadding = false;
            this.KIME.StylePriority.UseTextAlignment = false;
            this.KIME.Tag = "1";
            this.KIME.Text = "KIME";
            this.KIME.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // KIMICIN
            // 
            this.KIMICIN.BackColor = System.Drawing.Color.LightGray;
            this.KIMICIN.BorderColor = System.Drawing.Color.White;
            this.KIMICIN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.KIMICIN.Dpi = 254F;
            this.KIMICIN.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KIMICIN.ForeColor = System.Drawing.Color.Black;
            this.KIMICIN.LocationFloat = new DevExpress.Utils.PointFloat(1180F, 0F);
            this.KIMICIN.Name = "KIMICIN";
            this.KIMICIN.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.KIMICIN.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.KIMICIN.StylePriority.UseBackColor = false;
            this.KIMICIN.StylePriority.UseBorderColor = false;
            this.KIMICIN.StylePriority.UseBorders = false;
            this.KIMICIN.StylePriority.UseFont = false;
            this.KIMICIN.StylePriority.UseForeColor = false;
            this.KIMICIN.StylePriority.UsePadding = false;
            this.KIMICIN.StylePriority.UseTextAlignment = false;
            this.KIMICIN.Tag = "1";
            this.KIMICIN.Text = "KIMICIN";
            this.KIMICIN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GONDEREN
            // 
            this.GONDEREN.BackColor = System.Drawing.Color.LightGray;
            this.GONDEREN.BorderColor = System.Drawing.Color.White;
            this.GONDEREN.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.GONDEREN.Dpi = 254F;
            this.GONDEREN.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GONDEREN.ForeColor = System.Drawing.Color.Black;
            this.GONDEREN.LocationFloat = new DevExpress.Utils.PointFloat(280F, 0F);
            this.GONDEREN.Name = "GONDEREN";
            this.GONDEREN.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.GONDEREN.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.GONDEREN.StylePriority.UseBackColor = false;
            this.GONDEREN.StylePriority.UseBorderColor = false;
            this.GONDEREN.StylePriority.UseBorders = false;
            this.GONDEREN.StylePriority.UseFont = false;
            this.GONDEREN.StylePriority.UseForeColor = false;
            this.GONDEREN.StylePriority.UsePadding = false;
            this.GONDEREN.StylePriority.UseTextAlignment = false;
            this.GONDEREN.Tag = "1";
            this.GONDEREN.Text = "GONDEREN";
            this.GONDEREN.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // KIMETIP
            // 
            this.KIMETIP.BackColor = System.Drawing.Color.LightGray;
            this.KIMETIP.BorderColor = System.Drawing.Color.White;
            this.KIMETIP.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.KIMETIP.Dpi = 254F;
            this.KIMETIP.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KIMETIP.ForeColor = System.Drawing.Color.Black;
            this.KIMETIP.LocationFloat = new DevExpress.Utils.PointFloat(1780F, 0F);
            this.KIMETIP.Name = "KIMETIP";
            this.KIMETIP.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.KIMETIP.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.KIMETIP.StylePriority.UseBackColor = false;
            this.KIMETIP.StylePriority.UseBorderColor = false;
            this.KIMETIP.StylePriority.UseBorders = false;
            this.KIMETIP.StylePriority.UseFont = false;
            this.KIMETIP.StylePriority.UseForeColor = false;
            this.KIMETIP.StylePriority.UsePadding = false;
            this.KIMETIP.StylePriority.UseTextAlignment = false;
            this.KIMETIP.Tag = "1";
            this.KIMETIP.Text = "KIMETIP";
            this.KIMETIP.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GONDERENTC
            // 
            this.GONDERENTC.BackColor = System.Drawing.Color.LightGray;
            this.GONDERENTC.BorderColor = System.Drawing.Color.White;
            this.GONDERENTC.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.GONDERENTC.Dpi = 254F;
            this.GONDERENTC.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GONDERENTC.ForeColor = System.Drawing.Color.Black;
            this.GONDERENTC.LocationFloat = new DevExpress.Utils.PointFloat(79.99996F, 0F);
            this.GONDERENTC.Name = "GONDERENTC";
            this.GONDERENTC.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 254F);
            this.GONDERENTC.SizeF = new System.Drawing.SizeF(200F, 60F);
            this.GONDERENTC.StylePriority.UseBackColor = false;
            this.GONDERENTC.StylePriority.UseBorderColor = false;
            this.GONDERENTC.StylePriority.UseBorders = false;
            this.GONDERENTC.StylePriority.UseFont = false;
            this.GONDERENTC.StylePriority.UseForeColor = false;
            this.GONDERENTC.StylePriority.UsePadding = false;
            this.GONDERENTC.StylePriority.UseTextAlignment = false;
            this.GONDERENTC.Tag = "1";
            this.GONDERENTC.Text = "GONDERENTC";
            this.GONDERENTC.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel16,
            this.xrLabel14,
            this.xrLabel12,
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrLabel22,
            this.xrLabel21,
            this.xrLabel19});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 160F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel16
            // 
            this.xrLabel16.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel16.BorderColor = System.Drawing.Color.White;
            this.xrLabel16.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel16.Dpi = 254F;
            this.xrLabel16.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.ForeColor = System.Drawing.Color.White;
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(1780F, 99.99999F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel16.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel16.StylePriority.UseBackColor = false;
            this.xrLabel16.StylePriority.UseBorderColor = false;
            this.xrLabel16.StylePriority.UseBorders = false;
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UseForeColor = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.Text = "Kullanıcı Türü";
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel14
            // 
            this.xrLabel14.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel14.BorderColor = System.Drawing.Color.White;
            this.xrLabel14.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel14.Dpi = 254F;
            this.xrLabel14.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.ForeColor = System.Drawing.Color.White;
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(879.9999F, 100F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel14.StylePriority.UseBackColor = false;
            this.xrLabel14.StylePriority.UseBorderColor = false;
            this.xrLabel14.StylePriority.UseBorders = false;
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseForeColor = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Kullanıcı Türü";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel12
            // 
            this.xrLabel12.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel12.BorderColor = System.Drawing.Color.White;
            this.xrLabel12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel12.Dpi = 254F;
            this.xrLabel12.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.ForeColor = System.Drawing.Color.White;
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(579.9999F, 99.99999F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel12.StylePriority.UseBackColor = false;
            this.xrLabel12.StylePriority.UseBorderColor = false;
            this.xrLabel12.StylePriority.UseBorders = false;
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseForeColor = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Şube";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel5
            // 
            this.xrLabel5.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel5.BorderColor = System.Drawing.Color.White;
            this.xrLabel5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel5.Dpi = 254F;
            this.xrLabel5.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.ForeColor = System.Drawing.Color.White;
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(1180F, 100F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel5.StylePriority.UseBackColor = false;
            this.xrLabel5.StylePriority.UseBorderColor = false;
            this.xrLabel5.StylePriority.UseBorders = false;
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseForeColor = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Kim İçin";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel4.BorderColor = System.Drawing.Color.White;
            this.xrLabel4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel4.Dpi = 254F;
            this.xrLabel4.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.ForeColor = System.Drawing.Color.White;
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(1480F, 99.99998F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel4.StylePriority.UseBackColor = false;
            this.xrLabel4.StylePriority.UseBorderColor = false;
            this.xrLabel4.StylePriority.UseBorders = false;
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseForeColor = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "Kime";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel3.BorderColor = System.Drawing.Color.White;
            this.xrLabel3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.ForeColor = System.Drawing.Color.White;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(2080F, 99.99999F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(465F, 60.00001F);
            this.xrLabel3.StylePriority.UseBackColor = false;
            this.xrLabel3.StylePriority.UseBorderColor = false;
            this.xrLabel3.StylePriority.UseBorders = false;
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "Not";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel2.BorderColor = System.Drawing.Color.White;
            this.xrLabel2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.ForeColor = System.Drawing.Color.White;
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(2545F, 99.99998F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel2.StylePriority.UseBackColor = false;
            this.xrLabel2.StylePriority.UseBorderColor = false;
            this.xrLabel2.StylePriority.UseBorders = false;
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseForeColor = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Tarih";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel1
            // 
            this.xrLabel1.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel1.BorderColor = System.Drawing.Color.White;
            this.xrLabel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.ForeColor = System.Drawing.Color.White;
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(2850F, 60F);
            this.xrLabel1.StylePriority.UseBackColor = false;
            this.xrLabel1.StylePriority.UseBorderColor = false;
            this.xrLabel1.StylePriority.UseBorders = false;
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseForeColor = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "VELİ İLETİŞİM ÜÇGENİ";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel22
            // 
            this.xrLabel22.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel22.BorderColor = System.Drawing.Color.White;
            this.xrLabel22.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel22.Dpi = 254F;
            this.xrLabel22.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.ForeColor = System.Drawing.Color.White;
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(0F, 100F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(80F, 60F);
            this.xrLabel22.StylePriority.UseBackColor = false;
            this.xrLabel22.StylePriority.UseBorderColor = false;
            this.xrLabel22.StylePriority.UseBorders = false;
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseForeColor = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "Sn";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel21
            // 
            this.xrLabel21.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel21.BorderColor = System.Drawing.Color.White;
            this.xrLabel21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel21.Dpi = 254F;
            this.xrLabel21.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.ForeColor = System.Drawing.Color.White;
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(79.99996F, 100F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(200F, 60F);
            this.xrLabel21.StylePriority.UseBackColor = false;
            this.xrLabel21.StylePriority.UseBorderColor = false;
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseForeColor = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.Text = "Gönderen TC";
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel19
            // 
            this.xrLabel19.BackColor = System.Drawing.Color.DimGray;
            this.xrLabel19.BorderColor = System.Drawing.Color.White;
            this.xrLabel19.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrLabel19.Dpi = 254F;
            this.xrLabel19.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.ForeColor = System.Drawing.Color.White;
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(280F, 100F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(300F, 60F);
            this.xrLabel19.StylePriority.UseBackColor = false;
            this.xrLabel19.StylePriority.UseBorderColor = false;
            this.xrLabel19.StylePriority.UseBorders = false;
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseForeColor = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Gönderen ";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Mesaj
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.Dpi = 254F;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 2100;
            this.PageWidth = 2970;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.Version = "15.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Mesaj_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel GONDERENTC;
        private DevExpress.XtraReports.UI.XRLabel xrTarih;
        private DevExpress.XtraReports.UI.XRLabel GONDEREN;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel ACIKLAMA;
        private DevExpress.XtraReports.UI.XRLabel KIME;
        private DevExpress.XtraReports.UI.XRLabel KIMICIN;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel GONDERENSUBE;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel KIMETIP;
        private DevExpress.XtraReports.UI.XRLabel GONDERENTIP;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRLabel KYE_TARIH;
    }
}
