using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class AbideSinavExcelYukleController : ApiController
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

        public Object SinavBilgiEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavBilgiEkle(j);
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
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavBilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BilgiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGenelListeler.ID_MENU = ID_MENU;
                    return c.DGenelListeler.BilgiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BilisselSurecListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGenelListeler.ID_MENU = ID_MENU;
                    return c.DGenelListeler.BilisselSurecListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
