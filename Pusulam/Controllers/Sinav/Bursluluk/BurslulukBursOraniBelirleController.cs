using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav.Bursluluk
{
    [GzipCompression]
    public class BurslulukBursOraniBelirleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.BurslulukBursOraniBelirle;

        public Object BursOranListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BursOranListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukSinavBursOranKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BurslulukSinavBursOranKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDersleriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDersleriListele(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListeleKademeDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTuruListele(j);
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
    }
}
