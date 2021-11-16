using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav.Ogrenci
{
    [GzipCompression]
    public class OnlineSinavOgrenciSinavController : ApiController
    {

        internal int ID_MENU = (int)EMenu.OnlineSinavOgrenciSinav;

        public Object OnlineSinavSoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavSoruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavSoruIsaretle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavSoruIsaretle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object YonergeKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.YonergeKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object YonergeOnay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.YonergeOnay(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
