using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Tkt
{
    public class DTKTAktarim : DBase
    {
        /// <summary>
        /// geçen dönem ana okulu tkt son sınav bıu sene ilk okul tkt ilk sınava aktarılıyor.
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public String TktAktar(JObject j)
        {
            try
            {
                GetIp getIp = new GetIp();
                j.Add("ID_MENU", ID_MENU);

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_TKTAktarim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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
