using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Script.Serialization;

namespace PusulamBusiness.Mobile
{
    public class MMenu : DBase
    {
        public JArray MenuGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Menu.MenuGetir);
                j.Add("ID_MENU", (int)EMobileMenu.Anasayfa);
                j.Add("ID_UYGULAMA ", 5);

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();

                    var liste = db.Query<PusulamBusiness.Models.Ortak.MMenu> ("sp_Menu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                    string json = new JavaScriptSerializer().Serialize(liste);
                    return JArray.Parse(json);
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
