using Dapper;
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
    public class MTakvim: DBase
    {
        public JArray TakvimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Takvim.TakvimGetir);
                j.Add("ID_MENU", (int)EMobileMenu.Takvim);
                j.Add("ID_KADEME3", 0);
                j.Add("ID_KULLANICITIPI", 0);
                j.Add("ID_TAKVIMS", "[]");

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    string json = db.ExecuteScalar<string>("sp_Takvim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JArray.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject OdevDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevDetayGetir);
                j.Add("ID_MENU", (int)EMobileMenu.Takvim);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                   string json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                   return JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JArray EtutDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.EtutDetayGetir);
                j.Add("ID_MENU", (int)EMobileMenu.Takvim);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JArray.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}
