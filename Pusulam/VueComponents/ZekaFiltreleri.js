
//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti  MULTİSELECT
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-multi", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Şube </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE">{{u.AD}}</option>
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM  };
        WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_SUBE: 0, SUBENO: 0, AD: 'Tüm Şubeler' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_SUBE: el.ID_SUBE, SUBENO: el.SUBENO, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});


//----------------------------------------------------------------------------------------------
//Grup Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavgrup", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: JSON.stringify(this.idsube) };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                console.log(data)
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Sınıf Listesi Componenti  MULTİSELECT
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-multi", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange"  title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {

            var _idsinavgrup = this.idsinavgrup;
            //var _idsube = '';
            var _idsube = JSON.stringify(this.idsube)

            $.each(this.idsube, function (i) {
                _idsube += this + ',';
            });

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: JSON.stringify(this.idsube), ID_KADEME3: _idsinavgrup };
            WebPost(this, this.controller, "SinifListeleMulti", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD })
                });
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idsinavgrup() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Zeka Türü Listesi Componenti  MULTİSELECT
//----------------------------------------------------------------------------------------------
Vue.component("c-zeka-turu-multi", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Zeka Türü </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange"  title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTZEKATURU">{{u.ACIKLAMA}}</option>
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
        OnChange: function () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_TKTZEKATURU);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_TKTZEKATURU);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "TKTZekaTuruListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_TKTZEKATURU: 0, ACIKLAMA: 'Tüm Zeka Türleri' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_TKTZEKATURU: el.ID_TKTZEKATURU, ACIKLAMA: el.ACIKLAMA })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-sinavgrup-multi-bcyok", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select multiple="true"  class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            tumu: false,
            SelectedID: [],
            Liste: []
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAVGRUP);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
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
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAVGRUP);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var _idsube = JSON.stringify(this.idsube);
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, SUBELER: _idsube };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINAVGRUP: 0, GRUP: 'Tümü' })
                $.each(data, function (j, el) {
                    if (el.ID_SINAVGRUP != 4 && el.ID_SINAVGRUP != 5) {
                        parent.Liste.push({ ID_SINAVGRUP: el.ID_SINAVGRUP, GRUP: el.GRUP })
                    }
                });
            })
        }
    },

    watch: {
        idsube() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});