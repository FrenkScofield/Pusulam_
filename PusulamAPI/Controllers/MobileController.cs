using Newtonsoft.Json.Linq;
using PusulamBusiness;
using System;
using System.Web.Http;

namespace PusulamAPI.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class MobileController : ApiController
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Login Metodu
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "SIFRE": "okyanus",
        ///     "DEVICE_ID": "5485"
        /// }
        /// </remarks>
        public Object Login(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MAuth.Login(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Logout Metodu
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "DEVICE_ID": "5485"
        /// }
        /// </remarks>
        public Object Logout(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MAuth.Logout(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Şifre değiştirme engeli var mı? 0 yok
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        /// }
        /// </remarks>
        public Object SifreDegistirYetkiKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MAuth.SifreDegistirYetkiKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        ///Veli Tc kontrol için veli ad soyad * lı
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        /// }
        /// </remarks>
        public Object OgrenciVeliSifreSifirlaKontrol(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MAuth.OgrenciVeliSifreSifirlaKontrol(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Şifre sıfırlanır.
        /// </summary>
        /// <remarks>
        /// Param
        /// {"TCKIMLIKNO":"1111111111","ADSOYAD":"ZEYNEP aaaaaa","REFERANCE_TCNO":"1111111111","BIRTHDAY":"20.07.2012","REFERANCENAME":"Veli Ad Soyad","SIFRE":"okyanus"}
        /// </remarks>
        public Object OgrenciVeliSifreSifirla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MAuth.OgrenciVeliSifreSifirla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kullanıcının yetkili olduğu menü listesi
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0"
        /// }
        /// </remarks>
        public Object MenuGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MMenu.MenuGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kullanıcının Takvim Listesi (Etkinlik Detayı liste içerisinde)
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "BAS_TARIH": "16.04.2021"
        /// }
        /// </remarks>
        public Object TakvimGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MTakvim.TakvimGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takvim ödev ayrıntı getirir.
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "ID_ODEV": 729
        /// }
        /// </remarks>
        public Object TakvimOdevGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MTakvim.OdevDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Takvim etüt ayrıntı getirir.
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "ID_ETUT": 3267
        /// }
        /// </remarks>
        public Object TakvimEtutGetir(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MTakvim.EtutDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Online ders programını getirir.
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "TC_OGRENCI": "10028577460"
        /// }
        /// </remarks>
        public Object OnlineDersProgram(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MDersProgram.OnlineDersProgramiListelebyOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ders programını getirir.
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "TC_OGRENCI": "1111111111"
        /// }
        /// </remarks>
        public Object DersProgram(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MDersProgram.DersProgramiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hedef için diploma notu girilir..
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7AEB031A-421A-46E3-8EC4-2D3CAA7C55D0",
        ///     "OOBP": 95
        /// }
        /// </remarks>
        public Object OOBPKaydet(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MHedef.OOBPKaydet(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Üniversite Taban Puan
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1",
        ///     "StartCount" : 0,
        ///     "Row": 10,
        ///     "SearchKey": "adana"
        /// }
        /// </remarks>
        public Object UniversiteTapanPuan(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MHedef.UniversiteTabanPuanListeleOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Üniversite Hedef Liste
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1"
        /// }
        /// </remarks>
        public Object HedefListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MHedef.HedefListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Üniversite Hedef Ekle
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1",
        ///     "PROGRAMKODU" : "106510041"
        /// }
        /// </remarks>
        public Object HedefEkle(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MHedef.HedefEkle(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Üniversite Hedef Sil
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1",
        ///     "PROGRAMKODU" : "106510041"
        /// }
        /// </remarks>
        public Object HedefSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MHedef.HedefSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sınav Sonuç Dönem Listele
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1"
        /// }
        /// </remarks>
        public Object SinavSonucDonemListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MSinavSonuc.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sınav Sonuç Sinav Türü Listele
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "DONEM": "2020",
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "FAD9B948-DAEC-4F27-A876-7872F418E5E1"
        /// }
        /// </remarks>
        public Object SinavSonucSinavTurListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MSinavSonuc.SinavTuruListeleTc(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sınav Sonuç Sinav Listele
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "DONEM": "2020",
        ///     "ID_SINAVTURU":0,
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "7ADD6BFF-DA77-43C6-9F39-D3D4BDD3CE31"
        /// }
        /// Sınav Pdf Link: "https://pusulam.okyanuskoleji.k12.tr/Rapor.aspx?rapor=Sinav.SinavSonuclariANDraporTur=pdfANDp=tc;oturum;tc;id_sinav;0"
        /// AND yazan yerlere AND operotörü koyulmalı
        /// </remarks>
        public Object SinavSonucSinavListe(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MSinavSonuc.SinavListelebyOgrenci(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Öğremci Bilgi Paylaşımı
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "A29F07B4-89A0-4EE6-9A2F-1D512E0BB99A"
        /// }
        /// Örnek Dosya : https://pusulam.okyanuskoleji.k12.tr/OBSResim/202131492837-Ekdosya1.pdf
        /// Örnek Link : https://www.youtube.com/embed/ + link
        /// </remarks>
        public Object OgrenciBilgiPaylasim(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOBS.OBSListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Proje ve Dönem Ödevi
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "A29F07B4-89A0-4EE6-9A2F-1D512E0BB99A",
        ///     "YARIYIL": 1
        /// }
        /// YARIYIL: 1. Dönem = 1, 2.Dönem = 2
        /// </remarks>
        public Object ProjeDonemOdevListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOgrenciProje.OgrenciProjeListesi(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Bültenleri Listeler
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "A29F07B4-89A0-4EE6-9A2F-1D512E0BB99A"
        /// }
        /// Dosya Örnek: https://interaktif.okyanuskoleji.k12.tr/site/Ogrenci/OBSResim/'+ dosya.AD"
        /// </remarks>
        public Object BültenListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MBulten.BultenListesiGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Morpa Giriş
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "IP": "95.6.79.122",
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "E1902672-9D32-40A0-BD63-C690B2A318E6"
        /// }
        /// </remarks>
        public Object MorpaGiris(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MMorpa.MorpaLink(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Ödev Liste Dönem Listesi
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5"
        /// }
        /// </remarks>
        public Object OdevListeDonem(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.DonemListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// OgrenciOdevDersListele
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     DONEM: "2020"
        /// }
        /// </remarks>
        public Object OgrenciOdevDersListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OgrenciOdevDersListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Odevleri Listeler. Liste tamammlanmayanları, Tamamlanan tamamlanan olarak işaretlenen ödevleri listeler.
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     DONEM: "2020",
        ///     ID_DERS: 0
        /// }
        /// </remarks>
        public Object OgrenciOdevListele(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OgrenciOdevListele(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Odevleri Detay getir
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     ID_ODEV: 110893
        /// }
        /// Dosya indir Dosyalar listesinden indirilebilir. Örnek : https://okyanusdata.s3-eu-west-1.amazonaws.com/pusulam/odev/ogretmen/' + ITEM.GUID
        /// </remarks>
        public Object OgrenciOdevDetay(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OdevDetayGetir(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Odev Tamamla
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     ID_ODEV: 116946
        /// }
        /// </remarks>
        public Object OgrenciOdevTamamla(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OdevTamamla(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Odev Tamamlanmayana taşı
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     ID_ODEV: 158394
        /// }
        /// </remarks>
        public Object OgrenciOdevIptal(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OgrenciOdevIptal(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ogrenci Dosya Ekle
        /// </summary>
        /// <remarks>
        /// Form Data request olarak gönderilmeli. Örnek form data elamanları aşağıdadır.
        /// {
        ///   var file = HttpContext.Current.Request.Files.Count > 0 ? HttpContext.Current.Request.Files[0] : null;
        /// string OTURUM = HttpContext.Current.Request.Form["OTURUM"].ToString();
        /// string TCKIMLIKNO = HttpContext.Current.Request.Form["TCKIMLIKNO"].ToString();
        /// string TC_OGRENCI =  HttpContext.Current.Request.Form["TC_OGRENCI"].ToString();
        /// string DOSYAGUID = HttpContext.Current.Request.Form["DOSYAGUID"].ToString();
        /// string AD =  HttpContext.Current.Request.Form["AD"].ToString() ;
        /// int ID_ODEV = HttpContext.Current.Request.Form["ID_ODEV"].ToString();
        /// }
        /// </remarks>
        public Object OgrenciOdevDosyaYukle()
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OdevDosyaYukle();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ogrenci Dosya Sil
        /// </summary>
        /// <remarks>
        /// Param
        /// {
        ///     "TCKIMLIKNO": "1111111111",
        ///     "OTURUM": "00544093-4A43-4475-B956-4946BE4E55D5",
        ///     ID_ODEVSINIFOGRENCIDOSYA: 211642
        /// }
        /// </remarks>
        public Object OgrenciOdevDosyaSil(JObject j)
        {
            try
            {
                using (Channel c = new Channel())
                {
                    return c.MOdev.OdevDosyaSil(j);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
