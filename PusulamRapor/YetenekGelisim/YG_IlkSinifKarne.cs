using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_IlkSinifKarne : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_KATEGORIOGRENCI { get; set; }

        public DataTable dt1 { get; set; }
        public DataTable dt2 { get; set; }

        public YG_IlkSinifKarne(string tc, string oturum, string idKategoriOgrenci)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_KATEGORIOGRENCI = idKategoriOgrenci == "" ? 0 : Convert.ToInt32(idKategoriOgrenci);
                        
        }

        private void YG_IlkSinifKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dt1== null)
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@ID_KATEGORIOGRENCI", ID_KATEGORIOGRENCI);
                    b.ParametreEkle("@ISLEM", 5); // Rapor
                    b.ParametreEkle("@ID_MENU", 1171);

                    DataSet ds = b.SorguGetir("sp_YG_YetenekGelisim");

                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                }
            }
            FillReportDataFields.Fill(ReportHeader, dt1);
            this.DataSource = dt2;
            FillReportDataFields.Fill(Detail, dt2);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int yildiz = Convert.ToInt32(GetCurrentColumnValue("DERECE").ToString());

            string yol2 = "";
            switch (yildiz)
            {
                case 1: yol2 = "Dosyalar\\YetenekGelisim\\1yildiz.png"; break;
                case 2: yol2 = "Dosyalar\\YetenekGelisim\\2yildiz.png"; break;
                case 3: yol2 = "Dosyalar\\YetenekGelisim\\3yildiz.png"; break;
                case 4: yol2 = "Dosyalar\\YetenekGelisim\\4yildiz.png"; break;
                case 5: yol2 = "Dosyalar\\YetenekGelisim\\5yildiz.png"; break;
            }

            string yol = AppDomain.CurrentDomain.BaseDirectory;
            DataTable dt = new DataTable();
            dt.Columns.Add("YILDIZ", typeof(String));
            dt.Rows.Add(yol + yol2);

            pb5.DataBindings.Add("ImageUrl", dt, String.Format("{0}.{1}", dt.TableName, "YILDIZ"));
        }
    }
}
