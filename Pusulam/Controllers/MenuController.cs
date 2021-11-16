using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;
namespace Pusulam.Controllers
{
    [GzipCompression]
    public class MenuController : ApiController
    {
        public object MenuGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMenu.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DMenu.MenuGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MenuYardimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMenu.ID_MENU = (int)EMenu.Anasayfa;
                    return c.DMenu.MenuYardimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
