//----------------------------------------------------------------------------------------------
//İl Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-il", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-4" style="vertical-align:middle;">Şehir </label>
                    <div class ="col-md-8">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümünü Getir</option>
                            <option v-for="u in Liste" v-bind:value="u.IL">{{u.IL}}</option>
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
        }
    },

    mounted() {
        var idtktyasaraligi = this.idtktyasaraligi;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "IlListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Universite Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-universiteturu", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Üniversite Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümünü Getir</option>
                            <option v-for="u in Liste" v-bind:value="u.UNIVERSITETURU">{{u.UNIVERSITETURU}}</option>
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
        }
    },

    mounted() {
        var idtktyasaraligi = this.idtktyasaraligi;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "UniversiteTuruListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Puan Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-puanturu", {
    props: ['controller', 'tc'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <div class="col-md-12">
                        <select class ="form-control" v-model="SelectedID" @change="OnChange" style="margin-top: 0px;margin-bottom: 3px; border: 1px solid #EEE;">
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
        }
    },

    mounted() {
        var idtktyasaraligi = this.idtktyasaraligi;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, OGRTCKIMLIKNO: this.tc };
        WebPost(this, this.controller, "PuanTuruListele", p, '', '', function (data, parent) {
            if (data != null) {
                parent.Liste = JSON.parse(data);
                parent.$nextTick(function () {
                    parent.SelectedID = parent.Liste[0].ID_SINAVPUANTURU;
                    parent.$emit('onchange', parent.SelectedID);
                });
            }
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Puan Türü Genel Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-puanturugenel", {
    props: ['controller', 'tc'],
    template: `
                <div class ="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-4" style="vertical-align:middle;">Puan Türü </label>
                    <div class ="col-md-8">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümünü Getir</option>
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
        }
    },

    mounted() {
        var idtktyasaraligi = this.idtktyasaraligi;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, OGRTCKIMLIKNO: this.tc };
        WebPost(this, this.controller, "PuanTuruListeleGenel", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Bölüm Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-bolum", {
    props: ['controller', 'il', 'universiteturu', 'puanturu','idset'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Bölüm </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümünü Getir</option>
                            <option v-for="u in Liste" v-bind:value="u.BOLUM">{{u.BOLUM}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, IL: this.il, UNIVERSITETURU: this.universiteturu, PUANTURU: this.puanturu };
            WebPost(this, this.controller, "BolumListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        il() {
            this.Yenile();
        },
        universiteturu() {
            this.Yenile();
        },
        puanturu() {
            this.Yenile();
        },
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        }
    },
});

//----------------------------------------------------------------------------------------------
//Öğrenci Veli Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-veli", {

    props: ['controller', 'tc','idkademe'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-3">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" id="tc_c-ogrenci-veli">
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME: this.idkademe };

        WebPost(this, this.controller, "OgrenciListelebyVeli", p, '', '', function (data, parent) {
            parent.Liste = data;
            parent.$nextTick(function () {
                parent.SelectedID = data[0].TCKIMLIKNO;
                this.$emit('onchange', parent.SelectedID)
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
//Sınav Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavturu", {
    props: ['controller', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-12">
                        <select class ="selectpicker form-control" v-model="id_sinavturu" @change="OnChange">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" v-bind:data-subtext="u.KISAAD">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            Liste: [],
            id_sinavturu: 0
        }
    },

    methods: {
        OnChange () {
            this.$emit('onchange', this.id_sinavturu)
        },
        Listele() {

            var idtktyasaraligi = this.idtktyasaraligi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, OGRTCKIMLIKNO: this.tc };
            WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);

                parent.$nextTick(function () {
                    parent.id_sinavturu = parent.Liste[0].ID_SINAVTURU;
                    parent.$emit('onchange', parent.id_sinavturu);
                });
            })
        }
        
    },

    mounted() {
        this.Listele();
    },
    watch: {
        tc (){
            this.Listele();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});