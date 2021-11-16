//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller','id_sube'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Şube </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
        Selected() {
            this.SelectedID = this.id_sube;
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        id_sube() {
            this.Selected();
        }
    }
    

});
Vue.component("c-sube-yan", {

    props: ['controller', 'ilksecenek'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Şube Seçiniz...">
                            <option v-bind:value="0" v-bind:data-subtext="0" v-if="ilksecenek!=undefined">{{ilksecenek}}</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: -1,
            Liste: []
        }
    },

    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sube-yan12", {

    props: ['controller', 'ilksecenek'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Şube Seçiniz...">
                            <option v-bind:value="0" v-bind:data-subtext="0" v-if="ilksecenek!=undefined">{{ilksecenek}}</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: -1,
            Liste: []
        }
    },

    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
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

    props: ['controller', 'idsube', 'idset'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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
            var _idsube = this.idsube;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsube() {
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
Vue.component("c-sinavgrup2", {

    props: ['controller', 'id_kademe3', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="id_kademe3" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            Liste: []
        }
    },

    mounted() {
        this.Yenile();
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.id_kademe3)
        },

        Yenile() {
            var _idsube = this.idsube;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        id_kademe3() {
            this.Yenile();
        },
        idsube() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Grup Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavgrup3", {

    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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
        }
    },

    mounted() {
        var _idsube = 0;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube };
        WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinavgrup-multi", {

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
                    parent.Liste.push({ ID_SINAVGRUP: el.ID_SINAVGRUP, GRUP: el.GRUP })
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

Vue.component("c-sinavgrup-multisube", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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
            var _idsube = JSON.stringify(this.idsube);
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, SUBELER: _idsube };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
                parent.Liste = [];
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAVGRUP: el.ID_SINAVGRUP, GRUP: el.GRUP })
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


