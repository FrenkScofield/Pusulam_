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
    public class DersSaatleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DersSaatleri;
       
        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU) )
                {
                   
                    return c._cs.OgretmenSinifListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Object DersSaatleriListele(JObject j)
        {
            try
            {
                using (Channel2<DDersSaatleri> c = new Channel2<DDersSaatleri>(ID_MENU))
                {

                    return c._cs.DersSaatleriListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }
        public Object DersSaatleriKaydet(JObject j)
        {
            try
            {
                using (Channel2<DDersSaatleri> c = new Channel2<DDersSaatleri>(ID_MENU))
                {

                    return c._cs.DersSaatleriKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }






    }
}
