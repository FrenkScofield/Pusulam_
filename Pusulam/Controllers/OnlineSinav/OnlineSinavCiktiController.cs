using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav
{
    [GzipCompression]
    public class OnlineSinavCiktiController : ApiController
    {

        internal int ID_MENU = (int)EMenu.OnlineSinavCikti;

        public Object OnlineSinavCikti(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavCikti(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
