using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Etut
{
    [GzipCompression]
    public class EtutProgramiOVController : ApiController
    {
        internal int ID_MENU = (int)EMenu.EtutProgramiOV;

        public Object KullaniciTipiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = ID_MENU;
                    return c.DKullanici.KullaniciTipiGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object EtutSabitListeOV(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.EtutSabitListeOV(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutListeOV(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutListeOV(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavEtutKatilimGrafigiOV(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DEtut.ID_MENU = ID_MENU;
                    return c.DEtut.SinavEtutKatilimGrafigiOV(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}