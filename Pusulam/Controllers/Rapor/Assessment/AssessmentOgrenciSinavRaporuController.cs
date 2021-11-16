using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Assessment
{
    [GzipCompression]
    public class AssessmentOgrenciSinavRaporuController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AssessmentOgrenciSinavRaporu;

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

        public Object AssessmentOgrenciSinavRaporu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAssessment.ID_MENU = ID_MENU;
                    return c.DAssessment.AssessmentOgrenciSinavRaporu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
