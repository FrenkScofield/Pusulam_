using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.DisKaynak;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.DisKaynak
{
    public class DisKaynakKademeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DiskaynakKademeEkle;

        public Object Ekle(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakKademe> c = new Channel2<DDisKaynakKademe>(ID_MENU))
                {
                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Liste(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakKademe> c = new Channel2<DDisKaynakKademe>(ID_MENU))
                {
                    return c._cs.Liste(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Sil(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakKademe> c = new Channel2<DDisKaynakKademe>(ID_MENU))
                {
                    return c._cs.Sil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object ProjeListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakKademe> c = new Channel2<DDisKaynakKademe>(ID_MENU))
                {
                    return c._cs.ProjeListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
