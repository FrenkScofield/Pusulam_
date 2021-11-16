using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Degerlendirme
{
    [GzipCompression]
    public class DegerlendirmeDanismanOgrenciController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DegerlendirmeDanismanOgrenci;

        public Object KademeListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DBirimYetki.ID_MENU = ID_MENU;
                        return c.DBirimYetki.KademeListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object KullaniciTipiListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DBirimYetki.ID_MENU = ID_MENU;
                        return c.DBirimYetki.KullaniciTipiListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Listele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.SoruPuanYorumListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Object Kaydet(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.SoruPuanYorumKaydet(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

        public Object PersonelListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.PersonelListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

        public Object OgrenciListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDegerlendirme.ID_MENU = ID_MENU;
                    return c.DDegerlendirme.OgrenciListele(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Object PuanTurListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDegerlendirme.ID_MENU = ID_MENU;
                    return c.DDegerlendirme.PuanTurListele(j);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Object DanismanOgretmenSinifListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinif.ID_MENU = ID_MENU;
                    return c.DSinif.DanismanOgretmenSinifListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object SinifYorumListele(JObject j)
        {
            {
                try
                {
                    using (Channel c = new Channel())
                    {
                        c.DDegerlendirme.ID_MENU = ID_MENU;
                        return c.DDegerlendirme.SinifYorumListele(j);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
