using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Rapor.FrekansV2
{
    [GzipCompression]
    public class FrekansKampusV2Controller : ApiController
    {

        internal int ID_MENU = (int)EMenu.FrekansKampusV2;
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

        public Object FrekansSinifOgretmenSirali(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansSinifOgretmenSirali(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansGetirSinif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansGetirSinif(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object SinifFrekansGetirSinifOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinifFrekansGetirSinifOgrenci(j);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object SinifOgrenciFrekansDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinifOgrenciFrekansDetayGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifFrekansGrafikGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.SinifFrekansGrafikGetir(j);
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

        public Object FrekansKampusSirali(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansKampusSirali(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FrekansKampusSonFiltre(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFrekansV2.ID_MENU = ID_MENU;
                    return c.DFrekansV2.FrekansKampusSonFiltre(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
