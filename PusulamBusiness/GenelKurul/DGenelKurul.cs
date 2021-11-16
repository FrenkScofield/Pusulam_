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

namespace PusulamBusiness.GenelKurul
{
    public class DGenelKurul : DBase

    {
        GetIp getIp = new GetIp();
        public string DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.DonemListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string PeriyotEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.PeriyotEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

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
                j.Add("ISLEM", (int)sp_GenelKurul.PeriyotListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }


        }
        public string PeriyotSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.PeriyotSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }


        }
        public string KararEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.KararEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string MudurSubeListele(JObject j)

        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.SubeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string KademeYetkiListele(JObject j)

        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.KademeYetkiListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string KararListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.KararListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public string KararListeleGenel(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.KararListeleGenel);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string KararSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.KararSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string SubeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.SubeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public string EmailListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.EmailListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string EmailSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.EmailSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string EmailEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.EmailEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        //public static string MevcutPeriyotMailGonder(int ID_KULLANICI, string OTURUM)
        //{
        //    string sonuc = "";
        //    using (Baglanti b = new Baglanti())
        //    {
        //        b.ParametreEkle("@ID_KULLANICI", ID_KULLANICI);
        //        b.ParametreEkle("@OTURUM", OTURUM);
        //        b.ParametreEkle("@ISLEM", 11);

        //        b.Ac();
        //        SqlDataReader dr = b.VeriOkuyucuGetir("spGenelKurul", System.Data.CommandType.StoredProcedure);
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                sonuc = dr.GetString(0);
        //            }
        //        }
        //        b.Kapat();
        //    }
        //    return sonuc;
        //}

        //public static string EmailListele(int ID_KULLANICI, string OTURUM)
        //{
        //    string sonuc = "";
        //    using (Baglanti b = new Baglanti())
        //    {
        //        b.ParametreEkle("@ID_KULLANICI", ID_KULLANICI);
        //        b.ParametreEkle("@OTURUM", OTURUM);
        //        b.ParametreEkle("@ISLEM", 13);

        //        b.Ac();
        //        SqlDataReader dr = b.VeriOkuyucuGetir("spGenelKurul", System.Data.CommandType.StoredProcedure);
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                sonuc = dr.GetString(0);
        //            }
        //        }
        //        b.Kapat();
        //    }
        //    return sonuc;
        //}

        //public static string EmailEkle(int ID_KULLANICI, string OTURUM, string EMAIL)
        //{
        //    string sonuc = "";
        //    using (Baglanti b = new Baglanti())
        //    {
        //        b.ParametreEkle("@ID_KULLANICI", ID_KULLANICI);
        //        b.ParametreEkle("@OTURUM", OTURUM);
        //        b.ParametreEkle("@EMAIL", EMAIL);
        //        b.ParametreEkle("@ISLEM", 14);

        //        b.Ac();
        //        SqlDataReader dr = b.VeriOkuyucuGetir("spGenelKurul", System.Data.CommandType.StoredProcedure);
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                sonuc = dr.GetString(0);
        //            }
        //        }
        //        b.Kapat();
        //    }
        //    return sonuc;
        //}

        //public static string EmailSil(int ID_KULLANICI, string OTURUM, int ID_GENELKURULEMAIL)
        //{
        //    string sonuc = "";
        //    using (Baglanti b = new Baglanti())
        //    {
        //        b.ParametreEkle("@ID_KULLANICI", ID_KULLANICI);
        //        b.ParametreEkle("@OTURUM", OTURUM);
        //        b.ParametreEkle("@ID_GENELKURULEMAIL", ID_GENELKURULEMAIL);
        //        b.ParametreEkle("@ISLEM", 15);

        //        b.Ac();
        //        SqlDataReader dr = b.VeriOkuyucuGetir("spGenelKurul", System.Data.CommandType.StoredProcedure);
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                sonuc = dr.GetString(0);
        //            }
        //        }
        //        b.Kapat();
        //    }
        //    return sonuc;
        //}

        public string PeriyotMailGonder(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_GenelKurul.PeriyotMailGonder);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelKurul", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        //asdasasadasda
        public string BilgiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelListeler.BilgiListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelListeler", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string BilisselSurecListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelListeler.BilisselSurecListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelListeler", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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