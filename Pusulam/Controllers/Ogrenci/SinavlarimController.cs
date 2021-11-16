using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class SinavlarimController : ApiController
    {
        public object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SoruGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SoruGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SinavTuruGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SinavTuruGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GirisEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.GirisEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SinaviBitir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SinaviBitir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SoruListeleCevapli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOnlineSinav.SoruListeleCevapli(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
