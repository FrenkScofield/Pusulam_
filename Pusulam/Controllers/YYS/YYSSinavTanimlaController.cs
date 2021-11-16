using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YYS
{
    [GzipCompression]
    public class YYSSinavTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YYSSinavTanimla;

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

        public Object SinavTanimla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavTanimla(j);
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
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavListele(j);
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
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavDuzenle(j);
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
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
