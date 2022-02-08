using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Tanimlar;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tanimlar
{
    public class DersProgramiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DersProgrami;
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
        public Object SubeListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {

                    return c._cs.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {

                    return c._cs.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object SinifPersonel(JObject j)
        {
            try
            {
                using (Channel2<DDersProgrami> c = new Channel2<DDersProgrami>(ID_MENU))
                {

                    return c._cs.SinifPersonel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DersProgramiGetir(JObject j)
        {
            try
            {
                using (Channel2<DDersProgrami> c = new Channel2<DDersProgrami>(ID_MENU))
                {

                    return c._cs.DersProgramiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DersOgretmenGetir(JObject j)
        {
            try
            {
                using (Channel2<DDersProgrami> c = new Channel2<DDersProgrami>(ID_MENU))
                {

                    return c._cs.DersOgretmenGetir(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Object DersPersonelKaydet(JObject j)
        {
            try
            {
                using (Channel2<DDersProgrami> c = new Channel2<DDersProgrami>(ID_MENU))
                {

                    return c._cs.DersPersonelKaydet(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Object DersProgramKaydet(JObject j)
        {
            try
            {
                using (Channel2<DDersProgrami> c = new Channel2<DDersProgrami>(ID_MENU))
                {

                    return c._cs.DersProgramKaydet(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
