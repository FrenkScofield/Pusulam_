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
    public partial class OR_OkulPuanListesi : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dtKATILIM = new DataTable();
        public string SUBEAD { get; set; }
        public string SUBEIL { get; set; }
        public string SUBEILCE { get; set; }
        public string SINAVAD { get; set; }
        public List<string> dersKisa { get; set; }
        public List<string> dersUzun { get; set; }
        readonly FontFamily familyArial = new FontFamily("Arial");
        DataTable distinctValues;

        public OR_OkulPuanListesi(DataTable _dt1, DataTable _dt2, DataTable _dt3, DataTable _dtKATILIM, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            dersKisa = _dersKisa;
            dersUzun = _dersUzun;
            dt1 = _dt1;
            dt2 = _dt2;
            dt3 = _dt3;
            dtKATILIM = _dtKATILIM;

            SUBEAD = _SUBEAD;
            SUBEIL = _SUBEIL;
            SUBEILCE = _SUBEILCE;
            SINAVAD = _SINAVAD;

            InitializeComponent();
        }

        private void OR_OkulPuanListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
            table1.Columns.Add("ADSOYAD", typeof(string));
            table1.Columns.Add("SINIF", typeof(string));

            distinctValues = dt1.DefaultView.ToTable(true, "ID_SINAVPUANTURU", "KOD");
            foreach (DataRow item in distinctValues.Rows)
            {
                table1.Columns.Add("PUAN" + item["ID_SINAVPUANTURU"], typeof(string));
                table1.Columns.Add("SINIF" + item["ID_SINAVPUANTURU"], typeof(string));
                table1.Columns.Add("OKUL" + item["ID_SINAVPUANTURU"], typeof(string));
                table1.Columns.Add("ILCE" + item["ID_SINAVPUANTURU"], typeof(string));
                table1.Columns.Add("IL" + item["ID_SINAVPUANTURU"], typeof(string));
                table1.Columns.Add("GENEL" + item["ID_SINAVPUANTURU"], typeof(string));
            }


            String ad = "";
            DataRow newdr = table1.NewRow();
            int i = 1;
            int counter = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                if (dr["TCKIMLIKNO"].ToString().Length == 11)
                {
                    if (!ad.Equals(dr["TCKIMLIKNO"].ToString()))
                    {
                        counter++;
                        if (ad != "")
                        {
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

                    newdr["PUAN" + dr["ID_SINAVPUANTURU"].ToString()] = dr["PUAN"].ToString();
                    newdr["SINIF" + dr["ID_SINAVPUANTURU"].ToString()] = dr["SINIFSIRA"].ToString();
                    newdr["OKUL" + dr["ID_SINAVPUANTURU"].ToString()] = dr["OKULSIRA"].ToString();
                    newdr["ILCE" + dr["ID_SINAVPUANTURU"].ToString()] = dr["ILCESIRA"].ToString();
                    newdr["IL" + dr["ID_SINAVPUANTURU"].ToString()] = dr["ILSIRA"].ToString();
                    newdr["GENEL" + dr["ID_SINAVPUANTURU"].ToString()] = dr["GENELSIRA"].ToString();

                    i++;
                }
            }
            table1.Rows.Add(newdr);

            //Ders
            #region
            float X = 195;
            float SubX = 195;
            float Y = 67f;
            float SubY = 91.5f;
            float En = (1732 - 195) / ((float)(distinctValues.Rows.Count));
            float SubEn = (En / (float)6);
            float Boy = 25;
            float SubBoy = 25;
            FontFamily ff = new FontFamily("Tahoma");

            for (int k = 0; k < distinctValues.Rows.Count; k++)
            {
                XRLabel lblDers = new XRLabel()
                {
                    WidthF = En,
                    HeightF = Boy,
                    Text = distinctValues.Rows[k]["KOD"].ToString(),
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

                XRLabel lblPuan = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Puan",
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
                GroupHeader1.Controls.Add(lblPuan);

                XRLabel lblPuanDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "PUAN" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "PUAN" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblPuanDeger.WidthF;
                Detail.Controls.Add(lblPuanDeger);

                XRLabel lblSinif = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Snf",
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
                GroupHeader1.Controls.Add(lblSinif);

                XRLabel lblSinifDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "SINIF" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "SINIF" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblSinifDeger.WidthF;
                Detail.Controls.Add(lblSinifDeger);

                XRLabel lblOkul = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Okul",
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
                GroupHeader1.Controls.Add(lblOkul);

                XRLabel lblOkulDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "OKUL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "OKUL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblOkulDeger.WidthF;
                Detail.Controls.Add(lblOkulDeger);

                XRLabel lblIlce = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "İlçe",
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
                GroupHeader1.Controls.Add(lblIlce);

                XRLabel lblIlceDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "ILCE" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "ILCE" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblIlceDeger.WidthF;
                Detail.Controls.Add(lblIlceDeger);

                XRLabel lblIl = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "İl",
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
                GroupHeader1.Controls.Add(lblIl);

                XRLabel lblIlDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "IL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "IL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblIlDeger.WidthF;
                Detail.Controls.Add(lblIlDeger);

                XRLabel lblGenel = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "Genel",
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
                GroupHeader1.Controls.Add(lblGenel);

                XRLabel lblGenelDeger = new XRLabel()
                {
                    WidthF = SubEn,
                    HeightF = SubBoy,
                    Text = "GENEL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
                    Name = "GENEL" + distinctValues.Rows[k]["ID_SINAVPUANTURU"].ToString(),
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
                SubX += lblGenelDeger.WidthF;
                Detail.Controls.Add(lblGenelDeger);
            }
            #endregion            

            this.DataSource = table1;
            FillReportDataFields.Fill(Detail, table1);
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xr_puankar.Series.Clear();
            xr_subepuankar.Series.Clear();

            DataView view = new DataView(dt2);
            DataTable distinctValuesx = view.ToTable(true, "SIRALAMA");
            foreach (DataRow item in distinctValuesx.Rows)
            {
                Series s = new Series(item["SIRALAMA"].ToString(), ViewType.Bar);

                foreach (DataRow kod in distinctValues.Rows)
                {
                    DataTable dt = dt2.Select(string.Format("KOD='{0}'", kod["KOD"].ToString())).CopyToDataTable();
                    foreach (DataRow dr in dt.Rows)
                    {
                        s.Points.Add(new SeriesPoint(kod["KOD"].ToString(), Convert.ToDouble(dt.Select("SIRALAMA='" + item["SIRALAMA"].ToString() + "'").CopyToDataTable().Rows[0]["PUAN"])));
                    }
                }
                s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_puankar.Series.Add(s);
            }

            DataTable distinctValuesy = dt3.DefaultView.ToTable(true, "SINIF");
            foreach (DataRow item in distinctValuesy.Rows)
            {
                Series s = new Series(item["SINIF"].ToString(), ViewType.Bar);

                foreach (DataRow kod in distinctValues.Rows)
                {
                    foreach (DataRow dr in dt3.Rows)
                    {
                        s.Points.Add(new SeriesPoint(kod["KOD"].ToString(), Convert.ToDouble(dt3.Select(string.Format("KOD='{0}' AND SINIF='" + item["SINIF"].ToString() + "'", kod["KOD"].ToString())).CopyToDataTable().Rows[0]["PUAN"].ToString())));
                    }
                }
                s.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False;
                xr_subepuankar.Series.Add(s);
            }
        }
    }
}
