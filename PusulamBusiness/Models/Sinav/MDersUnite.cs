using System;

namespace PusulamBusiness.Models.Sinav
{
    public class MDersUnite
    {
        public int ID_DERSUNITE { get; set; }
        public int ID_DERS { get; set; }
        public String KOD { get; set; }
        public String AD { get; set; }
        public bool AKTIF { get; set; }
        public bool SECILEBILIR { get; set; }
    }
}