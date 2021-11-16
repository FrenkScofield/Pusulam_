using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaCalismalarimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaCalismalarim;

        public Object Calismalarim(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaCalismalarim(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
