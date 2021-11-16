using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class UniteTaramaExcelYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UniteTaramaExcelYukle;

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
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavBilgiEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavBilgiEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavBilgiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DbUniteTarama.ID_MENU = ID_MENU;
                    return c.DbUniteTarama.SinavBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
