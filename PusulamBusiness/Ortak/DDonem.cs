using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Models.Sinav;

namespace PusulamBusiness.Ortak
{
    public class DDonem : DBase
    {
        public List<MDonem> DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Donem.DonemListele);
                j.Add("ID_MENU", ID_MENU);
                List<MDonem> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDonem>("sp_Donem", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}