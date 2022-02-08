using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Models.Upgrade;
using PusulamBusiness.Models.Ogrenci;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ogrenci
{
    public class DOgrenci : DBase
    {
        GetIp getIp = new GetIp();
        public List<MUpgradeOgrenci> UpgradeOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.UpgradeOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MUpgradeOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MUpgradeOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OBSListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciBilgiSistemi.OBSListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciBilgiSistemi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }



        public string OgrenciDonemDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciDonemDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EslesmeyenOgrenciListeGetir(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.EslesmeyenOgrenciListeGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string TekrarEdenOgrenciGetir(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.TekrarEdenOgrenciGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string TekrarEdenOgrenciExcelGetir(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.TekrarEdenOgrenciExcelGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        internal object OgrenciListelebySinav(JObject j)
        {
            throw new NotImplementedException();
        }

        public int Eslestir(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.Eslestir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    //json = db.ExecuteScalar<string>("sp_SinavListele", j.ToDictionary(), commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int SinavTanimla(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavTanimla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int OgrenciKademeGetir(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciKademeGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MUpgradeOgrenci> TKTOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.TKTOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MUpgradeOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MUpgradeOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MOgrenci> OgrenciListelebyKullanici(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciListelebyKullanici);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MOgrenci> OgrenciListelebyVeli(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciListelebyVeli);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MOgrenci> OgrenciListelebyVeliSinav(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciListelebyVeliSinav);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MOgrenci> OgrenciListeleMultiSinif(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciListeleMultiSinif);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MOgrenci> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MOgrenci>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciSinavFrekansListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciSinavFrekansListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string JSON = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    JSON = db.ExecuteScalar<string>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return JSON;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //public string FrekansSinavOgrenciListele(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_Ogrenci.FrekansSinavOgrenciListele);
        //        j.Add("ID_MENU", ID_MENU);
        //        string JSON = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            JSON = db.ExecuteScalar<string>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return JSON;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        //public string FrekansSinavOgrenciListeleNew(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_Ogrenci.FrekansSinavOgrenciListeleNew);
        //        j.Add("ID_MENU", ID_MENU);
        //        string JSON = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            JSON = db.ExecuteScalar<string>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return JSON;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        public string OgrenciSinavDersFrekansListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Ogrenci.OgrenciSinavDersFrekansListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string JSON = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    JSON = db.ExecuteScalar<string>("sp_Ogrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return JSON;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string DersMuafOgrenciListele(JObject j)//JSON GELİYOR 
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciDersMuaf.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciDersMuaf", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciMuafDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciDersMuaf.OgrenciMuafDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciDersMuaf", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string FrekansMuafOgrenciListele(JObject j)//JSON GELİYOR
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciFrekansMuaf.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciFrekansMuaf", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciFrekansMuafDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciFrekansMuaf.OgrenciMuafDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciFrekansMuaf", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

    }
}