using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaTanimlamaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaTanimlama;

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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
