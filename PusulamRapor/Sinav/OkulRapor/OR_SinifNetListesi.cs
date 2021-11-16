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
    public partial class OR_SinifNetListesi : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dtKATILIM = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_SinifNetListesi(DataTable _dt1, DataTable _dt2, DataTable _dt3, DataTable _dt4, DataTable _dtKATILIM, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dt2 = _dt2;
            dt3 = _dt3;
            dt4 = _dt4;
            dtKATILIM = _dtKATILIM;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;
            SINAVAD = _SINAVAD;

            InitializeComponent();
        }

        private void OR_SinifNetListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            lbl_subeAd.Text = SUBEAD;
            lbl_subeIl.Text = SUBEIL;
            lbl_subeIlce.Text = SUBEILCE;
            lbl_sinavad.Text = SINAVAD;
            lbl_katilimokul.Text = dtKATILIM.Rows[0]["SUBE"].ToString();
            lbl_katilimilce.Text = dtKATILIM.Rows[0]["ILCE"].ToString();
            lbl_katilimil.Text = dtKATILIM.Rows[0]["IL"].ToString();
            lbl_katilimgenel.Text = dtKATILIM.Rows[0]["GENEL"].ToString();

            DataTable table1 = new DataTable();
            table1.Columns.Add("TCKIMLIKNO", typeof(string));
            table1.Columns.Add("SIRA", typeof(string));
            table1.Columns.Add("SINIF", typeof(string));
            table1.Columns.Add("ADSOYAD", typeof(string));
            int i = 1;
            foreach (var item in dersUzun)
            {
                table1.Columns.Add("D" + i, typeof(string));
                table1.Columns.Add("Y" + i, typeof(string));
                table1.Columns.Add("B" + i, typeof(string));
                table1.Columns.Add("N" + i, typeof(string));
                i++;
            }

            int TOPLAMSIRA = i;
            //TOPLAM İÇİN
            table1.Columns.Add("D" + i, typeof(string));
            table1.Columns.Add("Y" + i, typeof(string));
            table1.Columns.Add("B" + i, typeof(string));
            table1.Columns.Add("N" + i, typeof(string));

            DataView view = new DataView(dt2);
            DataTable distinctValues = view.ToTable(true, "SIRALAMA");

            i = 1;
            foreach (var item in distinctValues.Rows)
            {
                table1.Columns.Add("S" + i);
                i++;
            }

            String ad = "";
            DataRow newdr = table1.NewRow();
            i = 1;
            int j = 1;
            int counter = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                if (dr["TCKIMLIKNO"].ToString().Length == 11 && dr["ONCELIK"].ToString() == "1")
                {
                    if (!ad.Equals(dr["TCKIMLIKNO"].ToString()))
                    {
                        counter++;
                        if (ad != "")
                        {
                            j = 1;
                            foreach (DataRow item in dt2.Select("TCKIMLIKNO='" + ad + "'").CopyToDataTable().Rows)
                            {
                                newdr["S" + j] = item["SIRA"].ToString();
                                j++;
                            }

                            table1.Rows.Add(newdr);
                            newdr = table1.NewRow();
                            i = 1;
                        }
                        newdr["TCKIMLIKNO"] = dr["TCKIMLIKNO"].ToString();
                        ad = dr["TCKIMLIKNO"].ToString();
                    }
                    newdr["SIRA"] = counter;
                    newdr["SINIF"] = dr["SINIF"].ToString();
                    newdr["ADSOYAD"] = dr["AD"].ToString() + " " + dr["SOYAD"].ToString();

                    if (dr["TCKIMLIKNO"].ToString().Length > 0)
                    {
                        newdr["D" + i] = Convert.ToInt32(dr["DOGRU"]);
                        newdr["Y" + i] = Convert.ToInt32(dr["YANLIS"]);
                        newdr["B" + i] = Convert.ToInt32(dr["BOS"]);
                    }
                    else
                    {
                        newdr["D" + i] = dr["DOGRU"];
                        newdr["Y" + i] = dr["YANLIS"].ToString();
                        newdr["B" + i] = dr["BOS"].ToString();
                    }

                    newdr["N" + i] = dr["NET"].ToString();
                    i++;
                }
            }

            j = 1;
            foreach (DataRow item in dt2.Select("TCKIMLIKNO='" + ad + "'").CopyToDataTable().Rows)
            {
                newdr["S" + j] = item["SIRA"].ToString();
                j++;
            }

            table1.Rows.Add(newdr);

            foreach (DataRow item in table1.Rows)
            {
                DataRow row = dt1.Select("TCKIMLIKNO='" + item["TCKIMLIKNO"] + "' AND TAKMAAD='TOPLAM'").CopyToDataTable().Rows[0];
                item["D" + TOPLAMSIRA] = Convert.ToInt32(row["DOGRU"]);
                item["Y" + TOPLAMSIRA] = Convert.ToInt32(row["YANLIS"]);
                item["B" + TOPLAMSIRA] = Convert.ToInt32(row["BOS"]);
                item["N" + TOPLAMSIRA] = row["NET"];
            }

            i = 1;
            newdr = table1.NewRow();
            int TOPLAMDOGRU = 0;
            int TOPLAMYANLIS = 0;
            int TOPLAMBOS = 0;
            Double TOPLAMNET = 0.0;
            foreach (DataRow dr in dt1.Select("TCKIMLIKNO='' AND AD='OKUL'").CopyToDataTable().Rows)
            {
                newdr["SIRA"] = "";
                newdr["SINIF"] = "";
                newdr["ADSOYAD"] = dr["AD"].ToString() + " " + dr["SOYAD"].ToString();

                newdr["D" + i] = Convert.ToInt32(dr["DOGRU"]);
                newdr["Y" + i] = Convert.ToInt32(dr["YANLIS"]);
                newdr["B" + i] = Convert.ToInt32(dr["BOS"]);
                newdr["N" + i] = dr["NET"].ToString();
                TOPLAMDOGRU += Convert.ToInt32(dr["DOGRU"]);
                TOPLAMYANLIS += Convert.ToInt32(dr["YANLIS"]);
                TOPLAMBOS += Convert.ToInt32(dr["BOS"]);
                TOPLAMNET += dr["NET"].ToString() != "" ? Convert.ToDouble(dr["NET"].ToString()) : 0;
                i++;
            }
            newdr["D" + i] = TOPLAMDOGRU.ToString();
            newdr["Y" + i] = TOPLAMYANLIS.ToString();
            newdr["B" + i] = TOPLAMBOS.ToString();
            newdr["N" + i] = TOPLAMNET.ToString();
            table1.Rows.Add(newdr);

            //Ders
            #region
            float X = 195;
            float SubX = 195;
            float Y = 67f;
            float SubY = 91.5f;
            float En = (float)(1732 - X - (48 * 3)) / ((float)(dersUzun.Count) + 1f);
            float SubEn = (En / (float)4);
            float Boy = 25;
            float SubBoy = 25;
            FontFamily ff = new FontFamily("Tahoma");

            for (int k = 0; k <= dersUzun.Count; k++)
            {
                XRLabel lblDers = new XRLabel()
                {
                    WidthF = En,
                    HeightF = Boy,
                    Text = k < dersUzun.Count ? dersUzun[k].Substring(0, 3) + Regex.Match(dersUzun[k], @"\d+").Value : "TOPLAM",
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

                XRLabel lblB = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "B",
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
                GroupHeader1.Controls.Add(lblB);

                XRLabel lblBDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "B" + (k + 1),
                    Name = "B" + (k + 1),
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
                SubX += lblB.WidthF;
                Detail.Controls.Add(lblBDeger);

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

            //SIRA
            #region
            float PX = X;
            float PY = Y;
            float PEn = 48;
            float PBoy = 25;
            for (int k = 0; k < distinctValues.Rows.Count; k++)
            {
                XRLabel lblPT = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = PBoy,
                    Text = distinctValues.Rows[k]["SIRALAMA"].ToString(),
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

                XRLabel lblS = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = SubBoy,
                    Text = "SIRA",
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
                GroupHeader1.Controls.Add(lblS);

                XRLabel lblSDeger = new XRLabel()
                {
                    WidthF = PEn,
                    AutoWidth = false,
                    HeightF = SubBoy,
                    Text = "S" + (k + 1),
                    Name = "S" + (k + 1),
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
                PX += lblSDeger.WidthF;
                Detail.Controls.Add(lblSDeger);
            }
            #endregion

            DataTable filtered = table1.Select("SINIF<>''").CopyToDataTable();
            this.DataSource = filtered;
            GroupField sinif = new GroupField("SINIF");
            GroupHeader1.GroupFields.Add(sinif);
            FillReportDataFields.Fill(Detail, filtered);
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string SINIF = GetCurrentColumnValue("SINIF").ToString();
            TITLE.Text = SINIF + " SINIFI NET LİSTESİ";
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xr_netler.Series.Clear();

            string SINIF = GetCurrentColumnValue("SINIF").ToString();
            if (SINIF != "")
            {

                DataView view = new DataView(dt3);
                DataTable distinctValuesx = view.ToTable(true, "SIRALAMA");
                foreach (DataRow item in distinctValuesx.Rows)
                {
                    Series s = new Series(item["SIRALAMA"].ToString(), ViewType.Bar);

                    foreach (string ders in dersUzun)
                    {
                        DataTable dt = dt3.Select(string.Format("TAKMAAD='{0}'", ders)).CopyToDataTable();
                        foreach (DataRow dr in dt.Rows)
                        {
                            decimal d = Convert.ToDecimal(dt.Select("SIRALAMA='" + item["SIRALAMA"].ToString() + "'").CopyToDataTable().Rows[0]["DOGRU"]);
                            decimal y = Convert.ToDecimal(dt.Select("SIRALAMA='" + item["SIRALAMA"].ToString() + "'").CopyToDataTable().Rows[0]["YANLIS"]);
                            decimal b = Convert.ToDecimal(dt.Select("SIRALAMA='" + item["SIRALAMA"].ToString() + "'").CopyToDataTable().Rows[0]["BOS"]);
                            decimal net = Convert.ToDecimal(dt.Select("SIRALAMA='" + item["SIRALAMA"].ToString() + "'").CopyToDataTable().Rows[0]["NET"]);

                            decimal yuzde = net / (d + y + b) * 100;

                            s.Points.Add(new SeriesPoint(ders.Substring(0, 3).ToUpper() + Regex.Match(ders, @"\d+").Value, yuzde.ToString("0.00")));
                        }
                    }
                    s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                    xr_netler.Series.Add(s);
                }

                Series ssinif = new Series("SINIF", ViewType.Bar);

                DataTable distinctValuesy = dt4.Select("SINIF='" + SINIF + "'").CopyToDataTable();
                foreach (DataRow item in distinctValuesy.Rows)
                {
                    decimal d = Convert.ToDecimal(item["DOGRU"]);
                    decimal y = Convert.ToDecimal(item["YANLIS"]);
                    decimal b = Convert.ToDecimal(item["BOS"]);
                    decimal net = Convert.ToDecimal(item["NET"]);

                    decimal yuzde = net / (d + y + b) * 100;

                    ssinif.Points.Add(new SeriesPoint(item["TAKMAAD"].ToString().Substring(0, 3).ToUpper() + Regex.Match(item["TAKMAAD"].ToString(), @"\d+").Value, yuzde.ToString("0.00")));
                    ssinif.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                }
                xr_netler.Series.Add(ssinif);
                ((XYDiagram)xr_netler.Diagram).DefaultPane.BackColor = Color.White;
                ((XYDiagram)xr_netler.Diagram).DefaultPane.FillStyle.FillMode = FillMode.Solid;
                xr_netler.Legend.Visibility = DevExpress.Utils.DefaultBoolean.True;
                xr_netler.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;
                xr_netler.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
                xr_netler.Legend.Direction = LegendDirection.LeftToRight;
                XYDiagram diagram = (XYDiagram)xr_netler.Diagram;
                diagram.AxisY.WholeRange.SetMinMaxValues(0, 100);
            }

        }
    }
}
