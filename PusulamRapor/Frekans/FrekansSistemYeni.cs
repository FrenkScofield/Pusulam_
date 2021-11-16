
using System.Data;

namespace PusulamRapor.Frekans
{
    public partial class FrekansSistemYeni : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public FrekansSistemYeni(string TCKIMLIKNO, string OTURUM, string ID_SUBE, string ID_SINAVTURU, string ID_KADEMEs, string ID_KADEME3,string DERSLIST,string DONEM)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti()) // değişecek
            {

                //DERSLIST: "["Tümü","Din Kült. Ve A. B.","Fen Bilimleri","İngilizce","Matematik","Sosyal Bilgiler","Türkçe"]"
                //DONEM: "2020"
                //ID_KADEME3: "[12]"
                //ID_KADEMEs: "[4]"
                //ID_SINAVTURU: "[-1,8,9]"
                //ID_SUBE: "[236]"
                //OTURUM: "8F3FD760-53E6-451F-9D42-2681B36933DE"
                //TCKIMLIKNO: "31913154276"


                b.ParametreEkle("@RAPOR_DONUS_TIP", 1);
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_KADEMEs", ID_KADEMEs);     
                b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);               
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@ID_MENU", 1099);
                b.ParametreEkle("@DERSLIST", DERSLIST);
                b.ParametreEkle("@DONEM", DONEM);

                ds = b.SorguGetir("sp_v2FrekansSistem");
                this.DataSource = ds.Tables[0];
            }
        }

        private void FrekansSistemYeni_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }


       
    }
}
