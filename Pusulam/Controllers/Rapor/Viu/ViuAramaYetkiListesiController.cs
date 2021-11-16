using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Viu
{
    [GzipCompression]
    public class ViuAramaYetkiListesiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ViuAramaYetkiListesi;

        public Object ViuKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuAramaKullaniciYetki(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.ViuAramaKullaniciYetkiKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ViuAramaKullaniciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DViu.ViuAramaYetkiKullaniciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
