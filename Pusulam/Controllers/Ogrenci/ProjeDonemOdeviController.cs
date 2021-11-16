using Newtonsoft.Json.Linq;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using PusulamBusiness.Enums;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.Ogrenci
{
    [GzipCompression]
    public class ProjeDonemOdeviController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ProjeDonemOdevi;


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

        public Object OgrenciProjeListesi(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.OgrenciProjeListesi(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object OgrenciKademeGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.OgrenciKademeGetir(j);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object ProjeDonemDosyaListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DProjeDonem.ID_MENU = ID_MENU;
                    return c.DProjeDonem.ProjeDonemDosyaListele(j);
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
    }
}
