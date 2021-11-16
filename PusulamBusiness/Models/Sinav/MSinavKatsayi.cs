using System;

namespace PusulamBusiness.Models.Sinav
{
    public class MSinavKatsayi
    {
        public int ID_SINAV { get; set; }
        public int ID_SINAVPUANTURU { get; set; }
        public int ID_SINAVKATSAYI { get; set; }
        public String ID_DERS { get; set; }
        public String PUANTURU { get; set; }
        public String DERS { get; set; }
        public float KATSAYI { get; set; }
    }
}