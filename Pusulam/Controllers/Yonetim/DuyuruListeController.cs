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
    public class DuyuruListeController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DuyuruYonetimi;

        public Object DuyuruListe(JObject j)
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DuyuruAktifPasif (JObject j)
        {
            try
            {
                using (Channel2<DDuyuruYonetimi> c = new Channel2<DDuyuruYonetimi>(ID_MENU))
                {

                    return c._cs.DuyuruAktifPasif(j);
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
