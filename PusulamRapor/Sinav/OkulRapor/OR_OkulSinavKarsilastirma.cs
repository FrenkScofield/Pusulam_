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
    public partial class OR_OkulSinavKarsilastirma : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_OkulSinavKarsilastirma(DataTable _dt1, DataTable _dt2, string _SUBEAD, string _SUBEIL, string _SUBEILCE, List<string> _dersKisa, List<string> _dersUzun)
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

        private void OR_OkulSinavKarsilastirma_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;



            DataTable table1 = new DataTable();
            table1.Columns.Add("SINAVAD", typeof(string)); // ad soyad
            table1.Columns.Add("SINAVTARIH", typeof(string));
            table1.Columns.Add("KATILIM", typeof(string));
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
                if (!ad.Equals(dr["SINAVAD"].ToString()))
                {
                    if (ad != "")
                    {
                        j = 1;
                        if (dt2.Select("SINAVAD='" + ad + "'").Length>0)
                        {
                            foreach (DataRow item in dt2.Select("SINAVAD='" + ad + "'").CopyToDataTable().Rows)
                            {
                                newdr["P" + j] = item["PUAN"].ToString();
                                j++;
                            }
                        }                        

                        table1.Rows.Add(newdr);
                        newdr = table1.NewRow();
                        i = 1;
                    }
                    newdr["SINAVAD"] = dr["SINAVAD"].ToString();
                    newdr["SINAVTARIH"] = dr["SINAVTARIH"].ToString();
                    newdr["KATILIM"] = dr["KATILIM"].ToString();
                    ad = dr["SINAVAD"].ToString();
                }
                newdr["D" + i] = dr["DOGRU"].ToString();
                newdr["Y" + i] = dr["YANLIS"].ToString();
                newdr["N" + i] = dr["NET"].ToString();
                i++;
            }

            j = 1;
            if (dt2.Select("SINAVAD='" + ad + "'").Length>0)
            {
                foreach (DataRow item in dt2.Select("SINAVAD='" + ad + "'").CopyToDataTable().Rows)
                {
                    newdr["P" + j] = item["PUAN"].ToString();
                    j++;
                }
            }            

            table1.Rows.Add(newdr);

            //Ders
            #region
            float X = 329.82F;
            float SubX = 329.82F;
            float Y = 67f;
            float SubY = 91.5f;
            float En = (float)(1732 - X - (80* distinctValues.Rows.Count)) / (float)(dersUzun.Count);
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
        }


        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xr_netort.Series.Clear();
            xr_puanort.Series.Clear();

            DataView view = new DataView(dt2);
            DataTable distinctValues = view.ToTable(true, "SINAVAD");
            foreach (DataRow SINAV in distinctValues.Rows)
            {
                Series S = new Series(SINAV["SINAVAD"].ToString(), ViewType.Bar);
                foreach (string item in dersUzun)
                {
                    DataTable dt = dt1.Select(string.Format("TAKMAAD='{0}'", item)).CopyToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dt.Select("SINAVAD='" + SINAV["SINAVAD"].ToString() + "'").Length>0)
                        {
                            Double val = Convert.ToDouble(dt.Select("SINAVAD='" + SINAV["SINAVAD"].ToString() + "'").CopyToDataTable().Rows[0]["NET"]);
                            SeriesPoint sp = new SeriesPoint(item.Substring(0, 3) + Regex.Match(item, @"\d+").Value, val);
                            S.Points.Add(sp);
                        }
                        
                    }
                }
                S.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_netort.Series.Add(S);
            }

            xr_netort.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xr_netort.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            xr_netort.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            xr_netort.Legend.Direction = LegendDirection.LeftToRight;
            ((XYDiagram)xr_netort.Diagram).DefaultPane.BackColor = Color.White;
            ((XYDiagram)xr_netort.Diagram).DefaultPane.FillStyle.FillMode = FillMode.Solid;




            DataTable PUANTUR = dt2.DefaultView.ToTable(true, "KOD");
            foreach (DataRow SINAV in distinctValues.Rows)
            {
                Series S = new Series(SINAV["SINAVAD"].ToString(), ViewType.Bar);
                foreach (DataRow item in PUANTUR.Rows)
                {
                    DataTable dt = dt2.Select(string.Format("KOD='{0}'", item["KOD"].ToString())).CopyToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Double val = Convert.ToDouble(dt.Select("SINAVAD='" + SINAV["SINAVAD"].ToString() + "'").CopyToDataTable().Rows[0]["PUAN"]);
                        SeriesPoint sp = new SeriesPoint(item["KOD"].ToString(), val);
                        S.Points.Add(sp);
                    }
                }
                S.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_puanort.Series.Add(S);
            }

            xr_puanort.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
            xr_puanort.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
            xr_puanort.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
            xr_puanort.Legend.Direction = LegendDirection.LeftToRight;
            ((XYDiagram)xr_puanort.Diagram).DefaultPane.BackColor = Color.White;
            ((XYDiagram)xr_puanort.Diagram).DefaultPane.FillStyle.FillMode = FillMode.Solid;
        }

        private void xr_netort_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
    }
}
