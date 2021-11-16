using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PusulamBusiness.Models.Ogrenci
{
    public class MTakvim
    {
        
        public int id { get; set; }

        
        public int id_kullanicitip { get; set; }

        
        public string grup { get; set; }

        
        public string etkinlik { get; set; }

        
        public string aciklama { get; set; }

        
        public string bastarih { get; set; }

        
        public string bittarih { get; set; }

        
        public int id_kullanici { get; set; }

        
        public string eklemeTarihi { get; set; }

        
        public string kullanicitipi { get; set; }

        
        public string renk { get; set; }

        
        public string tip { get; set; }

        
        public int uzunluk { get; set; }

        
        public int id_takvim { get; set; }

        
        public int id_takvimetkinlik { get; set; }

        
        public bool sinavtakvimi { get; set; }

        
        public bool ustunzeka { get; set; }

        
        public string takvim { get; set; }

        
        public string idkullanicitipler { get; set; }
    }
}