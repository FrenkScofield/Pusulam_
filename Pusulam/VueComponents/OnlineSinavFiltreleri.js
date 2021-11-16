//----------------------------------------------------------------------------------------------
//Sınav Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavturu", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Sınav Türü Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_VARLIK">{{u.AD}}</option>
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SinavTuruGetir", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-genel-ders", {
    template: `
                <div class ="form-md-line-input" style="line-height: 34px;">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: 0 };
            WebPost(this, 'DersUnite', "GenelDersListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});