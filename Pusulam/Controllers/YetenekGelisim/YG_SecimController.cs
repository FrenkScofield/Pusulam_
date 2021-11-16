using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.YetenekGelisim
{
    [GzipCompression]
    public class YG_SecimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YG_Secim;
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

        public Object SecilebilirYetenekListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.SecilebilirYetenekListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YetenekDersSec(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekDersSec(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Object YetenekDersCik(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekDersCik(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YetenekKlupKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYetenekGelisim.ID_MENU = ID_MENU;
                    return c.DYetenekGelisim.YetenekKontrol(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
