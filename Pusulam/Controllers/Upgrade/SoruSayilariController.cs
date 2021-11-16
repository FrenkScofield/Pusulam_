using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Upgrade
{
    [GzipCompression]
    public class SoruSayilariController : ApiController
    {
        internal int ID_MENU = (int)EMenu.SoruSayilari;
        public Object SoruSayilari(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruSayilari(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object GrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.GrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
