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
    [GzipCompression]
    public class KotaBelirleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KulupKotaBelirle;

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
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {

                    return c._cs.Kademe3ListelebyKullanici(j);
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }        

        public Object KotaListele(JObject j)
        {
            try
            {
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {
                    
                    return c._cs.KotaBelirleListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KotaEkle(JObject j)
        {
            try
            {
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {

                    return c._cs.KotaEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KotaDuzenle(JObject j)
        {
            try
            {
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {                    
                    return c._cs.KotaDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KotaSil(JObject j)
        {
            try
            {
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {                    
                    return c._cs.KotaSil(j);
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
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {
                    return c._cs.KulupListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object KontrolKotaEkle(JObject j)
        {
            try
            {
                using (Channel2<DKotaBelirle> c = new Channel2<DKotaBelirle>(ID_MENU))
                {

                    return c._cs.KontrolKotaEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
