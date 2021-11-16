using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using PusulamBusiness.Ortak;
using System.Net;
using System.Xml.Linq;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ogrenci
{
    public class DHedef : DBase
    {
        GetIp getIp = new GetIp();
        public bool HedefEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String HedefListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String VeliHedefListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.VeliHedefListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String HedefListelePuanTur(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefListelePuanTur);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool HedefSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OOBPKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.OOBPKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int OOBPGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.OOBPGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int OOBP = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    OOBP = db.ExecuteScalar<int>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return OOBP;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String OgrenciSonPuanGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.OgrenciSonPuanGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String OgrenciSinavNetListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.OgrenciSinavNetListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool HedefNetEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.HedefNetEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String KazanimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.KazanimListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String KazanimListeleYeni(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.KazanimListeleYeni);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String PuanTuruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.PuanTuruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }




        public String OgrenciSonDogruGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedefOO.OgrenciSonDogruGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedefOO", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String SinavTuruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedefOO.SinavTuruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedefOO", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String SinavTuruListeleGenel(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.SinavTuruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String SinavTuruNetGrafikListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedef.SinavTuruNetGrafikListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHedef", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool HedefNetEkleOO(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHedefOO.HedefNetEkleOO);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_OgrenciHedefOO", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject MorpaLinkGetir(JObject j)
        {
            ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                var postData = "at=ac";
                postData += "&uyeip=" + j.GetValue("IP").ToString();
                postData += "&tckimlik=" + j.GetValue("TCKIMLIKNO").ToString();
                postData = "http://www.morpakampus.com/api.asp?" + postData;

                var responseString = client.DownloadString(postData);
                var truefalse = XElement.Parse(responseString).Descendants("OK").Single().Value;

                dynamic result = new JObject();

                if (truefalse == "1")
                {
                    var autcode = XElement.Parse(responseString).Descendants("R").Single().Attribute("authcode").Value;
                    var domain = XElement.Parse(responseString).Descendants("R").Single().Attribute("domain").Value;
                    result.SUCCESS = true;
                    result.URL = domain + "/api.asp?at=icerik&ac=" + autcode + "&it=d&konu=";
                    result.ERROR = "";
                    return result;
                }
                else
                {
                    result.SUCCESS = false;
                    result.URL = "";
                    result.ERROR = XElement.Parse(responseString).Descendants("HATA").Single().Value;
                    return result;
                }
            }
        }

        public JObject MorpaLinkGetirGiris(JObject j)
        {
            ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            using (var client = new WebClient())
            {
                var postData = "at=ac";
                postData += "&uyeip=" + j.GetValue("IP").ToString();
                postData += "&tckimlik=" + j.GetValue("TCKIMLIKNO").ToString();
                postData = "http://www.morpakampus.com/api.asp?" + postData;

                var responseString = client.DownloadString(postData);
                var truefalse = XElement.Parse(responseString).Descendants("OK").Single().Value;

                dynamic result = new JObject();

                if (truefalse == "1")
                {
                    var autcode = XElement.Parse(responseString).Descendants("R").Single().Attribute("authcode").Value;
                    var domain = XElement.Parse(responseString).Descendants("R").Single().Attribute("domain").Value;
                    result.SUCCESS = true;
                    result.URL = domain + "/api.asp?at=giris&ac=" + autcode + "";
                    result.ERROR = "";
                    return result;
                }
                else
                {
                    result.SUCCESS = false;
                    result.URL = "";
                    result.ERROR = XElement.Parse(responseString).Descendants("HATA").Single().Value;
                    return result;
                }
            }
        }
    }
}