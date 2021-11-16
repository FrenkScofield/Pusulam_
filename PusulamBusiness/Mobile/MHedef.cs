using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Mobile
{
    public class MHedef : DBase
    {
        public bool OOBPKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.OOBPKaydet);
                j.Add("ID_MENU", (int)EMobileMenu.HedefBelirle);
                int sonuc = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject UniversiteTabanPuanListeleOgrenci(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());
                int start = param.StartCount;
                int row = param.Row;
                string tc = param.TCKIMLIKNO;
                string oturum = param.OTURUM;
                string serach = param.SearchKey;

                JObject p = new JObject();

                p.Add("ISLEM", (int)sp_OSYM.TabanPuanListeleOgrenci);
                p.Add("ID_MENU", (int)EMobileMenu.HedefBelirle);
                p.Add("TCKIMLIKNO", tc);
                p.Add("OTURUM", oturum);
                p.Add("SQLJSON", "{\"draw\":1,\"columns\":[{\"data\":\"RowNumber\",\"name\":\"\",\"searchable\":false,\"orderable\":false,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"IL\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"UNIVERSITE\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"FAKULTE\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"BOLUM2\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"UNIVERSITETURU\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"UCRETBURS\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"OGRENIMSURESI\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"PUANTURU\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"TABANPUAN\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"KONTENJAN\",\"name\":\"\",\"searchable\":true,\"orderable\":true,\"search\":{\"value\":\"\",\"regex\":false}},{\"data\":\"PROGRAMKODU\",\"name\":\"\",\"searchable\":false,\"orderable\":false,\"search\":{\"value\":\"\",\"regex\":false}}],\"order\":[{\"column\":1,\"dir\":\"asc\"}],\"start\":" + start.ToString() + ",\"length\":10,\"search\":{\"value\":\"" + serach + "\",\"regex\":false}}");

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    string json = db.ExecuteScalar<string>("sp_OSYM", p.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    json = json.Substring(1, json.Length - 2).Replace("\"data\":null", "\"data\":\"[]\"");

                    return JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JArray HedefListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefListele);
                j.Add("ID_MENU", (int)EMobileMenu.HedefBelirle);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    string json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JArray.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool HedefEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefEkle);
                j.Add("ID_MENU", (int)EMobileMenu.HedefBelirle);
                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool HedefSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefSil);
                j.Add("ID_MENU", (int)EMobileMenu.HedefBelirle);
                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

    }
}
