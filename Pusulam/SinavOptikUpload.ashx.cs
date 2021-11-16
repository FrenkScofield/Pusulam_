using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Pusulam
{
    /// <summary>
    /// Summary description for SinavDosyaUpload
    /// </summary>

    public class Optik
    {
        public int b1 { get; set; }
        public int b2 { get; set; }
        public int k1 { get; set; }
        public int k2 { get; set; }
    }
    public class SinavDosyaUpload : IHttpHandler
    {
        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string DosyaAd = Guid.NewGuid().ToString();
            string DosyaTip = context.Request.Files[0].ContentType;
            string Filename = context.Request.Files[0].FileName;


            string DosyaUzanti = Filename.Substring(Filename.LastIndexOf('.') + 1);
            string DosyaYol = "~/Dosyalar/SinavOptik/";

            if (context.Request.Params.Get("tur") == "YYS")
                DosyaYol += "YYS/";

            string optik = context.Request.Params.Get(0);

            if (!optik.Equals(""))
            {
                List<Optik> optikdata = new JavaScriptSerializer().Deserialize<List<Optik>>(optik);                
                bool success = DosyaDuzelt(context, DosyaAd, DosyaYol, DosyaUzanti, optikdata);
                context.Response.ContentType = "application/json";
                if (!success)
                {
                    context.Response.Write("{\"success\": false}");
                }
                else
                {
                    context.Response.Write("{\"success\": true, \"guid\": \"" + DosyaAd + "\"}");
                }
            }
            else
            {
                bool success = UploadData(context, DosyaAd, DosyaYol, DosyaUzanti);
                context.Response.ContentType = "application/json";
                if (!success)
                {
                    context.Response.Write("{\"success\": false}");
                }
                else
                {
                    context.Response.Write("{\"success\": true, \"guid\": \"" + DosyaAd + "\"}");
                }
            }
        }

        public bool DosyaDuzelt(HttpContext context, string DosyaAd, string DosyaYol, string DosyaUzanti, List<Optik> optikdata)
        {
            var inputStream = context.Request.Files[0].InputStream;
            StreamReader sr = new StreamReader(inputStream, Encoding.GetEncoding("windows-1254"));
            using (StreamWriter sw = File.AppendText(context.Server.MapPath(DosyaYol) + DosyaAd + "." + DosyaUzanti))
            {
                try
                {
                    string satir = sr.ReadLine();
                    while (satir != null)
                    {
                        StringBuilder yeni = new StringBuilder(Bos(2048));
                        for (int i = 0; i < optikdata.Count; i++)
                        {
                            int b1 = optikdata[i].b1 - 1;
                            int b2 = optikdata[i].b2 - 1;
                            int k1 = optikdata[i].k1;
                            int k2 = optikdata[i].k2;
                            b1 = b1 < 0 ? 0 : b1;
                            b2 = b2 < 0 ? 0 : b2;
                            String data = satir.Substring(b2, k2);
                            yeni.Remove(b1, k1);
                            yeni.Insert(b1, data);
                        }
                        sw.WriteLine(yeni.ToString().Substring(0, satir.Length));
                        satir = sr.ReadLine();
                    }
                    sw.Close();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }

        public String Bos(int Count)
        {
            String bos = "";
            for (int i = 0; i < Count; i++)
            {
                bos += " ";
            }
            return bos;
        }

        public bool UploadData(HttpContext context, string DosyaAd, string DosyaYol, string DosyaUzanti)
        {
            var inputStream = context.Request.Files[0].InputStream;
            var FilePath = Path.Combine(context.Server.MapPath(DosyaYol), DosyaAd + "." + DosyaUzanti);

            int length = 0;
            using (FileStream writer = new FileStream(FilePath, FileMode.Create))
            {
                try
                {
                    int readCount;
                    var buffer = new byte[8192];
                    while ((readCount = inputStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        writer.Write(buffer, 0, readCount);
                        length += readCount;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}