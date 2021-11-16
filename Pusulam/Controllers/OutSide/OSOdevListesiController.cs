using Newtonsoft.Json.Linq;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OutSide
{
    public class OSOdevListesiController : ApiController
    {
        string APIKEY = "7da3b771-8007-455b-9bf0-595528512aa0";

        public Object OsOdevListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.OsOdevListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OsOdevTamamla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.OdevTamamla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TamamlandiListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.TamamlandiListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object OdevIptal(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.OgrenciOdevIptal(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Object OsOdevDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.OsOdevDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Object OdevDosyaYukle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DOSOdev.OdevDosyaYukle();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    j.Add("APIKEY", APIKEY);
                    return c.DOSOdev.OdevDosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object MorpaLinkGetirGiris(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.DHedef.MorpaLinkGetirGiris(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
