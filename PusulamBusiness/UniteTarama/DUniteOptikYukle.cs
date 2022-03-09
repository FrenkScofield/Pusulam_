using Dapper;
using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness.Models.Sinav;
using PusulamBusiness.Ortak;
using PusulamBusiness.Utility;
using PusulamBusiness.Utiliy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace PusulamBusiness.UniteTarama
{
    public class DUniteOptikYukle : DBase
    {
        GetIp getIp = new GetIp();
        public List<MSinavTuru> SinavTuruListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavTuruListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
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

        public string OdulListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OdulListesi.OdulListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OdulListesi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json == null ? "" : json;
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavListelebyTarih(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListelebyTarih);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                json = json == null ? "" : json;
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinavTuru> SinavTuruListeleGrubaGore(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavTuruListeleGrubaGore);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
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

        public string OgrenciIzleme(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_OgrenciIzleme.OgrenciIzleme);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_OgrenciIzleme",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
        public string OptikIndir(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Sinav.OptikIndir);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Sinav",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string SinavKatilim(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_SinavKatilim.SinavKatilim);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_SinavKatilim",j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }


        public string SinifNetPuanOrtalamalari(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_SinifNetPuanOrtalamalari.SinifNetPuanOrtalamalari);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_SinifNetPuanOrtalamalari",j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public List<MSinavTuru> SinavTuruListeleTc(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Sinav.SinavTuruListeleTc);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    liste=db.Query<MSinavTuru>("sp_Sinav",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
        public List<MSinavTuru> SinavTuruListeleTcDonem(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Sinav.SinavTuruListeleTc);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavTuru> liste;
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    liste=db.Query<MSinavTuru>("sp_Sinav",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string GelisimRaporu(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_GelisimRaporu.GelisimRaporu);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_GelisimRaporu",j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }
        public string GelisimRaporuHTML(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GelisimRaporu.GelisimRaporuHtml);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GelisimRaporuOO", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string GelisimRaporLSHTML(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GelisimRaporu.GelisimRaporuHtml);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_GelisimRaporuLS", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string GelisimFiltreList(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_GelisimRaporu.GelisimFiltreList);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_GelisimRaporu",j.ToDictionary(),commandTimeout: 600,commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }


        public string SinavDersSorulariListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDersSorulariListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int SinavSoruIslem(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavListele.SinavSoruIslem);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_SinavListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavSoruIslemSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavListele.SinavSoruIslemSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinavListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public string SinavSoruIslemleriListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavListele.SinavSoruIslemleriListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinavListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SorunluTcSayilariniGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.SorunluTcSayilariniGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SoruIslemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavListele.SoruIslemListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinavListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavDersleriListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDersleriListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public string SinavTuruDersleriListele(JObject j)
        {
            try
            {
                j.Add("ISLEM",(int)sp_Sinav.SinavTuruDersleriListele);
                j.Add("ID_MENU",ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using(IDbConnection db = new SqlConnection(conStr))
                {
                    if(db.State==ConnectionState.Closed)
                        db.Open();
                    json=db.ExecuteScalar<string>("sp_Sinav",j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json.Replace("\"selected\":1","\"selected\":true");
            }
            catch(Exception ex)
            {
                new DHataLog().HataLogKaydet(j,ex);
                throw ex;
            }
        }

        public string OptikDosyaGor(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.OptikDosyaGor);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_OptikYukle", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinavGrup> SinavGrupListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavGrupListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavGrup> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavGrup>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        
        public int SinavKopyala(JObject j) {
            try {
                j.Add("ISLEM", (int)sp_SinavListele.SinavKopyala);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr)) {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_SinavListele", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex) {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OgrenciListelebySinav(JObject j) {
            try {
                j.Add("ISLEM", (int)sp_Sinav.OgrenciListelebySinav);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr)) {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex) {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string OptikDosyaIcerik(JObject j) {
            try {
                j.Add("ISLEM", (int)sp_Sinav.OptikDosyaIcerik);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr)) {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex) {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int OptikDosyaIcerikDuzenle(JObject j) {
            try {
                j.Add("ISLEM", (int)sp_Sinav.OptikDosyaIcerikDuzenle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr)) {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex) {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }        

        public int SinavAktifPasifYap(JObject j) {
            try {
                j.Add("ISLEM", (int)sp_Sinav.SinavAktifPasifYap);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr)) {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex) {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public int SinavDurumlariDegistir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDurumlariDegistir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavDersListele(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDersListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int SinavTanimla(JObject j) 
        {
            try
            {
              
                j.Add("ISLEM", (int)sp_Sinav.SinavTanimla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public int YonergeSinavKaydet()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string ID_SINAV = (HttpContext.Current.Request.Form["ID_SINAV"] != null) ? HttpContext.Current.Request.Form["ID_SINAV"].ToString() : "";
                string SQLJSON = (HttpContext.Current.Request.Form["SQLJSON"] != null) ? HttpContext.Current.Request.Form["SQLJSON"].ToString() : "";
                var AD = String.Empty;
                var UZANTI = String.Empty;
                string GUID = "";

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[i] : null;

                    if (DOSYAGUID == "" && file != null)
                    {
                        if (file != null)
                        {
                            AD = file.FileName != null ? file.FileName : "";
                            UZANTI = AD.Split('.').Last();
                            GUID = Guid.NewGuid().ToString() + "." + UZANTI;

                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            AmazonDosyaYukle.sendMyFileToS3(@"pusulam/onlinesinav/yonerge", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                int sonuc = 0;
                var j = new JObject();
                j.Add("ISLEM", (int)sp_Sinav.SinavTanimla);
                j.Add("ID_MENU", ID_MENU);
                j.Add("SQLJSON", SQLJSON);
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("GUID", GUID);
                j.Add("ID_SINAV", ID_SINAV);
                j.Add("OTURUM",OTURUM);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                //new DHataLog()
                throw ex;
            }
        }

        public int YonergeSinavGuncelle()
        {
            try
            {
                string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
                string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
                string DOSYAGUID = (HttpContext.Current.Request.Form["DOSYAGUID"] != null) ? HttpContext.Current.Request.Form["DOSYAGUID"].ToString() : "";
                string ID_SINAV = (HttpContext.Current.Request.Form["ID_SINAV"] != null) ? HttpContext.Current.Request.Form["ID_SINAV"].ToString() : "";
                string SQLJSON = (HttpContext.Current.Request.Form["SQLJSON"] != null) ? HttpContext.Current.Request.Form["SQLJSON"].ToString() : "";
                var AD = String.Empty;
                var UZANTI = String.Empty;
                string GUID = "";

                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[i] : null;

                    if (DOSYAGUID == "" && file != null)
                    {
                        if (file != null)
                        {
                            AD = file.FileName != null ? file.FileName : "";
                            UZANTI = AD.Split('.').Last();
                            GUID = Guid.NewGuid().ToString() + "." + UZANTI;

                            MemoryStream fileToUpload = new MemoryStream();
                            file.InputStream.CopyTo(fileToUpload); // Amazon S3 İçin
                            file.InputStream.Position = 0;
                            AmazonDosyaYukle.sendMyFileToS3(@"pusulam/onlinesinav/yonerge", GUID.ToString(), fileToUpload, file.ContentType, TCKIMLIKNO);
                        }
                    }
                }
                int sonuc = 0;
                var j = new JObject();
                j.Add("ISLEM", (int)sp_Sinav.SinavGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("SQLJSON", SQLJSON);
                j.Add("TCKIMLIKNO", TCKIMLIKNO);
                j.Add("GUID", GUID);
                j.Add("ID_SINAV", ID_SINAV);
                j.Add("OTURUM", OTURUM);
                j.Add("IP", getIp.GetUser_IP());

                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                //new DHataLog()
                throw ex;
            }
        }
        public List<MDonem> DonemListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.DonemListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDonem> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDonem>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinav> SinavListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> SinavListeleMultiTur(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListeleMultiTur);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> SinavListeleKademeDonem(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListeleKademeDonem);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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
        public string UniteSinavListeleKademeDonem(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.UniteSinavListeleKademeDonem);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

               string json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinav> SinavListeleDegerlendirilmis(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListeleDegerlendirilmis);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> SinavListelePasifDahil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListelePasifDahil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public String SinavBilgiGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavBilgiGetir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool YonergeSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.YonergeSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_Sinav", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }
        public bool SinavGuncelle(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_Sinav", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public bool CATaslakYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.CATaslakYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_Sinav", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MDersUnite> DersKonuListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.DersKonuListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDersUnite> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDersUnite>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String KatsayiListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.KatsayiListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int KatsayiKaydet(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.KatsayiKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MDosya> DosyaListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.DosyaListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MDosya> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MDosya>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DosyaSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.DosyaSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_Sinav", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool SinavDegerlendir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDegerlendir);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinavPuanTuru> PuanTuruListebyOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.PuanTuruListebyOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinavPuanTuru> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinavPuanTuru>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }


        public String SinavSonuclariGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavIstatistik.Istatistik);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_SinavSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String AraKarneDeneme(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_AraKarne.AraKarneDeneme);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_AraKarne", j.ToDictionary(), commandTimeout:600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String AraKarneYazili(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_AraKarne.AraKarneYazili);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    json = db.ExecuteScalar<string>("sp_AraKarne", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String KonuAnaliziGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_KonuAnalizi.KonuAnaliz);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_KonuAnalizi", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String YaziliYoklamaSonuclariGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaSonuclari);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinav> DenemeSinavSonucListeGetir(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_OgrenciRaporlari.OgrenciRaporlari);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinav> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinav>("sp_OgrenciRaporlari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
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
                j.Add("ISLEM", (int)sp_Sinav.SinavListelebyOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> SinavListelebyGrup(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListelebyGrup);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> SinavListelebyGrupCoklu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavListelebyGrupCoklu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

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

        public List<MSinav> YaziliYoklamaSinavListesibyOgrenci(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaSinavListesibyOgrenci);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinav> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinav>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public List<MSinav> YaziliYoklamaSinavListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaSinavListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                List<MSinav> liste;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    liste = db.Query<MSinav>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure).ToList();
                }
                return liste;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String GenelSinavRaporu(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_GenelSinavRaporu.GenelSinavRaporu);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_GenelSinavRaporu", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String YaziliYoklamaSonuclariGetirGenel(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaSonuclariGetirGenel);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String YaziliYoklamaSonuclariGetirGenelYeni(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaSonuclariGetirGenelYeni);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public String UniteAra(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.UniteAra);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                String json;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool TcGuncelle(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.TcGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_OptikYukle", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OptikSatirSil(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_OptikYukle.OptikSatirSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    IDictionary dic = (IDictionary)j.ToDictionary();
                    sonuc = db.Execute("sp_OptikYukle", dic, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool OgrenciSil(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.OgrenciSil);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool DersAdiGuncelle(JObject j) 
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.DersAdiGuncelle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int HariciPuanSiraTaslakYukle(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_SinavHariciPuanSira.HariciPuanSiraTaslakYukle);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed)
                        db.Open();
                    sonuc = db.ExecuteScalar<int>("sp_SinavHariciPuanSira", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string BurslulukDosyaListe(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.BurslulukDosyaListe);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json != null ? json.Replace("\"selected\":1", "\"selected\":true"):"";
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string YaziliYoklamaOgrenciKOSListesi(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_YaziliYoklamaSonuclari.YaziliYoklamaOgrenciKOSListesi);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_YaziliYoklamaSonuclari", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string TytSecimTurListele(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.TytSecimTurListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public bool TytSecimKriterKaydet(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.TytSecimKriterKaydet);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int sonuc = 0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    sonuc = db.Execute("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return sonuc > 0;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SoruListesiGetirSB(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SoruListesiGetirSB);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json = json.Substring(1, json.Length - 2).Replace("\"data\":null", "\"data\":\"[]\""); ;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SoruBankasiAra(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SoruBankasiAra);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public int SinavDuzenleYetki(JObject j)
        {
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDuzenleYetki);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                int result =0;
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    result = db.ExecuteScalar<int>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return result;
            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
        }

        public string SinavDegerlendirLog(JObject j)
        {

            try
            {
                j.Add("ISLEM", (int)sp_Sinav.SinavDegerlendirmeLog);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }
            
        }
        public string UniteTaramaSinavListese(JObject j)
        {

            
            try
            {
                j.Add("ISLEM", (int)sp_Sinav.UniteTaramaSinavListele);
                j.Add("ID_MENU", ID_MENU);
                j.Add("IP", getIp.GetUser_IP());

                string json = "";
                using (IDbConnection db = new SqlConnection(conStr))
                {
                    if (db.State == ConnectionState.Closed) db.Open();
                    json = db.ExecuteScalar<string>("sp_Sinav", j.ToDictionary(), commandTimeout: 600, commandType: CommandType.StoredProcedure);
                }
                return json;

            }
            catch (Exception ex)
            {
                new DHataLog().HataLogKaydet(j, ex);
                throw ex;
            }

        }



    }
}