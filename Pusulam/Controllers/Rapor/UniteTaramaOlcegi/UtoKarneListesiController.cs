using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.UniteTaramaOlcegi
{
    [GzipCompression]
    public class UtoKarneListesiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UtoKarneListesi;

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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public Object SinavOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
