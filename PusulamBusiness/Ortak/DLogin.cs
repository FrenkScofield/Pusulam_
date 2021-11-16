using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System;
using RestSharp;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ortak
{
    public class DLogin : DBase
    {
        GetIp getIp = new GetIp();
        public Object Login(JObject j)
        {
            try
            {
                //j.Add("IP", getIp.GetUser_IP());

                Object m;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    m = db.Query("sp_Login", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool TechPassKullaniciMi(JObject j)
        {
            var client = new RestClient("https://techpass.techlife.com.tr/TechPassApi/TechPassAuthentication/TechPassKullaniciMi");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"TCKIMLIKNO\":\"" + j.SelectToken("TCKIMLIKNO") + "\",\r\n    \"U_ANAHTAR\":\"FE3CBB6213C67B67B2804A0E655B0DB969DA3729411483B5EA2E826B343E76CA1D691B5F18C25F34010DFDA7DD39094E0ADC37088199E094D2BDF387975FAF6D\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string sonuc = response.Content;


            //JObject json = new JObject();
            //json.Add("U_ANAHTAR", "FE3CBB6213C67B67B2804A0E655B0DB969DA3729411483B5EA2E826B343E76CA1D691B5F18C25F34010DFDA7DD39094E0ADC37088199E094D2BDF387975FAF6D");
            //json.Add("TCKIMLIKNO", j.SelectToken("TCKIMLIKNO"));
            //sonuc = client.TechPassKullaniciMi(json.ToString());
            return Convert.ToBoolean(sonuc);
        }
        public Object TechPassLogin(JObject j)
        {
            var client = new RestClient("https://techpass.techlife.com.tr/TechPassApi/TechPassAuthentication/GirisBilgiGetir");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"U_ANAHTAROZEL\":\"DFE7A667C0BAE6E60225741A0B4533BBBE1D08C914953B2F176CB600FD8C020169DA97A4F900FAE9DDFDB2976F19C91222BA033E49EF7303F7FE0021008DCFB4\",\r\n    \"U_ANAHTAR\":\"FE3CBB6213C67B67B2804A0E655B0DB969DA3729411483B5EA2E826B343E76CA1D691B5F18C25F34010DFDA7DD39094E0ADC37088199E094D2BDF387975FAF6D\",\r\n    \"OTURUM\":\""+ j.SelectToken("OTURUM") + "\"\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var k = new JObject();
            k = JObject.Parse(response.Content);
            //TPSERVICE.TPServiceClient client = new TPSERVICE.TPServiceClient();

            //j.Add("U_ANAHTAR", "FE3CBB6213C67B67B2804A0E655B0DB969DA3729411483B5EA2E826B343E76CA1D691B5F18C25F34010DFDA7DD39094E0ADC37088199E094D2BDF387975FAF6D");
            //j.Add("U_ANAHTAROZEL", "DFE7A667C0BAE6E60225741A0B4533BBBE1D08C914953B2F176CB600FD8C020169DA97A4F900FAE9DDFDB2976F19C91222BA033E49EF7303F7FE0021008DCFB4");
            //mKullanici K = client.GirisBilgiGetir(j.ToString());

            string json = "[]";
            var K_ANAHTAR = k["K_ANAHTAR"].ToString().Replace("{", "").Replace("}", "");
            Object m;
           
                JObject j2 = new JObject();
                j2.Add("K_ANAHTAR", K_ANAHTAR);
                j2.Add("ISLEM", 2);
                //j2.Add("IP", getIp.GetUser_IP());

            using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    //json = db.Query("sp_Login", j2.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                   

                    m = db.Query("sp_Login", j2.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return m;           
           
        }
        public Object GirisYap(JObject j)
        {
            try
            {
                Object m;
                j.Add("ISLEM", "1");
                //j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    m = db.Query("sp_Login", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SifreDegistirYetkiKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", "3");
                //j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OgrenciVeliSifreSifirla(JObject j)
        {
            try
            {
                j.Add("ISLEM", "2");
                //j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string OgrenciVeliSifreSifirlaKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", "1");
               // j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SifremiUnuttumOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}