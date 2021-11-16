using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_YorumEkleController : ApiController
    {

        internal int ID_MENU = (int)EMenu.YG_YorumEkle;

        public Object YorumListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YorumListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object YorumDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YorumDuzenle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
