using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav.Ogrenci
{
    [GzipCompression]
    public class TestSinavController : ApiController
    {

        internal int ID_MENU = (int)EMenu.TestSinav;

        public Object OnlineTestSinavSoruListele(JObject j)
        {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOnlineSinav.ID_MENU = ID_MENU;
                        return c.DOnlineSinav.OnlineTestSinavSoruListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public object OnlineTestSinavBitir(JObject j)
        {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOnlineSinav.ID_MENU = ID_MENU;
                        return c.DOnlineSinav.OnlineTestSinavBitir(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public Object OnlineSinavSoruIsaretle(JObject j)
        {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOnlineSinav.ID_MENU = ID_MENU;
                        return c.DOnlineSinav.OnlineSinavSoruIsaretle(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public object YonergeKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.YonergeKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object YonergeOnay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.YonergeOnay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
