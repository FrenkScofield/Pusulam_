using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Bultenler;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.Bulten
{
    [GzipCompression]
    public class BultenController : ApiController
    {

        internal int ID_MENU = (int)EMenu.Bultenler;
        public Object BultenEkle(JObject j)
        {
            try
            {
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
                {
                    return c._cs.BultenEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object BultenListele(JObject j)
        {
            try
            {
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
                {
                    return c._cs.BultenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BultenSil(JObject j)
        {
            try
            {
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
                {
                    return c._cs.BultenSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object BultenGetir(JObject j)
        {
            try
            {
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
                {
                    return c._cs.BultenGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object BultenDuzenle(JObject j)
        {
            try
            {
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
                {
                    return c._cs.BultenDuzenle(j);
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
                using (Channel2<DBulten> c = new Channel2<DBulten>(ID_MENU))
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
