using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Viu
{
    [GzipCompression]
    public class ViuRaporlarController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ViuRaporlar;

        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object PersonelListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDegerlendirme.ID_MENU = ID_MENU;
                    return c.DDegerlendirme.PersonelListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AramaSebepListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.AramaSebepListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AramaDurumListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.AramaDurumListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AramaRapor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.AramaRapor(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YoklamaRapor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.YoklamaRapor(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuAramaKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DViu.ViuAramaYetkiKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuAramaKullaniciYetki(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuAramaKullaniciYetkiKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KullaniciTipiListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
                }
            }
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
            }
        }
        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KullaniciListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ViuRandevuTakipListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuRandevuTakipListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuBildirimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuBildirimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuRandevuTakipDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuRandevuTakipDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KullaniciCihazlariListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.KullaniciCihazlariListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
