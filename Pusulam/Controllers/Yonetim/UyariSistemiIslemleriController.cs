using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    [GzipCompression]
    public class UyariSistemiIslemleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UyariSistemiIslemleri;

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

        public Object UyariEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUyari.ID_MENU = ID_MENU;
                    return c.DUyari.UyariEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object UyariDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUyari.ID_MENU = ID_MENU;
                    return c.DUyari.UyariDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
