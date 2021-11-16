using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class AbideSinavRaporHazirlikController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AbideSinavRaporHazirlik;

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

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SayfaTurListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SayfaTurListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object RaporDuzenKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.RaporDuzenKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ResimSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.ResimSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ResimEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.ResimEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
