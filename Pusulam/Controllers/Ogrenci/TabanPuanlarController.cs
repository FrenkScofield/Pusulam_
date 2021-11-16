using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class TabanPuanlarController : ApiController
    {
        internal int ID_MENU = (int)EMenu.TabanPuanlar;

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object UniversiteTabanPuanListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.UniversiteTabanPuanListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public object UniversiteTabanPuanListeleOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.UniversiteTabanPuanListeleOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniversiteTabanPuanSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.UniversiteTabanPuanSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object IlListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.IlListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniversiteTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.UniversiteTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object PuanTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.PuanTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object PuanTuruListeleGenel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.PuanTuruListeleGenel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object BolumListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOSYM.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DOSYM.BolumListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = (int)EMenu.Sinavlarim; ;
                    return c.DSinav.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
