using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;
using DevExpress.Utils;

namespace PusulamRapor.Sinav
{
    public partial class OgrenciIzlemePivot : DevExpress.XtraReports.UI.XtraReport
    {
        public string TCKIMLIKNO { get; set; }
        public string OTURUM { get; set; }
        public string DONEM { get; set; }
        public string DONEMSINAV { get; set; }
        public string ID_KADEME3 { get; set; }
        public string ID_SINAVTURU { get; set; }
        public string ID_SUBEs { get; set; }
        public string ID_SINIFs { get; set; }
        public string ID_DERSs { get; set; }
        DataSet ds = new DataSet();
        DataTable t1 = new DataTable();
        DataTable t2 = new DataTable();
        DataTable t3 = new DataTable();
        DataTable t4 = new DataTable();
        DataTable t5 = new DataTable();

        public OgrenciIzlemePivot(string tc, string oturum, string donem, string donemSinav, string idkademe3, string idsinavturu, string idsubelist, string idsiniflist, string idderslist)
        {
            TCKIMLIKNO = tc;
            OTURUM = oturum;
            DONEM = donem;
            DONEMSINAV = donemSinav;
            ID_KADEME3 = idkademe3;
            ID_SINAVTURU = idsinavturu;
            ID_SUBEs = idsubelist;
            ID_SINIFs = idsiniflist;
            ID_DERSs = idderslist;


            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);

                    b.ParametreEkle("@DONEM", DONEM);
                    b.ParametreEkle("@DONEMSINAV", DONEMSINAV);
                    b.ParametreEkle("@ID_KADEME3", ID_KADEME3);
                    b.ParametreEkle("@ID_SINAVTURU", ID_SINAVTURU);
                    b.ParametreEkle("@ID_SUBEs", ID_SUBEs);
                    b.ParametreEkle("@ID_SINIFs", ID_SINIFs);
                    b.ParametreEkle("@ID_DERSs", ID_DERSs);
                    b.ParametreEkle("@ISLEM", 2); // Rapor
                    b.ParametreEkle("@ID_MENU", 1063);

                    ds = b.SorguGetir("sp_OgrenciIzleme");


                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.DataSource = ds.Tables[0];
                        t1 = ds.Tables[0];
                        t2 = ds.Tables[1];
                        t3 = ds.Tables[2];
                        t4 = ds.Tables[3];
                        t5 = ds.Tables[4];

                        foreach (DataColumn col in t1.Columns)
                        {
                            foreach (DataRow row in t1.Rows)
                            {
                                row[col.ColumnName] = row[col.ColumnName].ToString().Replace(".", ",");
                            }
                        }

                        foreach (DataColumn col in t3.Columns)
                        {
                            foreach (DataRow row in t3.Rows)
                            {
                                row[col.ColumnName] = row[col.ColumnName].ToString().Replace(".", ",");
                            }
                        }

                        DataTable Table = new DataTable();

                        Table.Columns.Add("BASLIK", typeof(string)); // ad soyad
                        Table.Columns.Add("SIRA", typeof(string)); // ad soyad

                        Table.Columns.Add("SUBEAD", typeof(string)); // ad soyad
                        Table.Columns.Add("SINIFAD", typeof(string)); // ad soyad
                        Table.Columns.Add("TCKIMLIKNO", typeof(string)); // ad soyad
                        Table.Columns.Add("ADSOYAD", typeof(string)); // ad soyad


                        Table.Columns.Add("DERSAD", typeof(string)); // day2

                        Table.Columns.Add("SINAVAD", typeof(string)); // day

                        Table.Columns.Add("NET", typeof(string)); // Employee



                      


                        foreach (DataRow dr in t1.Rows)
                        {
                            foreach (DataRow sinav in t2.Rows)
                            {
                                string yaz = dr["SIRA"].ToString() == "1" ? "TOPLAM NET" : "LISTE";
                                if (yaz != "TOPLAM NET")
                                {
                                    Table.Rows.Add(new object[] {
                                 "DERSLER"
                                ,dr["SIRA"].ToString()
                                ,dr["SUBEAD"].ToString()
                                ,dr["SINIFAD"].ToString()
                                ,dr["TCKIMLIKNO"].ToString()
                                ,dr["ADSOYAD"].ToString()
                                ,dr["TAKMAAD"].ToString()
                                ,sinav["SINAVAD"].ToString()
                                ,dr[sinav["ID_SINAV"].ToString()].ToString() });
                                }
                            }
                        }


                        foreach (DataRow dr in t1.Rows)
                        {
                            foreach (DataRow sinav in t2.Rows)
                            {
                                string yaz = dr["SIRA"].ToString() == "1" ? "TOPLAM NET" : "LISTE";
                                if (yaz == "TOPLAM NET")
                                {
                                    Table.Rows.Add(new object[] {
                                 "NET TOPLAMLARI"
                                ,dr["SIRA"].ToString()
                                ,dr["SUBEAD"].ToString()
                                ,dr["SINIFAD"].ToString()
                                ,dr["TCKIMLIKNO"].ToString()
                                ,dr["ADSOYAD"].ToString()
                                ,dr["TAKMAAD"].ToString()
                                ,sinav["SINAVAD"].ToString()
                                ,dr[sinav["ID_SINAV"].ToString()].ToString() });
                                }
                            }
                        }

                        foreach (DataRow dr in t3.Rows)
                        {
                            foreach (DataRow sinav in t4.Rows)
                            {
                                Table.Rows.Add(new object[] {
                                 "PUAN TÜRLERİ"
                                ,"PUAN"            //dr["SIRA"].ToString()
                                ,dr["SUBEAD"].ToString()
                                ,dr["SINIFAD"].ToString()
                                ,dr["TCKIMLIKNO"].ToString()
                                ,dr["ADSOYAD"].ToString()
                                ,dr["PUANTURU"].ToString()
                                ,sinav["SINAVAD"].ToString()
                                ,dr[sinav["ID_SINAV"].ToString()].ToString() });
                            }
                        }



                        pivotGrid.DataSource = Table;

                        //PivotGridFieldBase f = new PivotGridFieldBase("SIRA",PivotArea.ColumnArea);
                        //f.SortByAttribute="SIRA";

                        //PivotGridFieldBase fDers = new PivotGridFieldBase("DERSAD",PivotArea.ColumnArea);
                        pivotGrid.Fields.Add("BASLIK", PivotArea.ColumnArea);

                        //pivotGrid.Fields.Add("SIRA",PivotArea.ColumnArea);
                        pivotGrid.Fields.Add("DERSAD", PivotArea.ColumnArea);
                        pivotGrid.Fields.Add("SINAVAD", PivotArea.ColumnArea);

                        pivotGrid.Fields.Add("SUBEAD", PivotArea.RowArea);
                        pivotGrid.Fields.Add("SINIFAD", PivotArea.RowArea);
                        pivotGrid.Fields.Add("TCKIMLIKNO", PivotArea.RowArea);
                        pivotGrid.Fields.Add("ADSOYAD", PivotArea.RowArea);

                        pivotGrid.Fields.Add("NET", PivotArea.DataArea);
                        pivotGrid.Fields["NET"].SummaryType = PivotSummaryType.Max;
                        pivotGrid.Fields["NET"].ValueFormat.FormatType = FormatType.Custom;
                        pivotGrid.Fields["NET"].ValueFormat.FormatString = "#,##";

                        pivotGrid.Fields["ADSOYAD"].Width = 125;
                        pivotGrid.Fields["NET"].Width = 80;

                        //fDers.SortBySummaryInfo.Field=f;
                    }



                }



            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
