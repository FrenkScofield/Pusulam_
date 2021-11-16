using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Odev
{
    [GzipCompression]
    public class OdevKontrolController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OdevKontrol;

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

        public Object KademeListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.KademeListele(j);
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

        public Object OdevTurListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.OdevTurListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltreEk.ID_MENU = ID_MENU;
                    return c.DFiltreEk.DersListele(j);
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

        public Object Listele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object Sil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevSil(j);
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
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgrenciListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DanismanMi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.DanismanMi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevVerenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevVerenListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevVerenOdevListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevVerenOdevListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevDetayGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OdevKontrolKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OdevKontrolKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public Object Guncelle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.Guncelle();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaMateryalListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaMateryalListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object MorpaKazanimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DMorpa.ID_MENU = ID_MENU;
                    return c.DMorpa.MorpaKazanimListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object OdevIptal(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOdev.ID_MENU = ID_MENU;
                    return c.DOdev.OgrenciOdevIptal(j);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
