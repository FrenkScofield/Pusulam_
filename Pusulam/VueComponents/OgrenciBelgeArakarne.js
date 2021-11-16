Vue.component("c-sinav-turu-obarakarne", {
    props: ['controller', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Seçiniz.." >
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
                parent.Liste.push({ "ID_SINAVTURU": -1, "KOD": "Yazılı Yoklama" });

                parent.$nextTick(function () {
                    $('.selectpicker').selectpicker('selectAll');
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

Vue.component("c-yazili-donem-obarakarne", {
    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Seçiniz.." >
                            <option v-for="u in Liste" v-bind:value="u.KOD" selected="true">{{u.KOD}}</option>
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
        }
    },

    mounted() {
        this.Liste = [];
        this.Liste.push({ ID: 1, KOD: '1. Dönem' })
        this.Liste.push({ ID: 2, KOD: '2. Dönem' })
        
        this.$nextTick(function () {
            $('.selectpicker').selectpicker('selectAll');
        });
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});