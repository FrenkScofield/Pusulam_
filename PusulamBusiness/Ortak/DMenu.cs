using Newtonsoft.Json.Linq;
using PusulamBusiness.Models.Ortak;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System.Collections.Generic;
using PusulamBusiness.Enums;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ortak
{
    public class DMenu : DBase
    {
        GetIp getIp = new GetIp();
        public List<MMenu> MenuGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Menu.MenuGetir);
                j.Add("ID_MENU", ID_MENU);
                //j.Add("IP", getIp.GetUser_IP());

                List<MMenu> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MMenu>("sp_Menu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public String MenuYardimGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Menu.MenuYardimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string html ="";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    html = db.ExecuteScalar<string>("sp_Menu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return html;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MMenu> MenuListelebyYetki(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Menu.MenuListelebyYetki);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MMenu> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MMenu>("sp_Menu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MMenu> KullaniciMenuListelebyYetki(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Menu.KullaniciMenuListelebyYetki);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MMenu> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MMenu>("sp_Menu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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