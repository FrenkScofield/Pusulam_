using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class MevcutDurumController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MevcutDurum;

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
                    return c.DHedef.KazanimListeleYeni(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.SinavTuruListeleGenel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SinavTuruNetGrafikListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.SinavTuruNetGrafikListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}