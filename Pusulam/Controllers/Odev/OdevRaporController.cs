using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class OdevRaporController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OdevRapor;

        public Object SubeListele(JObject j)
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

        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KademeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe3Listele(JObject j)
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

        public Object OdevTurListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OdevTurListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.DersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.OgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
