using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Rapor
{
    public class DOkulNetPuanOrtalamalari:DBase
    {
        GetIp getIp = new GetIp();

        public string OkulNetPuanOrtalamalari(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_OkulNetPuanOrtalamalari.OkulNetPuanOrtalamalari);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_OkulNetPuanOrtalamalari",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
    }
}