
//----------------------------------------------------------------------------------------------
//Donem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("is-donem", {

    props: ['controller', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Dönem Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.DONEM">{{u.ACIKLAMA}}</option>
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
            WebPost(this, this.controller, "DonemListele", p, '', '', function (data, parent) {
                parent.Liste = data;
                for (var i = 0; i < parent.Liste.length; i++) {
                    if (parent.Liste[i].AKTIF) {
                        parent.SelectedID = parent.Liste[i].DONEM;
                        parent.$emit('onchange', parent.SelectedID)
                    }
                }
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

//----------------------------------------------------------------------------------------------
//EXCEL SUTUN Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("is-excel-sutun", {
    props: ['liste', 'count', 'baslik', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <label  v-if="baslik != undefined"
                            class ="control-label col-md-3" 
                            style="vertical-align:middle;"> {{baslik}} </label>
                    <div    v-bind:class="[baslik != undefined ? 'col-md-9' : 'col-md-12']"  >
                        <select class ="selectpicker form-control" v-model="SelectedID">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in liste" v-bind:value="u">{{u}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            //liste: [],
            //count: 0,
            SelectedID: undefined,
        }
    },

    watch: {
        count() {
            this.SelectedID = this.count == 0 ? undefined : this.SelectedID
            this.SelectedID = this.count == 1000 ? this.idset : this.SelectedID
        },
        SelectedID(val, old) {
            if (val == old) {
                return;
            }
            else {
                //if (old == undefined) { this.count++; }
                this.$emit('onchange', old, val);
            }
        },

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Assessment Kategori Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("is-assessment-kategori", {

    props: ['controller', 'idset', 'kategorilist'],
    template: `
                <div class ="form-md-line-input">                    
                    <div class ="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_ASSESSMENTKATEGORI">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
            //idset:0,
        }
    },
    mounted() {
        this.Yenile();
    },
    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            if (this.kategorilist != undefined) {
                this.Liste = this.kategorilist;

                if (this.idset != undefined) {
                    this.SelectedID = this.idset
                }
            }
            else {

                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
                WebPost(this, this.controller, "AssessmentKategoriListesi", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);

                    if (parent.idset != undefined) {
                        parent.SelectedID = parent.idset
                    }
                })
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? undefined : this.idset;
        },
    },

});