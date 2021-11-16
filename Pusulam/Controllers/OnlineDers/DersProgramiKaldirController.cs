using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineDers
{
    [GzipCompression]
    public class DersProgramiKaldirController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineDersDersProgramiGiris;

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

        public Object OgretmenOnlineDersProgramiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.OgretmenOnlineDersProgramiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenOnlineDersProgramiKaldir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.OgretmenOnlineDersProgramiKaldir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifOnlineDersOgretmenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.SinifOnlineDersOgretmenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OgretmenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
