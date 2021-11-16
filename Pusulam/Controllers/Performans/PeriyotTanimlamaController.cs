using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Performans
{
    public class PeriyotTanimlamaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.PerformansTanimlama;

      

        public Object PeriyotListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PeriyotListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotTanimla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PeriyotTanimla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PeriyotDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object PeriyotSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PeriyotSil(j);
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
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
    }
}
