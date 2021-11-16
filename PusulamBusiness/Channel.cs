using PusulamBusiness.Abide;
using PusulamBusiness.AkilliOgretimBarkod;
using PusulamBusiness.Assessment;
using PusulamBusiness.CurpusStudy;
using PusulamBusiness.Degerlendirme;
using PusulamBusiness.DisOgrenciApi;
using PusulamBusiness.Etut;
using PusulamBusiness.Frekans;
using PusulamBusiness.GenelListeler;
using PusulamBusiness.MentulUp;
using PusulamBusiness.Mobile;
using PusulamBusiness.Morpa;
using PusulamBusiness.Odev;
using PusulamBusiness.Ogrenci;
using PusulamBusiness.OnlineDers;
using PusulamBusiness.Ortak;
using PusulamBusiness.OSYM;
using PusulamBusiness.OutSide;
using PusulamBusiness.Performans;
using PusulamBusiness.Personel;
using PusulamBusiness.Rapor;
using PusulamBusiness.Rapor.Analiz;
using PusulamBusiness.Rapor.Viu;
using PusulamBusiness.Rapor.Yazili;
using PusulamBusiness.Rehberlik.RehberlikOnline;
using PusulamBusiness.RehberlikEnvanter;
using PusulamBusiness.Sinav;
using PusulamBusiness.Takvim;
using PusulamBusiness.Tatil;
using PusulamBusiness.TKT;
using PusulamBusiness.UniteTarama;
using PusulamBusiness.Upgrade;
using PusulamBusiness.Veli;
using PusulamBusiness.Yardim;
using PusulamBusiness.Yazili;
using PusulamBusiness.Yonetim;
using PusulamBusiness.YYS;
using PusulamBusiness.ZekaTest;
using PusulamBusiness.ZumreCalisma;
using System;

namespace PusulamBusiness
{
    public class Channel : IDisposable
    {

        private DAbide dAbide = new DAbide();
        public DAbide DAbide
        {
            get
            {
                if (dAbide == null)
                {
                    dAbide = new DAbide();
                }
                return dAbide;
            }
        }

        private DAkilliOgretimBarkod dAkilliOgretimBarkod = new DAkilliOgretimBarkod();
        public DAkilliOgretimBarkod DAkilliOgretimBarkod
        {
            get
            {
                if (dAkilliOgretimBarkod == null)
                {
                    dAkilliOgretimBarkod = new DAkilliOgretimBarkod();
                }
                return dAkilliOgretimBarkod;
            }
        }

        private DAssessment dAssessment = new DAssessment();
        public DAssessment DAssessment
        {
            get
            {
                if (dAssessment == null)
                {
                    dAssessment = new DAssessment();
                }
                return dAssessment;
            }
        }

        private DCurpusStudy dCurpusStudy = new DCurpusStudy();
        public DCurpusStudy DCurpusStudy
        {
            get
            {
                if (dCurpusStudy == null)
                {
                    dCurpusStudy = new DCurpusStudy();
                }
                return dCurpusStudy;
            }
        }

        private DDegerlendirme dDegerlendirme = new DDegerlendirme();
        public DDegerlendirme DDegerlendirme
        {
            get
            {
                if (dDegerlendirme == null)
                {
                    dDegerlendirme = new DDegerlendirme();
                }
                return dDegerlendirme;
            }
        }

        private DDersCalismaProgrami dDersCalismaProgrami = new DDersCalismaProgrami();
        public DDersCalismaProgrami DDersCalismaProgrami
        {
            get
            {
                if (dDersCalismaProgrami == null)
                {
                    dDersCalismaProgrami = new DDersCalismaProgrami();
                }
                return dDersCalismaProgrami;
            }
        }

        private DEtut dEtut = new DEtut();
        public DEtut DEtut
        {
            get
            {
                if (dEtut == null)
                {
                    dEtut = new DEtut();
                }
                return dEtut;
            }
        }

        private DFrekans dFrekans = new DFrekans();
        public DFrekans DFrekans
        {
            get
            {
                if (dFrekans == null)
                {
                    dFrekans = new DFrekans();
                }
                return dFrekans;
            }
        }

