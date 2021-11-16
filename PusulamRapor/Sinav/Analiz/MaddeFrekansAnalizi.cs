using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav.Analiz
{
    public partial class MaddeFrekansAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string OGRENCIDONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ICDISOGRENCI { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }
        public string SINIFALANLIST { get; set; }

        DataSet ds = new DataSet();
        DataTable TblSinavOzellik = new DataTable();
        DataTable TblVeri = new DataTable();
        DataTable TblBolumNo = new DataTable();
        #endregion
        public MaddeFrekansAnalizi(string tc, string oturum, string ogrenciDonem, string idKademe3, string idSinavList, string idSubeList, string idDersList, string sinifAlanList,string icDisOgrenci)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;

            OGRENCIDONEM = ogrenciDonem;
            ID_KADEME3 = Convert.ToInt32(idKademe3);
            ID_SINAVs = idSinavList == "0" ? "[]" : idSinavList;
            ID_SUBEs = idSubeList == "0" ? "[]" : idSubeList;
            ID_DERSs = idDersList == "[0]" ? "[]" : idDersList;
            SINIFALANLIST = sinifAlanList == "[0]" ? "[]" : sinifAlanList;
            ICDISOGRENCI = Convert.ToInt32(icDisOgrenci);


            InitializeComponent();
        }

        private void MaddeFrekansAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {


            using (Baglanti b = new Baglanti())
            {


                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@OGRENCIDONEM", OGRENCIDONEM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_SINAVs", ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                b.ParametreEkle("@ID_DERSs", ID_DERSs);
                b.ParametreEkle("@SINIFALANLIST", SINIFALANLIST);
                b.ParametreEkle("@ICDISOGRENCI", ICDISOGRENCI);
                b.ParametreEkle("@ISLEM", 2); // Rapor
                b.ParametreEkle("@ID_MENU", 1113);
                
                ds = b.SorguGetir("sp_MaddeFrekansAnalizi");

                if (ds.Tables[1].Rows.Count > 0)
                {
                    TblVeri = ds.Tables[0];  // veri
                    TblSinavOzellik = ds.Tables[1];  // sube/sınıf list
                    TblBolumNo = ds.Tables[2];  // ogrsay

                    this.DataSource = TblBolumNo;
                    FillReportDataFields.Fill(ReportHeader, TblSinavOzellik);

                    GroupField grpField = new GroupField("BOLUMNO");
                    GroupHeader1.GroupFields.Add(grpField);
                    
                    string bugun = String.Format("{0:dd/MM/yy}", DateTime.Now);
                    lblRaporTarihi.Text = bugun;

                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int bolumNo = Convert.ToInt32(GetCurrentColumnValue("BOLUMNO").ToString());

            MFADersSoru dersSoru = new MFADersSoru(TblVeri.Select("BOLUMNO="+ bolumNo).CopyToDataTable(), TblBolumNo.Select("BOLUMNO=" + bolumNo).CopyToDataTable());
            srDersSoru.ReportSource = dersSoru;
        }
    }
}
