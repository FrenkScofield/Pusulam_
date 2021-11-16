using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineDers
{
    [GzipCompression]
    public class OnlineDersGenelKlasorController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineDersGenelKlasor;

        public Object GenelKlasorListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.GenelKlasorListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object GenelKlasorKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.GenelKlasorKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
