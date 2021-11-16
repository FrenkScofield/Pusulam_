using System;

namespace PusulamBusiness.Models.Sinav
{
    public class MDosya
    {
        public int ID_SINAVDOSYA { get; set; }
        public int ID_SINAV { get; set; }
        public int ID_SUBE { get; set; }
        public String AD { get; set; }
        public String GUID { get; set; }
        public String KYE_TCKIMLIKNO { get; set; }
        public String KYE_TARIH { get; set; }
        public bool AKTIF { get; set; }
        public bool ONLINESINAV { get; set; }
        public bool YUKLEMEKAPAT { get; set; }
        public String SIL_TCKIMLIKNO { get; set; }
        public String KAD { get; set; }
        public String KSOYAD { get; set; }
        public String SUBEAD { get; set; }
        public String SORUNLUTC { get; set; }
        public String TEKRARLAYANTC { get; set; }
        public int KATILIM { get; set; }
        public int DURUM { get; set; }
    }
}