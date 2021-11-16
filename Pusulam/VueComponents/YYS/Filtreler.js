//----------------------------------------------------------------------------------------------
//YYS Sınav Componenti (GENEL DERSLER)
//----------------------------------------------------------------------------------------------
Vue.component("c-yys-sinav", {
    props: ['controller', 'donem', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_YYSSINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
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
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
