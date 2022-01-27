using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Viu;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Viu
{
    public class ViuTopluBildirimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ViuTopluBildirim;

        public Object Sinif_listesi(JObject j)
        {
            try
            {
                using (Channel2<DViuTopluBildirim> c = new Channel2<DViuTopluBildirim>(ID_MENU))
                {
                    return c._cs.Sinif_listesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
