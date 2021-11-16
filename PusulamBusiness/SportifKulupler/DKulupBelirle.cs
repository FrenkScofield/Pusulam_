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
using System.Linq;

namespace PusulamBusiness.SportifKulupler
{
    public class DKulupBelirle : DBase
    {

        GetIp getIp = new GetIp();
        public string KulupKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.KulupBelirle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                //String json;
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public object KulupEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.KulupEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string KulupDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.KulupDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekKlupKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.YetenekKulupKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool KuluptenAyril(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.KuluptenAyril);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.Execute("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        
        public List<MKulup> KulupListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SportifKulup.KulupListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MKulup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MKulup>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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