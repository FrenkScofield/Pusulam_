using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yetkilendirme
{
    public class KullaniciYetkilendirmeKaldirController : ApiController
    {

        internal int ID_MENU = (int)EMenu.KullaniciYetkilendirmeKaldir;
        public Object MenuKullaniciYetkiListele(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetki> c = new Channel2<DKullaniciYetki>(ID_MENU))
                {
                    return c._cs.MenuKullaniciYetkiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object MenuKullaniciYetkiKaldir(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetki> c = new Channel2<DKullaniciYetki>(ID_MENU))
                {
                    return c._cs.MenuKullaniciYetkiKaldir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
