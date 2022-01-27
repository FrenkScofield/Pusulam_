using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ogrencilerim;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrencilerim
{
    public class ogrencilerimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ogrencilerim;
        public Object Ogrencilerimilistele(JObject j)
        {
            try
            {
                using (Channel2<Dogrencilerim> c = new Channel2<Dogrencilerim>(ID_MENU))
                {
                    return c._cs.Ogrencilerimilistele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
