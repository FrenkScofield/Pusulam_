using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Assessment
{
    [GzipCompression]
    public class AssessmentSinifSinavRaporuController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AssessmentSinifSinavRaporu;

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

        public Object AssessmentSinifSinavRaporu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.AssessmentSinifSinavRaporu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
