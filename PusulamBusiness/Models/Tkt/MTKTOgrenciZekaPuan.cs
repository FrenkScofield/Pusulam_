namespace PusulamBusiness.Models.Tkt
{
    public class MTKTOgrenciZekaPuan
    {
        public int ID_TKTZEKAPUAN { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string ADSOYAD { get; set; }
        public int YASAY { get; set; }
        public int ZEKAPUANI { get; set; }
        public int ID_TKTTEST { get; set; }

        public int ONZEKAPUANI { get; set; }
        public int ARAZEKAPUANI { get; set; }
        public int SONZEKAPUANI { get; set; }

        public string ONZEKAKATEGORI { get; set; }
        public string ARAZEKAKATEGORI { get; set; }
        public string SONZEKAKATEGORI { get; set; }

        
    }
}