using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;
namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_YetenekAciklamaEkleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_YetenekAciklamaEkle;

        public Object YG_KategoriListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_KategoriListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YG_YetenekAciklamaEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_YetenekAciklamaEkle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YG_KategoriAciklamaGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_KategoriAciklamaGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
