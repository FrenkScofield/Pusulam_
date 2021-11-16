namespace PusulamBusiness.Enums
{
    public enum EVarlik
    {
        TktSoruResmi = 1
        , TabanPuanExcel = 2
        , Klasik = 1
        , AçıkUçlu = 2
        , DoğruYanlış = 3
        , Test = 4
        , Eşleştirme = 5
        , BoşlukDoldurma = 6
        , Paragraf = 7
        //Soru Zorluk Seviyesi
        , Kolay = 8
        , Orta = 9
        , Zor = 10
        //Soru Hareket Durumu
        , OnayaGönderilmeyiBekliyor = 11
        , OnayaGönderildi = 25
        , DizgiyeGönderildi = 12
        , SoruHavuzunaGönderildi = 13
        , YazaraGönderildi = 14
        //Sınav Öğrenci Durumu
        , Bekliyor = 15
        , DevamEdiyor = 16
        , Bitti = 17
        //Sınav Türü
        , KonuTaramaTesti = 26
        //Soru Şablonu
        , TekSütun = 20
        , ÇiftSütun = 21
        //Çözüm Türleri
        , MetinEditörü = 22
        , Resim = 23
        //Döküman Türleri
        , SoruCozumResmi = 24
        //Sınav Ortamı
        , Online = 18
        , Offline = 19
    }
}