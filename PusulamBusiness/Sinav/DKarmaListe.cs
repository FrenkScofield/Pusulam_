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

namespace PusulamBusiness.Sinav
{
    public class DKarmaListe : DBase
    {
        GetIp getIp = new GetIp();
        public String KarmaListeGetir(JObject j)//JSON
        {
            try
            {
                j.Add("ISLEM", (int)sp_KarmaListe.KarmaListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KarmaListe", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public void ExcelYukle(string tc, string oturum,string sinavTarih,string s)
        {
            JObject j = new JObject();
            try
            {
                j.Add("ISLEM", (int)sp_KarmaListe.ExcelYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("TCKIMLIKNO", tc);
                j.Add("OTURUM", oturum);
                j.Add("SINAVTARIH", sinavTarih);
                j.Add("JSEXCEL", s);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KarmaListe", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}