using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Rapor.Viu
{
    [GzipCompression]
    public class RehberDanismanYoklamaController : ApiController
    {

        internal int ID_MENU = (int)EMenu.RehberDanismanYoklama;

        public Object OgrenciYoklamaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.OgrenciYoklamaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object RehberDanismanSinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DViu.ID_MENU = ID_MENU;
                    return c.DViu.RehberDanismanSinifListele(j);
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
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Kademe3Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.Kademe3Listele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
