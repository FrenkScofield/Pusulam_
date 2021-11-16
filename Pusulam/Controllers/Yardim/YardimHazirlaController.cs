using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yardim
{
    [GzipCompression]
    public class YardimHazirlaController : ApiController
    {

        internal int ID_MENU = (int)EMenu.YardimHazirla;
        public Object MenuListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYardimHazirla.ID_MENU = ID_MENU;
                    return c.DYardimHazirla.MenuListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YardimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYardimHazirla.ID_MENU = ID_MENU;
                    return c.DYardimHazirla.YardimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YardimKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYardimHazirla.ID_MENU = ID_MENU;
                    return c.DYardimHazirla.YardimKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
