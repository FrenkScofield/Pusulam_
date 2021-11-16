using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Veli.Fatura
{
    public class FaturaOdemeController : ApiController
    {

        internal int ID_MENU = (int)EMenu.FaturaOdeme;

        [GzipCompression]
        public Object FaturaOdemeEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFatura.ID_MENU = ID_MENU;
                    return c.DFatura.FaturaOdemeEkle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object FaturaOdemeCevap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFatura.ID_MENU = ID_MENU;
                    return c.DFatura.FaturaOdemeCevap(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [GzipCompression]
        public Object FaturaOdemeCevapKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFatura.ID_MENU = ID_MENU;
                    return c.DFatura.FaturaOdemeCevapKontrol(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
