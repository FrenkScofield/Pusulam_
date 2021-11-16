using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.DisKaynak;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;

namespace PusulamBusiness.DisKaynak
{
    public class DDisKaynak : DBase
    {
        GetIp getIp = new GetIp();
        public JObject ApiKeyAl(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.ApiKeyAl);

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return JObject.Parse(json);
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object OgrenciListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.OgrenciListe);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MOgrenci>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object OgretmenListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.OgretmenListe);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MOgretmen>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object SinifListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.SinifListe);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MSinif>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object SubeListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.SubeListe);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MKampus>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject SifreCoz(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.SifreCoz);

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return JObject.Parse(json);
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object OgrenciGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.OgrenciGetir);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MOgrenci>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object TumOgretmenListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.TumOgretmenGetir);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    List<MOgretmen> list =  db.Query<MOgretmen>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object SinifOgretmenDersListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.SinifOgretmenDersListele);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string dt = db.Query<string>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    object a = js.Deserialize(dt, typeof(object));

                    return a;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object SinifGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.SinifGetir);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    return db.Query<MSinif>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public Object OgretmenGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisKaynak.OgretmenGetir);

                string json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Diskaynak", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return JObject.Parse(json);
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}
