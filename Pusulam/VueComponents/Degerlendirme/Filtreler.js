//----------------------------------------------------------------------------------------------
//Personel Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-personel", {

    props: ['controller', 'idsube', 'idkademe', 'idkullanicitipi'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Personel </label>
                    <div class ="col-md-9">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" data-selected-text-format="count>3">
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;
            var ID_KULLANICITIPI = this.idkullanicitipi == null ? 0 : this.idkullanicitipi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, ID_KULLANICITIPI: ID_KULLANICITIPI };

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = [];
                }
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
        idkullanicitipi() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
Vue.component("c-ogretmen-multi", {

    props: ['controller', 'idsube', 'idkademe'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğretmen </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf('0') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('0') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('0') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('0') > -1) {
                    this.SelectedID.splice($.inArray('0', this.SelectedID), 1);
                }
            } 
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;
           

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, IDAREKULLANICITIPI: "3" }; // öğretmenler

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.Liste.unshift({ TCKIMLIKNO: '0', ADSOYAD: 'Tümü' });
                } else {
                    parent.Liste = [];
                }
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
Vue.component("c-idare-multi", {

    props: ['controller', 'idsube', 'idkademe'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">İdareci </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf('0') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('0') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('0') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('0') > -1) {
                    this.SelectedID.splice($.inArray('0', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;
         

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, IDAREKULLANICITIPI:"53,54" }; // idareci

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.Liste.unshift({ TCKIMLIKNO: '0', ADSOYAD: 'Tümü' });
                } else {
                    parent.Liste = [];
                }
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-rehber-multi", {
    props: ['controller', 'idsube', 'idkademe'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Rehber </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf('0') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('0') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('0') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('0') > -1) {
                    this.SelectedID.splice($.inArray('0', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;


            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, IDAREKULLANICITIPI: "26" }; // rehber

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.Liste.unshift({ TCKIMLIKNO: '0', ADSOYAD: 'Tümü' });
                } else {
                    parent.Liste = [];
                }
            })
        }
    },
    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-idare_rehber_ogretmen-multi", {
    props: ['controller', 'idsube', 'idkademe'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">İdareci-Rehber-Öğretmen </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf('0') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('0') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('0') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('0') > -1) {
                    this.SelectedID.splice($.inArray('0', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;


            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, IDAREKULLANICITIPI: "3,26,53,54" }; // rehber

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.Liste.unshift({ TCKIMLIKNO: '0', ADSOYAD: 'Tümü' });
                } else {
                    parent.Liste = [];
                }
            })
        }
    },
    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});



//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
        WebPost(this, this.controller, "SubeListelebyKullanici", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sube-multi", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Şube </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
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
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListelebyKullanici", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_SUBE: 0, SUBENO: 0, AD: 'Tüm Şubeler' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_SUBE: el.ID_SUBE, SUBENO: el.SUBENO, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idsube', 'idkademe3'],

    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            var _idsube = this.idsube;
            var _idkademe3 = this.idkademe3;

            if (_idkademe3 != null) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube, ID_KADEME3: _idkademe3 };

                WebPost(this, this.controller, "SinifListelebyKullanici", p, '', '', function (data, parent) {
                    parent.Liste = data;
                })
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        idsube() {
            this.Yenile
        },

        idkademe3() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Öğrenci Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci", {

    props: ['controller', 'idsube', 'idkademe3', 'idsinif', 'idlistelemetipi', 'tc', 'tur','kursinif'],

    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Öğrenci </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD + ' '+u.SOYAD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            if (this.idsinif != null) {
                var _idsinif = this.idsinif;

                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: _idsinif, ID_LISTELEMETIPI: this.idlistelemetipi, YORUMTUR: this.tur, KUR_SINIF: this.kursinif };

                WebPost(this, this.controller, "OgrenciListele", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                    vue.OGRENCILIST = parent.Liste;
                })
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },

        idkademe3() {
            this.Yenile();
        },

        idsinif() {
            this.Yenile();
        },

        idlistelemetipi() {
            this.Yenile();
        },

        kursinif() {
            this.Yenile();
        },

        tc() {
            this.SelectedID = this.tc;
        },

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Öğrenci Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-v4", {

    props: ['controller', 'idsube', 'idkademe3', 'idsinif', 'idlistelemetipi', 'tc', 'tur'],

    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Öğrenci </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD + ' '+u.SOYAD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            if (this.idsinif != null) {
                var _idsinif = this.idsinif;

                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: _idsinif, ID_LISTELEMETIPI: this.idlistelemetipi, YORUMTUR: this.tur };

                WebPost(this, this.controller, "OgrenciListeleV4", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                    vue.OGRENCILIST = parent.Liste;
                })
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },

        idkademe3() {
            this.Yenile();
        },

        idsinif() {
            this.Yenile();
        },

        idlistelemetipi() {
            this.Yenile();
        },

        tc() {
            this.SelectedID = this.tc;
        },

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});



