using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Morpa
{
    [GzipCompression]
    public class MorpaDersEkleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.MorpaDersEkle;

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
        
        public Object MorpaDersEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaDersEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Object AktifPasifDegistir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.AktifPasifDegistir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public Object MorpaDersGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaDersGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
