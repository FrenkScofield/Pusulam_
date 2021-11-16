using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Sinav
{
    public class DDersUnite: DBase
    {
        GetIp getIp = new GetIp();
        public List<MDers> DersGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavDersListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDers> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDers>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MDers> YaziliDersGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.YaziliDersGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDers> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDers>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
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
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string> ("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):""; 
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public MDersUnite DersUniteDetay(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                MDersUnite detay;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    detay = db.Query<MDersUnite>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return detay;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public bool DersUniteSil(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc =0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersUnite", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc>0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public bool UniteSilTumu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.UniteSilTumu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersUnite", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool UniteEkleExcel(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.UniteEkleExcel);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool UniteLinkEkleExcel(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.UniteLinkEkleExcel);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DersUniteGuncelle(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersUnite", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public bool DersUniteEkle(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersUnite", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public List<MDers> GenelDersListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.GenelDersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDers> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDers>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string DersUniteDosyaYukle()
        {
            try
            {
                var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string CONTENTTYPE = (HttpContext.Current.Request.Form["CONTENTTYPE"] != null) ? HttpContext.Current.Request.Form["CONTENTTYPE"].ToString() : "";                
                string KOD = (HttpContext.Current.Request.Form["KOD"] != null) ? HttpContext.Current.Request.Form["KOD"].ToString() : "";
                int ID_MEDYATUR = Convert.ToInt32(HttpContext.Current.Request.Form["ID_MEDYATUR"].ToString());
                if (file != null && file.ContentLength > 0)
                    CONTENTTYPE = file.ContentType;

                byte[] binary = null;

                JObject j = new JObject();

                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_DersUnite.DersUniteDosyaYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("KOD", KOD);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    string jsonArrayString = db.ExecuteScalar<string>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);

                    //JObject o = Json.Decode(jsonArrayString)[0];


                    string GUID = Json.Decode(jsonArrayString)[0]["GUID"].ToString();

                    bool result = false;
                    if (DOSYAGUID == "" && file != null && file.ContentLength > 0)
                    {
                        MemoryStream fileToUpload = new MemoryStream();
                        file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                        file.InputStream.Position = 0;

                        result = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/kazanim/dosya", GUID, fileToUpload, file.ContentType, TCKIMLIKNO);

                        if (!result)
                        {
                            JObject jKazanimDosyaSil = new JObject();

                            jKazanimDosyaSil.Add("TCKIMLIKNO", TCKIMLIKNO);
                            jKazanimDosyaSil.Add("OTURUM", OTURUM);
                            jKazanimDosyaSil.Add("ID_KAZANIMDOSYA", Json.Decode(jsonArrayString)[0]["ID_KAZANIMDOSYA"].ToString());
                            DersUniteDosyaPasif(jKazanimDosyaSil);
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

        public string DersUniteDosyaListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.DersUniteDosyaListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

        public bool DersUniteDosyaPasif(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.DersUniteDosyaPasif);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}