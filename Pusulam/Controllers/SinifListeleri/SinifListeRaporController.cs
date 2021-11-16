using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Yazili
{
    [GzipCompression]
    public class SinifListeRaporController : ApiController
    {
        internal int ID_MENU = (int)EMenu.SinifListeRaporu;

        public Object SinifListeRaporu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinifListeRaporu.ID_MENU = ID_MENU;
                    return c.DSinifListeRaporu.SinifListeRaporu(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListeRaporuFotografli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinifListeRaporu.ID_MENU = ID_MENU;
                    return c.DSinifListeRaporu.SinifListeFotografli(j);
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
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
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
