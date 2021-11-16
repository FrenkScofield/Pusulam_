using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ogretmen;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogretmen
{
    [GzipCompression]
    public class OgretmenBilgileriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OgretmenBilgileri;

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.SubeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltreEk> c = new Channel2<DFiltreEk>(ID_MENU))
                {
                    return c._cs.OgretmenListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Object OgretmenBilgileriListele(JObject j)
        {
            try
            {
                using (Channel2<DOgretmen> c = new Channel2<DOgretmen>(ID_MENU))
                {
                    return c._cs.OgretmenBilgileriListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
