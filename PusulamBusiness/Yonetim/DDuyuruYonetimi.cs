using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace PusulamBusiness.Yonetim
{
    public class DDuyuruYonetimi : DBase
    {
        GetIp getIp = new GetIp();
        public string ParametreListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Parametre.ParametreListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Parametre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool DuyuruDosya()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";

                string ID_DUYURU = (HttpContext.Current.Request.Form["ID_DUYURU"] != null) ? HttpContext.Current.Request.Form["ID_DUYURU"].ToString() : "";

                //j.Add("ISLEM", (int)sp_DuyuruYonetimi.DuyuruEkle);
                //j.Add("ID_MENU", ID_MENU);
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
                                    new JProperty("UZANTI", UZANTI)
                                ));
                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/Yonetim/DuyuruYonetimi", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Duyuru.DuyuruDosya);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_DUYURU", ID_DUYURU);
                j.Add("GUID", GUID);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }


            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }
        public bool DuyuruBanner()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string MOBIL_BANNER = HttpContext.Current.Request.Form["MOBIL_BANNER"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";

                string ID_DUYURU = (HttpContext.Current.Request.Form["ID_DUYURU"] != null) ? HttpContext.Current.Request.Form["ID_DUYURU"].ToString() : "";

                //j.Add("ISLEM", (int)sp_DuyuruYonetimi.DuyuruEkle);
                //j.Add("ID_MENU", ID_MENU);
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
                                    new JProperty("UZANTI", UZANTI)
                                ));
                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/Yonetim/DuyuruYonetimi", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Duyuru.DuyuruWebBaner);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_DUYURU", ID_DUYURU);
                j.Add("GUID", GUID);
                j.Add("MOBIL_BANNER", MOBIL_BANNER);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }


            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }
        public int DuyuruEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Duyuru.DuyuruEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }


            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }
        public int DuyuruGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Duyuru.DuyuruGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }


            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }
        public string DuyuruDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Duyuru.DuyuruDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string DuyuruListe(JObject j)
        {
            j.Add("ISLEM", (int)sp_Duyuru.DuyuruListe);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json.Replace("\"selected\":1", "\"selected\":true");
        }

        public string DuyuruAktifPasif(JObject j)
        {
            j.Add("ISLEM", (int)sp_Duyuru.DuyuruAktifPasif);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_Duyuru", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json.Replace("\"selected\":1", "\"selected\":true");
        }


    }
}