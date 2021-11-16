using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class sdaSinifKazanim : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable TblVeri = new DataTable();
        DataTable TblBolumNo = new DataTable();
        int GRUPLAMATURU = 1;
        public sdaSinifKazanim(DataTable dt1, DataTable dt2, int GRUPLAMATURU)
        {
            TblVeri = dt1;
            TblBolumNo = dt2;
            this.GRUPLAMATURU = GRUPLAMATURU;
            InitializeComponent();
        }

        private void sdaSinifKazanim_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            float kazanimWidth = 254.62f;
            float konuWidth = 129.17f;
            switch (GRUPLAMATURU)
            {
                case 1:
                    break;
                case 2:
                    xrLbl_KazanimHeader.Visible = false;
                    xrLbl_KazanimRow.Visible = false;

                    xrLbl_UniteHeader.WidthF += kazanimWidth / 2;
                    xrLbl_UniteRow.WidthF += kazanimWidth / 2;

                    xrLbl_KonuHeader.LocationF = new PointF(xrLbl_KonuHeader.LocationF.X + kazanimWidth / 2, xrLbl_KonuHeader.LocationF.Y);
                    xrLbl_KonuRow.LocationF = new PointF(xrLbl_KonuRow.LocationF.X + kazanimWidth / 2, xrLbl_KonuRow.LocationF.Y);

                    xrLbl_KonuHeader.WidthF += kazanimWidth / 2;
                    xrLbl_KonuRow.WidthF += kazanimWidth / 2;
                    break;
                case 3:
                    xrLbl_KazanimHeader.Visible = false;
                    xrLbl_KazanimRow.Visible = false;
                    xrLbl_KonuHeader.Visible = false;
                    xrLbl_KonuRow.Visible = false;

                    xrLbl_UniteHeader.WidthF += kazanimWidth + konuWidth;
                    xrLbl_UniteRow.WidthF += kazanimWidth + konuWidth;
                    break;
                default:
                    break;
            }

            xr_dersSoru.Series.Clear();
            Series srsYuzdeGenel = new Series("", ViewType.Bar);

            foreach (DataRow ders in TblBolumNo.Rows)
            {
                ReportHeader.Controls.Add(lblEkle(ders["DERSAD"].ToString(), LX, LY, 200, 20, Color.LightGray, Color.Black, Color.Black));
                ReportHeader.Controls.Add(lblEkle(ders["BOLUMYUZDESI"].ToString(), LX, LY, 100, 20, Color.LightGray, Color.Black, Color.Black));

                LY += 20;
                LX = 0;

                srsYuzdeGenel.Points.Add(new SeriesPoint(ders["DERSAD"].ToString().Substring(0, 3), Convert.ToDouble(ders["BOLUMYUZDESI"].ToString())));
            }

            #region Series Label
            srsYuzdeGenel.Label.TextOrientation = TextOrientation.Horizontal;
            ((BarSeriesLabel)srsYuzdeGenel.Label).Position = BarSeriesLabelPosition.Top;
            srsYuzdeGenel.Label.Border.Color = Color.Transparent;
            srsYuzdeGenel.Label.BackColor = Color.Transparent;
            srsYuzdeGenel.Label.TextColor = Color.DarkBlue;
            #endregion

            xr_dersSoru.Series.Add(srsYuzdeGenel);

            this.DataSource = TblVeri;
            FillReportDataFields.Fill(Detail, TblVeri);
        }

        float LX = 0;
        float LY = 60;
        public XRLabel lblEkle(string _text, float _LX, float _LY, float _lblEn, float _lblBoy, Color _backColor, Color _foreColor, Color _borderColor)
        {
            XRLabel xrLabelAdd = new XRLabel()
            {
                Text = _text,
                WidthF = _lblEn,
                HeightF = _lblBoy,
                BackColor = _backColor,
                ForeColor = _foreColor,
                BorderColor = _borderColor,
                BorderWidth = 1,
                WordWrap = true,
                Multiline = true,
                LocationF = new PointF(_LX, _LY),
                Borders = DevExpress.XtraPrinting.BorderSide.All
            };
            LX = _LX + xrLabelAdd.WidthF;
            return xrLabelAdd;
        }
    }
}
