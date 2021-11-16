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

namespace PusulamBusiness.Rapor.Yazili
{
    public class DKazanimAnalizi : DBase
    {
        GetIp getIp = new GetIp();
        public String KazanimAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KazanimAnalizi.KazanimAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KazanimAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}