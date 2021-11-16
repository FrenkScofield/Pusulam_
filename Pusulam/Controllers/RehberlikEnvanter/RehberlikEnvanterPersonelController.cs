using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Http;


namespace Pusulam.Controllers.RehberlikEnvanter
{
    [GzipCompression]
    public class RehberlikEnvanterPersonelController : ApiController
    {
        internal int ID_MENU = (int)EMenu.RehberlikEnvanterPersonel;
        public Object RehberlikEnvanterleriGetirSinif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikEnvanter.ID_MENU = ID_MENU;
                    return c.DRehberlikEnvanter.RehberlikEnvanterleriGetirSinif(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object RehberlikEnvanterleriGetirSubeKademe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikEnvanter.ID_MENU = ID_MENU;
                    return c.DRehberlikEnvanter.RehberlikEnvanterleriGetirSubeKademe(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object RehberlikEnvanterTestListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikEnvanter.ID_MENU = ID_MENU;
                    return c.DRehberlikEnvanter.RehberlikEnvanterTestListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3ListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object KullaniciTipiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = ID_MENU;
                    return c.DKullanici.KullaniciTipiGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object pdfKontrol(JObject j)
        {
            List<string> result = new List<string> ();
            JArray k = new JArray();
            
            foreach (string klasor in j.SelectToken("KLASORLISTE").ToObject<List<string>>())
            {
                string filePath = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\" + klasor.Replace(@"/", @"\"));                
                
                foreach (string tc in j.SelectToken("OGRLISTE").ToObject<List<string>>())
                {

                    if (File.Exists(filePath + @"\" + tc + ".pdf"))
                    {
                        result.Add(tc);
                        JObject a = new JObject();
                        a.Add("TCKIMLIKNO", tc.ToString());
                        a.Add("KLASOR", klasor.ToString());

                        k.Add(a);
                        //k.Add("TCKIMLIKNO", tc.ToString());
                        //k.Add("KLASOR", klasor.ToString());
                    }
                }
            }

            string json = JsonConvert.SerializeObject(k);
            return json;
        }

        public string TopluIndir(JObject j)
        {
            try
            {
                string OTURUM = j.SelectToken("OTURUM").ToString();
                string yolOturum = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\Temp\" + OTURUM);
                string yolTemp = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\Temp\");

                try
                {
                    Directory.Delete(yolOturum, true);
                    File.Delete(yolTemp + OTURUM + ".zip");
                }
                catch (Exception)
                {
                }

                foreach (string klasor in j.SelectToken("KLASORLISTE").ToObject<List<string>>())
                {
                    string filePath = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\" + klasor.Replace(@"/", @"\"));
                    string copyPath = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\Temp\" + OTURUM + @"\" + klasor.Replace(@"/", @"\"));
                    Directory.CreateDirectory(copyPath);

                    foreach (string tc in j.SelectToken("OGRLISTE").ToObject<List<string>>())
                    {
                        try
                        {
                            File.Copy(filePath + @"\" + tc + ".pdf", copyPath + @"\" + tc + ".pdf");
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                string zipFile = yolTemp + OTURUM + ".zip";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddItem(yolOturum);
                    zip.Save(zipFile);
                }
                Directory.Delete(yolOturum, true);
                return yolOturum;
            }
            catch (Exception)
            {
                return "";
                throw;
            }

        }

        public bool ZipSil(JObject j)
        {

            try
            {
                string OTURUM = j.SelectToken("OTURUM").ToString();
                string yolTemp = HttpContext.Current.Server.MapPath(@"~\img\RehberlikEnvanter_Raporlar\Temp\");
                File.Delete(yolTemp + OTURUM + ".zip");

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

    }
}
