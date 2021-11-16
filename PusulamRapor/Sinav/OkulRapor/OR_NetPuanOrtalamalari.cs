using System.Data;
using System.Collections.Generic;
using System;
using DevExpress.XtraReports.UI;
using System.Drawing;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_NetPuanOrtalamalari : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable _dt1;
        DataTable _dt2;
        string _SINAVAD;
        int puantursay = 0;
        Color mavi = Color.FromArgb(142, 169, 219);

        public OR_NetPuanOrtalamalari(DataTable _dt1, DataTable _dt2, string _SINAVAD)
        {
            InitializeComponent();
            this._dt1 = _dt1;
            this._dt2 = _dt2;
            this._SINAVAD = _SINAVAD;
        }

        private void OR_NetPuanOrtalamalari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("KAMPUS", typeof(string));

            DataView viewBolumNo = new DataView(_dt1);
            DataTable distinctBolumNo = viewBolumNo.ToTable(true, "BOLUMNO", "TAKMAAD");
            foreach (DataRow item in distinctBolumNo.Rows)
            {
                table1.Columns.Add(item["TAKMAAD"].ToString(), typeof(string));
            }

            //TOPLAM İÇİN
            table1.Columns.Add("TOPLAM", typeof(string));

            if (_dt2.Rows.Count > 0)
            {
                DataView viewPuanTur = new DataView(_dt2);
                DataTable distinctPuanTur = viewPuanTur.ToTable(true, "KOD");

                puantursay = distinctPuanTur.Rows.Count;

                foreach (DataRow item in distinctPuanTur.Rows)
                {
                    table1.Columns.Add(item["KOD"].ToString(), typeof(string));
                }
            }


            DataRow newdr = table1.NewRow();
            double toplam = 0d;
            int toplamss = 0;
            
            foreach (DataRow NET in _dt1.Rows)
            {
                if (Convert.ToInt32(NET["BOLUMNO"]) == 1)
                {
                    if (newdr["KAMPUS"].ToString().Length > 0)
                    {
                        newdr["TOPLAM"] = string.Format("{0:0.00}", toplam);
                        table1.Rows.Add(newdr);
                        toplam = 0f;
                        toplamss = 0;
                    }

                    newdr = table1.NewRow();
                    newdr["KAMPUS"] = NET["AD"].ToString();
                    if (_dt2.Rows.Count > 0)
                    {
                        foreach (DataRow PUAN in _dt2.Select("ID_SUBE=" + NET["ID_SUBE"].ToString() + "").CopyToDataTable().Rows)
                        {
                            newdr[PUAN["KOD"].ToString()] = PUAN["PUAN"].ToString();
                        }
                    }
                }
                newdr[NET["TAKMAAD"].ToString()] = NET["NET"].ToString();
                toplam += Convert.ToDouble(NET["NET"]);
                toplamss += Convert.ToInt32(NET["SS"]);
            }

            newdr["TOPLAM"] = string.Format("{0:0.00}", toplam);
            table1.Rows.Add(newdr);

            float colwidth = Convert.ToSingle(1732 - 200) / Convert.ToSingle(distinctBolumNo.Rows.Count + puantursay + 1);
            float X = 0f;
            float Y = 0f;
            float YBaslik = 25f;
            float Boy = 25;
            FontFamily ff = new FontFamily("Tahoma");

            {
                XRLabel lblSubeBaslik = new XRLabel()
                {
                    WidthF = 200f,
                    HeightF = 3 * Boy,
                    Text = "KAMPÜS",
                    Font = new Font(ff, 6, FontStyle.Bold),
                    BackColor = mavi,
                    ForeColor = Color.Black,
                    AutoWidth = false,
                    LocationF = new PointF(X, YBaslik),
                    TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                    Borders = DevExpress.XtraPrinting.BorderSide.All,
                    BorderWidth = 1,
                    BorderColor = Color.Black,
                    KeepTogether = true,
                    Tag = "0"
                };
                XRLabel lblSube = new XRLabel()
                {
                    WidthF = 200f,
                    HeightF = Boy,
                    Text = "KAMPUS",
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
                    Tag = "1"
                };
                X += lblSube.WidthF;
                PageHeader.Controls.Add(lblSubeBaslik);
                Detail.Controls.Add(lblSube);

                foreach (DataRow ders in distinctBolumNo.Rows)
                {
                    XRLabel lblDersBaslik = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = 2 * Boy,
                        Text = ders["TAKMAAD"].ToString().Replace("@@", Environment.NewLine),
                        Font = new Font(ff, 6, FontStyle.Bold),
                        BackColor = mavi,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, YBaslik),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        WordWrap = true,
                        CanGrow = true,
                        Multiline = true,
                        Tag = "0"
                    };

                    XRLabel lblSinav = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = Boy,
                        Text = _SINAVAD,
                        Font = new Font(ff, 6, FontStyle.Bold),
                        BackColor = mavi,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, 3 * Boy),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };

                    XRLabel lblDers = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = Boy,
                        Text = ders["TAKMAAD"].ToString(),
                        Font = new Font(ff, 6, FontStyle.Regular),
                        BackColor = Color.White,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "1"
                    };
                    X += lblDers.WidthF;
                    PageHeader.Controls.Add(lblDersBaslik);
                    PageHeader.Controls.Add(lblSinav);
                    Detail.Controls.Add(lblDers);
                }

                {
                    XRLabel lblDersBaslik = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = 2 * Boy,
                        Text = "TOPLAM" + Environment.NewLine + "S.S=" + toplamss,
                        Font = new Font(ff, 6, FontStyle.Bold),
                        BackColor = mavi,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, YBaslik),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Multiline = true,
                        Tag = "0"
                    };

                    XRLabel lblSinav = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = Boy,
                        Text = _SINAVAD,
                        Font = new Font(ff, 6, FontStyle.Bold),
                        BackColor = mavi,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, 3 * Boy),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "0"
                    };

                    XRLabel lblDers = new XRLabel()
                    {
                        WidthF = colwidth,
                        HeightF = Boy,
                        Text = "TOPLAM",
                        Font = new Font(ff, 6, FontStyle.Bold),
                        BackColor = mavi,
                        ForeColor = Color.Black,
                        AutoWidth = false,
                        LocationF = new PointF(X, Y),
                        TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                        Borders = DevExpress.XtraPrinting.BorderSide.All,
                        BorderWidth = 1,
                        BorderColor = Color.Black,
                        KeepTogether = true,
                        Tag = "1"
                    };
                    X += lblDers.WidthF;
                    PageHeader.Controls.Add(lblDersBaslik);
                    PageHeader.Controls.Add(lblSinav);
                    Detail.Controls.Add(lblDers);
                }

                if (_dt2.Rows.Count > 0)
                {
                    DataView viewPuanTur = new DataView(_dt2);
                    DataTable distinctPuanTur = viewPuanTur.ToTable(true, "KOD");

                    foreach (DataRow puan in distinctPuanTur.Rows)
                    {
                        XRLabel lblPuanTurBaslik = new XRLabel()
                        {
                            WidthF = colwidth,
                            HeightF = 2 * Boy,
                            Text = puan["KOD"].ToString(),
                            Font = new Font(ff, 6, FontStyle.Bold),
                            BackColor = mavi,
                            ForeColor = Color.Black,
                            AutoWidth = false,
                            LocationF = new PointF(X, YBaslik),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };

                        XRLabel lblSinav = new XRLabel()
                        {
                            WidthF = colwidth,
                            HeightF = Boy,
                            Text = _SINAVAD,
                            Font = new Font(ff, 6, FontStyle.Bold),
                            BackColor = mavi,
                            ForeColor = Color.Black,
                            AutoWidth = false,
                            LocationF = new PointF(X, 3 * Boy),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "0"
                        };

                        XRLabel lblPuanTur = new XRLabel()
                        {
                            WidthF = colwidth,
                            HeightF = Boy,
                            Text = puan["KOD"].ToString(),
                            Font = new Font(ff, 6, FontStyle.Regular),
                            BackColor = Color.White,
                            ForeColor = Color.Black,
                            AutoWidth = false,
                            LocationF = new PointF(X, Y),
                            TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter,
                            Borders = DevExpress.XtraPrinting.BorderSide.All,
                            BorderWidth = 1,
                            BorderColor = Color.Black,
                            KeepTogether = true,
                            Tag = "1"
                        };
                        X += lblPuanTur.WidthF;
                        PageHeader.Controls.Add(lblPuanTurBaslik);
                        PageHeader.Controls.Add(lblSinav);
                        Detail.Controls.Add(lblPuanTur);
                    }
                }
            }
            DataTable dt= PublicMetods.orderBYtoTable(table1, "TOPLAM desc");
            this.DataSource = dt;
            FillReportDataFields.Fill(Detail, dt);
        }
    }
}
