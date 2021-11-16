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

namespace PusulamBusiness.UniteTarama
{
    public class DSinifListeRaporu : DBase
    {

        GetIp getIp = new GetIp();
        public string SinifListeRaporu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinifListeRaporu.SinifListeRaporu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                //String json;
                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_SinifListeRaporu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string SinifListeFotografli(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinifListeRaporu.SinifListesiFotografli);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                //String json;
                string sonuc = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<string>("sp_SinifListeRaporu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    //dynamic json = JsonConvert.DeserializeObject(sonuc);
                    //return JsonConvert.SerializeObject(json, Formatting.Indented);

                }
                // string Resim = Convert.ToBase64String((byte[])["RESIM"]);

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