using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Personel
{
    [GzipCompression]
    public class ProjeDonemController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ProjeDonem;

        public Object ProjeDonemKayit(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemKayit(j);
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

        public Object OgretmenSinifListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.OgretmenSinifListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
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

        public Object SinifDersListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.SinifDersListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgretmenProjeTalepleri(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.OgretmenProjeTalepleri(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object DanismanOgretmenSinifProje(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.DanismanOgretmenSinifProje(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object KontrolKayit(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.KontrolKayit(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemSil(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemDurumDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemDurumDegistir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemDosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemDosyaListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemDosyaSil(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemNotEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemNotEkle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
