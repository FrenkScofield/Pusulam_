using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yetkilendirme
{
    [GzipCompression]
    public class BirimYetkilendirmeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.BirimYetkilendirme;
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

        public Object MenuListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMenu.ID_MENU = ID_MENU;
                    return c.DMenu.MenuListelebyYetki(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BirimMenuKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBirimYetki.ID_MENU = ID_MENU;
                    return c.DBirimYetki.BirimMenuKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
