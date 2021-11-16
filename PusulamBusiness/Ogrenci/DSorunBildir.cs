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

namespace PusulamBusiness.Ogrenci
{
    public class DSorunBildir : DBase
    {
        GetIp getIp = new GetIp();
        public bool Kaydet()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string ID_SINAV = (HttpContext.Current.Request.Form["ID_SINAV"] != null) ? HttpContext.Current.Request.Form["ID_SINAV"].ToString() : "";
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
                            GUID = Guid.NewGuid().ToString()+"."+UZANTI;
                           
                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            sonuc = AmazonDosyaYukle.sendMyFileToS3(@"pusulam/disogrenci/sorun", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                var jsonT = new JObject(new JProperty("DOSYA", jsonList));
                var DOSYA = jsonT.ToString();

                JObject j = new JObject();
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("OTURUM", OTURUM);
                j.Add("ISLEM", (int)sp_SorunBildir.SorunBildir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("SQLJSON", SQLJSON);
                j.Add("GUID", GUID);
                j.Add("ID_SINAV", ID_SINAV);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    int result = db.Execute("sp_SorunBildir", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(new JObject(), ex);
                throw ex;
            }
        }

        public string SinavListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_SorunBildir.SinavListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_SorunBildir", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
        }

    }
}