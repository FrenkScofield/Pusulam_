using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaKatilimciAtamaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaKatilimciAtama;

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
                    return c.DZumreCalisma.ZumreCalismaListeleSelect(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Object ZumreCalismaKatilimciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKatilimciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaKatilimciSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKatilimciSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaKatilimciSilToplu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKatilimciSilToplu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Object ZumreCalismaKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaKatilimciAta(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKatilimciAta(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ZumreCalismaKatilimciAtaToplu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaKatilimciAtaToplu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DFiltre.ID_MENU = ID_MENU;
                        return c.DFiltre.KullaniciTipiListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object BransListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DFiltreEk.ID_MENU = ID_MENU;
                        return c.DFiltreEk.BransListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object ExcelTopluZumreCalismaKatilimciKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ExcelTopluZumreCalismaKatilimciKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ExcelTopluZumreCalismaKatilimciEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ExcelTopluZumreCalismaKatilimciEkle(j);
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
