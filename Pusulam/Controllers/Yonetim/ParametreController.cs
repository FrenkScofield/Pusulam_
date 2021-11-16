using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    [GzipCompression]
    public class ParametreController : ApiController
    {
        internal int ID_MENU = (int)EMenu.Parametre;

        public Object ParametreListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DParametre.ID_MENU = ID_MENU;
                    return c.DParametre.ParametreListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ParametreKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DParametre.ID_MENU = ID_MENU;
                    return c.DParametre.ParametreKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
