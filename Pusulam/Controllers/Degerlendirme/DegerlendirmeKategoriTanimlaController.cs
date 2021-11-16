using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Degerlendirme
{
    [GzipCompression]
    public class DegerlendirmeKategoriTanimlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DegerlendirmeKategoriTanimla;

        public Object Listele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.KategoriListele(j);
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
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDegerlendirme.ID_MENU = ID_MENU;
                    return c.DDegerlendirme.KategoriKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Sil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDegerlendirme.ID_MENU = ID_MENU;
                    return c.DDegerlendirme.KategoriSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
