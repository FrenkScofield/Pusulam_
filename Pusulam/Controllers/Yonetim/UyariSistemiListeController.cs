using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    [GzipCompression]
    public class UyariSistemiListeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UyariSistemiListe;

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

        public Object UyariListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUyari.ID_MENU = ID_MENU;
                    return c.DUyari.UyariListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object UyariAktifPasif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUyari.ID_MENU = ID_MENU;
                    return c.DUyari.UyariAktifPasif(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
