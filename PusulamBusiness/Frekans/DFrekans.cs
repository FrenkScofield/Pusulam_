using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using PusulamBusiness.Ortak;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Frekans
{
    public class DFrekans : DBase
    {
        GetIp getIp = new GetIp();
        public string FrekansOgretmenSirali(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.FrekansOgretmenSirali);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string FrekansGetirOgretmenSinif(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.FrekansGetirOgretmenSinif);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinavTuru> SinavTuruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_FrekansSistem.SinavTuruListele);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    liste=db.Query<MSinavTuru>("sp_FrekansSistem",j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }


        public string FrekansDersListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.FrekansDersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string FrekansGetirSinifOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.FrekansGetirSinifOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciFrekansDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.OgrenciFrekansDetayGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinifFrekansGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistem.SinifFrekansGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistemEksikKazanim.OgrenciFrekansEksikKazanimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistemEksikKazanim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgretmenFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistemEksikKazanim.OgretmenFrekansEksikKazanimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistemEksikKazanim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinifFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSistemEksikKazanim.SinifFrekansEksikKazanimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSistemEksikKazanim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string FrekansSinavOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_FrekansSinavOgrenci.FrekansSinavOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_FrekansSinavOgrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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