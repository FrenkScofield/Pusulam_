using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_PuanGirisiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_PuanGirisi;

        public Object YetenekGelisim(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekGelisim(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YetenekGelisimIlkSinifPuanKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekGelisimIlkSinifPuanKaydet(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YetenekGelisimUstSinifPuanKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekGelisimUstSinifPuanKaydet(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object YetenekGelisimOgrenciGecmis(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekGelisimOgrenciGecmis(j);
                }
            }
            catch (Exception)
            {
                throw;
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

        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifListelebyKullaniciDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullaniciDonem(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
