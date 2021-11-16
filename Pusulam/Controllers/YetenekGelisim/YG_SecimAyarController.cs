using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_SecimAyarController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_SecimAyar;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object TarihAyarla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.TarihAyarla(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Object SecimTarihGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.SecimTarihGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciAktar(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.OgrenciAktar(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
