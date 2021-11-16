using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.AkilliOgretimBarkod
{
    [GzipCompression]
    public class AkilliOgretimBarkodExcelYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AkilliOgretimBarkodExcelYukle;

        public Object ExcelYukle(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DAkilliOgretimBarkod.ID_MENU = ID_MENU;
                        return c.DAkilliOgretimBarkod.ExcelYukle(j);
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
