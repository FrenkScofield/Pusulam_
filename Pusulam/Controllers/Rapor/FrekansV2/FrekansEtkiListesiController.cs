using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.FrekansV2
{
    [GzipCompression]
    public class FrekansEtkiListesiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.FrekansEtkiListesi;

        public Object Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansEtkiListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object FrekansDonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansDonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object FrekansSinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansSinavTuruListele(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
