using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class OdevKademeYetkiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OdevKademeYetki;

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

        public Object Kaydet(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DOdev.ID_MENU = ID_MENU;
                        return c.DOdev.OdevTurKademeKaydet(j);
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
                        c.DOdev.ID_MENU = ID_MENU;
                        return c.DOdev.OdevTurKademeListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
