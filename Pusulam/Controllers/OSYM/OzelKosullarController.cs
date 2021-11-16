using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OSYM
{
    [GzipCompression]
    public class OzelKosullarController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OzelKosullar;

        public Object OsymOzelKosulEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.OsymOzelKosulEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
