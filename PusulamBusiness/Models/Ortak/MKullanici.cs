
namespace PusulamBusiness.Models.Ortak
{
    public class MKullanici : MBase
    {
        public int ID_KULLANICI { get; set; }
        public int XBASEOGRENCIID { get; set; }
        public int XBASEPERSONELID { get; set; }
        public int XBASEOGRETMENID { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public int ID_SUBE { get; set; }
        public int ID_KULLANICITIPI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string KGUID { get; set; }
        public string KULLANICIAD { get; set; }
        public string SIFRE { get; set; }
        public bool AKTIF { get; set; }
        public string OTURUM { get; set; }
        public string ADSOYAD { get; set; }
    }
}