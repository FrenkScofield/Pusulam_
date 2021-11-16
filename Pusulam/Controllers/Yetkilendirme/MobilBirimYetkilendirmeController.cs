using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yetkilendirme
{
    [GzipCompression]
    public class MobilBirimYetkilendirmeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MobilBirimYetkilendirme;
        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMobilBirimYetki.ID_MENU = ID_MENU;
                    return c.DMobilBirimYetki.KademeListele(j);
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
                    c.DMobilBirimYetki.ID_MENU = ID_MENU;
                    return c.DMobilBirimYetki.KullaniciTipiListele(j);
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
                    c.DMobilBirimYetki.ID_MENU = ID_MENU;
                    return c.DMobilBirimYetki.MenuListelebyYetki(j);
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
                    c.DMobilBirimYetki.ID_MENU = ID_MENU;
                    return c.DMobilBirimYetki.BirimMenuKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
