using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.ProjeDonem
{
    public partial class ProjeDonemRapor : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string ID_SINIFs { get; set; }
        public int YARIYIL { get; set; }

        public ProjeDonemRapor(string tc, string oturum, string idSinifList,string yariYil)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_SINIFs = idSinifList;
            YARIYIL = Convert.ToInt32(yariYil);
        }

        private void ProjeDonemRapor_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                    b.ParametreEkle("@YARIYIL", YARIYIL);
                    b.ParametreEkle("@ID_MENU", 1190);
                    b.ParametreEkle("@ISLEM", 1); // Rapor

                    DataSet ds = b.SorguGetir("sp_ProjeDonemRapor");

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        Baslik();
                        Icerik();

                        this.DataSource = ds.Tables[0];
                        FillReportDataFields.Fill(Detail, ds.Tables[0]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        float LX = 0;
        float LY = 0;
        float LBLEN = 100;
        float LBLBOY = 40;

        private void Baslik()
        {
            Color backColor = Color.SkyBlue;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.White;

            ReportHeader.Controls.Add(PublicMetods.lblEkle("KAMPÜS", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("SINIF" + Environment.NewLine + "DÜZEYİ", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("SINIF", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("DANIŞMAN" + Environment.NewLine + "ÖĞRETMEN", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("ÖĞRENCİ" + Environment.NewLine + "NO", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;
            ReportHeader.Controls.Add(PublicMetods.lblEkle("ÖĞRENCİ" + Environment.NewLine + "AD SOYAD", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
            LX += LBLEN;

            for (int i = 0; i < 5; i++)
            {

                ReportHeader.Controls.Add(PublicMetods.lblEkle((i + 1) + ". DERS", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor));
                LX += LBLEN;
                ReportHeader.Controls.Add(PublicMetods.lblEkle((i + 1) + ". DERS" + Environment.NewLine + "KONU", LX, LY, LBLEN * 2, LBLBOY, backColor, foreColor, borderColor));
                LX += LBLEN*2;
                ReportHeader.Controls.Add(PublicMetods.lblEkle((i + 1) + ". DERS" + Environment.NewLine + "ÖĞRETMEN", LX, LY, LBLEN * 2, LBLBOY, backColor, foreColor, borderColor));
                LX += LBLEN*2;
            }

        }

        private void Icerik()
        {
            Color backColor = Color.White;
            Color foreColor = Color.MidnightBlue;
            Color borderColor = Color.SkyBlue;

            LX = 0;
            LY = 0;

            LBLBOY = 80;


            Detail.Controls.Add(PublicMetods.lblEkle("SUBEAD", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;
            Detail.Controls.Add(PublicMetods.lblEkle("KADEME3", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;
            Detail.Controls.Add(PublicMetods.lblEkle("SINIFAD", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;
            Detail.Controls.Add(PublicMetods.lblEkle("DANISMANOGRETMEN", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;
            Detail.Controls.Add(PublicMetods.lblEkle("OGRNO", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;
            Detail.Controls.Add(PublicMetods.lblEkle("ADSOYAD", LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
            LX += LBLEN;

            for (int i = 0; i < 5; i++)
            {

                Detail.Controls.Add(PublicMetods.lblEkle("A"+i, LX, LY, LBLEN, LBLBOY, backColor, foreColor, borderColor, "1"));
                LX += LBLEN;
                Detail.Controls.Add(PublicMetods.lblEkle("K" + i, LX, LY, LBLEN * 2, LBLBOY, backColor, foreColor, borderColor, "1"));
                LX += LBLEN*2;
                Detail.Controls.Add(PublicMetods.lblEkle("O" + i, LX, LY, LBLEN * 2, LBLBOY, backColor, foreColor, borderColor, "1"));
                LX += LBLEN*2;
            }

        }
    }
}
