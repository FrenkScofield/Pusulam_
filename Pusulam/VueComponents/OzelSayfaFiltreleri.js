Vue.component("c-sayfa-tur", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sayfa Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_OZELSAYFATURU" >{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SayfaTuruListe", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);                   
                }
            });
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