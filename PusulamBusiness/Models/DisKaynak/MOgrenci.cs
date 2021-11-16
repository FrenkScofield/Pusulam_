using System;

namespace PusulamBusiness.Models.DisKaynak
{
    public class MOgrenci
    {
        public string KODU { get; set; }
        public string AD { get; set; }
        public string SOYAD { get; set; }
        public int KAMPUSID { get; set; }
        public int SINIFKODU { get; set; }
        public string SINIF { get; set; }
        public string SUBE { get; set; }
        public int KAYITDURUMU { get; set; }
        public string DOGUMTARIHI { get; set; }
        public string CINSIYET { get; set; }
    }
}
