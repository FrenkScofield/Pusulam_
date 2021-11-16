using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Morpa
{
    public class DMorpa : DBase
    {
        GetIp getIp = new GetIp();

        /////////////////////////////////////////////   DERS
        
        public string MorpaDersListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.DersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
               
        public bool MorpaDersEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaDersEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
               
        public bool AktifPasifDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.AktifPasifDegistir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MorpaDersGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaDersGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        /////////////////////////////////////////////   KAZANIM
        
        public string MorpaKazanimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaKazanimListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        public bool MorpaKazanimEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaKazanimEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
               
        public bool KazanimAktifPasifDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.KazanimAktifPasifDegistir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MorpaKazanimGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaMateryalGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        /////////////////////////////////////////////   MATERYAL
        
        public string MorpaMateryalListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaMateryalListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        public bool MorpaMateryalEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaMateryalEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MateryalAktifPasifDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MateryalAktifPasifDegistir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MorpaMateryalGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaMateryalGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MorpaKazanimExcelYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Morpa.MorpaKazanimExcelYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Morpa", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        /////////////////////////////////////////////
        ///

        
        public string MorpaLink(JObject j)
        {
            dynamic jObj = j;

            using (var client = new WebClient())
            {
                var postData = "at=ac";
                postData += "&uyeip=" + jObj.IP;
                //95.6.79.122
                //postData += "&uyeip=95.6.79.122";
                postData += "&tckimlik=" + jObj.TCKIMLIKNO;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var responseString = client.DownloadString("https://www.morpakampus.com/api.asp?" + postData);
                var truefalse = XElement.Parse(responseString).Descendants("OK").Single().Value;


                if (truefalse == "1")
                {
                    var autcode = XElement.Parse(responseString).Descendants("R").Single().Attribute("authcode").Value;
                    var domain = XElement.Parse(responseString).Descendants("R").Single().Attribute("domain").Value;
                    //string autcode = responseString.ToString().Split(' ')[4].Split('=')[1];
                    //autcode = autcode.Substring(1, autcode.Length - 2);

                    //string domain = responseString.ToString().Split(' ')[5].Split('=')[1];
                    //domain = domain.Substring(1, domain.Length - 8);
                    return "1-" + domain + "/api.asp?at=giris&ac=" + autcode;
                }
                else
                {
                    var hata = "0-" + XElement.Parse(responseString).Descendants("HATA").Single().Value;
                    return hata;
                }


            }
        }       
        
        public JArray MorpaKullaniciListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", 1);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_MorpaKullaniciListesi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return (JArray)JsonConvert.DeserializeObject(json); //json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}