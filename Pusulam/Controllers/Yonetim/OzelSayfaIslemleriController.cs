using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    [GzipCompression]
    public class OzelSayfaIslemleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OzelSayfaIslemleri;

        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KademeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KullaniciTipiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SayfaTuruListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.SayfaTuruListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OzelSayfaEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.OzelSayfaEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OzelSayfaDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.OzelSayfaDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
