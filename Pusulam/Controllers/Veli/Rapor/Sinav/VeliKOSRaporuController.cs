﻿using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Veli.Rapor.Sinav
{
    [GzipCompression]
    public class VeliKOSRaporuController : ApiController
    {
        internal int ID_MENU = (int)EMenu.VeliKOSRaporu;

        public Object YaziliYoklamaSonuclari(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSonuclariGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSinavListesibyOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSinavListesibyOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSinavListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSinavListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object KullaniciTipiGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DKullanici.ID_MENU = ID_MENU;
                    return c.DKullanici.KullaniciTipiGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SubeListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSube.ID_MENU = ID_MENU;
                    return c.DSube.SubeListelebyKullanici(j);
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
    
        public Object Kademe3ListeleMultiSube(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DGrup.ID_MENU = ID_MENU;
                    return c.DGrup.Kademe3ListeleMultiSube(j);
                }
            }
            catch (Exception)
            {
                throw;
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

        public Object OgrenciListelebyKullanici(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyKullanici(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciListelebyVeli(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.OgrenciListelebyVeli(j);
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

        public Object SinavTuruListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTuruListele(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinavListelebyOgrenci(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListelebyOgrenci(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaSonuclariGetirGenel(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaSonuclariGetirGenel(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object YaziliYoklamaOgrenciKOSListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.YaziliYoklamaOgrenciKOSListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
