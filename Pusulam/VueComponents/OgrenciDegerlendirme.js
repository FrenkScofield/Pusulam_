Vue.component("c-od-donem", {

    props: ['controller', 'idset', 'tcogrenci'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.DONEM" >{{u.DONEM}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                TC_OGRENCI: this.tcogrenci,
            };

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "OD_DonemListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.SelectedID = parent.Liste[0].DONEM;
                    parent.OnChange();
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
        tcogrenci() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-od-ay", {

    props: ['controller', 'idset', 'tcogrenci', 'donem'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ay </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.AY" >{{u.AY}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                TC_OGRENCI: this.tcogrenci,
                DONEM: this.donem,
            };

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "OD_AyListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    parent.SelectedID = parent.Liste[0].AY;
                    parent.OnChange();
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
        tcogrenci() {
            this.GetData();
        },
        donem() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});