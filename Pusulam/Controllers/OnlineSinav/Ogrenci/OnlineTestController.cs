using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav.Ogrenci
{
    [GzipCompression]
    public class OnlineTestController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineTest;

        public Object OnlineTestSinavlarimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineTestSinavlarimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavSoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavSoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavPerformans(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavPerformans(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
