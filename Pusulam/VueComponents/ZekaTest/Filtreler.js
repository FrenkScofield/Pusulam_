//----------------------------------------------------------------------------------------------
//Zeka Test Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-zekatest", {
    props: ['controller', 'idzekatest'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Zeka Testi </label>
                    <div class="col-md-9">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_ZEKATEST">{{u.AD}}</option>
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

    mounted: function () {
        this.Yenile();
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "ZekaTestListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                if (parent.SelectedID == 0 && parent.idzekatest != undefined) {
                    parent.SelectedID = parent.idzekatest;
                    vue.$forceUpdate();
                }
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idzekatest() {
            this.Yenile();
        }
    }
});


//----------------------------------------------------------------------------------------------
//Zeka Test Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-aktif-pasif", {
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-2" style="vertical-align:middle;">Durum </label>
                    <div class="col-md-10">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Tümü</option>
                            <option value="1">Sadece Aktif Olanlar</option>
                            <option value="2">Sadece Pasif Olanlar</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        }
    }
});