        private DFrekansV2 dFrekansV2 = new DFrekansV2();
        public DFrekansV2 DFrekansV2
        {
            get
            {
                if (dFrekansV2 == null)
                {
                    dFrekansV2 = new DFrekansV2();
                }
                return dFrekansV2;
            }
        }

        private DGenelListeler dGenelListeler = new DGenelListeler();
        public DGenelListeler DGenelListeler
        {
            get
            {
                if (dGenelListeler == null)
                {
                    dGenelListeler = new DGenelListeler();
                }
                return dGenelListeler;
            }
        }

        private DMentalUp dMentalUp = new DMentalUp();
        public DMentalUp DMentalUp
        {
            get
            {
                if (dMentalUp == null)
                {
                    dMentalUp = new DMentalUp();
                }
                return dMentalUp;
            }
        }

        private DMorpa dMorpa = new DMorpa();
        public DMorpa DMorpa
        {
            get
            {
                if (dMorpa == null)
                {
                    dMorpa = new DMorpa();
                }
                return dMorpa;
            }
        }

        private DOdev dOdev = new DOdev();
        public DOdev DOdev
        {
            get
            {
                if (dOdev == null)
                {
                    dOdev = new DOdev();
                }
                return dOdev;
            }
        }

        private DYillik dYillik = new DYillik();
        public DYillik DYillik
        {
            get
            {
                if (dYillik == null)
                {
                    dYillik = new DYillik();
                }
                return dYillik;
            }
        }

        private DYetenekGelisim dYetenekGelisim = new DYetenekGelisim();
        public DYetenekGelisim DYetenekGelisim
        {
            get
            {
                if (dYetenekGelisim == null)
                {
                    dYetenekGelisim = new DYetenekGelisim();
                }
                return dYetenekGelisim;
            }
        }

        private DDemoOgrenci dDemoOgrenci = new DDemoOgrenci();
        public DDemoOgrenci DDemoOgrenci
        {
            get
            {
                if (dDemoOgrenci == null)
                {
                    dDemoOgrenci = new DDemoOgrenci();
                }
                return dDemoOgrenci;
            }
        }

        private DAkademikTakvim dAkademikTakvim = new DAkademikTakvim();
        public DAkademikTakvim DAkademikTakvim
        {
            get
            {
                if (dAkademikTakvim == null)
                {
                    dAkademikTakvim = new DAkademikTakvim();
                }
                return dAkademikTakvim;
            }
        }

        private DHedef dHedef = new DHedef();
        public DHedef DHedef
        {
            get
            {
                if (dHedef == null)
                {
                    dHedef = new DHedef();
                }
                return dHedef;
            }
        }

        private DOdevTakibi dOdevTakibi = new DOdevTakibi();
        public DOdevTakibi DOdevTakibi
        {
            get
            {
                if (dOdevTakibi == null)
                {
                    dOdevTakibi = new DOdevTakibi();
                }
                return dOdevTakibi;
            }
        }

        private DOgrenci dOgrenci = new DOgrenci();
        public DOgrenci DOgrenci
        {
            get
            {
                if (dOgrenci == null)
                {
                    dOgrenci = new DOgrenci();
                }
                return dOgrenci;
            }
        }

        private DOgrenciDegerlendirme dOgrenciDegerlendirme = new DOgrenciDegerlendirme();
        public DOgrenciDegerlendirme DOgrenciDegerlendirme
        {
            get
            {
                if (dOgrenciDegerlendirme == null)
                {
                    dOgrenciDegerlendirme = new DOgrenciDegerlendirme();
                }
                return dOgrenciDegerlendirme;
            }
        }

        private DOgrenciIsleri dOgrenciIsleri = new DOgrenciIsleri();
        public DOgrenciIsleri DOgrenciIsleri
        {
            get
            {
                if (dOgrenciIsleri == null)
                {
                    dOgrenciIsleri = new DOgrenciIsleri();
                }
                return dOgrenciIsleri;
            }
        }

