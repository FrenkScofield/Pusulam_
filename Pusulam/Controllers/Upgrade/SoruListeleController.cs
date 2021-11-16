using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Upgrade
{
    [GzipCompression]
    public class SoruListeleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UpgradeSoruListele;
        public Object SoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruListele(j);
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

        public Object SoruSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YasAraligiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.YasAraligiListele(j);
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

        public Object PuanAraligiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.PuanAraligiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Object SoruListelebyDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.SoruListelebyDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
