using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System;
using PusulamBusiness.Models.Ortak;

namespace PusulamBusiness.Ortak
{
    public class DHataLog : DBase
    {

        public int HataLogKaydet(JObject j, Exception hata)
        {
            int sonuc = 0;
            try
            {
                MHataLog hataLog = new MHataLog()
                {
                    ID_MENU = j.SelectToken("ID_MENU") == null ? 0 : Convert.ToInt32(j.SelectToken("ID_MENU")),
                    BASLIK = hata.Message,
                    ACIKLAMA = "NameSpace: " + hata.Source + " Fonksiyon Adı: " + hata.TargetSite.Name + " Detay: " + hata.StackTrace,
                    TCKIMLIKNO = j.SelectToken("TCKIMLIKNO").ToString(),
                    OTURUM = j.SelectToken("OTURUM") == null ? "" : Convert.ToString(j.SelectToken("OTURUM"))
                };

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_HataLog", hataLog, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
            }
            return sonuc;
        }
    }
}