        private DOnlineSinav dOnlineSinav = new DOnlineSinav();
        public DOnlineSinav DOnlineSinav
        {
            get
            {
                if (dOnlineSinav == null)
                {
                    dOnlineSinav = new DOnlineSinav();
                }
                return dOnlineSinav;
            }
        }

        private DSorunBildir dSorunBildir = new DSorunBildir();
        public DSorunBildir DSorunBildir
        {
            get
            {
                if (dSorunBildir == null)
                {
                    dSorunBildir = new DSorunBildir();
                }
                return dSorunBildir;
            }
        }      

        private DOnlineDers dOnlineDers = new DOnlineDers();
        public DOnlineDers DOnlineDers
        {
            get
            {
                if (dOnlineDers == null)
                {
                    dOnlineDers = new DOnlineDers();
                }
                return dOnlineDers;
            }
        }

        private DOSYM dOSYM = new DOSYM();
        public DOSYM DOSYM
        {
            get
            {
                if (dOSYM == null)
                {
                    dOSYM = new DOSYM();
                }
                return dOSYM;
            }
        }

        private DOSOdev dOSOdev = new DOSOdev();
        public DOSOdev DOSOdev
        {
            get
            {
                if (dOSOdev == null)
                {
                    dOSOdev = new DOSOdev();
                }
                return dOSOdev;
            }
        }

        private DProjeDonem dProjeDonem = new DProjeDonem();
        public DProjeDonem DProjeDonem
        {
            get
            {
                if (dProjeDonem == null)
                {
                    dProjeDonem = new DProjeDonem();
                }
                return dProjeDonem;
            }
        }

        private DSinavaKatilmayanOgrenciler dSinavaKatilmayanOgrenciler = new DSinavaKatilmayanOgrenciler();
        public DSinavaKatilmayanOgrenciler DSinavaKatilmayanOgrenciler
        {
            get
            {
                if (dSinavaKatilmayanOgrenciler == null)
                {
                    dSinavaKatilmayanOgrenciler = new DSinavaKatilmayanOgrenciler();
                }
                return dSinavaKatilmayanOgrenciler;
            }
        }

        private DSinavAnaliz dSinavAnaliz = new DSinavAnaliz();
        public DSinavAnaliz DSinavAnaliz
        {
            get
            {
                if (dSinavAnaliz == null)
                {
                    dSinavAnaliz = new DSinavAnaliz();
                }
                return dSinavAnaliz;
            }
        }

        private DSinifBazindaKazanimAnalizi dSinifBazindaKazanimAnalizi = new DSinifBazindaKazanimAnalizi();
        public DSinifBazindaKazanimAnalizi DSinifBazindaKazanimAnalizi
        {
            get
            {
                if (dSinifBazindaKazanimAnalizi == null)
                {
                    dSinifBazindaKazanimAnalizi = new DSinifBazindaKazanimAnalizi();
                }
                return dSinifBazindaKazanimAnalizi;
            }
        }

        private DSinavAnalizi dSinavAnalizi = new DSinavAnalizi();
        public DSinavAnalizi DSinavAnalizi
        {
            get
            {
                if (dSinavAnalizi == null)
                {
                    dSinavAnalizi = new DSinavAnalizi();
                }
                return dSinavAnalizi;
            }
        }

        private DViu dViu = new DViu();
        public DViu DViu
        {
            get
            {
                if (dViu == null)
                {
                    dViu = new DViu();
                }
                return dViu;
            }
        }

        private DGenelKazanimAnalizi dGenelKazanimAnalizi = new DGenelKazanimAnalizi();
        public DGenelKazanimAnalizi DGenelKazanimAnalizi
        {
            get
            {
                if (dGenelKazanimAnalizi == null)
                {
                    dGenelKazanimAnalizi = new DGenelKazanimAnalizi();
                }
                return dGenelKazanimAnalizi;
            }
        }

        private DKazanimAnalizi dKazanimAnalizi = new DKazanimAnalizi();
        public DKazanimAnalizi DKazanimAnalizi
        {
            get
            {
                if (dKazanimAnalizi == null)
                {
                    dKazanimAnalizi = new DKazanimAnalizi();
                }
                return dKazanimAnalizi;
            }
        }

