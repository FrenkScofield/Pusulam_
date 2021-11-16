//----------------------------------------------------------------------------------------------
//Menu Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-menu", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height: 34px;">
                    <label class="control-label col-md-1" style="vertical-align:middle;padding-top:0px;text-align:left;">Menü: </label>
                    <div class="col-md-11">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_MENU">{{u.AD}}</option>
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
        WebPost(this, this.controller, "MenuListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});