using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Ortak;
using PusulamBusiness.Yonetim;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Yonetim
{
    public class DuyuruYonetimiController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DuyuruYonetimi;

        public Object DuyuruEkle(JObject j)
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DuyuruGuncelle(JObject j)
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Object DuyuruBanner()
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruBanner();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public Object DuyuruDosya()
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruDosya();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public object DuyuruDetay(JObject j)
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruDetay(j);
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
        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {

                    return c._cs.Kademe3Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {

                    return c._cs.KademeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.KullaniciTipiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Object KullaniciTipiListele(JObject j)
        //{            
        //    try
        //    {
        //        using (Channel2<DBirimYetki> c = new Channel2<DBirimYetki>(ID_MENU))
        //        {
        //            return c._cs.KullaniciTipiListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


    }
}
