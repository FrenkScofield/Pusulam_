using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.DisKaynak;
using PusulamBusiness.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pusulam.Controllers.DisKaynak
{
    public class DisKaynakTanimController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DiskaynakKademeTanim;

        public Object Ekle(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakTanim> c = new Channel2<DDisKaynakTanim>(ID_MENU))
                {
                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Duzenle(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynakTanim> c = new Channel2<DDisKaynakTanim>(ID_MENU))
                {
                    return c._cs.Duzenle(j);
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
                using (Channel2<DDisKaynakTanim> c = new Channel2<DDisKaynakTanim>(ID_MENU))
                {
                    return c._cs.Liste(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
