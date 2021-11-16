using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
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

namespace PusulamBusiness.Zumre
{
    public class DZumre : DBase
    {
        GetIp getIp = new GetIp();
        public string KullaniciTipGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.KullaniciTipGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        public string DersUniteGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.SinavUniteListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true") : "";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string Kaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.Kaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);

                throw ex;
            }
        }
        public string Guncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.Guncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);

                throw ex;
            }
        }

        public string DosyaDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.DosyaList);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);

                throw ex;
            }
        }
        public string DosyaSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.DosyaSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);

                throw ex;
            }
        }
        public string LogKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.LogKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);

                throw ex;
            }
        }

        public string LogListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.LogListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool ZumreDosyaKaydet()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";

                string ID_ZUMRE_OZELLIK = (HttpContext.Current.Request.Form["ID_ZUMRE_OZELLIK"].ToString() != null) ? HttpContext.Current.Request.Form["ID_ZUMRE_OZELLIK"].ToString() : "";

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
                          sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/Zumre/ZumreKazanim", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                //List<int> list = new List<int>();
                //var a = Convert.ToInt32(ID_ZUMRE_OZELLIK_ODEV.Split(','));
                //foreach (var item in a)
                //{
                //    list.Add(item);
                //}

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Zumre.DosyaKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_ZUMRE_OZELLIK", ID_ZUMRE_OZELLIK);
                //j.Add("ID_ZUMRE_OZELLIK_ODEV",a);
                j.Add("DOSYALIST", DOSYA);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }

            }

            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }

        public List<MSinavGrup> SinavGrupListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.SinifGrupListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MDers> DersGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Zumre.DersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDers> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDers>("sp_Zumre", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

    }
}