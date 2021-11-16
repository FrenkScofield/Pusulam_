using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace PusulamBusiness.UniteTarama
{
    public class DbUniteTarama : DBase
    {
        GetIp getIp = new GetIp();
        public bool SinavTanimla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavTanimla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavListelebyOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavListelebyOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string SinavListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavSonucListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTaramaSinavSonucListesi.SinavSonucListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTaramaSinavSonucListesi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OVGorsunDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.OVGorsunDegistir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavDuzenle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavBilgiEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavBilgiEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
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
                j.Add("ISLEM", (int)sp_UniteTarama.SinavBilgiGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
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
                j.Add("ISLEM", (int)sp_UniteTarama.OgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciPuanListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.OgrenciPuanListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }



        public string MorpaKazanimBul(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.MorpaKazanimBul);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OgrenciPuanKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.OgrenciPuanKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DersPuanSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.DersPuanSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SayfaTurListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SayfaTurListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool RaporDuzenKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.RaporDuzenKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ResimSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.ResimSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string result = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<string>("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                try
                {
                    var AD = JObject.Parse(result)["AD"];
                    var ID_ABIDESINAV = JObject.Parse(result)["ID_ABIDESINAV"];
                    var ID_ABIDESAYFATUR = JObject.Parse(result)["ID_ABIDESAYFATUR"];

                    string yol = "~/Dosyalar/AbideResim/" + ID_ABIDESINAV + "/" + ID_ABIDESAYFATUR;

                    var path = Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath(yol + "/"), AD + ".png"));
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
                catch (Exception)
                {
                }                

                return result.Length > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ResimEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.ResimEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciListeleRapor(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.OgrenciListeleRapor);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavKarsilikGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavKarsilikGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavKarsilikEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinavKarsilikEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = -1;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.Execute("sp_Abide", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string KitapcikGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.KitapcikGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EksikKazanimListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.EksikKazanimListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EksikKazanimTabloSinavOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.EksikKazanimTabloSinavOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string EksikKazanimEtutSinavOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.EksikKazanimEtutSinavOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //public bool MorpaOdevKaydet(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_UniteTarama.MorpaOdevKaydet);
        //        j.Add("ID_MENU", ID_MENU);
        //        int RESULT = -1;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            RESULT = db.Execute("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return RESULT > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        public string SinifNetPuanOrtalamalari(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTaramaSinifPuanOrtalamalari.UniteTaramaSinifRaporu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTaramaSinifPuanOrtalamalari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        //public String SinifListeRaporu(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_UniteTarama.SinifListeRaporu);
        //        j.Add("ID_MENU", ID_MENU);
        //        String json;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            json = db.ExecuteScalar<string>("sp_SinifListeRaporu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        public string SinifMaddeAnalizi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.SinifMaddeAnalizi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MDers> GenelDersListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.GenelDersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDers> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDers>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string BildirimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_UniteTarama.BildirimListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());


                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = (string)db.ExecuteScalar("sp_UniteTarama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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