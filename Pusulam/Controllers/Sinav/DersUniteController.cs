using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Sinav
{
    [GzipCompression]
    public class DersUniteController : ApiController
    {

        internal int ID_MENU = (int)EMenu.DersUniteEkleListele;

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

        public Object UniteEkle(JObject j)
        {
            using (Channel c = new Channel())
            {
                c.DDersUnite.ID_MENU = ID_MENU;
                return c.DDersUnite.DersUniteEkle(j);
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
        public object UniteDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniteSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniteGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.DersUniteGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GenelDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.GenelDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniteEkleExcel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.UniteEkleExcel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniteLinkEkleExcel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.UniteLinkEkleExcel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UniteSilTumu(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersUnite.ID_MENU = ID_MENU;
                    return c.DDersUnite.UniteSilTumu(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
