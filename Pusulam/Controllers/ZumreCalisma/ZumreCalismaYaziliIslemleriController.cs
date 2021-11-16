using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaYaziliIslemleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaYaziliIslemleri;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaYaziliDurumGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaYaziliDurumGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaYaziliNotGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaYaziliNotGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaYaziliNotYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaYaziliNotYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
