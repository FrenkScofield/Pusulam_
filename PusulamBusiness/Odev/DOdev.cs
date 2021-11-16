using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Odev
{
    public class DOdev : DBase
    {
        GetIp getIp = new GetIp();
        public string OdevTurKademeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevTurKademeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OdevTurKademeKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevTurKademeKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public bool OgretmenOdevKaydet()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
                string VERILIS_TARIH = (HttpContext.Current.Request.Form["VERILISTARIH"] != null) ? HttpContext.Current.Request.Form["VERILISTARIH"].ToString() : "";
                string TESLIM_TARIH = (HttpContext.Current.Request.Form["TESLIMTARIHI"] != null) ? HttpContext.Current.Request.Form["TESLIMTARIHI"].ToString() : "";
                string ODEV_TUR = (HttpContext.Current.Request.Form["ID_ODEVTUR"] != null) ? HttpContext.Current.Request.Form["ID_ODEVTUR"].ToString() : "";
                string BASLIK = (HttpContext.Current.Request.Form["BASLIK"] != null) ? HttpContext.Current.Request.Form["BASLIK"].ToString() : "";
                string ID_MORPADERS = (HttpContext.Current.Request.Form["ID_MORPADERS"] != null) ? HttpContext.Current.Request.Form["ID_MORPADERS"].ToString() : "";
                string SQLJSON = (HttpContext.Current.Request.Form["SQLJSON"] != null) ? HttpContext.Current.Request.Form["SQLJSON"].ToString() : "";

                var AD = String.Empty;
                var UZANTI = String.Empty;
                string GUID = "";
                var jsonList = new List<JObject>();

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[i] : null;
                    bool sonuc = false;

                    if (DOSYAGUID == "" && file != null)
                    {
                        if (file != null)
                        {
                            AD = file.FileName != null ? file.FileName : "";
                            UZANTI = AD.Split('.').Last();
                            GUID = Guid.NewGuid().ToString();
                            jsonList.Add(
                                new JObject(
                                    new JProperty("AD", AD),
                                    new JProperty("GUID", GUID),
                                    new JProperty("UZANTI",UZANTI)
                                ));
                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/odev/ogretmen", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Odev.OdevKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_ODEVTUR", ODEV_TUR);
                j.Add("SQLJSON", SQLJSON);
                j.Add("DOSYA", DOSYA);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }

        }

        public bool OdevKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public string OdevListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OdevSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public string OgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DanismanMi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.DanismanMi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int RESULT = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    RESULT = db.ExecuteScalar<int>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return RESULT > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OdevVerenListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevVerenListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OdevVerenOdevListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevVerenOdevListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OdevDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevDetayGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OdevKontrolKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevKontrolKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public bool Guncelle()
        {
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
                string VERILIS_TARIH = (HttpContext.Current.Request.Form["VERILISTARIH"] != null) ? HttpContext.Current.Request.Form["VERILISTARIH"].ToString() : "";
                string TESLIM_TARIH = (HttpContext.Current.Request.Form["TESLIMTARIHI"] != null) ? HttpContext.Current.Request.Form["TESLIMTARIHI"].ToString() : "";
                string BASLIK = (HttpContext.Current.Request.Form["BASLIK"] != null) ? HttpContext.Current.Request.Form["BASLIK"].ToString() : "";
                string ID_MORPADERS = (HttpContext.Current.Request.Form["ID_MORPADERS"] != null) ? HttpContext.Current.Request.Form["ID_MORPADERS"].ToString() : "";
                string SQLJSON = (HttpContext.Current.Request.Form["SQLJSON"] != null) ? HttpContext.Current.Request.Form["SQLJSON"].ToString() : "";
                int ID_ODEV = Convert.ToInt32(HttpContext.Current.Request.Form["ID_ODEV"]);
                var AD = String.Empty;
                var UZANTI = String.Empty;
                string GUID = "";

                if (file != null)
                {
                    AD = file.FileName != null ? file.FileName : "";
                    UZANTI = AD.Split('.').Last();
                    GUID = Guid.NewGuid().ToString();
                    CONTENTTYPE = file.ContentType;
                }

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Odev.Guncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("SQLJSON", SQLJSON);
                j.Add("OGRETMENODEVAD", AD);
                j.Add("OGRETMENODEVGUID", GUID);
                j.Add("OGRETMENUZANTI", UZANTI);
                j.Add("ID_ODEV", ID_ODEV);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    bool sonuc = false;

                    if (DOSYAGUID == "" && file != null)
                    {
                        MemoryStream fileToUpload = new MemoryStream();
                        file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                        file.InputStream.Position = 0;
                        sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/odev/ogretmen", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                    }
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
            //try
            //{
            //    j.Add("ISLEM", (int)sp_Odev.Guncelle);
            //    j.Add("ID_MENU", ID_MENU);
            //    int RESULT = -1;
            //    using (IDbConnection db = new SqlConnection(conStr))
            //    {
            //        if (db.State == ConnectionState.Closed)
            //            db.Open();
            //        RESULT = db.Execute("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            //    }
            //    return RESULT > 0;
            //}
            //catch (Exception ex)
            //{
            //    new DHataLog().HataLogKaydet(j, ex);
            //    throw ex;
            //}
        }

        public bool OdevTamamla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevTamamla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public string OgrenciOdevListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OgrenciOdevListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgretmenOdevRaporu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OgretmenOdevRaporu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgretmenSinifOdevListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OgretmenSinifOdevListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public JObject MorpaOdevListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.MorpaOdevListele);
                j.Add("IP", getIp.GetUser_IP());

                JObject result;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);

                    result = JObject.Parse(json);
                }

                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OdevDosyaListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Odev.OdevDosyaListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Odev", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
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
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string TC_OGRENCI = (HttpContext.Current.Request.Form["TC_OGRENCI"] != null) ? HttpContext.Current.Request.Form["TC_OGRENCI"].ToString() : "";
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
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Odev.OdevDosyaYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_ODEV", ID_ODEV);
                j.Add("TC_OGRENCI", TC_OGRENCI);
                j.Add("IP", getIp.GetUser_IP());

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
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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