using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;

namespace PusulamSinav.Controllers
{
    public class MenuController : ApiController
    {
        // GET api/<controller>
        public object MenuGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMenu.ID_MENU = (int)EMenu.OnlineSinavlarim;
                    return c.DMenu.MenuGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MenuYardimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMenu.ID_MENU = (int)EMenu.OnlineSinavlarim;
                    return c.DMenu.MenuYardimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}