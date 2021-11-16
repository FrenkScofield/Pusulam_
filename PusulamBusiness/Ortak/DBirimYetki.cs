using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System.Collections.Generic;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ortak
{
    public class DBirimYetki : DBase
    {
        GetIp getIp = new GetIp();
        public List<MKademe> KademeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_BirimYetki.KademeListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MKademe> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKademe>("sp_BirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string KullaniciKademeListele(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_BirimYetki.KullaniciKademeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_BirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MKullaniciTipi> KullaniciTipiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_BirimYetki.KullaniciTipiListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MKullaniciTipi> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKullaniciTipi>("sp_BirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

       

        public bool BirimMenuKaydet(JObject j)
        {
            try
            {
                int sonuc = 0;
                j.Add("ISLEM", (int)sp_BirimYetki.BirimMenuKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_BirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}