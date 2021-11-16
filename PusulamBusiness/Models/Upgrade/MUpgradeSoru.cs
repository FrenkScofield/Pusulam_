using System;

namespace PusulamBusiness.Models.Upgrade
{
    public class MUpgradeSoru : MBase
    {
        public int ID_TKTSORU { get; set; }

        public int ID_TKTPUAN { get; set; }

        public int ID_TKTKATEGORI { get; set; }

        public string KATEGORIAD { get; set; }

        public string SEVIYE { get; set; }

        public string GRUP { get; set; }

        public byte ID_SINAVGRUP { get; set; }

        public int ID_SATISTURU { get; set; }

        public int? KAZANIMADEDI { get; set; }

        //public int SORUSAYISI { get; set; }

        public string HARF { get; set; }

        public string DOSYAAD { get; set; }

        public string DOSYAUZANTI { get; set; }

        public String KYE_KULLANICI { get; set; }

        public String KYE_TARIH { get; set; }

        public String DONEM { get; set; }

    }
}