using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.IdariIsler;
using PusulamBusiness.Ortak;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.IdariIsler.KullaniciYetkiIslemleri
{
    //[GzipCompression]
    public class KullaniciYetkiIslemleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KullaniciYetkiIslemleri;

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.KullaniciTipiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Listele(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.OgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Giris(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.Giris(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object YetkiKaldir(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.YetkiKaldir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object BransListele(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.BransListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SifreSifirla(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetkiIslemleri> c = new Channel2<DKullaniciYetkiIslemleri>(ID_MENU))
                {
                    return c._cs.SifreSifirla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
