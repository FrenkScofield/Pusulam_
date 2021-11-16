using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.MentalUp
{
    [GzipCompression]
    public class VeliMentalUpGirisController : ApiController
    {

        internal int ID_MENU = (int)EMenu.VeliMentalUpGiris;

        public Object VeliMentalUp(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMentalUp.ID_MENU = ID_MENU;
                    return c.DMentalUp.VeliMentalUp(j);
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
