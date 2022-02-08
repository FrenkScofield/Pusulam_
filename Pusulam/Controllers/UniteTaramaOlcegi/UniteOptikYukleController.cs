using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.UniteTarama;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.UniteTaramaOlcegi
{
    public class UniteOptikYukleController : ApiController
    {
        internal int ID_MENU = (int)EMenu.OptikYukle;

        public Object EslesmeyenOgrenciListeGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.EslesmeyenOgrenciListeGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavGrupListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DosyaListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OptikIndir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikIndir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListeleKademeDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaKaydet(JObject j)
        {
            try
            {
                using (Channel2<DUniteOptikYukle> c = new Channel2<DUniteOptikYukle>(ID_MENU))
                {

                    return c._cs.DosyaKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object DosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.DosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SinavDegerlendir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavDegerlendir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object UniteSinavListeleKademeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.UniteSinavListeleKademeDonem(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object OptikBelliMi(JObject j)
        {
            try
            {
                using (Channel2<DUniteOptikYukle> c = new Channel2<DUniteOptikYukle>(ID_MENU))
                {
                    
                    return c._cs.OptikBelliMi(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavListele(j);
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
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SinavTuruListele(j);
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

        public Object TekrarEdenOgrenciGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.TekrarEdenOgrenciGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TekrarEdenOgrenciExcelGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.TekrarEdenOgrenciExcelGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikDosyaGor(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikDosyaGor(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Eslestir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DOgrenci.ID_MENU = ID_MENU;
                    return c.DOgrenci.Eslestir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikDosyaIcerik(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikDosyaIcerik(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int OptikDosyaIcerikDuzenle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikDosyaIcerikDuzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object TcGuncelle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.TcGuncelle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikSatirSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.OptikSatirSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object SorunluTcSayilariniGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DSinav.ID_MENU = ID_MENU;
                    return c.DSinav.SorunluTcSayilariniGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Object OptikListele(JObject j)
        {
            try
            {
                using (Channel2<DUniteOptikTanimla> c = new Channel2<DUniteOptikTanimla>(ID_MENU))
                {
                    
                    return c._cs.OptikListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
