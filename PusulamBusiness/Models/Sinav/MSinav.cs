namespace PusulamBusiness.Models.Sinav
{
    public class MSinav
    {
        public int ID_SINAV { get; set; }
        public string KOD { get; set; }
        public string AD { get; set; }
        public string RAPORBASLIK { get; set; }
        public string TUR { get; set; }
        public int ID_KADEME3 { get; set; }
        public string GRUP { get; set; }
        public string SINAVTARIH { get; set; }
        public int DURUM { get; set; }
        public string DONEM { get; set; }
        public int PUAN { get; set; }
        public int HARICIOGRSAY { get; set; }
        public bool AKTIF { get; set; }
        public bool B_KITAPCIK { get; set; }
        public bool OZEL { get; set; }
        public bool FREKANSHESAPLA { get; set; }
        public bool PUANGIZLE { get; set; }
        public bool YUKLEMEKAPAT { get; set; }
        public bool CEVAPANAHTARIYUKLEMEKAPAT { get; set; }
        public bool ONLINESINAV { get; set; }
        public int KATILIM { get; set; }
    }
}