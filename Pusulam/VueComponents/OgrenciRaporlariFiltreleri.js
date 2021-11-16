//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Şube Seçiniz..</option>
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

//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Grup Seçiniz..</option>
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = this.idsube;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube };
            WebPost(this, this.controller, "Kademe3ListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
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
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idsube', 'idkademe3'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Sınıf Seçiniz..</option>
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
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = this.idsube;
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_KADEME3: _idkademe3 };

            WebPost(this, this.controller, "SinifListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
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

    props: ['controller', 'idsinif'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Öğrenci Seçiniz..</option>
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
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsinif = this.idsinif;
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINIF: _idsinif };

            WebPost(this, this.controller, "OgrenciListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinif() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Öğrenci Veli Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-veli", {

    props: ['controller', 'tc'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Öğrenci Seçiniz..</option>
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
    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };

        WebPost(this, this.controller, "OgrenciListelebyVeli", p, '', '', function (data, parent) {
            parent.Liste = data;
            parent.$nextTick(function () {
                if (parent.Liste.length == 1) {
                    parent.SelectedID = data[0].TCKIMLIKNO;
                    this.$emit('onchange', parent.SelectedID)
                }
            });
        })
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
    },


    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});


//----------------------------------------------------------------------------------------------
//Rapor Tür Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-rapor-tur", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbRaporTur">
                            <option v-for="u in Liste" v-bind:value="u.ID_RAPOR" >{{u.AD}}</option>
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
        this.Liste = [
              { ID_RAPOR: 0, AD: 'Seçiniz' }
            , { ID_RAPOR: 1, AD: 'Deneme Sınav Sonuçları' }
            , { ID_RAPOR: 2, AD: 'Yazılı Yoklama Sonuçları' }
            , { ID_RAPOR: 3, AD: 'Konu Analizleri' }
            , { ID_RAPOR: 4, AD: 'Gelişim Raporu' }
            , { ID_RAPOR: 5, AD: 'Ara Karne' }
            , { ID_RAPOR: 6, AD: 'Rehb. Envanter Sonuçları' }
            , { ID_RAPOR: 7, AD: 'Ödev Takibi' }
            , { ID_RAPOR: 8, AD: 'Proje Dönem Ödevleri' }
            , { ID_RAPOR: 9, AD: 'Mapp Raporu' }
            , { ID_RAPOR: 10, AD: 'Gelişim Raporu OO' }
        ]
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" >
                            <option value="0">Sınav Türü Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
        WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinav-turu-arakarne", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" >
                            <option value="0">Sınav Türü Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
        WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
            parent.Liste = data;
            parent.Liste.push({ "ID_SINAVTURU": -1, "AD": "Yazılı Yoklama" });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav", {
    props: ['controller', 'idsinavturu', 'donem', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var _idsinavturu = this.idsinavturu;
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, DONEM: _donem, TC_OGRENCI: _tc };
            WebPost(this, this.controller, "SinavListelebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Sınav Puan Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-puan-turu", {
    props: ['controller', 'tc', 'idsinavturu'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Puan Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbPuanTuru">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVPUANTURU">{{u.AD}}</option>
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, ID_SINAVTURU: this.idsinavturu };
            WebPost(this, this.controller, "PuanTuruListebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        tc() {
            this.Yenile();
        },
        idsinavturu() {
            this.Yenile();
        }
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-ders", {
    props: ['dersler'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler">
                            <option value="0">Toplam Net</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVDERS">{{u.DERSAD}}</option>
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },

    },

    mounted() {
        //    this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        dersler() {
            console.log(this.dersler)
            this.Liste = this.dersler;
        },
    },
});


//----------------------------------------------------------------------------------------------
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Dönem Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.DONEM" >{{u.ACIKLAMA}}</option>
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
        WebPost(this, this.controller, "DonemListele", p, '', '', function (data, parent) {
            parent.Liste = data;
            for (var i = 0; i < parent.Liste.length; i++) {
                if (parent.Liste[i].AKTIF) {
                    parent.SelectedID = parent.Liste[i].DONEM;
                    parent.$emit('onchange', parent.SelectedID)
                }
            }
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-multi", {
    props: ['controller', 'idsinavturu', 'donem', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: []
        }
    },

    methods: {
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var _idsinavturu = this.idsinavturu;
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, DONEM: _donem, TC_OGRENCI: _tc };
            WebPost(this, this.controller, "SinavListelebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Ders Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-ders-multi", {
    props: ['dersler'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler"  title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.DERSAD">{{u.DERSAD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            tumu: false,
            SelectedID: [],
            Liste: []
        }
    },

    methods: {
        OnChange () {
            if (this.tumu == false && this.SelectedID.indexOf('TÜMÜ') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].DERSAD);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('TÜMÜ') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('TÜMÜ') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('TÜMÜ') > -1) {
                    this.SelectedID.splice($.inArray('TÜMÜ', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)

            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].DERSAD);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

    },

    mounted() {
        //    this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        dersler() {
            this.Liste = JSON.parse(JSON.stringify(this.dersler));
            var u = { DERSAD: 'TÜMÜ' };
            this.Liste.splice(0, 0, u);
        },
    },
});

