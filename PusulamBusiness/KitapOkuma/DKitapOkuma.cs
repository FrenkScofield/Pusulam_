using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
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

namespace PusulamBusiness.KitapOkuma
{
    public class DKitapOkuma : DBase
    {
        GetIp getIp = new GetIp();
        public string KitapListele(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_KitapOkuma.Kitap_Listele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KitapOkuma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int Kaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZekaTest.Kaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_KitapOkuma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DosyaKaydet()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
                string ID_EKLENENKITAP = (HttpContext.Current.Request.Form["ID_EKLENENKITAP"] != null) ? HttpContext.Current.Request.Form["ID_EKLENENKITAP"].ToString() : "";


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
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/Bulten/Bultenler", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_Bulten.BultenDosyaKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("ID_EKLENENKITAP", ID_EKLENENKITAP);
                j.Add("DOSYA_GUID", GUID);
                j.Add("IP", getIp.GetUser_IP());
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_KitapOkuma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }

        }
        //public string KotaBelirleListe(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaListele);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        //String json;
        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        //public string KotaEkle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaEkle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public string KotaDuzenle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaDuzenle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public bool KotaSil(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaSil);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        int sonuc = 0;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            sonuc = db.Execute("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return sonuc > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }

        //}
        ////public string SubeListelebyKullanici(JObject j)
        ////{
        ////    try
        ////    {
        ////        j.Add("ISLEM", (int)sp_SportifKulup.KotaEkle);
        ////        j.Add("ID_MENU", ID_MENU);
        ////        string json = "";
        ////        using (IDbConnection db = new SqlConnection(conStr))
        ////        {
        ////            if (db.State == ConnectionState.Closed)
        ////                db.Open();
        ////            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        ////        }
        ////        return json;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        new DHataLog().HataLogKaydet(j, ex);
        ////        throw ex;
        ////    }
        ////}

        ////public string KulupListelebyKullanici(JObject j)
        ////{
        ////    try
        ////    {
        ////        j.Add("ISLEM", (int)sp_SportifKulup.KulupListele);
        ////        j.Add("ID_MENU", ID_MENU);
        ////        string json = "";
        ////        using (IDbConnection db = new SqlConnection(conStr))
        ////        {
        ////            if (db.State == ConnectionState.Closed)
        ////                db.Open();
        ////            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        ////        }
        ////        return json;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        new DHataLog().HataLogKaydet(j, ex);
        ////        throw ex;
        ////    }
        ////}
        //public List<MKulup> KulupListele(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KulupListele);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        List<MKulup> liste;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            liste = db.Query<MKulup>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
        //        }
        //        return liste;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public string KontrolKotaEkle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KontrolKotaEkle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
    }
}