using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class OdulListesi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBES { get; set; }
        public decimal MIN_NET { get; set; }
        public int MIN_DERECE { get; set; }
        public int MAX_DERECE { get; set; }
        public int ID_SINAVPUANTURU { get; set; }
        public DataSet ds { get; set; }

        public OdulListesi(string _TCKIMLIKNO, string _OTURUM, string _ID_SINAVs, string _ID_SUBES, string _MIN_NET, string _MIN_DERECE, string _MAX_DERECE, string _ID_SINAVPUANTURU)
        {

            InitializeComponent();
            TCKIMLIKNO = _TCKIMLIKNO;
            OTURUM = _OTURUM;
            ID_SINAVs = _ID_SINAVs;
            ID_SUBES = _ID_SUBES;
            MIN_NET = Convert.ToDecimal(_MIN_NET);
            MIN_DERECE = Convert.ToInt32(_MIN_DERECE);
            MAX_DERECE = Convert.ToInt32(_MAX_DERECE);
            ID_SINAVPUANTURU = Convert.ToInt32(_ID_SINAVPUANTURU);
        }

        private void OdulListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBES", ID_SUBES);
                b.ParametreEkle("@MIN_NET", MIN_NET);
                b.ParametreEkle("@MIN_DERECE", MIN_DERECE);
                b.ParametreEkle("@MAX_DERECE", MAX_DERECE);
                b.ParametreEkle("@ID_SINAVPUANTURU", ID_SINAVPUANTURU);

                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1087);

                ds = b.SorguGetir("sp_OdulListesi");

                GroupField sinavField = new GroupField("ID_SINAV");
                GroupHeader2.GroupFields.Add(sinavField);

                this.DataSource = ds.Tables[0];

                FillReportDataFields.Fill(GroupHeader2, ds.Tables[0]);
                FillReportDataFields.Fill(Detail, ds.Tables[0]);

            }

        }
    }
}
