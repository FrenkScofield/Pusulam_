Vue.component("c-viu-raportur", {

    props: ['controller'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Rapor Türü </label>
                    <div class ="col-md-9">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TUR">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0,
            Liste: [
                { TUR: 1, AD: 'Mesaj Raporu' },
                { TUR: 2, AD: 'Arama Raporu' },
                { TUR: 3, AD: 'Veli Menü Logları' },
                { TUR: 4, AD: 'Viu Yoklama Raporu' },
                { TUR: 5, AD: 'Viu Randevu Takip' },
                { TUR: 6, AD: 'Bildirim Raporları' }
            ]
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-arama-sebep-multi", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Arama Sebep </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ARAMASEBEP">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_ARAMASEBEP);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_ARAMASEBEP);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "AramaSebepListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_ARAMASEBEP: 0, AD: 'Tümü' })
            $.each(JSON.parse(data), function (j, el) {
                parent.Liste.push({ ID_ARAMASEBEP: el.ID_ARAMASEBEP, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-arama-durum-multi", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Arama Durum </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ARAMADURUM">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_ARAMADURUM);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_ARAMADURUM);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "AramaDurumListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_ARAMADURUM: 0, AD: 'Tümü' })
            $.each(JSON.parse(data), function (j, el) {
                parent.Liste.push({ ID_ARAMADURUM: el.ID_ARAMADURUM, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-viu-kullanici", {
    props: ['controller'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kullanıcı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"  data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0,
            Liste: []
        }
    },

    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, 'ViuRaporlar', "ViuKullaniciListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-sube-tumu", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: -1,
            Liste: []
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPostLink(this, this.controller + 'SubeGetir', p, '', '', function (data, parent) {
                if (data.HATA == undefined) {
                    parent.Liste = data.Sube;
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SUBE;
                        parent.OnChange();
                    } else {
                        parent.Liste.unshift({ ID_SUBE: 0, AD: 'Tümü' });
                    }
                }
            });
        }
    },

    mounted() {
        this.GetData();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-kademe3-tumu", {
    props: ['controller', 'idset', 'disabled', 'idsube'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.GRUP}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: -1,
            Liste: []
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube };
            WebPostLink(this, this.controller + 'SubeGrupGetir', p, '', '', function (data, parent) {
                if (data.HATA == undefined) {
                    parent.Liste = data.SubeGrup;
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME3;
                        parent.OnChange();
                    } else {
                        parent.Liste.unshift({ ID_KADEME3: 0, GRUP: 'Tümü' });
                    }
                }
            });
        }
    },

    mounted() {
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },

        idsube() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-sinif-multi", {
    props: ['controller', 'idset', 'idsube', 'idkademe3', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Sınıf </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF" :style="'font-weight:'+[u.ID_SINIF == 0 ? 'bold' : '']">
                                {{u.ID_SINIF != 0 ? u.SUBEAD + ' - ' + u.KADEME3 + ' - ' + u.AD: 'Tümünü Seç'}}
                            </option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                ListeTemizle(this.SelectedID);
            }
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBE: this.idsube,
                ID_KADEME3: this.idkademe3,
            };

            ListeTemizle(this.Liste);
            WebPostLink(this, this.controller + 'SinifGetir', p, '', '', function (data, parent) {
                if (data.HATA == undefined) {
                    parent.Liste = data.Sinif;
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_SINIF: 0, AD: 'Tümünü Seç', SUBEAD: '', KADEME3: '' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINIF;
                        parent.OnChange();
                    }

                    try {
                        if ((parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                    } catch (e) { }
                }
            })
        },
    },

    watch: {
        idsube() {
            this.GetData();
        },
        idkademe3() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-ogrenci-multi", {
    props: ['controller', 'idset', 'idsube', 'idsiniflist', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Öğrenci </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="{TCKIMLIKNO_KIMICIN: u.TCKIMLIKNO, TCKIMLIKNO_KIME: u.VELITCKIMLIKNO}" :style="'font-weight:'+[u.TCKIMLIKNO == 0 ? 'bold' : '']">
                                {{u.TCKIMLIKNO != '0' ? u.SUBEAD + ' - ' + u.SINIFAD + ' - ' + u.OGRAD + ' ' + u.OGRSOYAD + ' ('+ u.VELIAD + ' ' + u.VELISOYAD +')': 'Tümünü Seç'}}
                            </option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false
        }
    },

    methods: {
        OnChange() {
            var exists = getObjects(this.SelectedID, 'TCKIMLIKNO_KIME', 0).length > 0 ? true : false;

            if (this.tumu == false && exists) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push({ TCKIMLIKNO_KIMICIN: this.Liste[i].TCKIMLIKNO, TCKIMLIKNO_KIME: this.Liste[i].VELITCKIMLIKNO });
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && !exists) { // tümü iptal
                this.tumu = false;
                ListeTemizle(this.SelectedID);
            }
            if (this.tumu == true && exists) {
                this.tumu = false;
                if (exists) {
                    var item = getObjects(this.SelectedID, 'TCKIMLIKNO_KIME', this.SelectedID)[0];
                    this.SelectedID.splice(item, 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push({ TCKIMLIKNO_KIMICIN: this.Liste[i].TCKIMLIKNO, TCKIMLIKNO_KIME: this.Liste[i].VELITCKIMLIKNO });
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBE: this.idsube,
                ID_SINIFLIST: JSON.stringify(this.idsiniflist),
            };

            ListeTemizle(this.Liste);
            WebPostLink(this, this.controller + 'OgrenciOzetGetir', p, '', '', function (data, parent) {
                if (data.HATA == undefined) {
                    parent.Liste = data.Ogrenci;
                    if (data != "[]") {
                        parent.Liste.unshift({ SUBEAD: '', SINIFAD: '', TCKIMLIKNO: 0, OGRAD: '', OGRSOYAD: 'Tümünü Seç', VELITCKIMLIKNO: 0, VELIAD: '', VELISOYAD: '' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0];
                        parent.OnChange();
                    }

                    try {
                        if ((parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                    } catch (e) { }
                }
            })
        },
    },

    watch: {
        idsube() {
            this.GetData();
        },

        idsiniflist() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});


Vue.component("c-rehber-danisman-sinif-multi", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Sınıf </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF" :style="'font-weight:'+[u.ID_SINIF == 0 ? 'bold' : '']">
                                {{u.ID_SINIF != 0 ? u.SUBEAD + ' - ' + u.AD: u.AD}}
                            </option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false,
        }
    },

    mounted() {
        this.GetData();
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                ListeTemizle(this.SelectedID);
            }
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
            };

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "RehberDanismanSinifListele", p, '', '', function (data, parent) {

                parent.Liste = JSON.parse(data);
                if (data != "[]") {
                    parent.Liste.unshift({ ID_SINIF: 0, AD: 'Tümünü Seç', SUBEAD: '' });
                }

                if (parent.Liste.length == 1) {
                    parent.SelectedID = parent.Liste[0].ID_SINIF;
                    parent.OnChange();
                }

                try {
                    if ((parent.idset != undefined && parent.idset != null)) {
                        parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                        parent.OnChange();
                    }
                } catch (e) { }

            })
        },
    },

    watch: {
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});