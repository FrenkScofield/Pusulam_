using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Configuration;
using System.Web.Http;

namespace Pusulam.Controllers
{
    public class AnasayfaController : ApiController
    {
        public Object KullaniciYetkiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullaniciYetki.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DKullaniciYetki.KullaniciYetkiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciYetkiGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullaniciYetki.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DKullaniciYetki.KullaniciYetkiGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciSubeSinifGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullaniciYetki.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DKullaniciYetki.KullaniciSubeSinifGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AnasayfaDuyuruListeGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DAnasayfa.AnasayfaDuyuruListeGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AnasayfaFaturaListeGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DAnasayfa.AnasayfaFaturaListeGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DKullanici.KullaniciTipiGetir(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //  Web Config
        public bool IsTest()
        {
            var result = ConfigurationManager.AppSettings["IsTest"];
            return result == null ? false : Convert.ToBoolean(result);
        }

        public Object SifremiDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DKullanici.SifremiDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
