using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Mobile
{
    public class MSinavSonuc : DBase
    {
        public List<MDonem> DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Donem.DonemListele);
                j.Add("ID_MENU", (int)EMobileMenu.DenemeSinavSonuclari);

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
        public List<MSinavTuru> SinavTuruListeleTc(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());

                j.Add("ISLEM", (int)sp_Sinav.SinavTuruListeleTc);
                j.Add("ID_MENU", (int)EMobileMenu.DenemeSinavSonuclari);
                j.Add("ID_KADEME3", 3);
                j.Add("TC_OGRENCI", param.TCKIMLIKNO);

                List<MSinavTuru> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MSinavTuru>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinav> SinavListelebyOgrenci(JObject j)
        {
            try
            {
                dynamic param = JValue.Parse(j.ToString());

                j.Add("ISLEM", (int)sp_Sinav.SinavListelebyOgrenci);
                j.Add("ID_MENU", (int)EMobileMenu.DenemeSinavSonuclari);
                j.Add("TC_OGRENCI", param.TCKIMLIKNO);
                List<MSinav> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinav>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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
