using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav
{
    [GzipCompression]
    public class CevapAnahtariKullaniciYetkiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.CevapAnahtariKullaniciYetki;
        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBirimYetki.ID_MENU = ID_MENU;
                    return c.DBirimYetki.KademeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGrupListele(j);
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
                using (Channel c = new Channel())
                {
                    c.DBirimYetki.ID_MENU = ID_MENU;
                    return c.DBirimYetki.KullaniciTipiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullaniciYetki.ID_MENU = ID_MENU;
                    return c.DKullaniciYetki.KullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.CevapAnahtari.ID_MENU = ID_MENU;
                    return c.CevapAnahtari.DersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciYetkiKaydet(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.CevapAnahtari.ID_MENU = ID_MENU;
                        return c.CevapAnahtari.KullaniciYetkiKaydet(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
