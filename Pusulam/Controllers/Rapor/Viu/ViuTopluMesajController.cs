using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;
using PusulamBusiness.Rapor.Viu;

namespace Pusulam.Controllers.Rapor.Viu
{
    [GzipCompression]
    public class ViuTopluMesajController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ViuTopluMesaj;

        public Object ViuTopluMesajGonder(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuTopluMesajGonder(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Object ViuTopluResimliMesajGonder()
        {
            try
            {
                using (Channel2<DViu> c = new Channel2<DViu>(ID_MENU))
                {
                    return c._cs.ViuTopluResimliMesajGonder();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
