using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.GenelKurul;
using System;
using System.Web.Http;


namespace Pusulam.Controllers.GenelKurul
{
    [GzipCompression]
    public class GenelKurulController : ApiController
    {

        internal int ID_MENU = (int)EMenu.PeriyotEkle;
        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotEkle(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.PeriyotEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.PeriyotListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotSil(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.PeriyotSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object PeriyotMailGonder(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.PeriyotMailGonder(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KararEkle(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.KararEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KararListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.KararListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KararSil(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.KararSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KararListeleGenel(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.KararListeleGenel(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object KademeYetkiListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.KademeYetkiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object MudurSubeListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.MudurSubeListele(j);
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
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object EmailListele(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.EmailListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object EmailSil(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.EmailSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object EmailEkle(JObject j)
        {
            try
            {
                using (Channel2<DGenelKurul> c = new Channel2<DGenelKurul>(ID_MENU))
                {
                    return c._cs.EmailEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
