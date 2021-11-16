using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Morpa
{
    [GzipCompression]
    public class MorpaMateryalEkleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MorpaMateryalEkle;

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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaMateryalListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaMateryalListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaMateryalEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaMateryalEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MateryalAktifPasifDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MateryalAktifPasifDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaMateryalGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaMateryalGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
