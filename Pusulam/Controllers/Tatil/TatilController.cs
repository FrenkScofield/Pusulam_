using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tatil
{
    [GzipCompression]
    public class TatilController : ApiController
    {
        internal int ID_MENU = (int)EMenu.Tatil;
        public Object TatilListele(JObject j)
        {          
            
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTatil.ID_MENU = ID_MENU;
                    return c.DTatil.TatilListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TatilEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTatil.ID_MENU = ID_MENU;
                    return c.DTatil.TatilEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object TatilSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTatil.ID_MENU = ID_MENU;
                    return c.DTatil.TatilSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
