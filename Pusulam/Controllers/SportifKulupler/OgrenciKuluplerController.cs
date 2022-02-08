using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.SportifKulupler;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.SportifKulupler
{
    [GzipCompression]
    public class OgrenciKuluplerController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OgrenciKulupler;
        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
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
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object OgrenciKulupListele(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciKulupler> c = new Channel2<DOgrenciKulupler>(ID_MENU))
                {
                    
                    return c._cs.OgrenciKulupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Periyot(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciKulupler> c = new Channel2<DOgrenciKulupler>(ID_MENU))
                {                    
                    return c._cs.Periyot(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KulupListele(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {
                    return c._cs.KulupListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object KulupEkle(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {

                    return c._cs.KulupEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KulupDuzenle(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {
                    return c._cs.KulupDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object YetkiKontrol(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciKulupler> c = new Channel2<DOgrenciKulupler>(ID_MENU))
                {
                    return c._cs.YetkiKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
