using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.BildirimApi;
using System;
using System.Web.Http;

namespace PusulamAPI.Controllers
{
    public class BildirimApiController : ApiController
    {
        public Object VeliBildirimGonder(JObject j)
        {
            try
            {
                using (Channel2<DBildirimApi> c = new Channel2<DBildirimApi>(1))
                {
                    return c._cs.OgrenciVeliBildirim(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
