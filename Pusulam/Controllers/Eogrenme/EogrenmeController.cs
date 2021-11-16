using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Sinav;
using PusulamBusiness.Eogrenme;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Eogrenme
{
    //[GzipCompression]
    public class EogrenmeController : ApiController
    {

        internal int ID_MENU = (int)EMenu.Eogrenme;
        public Object KullaniciTipGetir(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.KullaniciTipGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {

                    return c._cs.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Object KademeDersGetir(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.DersGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object HaftaninKazanimi(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.DersUniteGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Listele(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.Listele(j);
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
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Guncelle(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.Guncelle(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Object DosyaDetay(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.DosyaDetay(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Object DosyaSil(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.DosyaSil(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Object LogKaydet(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.LogKaydet(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Object LogListele(JObject j)
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.LogListele(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public Object DosyaKaydet()
        {
            try
            {
                using (Channel2<DEogrenme> c = new Channel2<DEogrenme>(ID_MENU))
                {
                    return c._cs.DosyaKaydet();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}