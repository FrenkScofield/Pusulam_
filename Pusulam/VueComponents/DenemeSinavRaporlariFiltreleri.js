//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Tüm Şubeler</option>
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
            parent.$nextTick(() => {
                parent.$emit('onchange', "0")
            });
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

    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tüm Gruplar</option>
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

    mounted() {
        this.Yenile();
    },

    methods: {
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = this.idsube;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
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
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3-multisube", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tüm Gruplar</option>
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

    mounted() {
        this.Yenile();
    },

    methods: {

        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = '';
            if (this.idsube == 'idsube') {
                _idsube = '[]';
            } else {
                _idsube = JSON.stringify(this.idsube);
            }

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, SUBELER: _idsube };
            WebPost(this, this.controller, "Kademe3ListeleMultiSube", p, '', '', function (data, parent) {
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
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Dönem Seçiniz</option>
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
                            <option value="0">Tüm Sınav Türleri</option>
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

//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------

Vue.component("c-sinav-turu-tc", {

    props: ['controller', 'tc', 'donem', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz..." >
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
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavTuruListeleTc", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = data;
            });
        },
    },

    mounted() {

    },
    watch: {
        tc() {
            this.Yenile();
        },
        donem() {
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
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tüm Sınıflar</option>
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
            var _idsube = this.idsube[0];
            var _idsinavgrup = this.idsinavgrup;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube, ID_SINAVGRUP: _idsinavgrup };
            WebPost(this, this.controller, "SinifListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinavgrup() {
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