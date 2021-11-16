using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class HedefBelirlemeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.HedefBelirleme;

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object UniversiteTabanPuanListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.UniversiteTabanPuanListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object UniversiteTabanPuanListeleOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.UniversiteTabanPuanListeleOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object IlListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.IlListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniversiteTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.UniversiteTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BolumListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.BolumListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object PuanTuruListeleGenel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = ID_MENU;
                    return c.DOSYM.PuanTuruListeleGenel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefListelePuanTur(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefListelePuanTur(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OOBPKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.OOBPKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OOBPGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.OOBPGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OgrenciSonPuanGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.OgrenciSonPuanGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OgrenciSinavNetListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.OgrenciSinavNetListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefNetEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefNetEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object KazanimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.KazanimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public String PuanTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.PuanTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
