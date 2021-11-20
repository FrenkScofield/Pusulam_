using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Tkt;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tkt
{
    public class TKTAktarimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.TktAktarim;

        public Object TktAktar(JObject j)
        {
            try
            {
                using (Channel2<DTKTAktarim> c = new Channel2<DTKTAktarim>(ID_MENU))
                {
                    return c._cs.TktAktar(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
