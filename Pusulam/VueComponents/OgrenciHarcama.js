Vue.component("c-urun-grup", {

    props: ['link', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ürün Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ürün Grubu Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_URUNGRUP">{{u.AD}}</option>
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
            var p = { ISLEM: 13 };
            WebPostLink(this, this.link, p, '', '', function (data, parent) {
                if (data.DURUM && data.NESNE != null)
                    parent.Liste = JSON.parse(data.NESNE);
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



Vue.component("c-urun-multi", {
    props: ['link', 'idurungrup', 'link'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Ürün </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_STOKKART">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: [],
            tumu: false,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KADEME3);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                ListeTemizle(this.SelectedID);
            }
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KADEME3);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                ISLEM: 14,
                ID_URUNGRUP: this.idurungrup
            };
            ListeTemizle(this.Liste);
            WebPostLink(this, this.link, p, '', '', function (data, parent) {
                if (data.DURUM && data.NESNE != null) {
                    parent.Liste = JSON.parse(data.NESNE);
                }
            });
        }
    },

    mounted() {
        this.GetData();
    },

    watch: {
        idurungrup() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});