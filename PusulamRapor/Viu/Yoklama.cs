
using DevExpress.XtraReports.UI;
using System.Data;
using System.Drawing;

namespace PusulamRapor.Viu
{
    public partial class Yoklama : DevExpress.XtraReports.UI.XtraReport
    {
        DataSet ds;
        public Yoklama(string TCKIMLIKNO, string OTURUM, string ID_SUBELER,string ID_SINIFLAR,  string BASTARIH, string BITTARIH)
        {
            InitializeComponent();

            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@ISLEM", 6);
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);

                b.ParametreEkle("@BASTARIH", BASTARIH);
                b.ParametreEkle("@BITTARIH", BITTARIH);
                b.ParametreEkle("@ID_SUBELER", ID_SUBELER);
                b.ParametreEkle("@ID_SINIFLAR", ID_SINIFLAR);
                //b.ParametreEkle("@TCOGRETMENLIST", TCOGRETMENLIST);
                //b.ParametreEkle("@ID_ARAMASEBEPLIST", ID_ARAMASEBEPLIST);
                //b.ParametreEkle("@ID_ARAMADURUMLIST", ID_ARAMADURUMLIST);
                b.ParametreEkle("@JSON", 0);
                b.ParametreEkle("@ID_MENU", 1236);
                ds = b.SorguGetir("sp_VIU");
                Font FONTHEADER = new System.Drawing.Font(new FontFamily("TAHOMA"), 7, FontStyle.Bold);
                Font FONTROW = new System.Drawing.Font(new FontFamily("TAHOMA"), 7, FontStyle.Regular);

                float X = 940.4f;
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    if (i>6)
                    {
                        XRLabel LABEL = PublicMetods.lblEkle(ds.Tables[0].Columns[i].ColumnName, X, 39.37f, 169.15f, 24.14f, Color.FromArgb(255, 105, 105, 105), Color.White, Color.White, FONTHEADER);
                        LABEL.Text = ds.Tables[0].Columns[i].ColumnName;
                        PageHeader.Controls.Add(LABEL);

                        XRLabel LABELx = PublicMetods.lblEkle(ds.Tables[0].Columns[i].ColumnName, X, 0F, 169.15f, 24.14f, Color.FromArgb(255, 211, 211, 211), Color.Black, Color.White, FONTROW);
                        LABELx.Text = ds.Tables[0].Columns[i].ColumnName;
                        LABELx.Tag = "1";
                        Detail.Controls.Add(LABELx);

                        X += LABEL.WidthF;
                    }
                }
                xrLabel1.WidthF = X;
                this.DataSource = ds.Tables[0];
            }
        }

        private void Yoklama_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                FillReportDataFields.FillPanel(Detail, ds.Tables[0]);
            }
        }
    }
}
