using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Sinav
{
    [GzipCompression]
    public class UniteTaramaSinifRaporController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UniteTaramaSinifRaporu;

        public Object SinifNetPuanOrtalamalari(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinifNetPuanOrtalamalari(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

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


        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }

            }
            catch (Exception)
            {
                throw;
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

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavBilgiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
