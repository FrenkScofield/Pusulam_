using System;
using PusulamBusiness;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace PusulamSinav.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public Object Login(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DLogin.Login(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}