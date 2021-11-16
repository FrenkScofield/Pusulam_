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

namespace PusulamBusiness.Ortak
{
    public class DGrup : DBase
    {
        public List<MSinavGrup> SinavGrupListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.SinavGrupListele);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSinavGrup> Kademe3ListelebyKullanici(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.Kademe3ListelebyKullanici);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSinavGrup> Kademe3KurListelebyKullanici(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.Kademe3KurListelebyKullanici);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSinavGrup> Kademe3ListeleMultiSube(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.Kademe3ListeleMultiSube);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinavGrup> Kademe3ListeleMultiSubeYetkisiz(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.Kademe3ListeleMultiSubeYetkisiz);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public List<MSinavGrup> Kademe3ListeleSubesiz(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Grup.Kademe3ListeleSubesiz);
                j.Add("ID_MENU", ID_MENU);
                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Grup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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