using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.OgretmenPerformans;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace PusulamBusiness.Performans
{
    public class DPerformans:DBase
    {
        GetIp getIp = new GetIp();
        public string PeriyotTanimla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PeriyotTanımla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json ;
                
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
       


        public string PeriyotListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PeriyotListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool PeriyotSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PeriyotSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.Execute("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public List<MDonem> DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.DonemListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDonem> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDonem>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

       
        public List<MOgretmen> OgretmenListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.OgretmenListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgretmen> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MOgretmen>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
       
        public List<MPeriyotTarih> PeriyotTarihListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PeriyotTarihListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MPeriyotTarih> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MPeriyotTarih>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MPeriyotTarih> TumPeriyotTarihListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.TumPeriyotTarihListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MPeriyotTarih> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MPeriyotTarih>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MOgretmenKategori> KategoriListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.KategoriListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgretmenKategori> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MOgretmenKategori>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
       
        public string PerformansSoru(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PerformansSoru);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string KategoriSoru(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.KategoriSoru);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string DegerlendirilenlerListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.DegerlendirilenlerListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string DegerlendirilmeyenListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.DegerlendirilmeyenlerListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string DegerlendirmeKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.DegerlendirmeKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string PeriyotDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Performans.PeriyotDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Performans", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }



    }
}