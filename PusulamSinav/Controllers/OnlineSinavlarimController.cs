using System;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ogrenci;

namespace PusulamSinav.Controllers
{
    public class OnlineSinavlarimController : ApiController
    {
        // GET api/<controller>
        internal int ID_MENU = (int)EMenu.OnlineSinavlarim;

        public Object OnlineSinavlarimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavlarimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavSoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavSoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavPerformans(JObject j)
        {
            try
            {
                using (Channel2<DOnlineSinav> c = new Channel2<DOnlineSinav>(ID_MENU))
                {
                    
                    return c._cs.OnlineSinavPerformans(j);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}