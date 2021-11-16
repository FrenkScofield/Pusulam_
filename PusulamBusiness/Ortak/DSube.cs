using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using PusulamBusiness.Utility;
using System;
using System.Collections.Generic;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Ortak
{
    public class DSube : DBase
    {
        GetIp getIp = new GetIp();
        public List<MSube> SubeListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sube.SubeListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSube> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSube>("sp_Sube", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSube> SubeListelebyKullanici(JObject j)
        { 
            try
            {
                j.Add("ISLEM", (int)sp_Sube.SubeListelebyKullanici);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSube> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSube>("sp_Sube", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSube> SubeKurListelebyKullanici(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sube.SubeKurListelebyKullanici);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSube> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSube>("sp_Sube", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSube> SubeListeleTumu(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Sube.SubeListeleTumu);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSube> liste;
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    liste=db.Query<MSube>("sp_Sube",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
    }
}