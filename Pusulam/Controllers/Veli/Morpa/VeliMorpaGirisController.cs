using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.Morpa
{
    [GzipCompression]
    public class VeliMorpaGirisController : ApiController
    {

        internal int ID_MENU = (int)EMenu.VeliMorpaGiris;

        public Object MorpaLink(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaLink(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
