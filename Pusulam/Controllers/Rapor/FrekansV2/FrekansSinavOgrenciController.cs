using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.FrekansV2
{
    [GzipCompression]
    public class FrekansSinavOgrenciController : ApiController
    {
        internal int ID_MENU = (int)EMenu.FrekansSinavOgrenci;

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
            catch (Exception)
            {
                throw;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.SinavTuruListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.SinavListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public Object FrekansDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansDersListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansSinavOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.FrekansSinavOgrenciListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
