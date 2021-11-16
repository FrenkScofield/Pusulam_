using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Frekans
{
    [GzipCompression]
    public class FrekansSistemController : ApiController
    {
        internal int ID_MENU = (int)EMenu.FrekansSistem;
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

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.SinavTuruListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.FrekansDersListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansOgretmenSirali(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.FrekansOgretmenSirali(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansGetirOgretmenSinif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.FrekansGetirOgretmenSinif(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansGetirSinifOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.FrekansGetirSinifOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciFrekansDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.OgrenciFrekansDetayGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifFrekansGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.SinifFrekansGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.OgrenciFrekansEksikKazanimGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.SinifFrekansEksikKazanimGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgretmenFrekansEksikKazanimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekans.ID_MENU = ID_MENU;
                    return c.DFrekans.OgretmenFrekansEksikKazanimGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
