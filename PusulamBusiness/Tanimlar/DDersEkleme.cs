using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Enums;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Utility;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Tanimlar
{
    public class DDersEkleme : DBase
    {
        GetIp getIp = new GetIp();
        public string KademeKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SubeYetki.KademeYetkiKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SubeYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
      
        public string DersListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersEkleme.DersListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersEkleme", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            
            return json;
        }

    }
}
