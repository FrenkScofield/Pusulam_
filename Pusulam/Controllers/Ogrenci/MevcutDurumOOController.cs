using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class MevcutDurumOOController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MevcutDurumOO;

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
        public Object OgrenciListelebyVeliSinav(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeliSinav(j);
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

        public object OgrenciSonDogruGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.OgrenciSonDogruGetir(j);
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
                    return c.DHedef.SinavTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object HedefNetEkleOO(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.HedefNetEkleOO(j);
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


        public Object SinavListelebyOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListelebyOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object MorpaLinkGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.MorpaLinkGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
