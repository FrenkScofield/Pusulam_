using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rehberlik.RehberlikOnline
{
    [GzipCompression]
    public class RehberlikOnlineYukleController : ApiController
    {

        internal int ID_MENU = (int)EMenu.RehberlikOnlineYukle;

        public Object RehberlikOnlineExcelYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikOnlineYukle.ID_MENU = ID_MENU;
                    return c.DRehberlikOnlineYukle.RehberlikOnlineExcelYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object RehberlikOnlineListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikOnlineYukle.ID_MENU = ID_MENU;
                    return c.DRehberlikOnlineYukle.RehberlikOnlineListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
