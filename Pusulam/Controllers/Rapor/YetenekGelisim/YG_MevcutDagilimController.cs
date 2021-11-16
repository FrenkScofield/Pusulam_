using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.YetenekGelisim
{
    [GzipCompression]
    public class YG_MevcutDagilimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_MevcutDagilim;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YG_MevcutDagilim(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_MevcutDagilim(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