Vue.component("c-kademe3", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Grup Seçiniz.">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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
            var _idsube = this.idsube;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube };
            WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
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
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
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
            var _idsube = this.idsube;
            var _idsinavgrup = this.idsinavgrup;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_SINAVGRUP: _idsinavgrup };
            WebPost(this, this.controller, "SinifListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinavgrup() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-sinif-yan", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title = 'Sınıf Seçiniz..'>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
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
            var _idsube = this.idsube;
            var _idsinavgrup = this.idsinavgrup;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_SINAVGRUP: _idsinavgrup };
            WebPost(this, this.controller, "SinifListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinavgrup() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinif-multi", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
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
            var _idsube = this.idsube;
            var _idsinavgrup = this.idsinavgrup;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_KADEME3: _idsinavgrup };

            WebPost(this, this.controller, "SinifListelebyKullanici", p, '', '', function (data, parent) {
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

Vue.component("c-sinif-multi-id_subes", {

    props: ['controller', 'idsube', 'idsinavgrup'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
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
            var _idsube = JSON.stringify(this.idsube);
            var _idsinavgrup = JSON.stringify(this.idsinavgrup);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: _idsube, ID_KADEME3S: _idsinavgrup };

            WebPost(this, this.controller, "SinifListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.SUBEAD + ' - ' + el.AD })
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

Vue.component("c-sinif-multi-id_subes-donem", {

    props: ['controller', 'idsube', 'idsinavgrup', 'donem'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
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
            var _idsube = JSON.stringify(this.idsube);
            var _idsinavgrup = JSON.stringify(this.idsinavgrup);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBES: _idsube, ID_KADEME3S: _idsinavgrup, DONEM: this.donem };

            WebPost(this, this.controller, "SinifListelebyKullaniciDonem", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.SUBEAD + ' - ' + el.AD })
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
        },

        donem() {
            if (this.idsube.length > 0) {
                this.Yenile();
            }
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-sinif-multi-id_subes-donem-tekgrup", {

    props: ['controller', 'idsube', 'idsinavgrup', 'donem'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3">Sınıf </label>
                    <div class ="col-md-9">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
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
            var _idsube = JSON.stringify(this.idsube);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBES: _idsube, ID_KADEME3: this.idsinavgrup, DONEM: this.donem };

            WebPost(this, this.controller, "SinifListelebyKullaniciDonemMultiTekGrup", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.SUBEAD + ' - ' + el.AD })
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
        },

        donem() {
            if (this.idsube.length > 0) {
                this.Yenile();
            }
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-ogretmen-sinif", {

    props: ['controller', 'idsinif', 'tcogretmen'],

    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SINIF}}</option>
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

    mounted() {
        this.Yenile();
    },
    methods: {
        OnChange() {
            var _this = this;
            if (_this.SelectedID == undefined) {
                return;
            }

            vue.ID_KADEME3 = $.grep(this.Liste, function (el, j) {
                return el.ID_SINIF == _this.SelectedID;
            })[0].ID_KADEME3;
            if (vue.ID_KADEME3 == 14 || vue.ID_KADEME3 == 13 || vue.ID_KADEME3 == 12 || vue.ID_KADEME3 == 11) {
                vue.ortaOkul = true;
            }
            else {
                vue.ortaOkul = false;
            }

            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var tc = (this.tcogretmen != undefined && this.tcogretmen != '') ? this.tcogretmen : session.TCKIMLIKNO;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRETMEN: tc };
            WebPost(this, this.controller, "OgretmenSinifListesi", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].LISTE;
            })
        }
    },

    watch: {
        idsinif() {
            if (this.idsinif != undefined) {
                this.SelectedID = this.idsinif;
                $('.selectpicker').selectpicker('refresh');
            }
        },
        tcogretmen() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-danisman-ogretmen-sinif", {

    props: ['controller', 'idsinif', 'tcogretmen'],

    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SINIF}}</option>
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

    mounted() {
        this.Yenile();
    },
    methods: {
        OnChange() {
            var _this = this;
            if (_this.SelectedID == undefined) {
                return;
            }

            vue.ID_KADEME3 = $.grep(this.Liste, function (el, j) {
                return el.ID_SINIF == _this.SelectedID;
            })[0].ID_KADEME3;
            if (vue.ID_KADEME3 == 14 || vue.ID_KADEME3 == 13 || vue.ID_KADEME3 == 12 || vue.ID_KADEME3 == 11) {
                vue.ortaOkul = true;
            }
            else {
                vue.ortaOkul = false;
            }

            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var tc = (this.tcogretmen != undefined && this.tcogretmen != '') ? this.tcogretmen : session.TCKIMLIKNO;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRETMEN: tc };
            //var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "DanismanOgretmenSinifListesi", p, '', '', function (data, parent) {
                if (JSON.parse(data)[0].LISTE != undefined) {
                    parent.Liste = JSON.parse(data)[0].LISTE;
                    vue.tabDanisman = true;
                    vue.danismanSinifList = parent.Liste;
                }
            });
        }
    },

    watch: {
        idsinif() {
            if (this.idsinif != undefined) {
                this.SelectedID = this.idsinif;
                $('.selectpicker').selectpicker('refresh');
            }
        },
        tcogretmen() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3-multi", {

    props: ['controller', 'idsube', 'lise'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" title="Grup Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
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

            var _idsube = [];
            _idsube.push = this.idsube;
            //var _idsube = JSON.stringify('['+this.idsube+']');
            var _this = this;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  SUBELER: JSON.stringify(_idsube) };
            WebPost(this, this.controller, "Kademe3ListeleMultiSube", p, '', '', function (data, parent) {

                var list = [];

                if (_this.lise = true) {
                    list = $.grep(data, function (n, i) {
                        return n.ID_SINAVGRUP == 11
                            || n.ID_SINAVGRUP == 12
                            || n.ID_SINAVGRUP == 13
                            || n.ID_SINAVGRUP == 14
                            || n.ID_SINAVGRUP == 15
                            || n.ID_SINAVGRUP == 16
                            || n.ID_SINAVGRUP == 17
                            || n.ID_SINAVGRUP == 18;
                    });
                }
                else
                    list = data;



                parent.Liste = [];
                parent.Liste.push({ ID_SINAVGRUP: 0, GRUP: 'Tümü' })
                $.each(list, function (j, el) {
                    parent.Liste.push({ ID_SINAVGRUP: el.ID_SINAVGRUP, GRUP: el.GRUP })

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
//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu-tc", {

    props: ['controller', 'tc', 'donem', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz..." >
                            <option v-for="u in Liste" v-bind: value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavTuruListeleTc", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = data;
            });
        },
    },

    mounted() {

    },
    watch: {
        tc() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınıf Ders Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-ders", {

    props: ['controller', 'idsinif', 'idders'],

    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="c-sinif-ders" title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS" >{{u.DERSAD}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: this.idsinif };
            WebPost(this, this.controller, "SinifDersListesi", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].LISTE;
            });
        },
    },

    mounted() {

    },
    watch: {
        idsinif() {
            this.Yenile();
        },
        idders() {
            if (this.idders != undefined) {
                this.SelectedID = this.idders;
                $('.selectpicker').selectpicker('refresh');
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Şube Kademe Ders Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-kademe-ders", {

    props: ['controller', 'idsube', 'idkademe3', 'idders'],

    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.DERSAD" >{{u.DERSAD}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SubeKademeDersListesi", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].LISTE;
            });
        },
    },

    mounted() {

    },
    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        idders() {
            if (this.idders != undefined) {
                this.SelectedID = this.idders;
                $('.selectpicker').selectpicker('refresh');
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Şube Kademe Ders Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-kademe-ders-ogretmen", {

    props: ['controller', 'idsube', 'idkademe3', 'dersad', 'tcogretmen'],

    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Öğretmen Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" >{{u.ADSOYAD}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube, ID_KADEME3: this.idkademe3, DERSAD: this.dersad };
            WebPost(this, this.controller, "SubeKademeDersOgretmenListesi", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].LISTE;
            });
        },
    },

    mounted() {

    },
    watch: {
        idsube() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        dersad() {
            this.Yenile();
        },
        tcogretmen() {
            if (this.tcogretmen != undefined) {
                this.SelectedID = this.tcogretmen;
                $('.selectpicker').selectpicker('refresh');
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});