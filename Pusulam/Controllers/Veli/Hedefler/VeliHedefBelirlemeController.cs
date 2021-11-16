using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.Hedefler
{
    [GzipCompression]
    public class VeliHedefBelirlemeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.Hedefler;
        public object HedefListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.VeliHedefListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
