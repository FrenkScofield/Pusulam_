using DevExpress.XtraPrinting.Drawing;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace PusulamRapor.YetenekGelisim
{
    public partial class YG_UstSinifKarne : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public int ID_KATEGORIOGRENCI { get; set; }
        public DataSet ds { get; set; }

        public DataTable dt1 { get; set; }
        public DataTable dt2 { get; set; }

        public YG_UstSinifKarne(string tc, string oturum, string idKategoriOgrenci)
        {
            InitializeComponent();
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            ID_KATEGORIOGRENCI = idKategoriOgrenci == "" ? 0 : Convert.ToInt32(idKategoriOgrenci);
        }

        private void YG_UstSinifKarne_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (dt1 == null)
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@ID_KATEGORIOGRENCI", ID_KATEGORIOGRENCI);
                    b.ParametreEkle("@ISLEM", 5); // Rapor
                    b.ParametreEkle("@ID_MENU", 1171);

                    ds = b.SorguGetir("sp_YG_YetenekGelisim");

                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                }
            }

            int idKategori = Convert.ToInt32(dt1.Rows[0]["ID_KATEGORI"].ToString());

            //string yol2 = "";
            //switch (idKategori)
            //{
            //    case 1: yol2 = "Dosyalar\\YetenekGelisim\\muzik.png"; break;
            //    case 2: yol2 = "Dosyalar\\YetenekGelisim\\resim.png"; break;
            //    case 3: yol2 = "Dosyalar\\YetenekGelisim\\tiyatro.png"; break;
            //    case 4: yol2 = "Dosyalar\\YetenekGelisim\\satranc.png"; break;
            //    case 5: yol2 = "Dosyalar\\YetenekGelisim\\dans.png"; break;
            //}
            //XRPictureBox pb = new XRPictureBox();
            //string yol = AppDomain.CurrentDomain.BaseDirectory;
            //pb.Image = Image.FromFile(yol + yol2);
            //pb.SizeF = new SizeF(183.78f, 93.06f);//183,78; 93,06
            //pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
            //pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            //pb.LocationF = new PointF(933.8f, 723.94f);//933,8; 723,94
            //Detail.Controls.Add(pb);

            this.Watermark.ShowBehind = true;

            this.DataSource = dt1;
            FillReportDataFields.Fill(Detail, dt1);

            int idKategoriTavsiye = Convert.ToInt32(dt2.Rows[0]["ID_KATEGORITAVSIYE"].ToString());
            pb_y1.Visible = false;
            pb_y2.Visible = false;
            pb_y3.Visible = false;
            switch (idKategoriTavsiye)
            {
                case 1: pb_y1.Visible = true; break;
                case 2: pb_y2.Visible = true; break;
                case 3: pb_y3.Visible = true; break;
            }

            XRPictureBox p = new XRPictureBox();
            List<float> xK = new List<float>() {
                        (float) (1022.54),
                        (float) (913.71),
                        (float) (803.95),
                        (float) (695.67),
                        (float) (582.36)
                    };

            foreach (DataRow dr in PublicMetods.orderBYtoTable(dt2, "ID_KATEGORIPUAN").Rows)
            {
                int idKategoriPuan = Convert.ToInt32(dr["ID_KATEGORIPUAN"].ToString());
                int puan = Convert.ToInt32(dr["PUAN"].ToString());

                switch (idKategoriPuan)
                {
                    case 1: p = pb_1; break;
                    case 2: p = pb_2; break;
                    case 3: p = pb_3; break;
                    case 4: p = pb_4; break;
                    case 5: p = pb_5; break;
                }

                p.Visible = true;
                p.LocationF = new PointF(xK[puan - 1], p.LocationF.Y);
            }


        }

        private void YG_UstSinifKarne_AfterPrint(object sender, EventArgs e)
        {
            //XRWatermark textWatermark = new XRWatermark();


            //textWatermark.Image = Properties.Resources.UstSinifYetenekKarnesi;

            //textWatermark.ImageAlign = ContentAlignment.MiddleCenter;
            //textWatermark.ImageViewMode = ImageViewMode.Stretch;
            //textWatermark.ShowBehind = true;


            ///this.Watermark.Image = ((System.Drawing.Image)(Properties.Resources.UstSinifYetenekKarnesi));
            ///this.Watermark.ImageViewMode = DevExpress.XtraPrinting.Drawing.ImageViewMode.Stretch;
            ///
            ///this.PrintingSystem.Watermark.CopyFrom(this.Watermark);
            ///this.PrintingSystem.Pages.AddRange(this.Pages);


        }
        //private Watermark CreateTextWatermark()
        //{
        //    Watermark textWatermark = new Watermark();
            
            
        //    textWatermark.Image = Properties.Resources.UstSinifYetenekKarnesi;
            
        //    textWatermark.ImageAlign = ContentAlignment.MiddleCenter;
        //    textWatermark.ImageViewMode = ImageViewMode.Stretch;
        //    textWatermark.ShowBehind = true;

            
        //    return textWatermark;
        //}
    }
}