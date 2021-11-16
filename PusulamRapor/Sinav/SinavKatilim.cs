using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Sinav
{
    public partial class SinavKatilim:DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }

        public string DONEM { get; set; }
        public string DONEMSINAV { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_SINAVs { get; set; }
        public int ID_SINAVTURU { get; set; }
        public int ID_KADEME3 { get; set; }

        public int Secim { get; set; }

        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataTable t3 = new DataTable();
        DataSet ds = new DataSet();

        public SinavKatilim(string tckimlikno,string oturum, string donem, string sinavDonem, string idSubeList,string idSinifList,string idSinavList,string idSinavTuru,string idKademe3,string secim)//,string idsinavTuru
        {
            InitializeComponent();
            TCKIMLIKNO=tckimlikno;
            OTURUM=oturum;

            DONEM=donem;
            DONEMSINAV = sinavDonem;
            ID_SUBEs=idSubeList;
            ID_SINIFs=idSinifList;
            ID_SINAVs =idSinavList;
            ID_SINAVTURU=Convert.ToInt32(idSinavTuru);
            ID_KADEME3=Convert.ToInt32(idKademe3);
            Secim=Convert.ToInt32(secim);

        }

        private void SinavKatilim_BeforePrint(object sender,System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using(Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO",TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM",OTURUM);
                    b.ParametreEkle("@DONEM", DONEM);
                    b.ParametreEkle("@DONEMSINAV", DONEMSINAV);
                    b.ParametreEkle("@ID_SUBEs",ID_SUBEs);
                    b.ParametreEkle("@ID_SINIFs",ID_SINIFs);
                    b.ParametreEkle("@ID_SINAVs",ID_SINAVs);
                    b.ParametreEkle("@ID_SINAVTURU",ID_SINAVTURU);
                    b.ParametreEkle("@ID_KADEME3",ID_KADEME3);
                    b.ParametreEkle("@ISLEM",2); // Rapor
                    b.ParametreEkle("@ID_MENU", 1059);

                    ds =b.SorguGetir("sp_SinavKatilim");

                    //GroupField ogrenciField = new GroupField("TCKIMLIKNO");
                    //GroupHeader1.GroupFields.Add(ogrenciField);

                    if(ds.Tables[0].Rows.Count>0)
                    {
                        //this.DataSource=ds.Tables[0];
                        t1=ds.Tables[0];
                        t2=ds.Tables[1];
                        t3=ds.Tables[2];
                    }
                }

                if(ID_SINIFs!="[]")
                {
                    t1=t3;
                }

                if(Secim==1) // okul/sınıf bazlı rapor
                {
                    skOkulKatilim okul = new skOkulKatilim(t1,t2);
                    srSinavKatilimOkul.ReportSource=okul;
                }
                else if(Secim==2) // DENEME SINAVLARINA KATILMAYAN ÖĞRENCİ SAYISI
                {
                    skKatilmayanYuzde katilmayan = new skKatilmayanYuzde(t1,t2,false); // false katilmayan
                    srSinavKatilimKatilmayan.ReportSource=katilmayan;
                }
                else if(Secim==3) // DENEME SINAVLARINA KATILMAYAN ÖĞRENCİ SAYISI
                {
                    skKatilmayanYuzde katilmayan = new skKatilmayanYuzde(t1,t2,true);
                    srSinavKatilimKatilmayan.ReportSource=katilmayan;
                }

            }
            catch(Exception)
            {

                throw;
            }
        }
    }
}
