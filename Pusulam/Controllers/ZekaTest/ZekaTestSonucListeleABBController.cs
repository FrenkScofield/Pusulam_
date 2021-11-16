using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZekaTest
{
    [GzipCompression]
    public class ZekaTestSonucListeleABBController : ApiController
    {
        internal int ID_MENU = (int)EMenu.TabanPuanlar;

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object SonucListeleABB(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.SonucListeleABB(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruListeleABB(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DZekaTest.ID_MENU = ID_MENU;
                        return c.DZekaTest.SoruListeleListeSayfasiABB(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object PasifYapABB(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DZekaTest.ID_MENU = ID_MENU;
                        return c.DZekaTest.PasifYapABB(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object AktifYapABB(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DZekaTest.ID_MENU = ID_MENU;
                        return c.DZekaTest.AktifYapABB(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Guncelle(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DZekaTest.ID_MENU = ID_MENU;
                        return c.DZekaTest.Guncelle(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
