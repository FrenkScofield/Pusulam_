using Newtonsoft.Json.Linq;
using PusulamBusiness;
using PusulamBusiness.DisKaynak;
using System;
using System.Web.Http;

namespace PusulamAPI.Controllers
{
    public class DisKaynakController : ApiController
    {
        public Object ApiKeyAl(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.ApiKeyAl(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.OgrenciListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.OgretmenListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.SinifListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SubeListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.SubeListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SifreCoz(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.SifreCoz(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciGetir(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.OgrenciGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TumOgretmenListe(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.TumOgretmenListe(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifOgretmenDersListele(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.SinifOgretmenDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifGetir(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.SinifGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgretmenGetir(JObject j)
        {
            try
            {
                using (Channel2<DDisKaynak> c = new Channel2<DDisKaynak>(1))
                {
                    return c._cs.OgretmenGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
