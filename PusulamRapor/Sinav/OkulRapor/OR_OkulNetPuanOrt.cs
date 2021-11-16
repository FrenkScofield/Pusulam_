using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraCharts;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using System.Text.RegularExpressions;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_OkulNetPuanOrt : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_OkulNetPuanOrt(DataTable _dt1, DataTable _dt2, string _SUBEAD, string _SUBEIL, string _SUBEILCE, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dt2 = _dt2;
            //dt1=_dt1.Select(string.Format("ID<>0")).CopyToDataTable();
            //this.DataSource=dt1;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;

            InitializeComponent();
        }

        private void OR_OkulNetPuanOrt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;



            DataTable table1 = new DataTable();
            table1.Columns.Add("SIRALAMA", typeof(string)); // ad soyad
            int i = 1;
            foreach (var item in dersUzun)
            {
                table1.Columns.Add("D" + i, typeof(string)); // day
                table1.Columns.Add("Y" + i, typeof(string)); // day
                table1.Columns.Add("N" + i, typeof(string)); // day
                i++;
            }
            DataView view = new DataView(dt2);
            DataTable distinctValues = view.ToTable(true, "KOD");

            i = 1;
            foreach (var item in distinctValues.Rows)
            {
                table1.Columns.Add("P" + i);
                i++;
            }

            String ad = "";
            DataRow newdr = table1.NewRow();
            i = 1;
            int j = 1;
            foreach (DataRow dr in dt1.Rows)
            {
                if (!ad.Equals(dr["SIRALAMA"].ToString()))
                {
                    if (ad != "")
                    {
                        j = 1;
                        try
                        {
                            foreach (DataRow item in dt2.Select("SIRALAMA='" + ad + "'").CopyToDataTable().Rows)
                            {
                                newdr["P" + j] = item["PUAN"].ToString();
                                j++;
                            }
                        }
                        catch (Exception)
                        {

                        }

                        table1.Rows.Add(newdr);
                        newdr = table1.NewRow();
                        i = 1;
                    }
                    newdr["SIRALAMA"] = dr["SIRALAMA"].ToString();
                    ad = dr["SIRALAMA"].ToString();
                }
                newdr["D" + i] = dr["DOGRU"].ToString();
                newdr["Y" + i] = dr["YANLIS"].ToString();
                newdr["N" + i] = dr["NET"].ToString();
                i++;
            }

            j = 1;
            if (dt2.Select("SIRALAMA='" + ad + "'").Length > 0)
            {
                foreach (DataRow item in dt2.Select("SIRALAMA='" + ad + "'").CopyToDataTable().Rows)
                {
                    newdr["P" + j] = item["PUAN"].ToString();
                    j++;
                }
            }


            table1.Rows.Add(newdr);

            //Ders
            #region
            float X = 58;
            float SubX = 58;
            float Y = 67f;
            float SubY = 91.5f;
            float En = (float)(1732 - X - (80 * distinctValues.Rows.Count)) / (float)(dersUzun.Count);
            float SubEn = (En / (float)3);
            float Boy = 25;
            float SubBoy = 25;
            FontFamily ff = new FontFamily("Tahoma");

            for (int k = 0; k < dersUzun.Count; k++)
            {
                XRLabel lblDers = new XRLabel()
                {
                    WidthF = En,
                    HeightF = Boy,
                    Text = dersUzun[k],
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(X, Y),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                X += lblDers.WidthF;
                GroupHeader1.Controls.Add(lblDers);

                XRLabel lblD = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "D",
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, SubY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                GroupHeader1.Controls.Add(lblD);

                XRLabel lblDDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "D" + (k + 1),
                    Name = "D" + (k + 1),
                    Font = new Font(ff, 6, FontStyle.Regular),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, 0),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "1"
                };
                SubX += lblDDeger.WidthF;
                Detail.Controls.Add(lblDDeger);

                XRLabel lblY = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Y",
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, SubY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                GroupHeader1.Controls.Add(lblY);

                XRLabel lblYDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Y" + (k + 1),
                    Name = "Y" + (k + 1),
                    Font = new Font(ff, 6, FontStyle.Regular),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, 0),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "1"
                };
                SubX += lblY.WidthF;
                Detail.Controls.Add(lblYDeger);

                XRLabel lblN = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "N",
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, SubY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                GroupHeader1.Controls.Add(lblN);

                XRLabel lblNDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "N" + (k + 1),
                    Name = "N" + (k + 1),
                    Font = new Font(ff, 6, FontStyle.Regular),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(SubX, 0),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "1"
                };
                SubX += lblNDeger.WidthF;
                Detail.Controls.Add(lblNDeger);
            }
            #endregion

            //Puan
            #region
            float PX = X;
            float PY = Y;
            float PEn = 80;
            float PBoy = 25;
            for (int k = 0; k < distinctValues.Rows.Count; k++)
            {
                XRLabel lblPT = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = PBoy,
                    Text = distinctValues.Rows[k]["KOD"].ToString(),
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    LocationF = new PointF(PX, PY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                GroupHeader1.Controls.Add(lblPT);

                XRLabel lblP = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = SubBoy,
                    Text = "PUAN",
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    LocationF = new PointF(PX, SubY),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                GroupHeader1.Controls.Add(lblP);

                XRLabel lblPDeger = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = SubBoy,
                    Text = "P" + (k + 1),
                    Name = "P" + (k + 1),
                    Font = new Font(ff, 6, FontStyle.Regular),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    LocationF = new PointF(PX, 0),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "1"
                };
                PX += lblPDeger.WidthF;
                Detail.Controls.Add(lblPDeger);
            }
            #endregion

            this.DataSource = table1;
            FillReportDataFields.Fill(Detail, table1);

            //s=dt1.DefaultView.ToTable(true,"DERSAD","ID_DERS");
            //int k = 1000/(OkulRapor.dersSayisi+1);

            //pivotGrid.DataSource=table1;

            //pivotGrid.Fields.Add("TAKMAAD",PivotArea.ColumnArea);

            //pivotGrid.Fields.Add("SIRALAMA",PivotArea.RowArea);

            //pivotGrid.Fields.Add("D",PivotArea.DataArea);
            //pivotGrid.Fields.Add("Y",PivotArea.DataArea);
            ////pivotGrid.Fields.Add("B",PivotArea.DataArea);
            //pivotGrid.Fields.Add("N",PivotArea.DataArea);
            //pivotGrid.Fields["D"].SummaryType=PivotSummaryType.Max;
            //pivotGrid.Fields["Y"].SummaryType=PivotSummaryType.Max;
            ////pivotGrid.Fields["B"].SummaryType=PivotSummaryType.Max;
            //pivotGrid.Fields["N"].SummaryType=PivotSummaryType.Max;

            //pivotGrid.Fields["SIRALAMA"].Width=k;
            //pivotGrid.Fields["TAKMAAD"].MinWidth=k;
            //pivotGrid.Fields["TAKMAAD"].Width=k;
            //pivotGrid.Fields["D"].MinWidth=k/3;
            //pivotGrid.Fields["D"].Width=k/3;
            //pivotGrid.Fields["Y"].MinWidth=k/3;
            //pivotGrid.Fields["Y"].Width=k/3;
            //pivotGrid.Fields["N"].MinWidth=k/3;
            //pivotGrid.Fields["N"].Width=k/3;


            //DataTable table2 = new DataTable();
            //table2.Columns.Add("SIRALAMA",typeof(string)); // ad soyad
            //table2.Columns.Add("KOD",typeof(string)); // day2
            //table2.Columns.Add("PUAN",typeof(string)); // day

            //foreach(DataRow row in dt2.Rows)
            //{
            //    table2.Rows.Add(new object[] {
            //                             row["SIRALAMA"].ToString()
            //                            ,row["KOD"].ToString()
            //                            ,row["PUAN"].ToString()
            //                    });
            //}
            //int t = 184/4;
            //xrPivotGrid1.LeftF=pivotGrid.WidthF;
            //xrPivotGrid1.TopF=20;
            //xrPivotGrid1.DataSource=table2;

            //xrPivotGrid1.Fields.Add("KOD",PivotArea.ColumnArea);
            //xrPivotGrid1.Fields.Add("SIRALAMA",PivotArea.RowArea);
            ////xrPivotGrid1.Fields["SIRALAMA"].Visible=false;
            //xrPivotGrid1.Fields["SIRALAMA"].Options.ShowValues=false;


            //xrPivotGrid1.Fields.Add("PUAN",PivotArea.DataArea);
            //xrPivotGrid1.Fields["PUAN"].SummaryType=PivotSummaryType.Max;

            //xrPivotGrid1.Fields["SIRALAMA"].Width=t;
            //xrPivotGrid1.Fields["SIRALAMA"].MinWidth=t;
            //xrPivotGrid1.Fields["KOD"].Width=t;
            //xrPivotGrid1.Fields["KOD"].MinWidth=t;
            //xrPivotGrid1.Fields["PUAN"].Width=t;
            //xrPivotGrid1.Fields["PUAN"].MinWidth=t;

            //pivotGrid.BringToFront();
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xr_netort.Series.Clear();
            xr_puanort.Series.Clear();

            Series srsOk = new Series("OKUL", ViewType.Bar);
            Series srsIl = new Series("IL", ViewType.Bar);
            Series srsGe = new Series("GENEL", ViewType.Bar);

            srsOk.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsIl.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsGe.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;


            foreach (string item in dersUzun)
            {
                DataTable dt = dt1.Select(string.Format("TAKMAAD='{0}'", item)).CopyToDataTable();
                foreach (DataRow dr in dt.Rows)
                {
                    srsOk.Points.Add(new SeriesPoint(item.Substring(0, 3) + Regex.Match(item, @"\d+").Value, Convert.ToDouble(dt.Select("SIRALAMA='OKUL'").CopyToDataTable().Rows[0]["NET"])));
                    srsIl.Points.Add(new SeriesPoint(item.Substring(0, 3) + Regex.Match(item, @"\d+").Value, Convert.ToDouble(dt.Select("SIRALAMA='İl'").CopyToDataTable().Rows[0]["NET"])));
                    srsGe.Points.Add(new SeriesPoint(item.Substring(0, 3) + Regex.Match(item, @"\d+").Value, Convert.ToDouble(dt.Select("SIRALAMA='GENEL'").CopyToDataTable().Rows[0]["NET"])));
                }
            }

            xr_netort.Series.Add(srsOk);
            xr_netort.Series.Add(srsIl);
            xr_netort.Series.Add(srsGe);


            srsOk = new Series("OKUL", ViewType.Bar);
            srsIl = new Series("IL", ViewType.Bar);
            srsGe = new Series("GENEL", ViewType.Bar);

            srsOk.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsIl.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
            srsGe.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;


            foreach (DataRow item in dt2.DefaultView.ToTable(true, "KOD").Rows)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    srsOk.Points.Add(new SeriesPoint(item["KOD"], Convert.ToDouble(dt2.Select(string.Format("KOD='{0}' AND SIRALAMA='Okul'", item["KOD"].ToString())).CopyToDataTable().Rows[0]["PUAN"].ToString())));
                    srsIl.Points.Add(new SeriesPoint(item["KOD"], Convert.ToDouble(dt2.Select(string.Format("KOD='{0}' AND SIRALAMA='İl'", item["KOD"].ToString())).CopyToDataTable().Rows[0]["PUAN"].ToString())));
                    srsGe.Points.Add(new SeriesPoint(item["KOD"], Convert.ToDouble(dt2.Select(string.Format("KOD='{0}' AND SIRALAMA='Genel'", item["KOD"].ToString())).CopyToDataTable().Rows[0]["PUAN"].ToString())));
                }
            }


            xr_puanort.Series.Add(srsOk);
            xr_puanort.Series.Add(srsIl);
            xr_puanort.Series.Add(srsGe);

        }
    }
}
