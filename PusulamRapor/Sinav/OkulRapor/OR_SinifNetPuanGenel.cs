using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraCharts;

namespace PusulamRapor.Sinav.OkulRapor
{
    public partial class OR_SinifNetPuanGenel : DevExpress.XtraReports.UI.XtraReport
    {
        bool PUAN;
        DataTable dt2;
        DataTable dt4;
        DataTable dt7;
        DataTable dt8;
        DataTable dt9;
        DataTable dt10;
        DataTable dt11;
        DataTable dtKATILIM;
        string SUBEAD;
        string SUBEIL;
        string SUBEILCE;
        string SINAVAD;
        List<string> dersKisa;
        List<string> dersUzun;

        public OR_SinifNetPuanGenel(bool PUAN, DataTable _dt2, DataTable _dt4, DataTable _dt7, DataTable _dt8, DataTable _dt9, DataTable _dt10, DataTable _dt11, DataTable _dtKATILIM, string _SUBEAD, string _SUBEIL, string _SUBEILCE, string _SINAVAD, List<string> _dersKisa, List<string> _dersUzun)
        {
            InitializeComponent();

            this.PUAN = PUAN;
            this.dt2 = _dt2;
            this.dt4 = _dt4;
            this.dt7 = _dt7;
            this.dt8 = _dt8;
            this.dt9 = _dt9;
            this.dt10 = _dt10;
            this.dt11 = _dt11;
            this.dtKATILIM = _dtKATILIM;
            this.SUBEAD = _SUBEAD;
            this.SUBEIL = _SUBEIL;
            this.SUBEILCE = _SUBEILCE;
            this.SINAVAD = _SINAVAD;
            this.dersKisa = _dersKisa;
            this.dersUzun = _dersUzun;

            DataView view = new DataView(_dt4);
            DataTable distinctValues = view.ToTable(true, "SINIF");

            this.DataSource = distinctValues;
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string sinif = GetCurrentColumnValue("SINIF").ToString();

            if (PUAN)
            {
                if (dt9.Rows.Count > 0 && dt10.Rows.Count > 0 && dt11.Rows.Count > 0 && dtKATILIM.Rows.Count > 0)
                {
                    DataTable Sinifdt9 = dt9.Select("SINIF='" + sinif + "' OR SINIF = ''").CopyToDataTable();
                    DataTable Sinifdt11 = dt11.Select("SINIF='" + sinif + "' OR SINIF = ''").CopyToDataTable();
                    OR_SinifPuanListesi SinifPuanListesi = new OR_SinifPuanListesi(Sinifdt9, dt10, Sinifdt11, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, dersKisa, dersUzun);
                    xrSubreport_SinifPuanListesi.ReportSource = SinifPuanListesi;
                }
            }
            else
            {
                xrSubreport_SinifPuanListesi.Visible = false;
            }


            DataTable Sinifdt4 = dt4.Select("SINIF='" + sinif + "' OR SINIF = ''").CopyToDataTable();
            DataTable Sinifdt7 = PublicMetods.orderBYtoTable(dt7.Select("SINIF='" + sinif + "' OR SINIF = ''").CopyToDataTable(), "ONCELIK, SIRA, TCKIMLIKNO, BOLUMNO");
            DataTable Sinifdt8 = dt8.Select("SINIF='" + sinif + "' OR SINIF = ''").CopyToDataTable();
            OR_SinifNetListesi SinifNetListesi = new OR_SinifNetListesi(Sinifdt7, Sinifdt8, dt2, Sinifdt4, dtKATILIM, SUBEAD, SUBEIL, SUBEILCE, SINAVAD, dersKisa, dersUzun);
            xrSubreport_SinifNetListesi.ReportSource = SinifNetListesi;
        }
    }
}
