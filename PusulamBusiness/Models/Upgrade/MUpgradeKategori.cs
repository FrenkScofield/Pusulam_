namespace PusulamBusiness.Models.Upgrade
{
    public class MUpgradeKategori : MBase
    {
        public int ID_TKTKATEGORI { get; set; }

        public string AD { get; set; }

        public byte ID_SINAVGRUP { get; set; }

        public string GRUP { get; set; }

        public byte KADEME { get; set; }

        public int PUAN { get; set; }
    }
}