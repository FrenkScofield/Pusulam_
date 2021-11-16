using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class OgrenciOdevListesiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.VeliAraKarne;

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

        public Object Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgrenciOdevListele(j);
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
        public object OdevIptal(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgrenciOdevIptal(j);
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

        public Object OgrenciOdevDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OgrenciOdevDersListele(j);
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

        public Object OdevTamamla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevTamamla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Object OdevDosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevDosyaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Object OdevDosyaYukle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevDosyaYukle();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OdevDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevDosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
