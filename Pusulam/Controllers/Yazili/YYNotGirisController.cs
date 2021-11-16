using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yazili
{
    [GzipCompression]
    public class YYNotGirisController : ApiController
    {
        internal int ID_MENU = (int)EMenu.YYNotGiris;
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

        public Object SubeKurListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeKurListelebyKullanici(j);
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

        public Object SinifKurSinifListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifKurSinifListelebyKullanici(j);
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

        public Object Kademe3KurListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3KurListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliListeleSelect(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.YaziliListeleSelect(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.OgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.OgrenciKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.OgrenciSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciKaydetToplu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DYaziliYoklama.ID_MENU = ID_MENU;
                    return c.DYaziliYoklama.OgrenciKaydetToplu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
