
namespace PusulamBusiness.Models.Ortak
{
    public class MMenu : MBase
    {
        public int ID_MENU { get; set; }

        public string AD { get; set; }

        public string ACIKLAMA { get; set; }

        public string KOD { get; set; }

        public string URL { get; set; }

        public string RESIM { get; set; }

        public bool GIZLI { get; set; }

        public bool YETKILI { get; set; }

        public string YARDIMHTML { get; set; }        
    }
}