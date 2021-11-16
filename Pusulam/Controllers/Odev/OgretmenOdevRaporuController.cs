using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class OgretmenOdevRaporuController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OgretmenOdevRaporu;

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

        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KademeListele(j);
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

        public Object OdevTurListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OdevTurListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.DersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OgretmenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenOdevRaporu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgretmenOdevRaporu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenSinifOdevListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgretmenSinifOdevListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
