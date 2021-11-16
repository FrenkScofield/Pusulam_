using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Takvim
{
    [GzipCompression]
    public class TakvimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.Anasayfa;
        public Object TakvimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimListele(j);
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

        public Object TakvimEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.KullaniciTipListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object GrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.GrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YetkiKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.YetkiKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimEtkinlikListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEtkinlikListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimEtkinlikEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEtkinlikEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimEtkinlikSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEtkinlikSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimEtkinlikGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEtkinlikGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DuyuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.DuyuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TakvimEtkinlikEkleExcel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTakvim.ID_MENU = ID_MENU;
                    return c.DTakvim.TakvimEtkinlikEkleExcel(j);
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

        public Object EtutDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MorpaLinkGetirGiris(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DHedef.ID_MENU = ID_MENU;
                    return c.DHedef.MorpaLinkGetirGiris(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
