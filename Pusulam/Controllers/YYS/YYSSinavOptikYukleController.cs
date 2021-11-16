using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YYS
{
    [GzipCompression]
    public class YYSSinavOptikYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YYSSinavOptikYukle;
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

        public Object OptikYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.OptikYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavDosyaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYys.ID_MENU = ID_MENU;
                    return c.DYys.SinavDosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
