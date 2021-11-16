using System;
using System.Data;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Web;
using System.IO;
using DevExpress.Compression;

namespace PusulamRapor.Abide
{
    public partial class AbideSiniflaraGoreSorularinCozulmeOranlari : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_ABIDESINAV;
        string ID_KADEME3;
        DataTable DTOGRENCISORU;
        DataTable DTDERSSORUTOPLAM;
        DataTable DTKAMPUSSINIF;
        DataTable DTDERS;
        public AbideSiniflaraGoreSorularinCozulmeOranlari(string TCKIMLIKNO, string OTURUM, string ID_ABIDESINAV, string ID_KADEME3)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_ABIDESINAV = ID_ABIDESINAV;
            this.ID_KADEME3 = ID_KADEME3;
            InitializeComponent();
        }

        private void AbideSiniflaraGoreSorularinCozulmeOranlari_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ABIDESINAV", ID_ABIDESINAV);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_MENU", 1169);
                b.ParametreEkle("@ISLEM", 23);
                DataSet ds = b.SorguGetir("sp_Abide");

                DTOGRENCISORU = ds.Tables[0];
                DTDERSSORUTOPLAM = ds.Tables[1];
                DTKAMPUSSINIF = ds.Tables[2];
                DTDERS = ds.Tables[3];

                float Y = 0;

                foreach (DataRow DERSROW in DTDERS.Rows)
                {
                    string DERS = DERSROW["DERS"].ToString();
                    int SORUSAYISI = Convert.ToInt32(DERSROW["SORUSAYISI"]);

                    DataTable table = new DataTable();
                    table.Columns.Add("KAMPÜS", typeof(string));
                    table.Columns.Add("SINIF", typeof(string));

                    if (DTDERSSORUTOPLAM.Select("DERS='" + DERS + "'").Length > 0)
                    {
                        foreach (DataRow DERSSORU in DTDERSSORUTOPLAM.Select("DERS='" + DERS + "'").CopyToDataTable().Rows)
                        {
                            table.Columns.Add("SORU " + DERSSORU["SORUNO"] + Environment.NewLine + DERSSORU["BECERI"] + Environment.NewLine + DERSSORU["KAZANIM"], typeof(string));
                        }
                    }

                    foreach (DataRow KAMPUSSINIFROW in DTKAMPUSSINIF.Rows)
                    {
                        if (DTOGRENCISORU.Select("DERS = '" + DERS + "' AND KAMPUS = '" + KAMPUSSINIFROW["KAMPUS"] + "' AND SINIF = '" + KAMPUSSINIFROW["SINIF"] + "'").Length > 0)
                        {
                            DataTable DTSORU = DTOGRENCISORU.Select("DERS = '" + DERS + "' AND KAMPUS = '" + KAMPUSSINIFROW["KAMPUS"] + "' AND SINIF = '" + KAMPUSSINIFROW["SINIF"] + "'").CopyToDataTable();

                            DataRow newdr = table.NewRow();
                            newdr["KAMPÜS"] = KAMPUSSINIFROW["KAMPUS"].ToString();
                            newdr["SINIF"] = KAMPUSSINIFROW["SINIF"].ToString();
                            foreach (DataRow SORU in DTSORU.Rows)
                            {
                                newdr["SORU " + SORU["SORUNO"] + Environment.NewLine + SORU["BECERI"] + Environment.NewLine + SORU["KAZANIM"]] = SORU["ORAN"];
                            }

                            table.Rows.Add(newdr);
                        }
                    }

                    {
                        DataRow newdr = table.NewRow();
                        newdr["KAMPÜS"] = "GENEL";
                        newdr["SINIF"] = "GENEL";

                        if (DTDERSSORUTOPLAM.Select("DERS='" + DERS + "'").Length > 0)
                        {
                            foreach (DataRow DERSSORU in DTDERSSORUTOPLAM.Select("DERS='" + DERS + "'").CopyToDataTable().Rows)
                            {
                                newdr["SORU " + DERSSORU["SORUNO"] + Environment.NewLine + DERSSORU["BECERI"] + Environment.NewLine + DERSSORU["KAZANIM"]] = DERSSORU["ORAN"];
                            }
                        }

                        table.Rows.Add(newdr);
                    }

                    DataTable tableSIRALI = PublicMetods.orderBYtoTable(table, "KAMPUS, SINIF");

                    AbideSiniflaraGoreSorularinCozulmeOranlariSub subreport = new AbideSiniflaraGoreSorularinCozulmeOranlariSub(tableSIRALI, OTURUM, DERS);
                    XRSubreport report = new XRSubreport();
                    report.ReportSource = subreport;
                    report.LocationF = new PointF(0, Y);
                    report.CanGrow = true;
                    Detail.Controls.Add(report);

                    Y += (tableSIRALI.Rows.Count * 23);
                }
            }
        }

        private void AbideSiniflaraGoreSorularinCozulmeOranlari_AfterPrint(object sender, EventArgs e)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip")))
            {
                File.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));
            }

            string path = HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + "");

            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            using (ZipArchive archive = new ZipArchive())
            {
                foreach (FileInfo file in di.GetFiles())
                {
                    archive.AddFile(path + "/" + file.Name, "/");
                }
                archive.Save(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/" + OTURUM + ".zip"));

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(HttpContext.Current.Server.MapPath("/Dosyalar/AbideRapor/Temp/" + OTURUM + ""), true);
        }
    }
}
