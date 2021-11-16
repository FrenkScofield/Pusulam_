using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class AbideSinavKitapcikController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AbideSinavExcelYukle;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavKarsilikEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavKarsilikEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavKarsilikGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavKarsilikGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