        private DKazanimAnaliziOO dKazanimAnaliziOO = new DKazanimAnaliziOO();
        public DKazanimAnaliziOO DKazanimAnaliziOO
        {
            get
            {
                if (dKazanimAnaliziOO == null)
                {
                    dKazanimAnaliziOO = new DKazanimAnaliziOO();
                }
                return dKazanimAnaliziOO;
            }
        }

        private DOrtalamaListesi dOrtalamaListesi = new DOrtalamaListesi();
        public DOrtalamaListesi DOrtalamaListesi
        {
            get
            {
                if (dOrtalamaListesi == null)
                {
                    dOrtalamaListesi = new DOrtalamaListesi();
                }
                return dOrtalamaListesi;
            }
        }

        private DPuanaGoreYaziliSonuclari dPuanaGoreYaziliSonuclari = new DPuanaGoreYaziliSonuclari();
        public DPuanaGoreYaziliSonuclari DPuanaGoreYaziliSonuclari
        {
            get
            {
                if (dPuanaGoreYaziliSonuclari == null)
                {
                    dPuanaGoreYaziliSonuclari = new DPuanaGoreYaziliSonuclari();
                }
                return dPuanaGoreYaziliSonuclari;
            }
        }

        private DTopluYaziliYoklamaSonuclari dTopluYaziliYoklamaSonuclari = new DTopluYaziliYoklamaSonuclari();
        public DTopluYaziliYoklamaSonuclari DTopluYaziliYoklamaSonuclari
        {
            get
            {
                if (dTopluYaziliYoklamaSonuclari == null)
                {
                    dTopluYaziliYoklamaSonuclari = new DTopluYaziliYoklamaSonuclari();
                }
                return dTopluYaziliYoklamaSonuclari;
            }
        }

        private DDenemeSinavRaporlari dDenemeSinavRaporlari = new DDenemeSinavRaporlari();
        public DDenemeSinavRaporlari DDenemeSinavRaporlari
        {
            get
            {
                if (dDenemeSinavRaporlari == null)
                {
                    dDenemeSinavRaporlari = new DDenemeSinavRaporlari();
                }
                return dDenemeSinavRaporlari;
            }
        }

        private DOkulNetPuanOrtalamalari dOkulNetPuanOrtalamalari = new DOkulNetPuanOrtalamalari();
        public DOkulNetPuanOrtalamalari DOkulNetPuanOrtalamalari
        {
            get
            {
                if (dOkulNetPuanOrtalamalari == null)
                {
                    dOkulNetPuanOrtalamalari = new DOkulNetPuanOrtalamalari();
                }
                return dOkulNetPuanOrtalamalari;
            }
        }

        private DOnlineTestRaporlari dOnlineTestRaporlari = new DOnlineTestRaporlari();
        public DOnlineTestRaporlari DOnlineTestRaporlari
        {
            get
            {
                if (dOnlineTestRaporlari == null)
                {
                    dOnlineTestRaporlari = new DOnlineTestRaporlari();
                }
                return dOnlineTestRaporlari;
            }
        }

        private DRehberlikOnlineYukle dRehberlikOnlineYukle = new DRehberlikOnlineYukle();
        public DRehberlikOnlineYukle DRehberlikOnlineYukle
        {
            get
            {
                if (dRehberlikOnlineYukle == null)
                {
                    dRehberlikOnlineYukle = new DRehberlikOnlineYukle();
                }
                return dRehberlikOnlineYukle;
            }
        }

        private DRehberlikEnvanter dRehberlikEnvanter = new DRehberlikEnvanter();
        public DRehberlikEnvanter DRehberlikEnvanter
        {
            get
            {
                if (dRehberlikEnvanter == null)
                {
                    dRehberlikEnvanter = new DRehberlikEnvanter();
                }
                return dRehberlikEnvanter;
            }
        }

        private DSinav dSinav = new DSinav();
        public DSinav DSinav
        {
            get
            {
                if (dSinav == null)
                {
                    dSinav = new DSinav();
                }
                return dSinav;
            }
        }

