using System;
using System.Data;
using System.Collections.Generic;
using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Web;
using System.IO;
using DevExpress.Compression;

namespace PusulamRapor.Abide
{
    public partial class AbidePuanaGoreGenelSonucListe : DevExpress.XtraReports.UI.XtraReport
    {
        string TCKIMLIKNO;
        string OTURUM;
        string ID_ABIDESINAV;
        string ID_KADEME3;

        DataTable DTOGRENCISORU;
        DataTable DTOGRENCIDERSTOPLAM;
        DataTable DTDERS;
        public AbidePuanaGoreGenelSonucListe(string TCKIMLIKNO, string OTURUM, string ID_ABIDESINAV, string ID_KADEME3)
        {
            this.TCKIMLIKNO = TCKIMLIKNO;
            this.OTURUM = OTURUM;
            this.ID_ABIDESINAV = ID_ABIDESINAV;
            this.ID_KADEME3 = ID_KADEME3;
            InitializeComponent();
        }

        private void AbidePuanaGoreGenelSonucListe_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            using (Baglanti b = new Baglanti())
            {
                b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                b.ParametreEkle("@OTURUM", OTURUM);
                b.ParametreEkle("@ID_ABIDESINAV", ID_ABIDESINAV);
                b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                b.ParametreEkle("@ID_MENU", 1169);
                b.ParametreEkle("@ISLEM", 22);
                DataSet ds = b.SorguGetir("sp_Abide");

                DTOGRENCISORU = ds.Tables[0];
                DTOGRENCIDERSTOPLAM = ds.Tables[1];
                DTDERS = ds.Tables[2];

                float Y = 0;

                foreach (DataRow DERSROW in DTDERS.Rows)
                {
                    string DERS = DERSROW["DERS"].ToString();
                    int SORUSAYISI = Convert.ToInt32(DERSROW["SORUSAYISI"]);
                    int TOPLAMMAXPUAN = Convert.ToInt32(DERSROW["TOPLAMMAXPUAN"]);

                    DataTable table = new DataTable();
                    table.Columns.Add("KAMPÜS", typeof(string));
                    table.Columns.Add("SINIF", typeof(string));
                    table.Columns.Add("TC NO", typeof(string));
                    table.Columns.Add("ADI-SOYADI", typeof(string));
                    table.Columns.Add("KİTAPÇIK", typeof(string));

                    for (int i = 1; i <= SORUSAYISI; i++)
                    {
                        table.Columns.Add(i + ". SORU", typeof(string));
                    }

                    string TOPLAMMAXPUANNAME = "TOPLAM MAX" + Environment.NewLine + "PUAN: " + TOPLAMMAXPUAN;
                    table.Columns.Add(TOPLAMMAXPUANNAME, typeof(int));
                    table.Columns.Add("DÜZEYİ", typeof(string));

                    if (DTOGRENCISORU.Select("DERS = '" + DERS + "'").Length > 0)
                    {
                        DataTable DTDERSOGRENCI = DTOGRENCISORU.Select("DERS = '" + DERS + "'").CopyToDataTable();
                        DataTable DTDERSOGRENCISIRALI = PublicMetods.orderBYtoTable(DTDERSOGRENCI, "TCKIMLIKNO, SORUNO");
                        string TCKIMLIKNO = "";

                        DataRow newdr = table.NewRow();
                        foreach (DataRow DERSOGRENCI in DTDERSOGRENCI.Rows)
                        {
                            if (TCKIMLIKNO == "" || TCKIMLIKNO != DERSOGRENCI["TCKIMLIKNO"].ToString())
                            {
                                TCKIMLIKNO = DERSOGRENCI["TCKIMLIKNO"].ToString();
                                if (!newdr["TC NO"].ToString().Equals(""))
                                {
                                    table.Rows.Add(newdr);
                                }
                                newdr = table.NewRow();
                                newdr["KAMPÜS"] = DERSOGRENCI["KAMPUS"].ToString();
                                newdr["SINIF"] = DERSOGRENCI["SINIF"].ToString();
                                newdr["TC NO"] = DERSOGRENCI["TCKIMLIKNO"].ToString();
                                newdr["ADI-SOYADI"] = DERSOGRENCI["ADSOYAD"].ToString();
                                newdr["KİTAPÇIK"] = DERSOGRENCI["KITAPCIK"].ToString();

                                if (DTOGRENCIDERSTOPLAM.Select("DERS='" + DERS + "' AND TCKIMLIKNO='" + DERSOGRENCI["TCKIMLIKNO"] + "'").Length > 0)
                                {
                                    DataRow DROGRENCITOPLAM = DTOGRENCIDERSTOPLAM.Select("DERS='" + DERS + "' AND TCKIMLIKNO='" + DERSOGRENCI["TCKIMLIKNO"] + "'").CopyToDataTable().Rows[0];

                                    newdr[TOPLAMMAXPUANNAME] = DROGRENCITOPLAM["TOPLAMPUAN"];
                                    newdr["DÜZEYİ"] = DROGRENCITOPLAM["DUZEY"];
                                }
                            }
                            newdr[Convert.ToInt32(DERSOGRENCI["SORUNO"]) + ". SORU"] = DERSOGRENCI["PUAN"].ToString();
                        }
                        table.Rows.Add(newdr);


                        DataTable tableSIRALI = PublicMetods.orderBYtoTable(table, TOPLAMMAXPUANNAME + " DESC");

                        AbidePuanaGoreGenelSonucListeSub subreport = new AbidePuanaGoreGenelSonucListeSub(tableSIRALI, OTURUM, DERS);
                        XRSubreport report = new XRSubreport();
                        report.ReportSource = subreport;
                        report.LocationF = new PointF(0, Y);
                        report.CanGrow = true;
                        Detail.Controls.Add(report);

                        Y += (tableSIRALI.Rows.Count * 23);
                    }
                }
            }
        }

        private void AbidePuanaGoreGenelSonucListe_AfterPrint(object sender, EventArgs e)
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
