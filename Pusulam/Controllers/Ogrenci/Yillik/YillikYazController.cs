using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci.Yillik
{
    [GzipCompression]
    public class YillikYazController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YillikYaz;

        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYillik.ID_MENU = ID_MENU;
                    return c.DYillik.OgrenciListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YillikYaziKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYillik.ID_MENU = ID_MENU;
                    return c.DYillik.YillikYaziKaydet(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YillikYaziSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYillik.ID_MENU = ID_MENU;
                    return c.DYillik.YillikYaziSil(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
