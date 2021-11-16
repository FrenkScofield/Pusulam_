using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Degerlendirme
{
    [GzipCompression]
    public class DegerlendirmePeriyotTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DegerlendirmePeriyotTanimla;

        public Object KademeListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DBirimYetki.ID_MENU = ID_MENU;
                        return c.DBirimYetki.KademeListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DBirimYetki.ID_MENU = ID_MENU;
                        return c.DBirimYetki.KullaniciTipiListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Listele(JObject j)
        {
            {
                try
                {

                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.Listele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Kaydet(JObject j)
        {
            {
                try
                {

                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.Kaydet(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Sil(JObject j)
        {
            {
                try
                {

                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.PeriyotSil(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

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
    }
}
