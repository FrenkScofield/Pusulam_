using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;
using static PusulamBusiness.Enums.MobileStoredProcedureIslem;

namespace PusulamBusiness.Mobile
{
    public class MAuth : DBase
    {
        public JObject Login(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Login.Login);
                j.Add("@ID_UYGULAMA", 5);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    Ogrenci str = db.Query<Ogrenci>("sp_Login", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    string json = new JavaScriptSerializer().Serialize(str);
                    return JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject Logout(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Logout.Logout);
                j.Add("@ID_UYGULAMA", 5);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                        db.ExecuteScalar<string>("sp_Logout", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return JObject.Parse("{logout:1}");
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject SifreDegistirYetkiKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", "3");
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JObject.Parse(json);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject OgrenciVeliSifreSifirla(JObject j)
        {
            try
            {
                j.Add("ISLEM", "2");

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JObject OgrenciVeliSifreSifirlaKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", "1");

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Ogrenci
    {
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string TCKIMLIKNO { get; set; }
        public bool CINSIYET { get; set; }
        public DateTime DOGUMTARIHI { get; set; }
        public string OTURUM { get; set; }
    }
}
