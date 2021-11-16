using System.Drawing;
using DevExpress.XtraReports.UI;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PusulamRapor.SinifListesi
{
    public partial class SinifListeRaporu : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINIFLAR { get; set; }
        public string ID_SUBELER { get; set; }
        public bool VELI { get; set; }
        public bool DANISMAN_OGRETMEN { get; set; }
        DataSet ds;
        List<string> istisna = new List<string>();
        List<string> Kur = new List<string>();
        List<DataRow> list = new List<DataRow>();
        bool kontrol = false;
        public XRLabel lbl { get; set; }
        float LX = 0f;
        float LY = 0f;
        float en = 130f;
        float boy = 50f;

        float sayfaEn = 827f - 160f;

        public SinifListeRaporu(string tckimlikno, string oturum, string idSiniflar, string idSubeler, string veli/*, string idKademe3*/)
        {
            InitializeComponent();
            TCKIMLIKNO = tckimlikno;
            OTURUM = oturum;
            ID_SINIFLAR = idSiniflar;
            ID_SUBELER = idSubeler;
            VELI = veli == "1";
            if (veli == "2")
            {
                DANISMAN_OGRETMEN = true;
            }

            //ID_KADEME3 = idKademe3;
        }

        private void SinifListeRaporu_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                //b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@VELI", VELI);
                b.ParametreEkle("@DANISMAN_OGRETMEN", DANISMAN_OGRETMEN);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@ID_MENU", 1098);
                b.ParametreEkle("@ISLEM", 1);
                ds = b.SorguGetir("sp_SinifListeRaporu");

                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    Detail.Controls.Clear();
                    return;
                }

                DataTable ogrenciList = ds.Tables[0];// PublicMetods.orderBYtoTable(ds.Tables[0], "SUBEAD,SIRA,SINIF,TCKIMLIKNO,SEVIYE");

                List<DataRow> list = ogrenciList.AsEnumerable().ToList();
                foreach (var item in list)
                {
                    string tip = item.ItemArray[1].GetType().ToString();
                    if (tip != "System.DBNull")
                    {
                        kontrol = true;
                        break;
                    }
                }

                int veliKolon = VELI ? 2 : 0;
                int danismanKolon = DANISMAN_OGRETMEN ? 2 : 0;
                if (DANISMAN_OGRETMEN)
                {
                    istisna.Add("SIRA");
                    en = sayfaEn / (6 + danismanKolon);
                }
                if (VELI)
                {
                    istisna.Add("SIRA");
                    en = sayfaEn / (6 + veliKolon);
                }



                if (!kontrol)
                {
                    istisna.Add("KUR SEVİYESİ");
                    if (DANISMAN_OGRETMEN)
                    {
                        en = sayfaEn / (5 + danismanKolon);
                    }
                    if (VELI)
                    {
                        en = sayfaEn / (5 + veliKolon);

                    }
                }

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
            boy = 50f;

            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (istisna.IndexOf(dc.ToString()) == -1)
                {
                    lbl = PublicMetods.lblDetay(dc.ToString(), LX, LY, en, boy, "1");
                    Detail.Controls.Add(lbl);
                    LX += lbl.WidthF;
                }
            }
        }

        int i = 0;
        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Color backcolorBody;
            if (i % 2 == 0)
            {
                backcolorBody = Color.White;
            }
            else
            {
                backcolorBody = Color.FromArgb(255, 212, 216, 249);
            }
            i++;
        }

    }
}
