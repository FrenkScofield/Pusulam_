using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.UniteTaramaOlcegi;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.UniteTaramaOlcegi
{
    [GzipCompression]
    public class UniteTaramaOptikYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UniteTaramaOptikYukle;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.Kademe3Listele(j);
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
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikYukle(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OptikYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikDetayGetir(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OptikDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikTumDetay(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OptikTumDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikSil(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OptikSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciEslestir(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OgrenciEslestir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciEslestirTamamla(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OgrenciEslestirTamamla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciMukerrer(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.OgrenciMukerrer(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object MukerrerIdSil(JObject j)
        {
            try
            {
                using (Channel2<DUniteTaramaOlcegi> c = new Channel2<DUniteTaramaOlcegi>(ID_MENU))
                {
                    return c._cs.MukerrerIdSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
