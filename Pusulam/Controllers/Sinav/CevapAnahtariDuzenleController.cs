using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav
{
    [GzipCompression]
    public class CevapAnahtariDuzenleController : ApiController
    {

        internal int ID_MENU = (int)EMenu.CevapAnahtariDuzenle;
        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTuruListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavTanimla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTanimla(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListelePasifDahil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListelePasifDahil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListeleKademeDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object SinavBilgiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.CevapAnahtari.ID_MENU = ID_MENU;
                    return c.CevapAnahtari.SinavBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object CATaslakYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.CATaslakYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object CevapAnahtariKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.CevapAnahtari.ID_MENU = ID_MENU;
                    return c.CevapAnahtari.CevapAnahtariKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersKonuListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DersKonuListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KatsayiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.KatsayiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KatsayiKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.KatsayiKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DosyaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DosyaKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikBelliMi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikBelliMi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDegerlendir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDegerlendir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object UniteAra(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.UniteAra(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersAdiGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DersAdiGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}