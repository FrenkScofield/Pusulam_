using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.OgrenciDegerlendirme
{
    [GzipCompression]
    public class OD_AylikController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OD_Aylik;

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

        public Object OD_DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciDegerlendirme.ID_MENU = ID_MENU;
                    return c.DOgrenciDegerlendirme.OD_DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OD_AyListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciDegerlendirme.ID_MENU = ID_MENU;
                    return c.DOgrenciDegerlendirme.OD_AyListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OD_DegerlendirmeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciDegerlendirme.ID_MENU = ID_MENU;
                    return c.DOgrenciDegerlendirme.OD_DegerlendirmeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OD_OgrenciDonemDetay(JObject j)
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
    }
}
