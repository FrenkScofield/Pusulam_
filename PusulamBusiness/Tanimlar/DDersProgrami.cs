using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Enums;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Utility;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Tanimlar
{
    public class DDersProgrami : DBase
    {
        GetIp getIp = new GetIp();
        public string KademeKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SubeYetki.KademeYetkiKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SubeYetki", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string DersListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersEkleme.DersListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersEkleme", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3ListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3ListeleMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListeleMultiSube(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListelebyKullaniciMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullaniciMultiSube(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string SinifPersonel(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersProgrami.PersonelListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            json = json == null ? "" : json;
            return json != null ? json.Replace("\"selected\":1", "\"selected\":true") : "";
        }
        public string DersProgramiGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersProgrami.DersProgram);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public string DersOgretmenGetir(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersProgrami.DersOgretmen);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public string DersPersonelKaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersProgrami.DersPersonelKaydet);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersProgrami", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }

    }
}