        private DSinavIstatistik dSinavIstatistik = new DSinavIstatistik();
        public DSinavIstatistik DSinavIstatistik
        {
            get
            {
                if (dSinavIstatistik == null)
                {
                    dSinavIstatistik = new DSinavIstatistik();
                }
                return dSinavIstatistik;
            }
        }

        private DKarmaListe dKarmaListe = new DKarmaListe();
        public DKarmaListe DKarmaListe
        {
            get
            {
                if (dKarmaListe == null)
                {
                    dKarmaListe = new DKarmaListe();
                }
                return dKarmaListe;
            }
        }

        private DDersUnite dDersUnite = new DDersUnite();
        public DDersUnite DDersUnite
        {
            get
            {
                if (dDersUnite == null)
                {
                    dDersUnite = new DDersUnite();
                }
                return dDersUnite;
            }
        }

        private DBursluluk dBursluluk = new DBursluluk();
        public DBursluluk DBursluluk
        {
            get
            {
                if (dBursluluk == null)
                {
                    dBursluluk = new DBursluluk();
                }
                return dBursluluk;
            }
        }

        private CevapAnahtari cevapAnahtari = new CevapAnahtari();
        public CevapAnahtari CevapAnahtari
        {
            get
            {
                if (cevapAnahtari == null)
                {
                    cevapAnahtari = new CevapAnahtari();
                }
                return cevapAnahtari;
            }
        }

        private DSinifListeRaporu dSinifListeRaporu = new DSinifListeRaporu();
        public DSinifListeRaporu DSinifListeRaporu
        {
            get
            {
                if (dSinifListeRaporu == null)
                {
                    dSinifListeRaporu = new DSinifListeRaporu();
                }
                return dSinifListeRaporu;
            }
        }

        private DTakvim dTakvim = new DTakvim();
        public DTakvim DTakvim
        {
            get
            {
                if (dTakvim == null)
                {
                    dTakvim = new DTakvim();
                }
                return dTakvim;
            }
        }

        private DTatil dTatil = new DTatil();
        public DTatil DTatil
        {
            get
            {
                if (dTatil == null)
                {
                    dTatil = new DTatil();
                }
                return dTatil;
            }
        }

        private DTKTTest dTKTTest = new DTKTTest();
        public DTKTTest DTKTTest
        {
            get
            {
                if (dTKTTest == null)
                {
                    dTKTTest = new DTKTTest();
                }
                return dTKTTest;
            }
        }

        private DbUniteTarama dbUniteTarama = new DbUniteTarama();
        public DbUniteTarama DbUniteTarama
        {
            get
            {
                if (dbUniteTarama == null)
                {
                    dbUniteTarama = new DbUniteTarama();
                }
                return dbUniteTarama;
            }
        }

        private DUpgradeSoru dUpgradeSoru = new DUpgradeSoru();
        public DUpgradeSoru DUpgradeSoru
        {
            get
            {
                if (dUpgradeSoru == null)
                {
                    dUpgradeSoru = new DUpgradeSoru();
                }
                return dUpgradeSoru;
            }
        }

        private DFatura dFatura = new DFatura();
        public DFatura DFatura
        {
            get
            {
                if (dFatura == null)
                {
                    dFatura = new DFatura();
                }
                return dFatura;
            }
        }

        private DOgrenciHarcama dOgrenciHarcama = new DOgrenciHarcama();
        public DOgrenciHarcama DOgrenciHarcama
        {
            get
            {
                if (dOgrenciHarcama == null)
                {
                    dOgrenciHarcama = new DOgrenciHarcama();
                }
                return dOgrenciHarcama;
            }
        }     

        private DYardimHazirla dYardimHazirla = new DYardimHazirla();
        public DYardimHazirla DYardimHazirla
        {
            get
            {
                if (dYardimHazirla == null)
                {
                    dYardimHazirla = new DYardimHazirla();
                }
                return dYardimHazirla;
            }
        }

        private DYaziliYoklama dYaziliYoklama = new DYaziliYoklama();
        public DYaziliYoklama DYaziliYoklama
        {
            get
            {
                if (dYaziliYoklama == null)
                {
                    dYaziliYoklama = new DYaziliYoklama();
                }
                return dYaziliYoklama;
            }
        }

