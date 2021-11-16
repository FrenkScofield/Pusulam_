using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.UniteTaramaOlcegi
{
    [GzipCompression]
    public class UniteTaramaEtutOlusturController : ApiController
    {

        internal int ID_MENU = (int)EMenu.UniteTaramaEtutOlustur;

        public Object EtutTabloSinav(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutTabloSinav(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object EtutTabloYazili(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutTabloYazili(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Object EtutTabloSinavOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutTabloSinavOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object EtutTabloYaziliOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutTabloYaziliOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object EksikKazanimEtutSinavOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.EksikKazanimEtutSinavOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object EksikKazanimListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.EksikKazanimListesi(j);
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

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        public Object SinifListelebyKullaniciMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullaniciMultiSube(j);
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

        public Object EtutOlustur(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutOlustur(j);
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
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavTuruListele(j);
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

        public Object SinavTuruDersleriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavTuruDersleriListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SubeOgretmenListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SubeOgretmenListe(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifListelebyKullaniciMultiSubeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullaniciMultiSubeDonem(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
