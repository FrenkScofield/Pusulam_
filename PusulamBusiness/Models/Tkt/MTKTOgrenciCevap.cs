namespace PusulamBusiness.Models.TKT
{
    public class MTKTOgrenciCevap
    {
        public int ID_TKTSORU { get; set; }
        public int SORUNO { get; set; }
        public int ID_TKTTEST { get; set; }
        public string TEST { get; set; }
        public int ID_KATEGORI { get; set; }
        public string KATEGORI { get; set; }
        public int ID_ALTKATEGORI { get; set; }
        public string ALTKATEGORI { get; set; }
        public int ID_SORUTUR { get; set; }
        public string SORUTURU { get; set; }
        public string DOGRUCEVAP { get; set; }
        public string OGRENCICEVAP { get; set; }
        public string SONUC { get; set; }
        public string TARIH { get; set; }
    }
}