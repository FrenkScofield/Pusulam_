using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav
{
    [GzipCompression]
    public class OnlineSinavOnizlemeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineSinavOnizleme;

        public Object OnlineSinavOnizlemeSoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavOnizlemeSoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
