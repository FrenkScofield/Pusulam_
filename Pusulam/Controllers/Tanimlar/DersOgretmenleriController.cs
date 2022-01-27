using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.Tanimlar;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Tanimlar
{
    [GzipCompression]
    public class DersOgretmenleriController : ApiController
    {
        internal int ID_MENU = (int)EMenu.DersOgretmenleri;
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
        public Object OgretmenListele(JObject j)
        {
            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU) )
                {
                   
                    return c._cs.OgretmenSinifListesi(j);
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
        public Object DersOgretmenListele(JObject j)
        {
            try
            {
                using (Channel2<DDersOgretmenleri> c = new Channel2<DDersOgretmenleri>(ID_MENU))
                {
                    return c._cs.DersListele(j);
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
                using (Channel2<DFiltreEk> c = new Channel2<DFiltreEk>(ID_MENU))
                {
                    return c._cs.DersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DersKaydet(JObject j)
        {
            try
            {
                using (Channel2<DDersOgretmenleri> c = new Channel2<DDersOgretmenleri>(ID_MENU))
                {

                    return c._cs.Kaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DuzenleModal(JObject j)
        {
            try
            {
                using (Channel2<DDersOgretmenleri> c = new Channel2<DDersOgretmenleri>(ID_MENU))
                {

                    return c._cs.DuzenleModal(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object DersSil(JObject j)
        {
            try
            {
                using (Channel2<DDersOgretmenleri> c = new Channel2<DDersOgretmenleri>(ID_MENU))
                {
                    return c._cs.DersSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object Duzenle(JObject j)
        {
            try
            {
                using (Channel2<DDersOgretmenleri> c = new Channel2<DDersOgretmenleri>(ID_MENU))
                {
                    return c._cs.Duzenle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Object EgitimTuruListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DFiltre.ID_MENU = ID_MENU;
                    return c.DFiltre.EgitimTuruListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
      

        
       
    }
}
