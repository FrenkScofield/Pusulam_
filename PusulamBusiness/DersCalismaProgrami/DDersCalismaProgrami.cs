using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Sinav
{
    public class DDersCalismaProgrami : DBase
    {
        GetIp getIp = new GetIp();
        public List<MDers> DersGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavDersListe);
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
        
        public string DersUniteGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.SinavUniteListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_DersCalismaProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public string DersUniteKazanimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.DersCalismaKazanimList);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_DersCalismaProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }


        public MDersUnite DersUniteDetay(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersUnite.SinavUniteDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                MDersUnite detay;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    detay = db.Query<MDersUnite>("sp_DersUnite", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                return detay;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

     

        public bool DersUniteKazanimOdevEkle(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.KazanimOdevEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersCalismaProgrami", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
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
      
       
        

        public List<MSinavGrup> SinavGrupListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavGrupListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public String KazanimListeleYeni(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.KazanimListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_DersCalismaProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String ProgramListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.ProgramListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_DersCalismaProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DersUniteKazanimProgramEkle(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.KazanimProgramEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersCalismaProgrami", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }
        public bool DersUniteKazanimProgramSil(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_DersCalismaProgrami.KazanimProgramSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());
                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_DersCalismaProgrami", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }

    }
}