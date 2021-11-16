using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Ortak
{
    public class DKullaniciYetki: DBase
    {
        GetIp getIp = new GetIp();
        public List<MKullaniciYetki> KullaniciYetkiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KullaniciYetki.KullaniciYetkiListele);
                j.Add("ID_MENU", ID_MENU);
                List<MKullaniciYetki> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKullaniciYetki>("sp_KullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool KullaniciYetkiGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KullaniciYetki.KullaniciYetkiGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_KullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc>0?true:false;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public object KullaniciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KullaniciYetki.KullaniciListele);
                j.Add("ID_MENU", ID_MENU);
                List<MKullanici> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKullanici>("sp_KullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool KullaniciMenuKaydet(JObject j)
        {
            try
            {
                int sonuc = 0;
                j.Add("ISLEM", (int)sp_KullaniciYetki.KullaniciMenuKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_KullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String KullaniciSubeSinifGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KullaniciYetki.KullaniciSubeSinifGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KullaniciYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String MenuKullaniciYetkiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KullaniciMenuYetkiKaldir.MenuKullaniciYetkiListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KullaniciMenuYetkiKaldir", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool MenuKullaniciYetkiKaldir(JObject j)
        {
            try
            {
                int sonuc = 0;
                j.Add("ISLEM", (int)sp_KullaniciMenuYetkiKaldir.MenuKullaniciYetkiKaldir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_KullaniciMenuYetkiKaldir", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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