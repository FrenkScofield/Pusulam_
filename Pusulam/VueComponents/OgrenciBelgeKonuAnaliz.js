Vue.component("c-sinav-turu-obkonuanaliz", {
    props: ['controller', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Seçiniz.." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" selected="true">{{u.KOD}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID);
        },
        Yenile: function () {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavTuruListeleGrubaGore", p, '', '', function (data, parent) {
                parent.Liste = [];
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, KOD: el.KISAAD })
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
        idkademe3() {
            this.Yenile();
        }
    }
});

Vue.component("c-sinav-multi-obkonuanaliz", {
    props: ['controller', 'idsinavturu', 'donem', 'idkademe3'],
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURUS: JSON.stringify(_idsinavturu), DONEM: _donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListelebyGrupCoklu", p, '#FiltreDivOgrenci', '', function (data, parent) {
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
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        }
    },
});