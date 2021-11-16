using Newtonsoft.Json.Linq;
using PusulamBusiness.Models.Ortak;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System.Collections.Generic;
using PusulamBusiness.Enums;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ortak
{
    public class DKullanici : DBase
    {
        GetIp getIp = new GetIp();
        public object KullaniciTipiGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Kullanici.KullaniciTipiGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                List<MKullanici> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKullanici>("sp_Kullanici", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SifremiDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", 1);
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_SifremiDegistir", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw;
            }
        }
    }
}