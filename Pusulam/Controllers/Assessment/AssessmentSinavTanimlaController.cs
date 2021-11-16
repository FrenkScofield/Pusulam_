using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Assessment
{
    [GzipCompression]
    public class AssessmentSinavTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AssessmentSinavTanimla;

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

        public Object AssessmentKategoriListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.AssessmentKategoriListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.SinavKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.SinavDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
