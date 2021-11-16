using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class UniteTaramaOlcegiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UniteTaramaSinavTanimla;
        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavTanimla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavTanimla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OVGorsunDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.OVGorsunDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object BildirimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.BildirimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
