using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.Rapor.Sinav
{
    [GzipCompression]
    public class VeliUniteTaramaRaporuController : ApiController
    {
        internal int ID_MENU = (int)EMenu.VeliUniteTaramaRaporu;

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

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Object SinavListelebyOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.UniteTaramaSinavListese(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
