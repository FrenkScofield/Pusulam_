using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineDers.Ogrenci
{
    [GzipCompression]
    public class OnlineDersProgramiOgrenciController : ApiController
    {

        internal int ID_MENU = (int)EMenu.OnlineDersDersProgramiOgrenci;

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
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOgrenci.ID_MENU = ID_MENU;
                        return c.DOgrenci.OgrenciListelebyVeli(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object TarihListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOnlineDers.ID_MENU = ID_MENU;
                        return c.DOnlineDers.TarihListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object OnlineDersProgramiListelebyOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineDers.ID_MENU = ID_MENU;
                    return c.DOnlineDers.OnlineDersProgramiListelebyOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
