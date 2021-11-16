using Newtonsoft.Json.Linq;
using PusulamBusiness;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PusulamAPI.Controllers
{

    public class DisOgrenciController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpPost]
        public HttpResponseMessage ExternalStudent(JObject j,string key)
        { 
            if(!ApiYetkiKontrol(key))
            {
                HttpResponseMessage x = Request.CreateResponse(HttpStatusCode.Unauthorized, "Api Erişim yetkiniz yok");
                return x;
            }

            else
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, c.DDisOgrenciApi.OnlineSinavKurumDisiOgrenciAtama(j));
                        return response;
                    }
                }
                catch (Exception ex)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }
            }
        }

        private bool ApiYetkiKontrol(string key)
        {
            bool yetki = false;
            string ApiPass = ConfigurationManager.AppSettings.Get("ApiPass").ToString();

            if (key == ApiPass)
            {
                yetki = true;
            }
            return yetki;
            
        }
    }
}