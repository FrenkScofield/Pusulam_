//----------------------------------------------------------------------------------------------
//Şube Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-zumre-calisma", {
    props: ['controller', 'idset', 'disabled', 'donem', 'idsube'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Zümre Çalışma </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_ZUMRECALISMA">{{u.AD}}</option>
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

        GetData() {
            ListeTemizle(this.Liste)
            if (this.donem != '') {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_SUBE: this.idsube };
                WebPost(this, this.controller, "ZumreCalismaListele", p, '', '', function (data, parent) {
                    if (data != null) {
                        parent.Liste = JSON.parse(data);
                        if (parent.Liste.length == 1) {
                            parent.SelectedID = parent.Liste[0].ID_ZUMRECALISMA;
                            parent.OnChange();
                        }
                    }
                });
            }
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
        donem() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

