using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class DenemeSinaviSinifNetPuanOrt:DevExpress.XtraReports.UI.XtraReport
    {
        #region tanımlamalar
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public int ID_KADEME3 { get; set; }
        public int ID_SINAVTURU { get; set; }
        public string ID_SINAVs { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_DERSs { get; set; }

        DataSet ds = new DataSet();
        DataTable dt1 = new DataTable();
        #endregion
        public DenemeSinaviSinifNetPuanOrt(string tc,string oturum,string donem,string idKademe3,string idSinavturu,string idSinavList,string idSubeList,string idDersList)
        {
            TCKIMLIKNO=tc;
            OTURUM=oturum;

            DONEM=donem;
            ID_KADEME3=Convert.ToInt32(idKademe3);
            ID_SINAVTURU=Convert.ToInt32(idSinavturu);
            ID_SINAVs=idSinavList=="0" ? "[]" : idSinavList;
            ID_SUBEs=idSubeList=="0" ? "[]" : idSubeList;
            //ID_SINIFs=idSinifList=="[0]" ? "[]" : idSinifList;
            ID_DERSs=idDersList=="0" ? "[]" : idDersList;
            
            InitializeComponent();
        }

        private void DenemeSinaviSinifNetPuanOrt_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            //TCKIMLIKNO="32051057542";
            //OTURUM="15C4B03C-64CC-4E34-A90A-69FDA737F242";
            //DONEM="2017";
            ////ID_SINIFs="[0,101677,101671,101679,101672,101676,101680]";
            //ID_SUBEs="[11,444]";
            //ID_KADEME3=18;
            //ID_SINAVTURU=2;

            //ID_DERSs="[]";
            //ID_SINAVs="[152]";


            //pusulam/Rapor.aspx?rapor=Sinav.DenemeSinaviSinifNetPuanOrt&raporTur=pdf&p=32051057542;F93FBC75-79CC-47CC-8755-3E233E2EAF60;2017;18;2;[179];[11];[101677]
            using(Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@DONEM",DONEM);
                b.ParametreEkle("@ID_KADEME3",ID_KADEME3);
                b.ParametreEkle("@ID_SINAVTURU",ID_SINAVTURU);
                b.ParametreEkle("@ID_SINAVs",ID_SINAVs);
                b.ParametreEkle("@ID_SUBEs",ID_SUBEs);
                b.ParametreEkle("@ID_DERSs",ID_DERSs);

                b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                b.ParametreEkle("@OTURUM",OTURUM);
                b.ParametreEkle("@ISLEM",1); // Rapor
                b.ParametreEkle("@ID_MENU",1074);

                ds=b.SorguGetir("sp_DSNetPuanOrtalamalariOS");


                if(ds.Tables[0].Rows.Count>0)
                {
                    dt1=ds.Tables[0];  

                    GroupField grDers = new GroupField("BOLUMNO");
                    GroupHeader1.GroupFields.Add(grDers);

                    this.DataSource=dt1;                    
                    FillReportDataFields.Fill(Detail,dt1);

                }
            }
        }

        private void GroupHeader1_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            string s = this.GetCurrentColumnValue("BOLUMNO").ToString();
            DataRow dr = dt1.Select(string.Format("BOLUMNO='{0}'", s)).CopyToDataTable().Rows[0];
            lblBaslik.Text=dr["KADEME3"].ToString() + " "+dr["SINAVAD"].ToString()+" SINAVI SINIF ORTALAMALARI"  ;
            lblSinavAd.Text=dr["SINAVAD"].ToString()+" ("+dr["SINAVTARIH"].ToString()+")";
            lblDers.Text=dr["DERSAD"].ToString()+" ( Soru Sayısı : "+dr["SORUSAYISI"].ToString()+")";
        }
    }
}
