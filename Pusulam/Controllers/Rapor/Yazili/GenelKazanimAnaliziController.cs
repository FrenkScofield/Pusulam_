using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Yazili
{
    [GzipCompression]
    public class GenelKazanimAnaliziController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OrtalamaListesi;

        public Object OrtalamaListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOrtalamaListesi.ID_MENU = ID_MENU;
                    return c.DOrtalamaListesi.OrtalamaListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
