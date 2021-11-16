using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Pusulam
{
    public class SinavExcelYukle : IHttpHandler
    {
        string OTURUM = "";
        string TCKIMLIKNO = "";
        public int type = 0;
        public string donem = "";
        public DataTable dtOkunanDosya = new DataTable();
        HttpContext context;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            string DosyaTip = context.Request.Files[0].ContentType;
            string DosyaAd = Guid.NewGuid().ToString();
            string yol = "~/Dosyalar/";

            type = Convert.ToInt32(context.Request.Params.Get(0));
            donem = context.Request.Params.Get(1);
            TCKIMLIKNO = context.Request.Params.Get(2);
            OTURUM = context.Request.Params.Get(3);
            string extension = System.IO.Path.GetExtension(context.Request.Files[0].FileName);

            if ((extension == ".xls" || extension == ".xlsx"))
            {
                #region Dosya 
                if (context.Request.Files.Count > 0)
                {
                    HttpPostedFile file = null;

                    for (int i = 0; i < context.Request.Files.Count; i++)
                    {
                        file = context.Request.Files[i];
                        if (file.ContentLength > 0)
                        {
                            var path = Path.Combine(Path.Combine(context.Server.MapPath(yol), DosyaAd + extension));
                            file.SaveAs(path);
                        }
                    }

                }
                #endregion

                string filepath = context.Server.MapPath(yol) + DosyaAd + extension;
                OleDbConnection baglanti;
                try
                {
                    baglanti = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0;IMEX=1'", filepath));
                    baglanti.Open();
                }
                catch (Exception)
                {
                    baglanti = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0;'", filepath));
                    baglanti.Open();
                }

                if (type == (int)EVarlik.TabanPuanExcel)
                {
                    UniversiteTabanPuanlariSonucYukle(baglanti);
                    //YerlestirmeSonucYerlesemeyenYukle(baglanti);
                }
            }
            else
            {
                context.Response.Write("Yalnızca xls ve xlsx dosyaları yükleyebilirsiniz.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void UniversiteTabanPuanlariSonucYukle(OleDbConnection baglanti)
        {
            bool success = true;
            try
            {
                string sorgu = "select * from [üniversite taban puanları$A:V] ";
                OleDbDataAdapter data_adaptor = new OleDbDataAdapter(sorgu, baglanti);
                baglanti.Close();

                DataTable dt = new DataTable();

                data_adaptor.Fill(dt);

                dt.Columns[0].ColumnName = "PROGRAMKODU";
                dt.Columns[1].ColumnName = "UNIVERSITE";
                dt.Columns[2].ColumnName = "FAKULTE";
                dt.Columns[3].ColumnName = "BOLUM";
                dt.Columns[4].ColumnName = "BOLUM2";
                dt.Columns[5].ColumnName = "EGITIMDILI";
                dt.Columns[6].ColumnName = "UCRETBURS";
                dt.Columns[7].ColumnName = "IL";
                dt.Columns[8].ColumnName = "UNIVERSITETURU";
                dt.Columns[9].ColumnName = "OGRENIMSURESI";
                dt.Columns[10].ColumnName = "OGRENIMTURU";
                dt.Columns[11].ColumnName = "PUANTURU";
                dt.Columns[12].ColumnName = "YIL";
                dt.Columns[13].ColumnName = "TABANPUAN";
                dt.Columns[14].ColumnName = "KONTENJAN";
                dt.Columns[15].ColumnName = "PROGRAMTURU";
                dt.Columns[16].ColumnName = "SONYGSYERLESTIRME";
                dt.Columns[17].ColumnName = "OKULBIRINCILIGIKONTENJANI";
                dt.Columns[18].ColumnName = "YERONCELIKLERI";
                dt.Columns[19].ColumnName = "ENBUYUKPUAN";
                dt.Columns[20].ColumnName = "BASARISIRALAMA";
                dt.Columns[21].ColumnName = "OZELKOSULACIKLAMA";

                //dt.Rows.RemoveAt(0);
                //dt.Rows.RemoveAt(0);
                DataTable dt1 = RemoveEmptyRows(dt);
                using (Baglanti b = new Baglanti())
                {
                    b.ParametreEkle("@DATATABLE", dt1);
                    b.ParametreEkle("@DONEM", donem);
                    b.ParametreEkle("@TCKIMLIKNO", TCKIMLIKNO);
                    b.ParametreEkle("@OTURUM", OTURUM);
                    b.ParametreEkle("@ID_MENU", (int)EMenu.TabanPuanlar);
                    b.ParametreEkle("@ISLEM", (int)sp_OSYM.TabanPuanKaydet);

                    b.Ac();
                    b.SorguGotur("sp_OSYM", CommandType.StoredProcedure);
                    b.Kapat();
                }
            }
            catch (Exception ex)
            {
                success = false;
            }

            if (!success)
            {
                context.Response.Write("{\"success\": false}");
            }
            else
            {
                context.Response.Write("{\"success\": true}");
            }
        }

        private static DataTable RemoveEmptyRows(DataTable source)
        {
            DataTable dt1 = source.Clone();
            dt1.Columns[0].DataType = typeof(string);
            for (int i = 0; i <= source.Rows.Count - 1; i++) 
            {
                DataRow currentRow = source.Rows[i];

                string item = currentRow[0].ToString();
                if (!string.IsNullOrEmpty(item)) 
                {
                    dt1.ImportRow(currentRow);               
                }
            }
            return dt1;
        }
    }
}