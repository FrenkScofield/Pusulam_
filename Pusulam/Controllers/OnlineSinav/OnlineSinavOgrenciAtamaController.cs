using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.OnlineSinav
{
    [GzipCompression]
    public class OnlineSinavOgrenciAtamaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OnlineSinavOgrenciAtama;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.DonemListele(j);
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

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.SinavTuruListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.SinavListele(j);
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
        public Object SinifAlanListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.SinifAlanListele(j);
                }
            }
            catch (Exception)
            {
                throw;
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

        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.OgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavOgrenciAta(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavOgrenciAta(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavOgrenciAtamaKaldir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavOgrenciAtamaKaldir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OnlineSinavKurumDisiOgrenciAtama(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.OnlineSinavKurumDisiOgrenciAtama(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object APIOnlineSinavOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOnlineSinav.ID_MENU = ID_MENU;
                    return c.DOnlineSinav.APIOnlineSinavOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
