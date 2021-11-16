using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.OutSide
{
    public class DOSOdev : DBase
    {
        GetIp getIp = new GetIp();

        string APIKEY = "7da3b771-8007-455b-9bf0-595528512aa0";

        public string OsOdevListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OsOdevListele.OsOdevListele);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string TamamlandiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OsOdevListele.OdevTamamlandiListele);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }        
        public bool OdevTamamla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OsOdevListele.OdevTamamla);
                j.Add("IP", getIp.GetUser_IP());

                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return RESULT > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OgrenciOdevIptal(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OsOdevListele.OgrenciOdevIptal);
                j.Add("IP", getIp.GetUser_IP());

                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return RESULT > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string OsOdevDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OsOdevListele.OsOdevDetayGetir);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string OdevDosyaYukle()
        {
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string TC_OGRENCI = HttpContext.Current.Request.Form["TC_OGRENCI"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
                string AD = (HttpContext.Current.Request.Form["AD"] != null) ? HttpContext.Current.Request.Form["AD"].ToString() : "";
                int ID_ODEV = (HttpContext.Current.Request.Form["ID_ODEV"] != null) ? Convert.ToInt32(HttpContext.Current.Request.Form["ID_ODEV"].ToString()) : 0;
                int ID_MEDYATUR = Convert.ToInt32(HttpContext.Current.Request.Form["ID_MEDYATUR"].ToString());
                if (file != null && file.ContentLength > 0)
                    CONTENTTYPE = file.ContentType;

                byte[] binary = null;

                JObject j = new JObject();

                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("TC_OGRENCI", TC_OGRENCI);
                j.Add("ISLEM", (int)sp_OsOdevListele.OdevDosyaYukle);
                j.Add("ID_ODEV", ID_ODEV);
                j.Add("APIKEY", APIKEY);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string jsonArrayString = db.ExecuteScalar<string>("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);

                    //JObject o = Json.Decode(jsonArrayString)[0];


                    string GUID = Json.Decode(jsonArrayString)[0]["GUID"].ToString();

                    bool result = false;
                    if (DOSYAGUID == "" && file != null && file.ContentLength > 0)
                    {
                        MemoryStream fileToUpload = new MemoryStream();
                        file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                        file.InputStream.Position = 0;

                        result = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/odev/ogrenci", GUID, fileToUpload, file.ContentType, TCKIMLIKNO);

                        if (!result)
                        {
                            JObject jOdevSil = new JObject();

                            jOdevSil.Add("TCKIMLIKNO", TCKIMLIKNO);
                            jOdevSil.Add("TC_OGRENCI", TC_OGRENCI);
                            jOdevSil.Add("APIKEY", APIKEY);
                            jOdevSil.Add("ID_ODEVSINIFOGRENCIDOSYA", Json.Decode(jsonArrayString)[0]["ID_ODEVSINIFOGRENCIDOSYA"].ToString());
                            OdevDosyaSil(jOdevSil);
                        }
                    }

                    return result ? jsonArrayString : null;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }

        public bool OdevDosyaSil(JObject j)
        {
            try
            {
                j.Add("IP", getIp.GetUser_IP());
                j.Add("ISLEM", (int)sp_OsOdevListele.OdevDosyaSil);
                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_OsOdevListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return RESULT > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}