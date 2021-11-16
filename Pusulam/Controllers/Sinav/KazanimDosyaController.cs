using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav
{
    [GzipCompression]
    public class KazanimDosyaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KazanimDosya;

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

        public Object KademeDersGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersUniteGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Object DersUniteDosyaListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteDosyaListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersUniteDosyaPasif(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteDosyaPasif(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersUniteDosyaYukle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteDosyaYukle();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
