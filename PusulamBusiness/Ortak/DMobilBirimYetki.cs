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

namespace PusulamBusiness.Ortak
{
    public class DMobilBirimYetki : DBase
    {

        public List<MKademe> KademeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_MobilBirimYetki.KademeListe);
                j.Add("ID_MENU", ID_MENU);
                List<MKademe> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKademe>("sp_MobilBirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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
                j.Add("ISLEM", (int)sp_MobilBirimYetki.KullaniciKademeListele);
                j.Add("ID_MENU", ID_MENU);
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_MobilBirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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
                j.Add("ISLEM", (int)sp_MobilBirimYetki.KullaniciTipiListele);
                j.Add("ID_MENU", ID_MENU);
                List<MKullaniciTipi> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKullaniciTipi>("sp_MobilBirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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
                j.Add("ISLEM", (int)sp_MobilBirimYetki.BirimMenuKaydet);
                j.Add("ID_MENU", ID_MENU);
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_MobilBirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MMenu> MenuListelebyYetki(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_MobilBirimYetki.MenuListelebyYetki);
                j.Add("ID_MENU", ID_MENU);
                List<MMenu> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MMenu>("sp_MobilBirimYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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