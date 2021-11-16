using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;


namespace PusulamRapor.Yazili
{
    public partial class GenelKazanimAnalizi : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_SUBE { get; set; }
        public string DONEM { get; set; }

        DataSet ds;
        DataTable dtanaliz = new DataTable();
        DataTable dtkazanimlist = new DataTable();

        public GenelKazanimAnalizi(string TCKIMLIKNO, string OTURUM, string ID_SUBE, string DONEM)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_SUBE = Convert.ToInt32(ID_SUBE);
            this.DONEM = DONEM;
            InitializeComponent();
        }

        private void GenelKazanimAnalizi_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_SUBE", ID_SUBE);
                b.ParametreEkle("@ID_MENU", 1093);
                b.ParametreEkle("@DONEM", DONEM);
                b.ParametreEkle("@ISLEM", 2); // 1 ESKİ
                ds = b.SorguGetir("sp_GenelKazanimAnalizi");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    dtanaliz = ds.Tables[0];
                    this.DataSource = dtanaliz;
                    FillReportDataFields.Fill(Detail, dtanaliz);
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

            lblkampus.BackColor = backcolorBody;
            lblgrup.BackColor = backcolorBody;
            lblders.BackColor = backcolorBody;
            lblsinav.BackColor = backcolorBody;
            lblsinif.BackColor = backcolorBody;
            lbladsoyad.BackColor = backcolorBody;
            lblkazanim.BackColor = backcolorBody;
            lblsoruno.BackColor = backcolorBody;
            lblpuan.BackColor = backcolorBody;
            lblpuandegeri.BackColor = backcolorBody;
            i++;
        }
    }
}
