using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Morpa
{
    [GzipCompression]
    public class MorpaGirisController : ApiController
    {

        internal int ID_MENU = (int)EMenu.MorpaGiris;

        public Object MorpaLink(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaLink(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
