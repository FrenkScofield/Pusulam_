using System;
using PusulamBusiness;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;
using PusulamBusiness.Models.Ortak;
using PusulamBusiness.Ortak;

namespace PusulamSinav.Controllers
{
    public class AnasayfaController : ApiController
    {
        // GET api/<controller>
        protected string conStr = ConfigurationManager.ConnectionStrings["pusulamCS"].ConnectionString;
        public Object KullaniciYetkiListele(JObject j)
        {
            try
            {
                using (Channel2<DKullaniciYetki> c = new Channel2<DKullaniciYetki>(1333))
                {
                    return c._cs.KullaniciListele(j);
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsTest()
        {
            var result = ConfigurationManager.AppSettings["IsTest"];
            return result == null ? false : Convert.ToBoolean(result);
        }
    }
}