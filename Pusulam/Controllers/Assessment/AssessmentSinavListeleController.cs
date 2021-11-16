using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Assessment
{
    [GzipCompression]
    public class AssessmentSinavListeleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AssessmentSinavListele;

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

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object AktifPasifDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.AktifPasifDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
