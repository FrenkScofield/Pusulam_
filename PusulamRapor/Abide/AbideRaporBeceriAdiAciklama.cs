using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;

namespace PusulamRapor.Abide
{
    public partial class AbideRaporBeceriAdiAciklama : DevExpress.XtraReports.UI.XtraReport
    {
        DataTable DTBECERI;
        DataTable DTBECERIKAPAK;
        public AbideRaporBeceriAdiAciklama(DataTable DTBECERI, DataTable DTBECERIKAPAK)
        {
            this.DTBECERI = DTBECERI;
            this.DTBECERIKAPAK = DTBECERIKAPAK;
            InitializeComponent();
        }

        private void AbideRaporBeceriAdiAciklama_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GroupHeader1.GroupFields.Add(new GroupField("BOLUMNO"));
            GroupHeader1.GroupFields.Add(new GroupField("DERS"));


            int resimGor = Convert.ToInt32(DTBECERI.Rows[0]["RESIMGOR"]);


            /*		RESIMGOR (Beceri Adı ve Açıklama)
                0 => Resim + Aciklama
                1 => Sadece Aciklama 
                2 => Sadece Resim
            */

            if (resimGor == 0 || resimGor == 2)
            {                               
                string ID_ABIDESINAV = DTBECERI.Rows[0]["ID_ABIDESINAV"].ToString();
                float y = 0;
                foreach (DataRow dr in DTBECERIKAPAK.Rows)
                {
                    string RESIMAD = dr["AD"].ToString();
                    XRPictureBox pb = new XRPictureBox();
                    string yol = AppDomain.CurrentDomain.BaseDirectory;
                    pb.Image = Image.FromFile(yol + "Dosyalar\\AbideResim\\" + ID_ABIDESINAV + "\\" + 5 + "\\" + RESIMAD + ".png");
                    pb.SizeF = new SizeF(827f, 1169f);
                    pb.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.MiddleCenter;
                    pb.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
                    pb.LocationF = new PointF(0, y);
                    ReportHeader.Controls.Add(pb);

                    y += pb.HeightF;
                }
                if (resimGor == 2)
                {
                    GroupHeader1.Controls.Clear();
                    Detail.Controls.Clear();
                }
            }
            if (resimGor == 0 || resimGor == 1)
            {
                this.DataSource = DTBECERI;
                FillReportDataFields.Fill(GroupHeader1, DTBECERI);
                FillReportDataFields.FillPanel(Detail, DTBECERI);

            }

        }
    }
}
