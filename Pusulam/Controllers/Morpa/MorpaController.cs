using Newtonsoft.Json.Linq;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Morpa
{
    public class MorpaController : ApiController
    {
        public Object MorpaKullaniciListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DMorpa.MorpaKullaniciListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
