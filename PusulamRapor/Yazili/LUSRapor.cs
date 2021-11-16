using System;
using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Yazili
{
    public partial class LUSRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string SUBELER { get; set; }
        public string SINIFLAR { get; set; }
        public string TC_OGRENCI { get; set; }
        public string DONEM { get; set; }
        public int ID_MENU { get; set; }

        DataSet ds;
        DataTable dtTEK = new DataTable();

        public LUSRapor(string TCKIMLIKNO, string OTURUM, string SUBELER, string SINIFLAR, string TC_OGRENCI, string DONEM, string ID_MENU)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.SUBELER = SUBELER;
            this.SINIFLAR = SINIFLAR;
            this.TC_OGRENCI = TC_OGRENCI;
            this.DONEM = DONEM;
            this.ID_MENU = Convert.ToInt32(ID_MENU);
            InitializeComponent();
        }

        private void LUSRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@SUBELER", SUBELER);
                b.ParametreEkle("@SINIFLAR", SINIFLAR);
                b.ParametreEkle("@TC_OGRENCI", TC_OGRENCI);
                b.ParametreEkle("@ID_MENU", ID_MENU);
                b.ParametreEkle("@ISLEM", 1);
                ds = b.SorguGetir("sp_LUSRapor");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtTEK = ds.Tables[0];

                    GroupHeader1.GroupFields.Add(new GroupField("TCKIMLIKNO"));
                    this.DataSource = dtTEK;
                    FillReportDataFields.Fill(Detail, dtTEK);
                }
            }
        }
    }
}
