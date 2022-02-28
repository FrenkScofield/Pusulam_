using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using PusulamBusiness.KitapOkuma;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.KitapOkuma
{
    [GzipCompression]
    public class KitapOkumaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.KitapOkuma;

        public Object DonemListele(JObject j)
        {
            try
            {
                using (Channel2<DKitapOkuma> c = new Channel2<DKitapOkuma>(ID_MENU))
                {

                    return c._cs.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        //public Object SubeListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
        //        {
        //            return c._cs.SubeListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public Object Kademe3Listele(JObject j)
        {

            try
            {
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.Kademe3Listele(j);
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
                using (Channel2<DFiltre> c = new Channel2<DFiltre>(ID_MENU))
                {
                    return c._cs.SinifAlanListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public Object SinavListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.SinavListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object OgrenciListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.OgrenciListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object SubeListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSube.ID_MENU = ID_MENU;
        //            return c.DSube.SubeListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object SubeListelebyKullanici(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSube.ID_MENU = ID_MENU;
        //            return c.DSube.SubeListelebyKullanici(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object SinifListelebyKullaniciMultiSube(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSinif.ID_MENU = ID_MENU;
        //            return c.DSinif.SinifListelebyKullaniciMultiSube(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object SinifListelebyKullaniciMultiSubeDonem(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSinif.ID_MENU = ID_MENU;
        //            return c.DSinif.SinifListelebyKullaniciMultiSubeDonem(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        //public Object SinavGrupListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DGrup.ID_MENU = ID_MENU;
        //            return c.DGrup.SinavGrupListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object Kademe3ListelebyKullanici(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DGrup.ID_MENU = ID_MENU;
        //            return c.DGrup.Kademe3ListelebyKullanici(j);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public Object Kademe3ListeleMultiSube(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DGrup.ID_MENU = ID_MENU;
        //            return c.DGrup.Kademe3ListeleMultiSube(j);
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public Object SinifListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSinif.ID_MENU = ID_MENU;
        //            return c.DSinif.SinifListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object OgrenciPuanListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.OgrenciPuanListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object OgrenciPuanKaydet(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.OgrenciPuanKaydet(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object DersPuanSil(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.DersPuanSil(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object SinifListelebyKullaniciDonem(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DSinif.ID_MENU = ID_MENU;
        //            return c.DSinif.SinifListelebyKullaniciDonem(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object KitapcikGetir(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.KitapcikGetir(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object EksikKazanimListesi(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.EksikKazanimListesi(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object EksikKazanimTabloSinavOgrenci(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.EksikKazanimTabloSinavOgrenci(j);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public Object Kaydet(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DOdev.ID_MENU = ID_MENU;
        //            return c.DOdev.OdevKaydet(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object MorpaDersListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DMorpa.ID_MENU = ID_MENU;
        //            return c.DMorpa.MorpaDersListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object MorpaMateryalListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DMorpa.ID_MENU = ID_MENU;
        //            return c.DMorpa.MorpaMateryalListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object MorpaKazanimListele(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DMorpa.ID_MENU = ID_MENU;
        //            return c.DMorpa.MorpaKazanimListele(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public Object MorpaKazanimBul(JObject j)
        //{
        //    try
        //    {
        //        using (Channel c = new Channel())
        //        {
        //            c.DbUniteTarama.ID_MENU = ID_MENU;
        //            return c.DbUniteTarama.MorpaKazanimBul(j);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
