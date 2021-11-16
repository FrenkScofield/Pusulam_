using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Upgrade
{
    [GzipCompression]
    public class UpgradeSonucGirController : ApiController
    {
        internal int ID_MENU = (int)EMenu.UpgradeSonucGir;
        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.UpgradeOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object UpgradeSinavListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.UpgradeSinavListele(j);
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
                    return c.DGrup.Kademe3ListelebyKullanici(j);
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

        public Object SinifListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.SinifListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object UpgradeOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.UpgradeOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public Object TKTOgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTOgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TKTOgrenciSonucListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DTKTTest.ID_MENU = ID_MENU;
                    return c.DTKTTest.TKTOgrenciSonucListele(j);
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
                    return c.DSinif.SinifListelebyKullaniciMultiSubeDonem(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object UpgradeOgrenciSoruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.UpgradeOgrenciSoruListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object UpgradeOgrenciSoruPuanKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DUpgradeSoru.ID_MENU = ID_MENU;
                    return c.DUpgradeSoru.UpgradeOgrenciSoruPuanKaydet(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}
