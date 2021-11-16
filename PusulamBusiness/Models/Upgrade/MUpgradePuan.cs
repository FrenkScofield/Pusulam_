namespace PusulamBusiness.Models.Upgrade
{
    public class MUpgradePuan : MBase
    {
        public int ID_TKTPUAN { get; set; }

        public int ID_TKTKATEGORI { get; set; }
        
        public string AD { get; set; }

        public byte ID_SINAVGRUP { get; set; }

        public string GRUP { get; set; }

        public string KADEME { get; set; }

        public int ID_TKTYASARALIGI { get; set; }

        public byte YASYIL1 { get; set; }

        public byte YASYIL2 { get; set; }

        public byte YASAY1 { get; set; }

        public byte YASAY2 { get; set; }

        public int PUAN1 { get; set; }

        public int PUAN2 { get; set; }

        //public int SORUSAYISI { get; set; }

        public string HARF { get; set; }

        public string SEVIYE { get; set; }
    }
}