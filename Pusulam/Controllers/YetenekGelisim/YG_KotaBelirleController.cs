using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_KotaBelirleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_KotaBelirle;

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
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
        
        public Object YG_KategoriKotaGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_KategoriKotaGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public Object YG_KotaBelirle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_KotaBelirle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object YetenekGelisimDersListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekGelisimDersListe(j);
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YG_OgrenciBosalt(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YG_OgrenciBosalt(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
