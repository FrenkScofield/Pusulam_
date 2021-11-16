using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Rehberlik.RehberlikOnline
{
    public class DRehberlikOnlineYukle : DBase
    {
        GetIp getIp = new GetIp();
        public string RehberlikOnlineExcelYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikOnlineYukle.RehberlikOnlineExcelYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikOnlineYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string RehberlikOnlineListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikOnlineYukle.RehberlikOnlineListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikOnlineYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}