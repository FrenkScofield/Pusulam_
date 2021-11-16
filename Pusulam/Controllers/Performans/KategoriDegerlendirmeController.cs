using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Performans
{
    public class KategoriDegerlendirmeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KategoriDegerlendirme;

      

        public Object PeriyotTarihListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PeriyotTarihListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.OgretmenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KategoriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.KategoriListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PerformansSoru(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.PerformansSoru(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KategoriSoru(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.KategoriSoru(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DegerlendirmeKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DPerformans.ID_MENU = ID_MENU;
                    return c.DPerformans.DegerlendirmeKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
