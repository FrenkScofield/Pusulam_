using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.ZumreCalisma
{
    [GzipCompression]
    public class ZumreCalismaYoklamaGirisiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ZumreCalismaYoklamaGirisi;

        public Object YoklamaGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DZumreCalisma.ID_MENU = ID_MENU;
                    return c.DZumreCalisma.ZumreCalismaYoklamaGetir(j);
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
                    return c.DZumreCalisma.ZumreCalismaYoklamaGirisGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
