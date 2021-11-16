using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Upgrade
{
    [GzipCompression]
    public class SorularinCozulmeOranlariController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UpgradeSoruListele;
        public Object SoruCozulmeOraniListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruCozulmeOraniListele(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
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

        public Object KategoriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.KategoriListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
