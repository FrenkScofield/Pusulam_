using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PusulamBusiness.Mobile
{
    public class MBulten : DBase
    {
        public JArray BultenListesiGetir(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());

                j.Add("ISLEM", (int)sp_Bulten.BultenListesiGetir);
                j.Add("ID_MENU", (int)EMobileMenu.Bultenler);
                j.Add("TC_OGRENCI", param.TCKIMLIKNO);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_Bulten", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    json = json != null ? json.Replace("\"selected\":1", "\"selected\":true") : "[]";

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
