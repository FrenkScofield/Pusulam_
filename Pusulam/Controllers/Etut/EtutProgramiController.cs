using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Etut
{
    [GzipCompression]
    public class EtutProgramiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.EtutProgrami;

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

        public Object Kademe3ListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object DersOgretmenListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.DersOgretmenListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object EtutSabitKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutSabitKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object EtutSabitListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutSabitListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutKatilimDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutKatilimDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutOgrenciListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutOgrenciListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutOgrenciDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutOgrenciDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SubeKademeDersListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SubeKademeDersListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SubeKademeDersOgretmenListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SubeKademeDersOgretmenListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutDuzelt(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutDuzelt(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object AktifPasifYap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.AktifPasifYap(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DanismanOgretmenSinifListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.DanismanOgretmenSinifListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
