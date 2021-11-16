using System;

namespace PusulamBusiness.Models.Ogrenci
{
    public class MOgrenci:MBase
    {
        public int ID_OGRENCI { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public string DOGUMTARIHI { get; set; }
        public string YASAY { get; set; }
        public string DURUM { get; set; }
        public string SINIF { get; set; }
        public int ID_KADEME { get; set; }
    }
}