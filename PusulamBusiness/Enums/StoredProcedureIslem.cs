namespace PusulamBusiness.Enums
{
    public enum sp_GenelListeler
    {
        BilgiListele = 1,
        BilisselSurecListele = 2
    }
    public enum sp_AnaSayfa
    {
        AnasayfaDuyuruListeGetir = 1,
        AnasayfaFaturaListeGetir = 2
    }
    public enum sp_Menu
    {
        MenuGetir = 1,
        MenuListelebyYetki = 2,
        KullaniciMenuListelebyYetki = 3,
        MenuYardimGetir = 4
    }

    public enum sp_Filtre
    {
        SubeListele = 1,
        KademeListele = 2,
        Kademe3Listele = 3,
        SinifListele = 4,
        OgrenciListele = 5,
        DonemListele = 6,
        KullaniciTipiListele = 7,
        KullaniciListele = 8,
        SinifAlanListele = 9,
        EgitimTuruListele=10
    }

    public enum sp_FiltreEk
    {
        DersListele = 1,
        OdevTurListele = 2,
        OgrenciOdevDersListele = 3,
        OgretmenListele = 4,
        BransListele = 5,
        SinavTuruListele = 6,
        SinavListele = 7,
        OgrenciListelebyVeli = 8
    }

    public enum sp_UpgradeSoru
    {
        YasAraligiListele = 1,
        KategoriListele = 2,
        PuanAraligiListele = 3,
        SoruEkle = 4,
        SoruListele = 5,
        SoruSil = 6,
        SinavListele = 7,
        ModalKategoriPuanListele = 8,
        PuanKaydet = 9,
        DonemListele = 11,
        SoruListelebyDonem = 12,
        GrupListele = 13,
        SoruSayilari = 14,
        SinavKagidiSil = 15,
        SinavKagidiSilToplu = 16,
        SoruGuncelle = 17,
        UpgradeOgrenciSoruListele = 18,
        UpgradeOgrenciSoruPuanKaydet = 19,
        UpgradeOgrenciSonucListele = 20,
        SoruCozulmeOraniListele = 22,
        UpgradeIslemRaporLog = 23
    }


    public enum sp_TKTTest
    {
        TestListele = 1,
        KategoriListele = 2,
        OgrenciCevapListele = 3,
        OgrenciCevapKaydet = 4,
        OgrenciListele = 5,
        OgrenciSonucListele = 6,
        OgrenciCevapSil = 8,
        TKTZekaTuruListele = 9,
        TKTOgrenciZekaPuan = 10,
        TopluSonucGor = 12
    }

    public enum sp_Sube
    {
        SubeListele = 1,
        SubeListelebyKullanici = 2,
        SubeListeleTumu = 3,
        SubeKurListelebyKullanici = 4
    }

    public enum sp_EtutSinifRapor
    {
        SinifOgretmenSureListele = 1,
        SinifOgretmenDetayListele = 2,
    }
    public enum sp_EtutOgretmenRapor
    {
        OgretmenSinifSureListele = 1,
        OgretmenSinifDetayListele = 2,
        SubeOgretmenListeRapor = 3,
    }
    public enum sp_Grup
    {
        SinavGrupListele = 1,
        Kademe3ListelebyKullanici = 2,
        Kademe3ListeleMultiSube = 3,
        Kademe3ListeleMultiSubeYetkisiz = 4,
        Kademe3ListeleSubesiz = 5,
        Kademe3KurListelebyKullanici = 6
    }

    public enum sp_Sinif
    {
        SinifListele = 1,
        SinifListeleMulti = 2,
        SinifListelebyKullanici = 3,
        SinifListelebyKullaniciMultiSube = 4,
        SinifListeleTumu = 5,
        OgretmenSinifListesi = 6,
        DanismanOgretmenSinifListesi = 7,
        SinifListelebyKullaniciMultiSubeDonem = 8,
        SinifListelebyKullaniciDonem = 9,
        SinifListelebyKullaniciDonemMultiGenelYetki = 10,
        SinifAlanListele = 11,
        SinifKurSinifListelebyKullanici = 12,
        SinifListelebyKullaniciDonemMultiTekGrup = 13
    }

    public enum sp_Ogrenci
    {
        UpgradeOgrenciListele = 1,
        TKTOgrenciListele = 2,
        OgrenciListelebyKullanici = 4,
        OgrenciListelebyVeli = 5,
        OgrenciListeleMultiSinif = 6,
        OgrenciKademeGetir = 8,
        OgrenciSinavFrekansListe = 9,
        OgrenciSinavDersFrekansListe = 10,
        FrekansSinavOgrenciListele = 11,
        FrekansSinavOgrenciListeleNew = 12,
        OgrenciDonemDetay = 13,
        OgrenciListelebyVeliSinav = 14
    }
    public enum sp_DemoOgrenci
    {
        DemoOgrenciList = 1,
        DemoOgrenciLogin = 2
    }
    public enum sp_YG_YorumEkle
    {
        YorumListesi = 1,
        YorumDuzenle = 2
    }
    public enum sp_YG_Karne
    {
        OgrenciListele = 1,
        OgrenciKarneListele = 2,
    }
    public enum sp_YG_KotaBelirle
    {
        YG_KategoriKotaGetir = 1,
        YG_KotaBelirle = 2,
        YG_OgrenciBosalt = 3,
    }
    public enum sp_YG_Secim
    {
        SecilebilirYetenekListesi = 1,
        YetenekDersSec = 2,
        YetenekDersCik = 3,
        YetenekKulupKontrol = 4
    }
    public enum sp_YG_SecimYapmayanlar
    {
        SecimYapmayanlarListele = 1,
        SecimYapmayanlarRapor = 2,
    }
    public enum sp_YG_MevcutDagilim
    {
        YG_MevcutDagilim = 1,
    }
    public enum sp_YG_SecimAyar
    {
        TarihAyarla = 1,
        OgrenciAktar = 2,
        SecimTarihGetir = 3,
    }
    public enum sp_YG_YetenekGelisim
    {
        YetenekGelisim = 2,
        YetenekGelisimIlkSinifPuanKaydet = 3,
        YetenekGelisimOgrenciGecmis = 4,
        YetenekGelisimUstSinifPuanKaydet = 6,
        YG_KategoriListesi = 8,
        YG_YetenekAciklamaEkle = 9,
        YG_KategoriAciklamaGetir = 10,
        YetenekGelisimDersListe = 11,
    }
    public enum sp_SinavHariciPuanSira
    {
        HariciPuanSiraTaslakYukle = 2,
    }
    public enum sp_OdulListesi
    {
        OdulListesi = 1,
    }

    public enum sp_Sinav
    {
        DonemListele = 0,
        SinavTuruListele = 1,
        SinavGrupListele = 2,
        SinavDersListele = 3,
        SinavTanimla = 4,
        SinavListele = 5,
        SinavBilgiGetir = 6,
        SinavGuncelle = 7,
        DersKonuListele = 8,
        KatsayiListele = 9,
        KatsayiKaydet = 10,
        OptikListele = 11,
        OptikKaydet = 12,
        DosyaListele = 13,
        DosyaKaydet = 14,
        DosyaSil = 15,
        OptikBelliMi = 16,
        SinavDegerlendir = 17,
        PuanTuruListebyOgrenci = 18,
        SinavListelebyOgrenci = 19,
        UniteAra = 20,
        SinavAktifPasifYap = 21,
        OptikDosyaIcerik = 22,
        OptikDosyaIcerikDuzenle = 23,
        OgrenciListelebySinav = 24,
        SinavDersleriListele = 26,
        SinavDersSorulariListele = 27,
        SinavListelePasifDahil = 28,
        OgrenciSil = 29,
        DersAdiGuncelle = 30,
        SinavTuruDersleriListele = 31,
        SinavListeleDegerlendirilmis = 32,
        SinavTuruListeleGrubaGore = 33,
        SinavTuruListeleTc = 34,
        SinavListelebyGrup = 35,
        SinavListelebyGrupCoklu = 36,
        SinavDurumlariDegistir = 37,
        SinavListeleKademeDonem = 38,
        CASinavDersList = 39,
        CATaslakYukle = 40,
        SinavListelebyTarih = 41,
        BurslulukDosyaListe = 42,
        SinavListeleMultiTur = 43,
        TytSecimTurListele = 44,
        TytSecimKriterKaydet = 45,
        SoruListesiGetirSB = 46,
        SoruBankasiAra = 47,
        OptikIndir = 48,
        SinavDuzenleYetki = 49,
        YonergeSil = 50,
        YonergeGuncelle = 51,
        SinavDegerlendirmeLog = 52,
        UniteTaramaSinavListele = 53
    }

    public enum sp_DersUnite
    {
        SinavDersListe = 1,
        SinavUniteListele = 2,
        SinavUniteDetay = 4,
        SinavUniteSil = 3,
        SinavUniteGuncelle = 5,
        SinavUniteEkle = 6,
        GenelDersListele = 7,
        UniteEkleExcel = 8,
        UniteSilTumu = 9,
        YaziliDersGetir = 10,
        SinavDerseListe = 11,
        UniteLinkEkleExcel = 12,
        DersUniteDosyaListesi = 13,
        DersUniteDosyaPasif = 14,
        DersUniteDosyaYukle = 15
    }

    public enum sp_MentalUp
    {
        VeliMentalUp = 1,
    }
    public enum sp_BirimYetki
    {
        KademeListe = 1,
        KullaniciTipiListele = 2,
        BirimMenuKaydet = 3,
        KullaniciKademeListele = 4,
    }

    public enum sp_MobilBirimYetki
    {
        KademeListe = 1,
        KullaniciTipiListele = 2,
        BirimMenuKaydet = 3,
        KullaniciKademeListele = 4,
        MenuListelebyYetki = 5
    }

    public enum sp_BurslulukOgrenciIslemleri
    {
        BurslulukDosyaYukle = 1,
        OgrenciListelebyBurslulukDosya = 2,
        BurslulukOgrenciDuzenle = 3,
        BurslulukOgrenciSil = 4,
        SinavListeleBursluluk = 6,
        BurslulukDosyaSil = 7,
        BurslulukDosyaTarihSeansListe = 9,
    }
    public enum sp_BurslulukBursOraniBelirle
    {
        BursOranListele = 1,
        BurslulukSinavBursOranKaydet = 2,
    }

    public enum sp_KullaniciMenuYetkiKaldir
    {
        MenuKullaniciYetkiListele = 1,
        MenuKullaniciYetkiKaldir = 2,
    }

    public enum sp_KullaniciYetki
    {
        KullaniciYetkiListele = 1,
        KullaniciYetkiGuncelle = 2,
        KullaniciListele = 3,
        KullaniciMenuKaydet = 4,
        KullaniciSubeSinifGetir = 5
    }
    public enum sp_Kullanici
    {
        KullaniciTipiGetir = 1,
    }

    public enum sp_SinavIstatistik
    {
        Istatistik = 1,
        SinavDetayGetir = 2
    }
    public enum sp_KarmaListe
    {
        KarmaListe = 1,
        ExcelYukle = 2,
        BurslulukSinavListelebyTarihSeans = 4,
    }
    public enum sp_AraKarne
    {
        AraKarneDeneme = 1,
        AraKarneDenemeRapor = 2,
        AraKarneYazili = 3,
        AraKarneYaziliRapor = 4
    }
    public enum sp_KonuAnalizi
    {
        KonuAnaliz = 1
    }
    public enum sp_YaziliYoklamaSonuclari
    {
        YaziliYoklamaSonuclari = 1,
        YaziliYoklamaSinavListesibyOgrenci = 2,
        YaziliYoklamaSonuclariGetirGenel = 3,
        YaziliYoklamaSinavListesi = 4,
        YaziliYoklamaSonuclariGetirGenelYeni = 5,
        YaziliYoklamaOgrenciKOSListesi = 9
    }
    public enum sp_Donem
    {
        DonemListele = 1
    }

    public enum sp_OSYM
    {
        TabanPuanKaydet = 0,
        TabanPuanListele = 1,
        TabanPuanSil = 2,
        TabanPuanListeleOgrenci = 3,
        IlListele = 4,
        UniversiteTuruListele = 5,
        PuanTuruListele = 6,
        BolumListele = 7,
        PuanTuruListeleGenel = 8,
        OsymOzelKosulEkle = 9

    }

    public enum sp_OgrenciHedef
    {
        HedefEkle = 1,
        HedefListele = 2,
        HedefSil = 3,
        HedefListelePuanTur = 4,
        OgrenciSonPuanGetir = 5,
        OgrenciSinavNetListele = 6,
        HedefNetEkle = 7,
        KazanimListele = 8,
        OOBPKaydet = 9,
        OOBPGetir = 10,
        PuanTuruListele = 11,
        VeliHedefListele = 12,
        KazanimListeleYeni = 13,
        SinavTuruNetGrafikListele = 14,
        SinavTuruListele = 15
    }

    public enum sp_OgrenciHedefOO
    {
        OgrenciSonDogruGetir = 1,
        SinavTuruListele = 2,
        HedefNetEkleOO = 3
    }

    public enum sp_OnlineSinav
    {
        SinavListele = 1,
        SoruListele = 2,
        SoruGetir = 3,
        SinavTuruGetir = 4,
        GirisEkle = 5,
        SinaviBitir = 6,
        SoruListeleCevapli = 7,
        OnlineSinavDetay = 8,
        OnlineSinavDetayKaydet = 9,
        OnlineSinavOgrenciAta = 10,
        OnlineSinavOgrenciListele = 11,
        OnlineSinavlarimListele = 12,
        OnlineSinavSoruListele = 13,
        OnlineSinavSoruIsaretle = 14,
        OnlineSinavPerformans = 15,
        OnlineSinavOgrenciAtamaKaldir = 16,
        OnlineSinavOnizlemeRapor = 17,
        OnlineSinavOnizlemeSoruListele = 18,
        OnlineSinavOgrenciOturumListele = 19,
        OnlineSinavOgrenciOturumSil = 20,
        OnlineSinavCikti = 21,
        OnlineSinavOturumOgrenciListele = 22,
        OnlineSinavOgrenciOturumSifirla = 23,
        YonergeKontrol = 24,
        YonergeOnay = 25,
        APIOgrenciSinavListele = 26,
        OnlineSinavLog = 27,
        OnlineTestListele = 28,
        OnlineTestSinavSoruListele = 29,
        OnlineTestSinavBitir = 30,
        OnlineSinavDisOgrenciListele = 31
    }

    public enum sp_SorunBildir
    {
        SorunBildir = 1,
        SinavListele = 2
    }
    public enum sp_DisOgrenci
    {
        OnlineSinavKurumDisiOgrenciListe = 1,
        OnlineSinavKurumDisiOgrenciAtama = 2
    }
    public enum sp_GenelSinavRaporu
    {
        GenelSinavRaporu = 1
    }
    public enum sp_OgrenciRaporlari
    {
        OgrenciRaporlari = 1
    }
    public enum sp_OgrenciBilgiSistemi
    {
        OBSListele = 1,
        BilgiEkle = 2,
        BilgiDosyaKaydet = 3,
        BilgiListele = 4,
        BilgiSil = 5,
        BilgiGetir = 6,
        BilgiDuzenle = 7
    }
    public enum sp_SinavListele
    {
        EslesmeyenOgrenciListeGetir = 1,
        Eslestir = 2,
        SinavKopyala = 3,
        SoruIslemListele = 5,
        SinavSoruIslem = 6,
        SinavSoruIslemleriListele = 7,
        SinavSoruIslemSil = 8
    }
    public enum sp_OptikYukle
    {
        EslesmeyenOgrenciListeGetir = 1,
        Eslestir = 2,
        OptikDosyaGor = 3,
        TcGuncelle = 4,
        OptikSatirSil = 5,
        TekrarEdenOgrenciGetir = 6,
        SorunluTcSayilariniGetir = 7,
        TekrarEdenOgrenciExcelGetir = 8
    }
    public enum sp_Yardim
    {
        MenuListele = 1,
        YardimGetir = 2,
        YardimKaydet = 3
    }
    public enum sp_OgrenciDersMuaf
    {
        OgrenciListele = 1,
        OgrenciMuafDuzenle = 2,
    }
    public enum sp_OgrenciFrekansMuaf
    {
        OgrenciListele = 1,
        OgrenciMuafDuzenle = 2,
    }
    public enum sp_FrekansSinavOgrenci
    {
        FrekansSinavOgrenciListele = 1,
    }
    public enum sp_FrekansSistem
    {
        FrekansOgretmenSirali = 1,
        FrekansGetirOgretmenSinif = 2,
        FrekansGetirSinifOgrenci = 3,
        OgrenciFrekansDetayGetir = 4,
        SinifFrekansGetir = 5,
        FrekansDersListele = 6,
        SinavTuruListele = 7
    }
    public enum sp_FrekansSistemEksikKazanim
    {
        OgrenciFrekansEksikKazanimGetir = 1,
        SinifFrekansEksikKazanimGetir = 2,
        OgretmenFrekansEksikKazanimGetir = 3
    }
    public enum sp_v2FrekansSistem
    {
        FrekansOgretmenSirali = 1,
        FrekansGetirOgretmenSinif = 2,
        FrekansGetirSinifOgrenci = 3,
        OgrenciFrekansDetayGetir = 4,
        SinifFrekansGetir = 5,
        FrekansDersListele = 6,
        SinavTuruListele = 7,
        FrekansSistemSonFiltre = 8
    }
    public enum sp_v2FrekansSinif
    {
        FrekansSinifOgretmenSirali = 1,
        FrekansGetirSinif = 2,
        FrekansGetirSinifOgrenci = 3,
        OgrenciFrekansDetayGetir = 4,
        SinifFrekansGrafikGetir = 5,
        FrekansDersListele = 6,
        SinavTuruListele = 7,
        FrekansSinifSonFiltre = 8
    }
    public enum sp_v2FrekansKampus
    {
        FrekansKampusSirali = 1,
        KampusFrekansGetirKademeSinif = 2,
        KampusFrekansGetirSinifOgrenci = 3,
        KampusSinifOgrenciFrekansDetayGetir = 4,
        KampusFrekansGrafikGetir = 5,
        FrekansDersListele = 6,
        SinavTuruListele = 7,
        FrekansKampusSonFiltre = 9
    }
    public enum sp_v2FrekansEtkiListesi
    {
        FrekansEtkiListesi = 1,
        FrekansDonemListele = 2,
        FrekansSinavTuruListele = 3,
    }
    public enum sp_v2FrekansSistemEksikKazanim
    {
        OgrenciFrekansEksikKazanimGetir = 1,
        SinifFrekansEksikKazanimGetir = 2,
        OgretmenFrekansEksikKazanimGetir = 3
    }
    public enum sp_GelisimRaporu
    {
        GelisimRaporu = 1,
        GelisimFiltreList = 3,
        GelisimRaporuHtml = 4
    }
    public enum sp_OkulNetPuanOrtalamalari
    {
        OkulNetPuanOrtalamalari = 1,
    }
    public enum sp_RehberlikEnvanterTanimlama
    {
        RehberlikEnvanterEkle = 1,
        RehberlikEnvanterListele = 2,
        RehberlikEnvanterAktifPasif = 3
    }
    public enum sp_RehberlikEnvanter
    {
        RehberlikEnvanterleriGetir = 1,
        RehberlikEnvanterleriGetirSinif = 2,
        RehberlikEnvanterleriGetirSubeKademe = 3,
    }
    public enum sp_SinifMaddeAnalizi
    {
        SinifMaddeAnalizi = 3,
    }
    public enum sp_MaddeFrekansAnalizi
    {
        MaddeFrekansAnalizi = 1,
    }
    public enum sp_SinifDersAnalizi
    {
        SinifDersAnalizi = 1,
        SinifDersAnaliziYeni = 3
    }
    public enum sp_ProjeDonem
    {
        OgrenciKademeGetir = 1,
        SinifDersListesi = 2,
        ProjeDonemDurumDegistir = 4,
        OgretmenProjeTalepleri = 6,
        KontrolKayit = 8,
        OgrenciProjeListesi = 10,
        DanismanOgretmenSinifProje = 14,
        ProjeDonemDosyaEkle = 16,
        ProjeDonemDosyaSil = 17,
        ProjeDonemDosyaListele = 18,
        ProjeDonemKayit = 20,
        ProjeDonemSil = 21,
        ProjeDonemNotEkle = 22,
    }
    public enum sp_GenelSoruAnalizi
    {
        GenelSoruAnalizi = 1,
    }
    public enum sp_SinavKonuAnalizi
    {
        SinavKonuAnalizi = 1,
    }
    public enum sp_TestMaddeAnalizi
    {
        TestMaddeAnalizi = 1,
    }
    public enum sp_OgrenciMaddeAnalizi
    {
        OgrenciMaddeAnalizi = 1,
    }
    public enum sp_SinifNetPuanOrtalamalari
    {
        SinifNetPuanOrtalamalari = 1,
    }
    public enum sp_SinavKatilim
    {
        SinavKatilim = 1,
        SinavKatilimRapor = 2
    }
    public enum sp_Takvim
    {
        TakvimListele = 1,
        TakvimEkle = 2,
        TakvimSil = 3,
        KullaniciTipListele = 4,
        GrupListele = 5,
        YetkiKaydet = 6,
        TakvimEtkinlikListele = 7,
        TakvimEtkinlikEkle = 8,
        TakvimEtkinlikSil = 9,
        TakvimEtkinlikGuncelle = 10,
        DuyuruListele = 11,
        TakvimGetir = 12,
        TakvimEtkinlikEkleExcel = 13,
        TatilListele = 14,
        TatilEkle = 15,
        TatilSil = 16
    }
    public enum sp_OgrenciTakvim
    {
        TakvimGetir = 1
    }
    public enum sp_OgrenciIzleme
    {
        OgrenciIzleme = 1
    }
    public enum sp_OdevTakibi
    {
        OdevTakibi = 1
    }
    public enum sp_EtutTakvimi
    {
        TakvimGetir = 1,
        EtutDetay = 2,
    }
    public enum sp_AssessmentSinifSinavRaporu
    {
        AssessmentSinifSinavRaporu = 1,
    }
    public enum sp_AssessmentOgrenciSinavRaporu
    {
        AssessmentOgrenciSinavRaporu = 1,
    }
    public enum sp_AssessmentSinavOgrenciRaporu
    {
        AssessmentSinavOgrenciRaporu = 1,
    }
    public enum sp_AssessmentSinavSoruAnalizi
    {
        AssessmentSinavSoruAnalizi = 1,
    }
    public enum sp_SinifOgretmenListesi
    {
        OgretmenListesiGetir = 1,
    }
    public enum sp_SinifDersProgram
    {
        DersProgramiGetir = 1,
    }
    public enum sp_Assessment
    {
        AssessmentKategoriListesi = 1,
        SinavKaydet = 2,
        SinavListele = 3,
        SinavDetay = 4,
        OgrenciYukle = 5,
        EslesmeyenOgrenciListeGetir = 6,
        TcGuncelle = 7,
        OgrenciPasif = 8,
        AktifPasifDegistir = 9,
    }
    public enum sp_Etut
    {
        EtutTabloSinav = 1,
        EtutTabloYazili = 2,
        SinavTuruListele = 3,
        SinavTuruDersleriListele = 4,
        EtutTabloSinavOgrenci = 5,
        EtutTabloYaziliOgrenci = 6,
        SubeOgretmenListe = 7,
        EtutOlustur = 8,
        SinavEtutListe = 9,
        SinavEtutKatilimDuzenle = 10,
        SinavEtutOgrenciListe = 11,
        SinavEtutOgrenciDuzenle = 12,
        SubeKademeDersListesi = 13,
        SubeKademeDersOgretmenListesi = 14,
        SinavEtutDuzelt = 15,
        AktifPasifYap = 16,
        EtutDetayGetir = 17
    }
    public enum sp_EtutSabitOlustur
    {
        DersOgretmenListesi = 1,
        EtutSabitKaydet = 2,
        EtutSabitListe = 3,
    }
    public enum sp_EtutProgramiOV
    {
        EtutSabitListeOV = 1,
        SinavEtutListeOV = 2,
        SinavEtutKatilimGrafigiOV = 3,
    }
    public enum sp_DenemeSinavRaporlari
    {
        SinavListele = 1
    }
    public enum sp_SinavAnaliz
    {
        SinavAnaliz = 1,
        SinavAnalizYeni = 3
    }
    public enum sp_OrtalamaListesi
    {
        OrtalamaListesi = 1,
        OrtalamaListesiYeni = 3
    }
    public enum sp_TopluYaziliYoklamaSonuclari
    {
        TopluYaziliYoklamaSonuclari = 1,
        YaziliYoklamaSinavListesi = 3,
        YaziliYoklamaSinavListesiYeni = 4,
        TopluYaziliYoklamaSonuclariYeni = 5
    }
    public enum sp_PuanaGoreYaziliSonuclari
    {
        PuanaGoreYaziliSonuclari = 1,
        PuanaGoreYaziliSonuclariYeni = 3
    }
    public enum sp_GenelKazanimAnalizi
    {
        GenelKazanimAnalizi = 1
    }
    public enum sp_SinifBazindaKazanimAnalizi
    {
        SinifBazindaKazanimAnalizi = 1,
        SinifBazindaKazanimAnaliziYeni = 3
    }
    public enum sp_CevapAnahtariKullaniciYetki
    {
        DersListele = 1,
        KullaniciYetkiKaydet = 2
    }
    public enum sp_CevapAnahtariDuzenle
    {
        SinavBilgiGetir = 1,
        CevapAnahtariKaydet = 2
    }
    public enum sp_KazanimAnalizi
    {
        KazanimAnalizi = 1
    }
    public enum sp_SinavaKatilmayanOgrenciler
    {
        SinavaKatilmayanOgrenciler = 1,
        SinavaKatilmayanOgrencilerYeni = 3
    }
    public enum sp_YaziliYoklama
    {
        Kademe3Listele = 1,
        Kademe2Listele = 2,
        YaziliTanimla = 3,
        YaziliGuncelle = 4,
        YaziliBilgiGetir = 5,
        YaziliListele = 6,
        YaziliAktifPasifYap = 7,
        OgrenciListele = 8,
        YaziliKopyala = 9,
        YaziliBilgiGetirListe = 10,
        YaziliListeleSelect = 11,
        OgrenciKaydet = 12,
        OgrenciSil = 13,
        OgrenciKaydetToplu = 14,
        YaziliYoklamaSinifSinavListesi = 15,
        YaziliTanimlaExcel = 16,
        OVGorsunDegistir = 17,
        AktifDegistir = 18,
        YaziliTuruListele = 19,
        YaziliBilgileriniGuncelle = 20,
        YaziliYoklamaSinifSinavListesiKOS = 21,
        KarnedeGorunsunDegistir = 22,
        ExceldenTopluSonucKontrol = 23,
        ExceldenTopluSonucKaydet = 24,
        BildirimListele = 26
    }

    public enum sp_KazanimAnaliziOO
    {
        KazanimAnaliziOO = 1
    }

    public enum sp_Abide
    {
        SinavTanimla = 1,
        SinavListele = 2,
        SinavDuzenle = 3,
        SinavSil = 4,
        SinavBilgiEkle = 5,
        SinavBilgiGetir = 6,
        OgrenciListele = 7,
        OgrenciPuanListele = 8,
        OgrenciPuanKaydet = 9,
        DersPuanSil = 10,
        SayfaTurListele = 11,
        RaporDuzenKaydet = 12,
        ResimSil = 13,
        ResimEkle = 14,
        OgrenciListeleRapor = 15,
        SinavListelebyOgrenci = 17,
        PuanListeleExcel = 18,
        SinavKarsilikEkle = 19,
        SinavKarsilikGetir = 20,
        KitapcikGetir = 21,
        OVGorsunDegistir = 25,
        OgrenciRaporDetay = 26,
        BildirimListele = 27
    }
    public enum sp_YYS
    {
        SinavTanimla = 1,
        SinavListele = 2,
        SinavDuzenle = 3,
        SinavSil = 4,
        SinavBilgiEkle = 5,
        SinavBilgiGetir = 6,
        OptikYukle = 7,
        SinavDosyaListele = 8,
        Karne = 9,
        SinavDosyaSil = 10,
    }

    public enum sp_UniteTarama
    {
        SinavTanimla = 1,
        SinavListele = 2,
        SinavDuzenle = 3,
        SinavSil = 4,
        SinavBilgiEkle = 5,
        SinavBilgiGetir = 6,
        OgrenciListele = 7,
        OgrenciPuanListele = 8,
        OgrenciPuanKaydet = 9,
        DersPuanSil = 10,
        SayfaTurListele = 11,
        RaporDuzenKaydet = 12,
        ResimSil = 13,
        ResimEkle = 14,
        OgrenciListeleRapor = 15,
        SinavListelebyOgrenci = 17,
        PuanListeleExcel = 18,
        SinavKarsilikEkle = 19,
        SinavKarsilikGetir = 20,
        KitapcikGetir = 21,
        OVGorsunDegistir = 25,
        EksikKazanimListesi = 26,
        EksikKazanimTabloSinavOgrenci = 27,
        EksikKazanimEtutSinavOgrenci = 28,
        UTSinavListelebyOgrenci = 29,
        SinifListeRaporu = 30,
        MorpaKazanimBul = 31,
        SinavOgrenciListele = 32,
        SinifMaddeAnalizi = 33,
        SinifMaddeAnaliziRapor = 34,
        BildirimListele = 35
    }
    public enum sp_Degerlendirme
    {
        PeriyotTanimla = 1,
        PeriyotListele = 2,
        SoruTanimla = 3,
        SoruListele = 4,
        PersonelListele = 5,
        SoruPuanYorumListele = 6,
        SoruPuanYorumKaydet = 7,
        OgrenciListele = 8,
        OgretmenSoruPuanYorumListele = 9,
        OgretmenSoruPuanYorumKaydet = 10,
        PeriyotSil = 11,
        SoruSil = 12,
        PuanTurListele = 13,
        SinifYorumListele = 14,
        SoruYorumKaydet = 15,
        KategoriListele = 16,
        KategoriKaydet = 17,
        KategoriSil = 18,
        PersonelSubeKademeGetir = 19,
        KullaniciTipiListeleMMY = 20,
        PersonelListeleMulti = 21,
        PeriyotListeleMulti = 22,
        PersonelKademeGetir = 25,
        PeriyotListeleTipsiz = 26,
        DegerlendirmeListele = 27,
        PersonelListeleMultiSube = 28,
        OgrenciListeleV4 = 29,
        DegerlendirmeSil = 30,
    }

    public enum sp_ZekaTest
    {
        ZekaTestListele = 1,
        OgrenciAra = 2,
        SoruListele = 3,
        Kaydet = 4,
        Sil = 5,
        DosyaSil = 7,
        SonucListele = 8,
        SoruListeleListeSayfasi = 9,
        SonucListeleABB = 11,
        PasifYapABB = 12,
        AktifYapABB = 13,
        SoruListeleListeSayfasiABB = 14,
        Guncelle = 15
    }

    public enum sp_Morpa
    {
        DersListele = 1,
        MorpaDersEkle = 2,
        AktifPasifDegistir = 3,
        MorpaDersGuncelle = 4,

        MorpaKazanimListele = 5,
        MorpaKazanimEkle = 6,
        KazanimAktifPasifDegistir = 7,
        MorpaKazanimGuncelle = 8,

        MorpaMateryalListele = 9,
        MorpaMateryalEkle = 10,
        MateryalAktifPasifDegistir = 11,
        MorpaMateryalGuncelle = 12,
        MorpaKazanimExcelYukle = 13,
    }

    public enum sp_Odev
    {
        OdevTurKademeListele = 1,
        OdevTurKademeKaydet = 2,
        OdevKaydet = 3,
        OdevListele = 4,
        OdevSil = 5,
        OgrenciListele = 6,
        DanismanMi = 7,
        OdevVerenListele = 8,
        OdevVerenOdevListele = 9,
        OdevDetayGetir = 10,
        OdevKontrolKaydet = 11,
        Guncelle = 12,
        OgrenciOdevListele = 14,
        OgretmenOdevRaporu = 16,
        OgretmenSinifOdevListesi = 17,
        MorpaOdevListele = 18,
        OdevTamamla = 21,
        OdevDosyaListele = 22,
        OdevDosyaYukle = 23,
        OdevDosyaSil = 24,
        OgrenciOdevIptal = 25
    }

    public enum sp_VIU
    {
        MesajRapor = 1,
        KullanmayanRapor = 2,
        AramaSebepListele = 3,
        AramaDurumListele = 4,
        AramaRapor = 5,
        YoklamaRapor = 6,
        ViuAramaYetkiListesi = 7,
        ViuAramaKullaniciYetkiKaydet = 8,
        ViuKullaniciListesi = 9,
        ViuTopluMesajGonder = 10,
        KullaniciCihazlariListele = 11
    }

    public enum sp_UniteTaramaSinavSonucListesi
    {
        SinavSonucListesi = 1,
    }

    public enum sp_OsOdevListele
    {
        OsOdevListele = 1,
        OsOdevDetayGetir = 2,
        OdevDosyaYukle = 3,
        OdevDosyaSil = 4,
        OdevTamamla = 5,
        OdevTamamlandiListele = 6,
        OgrenciOdevIptal = 7
    }

    public enum sp_VeliUniteTaramaRaporu
    {
        UTSinavListelebyOgrenci = 1,
    }

    public enum sp_Yillik
    {
        OgrenciListele = 1,
        YillikYaziKaydet = 2,
        YillikYaziSil = 3,
    }

    public enum sp_DegerlendirmeFormu
    {
        OD_DonemListele = 1,
        OD_AyListele = 2,
        OD_DegerlendirmeListele = 3,
        OD_OgrenciDonemDetay = 4,
        OD_DonemlikDegerlendirmeListele = 5,
        OD_DonemlikDegerlendirmeDetay = 6,
    }

    public enum sp_CurpusStudyYukle
    {
        CurpusStudyListe = 1,
        CurpusStudyExcelYukle = 2,
    }
    public enum sp_RehberlikOnlineYukle
    {
        RehberlikOnlineExcelYukle = 1,
        RehberlikOnlineListe = 2,
    }

    public enum sp_OzelSayfaIslemleri
    {
        SayfaTuruListe = 1,
        OzelSayfaEkle = 2,
        OzelSayfaDetay = 3,
        OzelSayfaListe = 4,
        OS_AktifPasif = 5,
        OS_Sil = 6,
    }

    public enum sp_ZumreCalisma
    {
        ZumreCalismaEkle = 1,
        ZumreCalismaGuncelle = 2,
        ZumreCalismaSil = 3,
        ZumreCalismaListele = 4,
        ZumreCalismaKatilimciListele = 5,
        ZumreCalismaKatilimciSil = 6,
        ZumreCalismaKullaniciListele = 7,
        ZumreCalismaKatilimciAta = 8,
        ZumreCalismaListeleSelect = 10,
        ZumreCalismaListeleOnaySelect = 11,
        ZumreCalismaOnayKatilimciListele = 12,
        ZumreCalismaKatilimciOnay = 13,
        ZumreCalismaKatilimciAtaToplu = 14,
        ZumreCalismaKatilimciSilToplu = 15,
        ZumreCalismaYoklamaDurumGuncelle = 16,
        ZumreCalismaYoklamaGetir = 18,
        ZumreCalismaYoklamaGirisGuncelle = 19,
        ZumreCalismaYaziliDurumGuncelle = 20,
        ZumreCalismaYaziliGetir = 22,
        ZumreCalismaYaziliGirisGuncelle = 23,
        ZumreCalismaYaziliNotGetir = 24,
        ZumreCalismaYaziliNotYukle = 25,
        ZumreCalismaCalismalarim = 26,
        ExcelTopluZumreCalismaKatilimciKontrol = 28,
        ExcelTopluZumreCalismaKatilimciEkle = 29
    }
    public enum sp_UniteTaramaSinifPuanOrtalamalari
    {
        UniteTaramaSinifRaporu = 1,
    }

    public enum sp_SinifListeRaporu
    {
        SinifListeRaporu = 1,
        SinifListesiFotografli = 2
    }

    public enum sp_Bulten
    {
        BultenListesiGetir = 1,
        BultenEkle = 2,
        BultenDosyaKaydet = 3,
        BultenListele = 4,
        BultenSil = 5,
        BultenGetir = 6,
        BultenDuzenle = 7
    }

    public enum sp_ViuRandevuTakip
    {
        ViuRandevuTakipListele = 1,
        ViuRandevuTakipDetay = 2
    }
    public enum sp_ViuBildirimRapor
    {
        sp_ViuBildirimListele = 1
    }
    public enum sp_RehberDanismanYoklama
    {
        OgrenciYoklamaListele = 1,
        RehberDanismanSinifListele = 2,
    }
    public enum sp_AkilliOgretimBarkod
    {
        ExcelYukle = 1,
    }

    public enum sp_DersCalismaProgrami
    {
        KazanimOdevEkle = 1,
        SinavUniteListele = 2,
        KazanimProgramEkle = 3,
        KazanimListele = 4,
        ProgramListele = 5,
        DersCalismaKazanimList = 8,
        KazanimProgramSil = 9,
    }
    public enum sp_OnlineDers
    {
        GenelKlasorListele = 1,
        GenelKlasorKaydet = 2,
        TarihListele = 3,
        OnlineDersProgramiListele = 4,
        OnlineDersProgramiKaydet = 5,
        OnlineDersProgramiListelebyOgrenci = 6,
        OnlineDersProgramiListeleSinif = 7,
        OnlineDersEkle = 9,
        OnlineDersSil = 10,
        OnlineDersEkranPaylasimIdGuncelle = 11,
        OnlineDersProgramiOgretmenKlasorGetir = 12,
        OnlineDersProgramiOgretmenKlasorKaydet = 13,
        OnlineDersHerkesiSustur = 14,
        OnlineDersHerkesiSusturGetir = 15,
        OgretmenOnlineDersProgramiListele = 16,
        OgretmenOnlineDersProgramiKaldir = 17,
        SinifOnlineDersOgretmenListele = 18,       
    }  

    public enum sp_Fatura
    {
        FaturaOdemeEkle = 1,
        FaturaOdemeCevapSet = 2,
        FaturaOdemeCevap = 3,
        FaturaOdemeCevapKontrol = 4,
    }

    public enum sp_OgrenciHarcama
    {
        ParaYukle = 1,
        ParaYukleCevapSet = 2,
        ParaYukleCevap = 3,
        ParaYukleCevapKontrol = 4,
    }

    public enum sp_UyariSistemleri
    {
        UyariEkle = 1,
        UyariDetay = 2,
        UyariListe = 3,
        UyariAktifPasif = 4,
        UyariGoster = 5,
        UyariKullaniciOnay = 6,
    }

    public enum sp_Parametre
    {
        ParametreListele = 1,
        ParametreKaydet = 2,
    }

    public enum sp_SinavSonuclari
    {
        OnlineTestRapor = 3,
        SinavListele = 4
    }

    public enum sp_EDisOgrenciApi
    {
        OnlineSinavKurumDisiOgrenciAtama = 1
    }
    public enum sp_OgretmenBilgileri
    {
        OgretmenBilgileriListele = 1
    }
    public enum sp_Performans
    {
        DonemListele = 0,
        PeriyotTanımla = 1,
        PeriyotListele = 2,
        PeriyotSil = 3,
        OgretmenListele = 4,
        PeriyotTarihListele = 5,
        KategoriListele = 6,
        PerformansSoru = 7,
        KategoriSoru = 8,
        DegerlendirmeKaydet = 9,
        DegerlendirilenlerListele = 10,
        DegerlendirilmeyenlerListele = 11,
        TumPeriyotTarihListele = 14,
        PeriyotDuzenle = 15
    }
    public enum sp_GenelKurul
    {
        DonemListele = 1,
        PeriyotEkle = 2,
        PeriyotListele = 3,
        PeriyotSil = 4,
        PeriyotMailGonder = 5,
        KararEkle = 6,
        SubeListele = 7,
        KademeYetkiListele = 8,
        KararListele = 9,
        KararListeleGenel = 10,
        KararSil = 11,
        EmailListele = 12,
        EmailSil = 13,
        EmailEkle = 14,

    }
    public enum sp_Duyuru
    {
        DuyuruEkle = 1,
        DuyuruListe = 2,
        DuyuruAktifPasif = 3,
        DuyuruDetay = 4,
        DuyuruGuncelle = 5,
        DuyuruWebBaner = 7,
        DuyuruDosya = 8
    }
    public enum sp_Zumre
    {
        KullaniciTipGetir = 1,
        SinavUniteListele = 2,
        Kaydet = 3,
        DosyaKaydet = 4,
        Guncelle = 5,
        DosyaList = 6,
        DosyaSil = 7,
        LogKaydet = 8,
        LogListele = 9,
        SinifGrupListele = 10,
        DersListele = 11
    }
    public enum sp_Eogrenme
    {
        KullaniciTipGetir = 1,
        SinifGrupListele = 2,
        DersUniteGetir = 3,
        DersListele = 4,
        Deneme = 5,
        Kaydet = 6,
        DosyaKaydet = 7,
        Guncelle = 8,
        DosyaList = 9,
        DosyaSil = 10,
        LogKaydet = 11


    }
    public enum sp_SportifKulup
    {
        PeriyotListele = 1,
        PeriyotEkle = 2,
        KotaListele = 3,
        KotaEkle = 4,
        KulupListele = 5,
        KotaSil = 6,
        KotaDuzenle = 7,
        KulupBelirle = 8,
        Periyot = 9,
        KulupDuzenle = 10,
        KuluptenAyril = 11,
        OgrenciKulupListele = 12,
        PeriyotDuzenle = 14,
        KulupEkle = 15,
        KontrolKotaEkle = 16,
        Kademe3ListelebyKullanici = 17,
        YetenekKulupKontrol = 18
    }

    public enum sp_DisKaynak
    {
        ApiKeyAl = 1,
        SubeListe = 2,
        SinifListe = 3,
        OgrenciListe = 4,
        OgretmenListe = 5,
        SifreCoz = 6,
        OgrenciGetir = 7,
        TumOgretmenGetir = 8,
        SinifOgretmenDersListele = 9,
        SinifGetir = 10,
        OgretmenGetir = 11
    }

    public enum sp_DisKaynakKademe
    {
        Kaydet = 1,
        Liste = 2,
        Sil = 3,
        ProjeListele = 4
    }

    public enum sp_DisKaynakTanim
    {
        Kaydet = 2,
        Liste = 1,
        Duzenle = 3,
    }
    public enum sp_KullaniciArama
    {
        OgrenciListele=1,
        Brans = 2,
        YetkiKaldir=3
    }
    public enum sp_SubeYetki
    {
        KademeListele = 1,
        KademeYetkiKaydet = 2,
        KullaniciTipiSubeGetir=3,
        KullaniciTipiSubeKaydet = 4,
        KullaniciTipiListele = 5
    }
    public enum sp_DersEkleme
    {
        DersListele=1
    }
    public enum sp_DersProgrami
    {
        PersonelListele=1,
        DersProgram=2,
        DersOgretmen=3,
        DersPersonelKaydet = 4
    }
}