using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Veli.OgrenciHarcama
{
    public class OgrenciHarcamalariController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OgrenciHarcamalari;
        [GzipCompression]
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

        [GzipCompression]
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

        [GzipCompression]
        public Object OgrenciDonemDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciDonemDetay(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GzipCompression]
        public Object ParaYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciHarcama.ID_MENU = ID_MENU;
                    return c.DOgrenciHarcama.ParaYukle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ParaYukleCevap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciHarcama.ID_MENU = ID_MENU;
                    return c.DOgrenciHarcama.ParaYukleCevap(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [GzipCompression]
        public Object ParaYukleCevapKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciHarcama.ID_MENU = ID_MENU;
                    return c.DOgrenciHarcama.ParaYukleCevapKontrol(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
