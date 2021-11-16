using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZekaTest
{
    [GzipCompression]
    public class ZekaTestSonucListeleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.TabanPuanlar;

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object SonucListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.SonucListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DZekaTest.ID_MENU = ID_MENU;
                        return c.DZekaTest.SoruListeleListeSayfasi(j);
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