        private DOzelSayfa dOzelSayfa = new DOzelSayfa();
        public DOzelSayfa DOzelSayfa
        {
            get
            {
                if (dOzelSayfa == null)
                {
                    dOzelSayfa = new DOzelSayfa();
                }
                return dOzelSayfa;
            }
        }

        private DParametre dParametre = new DParametre();
        public DParametre DParametre
        {
            get
            {
                if (dParametre == null)
                {
                    dParametre = new DParametre();
                }
                return dParametre;
            }
        }

        private DUyari dUyari = new DUyari();
        public DUyari DUyari
        {
            get
            {
                if (dUyari == null)
                {
                    dUyari = new DUyari();
                }
                return dUyari;
            }
        }

        private DYys dYys = new DYys();
        public DYys DYys
        {
            get
            {
                if (dYys == null)
                {
                    dYys = new DYys();
                }
                return dYys;
            }
        }

        private DZekaTest dZekaTest = new DZekaTest();
        public DZekaTest DZekaTest
        {
            get
            {
                if (dZekaTest == null)
                {
                    dZekaTest = new DZekaTest();
                }
                return dZekaTest;
            }
        }

        private DZumreCalisma dZumreCalisma = new DZumreCalisma();
        public DZumreCalisma DZumreCalisma
        {
            get
            {
                if (dZumreCalisma == null)
                {
                    dZumreCalisma = new DZumreCalisma();
                }
                return dZumreCalisma;
            }
        }

        private DAnasayfa dAnasayfa = new DAnasayfa();
        public DAnasayfa DAnasayfa
        {
            get
            {
                if (dAnasayfa == null)
                {
                    dAnasayfa = new DAnasayfa();
                }
                return dAnasayfa;
            }
        }

        private DFiltre dFiltre = new DFiltre();
        public DFiltre DFiltre
        {
            get
            {
                if (dFiltre == null)
                {
                    dFiltre = new DFiltre();
                }
                return dFiltre;
            }
        }

        private DFiltreEk dFiltreEk = new DFiltreEk();
        public DFiltreEk DFiltreEk
        {
            get
            {
                if (dFiltreEk == null)
                {
                    dFiltreEk = new DFiltreEk();
                }
                return dFiltreEk;
            }
        }

        private DBirimYetki dBirimYetki = new DBirimYetki();
        public DBirimYetki DBirimYetki
        {
            get
            {
                if (dBirimYetki == null)
                {
                    dBirimYetki = new DBirimYetki();
                }
                return dBirimYetki;
            }
        }
        private DMobilBirimYetki dMobilBirimYetki = new DMobilBirimYetki();
        public DMobilBirimYetki DMobilBirimYetki
        {
            get
            {
                if (dMobilBirimYetki == null)
                {
                    dMobilBirimYetki = new DMobilBirimYetki();
                }
                return dMobilBirimYetki;
            }
        }

        private DDonem dDonem = new DDonem();
        public DDonem DDonem
        {
            get
            {
                if (dDonem == null)
                {
                    dDonem = new DDonem();
                }
                return dDonem;
            }
        }

        private DGrup dGrup = new DGrup();
        public DGrup DGrup
        {
            get
            {
                if (dGrup == null)
                {
                    dGrup = new DGrup();
                }
                return dGrup;
            }
        }

        private DKullanici dKullanici = new DKullanici();
        public DKullanici DKullanici
        {
            get
            {
                if (dKullanici == null)
                {
                    dKullanici = new DKullanici();
                }
                return dKullanici;
            }
        }


        private DKullaniciYetki dKullaniciYetki = new DKullaniciYetki();
        public DKullaniciYetki DKullaniciYetki
        {
            get
            {
                if (dKullaniciYetki == null)
                {
                    dKullaniciYetki = new DKullaniciYetki();
                }
                return dKullaniciYetki;
            }
        }

        private DLogin dLogin = new DLogin();
        public DLogin DLogin
        {
            get
            {
                if (dLogin == null)
                {
                    dLogin = new DLogin();
                }
                return dLogin;
            }
        }


