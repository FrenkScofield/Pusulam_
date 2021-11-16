using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.RehberlikEnvanter
{
    [GzipCompression]
    public class RehberlikEnvanterController : ApiController
    {
        internal int ID_MENU = (int)EMenu.RehberlikEnvanterTanimlama;
        public Object RehberlikEnvanterEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DRehberlikEnvanter.ID_MENU = ID_MENU;
                    return c.DRehberlikEnvanter.RehberlikEnvanterEkle(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object Kademe3ListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDonem.ID_MENU = ID_MENU;
                    return c.DDonem.DonemListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
