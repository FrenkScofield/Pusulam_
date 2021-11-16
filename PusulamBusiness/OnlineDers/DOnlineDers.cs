using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.OnlineDers
{
    public class DOnlineDers : DBase
    {
        GetIp getIp = new GetIp();
        public string GenelKlasorListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.GenelKlasorListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public bool GenelKlasorKaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.GenelKlasorKaydet);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public string TarihListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.TarihListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string OnlineDersProgramiListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public bool OnlineDersProgramiKaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiKaydet);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public bool OnlineDersSil(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersSil);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public bool OnlineDersEkranPaylasimIdGuncelle(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersEkranPaylasimIdGuncelle);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public int OnlineDersEkle(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersEkle);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.ExecuteScalar<int>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public string OnlineDersProgramiListelebyOgrenci(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiListelebyOgrenci);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string OnlineDersProgramiListeleSinif(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiListeleSinif);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string OnlineDersProgramiOgretmenKlasorGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiOgretmenKlasorGetir);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public bool OnlineDersProgramiOgretmenKlasorKaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersProgramiOgretmenKlasorKaydet);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public bool OnlineDersHerkesiSustur(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersHerkesiSustur);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public bool OnlineDersHerkesiSusturGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OnlineDersHerkesiSusturGetir);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            bool result = false;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.ExecuteScalar<bool>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result;

        }




        public string OgretmenOnlineDersProgramiListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OgretmenOnlineDersProgramiListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public bool OgretmenOnlineDersProgramiKaldir(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.OgretmenOnlineDersProgramiKaldir);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            int result = 0;

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                result = db.Execute("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return result > 0;

        }

        public string SinifOnlineDersOgretmenListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OnlineDers.SinifOnlineDersOgretmenListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed) db.Open();
                json = db.ExecuteScalar<string>("sp_OnlineDers", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }       
    }
}