using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.IdariIsler;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.IdariIsler.SubeYetki
{
    //[GzipCompression]
    public class SubeYetkiController : ApiController
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
                using (Channel2<DSubeYetki> c = new Channel2<DSubeYetki>(ID_MENU))
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
        public Object KademeGetir(JObject j)
        {
            try
            {
                using (Channel2<DSubeYetki> c = new Channel2<DSubeYetki>(ID_MENU))
                {
                    return c._cs.KademeGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KademeKaydet(JObject j)
        {
            try
            {
                using (Channel2<DSubeYetki> c = new Channel2<DSubeYetki>(ID_MENU))
                {
                    return c._cs.KademeKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KullaniciTipiSubeGetir(JObject j)
        {
            try
            {
                using (Channel2<DSubeYetki> c = new Channel2<DSubeYetki>(ID_MENU))
                {
                    return c._cs.KullaniciTipiSubeGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KullaniciTipiSubeKaydet(JObject j)
        {
            try
            {
                using (Channel2<DSubeYetki> c = new Channel2<DSubeYetki>(ID_MENU))
                {
                    return c._cs.KullaniciTipiSubeKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
