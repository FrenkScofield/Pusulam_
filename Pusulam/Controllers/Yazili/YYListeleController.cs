using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yazili
{
    [GzipCompression]
    public class YYListeleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YYListele;

        public Object OVGorsunDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.OVGorsunDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KarnedeGorunsunDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.KarnedeGorunsunDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AktifDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.AktifDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public Object YaziliBilgiGetirListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliBilgiGetirListe(j);
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

        public Object YaziliListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object YaziliAktifPasifYap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliAktifPasifYap(j);
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

        public Object YaziliKopyala(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliKopyala(j);
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

        public Object ExcelTopluSonucKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.ExcelTopluOgrenciCevapKontrol(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Object ExcelTopluSonucKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.ExcelTopluOgrenciCevapKaydet(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object BildirimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.BildirimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
