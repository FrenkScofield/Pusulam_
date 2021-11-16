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

namespace PusulamBusiness.RehberlikEnvanter
{
    public class DRehberlikEnvanter : DBase
    {
        GetIp getIp = new GetIp();
        public bool RehberlikEnvanterEkle(JObject j)
        {
            bool result = false;
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanterTanimlama.RehberlikEnvanterEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<bool>("sp_RehberlikEnvanterTanimlama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                return result;
            }
        }

        public string RehberlikEnvanterleriGetirSinif(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanter.RehberlikEnvanterleriGetirSinif);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikEnvanter", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string RehberlikEnvanterleriGetirSubeKademe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanter.RehberlikEnvanterleriGetirSubeKademe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikEnvanter", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string RehberlikEnvanterleriGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanter.RehberlikEnvanterleriGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikEnvanter", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string RehberlikEnvanterListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanterTanimlama.RehberlikEnvanterListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikEnvanterTanimlama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string RehberlikEnvanterTestListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanter.RehberlikEnvanterleriGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberlikEnvanter", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool RehberlikEnvanterAktifPasif(JObject j)
        {
            bool result = false;
            try
            {
                j.Add("ISLEM", (int)sp_RehberlikEnvanterTanimlama.RehberlikEnvanterAktifPasif);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<bool>("sp_RehberlikEnvanterTanimlama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                return result;
            }
        }
    }
}