using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZekaTest
{
    [GzipCompression]
    public class ZekaTestSonucGirController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZekaTestSonucGir;

        public Object ZekaTestListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.ZekaTestListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciAra(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.OgrenciAra(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.SoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Sil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.Sil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZekaTest.ID_MENU = ID_MENU;
                    return c.DZekaTest.DosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
