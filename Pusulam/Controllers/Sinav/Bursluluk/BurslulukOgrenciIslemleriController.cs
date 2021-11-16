using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Sinav.Bursluluk
{
    [GzipCompression]
    public class BurslulukOgrenciIslemleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.BurslulukOgrenciIslemleri;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukDosyaListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.BurslulukDosyaListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukDosyaYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BurslulukDosyaYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListelebyBurslulukDosya(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.OgrenciListelebyBurslulukDosya(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukOgrenciDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BurslulukOgrenciDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukOgrenciSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BurslulukOgrenciSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListeleBursluluk(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.SinavListeleBursluluk(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BurslulukDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBursluluk.ID_MENU = ID_MENU;
                    return c.DBursluluk.BurslulukDosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
