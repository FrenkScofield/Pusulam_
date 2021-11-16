using System;
using System.Drawing;
using System.Data;
using DevExpress.XtraCharts;

namespace PusulamRapor.Odev
{
    public partial class OdevOzetRapor : DevExpress.XtraReports.UI.XtraReport
    {
        Font font12b = new Font(new FontFamily("Tahoma"), 12, FontStyle.Bold);
        Font font12r = new Font(new FontFamily("Tahoma"), 12, FontStyle.Regular);
        public OdevOzetRapor(string tc, string oturum, string idsubelist, string idkademe3list, string idderslist, string tcogretmenlist, string donem, string idodevtur, string baslangictarihi, string bitistarihi)
        {
            InitializeComponent();
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", tc);
                b.ParametreEkle("@OTURUM", oturum);
                b.ParametreEkle("@ID_SUBELIST", idsubelist);
                b.ParametreEkle("@ID_KADEME3LIST", idkademe3list);
                b.ParametreEkle("@ID_DERSLIST", idderslist);
                b.ParametreEkle("@TC_OGRETMENLIST", tcogretmenlist);
                b.ParametreEkle("@DONEM", donem);
                b.ParametreEkle("@ID_ODEVTUR", idodevtur);
                b.ParametreEkle("@BASLANGIC_TARIHI", baslangictarihi);
                b.ParametreEkle("@BITIS_TARIHI", bitistarihi);
                b.ParametreEkle("@RAPORMU", 1);
                b.ParametreEkle("@ISLEM", 16);
                b.ParametreEkle("@ID_MENU", 1232);

                DataSet ds = b.SorguGetir("sp_Odev");

                this.DataSource = ds.Tables[0];
                FillReportDataFields.Fill(Detail, ds.Tables[0]);
            }
        }
    }
}
