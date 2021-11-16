using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class ViuAramaYetkisi : ApiController
    {
        public Object ViuAramaKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DViu.ViuAramaYetkiKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
