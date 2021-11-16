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

namespace PusulamBusiness.Ogrenci
{
    public class DOnlineSinav : DBase
    {
        GetIp getIp = new GetIp();
        public String SinavListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SinavListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String SoruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SoruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String SoruListeleCevapli(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SoruListeleCevapli);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String SoruGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SoruGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String SinavTuruGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SinavTuruGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public int GirisEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.GirisEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.ExecuteScalar<int>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool SinaviBitir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.SinaviBitir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public String OnlineSinavDetay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavDetay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OnlineSinavDetayKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavDetayKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());


                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OnlineSinavOgrenciAta(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciAta);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String APIOnlineSinavOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.APIOgrenciSinavListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
            
        }
        public String OnlineSinavlarimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavlarimListele);
                j.Add("ID_MENU", ID_MENU);

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineTestSinavlarimListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineTestListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavSoruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavSoruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OnlineSinavSoruIsaretle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavSoruIsaretle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavPerformans(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavPerformans);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public int OnlineSinavOgrenciAtamaKaldir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciAtamaKaldir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.ExecuteScalar<int>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavOnizlemeSoruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOnizlemeSoruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavOgrenciOturumListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciOturumListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OnlineSinavOgrenciOturumSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciOturumSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OnlineSinavOgrenciOturumSifirla(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOgrenciOturumSifirla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String OnlineSinavCikti(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavCikti);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineSinavOturumOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavOturumOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OnlineSinavKurumDisiOgrenciAtama(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_DisOgrenci.OnlineSinavKurumDisiOgrenciAtama);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string result = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                     result = db.ExecuteScalar<string>("sp_DisOgrenci", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }


           
        }

        public string YonergeKontrol(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.YonergeKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string result = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool YonergeOnay(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.YonergeOnay);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool result =false;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    result = db.ExecuteScalar<bool>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }

                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        
        public String OnlineSinavLog(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavLog);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String OnlineTestSinavSoruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineTestSinavSoruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool OnlineTestSinavBitir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineTestSinavBitir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                bool result = false;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.ExecuteScalar<bool>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String OnlineSinavDisOgrenciListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OnlineSinav.OnlineSinavDisOgrenciListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json = "";

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OnlineSinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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