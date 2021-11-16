using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.OgrenciBilgiPaylasimi;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.OgrenciBilgiPaylasimi
{
    [GzipCompression]
    public class OgrenciBilgiController : ApiController
    {

        internal int ID_MENU = (int)EMenu.OgrenciBilgiPaylasimi;
        public Object BilgiEkle(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
                {
                    return c._cs.BilgiEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object BilgiListele(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
                {
                    return c._cs.BilgiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BilgiSil(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
                {
                    return c._cs.BilgiSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object BilgiGetir(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
                {
                    return c._cs.BilgiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BilgiDuzenle(JObject j)
        {
            try
            {
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
                {
                    return c._cs.BilgiDuzenle(j);
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
                using (Channel2<DOgrenciBilgiPaylasimi> c = new Channel2<DOgrenciBilgiPaylasimi>(ID_MENU))
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
