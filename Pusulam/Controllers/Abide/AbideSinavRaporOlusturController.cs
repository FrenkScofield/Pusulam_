using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using PusulamBusiness;
using System;
using System.Web.Http;
using Pusulam.Utility.Filter;

namespace Pusulam.Controllers.Abide
{
    [GzipCompression]
    public class AbideSinavRaporOlusturController : ApiController
    {
        internal int ID_MENU = (int)EMenu.AbideSinavRaporOlustur;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DonemListele(j);
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
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.SinavListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OgrenciListeleRapor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.OgrenciListeleRapor(j);
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
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavGrupListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.SinavGrupListele(j);
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
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinifListelebyKullaniciDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullaniciDonem(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciRaporDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DAbide.ID_MENU = ID_MENU;
                    return c.DAbide.OgrenciRaporDetay(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}