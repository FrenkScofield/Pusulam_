if (session == null) {
    $.removeCookie("K");
    window.location.replace("/login.html");
}

var dataMenu = null;
var htmKod = "";
var mesajogretmen = false;
var mesajveli = false;
var divBildirim = false;
function MenuGetir(menu, vueanasayfa) {

    $(menu).html('');
    SqlWebActionSync("POST", "Menu", "MenuGetir", JSON.stringify({ TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM }), '', '', function (data) {
        dataMenu = data;//JSON.parse(data[0]["JSON_F52E2B61-18A1-11d1-B105-00805F49916B"])[0]["B"];      
        if (dataMenu != null) {
            var kategoriMenuler = $.grep(dataMenu, function (element, index) {
                return element.KOD.length == 3;
            });


            $.each(kategoriMenuler, function (index, value) {
                var kod = value.KOD;
                var kodLength = value.KOD.length;

                var item = this;
                
                if (this.ID_MENU == 1062) {
                    SqlWebActionSync("POST", "Anasayfa", "KullaniciSubeSinifGetir", JSON.stringify({ TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM }), '', '', function (data) {
                        if (data != null) {
                            var itemx = JSON.parse(data)[0];
                            htmKod += '<li class="nav-item  "> ' +
                                '<a href="http://www.kariyerplanim.com/okyanusinteraktif/giris?tc=' + session.TCKIMLIKNO + '&isim=' + session.AD + '&soyad=' + session.SOYAD + '&kampus=' + itemx.ID_SUBE + '&sinif=' + itemx.ID_SINIF + '&kod=Um1GMGFXaFRZWEpoWXc9PQ==" class="nav-link nav-toggle" target="_blank" >' +
                                '<i class="' + item.RESIM + '"></i> ' +
                                '<span class="title">' + item.AD + '</span> ' +
                                '<span class="arrow"></span> ' +
                                '</a> ' +
                                '</li>';
                        }
                    });
                }
                else if (this.ID_MENU == 1311) { // mentalUp
                    htmKod += '<li class="nav-item  "> ' +
                        '<a href="' + this.URL + '" class="nav-link nav-toggle" onclick="MenuDegis(' + this.ID_MENU + ')" target="_blank"> ' +
                        '<i class="' + this.RESIM + '"></i> ' +
                        '<span class="title">' + this.AD + '</span> ' +
                        '<span class="arrow"></span> ' +
                        '</a> ' +
                        altMenuGetir(kod, kodLength) +
                        '</li>';
                }
                else {

                    htmKod += '<li class="nav-item  "> ' +
                        '<a href="' + this.URL + '" class="nav-link nav-toggle" onclick="MenuDegis(' + this.ID_MENU + ')"> ' +
                        '<i class="' + this.RESIM + '"></i> ' +
                        '<span class="title">' + this.AD + '</span> ' +
                        '<span class="arrow"></span> ' +
                        '</a> ' +
                        altMenuGetir(kod, kodLength) +
                        '</li>';
                }
            });


            $(menu).append(htmKod);

            if (window.location.hash) {
                var adres = window.location.hash;
                adres = adres.replace(/[#?0-9]+/g, "");
                adres = adres.replace("undefined", "");

                var param = "";
                if (window.location.hash.indexOf('?') > -1) {
                    param = window.location.hash.substring(window.location.hash.indexOf('?') + 1, 999);
                    adres = window.location.hash.substring(0, window.location.hash.indexOf('?'));
                }

                adres = adres.replace("#", "");
                goto(adres, param);
            }

            window.onhashchange = function () {

                var adres = window.location.hash;
                adres = adres.replace(/[#?0-9]+/g, "");
                adres = adres.replace("undefined", "");
                var param = "";
                if (window.location.hash.indexOf('?') > -1) {
                    param = window.location.hash.substring(window.location.hash.indexOf('?') + 1, 999);
                    adres = window.location.hash.substring(0, window.location.hash.indexOf('?'));
                }

                adres = adres.replace("#", "");
                goto(adres, param);
            }
        }
        vueanasayfa.MesajGetir();
    });
}
$('.modal-backdrop').remove();

function altMenuGetir(kod, kodLength) {
    var altMenuler = $.grep(dataMenu, function (element, ix) {
        return (element.KOD.substring(0, kodLength) == kod) && (element.KOD.length == (kodLength + 3));
    });

    if (altMenuler.length == 0)
        return "";

    var htmKod = '<ul class="sub-menu">';

    $.each(altMenuler, function (index, value) {
        var subkod = value.KOD;
        var subkodLength = value.KOD.length;

        if (this.ID_MENU == 1333) {
            this.URL ='https://sinav.okyanuskoleji.k12.tr/index.html?TC=' + session.TCKIMLIKNO + '&OTURUM=' + session.OTURUM;
        }
        if (this.ID_MENU == 1302) {
            mesajogretmen = true;
        }

        if (this.ID_MENU == 1304) {
            mesajveli = true;
        }


        if (altMenuGetir(subkod, subkodLength) == "") {
            let newurl = "";
            let parametreKontrol = "";
            let httpKontrol = this.URL.includes("http");


            if (this.ID_MENU == -1 && httpKontrol) {
                newurl = new URL(this.URL);
                parametreKontrol = getParameterByName("appSite")
                if (parametreKontrol == null) {
                    let Tarih = new Date();
                    let gun = (Tarih.getDate() < 10 ? "0" + Tarih.getDate() : Tarih.getDate());
                    let ay = ((Tarih.getMonth() + 1) < 10 ? "0" + (Tarih.getMonth() + 1) : (Tarih.getMonth() + 1))
                    let gunAyYıl = "." + gun + "." + ay + "." + Tarih.getFullYear();
                    let pass = 'Pusulam' + gunAyYıl;

                    var cipher = crypt.encrypt(pass);
                    newurl.searchParams.append("appSite", cipher);
                    this.URL = newurl + "#";
                }


            }


            htmKod += '<li class="nav-item  "> ' +
                '<a href="' + this.URL + '" ' + (this.ID_MENU == -1 ? ' target="_blank" ' : '') + '  class="nav-link " onclick="MenuDegis(' + this.ID_MENU + ')"' + (this.URL.indexOf('#') == -1 ? 'target="_blank"' : '') + '> ' +
                '<i class="' + this.RESIM + '"></i> ' +
                '<span class="title">' + this.AD + '</span> ' +
                //'<span class="arrow"></span> ' +
                '</a> ' +
                altMenuGetir(subkod, subkodLength) +
                '</li>';
        }
        else {

            htmKod += '<li class="nav-item  "> ' +
                '<a href="' + this.URL + '" ' + (this.ID_MENU == -1 ? ' target="_blank" ' : '') + '  class="nav-link " onclick="MenuDegis(' + this.ID_MENU + ')"' + (this.URL.indexOf('#') == -1 ? 'target="_blank"' : '') + '> ' +
                '<i class="' + this.RESIM + '"></i> ' +
                '<span class="title">' + this.AD + '</span> ' +
                '<span class="arrow"></span> ' +
                '</a> ' +
                altMenuGetir(subkod, subkodLength) +
                '</li>';
        }
    });
    htmKod += '</ul>';

    return htmKod;
}

var ID_MENU = 1;
function MenuDegis(ID_MENU) {
    this.ID_MENU = ID_MENU;
}

function goto(url, param) {

    var newurl = 'Site/' + url + '.html';
    if (url == "/" || url == "#") {
        newurl = '/anasayfa.html';
    }
    window.scrollTo(0, 0);
    var pageContentBody = $('.page-content');

    App.startPageLoading();
    $.ajax({
        type: "GET",
        cache: false,
        url: newurl,
        dataType: "html",
        success: function (res) {
            App.stopPageLoading();
            pageContentBody.html(res);
            Layout.fixContentHeight(); // fix content height
            App.initAjax(); // initialize core stuff
        },
        error: function (xhr, ajaxOptions, thrownError) {
            App.stopPageLoading();
            //pageContentBody.html('<h4>Erişmeye Çalıştığınız Sayfada İçerik Yok !.</h4>');
            window.location.replace("/anasayfa.html");
        }
    });
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}


var keySize = 256;
var ivSize = 128;
var iterations = 100;

function encrypt(msg, pass) {
    var salt = CryptoJS.lib.WordArray.random(128 / 8);

    var key = CryptoJS.PBKDF2(pass, salt, {
        keySize: keySize / 32,
        iterations: iterations
    });

    var iv = CryptoJS.lib.WordArray.random(128 / 8);

    var encrypted = CryptoJS.AES.encrypt(msg, key, {
        iv: iv,
        padding: CryptoJS.pad.Pkcs7,
        mode: CryptoJS.mode.CBC

    });

    // salt, iv will be hex 32 in length
    // append them to the ciphertext for use  in decryption
    var transitmessage = salt.toString() + iv.toString() + encrypted.toString();
    return transitmessage;
}

function decrypt(transitmessage, pass) {
    var salt = CryptoJS.enc.Hex.parse(transitmessage.substr(0, 32));
    var iv = CryptoJS.enc.Hex.parse(transitmessage.substr(32, 32))
    var encrypted = transitmessage.substring(64);

    var key = CryptoJS.PBKDF2(pass, salt, {
        keySize: keySize / 32,
        iterations: iterations
    });

    var decrypted = CryptoJS.AES.decrypt(encrypted, key, {
        iv: iv,
        padding: CryptoJS.pad.Pkcs7,
        mode: CryptoJS.mode.CBC

    })
    return decrypted;
}

var vueanasayfa = new Vue({
    name: "Anasayfa",

    el: "#anasayfa",

    data: {
        Kullanici: null,
        KullaniciYetkiListesi: null,
        BildirimListe: null,
        MesajListe: null,
        href: 'anasayfa.html#anasayfa',
        menuDaralt: false,
        VeliYeniMesajSayisi: 0,
        VeliMesajListe: [],
        VeliMesajKullanici: null,
        OgretmenYeniMesajSayisi: 0,
        BildirimSayisi: 0,
        OgretmenMesajListe: [],
        OgretmenMesajKullanici: null,
        //VeliMesajListesiTimeOut: null,
        //OgretmenMesajListesiTimeOut: null,
        VeliMesajListesiAnasayfaTimeOut: null,
        OgretmenMesajListesiAnasayfaTimeOut: null,
        BildirimListesiAnasayfaTimeOut: null,
        bildirim: null,
        isTest: false,
        OLDPASS: '',
        NEWPASS: '',
        REPASS: '',
        ERRSHOW: false,
        ERRMSG: '',
        ERRSUCCESS: false,
        DISOGRENCI: 0,
        BUTTONDISABLE: false,
        SIFREDEGITSIR_GOSTER: true
    },

    methods: {
        ProfilGetir() {
            this.Kullanici = session;
            this.DISOGRENCI = this.Kullanici.DISOGRENCI;
            this.SIFREDEGITSIR_GOSTER = this.Kullanici.OVT;
            this.TemaDegistir();
        },

        MenuDaraltSelected() {
            this.menuDaralt = !this.menuDaralt;
        },

        TemaDegistir() {
            $("#style_color").attr("href", "assets/layouts/layout4/css/themes/light.min.css");
            document.title = 'Okyanus Pusulam';
        },

        KullaniciYetkileriniListele() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, "Anasayfa", "KullaniciYetkiListele", p, '', '', function (data, parent) {

                parent.KullaniciYetkiListesi = data;
                parent.MesajGetir();
            })
        },

        KullaniciTipDegistir(kademe) {
            window.location.replace("index.html?TC=" + session.TCKIMLIKNO + "&OTURUM=" + session.OTURUM + "&Kademe=" + kademe + "");
        },

        MesajGetir() {
            //kadirsil
            return;
            if (location.host == "pusulam") {
                return;
            }

            this.BildirimGetir()
            if (mesajogretmen) {
                this.OgretmenMesajGetir();
            }

            if (mesajveli) {
                this.VeliMesajGetir()
            }
        },

        VeliMesajGetir() {

            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                YENIMESAJ: true
            };

            WebPostLink(this, 'https://okyanusiletisim.okyanuskoleji.k12.tr/OkyanusApi/VeliMesaj/MesajListe', p, '-', '', function (data, parent) {
                parent.VeliMesajListe = data.MesajListe;

                var VeliYeniMesajSayisi = 0
                for (var i = 0; i < parent.VeliMesajListe.length; i++) {
                    VeliYeniMesajSayisi += parent.VeliMesajListe[i].OKUNMAYANMESAJSAYISI;
                }

                if (parent.VeliYeniMesajSayisi < VeliYeniMesajSayisi) {
                    document.getElementById("audioContainer").play();
                }

                if (typeof vueVeliMesajListesi !== 'undefined') {
                    if (parent.VeliYeniMesajSayisi != VeliYeniMesajSayisi) {
                        vueVeliMesajListesi.MesajKontrol();
                    }
                }

                parent.VeliYeniMesajSayisi = VeliYeniMesajSayisi;

                parent.$forceUpdate();
                parent.$nextTick(function () {
                    $('#chat-users-veli').slimScroll({
                        height: '300px'
                    });
                    $('#mesajveli').css('display', 'block');
                    clearTimeout(parent.VeliMesajListesiAnasayfaTimeOut);
                    parent.VeliMesajListesiAnasayfaTimeOut = setTimeout(function () { parent.VeliMesajGetir() }, 30000);
                });
            });

        },

        VeliKullaniciSec(KULLANICI) {

            this.VeliMesajKullanici = KULLANICI;
            window.location.href = "#iletisim/VeliMesajListesi";
        },

        OgretmenMesajGetir() {

            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                YENIMESAJ: true
            };

            WebPostLink(this, 'https://okyanusiletisim.okyanuskoleji.k12.tr/OkyanusApi/OgretmenMesaj/MesajListe', p, '-', '', function (data, parent) {
                parent.OgretmenMesajListe = data.MesajListeOgretmen;

                var OgretmenYeniMesajSayisi = 0
                if (parent.OgretmenMesajListe.length > 0) {
                    for (var i = 0; i < parent.OgretmenMesajListe[0].KULLANICILISTE.length; i++) {
                        OgretmenYeniMesajSayisi += parent.OgretmenMesajListe[0].KULLANICILISTE[i].OKUNMAYANMESAJSAYISI;
                    }
                }

                if (parent.OgretmenYeniMesajSayisi < OgretmenYeniMesajSayisi) {
                    document.getElementById("audioContainer").play();
                }

                if (typeof vueMesajlar !== 'undefined') {
                    if (parent.OgretmenYeniMesajSayisi != OgretmenYeniMesajSayisi) {
                        vueMesajlar.MesajKontrol();
                    }
                }

                parent.OgretmenYeniMesajSayisi = OgretmenYeniMesajSayisi;

                parent.$forceUpdate();
                parent.$nextTick(function () {
                    $('#chat-users-ogretmen').slimScroll({
                        height: '300px'
                    });
                    $('#mesajogretmen').css('display', 'block');
                    clearTimeout(parent.OgretmenMesajListesiAnasayfaTimeOut);
                    parent.OgretmenMesajListesiAnasayfaTimeOut = setTimeout(function () { parent.OgretmenMesajGetir() }, 30000);
                });
            });
        },

        OgretmenKullaniciSec(KULLANICI) {
            this.OgretmenMesajKullanici = KULLANICI;
            window.location.href = "#iletisim/Mesajlar";
        },

        BildirimAc(bildirim) {
            this.bildirim = bildirim;
            window.location.href = "#Iletisim/Bildirimler";

            this.$nextTick(function () {
                vueBildirimler.BildirimKontrol();
            });
        },

        BildirimGetir() {

            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM
            };

            $('#divBildirim').css('display', 'none');
            WebPostLink(this, 'https://okyanusiletisim.okyanuskoleji.k12.tr/OkyanusApi/ViuBildirim/BildirimListe', p, '-', '', function (data, parent) {

                parent.BildirimListe = data.Bildirim;
                parent.BildirimSayisi = parent.BildirimListe.length;
                parent.$forceUpdate();
                parent.$nextTick(function () {
                    $('#chat-users-bildirim').slimScroll({
                        height: '300px'
                    });
                    $('#divBildirim').css('display', 'block');
                    clearTimeout(parent.BildirimListesiAnasayfaTimeOut);
                    parent.BildirimListesiAnasayfaTimeOut = setTimeout(function () { parent.BildirimGetir() }, 30000);
                });
            });
        },

        IsTest() {
            try {
                WebPost(this, "Anasayfa", "IsTest", '', '', '', function (data, parent) {
                    parent.isTest = data;
                });

            } catch (e) {
                vueanasayfa.isTest = false;
            }
        },

        SifremiDegistir() {
            let oldpass = this.OLDPASS;
            let newpass = this.NEWPASS;
            let repass = this.REPASS;

            if (newpass === "" || repass === "" || oldpass === "") {
                this.MessageGoster("Tüm alanlar zorunludur.Lütfen tüm alanları doldunuz!", 3000, 0);
                return false
            }
            else if (newpass === oldpass) {
                this.MessageGoster("Eski şifre ve yeni şifre aynı olamaz!", 3000, 0);
            }
            else if (newpass !== repass) {
                this.MessageGoster("Yeni şifre ve yeni şifre tekrar alanları uyuşmuyor.Lütfen kontrol ederek tekrar deneyiniz!", 3000, 0);
                return false
            }
            else {
                var p = {
                    TCKIMLIKNO: session.TCKIMLIKNO,
                    OTURUM: session.OTURUM,
                    OLDPASS: this.OLDPASS,
                    NEWPASS: this.NEWPASS
                };
                WebPost(this, "Anasayfa", "SifremiDegistir", p, '', '', function (data, parent) {
                    let message = JSON.parse(data)[0].ERRMESSAGE;

                    if (JSON.parse(data)[0].ERRCODE === 0) {
                        message += ' Login sayfasına yönlendiriliyorsunuz!';
                        parent.CikisYap(6000);
                        this.ERRSUCCESS = 1;
                        this.BUTTONDISABLE = true;
                    }
                    parent.MessageGoster(message, 3000, this.ERRSUCCESS);
                    this.OLDPASS = '';
                    this.NEWPASS = '';
                    this.REPASS = '';
                })
            }
        },

        MessageGoster(message, time, basariliMi) {
            this.ERRSUCCESS = basariliMi;
            this.ERRSHOW = true;
            this.ERRMSG = message;
            setTimeout(function () {
                this.ERRSHOW = false
            }.bind(this), time)
            return false;
        },

        CikisYap(time) {
            setTimeout(function () {
                $.removeCookie("K");
                window.open("/login.html", "_self");
            }.bind(this), time)
        }

    },

    mounted() {
        MenuGetir($("#AnaMenu"), this);
        this.IsTest();
        this.ProfilGetir();
        this.KullaniciYetkileriniListele();

        $('.scrollable').on('mousewheel DOMMouseScroll', function (e) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;

            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        });
    }
})