        private DMenu dMenu = new DMenu();
        public DMenu DMenu
        {
            get
            {
                if (dMenu == null)
                {
                    dMenu = new DMenu();
                }
                return dMenu;
            }
        }


        private DSinif dSinif = new DSinif();
        public DSinif DSinif
        {
            get
            {
                if (dSinif == null)
                {
                    dSinif = new DSinif();
                }
                return dSinif;
            }
        }


        private DSube dSube = new DSube();
        public DSube DSube
        {
            get
            {
                if (dSube == null)
                {
                    dSube = new DSube();
                }
                return dSube;
            }
        }


        private DZekaTuru dZekaTuru = new DZekaTuru();

        public DZekaTuru DZekaTuru
        {
            get
            {
                if (dZekaTuru == null)
                {
                    dZekaTuru = new DZekaTuru();
                }
                return dZekaTuru;
            }
        }


        private DDisOgrenciApi dDisOgrenciApi = new DDisOgrenciApi();

        public DDisOgrenciApi DDisOgrenciApi
        {
            get
            {
                if (dDisOgrenciApi == null)
                {
                    dDisOgrenciApi = new DDisOgrenciApi();
                }
                return dDisOgrenciApi;
            }
        }
        private DPerformans dPerformans = new DPerformans();
        public DPerformans DPerformans
        {
            get
            {
                if (dPerformans == null)
                {
                    dPerformans = new DPerformans();
                }
                return dPerformans;
            }
        }

        private MAuth mAuth = new MAuth();

        public MAuth MAuth
        {
            get
            {
                if (mAuth == null)
                {
                    mAuth = new MAuth();
                }
                return mAuth;
            }
        }

        private MBulten mBulten = new MBulten();

        public MBulten MBulten
        {
            get
            {
                if (mBulten == null)
                {
                    mBulten = new MBulten();
                }
                return mBulten;
            }
        }

        private MDersProgram mDersProgram = new MDersProgram();

        public MDersProgram MDersProgram
        {
            get
            {
                if (mDersProgram == null)
                {
                    mDersProgram = new MDersProgram();
                }
                return mDersProgram;
            }
        }

        private MHedef mHedef = new MHedef();

        public MHedef MHedef
        {
            get
            {
                if (mHedef == null)
                {
                    mHedef = new MHedef();
                }
                return mHedef;
            }
        }

        private MMenu mMenu = new MMenu();

        public MMenu MMenu
        {
            get
            {
                if (mMenu == null)
                {
                    mMenu = new MMenu();
                }
                return mMenu;
            }
        }

        private MMorpa mMorpa = new MMorpa();

        public MMorpa MMorpa
        {
            get
            {
                if (mMorpa == null)
                {
                    mMorpa = new MMorpa();
                }
                return mMorpa;
            }
        }

        private MOBS mOBS = new MOBS();

        public MOBS MOBS
        {
            get
            {
                if (mOBS == null)
                {
                    mOBS = new MOBS();
                }
                return mOBS;
            }
        }

        private MOgrenciProje mOgrenciProje = new MOgrenciProje();

        public MOgrenciProje MOgrenciProje
        {
            get
            {
                if (mOgrenciProje == null)
                {
                    mOgrenciProje = new MOgrenciProje();
                }
                return mOgrenciProje;
            }
        }


        private MSinavSonuc mSinavSonuc = new MSinavSonuc();

        public MSinavSonuc MSinavSonuc
        {
            get
            {
                if (mSinavSonuc == null)
                {
                    mSinavSonuc = new MSinavSonuc();
                }
                return mSinavSonuc;
            }
        }


        private MTakvim mTakvim = new MTakvim();

        public MTakvim MTakvim
        {
            get
            {
                if (mTakvim == null)
                {
                    mTakvim = new MTakvim();
                }
                return mTakvim;
            }
        }

        private MOdev mOdev = new MOdev();

        public MOdev MOdev
        {
            get
            {
                if (mOdev == null)
                {
                    mOdev = new MOdev();
                }
                return mOdev;
            }
        }

       
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
