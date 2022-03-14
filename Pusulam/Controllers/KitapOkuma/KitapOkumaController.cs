using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.KitapOkuma;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.KitapOkuma
{
    [GzipCompression]
    public class KitapOkumaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KitapOkuma;

        public Object KitapListele(JObject j)
        {
            try
            {
                using (Channel2<DKitapOkuma> c = new Channel2<DKitapOkuma>(ID_MENU))
                {

                    return c._cs.KitapListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public Object SubeListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
        //        {
        //            return c._cs.SubeListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
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

        public Object SinifAlanListele(JObject j)
        {

            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.SinifAlanListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        //Recording style with the seccond Channel
        public Object Kaydet(JObject j)
        {
            try
            {
                using (Channel2<DKitapOkuma> c = new Channel2<DKitapOkuma>(ID_MENU))
                {
                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaKaydet()
        {
            try
            {
                using (Channel2<DKitapOkuma> c = new Channel2<DKitapOkuma>(ID_MENU))
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
