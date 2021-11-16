using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineDers
{
    [GzipCompression]
    public class OnlineDersProgramiSinifController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineDersProgramiSinif;

        public Object SubeListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DFiltre.ID_MENU = ID_MENU;
                        return c.DFiltre.SubeListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DFiltre.ID_MENU = ID_MENU;
                        return c.DFiltre.Kademe3Listele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TarihListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.TarihListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineDersProgramiListeleSinif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.OnlineDersProgramiListeleSinif(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
