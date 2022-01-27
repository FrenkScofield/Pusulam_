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
    public class DDersOgretmenleri : DBase
    {
        GetIp getIp = new GetIp();
        
        public string DersListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersOgretmenleri.DersListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersOgretmenleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public string DersSil(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersOgretmenleri.DersSil);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersOgretmenleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }

        public string Duzenle(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersOgretmenleri.DersDuzenle);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersOgretmenleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }


        public string Kaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersOgretmenleri.DersEkle);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersOgretmenleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public string DuzenleModal(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersOgretmenleri.DersGetir);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersOgretmenleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        

    }
}
