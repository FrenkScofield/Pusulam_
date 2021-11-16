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
    public class PeriyotBelirleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KulupPeriyotBelirle;
        public Object PeriyotListele(JObject j)
        {
            try
            {
                using (Channel2<DPeriyotBelirle> c = new Channel2<DPeriyotBelirle>(ID_MENU))
                {
                    
                    return c._cs.PeriyotBelirleListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object PeriyotEkle(JObject j)
        {
            try
            {
                using (Channel2<DPeriyotBelirle> c = new Channel2<DPeriyotBelirle>(ID_MENU))
                {
                    return c._cs.PeriyotEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotDuzenle(JObject j)
        {
            try
            {
                using (Channel2<DPeriyotBelirle> c = new Channel2<DPeriyotBelirle>(ID_MENU))
                {
                    return c._cs.PeriyotDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
