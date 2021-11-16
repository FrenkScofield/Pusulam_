using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PusulamBusiness.Veli
{
    public class DFatura : DBase
    {
        GetIp getIp = new GetIp();
        public int FaturaOdemeEkle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Fatura.FaturaOdemeEkle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.ExecuteScalar<int>("sp_Fatura", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool FaturaOdemeCevap(JObject j)
        {
            try
            {
                
                JObject jsonRequest = new JObject();
                jsonRequest.Add("ISLEM", (int)sp_Fatura.FaturaOdemeCevap);
                jsonRequest.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                jsonRequest.Add("SQLJSON", JsonConvert.SerializeObject(j));

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_Fatura", jsonRequest.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        //public bool FaturaOdemeCevapSet(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_Fatura.FaturaOdemeCevapSet);
        //        j.Add("ID_MENU", ID_MENU);
        //        int result = 0;
        //
        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            result = db.Execute("sp_Fatura", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return result > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        public string FaturaOdemeCevapKontrol(JObject j)
        {
            try
            {

                j.Add("ISLEM", (int)sp_Fatura.FaturaOdemeCevapKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Fatura", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;

                
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
    }
}