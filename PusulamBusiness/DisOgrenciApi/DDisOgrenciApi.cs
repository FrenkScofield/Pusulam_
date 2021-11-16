using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using PusulamBusiness.Ortak;
using System.Web.Script.Serialization;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.DisOgrenciApi
{
    public class DDisOgrenciApi : DBase
    {
        GetIp getIp = new GetIp();
        public string OnlineSinavKurumDisiOgrenciAtama(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EDisOgrenciApi.OnlineSinavKurumDisiOgrenciAtama);
                j.Add("IP", getIp.GetUser_IP());

                string result = "";
                decimal data = Decimal.Parse(j["TCKIMLIKNO"].ToString());
                data++;
                j["TCKIMLIKNO"] = data.ToString();
                var json = new JavaScriptSerializer().Serialize(j);
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<string>("sp_ApiDisOgrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }



        }
    }
}