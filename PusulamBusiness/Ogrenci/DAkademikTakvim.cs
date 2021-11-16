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

namespace PusulamBusiness.Ogrenci
{
    public class DAkademikTakvim : DBase
    {
        GetIp getIp = new GetIp();
        public String Takvim_EtkinlikListOgrenci(JObject j)
        {
            //try
            //{
            //    List<MTakvim> liste = new List<MTakvim>();
            //    j.Add("ISLEM", (int)sp_OgrenciTakvim.TakvimGetir);
            //    j.Add("ID_MENU", ID_MENU);
            //    using (IDbConnection db = new SqlConnection(conStr))
            //    {
            //        if (db.State == ConnectionState.Closed) db.Open();
            //        liste = db.Query<MTakvim>("sp_OgrenciTakvim", j.ToDictionary(), commandType: CommandType.StoredProcedure).ToList();
            //    }
            //    return liste;
            //}
            //catch (Exception ex)
            //{
            //    new DHataLog().HataLogKaydet(j, ex);
            //    throw ex;
            //}
            
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciTakvim.TakvimGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OgrenciTakvim", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        //List<MTakvim> l = new List<MTakvim>();
        //using (Baglanti b = new Baglanti())
        //{
        //    using (DataTable dt = b.SorguGetir("eokul_v2.dbo.spTakvim_TakvimListPersonel", System.Data.CommandType.StoredProcedure))
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            l.Add(new MTakvim()
        //            {
        //                id = Convert.ToInt32(dr["ID_ETKINLIK"])
        //                ,
        //                grup = dr["GRUP"].ToString()
        //                ,
        //                etkinlik = dr["ETKINLIK"].ToString()
        //                ,
        //                aciklama = dr["ACIKLAMA"].ToString()
        //                ,
        //                bastarih = dr["BASTARIH"].ToString()
        //                ,
        //                bittarih = dr["BITTARIH"].ToString()
        //                 ,
        //                uzunluk = Convert.ToInt32(dr["UZUNLUK"])
        //                ,
        //                renk = dr["RENK"].ToString()
        //                ,
        //                tip = dr["TIP"].ToString()
        //                  ,
        //                id_kullanici = Convert.ToInt32(dr["ID_KULLANICI"])
        //                ,
        //                kullanicitipi = dr["KullaniciTipi"].ToString()
        //            });
        //        }
        //    }
        //}
        //return l;
    }
    }
}