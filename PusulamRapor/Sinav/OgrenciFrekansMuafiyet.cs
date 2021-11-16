using System.Data;
using System.Drawing;

namespace PusulamRapor.Sinav
{
    public partial class OgrenciFrekansMuafiyet : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public OgrenciFrekansMuafiyet(string TCKIMLIKNO, string OTURUM, string ID_KADEME3)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 3);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_MENU", 1198);

                ds = b.SorguGetir("sp_OgrenciFrekansMuaf");
            }
        }

        private void OgrenciFrekansMuafiyet_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    FillReportDataFields.Fill(Detail, ds.Tables[0]);
            //}

            Baslik();
            Icerik();
        }

        public void Baslik()
        {
            Color backColor = Color.DimGray;
            Color foreColor = Color.White;
            Color borderColor = Color.White;
            float LX = 0;
            float LY = 0;
            float lblEn = 180;
            float lblBoy = 25;

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                PageHeader.Controls.Add(PublicMetods.lblEkle(ds.Tables[0].Columns[i].ColumnName, LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor));
                LX += lblEn;
            }
        }

        public void Icerik()
        {
            Color backColor = Color.LightGray;
            Color foreColor = Color.Black;
            Color borderColor = Color.White;
            float LX = 0;
            float LY = 0;
            float lblEn = 180;
            float lblBoy = 25;

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                Detail.Controls.Add(PublicMetods.lblEkle(ds.Tables[0].Columns[i].ColumnName, LX, LY, lblEn, lblBoy, backColor, foreColor, borderColor, "1"));
                LX += lblEn;
            }

            this.DataSource = ds;
            FillReportDataFields.Fill(Detail, ds.Tables[0]);
        }
    }
}
