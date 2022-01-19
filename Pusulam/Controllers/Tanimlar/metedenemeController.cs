using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Tanimlar;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tanimlar
{
    public class metedenemeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.metedeneme;
        public Object Listele(JObject j)
        {
            try
            {
                using (Channel2<Dmetedeneme> c = new Channel2<Dmetedeneme>(ID_MENU))
                {
                    return c._cs.Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
