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

namespace PusulamBusiness.Rapor.Analiz
{
    public class DSinavAnalizi : DBase
    {
        GetIp getIp = new GetIp();
        public string SinifMaddeAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_SinifMaddeAnalizi.SinifMaddeAnalizi);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_SinifMaddeAnalizi", j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string SinifDersAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinifDersAnalizi.SinifDersAnaliziYeni);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinifDersAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string MaddeFrekansAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_MaddeFrekansAnalizi.MaddeFrekansAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_MaddeFrekansAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciMaddeAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciMaddeAnalizi.OgrenciMaddeAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciMaddeAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string TestMaddeAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_TestMaddeAnalizi.TestMaddeAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_TestMaddeAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavKonuAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavKonuAnalizi.SinavKonuAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinavKonuAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string GenelSoruAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_GenelSoruAnalizi.GenelSoruAnalizi);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_GenelSoruAnalizi", j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
    }
}