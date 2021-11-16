using Newtonsoft.Json.Linq;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    //[GzipCompression]
    public class OdevMorpaController : ApiController
    {
        public Object OdevListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DOdev.MorpaOdevListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
