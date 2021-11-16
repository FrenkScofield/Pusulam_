using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.OgrenciDegerlendirme
{
    [GzipCompression]
    public class OD_DonemlikController : ApiController
    {

        internal int ID_MENU = (int)EMenu.OD_Donemlik;

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

        public Object OD_DonemlikDegerlendirmeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciDegerlendirme.ID_MENU = ID_MENU;
                    return c.DOgrenciDegerlendirme.OD_DonemlikDegerlendirmeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OD_DonemlikDegerlendirmeDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenciDegerlendirme.ID_MENU = ID_MENU;
                    return c.DOgrenciDegerlendirme.OD_DonemlikDegerlendirmeDetay(j);
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
