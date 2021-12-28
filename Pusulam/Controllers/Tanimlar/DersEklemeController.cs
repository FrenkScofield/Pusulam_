using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Tanimlar;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tanimlar
{
    [GzipCompression]
    public class DersEklemeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DersEkleme;
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

       
        public Object EgitimTuruListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.EgitimTuruListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DersListele(JObject j)
        {
            try
            {
                using (Channel2<DDersEkleme> c = new Channel2<DDersEkleme>(ID_MENU))
                {

                    return c._cs.DersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DuzenleModal(JObject j)
        {
            try
            {
                using (Channel2<DDersEkleme> c = new Channel2<DDersEkleme>(ID_MENU))
                {

                    return c._cs.DuzenleModal(j);
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
                using (Channel2<DDersEkleme> c = new Channel2<DDersEkleme>(ID_MENU))
                {

                    return c._cs.Duzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Kaydet(JObject j)
        {
            try
            {
                using (Channel2<DDersEkleme> c = new Channel2<DDersEkleme>(ID_MENU))
                {

                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
