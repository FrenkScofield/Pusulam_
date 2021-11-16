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

    props: ['controller', 'idsube',  'idkademe'],
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = this.idsube;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube, ID_KADEME: this.idkademe };
            WebPost(this, this.controller, "Kademe3ListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe() {
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

    props: ['controller', 'idsube', 'idkademe3', 'idsinif'],

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
        },
        idsube() {
            this.Yenile();
        },
        idsinif() {
            this.SelectedID = (this.idsinif == undefined || this.idsinif == null) ? this.SelectedID = 0 : this.idsinif;

            this.OnChange();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinif-tumu", {

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

            WebPost(this, this.controller, "SinifListeleTumu", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idkademe3() {
            this.Yenile();
        },
        idsube() {
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

    props: ['controller', 'idsinif', 'tcogrenci'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" >
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINIF: _idsinif };

            WebPost(this, this.controller, "OgrenciListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinif() {
            this.Yenile();
        },
        tcogrenci() {

            this.SelectedID = (this.tcogrenci == undefined || this.tcogrenci == null) ? this.SelectedID = 0 : this.tcogrenci;

            this.OnChange();
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

Vue.component("c-ogrenci-veli-sinav", {
    props: ['controller', 'tc', 'idkademe'],
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
        this.Listele();
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        Listele() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME: this.idkademe };

            WebPost(this, this.controller, "OgrenciListelebyVeliSinav", p, '', '', function (data, parent) {
                parent.Liste = data;
                parent.$nextTick(function () {
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = data[0].TCKIMLIKNO;
                        this.$emit('onchange', parent.SelectedID)
                    }
                });
            })
        }
    },
    watch: {
        idkademe() {
            this.Listele();
        },
        tc() {
            this.Listele();
        }

    },


    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

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
//Sınav Componenti 
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
        OnChange() {
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
                    <label class ="control-label col-md-3" style="vertical-align:middle; line-height:34px;">Puan Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbPuanTuru" title="Seçiniz...">
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, ID_SINAVTURU: this.idsinavturu };
            WebPost(this, this.controller, "PuanTuruListebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
                parent.$nextTick(function () {
                    if (data != null && data.length > 0) {
                        parent.SelectedID = data[0].ID_SINAVPUANTURU;
                        parent.$emit('onchange', parent.SelectedID)
                    }
                });
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
                            <option v-for="u in Liste">{{u.DERSAD}}</option>
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
//Rehberlik Envanter Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-rehberlik-envanter", {

    props: ['controller', 'tcogrenci', 'donem', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Rehberlik Envanteri Seçiniz.">
                            <option v-for="u in Liste" v-bind:value="u.ID_REHBERLIKENVANTER" >{{u.TESTADI}}</option>
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
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tcogrenci, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "RehberlikEnvanterTestListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].LISTE;
            })
        }
    },

    mounted() {
        this.Yenile();
    },
    watch: {
        donem() {
            this.Yenile();
        },
        tcogrenci() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Componenti 
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-ogrenci", {
    props: ['controller', 'idsinavturu', 'donem', 'tc', 'idset'],
    template: `
                <div class="form-md-line-input">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmb_Sinav">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>      
                        </select>
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
            var _idsinavturu = this.idsinavturu;
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURU: _idsinavturu, DONEM: _donem, TC_OGRENCI: _tc };
            WebPost(this, this.controller, "SinavListelebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();

        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
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
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },
});