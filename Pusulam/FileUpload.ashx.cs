using PusulamBusiness.Enums;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

namespace Pusulam
{
    /// <summary>
    /// Summary description for FileUpload
    /// </summary>
    public class FileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string Tur = context.Request.Params.Get(0);
            string TCKIMLIKNO = context.Request.Params.Get(1);
            string kOturum = context.Request.Params.Get(2);
            string DosyaAd = Guid.NewGuid().ToString();
            string DosyaTip = context.Request.Files[0].ContentType;
            string Filename = context.Request.Files[0].FileName;
            string DosyaUzanti = Filename.Substring(Filename.LastIndexOf('.') + 1);
            string DosyaYol = "";
            int ID_MENU = 0;
            int ID_DOKUMANTUR = 0;

            bool sonuc = false;
            if (Tur == "TktSoru")
            {
                int ID = Convert.ToInt32(context.Request.Params.Get(3));
                ID_MENU = (int)EMenu.UpgradeSoruEkle;
                ID_DOKUMANTUR = (int)EVarlik.TktSoruResmi;
                sonuc = DokumanKaydet(TCKIMLIKNO, kOturum, ID_MENU, DosyaAd, DosyaUzanti, ID, ID_DOKUMANTUR);
                DosyaYol = "~/Dosyalar/";

                if (sonuc)
                {
                    UploadData(context, DosyaAd, DosyaYol, DosyaUzanti);
                }
            }
            else if (Tur == "ProjeDonem")
            {
                int ID = Convert.ToInt32(context.Request.Params.Get(3));
                ID_MENU = (int)EMenu.ProjeDonem;

                DateTime d = DateTime.Now;
                string guid = d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString() + d.Millisecond.ToString();
                DosyaAd = guid + "-Ekdosya";
                sonuc = DokumanKaydetProjeDonem(TCKIMLIKNO, kOturum, ID_MENU, DosyaAd, DosyaUzanti, guid, ID);
                DosyaYol = "~/Dosyalar/ProjeDonem/";

                if (sonuc)
                {
                    UploadData(context, DosyaAd, DosyaYol, DosyaUzanti);
                }
            }
            else if (Tur == "ZekaTest")
            {
                int ID = Convert.ToInt32(context.Request.Params.Get(3));
                int ID_ZEKATESTOGRENCIDOSYATUR = Convert.ToInt32(context.Request.Params.Get(4));
                ID_MENU = (int)EMenu.ZekaTestSonucGir;
                DosyaYol = "~/Dosyalar/ZekaTest/";
                sonuc = DokumanKaydetZekaTest(TCKIMLIKNO, kOturum, ID_MENU, Filename, DosyaAd, DosyaUzanti, ID_ZEKATESTOGRENCIDOSYATUR, ID);
                if (sonuc)
                {
                    UploadDataZeka(context, Filename, DosyaAd, DosyaYol, DosyaUzanti);
                }
            }
            else if (Tur == "Odev")
            {
                ID_MENU = (int)EMenu.OdevEkle;
                DosyaYol = "~/Dosyalar/Odev/";
                UploadDataZeka(context, Filename, DosyaAd, DosyaYol, DosyaUzanti);
            }
            else if (Tur == "OzelSayfa")
            {
                //DateTime d = DateTime.Now;
                //string guid = d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString() + d.Millisecond.ToString();
                DosyaAd = Filename.Substring(0, Filename.LastIndexOf('.')) + " - " + Guid.NewGuid().ToString();
                DosyaAd = DosyaAd.Length > 60 ? DosyaAd.Substring(0, 60) : DosyaAd;
                DosyaYol = "Dosyalar/OzelSayfa/";
                if (DosyaUzanti == "pdf")
                {
                    UploadData(context, DosyaAd, DosyaYol, DosyaUzanti);
                   //context.Response.Write("{\"Yol\":\"" + DosyaYol + DosyaAd + "." + DosyaUzanti + "\"}");
                }
                else
                {
                    context.Response.Write("{\"success\":false}");
                }

            }
        }

        public bool IsReusable {
            get {
                return false;
            }
        }

        public void UploadData(HttpContext context, string DosyaAd, string DosyaYol, string DosyaUzanti)
        {
            var inputStream = context.Request.Files[0].InputStream;

            var FilePath = Path.Combine(context.Server.MapPath(DosyaYol), DosyaAd + "." + DosyaUzanti);

            int length = 0;
            using (FileStream writer = new FileStream(FilePath, FileMode.Create))
            {
                int readCount;
                var buffer = new byte[8192];
                while ((readCount = inputStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    writer.Write(buffer, 0, readCount);
                    length += readCount;
                }
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"success\":true,\"YOL\":\"" + DosyaYol + DosyaAd + "." + DosyaUzanti + "\"}");
                return;
            }
        }

        public void UploadDataZeka(HttpContext context, string Ad, string Guid, string DosyaYol, string DosyaUzanti)
        {
            var inputStream = context.Request.Files[0].InputStream;

            var FilePath = Path.Combine(context.Server.MapPath(DosyaYol), Guid + "." + DosyaUzanti);

            int length = 0;
            using (FileStream writer = new FileStream(FilePath, FileMode.Create))
            {
                int readCount;
                var buffer = new byte[8192];
                while ((readCount = inputStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    writer.Write(buffer, 0, readCount);
                    length += readCount;
                }
                context.Response.ContentType = "application/json";
                context.Response.Write("{\"success\":true, \"ad\":\"" + Ad + "\", \"guid\":\"" + Guid + "\", \"uzanti\":\"" + DosyaUzanti + "\"}");
                return;
            }
        }

        public bool DokumanKaydet(string TCKIMLIKNO, string kOturum, int idMenu, string DosyaAd, string DosyaUzanti, int ID, int idDokumanTur)
        {
            try
            {
                int sonuc = 0;
                bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
                var conString = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

                if (isTest)
                {
                    conString = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
                }

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Dokuman";
                    cmd.Parameters.AddWithValue("@TCKIMLIKNO", TCKIMLIKNO);
                    cmd.Parameters.AddWithValue("@OTURUM", kOturum);
                    cmd.Parameters.AddWithValue("@ID_MENU", idMenu);
                    cmd.Parameters.AddWithValue("@ISLEM", 1);
                    cmd.Parameters.AddWithValue("@DOSYAAD", DosyaAd);
                    cmd.Parameters.AddWithValue("@DOSYAUZANTI", DosyaUzanti);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@ID_DOKUMANTUR", idDokumanTur);
                    sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DokumanKaydetProjeDonem(string TCKIMLIKNO, string kOturum, int idMenu, string DosyaAd, string DosyaUzanti, string guid, int ID)
        {
            try
            {


                int sonuc = 0;
                bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
                var conString = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

                if (isTest)
                {
                    conString = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
                }

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ProjeDonem";
                    cmd.Parameters.AddWithValue("@TCKIMLIKNO", TCKIMLIKNO);
                    cmd.Parameters.AddWithValue("@OTURUM", kOturum);
                    cmd.Parameters.AddWithValue("@ID_MENU", idMenu);
                    cmd.Parameters.AddWithValue("@ISLEM", 16);
                    cmd.Parameters.AddWithValue("@DOSYA", DosyaAd + "." + DosyaUzanti);
                    cmd.Parameters.AddWithValue("@DOSYAGUID", guid);
                    cmd.Parameters.AddWithValue("@ID_PROJETALEP", ID);
                    sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DokumanKaydetZekaTest(string TCKIMLIKNO, string kOturum, int idMenu, string DosyaAd, string guid, string DosyaUzanti, int ID_ZEKATESTOGRENCIDOSYATUR, int ID)
        {
            try
            {
                int sonuc = 0;
                bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
                var conString = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

                if (isTest)
                {
                    conString = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
                }

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ZekaTest";
                    cmd.Parameters.AddWithValue("@TCKIMLIKNO", TCKIMLIKNO);
                    cmd.Parameters.AddWithValue("@OTURUM", kOturum);
                    cmd.Parameters.AddWithValue("@ID_MENU", idMenu);
                    cmd.Parameters.AddWithValue("@ISLEM", 6);
                    cmd.Parameters.AddWithValue("@DOSYAAD", DosyaAd);
                    cmd.Parameters.AddWithValue("@DOSYAGUID", guid);
                    cmd.Parameters.AddWithValue("@DOSYATIP", DosyaUzanti);
                    cmd.Parameters.AddWithValue("@ID_ZEKATESTOGRENCIDOSYATUR", ID_ZEKATESTOGRENCIDOSYATUR);
                    cmd.Parameters.AddWithValue("@ID_ZEKATESTOGRENCIZEKATEST", ID);
                    sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DokumanKaydetOdev(string TCKIMLIKNO, string kOturum, int idMenu, string DosyaAd, string guid, string DosyaUzanti, int ID)
        {
            try
            {
                int sonuc = 0;
                bool isTest = ConfigurationManager.AppSettings["IsTest"] == null ? false : Convert.ToBoolean(ConfigurationManager.AppSettings["IsTest"]);
                var conString = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;

                if (isTest)
                {
                    conString = ConfigurationManager.ConnectionStrings["pusulamTest"].ConnectionString;
                }

                using (SqlConnection con = new SqlConnection(conString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_Odev";
                    cmd.Parameters.AddWithValue("@TCKIMLIKNO", TCKIMLIKNO);
                    cmd.Parameters.AddWithValue("@OTURUM", kOturum);
                    cmd.Parameters.AddWithValue("@ID_MENU", idMenu);
                    cmd.Parameters.AddWithValue("@ISLEM", 4);
                    cmd.Parameters.AddWithValue("@AD", DosyaAd);
                    cmd.Parameters.AddWithValue("@GUID", guid);
                    cmd.Parameters.AddWithValue("@UZANTI", DosyaUzanti);
                    cmd.Parameters.AddWithValue("@ID_ODEV", ID);
                    sonuc = cmd.ExecuteNonQuery();
                    con.Close();
                }
                return sonuc > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}