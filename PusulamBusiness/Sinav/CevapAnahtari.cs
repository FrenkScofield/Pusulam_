using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Sinav
{
    public class CevapAnahtari:DBase
    {
        GetIp getIp = new GetIp();
        public string DersListele(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_CevapAnahtariKullaniciYetki.DersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_CevapAnahtariKullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int KullaniciYetkiKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_CevapAnahtariKullaniciYetki.KullaniciYetkiKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_CevapAnahtariKullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavBilgiGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_CevapAnahtariDuzenle.SinavBilgiGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_CevapAnahtariDuzenle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json == null ? "" : json;
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int CevapAnahtariKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_CevapAnahtariDuzenle.CevapAnahtariKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_CevapAnahtariDuzenle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}