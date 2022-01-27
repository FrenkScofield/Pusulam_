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
    public class DDersSaatleri : DBase
    {
        GetIp getIp = new GetIp();

        public string DersSaatleriListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersSaatleri.DersSaatleriListele);
            j.Add("ID_MENU", ID_MENU);

            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_DersSaatleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return json;
        }
        public bool DersSaatleriKaydet(JObject j)
        {
            j.Add("ISLEM", (int)sp_DersSaatleri.DersSaatleriKaydet);
            j.Add("ID_MENU", ID_MENU);

            int sonuc = -1;
            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                sonuc = db.Execute("sp_DersSaatleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }
            return sonuc > 0;
        }
        public string DersListele(JObject j)
        {
            j.Add("ISLEM", (int)sp_OgretmenDersleri.DersListele);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_OgretmenDersleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }
        public string DersSil(JObject j)
        {
            j.Add("ISLEM", (int)sp_OgretmenDersleri.DersSil);
            j.Add("ID_MENU", ID_MENU);
            j.Add("IP", getIp.GetUser_IP());
            string json = "";

            using (IDbConnection db = new SqlConnection(conStr))
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                json = db.ExecuteScalar<string>("sp_OgretmenDersleri", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
            }

            return json;
        }



    }
}
