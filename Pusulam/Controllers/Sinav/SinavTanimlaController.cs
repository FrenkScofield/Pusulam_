using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav
{
    //[GzipCompression]
    public class SinavTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.SinavTanimla;
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
            catch (Exception ex)
            {
                throw ex;
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
        [HttpPost]
        public object YonergeSinavKaydet()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YonergeSinavKaydet();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object YonergeSinavGuncelle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YonergeSinavGuncelle();
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

        public Object SinavBilgiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object YonergeSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YonergeSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGuncelle(j);
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

        public Object BilgiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGenelListeler.ID_MENU = ID_MENU;
                    return c.DGenelListeler.BilgiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BilisselSurecListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGenelListeler.ID_MENU = ID_MENU;
                    return c.DGenelListeler.BilisselSurecListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruListesiGetirSB(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SoruListesiGetirSB(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruBankasiAra(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SoruBankasiAra(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
