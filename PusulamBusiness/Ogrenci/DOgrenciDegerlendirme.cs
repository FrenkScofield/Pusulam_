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

namespace PusulamBusiness.Ogrenci
{
    public class DOgrenciDegerlendirme : DBase
    {
        GetIp getIp = new GetIp();
        public string OD_DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_DonemListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OD_AyListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_AyListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OD_DegerlendirmeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_DegerlendirmeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OD_OgrenciDonemDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_OgrenciDonemDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OD_DonemlikDegerlendirmeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_DonemlikDegerlendirmeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OD_DonemlikDegerlendirmeDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DegerlendirmeFormu.OD_DonemlikDegerlendirmeDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_DegerlendirmeFormu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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