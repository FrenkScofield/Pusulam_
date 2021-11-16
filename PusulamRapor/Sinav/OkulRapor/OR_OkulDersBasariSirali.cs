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
    public partial class OR_OkulDersBasariSirali : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dtKATILIM = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");

        public OR_OkulDersBasariSirali(DataTable _dt1, DataTable _dt2, DataTable _dtKATILIM, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dt2 = _dt2;
            dtKATILIM = _dtKATILIM;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;
            SINAVAD = _SINAVAD;

            InitializeComponent();
        }

        private void OR_OkulDersBasariSirali_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
                    newdr["ADSOYAD"] = dr["AD"].ToString() + ' ' + dr["SOYAD"].ToString();

                    newdr["D" + i] = Convert.ToInt32(dr["DOGRU"]);
                    newdr["Y" + i] = Convert.ToInt32(dr["YANLIS"]);
                    newdr["B" + i] = Convert.ToInt32(dr["BOS"]);
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
            double TOPLAMDOGRU = 0;
            double TOPLAMYANLIS = 0;
            double TOPLAMBOS = 0;
            Double TOPLAMNET = 0.0;

            DataTable dtsirala = PublicMetods.orderBYtoTable(dt1.Select("TCKIMLIKNO='' AND AD='OKUL'").CopyToDataTable(),"BOLUMNO");
            foreach (DataRow dr in dtsirala.Rows)
            {
                newdr["SIRA"] = "";
                newdr["SINIF"] = "";
                newdr["ADSOYAD"] = dr["AD"].ToString() + " " + dr["SOYAD"].ToString();

                newdr["D" + i] = Convert.ToDouble(dr["DOGRU"]);
                newdr["Y" + i] = Convert.ToDouble(dr["YANLIS"]);
                newdr["B" + i] = Convert.ToDouble(dr["BOS"]);
                newdr["N" + i] = dr["NET"].ToString();
                TOPLAMDOGRU += dr["DOGRU"].ToString() != "" ? Convert.ToDouble(dr["DOGRU"].ToString()) : 0;
                TOPLAMYANLIS += dr["YANLIS"].ToString() != "" ? Convert.ToDouble(dr["YANLIS"].ToString()) : 0;
                TOPLAMBOS += dr["BOS"].ToString() != "" ? Convert.ToDouble(dr["BOS"].ToString()) : 0;
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
            float En = (float)(1732 - 195 - (48 * 3)) / ((float)(dersUzun.Count) + 1f);
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
                PageHeader.Controls.Add(lblDers);

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
                PageHeader.Controls.Add(lblD);

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
                PageHeader.Controls.Add(lblY);

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
                PageHeader.Controls.Add(lblB);

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
                PageHeader.Controls.Add(lblN);

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
                PageHeader.Controls.Add(lblPT);

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
                PageHeader.Controls.Add(lblS);

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

            this.DataSource = table1;
            FillReportDataFields.Fill(Detail, table1);
        }
    }
}
