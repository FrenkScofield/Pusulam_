using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.SportifKulupler;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.SportifKulupler
{
    //[GzipCompression]
    public class KulupBelirleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KulupKotaBelirle;
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
        public Object KulupKontrol(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {
                    
                    return c._cs.KulupKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        public Object KuluptenAyril(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {                    
                    return c._cs.KuluptenAyril(j);
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
                using (Channel2<DSube> c = new Channel2<DSube>(ID_MENU))                
                {                    
                    return c._cs.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
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

        public Object YetenekKulupKontrol(JObject j)
        {
            try
            {
                using (Channel2<DKulupBelirle> c = new Channel2<DKulupBelirle>(ID_MENU))
                {
                    return c._cs.YetenekKlupKontrol(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
