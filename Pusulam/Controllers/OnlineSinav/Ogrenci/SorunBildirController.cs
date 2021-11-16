using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav.Ogrenci
{
    public class SorunBildirController : ApiController
    {
        internal int ID_MENU = (int)EMenu.SorunBildir;

        [HttpPost]
        public Object Kaydet()
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DSorunBildir.ID_MENU = ID_MENU;
                        return c.DSorunBildir.Kaydet();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSorunBildir.ID_MENU = ID_MENU;
                    return c.DSorunBildir.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
