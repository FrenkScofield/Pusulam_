using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Ortak;
using System;
using System.Web.Http;

namespace Pusulam.Controllers
{
    [GzipCompression]
    public class LoginController : ApiController
    {
        public Object Login(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DLogin.Login(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object GirisYap(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DLogin.GirisYap(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TechPassKullaniciMi(JObject j)
        {
            try
            {
                DLogin d = new DLogin();
                return d.TechPassKullaniciMi(j);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object TechPassLogin(JObject j)
        {
            try
            {
                DLogin d = new DLogin();
                return d.TechPassLogin(j);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SifreDegistirYetkiKontrol(JObject j)
        {
            try
            {
                using (Channel2<DLogin> c = new Channel2<DLogin>(0))
                {
                    return c._cs.SifreDegistirYetkiKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OgrenciVeliSifreSifirlaKontrol(JObject j)
        {
            try
            {
                using (Channel2<DLogin> c = new Channel2<DLogin>(0))
                {
                    return c._cs.OgrenciVeliSifreSifirlaKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OgrenciVeliSifreSifirla(JObject j)
        {
            try
            {
                using (Channel2<DLogin> c = new Channel2<DLogin>(0))
                {
                    return c._cs.OgrenciVeliSifreSifirla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
