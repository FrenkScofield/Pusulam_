namespace PusulamBusiness.Models.Upgrade
{
    public class MUpgradeYasAraligi : MBase
    {
        public int ID_TKTYASARALIGI { get; set; }

        public byte YASYIL1 { get; set; }

        public byte YASYIL2 { get; set; }

        public byte YASAY1 { get; set; }

        public byte YASAY2 { get; set; }

        public byte ID_SINAVGRUP { get; set; }

        public string GRUP { get; set; }
    }
}