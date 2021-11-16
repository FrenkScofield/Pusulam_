using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness
{
    public class DAnasayfa : DBase
    {
        GetIp getIp = new GetIp();
        public string AnasayfaDuyuruListeGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_AnaSayfa.AnasayfaDuyuruListeGetir);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_AnaSayfa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string AnasayfaFaturaListeGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_AnaSayfa.AnasayfaFaturaListeGetir);
            j.Add("ID_MENU", ID_MENU);
            //j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_AnaSayfa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }
    }
}