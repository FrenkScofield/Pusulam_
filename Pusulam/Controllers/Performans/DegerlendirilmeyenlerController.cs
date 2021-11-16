using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Performans
{
    public class DegerlendirilmeyenlerController : ApiController
    {
        internal int ID_MENU = (int)EMenu.PerformansDegerlendirilmeyenler;

      

        public Object DegerlendirilmeyenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.DegerlendirilmeyenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotTarihListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.TumPeriyotTarihListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
