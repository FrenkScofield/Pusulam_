using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci.OgrenciIsleri
{
    [GzipCompression]
    public class OI_BultenListesiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OI_BultenListesi;

        public Object KullaniciTipiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = ID_MENU;
                    return c.DKullanici.KullaniciTipiGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object BultenListesiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciIsleri.ID_MENU = ID_MENU;
                    return c.DOgrenciIsleri.BultenListesiGetir(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
