using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaYaziliGirisiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaYaziliGirisi;

        public Object YaziliGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaYaziliGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object GirisGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    j.Add("IP", DBase.GetUser_IP());
                    return c.DZumreCalisma.ZumreCalismaYaziliGirisGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
