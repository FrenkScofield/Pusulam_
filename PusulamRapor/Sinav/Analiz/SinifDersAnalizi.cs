using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class SinifDersAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }
        public int GRUPLAMATURU { get; set; }

        DataSet ds = new DataSet();

        DataTable TblSinifList = new DataTable();
        DataTable TblSinavList = new DataTable();
        DataTable TblBolumList = new DataTable();
        DataTable TblKazanimList = new DataTable();

        FontFamily ff = new FontFamily("Tahoma");

        #endregion
        public SinifDersAnalizi(string tc, string oturum, string donem, string idKademe3, string idSinavList, string idSubeList, string idSinifList, string idDersList, string GRUPLAMATURU)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            DONEM = donem;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_SINIFs = idSinifList == "[0]" ? "[]" : idSinifList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;
            this.GRUPLAMATURU = Convert.ToInt32(GRUPLAMATURU);

            InitializeComponent();
        }

        private void SinifDersAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@GRUPLAMATURU", GRUPLAMATURU);
                b.ParametreEkle("@ISLEM", 4); // Rapor Yeni
                b.ParametreEkle("@ID_MENU", 1111);

                ds = b.SorguGetir("sp_SinifDersAnalizi");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    TblSinifList = ds.Tables[0];
                    TblSinavList = ds.Tables[1];
                    TblBolumList = ds.Tables[2];
                    TblKazanimList = ds.Tables[3];

                    this.DataSource = TblSinifList;
                    FillReportDataFields.Fill(GroupHeader2, TblSinifList);

                    GroupField grpField = new GroupField("ID_SINIF");
                    GroupHeader2.GroupFields.Add(grpField);

                    string bugun = String.Format("{0:dd/MM/yy}", DateTime.Now);
                    lblRaporTarihi.Text = bugun;
                }
            }
        }
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int idSinif = Convert.ToInt32(GetCurrentColumnValue("ID_SINIF").ToString());

            sdaSinav sinav = new sdaSinav(TblSinavList.Select("ID_SINIF=" + idSinif).CopyToDataTable());
            srSinav.ReportSource = sinav;

            sdaSinifKazanim sinifKazanim = new sdaSinifKazanim(TblKazanimList.Select("ID_SINIF=" + idSinif).CopyToDataTable(), TblBolumList.Select("ID_SINIF=" + idSinif).CopyToDataTable(), GRUPLAMATURU);
            srBasari.ReportSource = sinifKazanim;
        }

    }
}
