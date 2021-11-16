using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yazili
{
    [GzipCompression]
    public class YYTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YYTanimla;

        public Object YaziliTanimla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliTanimla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliTanimlaExcel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliTanimlaExcel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliBilgiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.Kademe3Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe2Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.Kademe2Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliDersListele(JObject j)
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

        public Object KademeDersGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KademeYaziliDersGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.YaziliDersGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliBilgileriniGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliBilgileriniGuncelle(j);
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
    }
}
