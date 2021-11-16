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
    public class MDersProgram : DBase
    {
        public JArray OnlineDersProgramiListelebyOgrenci(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiListelebyOgrenci);
            j.Add("ID_MENU", (int)EMobileMenu.OnlineDersProgrami);

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) 
                    db.Open();

                string json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                json = String.IsNullOrEmpty(json) ? String.Empty : json.Replace("\"selected\":1", "\"selected\":true");

                return JArray.Parse(json);
            }
        }

        public JArray DersProgramiGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinifDersProgram.DersProgramiGetir);
                j.Add("ID_MENU", (int)EMobileMenu.DersProgrami);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    string json = db.ExecuteScalar<string>("sp_SinifDersProgram", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    json = String.IsNullOrEmpty(json) ? String.Empty : json.Replace("\"selected\":1", "\"selected\":true");

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