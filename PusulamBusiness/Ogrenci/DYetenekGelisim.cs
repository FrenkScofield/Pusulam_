using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ogrenci
{
    public class DYetenekGelisim : DBase
    {
        GetIp getIp = new GetIp();
        public string YetenekGelisim(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YetenekGelisim);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YG_MevcutDagilim(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_MevcutDagilim.YG_MevcutDagilim);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_MevcutDagilim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool TarihAyarla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_SecimAyar.TarihAyarla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_SecimAyar", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YG_OgrenciBosalt(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_KotaBelirle.YG_OgrenciBosalt);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_KotaBelirle", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YetenekDersSec(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_Secim.YetenekDersSec);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_Secim", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YetenekDersCik(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_Secim.YetenekDersCik);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_Secim", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_Secim.YetenekKulupKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_Secim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YG_YetenekAciklamaEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YG_YetenekAciklamaEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_YetenekGelisim", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OgrenciAktar(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_SecimAyar.OgrenciAktar);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_SecimAyar", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YG_KotaBelirle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_KotaBelirle.YG_KotaBelirle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_YG_KotaBelirle", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SecimTarihGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_SecimAyar.SecimTarihGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_SecimAyar", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YG_KategoriKotaGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_KotaBelirle.YG_KategoriKotaGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_KotaBelirle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YG_KategoriAciklamaGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YG_KategoriAciklamaGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YG_KategoriListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YG_KategoriListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekGelisimIlkSinifPuanKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YetenekGelisimIlkSinifPuanKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekGelisimUstSinifPuanKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YetenekGelisimUstSinifPuanKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekGelisimOgrenciGecmis(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YetenekGelisimOgrenciGecmis);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YetenekGelisimDersListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YetenekGelisim.YetenekGelisimDersListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YetenekGelisim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YorumListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YorumEkle.YorumListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YorumEkle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YorumDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_YorumEkle.YorumDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_YorumEkle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
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
                j.Add("ISLEM", (int)sp_YG_Karne.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_Karne", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciKarneListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_Karne.OgrenciKarneListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_Karne", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SecilebilirYetenekListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_Secim.SecilebilirYetenekListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_Secim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SecimYapmayanlarListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YG_SecimYapmayanlar.SecimYapmayanlarListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_YG_SecimYapmayanlar", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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