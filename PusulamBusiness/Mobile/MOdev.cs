using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;

namespace PusulamBusiness.Mobile
{
    public class MOdev: DBase
    {
        public JArray DonemListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_Filtre.DonemListele);
            j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);


            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                {
                    db.Open();
                }

                string json = db.ExecuteScalar<string>("sp_Filtre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                json = json == null ? "" : json;
                json =  json != null ? json.Replace("\"selected\":1", "\"selected\":true") : "";

                return JArray.Parse(json);
            }
        }

        public JArray OgrenciOdevDersListele(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());

                j.Add("TC_OGRENCI", param.TCKIMLIKNO);
                j.Add("ISLEM", (int)sp_FiltreEk.OgrenciOdevDersListele);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_FiltreEk", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JArray.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JArray OgrenciOdevListele(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());

                j.Add("TC_OGRENCI", param.TCKIMLIKNO);
                j.Add("ISLEM", (int)sp_Odev.OgrenciOdevListele);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JArray.Parse(json);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject OdevDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevDetayGetir);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string  json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return JObject.Parse(json);
                }
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
                j.Add("ISLEM", (int)sp_Odev.OdevTamamla);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);

                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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
                j.Add("ISLEM", (int)sp_Odev.OgrenciOdevIptal);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);

                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return RESULT > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string OdevDosyaYukle()
        {
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string TC_OGRENCI = (HttpContext.Current.Request.Form["TC_OGRENCI"] != null) ? HttpContext.Current.Request.Form["TC_OGRENCI"].ToString() : "";
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
                string AD = (HttpContext.Current.Request.Form["AD"] != null) ? HttpContext.Current.Request.Form["AD"].ToString() : "";
                int ID_ODEV = (HttpContext.Current.Request.Form["ID_ODEV"] != null) ? Convert.ToInt32(HttpContext.Current.Request.Form["ID_ODEV"].ToString()) : 0;
                if (file != null && file.ContentLength > 0)
                    CONTENTTYPE = file.ContentType;

                byte[] binary = null;

                JObject j = new JObject();

                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Odev.OdevDosyaYukle);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);
                j.Add("ID_ODEV", ID_ODEV);
                j.Add("TC_OGRENCI", TC_OGRENCI);
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string jsonArrayString = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);

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
                            jOdevSil.Add("OTURUM", OTURUM);
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
                j.Add("ISLEM", (int)sp_Odev.OdevDosyaSil);
                j.Add("ID_MENU", (int)EMobileMenu.OgrenciOdevListesi);
                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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
