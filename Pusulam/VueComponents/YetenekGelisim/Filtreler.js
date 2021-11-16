
Vue.component("c-yetenek-ders-listesi-hepsi", {

    props: ['controller', 'idset'],
    template: `

                <div class="form-group form-md-line-input">
                    <label class ="control-label col-md-3">Yetenek Seçiniz </label>
                    <div class ="col-md-9">
                        <select class="selectpicker form-control col-md-9" v-model="SelectedID"  @change="OnChange" title="Yetenek Dersini Seçiniz..">
                            <optgroup v-for="list in Liste" :label="''">
                                <option v-bind:value="list.ID_KATEGORI" class="optionGroup">{{list.AD}}</option>
                                <option v-for="u in list.A" v-bind:value="u.ID_KATEGORI" class="optionChild" v-if="list.A.length > 1 ">{{u.AD}}</option>
                            </optgroup>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: undefined,
            Liste: []
        }
    },
    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        GetData() {
            var _this = this;
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
            };
            WebPost(this, this.controller, "YG_KategoriListesi", p, "", "Yükleniyor...", function (data, parent) {
                _this.Liste = JSON.parse(data);
            });
        },
    },
    mounted() {
        this.GetData();
        this.SelectedID = this.idset;

    },
    watch: {
        idset() {
            this.SelectedID = this.idset;
            this.OnChange();
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-yetenek-ders-secilebilir", {

    props: ['controller', 'idset', 'tcogrenci'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Yetenek Seçiniz : </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Yetenek Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_KATEGORI" v-bind:data-subtext="u.ACIKLAMA">{{u.AD}}</option>
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

        GetData() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tcogrenci };
            WebPost(this, this.controller, "SecilebilirYetenekListesi", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                parent.Liste.unshift({ ID_KATEGORI: 0, AD: 'Seçiniz..' })
            })
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
        tcogrenci() {
            this.GetData();
        },

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});

Vue.component("c-yetenek-ders", {

    props: ['controller', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Yetenek Seçiniz : </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Yetenek Seçiniz...">                            
                            <optgroup v-for="list in Liste" :label="list.A.length > 1 ? list.AD : '' ">
                                <option v-for="u in list.A" v-bind:value="u.ID_KATEGORI">{{u.AD}}</option>
                            </optgroup>

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

        GetData() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "YetenekGelisimDersListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                parent.Liste.unshift({ ID_KATEGORI: 0, AD: 'Seçiniz..' ,A:[]})
            })
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