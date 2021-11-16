using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.ViuKullaniciYetkilendirme;
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

namespace PusulamBusiness.Rapor.Viu
{
    public class DViu : DBase
    {
        GetIp getIp = new GetIp();
        public String AramaSebepListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.AramaSebepListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ViuTopluMesajGonder(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.ViuTopluMesajGonder);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_VIU", j.ToDictionary(), commandTimeout: int.MaxValue, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool ViuTopluResimliMesajGonder()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";
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
                                    new JProperty("UZANTI", UZANTI)
                                ));
                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/viu/ViuTopluMesaj", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                
                var resimUrl = "https://okyanusdata.s3-eu-west-1.amazonaws.com/pusulam/viu/ViuTopluMesaj/" + GUID;
                JObject j = new JObject();
                j.Add("ISLEM", (int)sp_VIU.ViuTopluMesajGonder);
                j.Add("ID_MENU", ID_MENU);
                j.Add("RESIM_URL", resimUrl);
                j.Add("SQLJSON", SQLJSON);
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_VIU", j.ToDictionary(), commandTimeout: int.MaxValue, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }

        }

        public String AramaDurumListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.AramaDurumListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String AramaRapor(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.AramaRapor);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String YoklamaRapor(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.YoklamaRapor);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }



        public string ViuAramaYetkiKullaniciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.ViuAramaYetkiListesi);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);


                }

                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ViuAramaKullaniciYetkiKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.ViuAramaKullaniciYetkiKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public object ViuKullaniciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.ViuKullaniciListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MViuKullanici> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MViuKullanici>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
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
                j.Add("ISLEM", (int)sp_UniteTarama.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string ViuRandevuTakipListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ViuRandevuTakip.ViuRandevuTakipListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ViuRandevuTakip", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string ViuBildirimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ViuBildirimRapor.sp_ViuBildirimListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ViuBildirimRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string ViuRandevuTakipDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ViuRandevuTakip.ViuRandevuTakipDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ViuRandevuTakip", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string OgrenciYoklamaListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberDanismanYoklama.OgrenciYoklamaListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberDanismanYoklama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string RehberDanismanSinifListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_RehberDanismanYoklama.RehberDanismanSinifListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_RehberDanismanYoklama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string KullaniciCihazlariListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_VIU.KullaniciCihazlariListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_VIU", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}