using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.CurpusStudy
{
    [GzipCompression]
    public class CurpusStudyYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.CurpusStudyYukle;

        public Object CurpusStudyListe(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DCurpusStudy.ID_MENU = ID_MENU;
                        return c.DCurpusStudy.CurpusStudyListe(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object CurpusStudyExcelYukle(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DCurpusStudy.ID_MENU = ID_MENU;
                        return c.DCurpusStudy.CurpusStudyExcelYukle(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
