using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness
{
    public class DFiltre:DBase
    {
        public string SubeListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.SubeListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string KademeListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.KademeListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string Kademe3Listele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.Kademe3Listele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string SinifAlanListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.SinifAlanListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string SinifListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.SinifListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string KullaniciTipiListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.KullaniciTipiListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string DonemListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.DonemListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string OgrenciListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.OgrenciListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

        public string KullaniciListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.KullaniciListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }
        public string EgitimTuruListesi(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.EgitimTuruListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true") : "";
        }
    }
}