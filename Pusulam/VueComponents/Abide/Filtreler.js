//----------------------------------------------------------------------------------------------
//Abide Sınav Componenti (GENEL DERSLER)
//----------------------------------------------------------------------------------------------
Vue.component("c-abide-sinav", {
    props: ['controller', 'donem', 'idkademe3', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ABIDESINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                if (data != null && data != undefined) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = []
                }
            })
        }
    },

    watch: {
        donem() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Sınıf Dönem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-donem", {

    props: ['controller', 'idsube', 'idkademe3', 'donem', 'idset'],

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
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = this.idsube;
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube, ID_KADEME3: _idkademe3, DONEM: this.donem };

            WebPost(this, this.controller, "SinifListelebyKullaniciDonem", p, '', '', function (data, parent) {
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
        donem() {
            this.Yenile();
        },
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-abide-sinav-ogrenci", {
    props: ['controller', 'donem', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ABIDESINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, O_TCKIMLIKNO: this.tc };
            WebPost(this, this.controller, "SinavListelebyOgrenci", p, '', '', function (data, parent) {
                if (data != null && data != undefined) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = []
                }
            })
        }
    },

    watch: {
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-abide-kitapcik", {
    props: ['controller', 'idabidesinav', 'ders', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="idabidekitapcik" @change="OnChange">
                            <option value="0">Seçiniz...</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_ABIDEKITAPCIK">{{u.AD}} Kitapçığı</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            idabidekitapcik: 0,
            Liste: []
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.idabidekitapcik)
        },

        Yenile() {
            if (this.idabidesinav > 0 && this.ders != '' && this.tc != '') {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_ABIDESINAV: this.idabidesinav, DERS: this.ders, O_TCKIMLIKNO: this.tc };
                WebPost(this, this.controller, "KitapcikGetir", p, '', '', function (data, parent) {
                    if (data != null && data != undefined) {
                        parent.Liste = JSON.parse(data);
                        parent.idabidekitapcik = 0;
                        for (var i = 0; i < parent.Liste.length; i++) {
                            if (parent.Liste[i].SECILI) {
                                parent.idabidekitapcik = parent.Liste[i].ID_ABIDEKITAPCIK;
                                parent.$emit('onchange', parent.idabidekitapcik)
                            }
                        }

                        if (parent.Liste.length == 1) {
                            parent.idabidekitapcik = parent.Liste[0].ID_ABIDEKITAPCIK;
                            parent.$emit('onchange', parent.idabidekitapcik)
                        }
                    } else {
                        parent.Liste = []
                    }
                })
            }
        }
    },

    watch: {
        ders: function (yeni, eski) {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});