using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Yazili
{
    [GzipCompression]
    public class SinifBazindaKazanimAnaliziController : ApiController
    {
        internal int ID_MENU = (int)EMenu.SinifBazindaKazanimAnalizi;

        public Object SinifBazindaKazanimAnalizi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinifBazindaKazanimAnalizi.ID_MENU = ID_MENU;
                    return c.DSinifBazindaKazanimAnalizi.SinifBazindaKazanimAnalizi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifBazindaKazanimAnaliziYeni(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinifBazindaKazanimAnalizi.ID_MENU = ID_MENU;
                    return c.DSinifBazindaKazanimAnalizi.SinifBazindaKazanimAnaliziYeni(j);
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

        public Object Kademe3ListeleMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListeleMultiSube(j);
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

        public Object OgrenciListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyKullanici(j);
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

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        public Object YaziliYoklamaSinavListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSinavListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSinavListesiYeni(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTopluYaziliYoklamaSonuclari.ID_MENU = ID_MENU;
                    return c.DTopluYaziliYoklamaSonuclari.YaziliYoklamaSinavListesiYeni(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListeleMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListeleMulti(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSonuclari(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSonuclariGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSonuclariGetirGenel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSonuclariGetirGenel(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSinifSinavListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliYoklamaSinifSinavListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
