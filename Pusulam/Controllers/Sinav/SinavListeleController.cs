using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Sinav
{
    [GzipCompression]
    public class SinavListeleController : ApiController
    {

        internal int ID_MENU = (int)EMenu.SinavListele;
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

        public Object SinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListeleKademeDonem(j);
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

        public Object SinavDersleriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDersleriListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDersSorulariListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDersSorulariListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SoruIslemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SoruIslemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavSoruIslemleriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavSoruIslemleriListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavSoruIslemSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavSoruIslemSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SinavSoruIslem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavSoruIslem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SinavAktifPasifYap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavAktifPasifYap(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListelebySinav(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OgrenciListelebySinav(j);
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

        public int SinavKopyala(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavKopyala(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OgrenciSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object SinavDuzenleYetki(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDuzenleYetki(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SinavDurumlariDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDurumlariDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int HariciPuanSiraTaslakYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.HariciPuanSiraTaslakYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDegerlendirLog(JObject j)
        {
            using (Channel c = new Channel())
            {
                c.DSinav.ID_MENU = ID_MENU;
                return c.DSinav.SinavDegerlendirLog(j);
            }
        }
    }
}
