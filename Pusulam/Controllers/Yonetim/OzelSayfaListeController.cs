using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    [GzipCompression]
    public class OzelSayfaListeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OzelSayfaListe;

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
               
        public Object OzelSayfaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.OzelSayfaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        public Object OS_AktifPasif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.OS_AktifPasif(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
               
        public Object OS_Sil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOzelSayfa.ID_MENU = ID_MENU;
                    return c.DOzelSayfa.OS_Sil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
