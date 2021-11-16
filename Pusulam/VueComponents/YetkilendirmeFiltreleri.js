
//----------------------------------------------------------------------------------------------
//Menu Yetki Kademe Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe", {
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
        WebPost(this, this.controller, "KademeListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Menu Yetki Kullanıcı Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici-tipi", {
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
        WebPost(this, this.controller, "KullaniciTipiListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Menu Yetki Kullanıcı Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici", {
    props: ['controller', 'id_sube', 'id_kademe3', 'idkullanicitipi'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kullanıcı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"  data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD.trim().length>0?u.ADSOYAD:u.TCKIMLIKNO}}</option>
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
            var _id_sube = this.id_sube;
            var _id_kademe3 = this.id_kademe3;
            var _idkullanicitipi = this.idkullanicitipi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _id_sube, ID_KADEME3: _id_kademe3, ID_KULLANICITIPI: _idkullanicitipi };
            WebPost(this, this.controller, "KullaniciListele", p, '#filters', 'Yükleniyor..', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    //mounted() {
    //    var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
    //    WebPost(this, this.controller, "KadullaniciTipiListele", p, '', '', function (data, parent) {
    //        parent.Liste = data;
    //    })

    //},

    watch: {
        idkullanicitipi() {
            this.Yenile();
        },
        id_sube() {
            this.Yenile();
        },
        id_kademe3() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//KADEME3 Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {
    props: ['controller', 'id_kademe'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
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
            var id_kademe = this.id_kademe;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, KADEME: id_kademe };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe() {
            this.Yenile();
        }
    },
});