//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            if (this.idsube != null) {
                var _idsube = this.idsube;

                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube };
                WebPost(this, this.controller, "Kademe3ListelebyKullanici", p, '', '', function (data, parent) {
                    parent.Liste = data;
                });
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Listeleme Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-listelemetipi", {

    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Listeleme Şekli </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümü</option>
                            <option value="1">Değerlendirilenler</option>
                            <option value="2">Değerlendirilmeyenler</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Danışman Öğretmen Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-danisman-ogretmen-sinif", {

    props: ['controller', 'idsinif'],

    template: `
                <div class ="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SINIF}}</option>
                        </select>
                    </div>
                </div>
            `
    ,

    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
        }
    },

    mounted() {
        this.Yenile();
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            //var p = { TCKIMLIKNO: '47056005108', OTURUM: 'A8076D7C-F125-4C7A-BFEC-9AE67F4894A2' };
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "DanismanOgretmenSinifListesi", p, '', '', function (data, parent) {
                if (JSON.parse(data)[0].LISTE != undefined) {
                    parent.Liste = JSON.parse(data)[0].LISTE;
                    vue.tabDanisman = true;
                    vue.danismanSinifList = parent.Liste;
                }
            });
        }
    },

    watch: {
        idsinif() {
            if (this.idsinif != undefined) {
                this.SelectedID = this.idsinif;
                $('.selectpicker').selectpicker('refresh');
            }
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Kategori Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kategori", {

    props: ['controller', 'id'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Kategori </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="id" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DEGERLENDIRMEKATEGORI">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            Liste: []
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "KategoriListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        });
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.id);
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//PERSONEL Kademe BY ID_SUBE Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe-sube-personel", {
    props: ['controller', 'idsube'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kademe </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME">{{u.AD}}</option>
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
        },

        Yenile: function () {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube };
            WebPost(this, this.controller, "PersonelSubeKademeGetir", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        }
    },

    mounted() {

    },

    watch: {
        idsube() {
            if (this.idsube != undefined) {
                this.Yenile();
            }
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//PERSONEL Kademe BY ID_SUBE Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe-personel", {
    props: ['controller'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kademe </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME">{{u.AD}}</option>
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
        WebPost(this, this.controller, "PersonelKademeGetir", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Kullanıcı Tipi Componenti (MÜDÜR - MÜDÜR YARDIMCISI)
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici-tipimmy", {
    props: ['controller'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kullanıcı Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"  data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KULLANICITIPI">{{u.AD}}</option>
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
        WebPost(this, this.controller, "KullaniciTipiListeleMMY", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//PERSONEL Kademe BY ID_SUBE Componenti - Multi
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe-sube-personel-multi", {
    props: ['controller', 'idsube'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kademe </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" multiple="true" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME">{{u.AD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile: function () {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube };
            WebPost(this, this.controller, "PersonelSubeKademeGetir", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_KADEME: -1, AD: 'Tümü' });
                for (var i = 0; i < JSON.parse(data).length; i++) {
                    parent.Liste.push(JSON.parse(data)[i]);
                }
            })
        }
    },

    mounted() {

    },

    watch: {
        idsube() {
            if (this.idsube != undefined) {
                this.Yenile();
            }
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Kullanıcı Tipi Componenti (MÜDÜR - MÜDÜR YARDIMCISI) - Multi
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici-tipimmy-multi", {
    props: ['controller'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kullanıcı Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" multiple="true" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_KULLANICITIPI">{{u.AD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KULLANICITIPI);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KULLANICITIPI);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "KullaniciTipiListeleMMY", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_KULLANICITIPI: -1, AD: 'Tümü' });
            for (var i = 0; i < JSON.parse(data).length; i++) {
                parent.Liste.push(JSON.parse(data)[i]);
            }
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Personel Listesi Componenti - Multi
//----------------------------------------------------------------------------------------------
Vue.component("c-personel-multi", {

    props: ['controller', 'idsube', 'idkademe', 'idkullanicitipi'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Personel </label>
                    <div class ="col-md-9">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange" multiple="true" data-live-search="true" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_SUBE = this.idsube == null ? 0 : this.idsube;
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;
            var ID_KULLANICITIPI = this.idkullanicitipi == null ? 0 : this.idkullanicitipi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: ID_SUBE, ID_KADEME: ID_KADEME, ID_KULLANICITIPI: ID_KULLANICITIPI };

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = [];
                    parent.Liste.push({ TCKIMLIKNO: -1, ADSOYAD: 'Tümü' });
                    for (var i = 0; i < JSON.parse(data).length; i++) {
                        parent.Liste.push(JSON.parse(data)[i]);
                    }
                } else {
                    parent.Liste = [];
                }
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
        idkullanicitipi() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Periyot Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-periyot", {

    props: ['controller', 'donem', 'idkademe', 'idkullanicitipi'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Periyot </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DEGERLENDIRMEPERIYOT">{{u.AD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            if (this.idkullanicitipi != null && this.idkademe != null) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME: this.idkademe, ID_KULLANICITIPI: this.idkullanicitipi };
                WebPost(this, this.controller, "PeriyotListele", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                });
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        idkullanicitipi() {
            this.Yenile();
        },

        idkademe() {
            this.Yenile();
        },

        donem() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Periyot Componenti Tipsiz
//----------------------------------------------------------------------------------------------
Vue.component("c-periyot-tipsiz", {

    props: ['controller', 'donem', 'idkademe', 'raportipi'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Periyot </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DEGERLENDIRMEPERIYOT">{{u.AD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            if (this.raportipi != null && this.idkademe != null) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME: this.idkademe, RAPORTIPI: this.raportipi };
                WebPost(this, this.controller, "PeriyotListeleTipsiz", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                });
            } else {
                this.Liste = [];
            }
        }
    },

    watch: {
        raportipi() {
            this.Yenile();
        },

        idkademe() {
            this.Yenile();
        },

        donem() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Rapor Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-raportipi", {

    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Listeleme Şekli </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option value="1">360 Derece PDS</option>
                            <option value="2">Öğrenci Değerlendirme</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Personel Listesi Componenti - Multi
//----------------------------------------------------------------------------------------------
Vue.component("c-personel-multi-sube-multi", {

    props: ['controller', 'idsube', 'idkademe', 'idkullanicitipi'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Personel </label>
                    <div class ="col-md-9">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange" multiple="true" data-live-search="true" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var ID_KADEME = this.idkademe == null ? 0 : this.idkademe;
            var ID_KULLANICITIPI = this.idkullanicitipi == null ? 0 : this.idkullanicitipi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBELIST: JSON.stringify(this.idsube), ID_KADEME: ID_KADEME, ID_KULLANICITIPI: ID_KULLANICITIPI };

            WebPost(this, this.controller, "PersonelListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = [];
                    parent.Liste.push({ TCKIMLIKNO: -1, ADSOYAD: 'Tümü' });
                    for (var i = 0; i < JSON.parse(data).length; i++) {
                        parent.Liste.push(JSON.parse(data)[i]);
                    }
                } else {
                    parent.Liste = [];
                }
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
            this.Yenile();
        },
        idkullanicitipi() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Değerlendirme Yapma Durumu Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-degerlendirme-durum", {

    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Durum </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Değerlendirme Yapmayanlar</option>
                            <option value="1">Değerlendirme Yapanlar</option>
                            <option value="2">Değerlendirilmeyenler</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//Sinav Listele
Vue.component("c-sinav", {

    props: ['controller'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
        WebPost(this, this.controller, "SubeListelebyKullanici", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//Onlne Test Sinav Listele
Vue.component("c-testSinav", {

    props: ['controller','idkademe3'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3,};
        WebPost(this, this.controller, "SinavBilgiGetir", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});





