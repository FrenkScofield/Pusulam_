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
    public partial class OR_SinifNetPuanOrt : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_SinifNetPuanOrt(DataTable _dt1, DataTable _dt2, string _SUBEAD, string _SUBEIL, string _SUBEILCE, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dt2 = _dt2;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;

            InitializeComponent();
        }

        private void OR_SinifNetPuanOrt_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;

            DataTable table1 = new DataTable();
            table1.Columns.Add("SINIF", typeof(string)); // ad soyad
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
                if (!ad.Equals(dr["SINIF"].ToString()))
                {
                    if (ad != "")
                    {
                        j = 1;
                        if (dt2.Select("SINIF='" + ad + "'").Length > 0)
                        {
                            foreach (DataRow item in dt2.Select("SINIF='" + ad + "'").CopyToDataTable().Rows)
                            {
                                newdr["P" + j] = item["PUAN"].ToString();
                                j++;
                            }
                        }

                        table1.Rows.Add(newdr);
                        newdr = table1.NewRow();
                        i = 1;
                    }
                    newdr["SINIF"] = dr["SINIF"].ToString();
                    ad = dr["SINIF"].ToString();
                }
                newdr["D" + i] = dr["DOGRU"].ToString();
                newdr["Y" + i] = dr["YANLIS"].ToString();
                newdr["N" + i] = dr["NET"].ToString();
                i++;
            }

            j = 1;
            if (dt2.Select("SINIF='" + ad + "'").Length > 0)
            {
                foreach (DataRow item in dt2.Select("SINIF='" + ad + "'").CopyToDataTable().Rows)
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
            float En = (float)((1732 - X - (80 * distinctValues.Rows.Count)) / (float)(dersUzun.Count));
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
                    CanShrink = false,
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
            DataTable distinctValues = view.ToTable(true, "SINIF");
            foreach (DataRow item in distinctValues.Rows)
            {
                Series s = new Series(item["SINIF"].ToString(), ViewType.Bar);

                foreach (string ders in dersUzun)
                {
                    DataTable dt = dt1.Select(string.Format("TAKMAAD='{0}'", ders)).CopyToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {

                        s.Points.Add(new SeriesPoint(ders.Substring(0, 3) + Regex.Match(ders, @"\d+").Value, Convert.ToDouble(dt.Select("SINIF='" + item["SINIF"].ToString() + "'").CopyToDataTable().Rows[0]["NET"])));
                    }
                }
                s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_netort.Series.Add(s);
            }

            foreach (DataRow item in distinctValues.Rows)
            {
                Series s = new Series(item["SINIF"].ToString(), ViewType.Bar);

                foreach (DataRow item2 in dt2.DefaultView.ToTable(true, "KOD").Rows)
                {
                    foreach (DataRow dr in dt2.Rows)
                    {
                        s.Points.Add(new SeriesPoint(item2[0], Convert.ToDouble(dt2.Select(string.Format("KOD='{0}' AND SINIF='" + item["SINIF"].ToString() + "'", item2[0].ToString())).CopyToDataTable().Rows[0]["PUAN"].ToString())));
                    }
                }
                s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_puanort.Series.Add(s);
            }
        }

    }
}
