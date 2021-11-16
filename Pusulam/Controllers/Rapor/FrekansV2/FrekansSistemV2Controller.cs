using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.FrekansV2
{
    [GzipCompression]
    public class FrekansSistemV2Controller : ApiController
    {
        internal int ID_MENU = (int)EMenu.FrekansSistemV2;
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

        public Object KullaniciKademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DBirimYetki.ID_MENU = ID_MENU;
                    return c.DBirimYetki.KullaniciKademeListele(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinavTuruListele(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansDersListele(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansOgretmenSirali(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansGetirOgretmenSinif(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansGetirSinifOgrenci(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.OgrenciFrekansDetayGetir(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinifFrekansGetir(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.OgrenciFrekansEksikKazanimGetir(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinifFrekansEksikKazanimGetir(j);
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
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.OgretmenFrekansEksikKazanimGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansSistemSonFiltre(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansSistemSonFiltre(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
