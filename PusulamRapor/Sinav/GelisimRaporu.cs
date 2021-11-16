using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class GelisimRaporu:DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string TC_OGRENCI { get; set; }
        public string DONEM { get; set; }
        public string ID_DERSs { get; set; }
        public string ID_SINAVPUANTURUs { get; set; }
        public bool TN { get; set; }
        public int ID_MENU { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();

        //p=' + session.TCKIMLIKNO + ';' + session.OTURUM + ';' + this.tcogrenci + ';' + this.dersList + ';' + this.ptList + ';' + this.TN + ';'+ this.DONEM + ";" + 0, '_blank');
        public GelisimRaporu(string tc,string oturum,string tcOgrenci,string idDersList,string idptList,string tn,string donem,string which)
        {
            TC_OGRENCI=tcOgrenci;
            DONEM=donem;
            //ID_DERS=Convert.ToInt32(idDers);
            ID_DERSs=idDersList;
            ID_SINAVPUANTURUs=idptList;
            TN=Convert.ToBoolean(tn);
            TCKIMLIKNO=tc;
            OTURUM=oturum;
            ID_MENU=which.Equals("0") ? 1056 : which.Equals("1") ? 1045: 1087;

            InitializeComponent();
        }

        private void GelisimRaporu_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {

            using(Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                b.ParametreEkle("@OTURUM",OTURUM);
                b.ParametreEkle("@DONEM",DONEM);
                b.ParametreEkle("@TC_OGRENCI",TC_OGRENCI);
                b.ParametreEkle("@ID_DERSs",ID_DERSs);
                b.ParametreEkle("@ID_SINAVPUANTURUs",ID_SINAVPUANTURUs);
                b.ParametreEkle("@TN",TN);
                b.ParametreEkle("@ISLEM",2); // Rapor
                b.ParametreEkle("@ID_MENU",ID_MENU);

                ds=b.SorguGetir("sp_GelisimRaporu");


                if(ds.Tables[0].Rows.Count>0)
                {

                    GroupField grpField = new GroupField("TCKIMLIKNO");
                    GroupHeader1.GroupFields.Add(grpField);
                   
                    GroupField grpField2 = new GroupField("DERSAD");
                    GroupHeader2.GroupFields.Add(grpField2);
                    

                    this.DataSource=ds.Tables[0].DefaultView.ToTable(true,"TCKIMLIKNO","DERSAD");

                    dt1=ds.Tables[0];
                    dt2=ds.Tables[1];
                    dt3=ds.Tables[2];

                    //grDersSinav ders = new grDersSinav(dt1,false);
                    //srDersSinav.ReportSource=ders;

                   
                }

            }
        }

        private void Detail_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {

        }
        string tc = "";
        private void GroupHeader1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            tc=GetCurrentColumnValue("TCKIMLIKNO").ToString();
            DataTable d = dt1.Select(String.Format("TCKIMLIKNO = '{0}'",tc)).CopyToDataTable();
            FillReportDataFields.Fill(GroupHeader1,d);

        }

        private void PageHeader_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void GroupHeader2_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            DataTable d = dt1.Select(String.Format("TCKIMLIKNO = '{0}' AND DERSAD = '{1}'",tc,GetCurrentColumnValue("DERSAD").ToString())).CopyToDataTable();

            grDersSinav ders = new grDersSinav(d,false);
            srDersSinav.ReportSource=ders;

            

        }

        private void GroupFooter1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {

            if(dt2.Rows.Count>0&&TN==true)
            {
                grDersSinav net = new grDersSinav(dt2,true);
                srPuanNet.ReportSource=net;
            }
            if(dt3.Rows.Count>0)
            {
                grPuanSinav puan = new grPuanSinav(dt3);
                srPuanSinav.ReportSource=puan;
            }

            if (ds.Tables[4].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).Length > 0)
            {
                DataTable d5 = ds.Tables[4].Select(String.Format("TCKIMLIKNO = '{0}'", tc)).CopyToDataTable();
                if (d5.Rows.Count > 0)
                {
                    grEtutRaporu gretutraporu = new grEtutRaporu(d5);
                    srEtutRaporu.ReportSource = gretutraporu;
                }
            }
        }
    }
}
