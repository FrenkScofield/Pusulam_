using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.YetenekGelisim
{
    [GzipCompression]
    public class VeliYGKarneController : ApiController
    {

        internal int ID_MENU = (int)EMenu.VeliYGKarne;

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

        public Object OgrenciKarneListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.OgrenciKarneListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
