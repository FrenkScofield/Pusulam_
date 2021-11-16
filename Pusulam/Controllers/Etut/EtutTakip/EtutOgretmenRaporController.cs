using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Etut.EtutTakip
{
    [GzipCompression]
    public class EtutOgretmenRaporController : ApiController
    {
        internal int ID_MENU = (int)EMenu.EtutOgretmenRapor;

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

        public Object OgretmenSinifSureListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.OgretmenSinifSureListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object OgretmenSinifDetayListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.OgretmenSinifDetayListele(j);
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
                    return c.DEtut.SubeOgretmenListeRapor(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object PersonelSubeKademeGetir(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.PersonelSubeKademeGetir(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
    }
}
