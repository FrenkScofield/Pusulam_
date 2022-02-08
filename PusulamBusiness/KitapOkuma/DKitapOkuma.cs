using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.KitapOkuma
{
    public class DKitapOkuma : DBase
    {
        GetIp getIp = new GetIp();
        public string DonemListele(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_KitapOkuma.Kitap_Listele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KitapOkuma", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        //public string KotaBelirleListe(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaListele);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        //String json;
        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        //public string KotaEkle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaEkle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public string KotaDuzenle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaDuzenle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public bool KotaSil(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KotaSil);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        int sonuc = 0;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            sonuc = db.Execute("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return sonuc > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }

        //}
        ////public string SubeListelebyKullanici(JObject j)
        ////{
        ////    try
        ////    {
        ////        j.Add("ISLEM", (int)sp_SportifKulup.KotaEkle);
        ////        j.Add("ID_MENU", ID_MENU);
        ////        string json = "";
        ////        using (IDbConnection db = new SqlConnection(conStr))
        ////        {
        ////            if (db.State == ConnectionState.Closed)
        ////                db.Open();
        ////            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        ////        }
        ////        return json;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        new DHataLog().HataLogKaydet(j, ex);
        ////        throw ex;
        ////    }
        ////}

        ////public string KulupListelebyKullanici(JObject j)
        ////{
        ////    try
        ////    {
        ////        j.Add("ISLEM", (int)sp_SportifKulup.KulupListele);
        ////        j.Add("ID_MENU", ID_MENU);
        ////        string json = "";
        ////        using (IDbConnection db = new SqlConnection(conStr))
        ////        {
        ////            if (db.State == ConnectionState.Closed)
        ////                db.Open();
        ////            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        ////        }
        ////        return json;

        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        new DHataLog().HataLogKaydet(j, ex);
        ////        throw ex;
        ////    }
        ////}
        //public List<MKulup> KulupListele(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KulupListele);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        List<MKulup> liste;
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            liste = db.Query<MKulup>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
        //        }
        //        return liste;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
        //public string KontrolKotaEkle(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_SportifKulup.KontrolKotaEkle);
        //        j.Add("ID_MENU", ID_MENU);
        //        j.Add("IP", getIp.GetUser_IP());

        //        string json = "";
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed)
        //                db.Open();
        //            json = db.ExecuteScalar<string>("sp_SportifKulup", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return json;

        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}
    }
}