using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.PerformansDegerlendirmeSonuclari
{
    public partial class DegerlendirmeYapmayanlar : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_KADEME;
        string ID_DEGERLENDIRMEPERIYOT;
        string DURUM;

        public DegerlendirmeYapmayanlar(string TCKIMLIKNO, string OTURUM, string ID_KADEME, string ID_DEGERLENDIRMEPERIYOT, string DURUM)
        {
            InitializeComponent();

            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_KADEME = ID_KADEME;
            this.ID_DEGERLENDIRMEPERIYOT = ID_DEGERLENDIRMEPERIYOT;
            this.DURUM = DURUM;

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_DEGERLENDIRMEPERIYOT", ID_DEGERLENDIRMEPERIYOT);
                b.ParametreEkle("@ID_KADEME", ID_KADEME);
                b.ParametreEkle("@DURUM", DURUM);
                b.ParametreEkle("@ID_MENU", 1165);
                b.ParametreEkle("@ISLEM", 27);
                DataSet ds = b.SorguGetir("sp_Degerlendirme");
                this.DataSource = ds.Tables[0];
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }

    }
}
