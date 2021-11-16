
using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PusulamRapor.Sinav.Online
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    //sinavDetay = JsonConvert.DeserializeObject<OnlineSinavDetay>(result).SINAV[0];
    public class SBC
    {
        public int CEVAPNO { get; set; }
        public int ID_CEVAP { get; set; }
        public string CEVAPHTML { get; set; }
        public bool DEGER { get; set; }
    }

    public class SA
    {
        public int SORUNO { get; set; }
        public int ID_SINAVANAHTAR { get; set; }
        public int DOGRUCEVAP { get; set; }
        public string SORUHTML { get; set; }
        public List<SBC> SBC { get; set; }
    }

    public class SD
    {
        public int ID_SINAVDERS { get; set; }
        public int BOLUMNO { get; set; }
        public string TAKMAAD { get; set; }
        public List<SA> SA { get; set; }
    }

    public class SINAV
    {
        public int ID_SINAV { get; set; }
        public string AD { get; set; }
        public List<SD> SD { get; set; }
    }

    public class OnlineSinavDetay
    {
        public List<SINAV> SINAV { get; set; }
    }
}
