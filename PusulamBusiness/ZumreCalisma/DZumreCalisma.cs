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

namespace PusulamBusiness.ZumreCalisma
{
    public class DZumreCalisma : DBase
    {
        GetIp getIp = new GetIp();
        public bool ZumreCalismaEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaKatilimciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json.Substring(1, json.Length - 2).Replace("\"data\":null", "\"data\":\"[]\"");
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaKatilimciSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaKullaniciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKullaniciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json.Substring(1, json.Length - 2).Replace("\"data\":null", "\"data\":\"[]\"");
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaKatilimciAta(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciAta);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaListeleSelect(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaListeleSelect);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaListeleOnaySelect(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaListeleOnaySelect);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaOnayKatilimciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaOnayKatilimciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json.Substring(1, json.Length - 2).Replace("\"data\":null", "\"data\":\"[]\"");
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaKatilimciOnay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciOnay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaKatilimciAtaToplu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciAtaToplu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaKatilimciSilToplu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaKatilimciSilToplu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaYoklamaDurumGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYoklamaDurumGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaYoklamaGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYoklamaGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaYoklamaGirisGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYoklamaGirisGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaYaziliDurumGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYaziliDurumGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaYaziliGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYaziliGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaYaziliGirisGuncelle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYaziliGirisGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaYaziliNotGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYaziliNotGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ZumreCalismaYaziliNotYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaYaziliNotYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String ZumreCalismaCalismalarim(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ZumreCalismaCalismalarim);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string ExcelTopluZumreCalismaKatilimciKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ExcelTopluZumreCalismaKatilimciKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int ExcelTopluZumreCalismaKatilimciEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_ZumreCalisma.ExcelTopluZumreCalismaKatilimciEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<int>("sp_ZumreCalisma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

    }
}