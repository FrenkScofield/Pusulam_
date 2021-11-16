using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Etut
{
    public class DEtut:DBase
    {
        GetIp getIp = new GetIp();
        public string EtutTabloSinav(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.EtutTabloSinav);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string EtutTabloYaziliOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.EtutTabloYaziliOgrenci);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string EtutTabloSinavOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.EtutTabloSinavOgrenci);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public List<MSinavTuru> SinavTuruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.SinavTuruListele);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    liste=db.Query<MSinavTuru>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public bool SinavEtutKatilimDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SinavEtutKatilimDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool json = false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<bool>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SubeKademeDersListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SubeKademeDersListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavEtutDuzelt(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SinavEtutDuzelt);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool json = false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<bool>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool AktifPasifYap(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.AktifPasifYap);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool json = false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<bool>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SubeKademeDersOgretmenListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SubeKademeDersOgretmenListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavEtutOgrenciListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SinavEtutOgrenciListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavEtutOgrenciDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SinavEtutOgrenciDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool json = false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<bool>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EtutOlustur(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.EtutOlustur);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string SinavTuruDersleriListele(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.SinavTuruDersleriListele);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
        public string SubeOgretmenListe(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.SubeOgretmenListe);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string EtutTabloYazili(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Etut.EtutTabloYazili);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Etut",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }




        public string SinavEtutListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.SinavEtutListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        //sp_EtutSabitOlustur

        public string DersOgretmenListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutSabitOlustur.DersOgretmenListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutSabitOlustur", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool EtutSabitKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutSabitOlustur.EtutSabitKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool json = false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<bool>("sp_EtutSabitOlustur", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EtutSabitListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutSabitOlustur.EtutSabitListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutSabitOlustur", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //sp_EtutProgramiOV
        public string EtutSabitListeOV(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutProgramiOV.EtutSabitListeOV);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutProgramiOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavEtutListeOV(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutProgramiOV.SinavEtutListeOV);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutProgramiOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavEtutKatilimGrafigiOV(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutProgramiOV.SinavEtutKatilimGrafigiOV);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutProgramiOV", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        //sp_EtutTakvimi
        public string TakvimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutTakvimi.TakvimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutTakvimi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EtutDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutTakvimi.EtutDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutTakvimi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //sp_EtutSinifRapor
        public string SinifOgretmenSureListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutSinifRapor.SinifOgretmenSureListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutSinifRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string SinifOgretmenDetayListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutSinifRapor.SinifOgretmenDetayListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutSinifRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //sp_EtutOgretmenRapor
        public string OgretmenSinifSureListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutOgretmenRapor.OgretmenSinifSureListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutOgretmenRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string OgretmenSinifDetayListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutOgretmenRapor.OgretmenSinifDetayListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutOgretmenRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string SubeOgretmenListeRapor(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_EtutOgretmenRapor.SubeOgretmenListeRapor);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_EtutOgretmenRapor", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string EtutDetayGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Etut.EtutDetayGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Etut", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}