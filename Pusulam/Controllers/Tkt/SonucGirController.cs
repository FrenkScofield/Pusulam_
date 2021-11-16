using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.TKT
{
    [GzipCompression]
    public class SonucGirController : ApiController
    {
        internal int ID_MENU = (int)EMenu.TKTSonucGir;
        public Object TKTTestListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTTestListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TKTKategoriListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTKategoriListele(j);
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

        public Object TKTOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.TKTOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TKTOgrenciCevapListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTOgrenciCevapListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TKTOgrenciCevapKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTOgrenciCevapKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TKTOgrenciCevapSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTOgrenciCevapSil(j);
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
    }
}
