using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PusulamRapor.Ogretmen
{
    public partial class OgretmenBilgileri : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string TCOGRETMENLIST { get; set; }

        DataSet ds;
        List<string> istisna = new List<string>();
        List<string> htmlYaz = new List<string>();
        List<DataRow> list = new List<DataRow>();
        public XRLabel lbl { get; set; }
        public XRRichText rt { get; set; }
        float LX = 0f;
        float LY = 0f;
        float en = 130f;
        float boy = 50f;

        float sayfaEn = 1169F - 20F;

        public OgretmenBilgileri(string tckimlikno, string oturum, string tcOgretmenList)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            TCOGRETMENLIST = tcOgretmenList;
        }

        private void OgretmenBilgileri_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@TCOGRETMENLIST", TCOGRETMENLIST);
                b.ParametreEkle("@ID_MENU", 1386);
                b.ParametreEkle("@ISLEM", 2);
                ds = b.SorguGetir("sp_OgretmenBilgileri");

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Detail.Controls.Clear();
                    return;
                }

                DataTable ogrenciList = ds.Tables[0];

                istisna.Add("");

                htmlYaz.Add("SUBE");
                htmlYaz.Add("SINIF");
                htmlYaz.Add("KADEME");
                htmlYaz.Add("DERS");
                en = sayfaEn / (9);

                Baslik();
                Icerik();

                this.DataSource = ogrenciList;

                FillReportDataFields.Fill(Detail, ogrenciList);
            }
        }

        private void Baslik()
        {
            LX = 0;
            LY = 0;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    lbl = PublicMetods.lblBaslik(dc.ToString(), LX, LY, en, boy);
                    PageHeader.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }
        }

        private void Icerik()
        {
            LX = 0;
            LY = 0;
            boy = 150f;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    if (htmlYaz.IndexOf(dc.ToString()) == -1)
                    {
                        lbl = PublicMetods.lblDetay(dc.ToString(), LX, LY, en, boy, "1");
                        Detail.Controls.Add(lbl);
                        LX += lbl.WidthF;
                    }
                    else
                    {
                        rt = PublicMetods.lblDetayHtml(dc.ToString(), LX, LY, en, boy, "1");
                        Detail.Controls.Add(rt);
                        LX += rt.WidthF;
                    }
                }
            }
        }

        // Color backcolorBody = Color.White;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //backcolorBody = backcolorBody != Color.White
            //                    ? Color.White
            //                    : Color.FromArgb(255, 212, 216, 249);

        }

    }
}
