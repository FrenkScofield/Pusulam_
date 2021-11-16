using Newtonsoft.Json.Linq;
using PusulamBusiness.Enums;
using Pusulam.Utility.Filter;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace Pusulam.Controllers.DersCalismaProgrami
{
    [GzipCompression]
    public class ProgramHazirlaController : ApiController
    {
        internal int ID_MENU = (int)EMenu.ProgramHazirla;
        public object ProgramEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteKazanimProgramEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public object ProgramSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteKazanimProgramSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public object KazanimListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.KazanimListeleYeni(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        public Object DersUniteKazanimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    c.DDersCalismaProgrami.ID_MENU = ID_MENU;
                    return c.DDersCalismaProgrami.DersUniteKazanimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
