namespace PusulamBusiness.Models.Sinav
{
    public class MSinavKonu
    {
        public int ID_SINAV { get; set; }
        public string TCKIMLIKNO { get; set; }
        public string KOD { get; set; }
        public string KONUAD { get; set; }
        public float YUZDE { get; set; }
        public int DOGRU { get; set; }
        public int YANLIS { get; set; }
        public int BOS { get; set; }

    }
}