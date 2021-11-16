using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Ortak;
using PusulamBusiness.Enums;
using PusulamBusiness.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using PusulamBusiness.Utiliy;

namespace PusulamBusiness.Veli
{
    public class DOgrenciHarcama : DBase
    {
        GetIp getIp = new GetIp();
        public string ParaYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciHarcama.ParaYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHarcama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json == null ? "[]" : json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool ParaYukleCevap(JObject j)
        {
            try
            {

                JObject jsonRequest = new JObject();
                jsonRequest.Add("ISLEM", (int)sp_OgrenciHarcama.ParaYukleCevap);
                jsonRequest.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                jsonRequest.Add("SQLJSON", JsonConvert.SerializeObject(j));

                int result = 0;

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.Execute("sp_OgrenciHarcama", jsonRequest.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        //public bool ParaYukleCevapSet(JObject j)
        //{
        //    try
        //    {
        //        j.Add("ISLEM", (int)sp_OgrenciHarcama.ParaYukleCevapSet);
        //        j.Add("ID_MENU", ID_MENU);
        //        int result = 0;

        //        using (IDbConnection db = new SqlConnection(conStr))
        //        {
        //            if (db.State == ConnectionState.Closed) db.Open();
        //            result = db.Execute("sp_OgrenciHarcama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //        }
        //        return result > 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        new DHataLog().HataLogKaydet(j, ex);
        //        throw ex;
        //    }
        //}

        public string ParaYukleCevapKontrol(JObject j)
        {
            try
            {

                j.Add("ISLEM", (int)sp_OgrenciHarcama.ParaYukleCevapKontrol);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciHarcama", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
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