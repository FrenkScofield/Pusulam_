using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Data;
using System.Net;

namespace PusulamRapor.Mete
{
    public partial class IbanListesi : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable dt;
        public IbanListesi(string TCKIMLIKNO, string OTURUM)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 1);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@ID_MENU", 1368);
                DataSet ds = b.SorguGetir("sp_GenelHak");
                bool GENELYETKI = bool.Parse(ds.Tables[0].Rows[0][0].ToString());

                if (GENELYETKI)
                {
                    using (WebClient client = new WebClient())
                    {
                        byte[] response =
                        client.UploadValues("https://api.xbase.web.tr/xBase/OgrenciSozlesmeBorclusuIbanListesiDetayli", new NameValueCollection()
                        {{ "APIKEY", "483747ce-e66d-4c3f-b299-ca4761bc41a4" }});

                        string result = System.Text.Encoding.UTF8.GetString(response);
                        dt = JsonConvert.DeserializeObject<DataTable>(result);
                    }

                    this.DataSource = dt;
                }
            }
        }

        private void IbanListesi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, dt);
            }
        }
    }
}
