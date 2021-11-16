using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class AkademikTakvimController : ApiController
    {
        public object Takvim_EtkinlikListOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAkademikTakvim.ID_MENU = (int)EMenu.AkademikTakvim;
                    return c.DAkademikTakvim.Takvim_EtkinlikListOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
