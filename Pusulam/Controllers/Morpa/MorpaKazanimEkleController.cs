using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Morpa
{
    [GzipCompression]
    public class MorpaKazanimEkleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MorpaKazanimEkle;

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

        public Object MorpaKazanimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaKazanimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaKazanimEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaKazanimEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KazanimAktifPasifDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.KazanimAktifPasifDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaKazanimGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaKazanimGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaKazanimExcelYukle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaKazanimExcelYukle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
