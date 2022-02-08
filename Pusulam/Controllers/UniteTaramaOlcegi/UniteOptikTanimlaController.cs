using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.UniteTarama;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.UniteTaramaOlcegi
{
    [GzipCompression]
    public class UniteOptikTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OptikTanimla;

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

        public Object UniteSinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.UniteSinavListeleKademeDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTuruListele(j);
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

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikDosyaGor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikDosyaGor(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikListele(JObject j)
        {
            try
            {
                using (Channel2<DUniteOptikTanimla> c = new Channel2<DUniteOptikTanimla>(ID_MENU))
                {
                    
                    return c._cs.OptikListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikKaydet(JObject j)
        {
            try
            {
                using (Channel2<DUniteOptikTanimla> c = new Channel2<DUniteOptikTanimla>(ID_MENU))
                {

                    return c._cs.OptikKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
