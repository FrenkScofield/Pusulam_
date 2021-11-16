using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.DersCalismaProgrami
{
    [GzipCompression]
    public class KazanimOdevAtamaController : ApiController
    {

        internal int ID_MENU = (int)EMenu.DersCalismaProgrami;

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.SinavGrupListele(j);
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
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersGetir(j);
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
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteGetir(j);
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
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteDetay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


     

        public object OdevEkleGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteKazanimOdevEkle(j);
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
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.GenelDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
