using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Performans
{
    public class DegerlendirilenlerController : ApiController
    {
        internal int ID_MENU = (int)EMenu.PerformansDegerlendirilenler;

      

        public Object DegerlendirilenlerListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.DegerlendirilenlerListele(j);
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
