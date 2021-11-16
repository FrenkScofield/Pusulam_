using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Upgrade
{
    [GzipCompression]
    public class UpgradeIslemRaporController : ApiController
    {
        // GET: UpgradeIslemRapor
        internal int ID_MENU = (int)EMenu.UpgradeIslemRapor;
        public Object UpgradeIslemRapor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.UpgradeIslemRapor(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}