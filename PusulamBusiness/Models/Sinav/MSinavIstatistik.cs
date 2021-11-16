using System.Collections.Generic;

namespace PusulamBusiness.Models.Sinav
{
    public class MSinavIstatistik
    {
        public List<IstatistikGenel> istatistikGenel { get; set; }
        public List<IstatistikDers> istatistikDers { get; set; }
        public List<MSinavKonu> sinavKonu { get; set; }

        public class IstatistikGenel
        {
            public int ID_SINAV { get; set; }
            public float TOPLAMNET { get; set; }
            public float PUAN { get; set; }
            public int ID_SINAVPUANTUR { get; set; }
            public string PUANTURU { get; set; }
            public string SINAVAD { get; set; }
        }
        public class IstatistikDers
        {
            public int ID_SINAV { get; set; }
            public string DERSAD { get; set; }
            public float NET { get; set; }
            public int ID_SINAVPUANTUR { get; set; }
            public string PUANTURU { get; set; }
            public string SINAVAD { get; set; }
        }
    }
}