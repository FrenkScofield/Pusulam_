//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Şube Seçiniz..</option>
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
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListelebyKullanici", p, '', '', function (data, parent) {
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
                    <div class="col-md-2">
                        <select multiple="multiple" class="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Şube Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE">{{u.AD}}</option>
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
        OnChange: function () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
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
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
                }
                this.$emit('onchange', this.SelectedID);
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SubeListelebyKullanici", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_SUBE: 0, AD: 'Tüm Şubeler' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_SUBE: el.ID_SUBE, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {

    props: ['controller', 'idsubelist', 'idset'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="sel-kademe3">
                            <option value=0>Grup Seçiniz..</option>
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
            this.$emit('onchange', parseInt(this.SelectedID))
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  SUBELER: JSON.stringify(this.idsubelist) };
            WebPost(this, this.controller, "Kademe3ListeleMultiSube", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsubelist() {
            this.Yenile();
            //this.SelectedID= 0;
            //$('.selectpicker').selectpicker('refresh');
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});


Vue.component("c-kademe3-tek-sube", {

    props: ['controller', 'idsube', 'idset'],
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
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});


//----------------------------------------------------------------------------------------------
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-multi", {

    props: ['controller', 'idsubelist', 'idkademe3'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SUBEAD+' - '+u.AD}}</option>
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
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: JSON.stringify(this.idsubelist), ID_KADEME3: _idkademe3 };

            WebPost(this, this.controller, "SinifListelebyKullaniciMultiSube", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar', SUBEAD: '' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD })
                });
            })
        }
    },

    watch: {
        idsubelist() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinif-multi-col12", {

    props: ['controller', 'idsubelist', 'idkademe3'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select multiple="multiple" class ="selectpicker form-control selsinif" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." data-selected-text-format="count>2">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SUBEAD+' - '+u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBES: JSON.stringify(this.idsubelist), ID_KADEME3: this.idkademe3 };

            WebPost(this, this.controller, "SinifListelebyKullaniciMultiSube", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar', SUBEAD: '' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD });
                });
            })
        },
    },

    watch: {
        idsubelist() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("c-sinif-multi-karma-liste", {

    props: ['controller', 'idsubelist', 'idkademe3', 'divid', 'sinifturu', 'ist', 'kursinif'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select multiple="multiple" class="selectpicker form-control selsinif"  v-model="SelectedID"
                        @change="OnChange" title="Sınıf Seçiniz..." :id=divid data-selected-text-format="count>2">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SUBEAD+' - '+u.AD}}</option>
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
                Liste: [],
                ist: 0
            }
        },
    mounted() {
        this.Yenile();
    },
    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID, this.divid);
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
            this.$emit('onchange', this.SelectedID, this.divid)

            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID, this.divid);
            }
        },

        Yenile() {

            var idSubeList = jQuery.type(this.idsubelist) == "array" ? JSON.stringify(this.idsubelist) : this.idsubelist;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBES: idSubeList, ID_KADEME3: this.idkademe3, KUR: this.kursinif };

            WebPost(this, this.controller, "SinifListelebyKullaniciMultiSube", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar', SUBEAD: '' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD });
                    try {
                        vue.siniflar.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD })
                    } catch (e) {

                    }
                });
            })
        },

        asd() {
            var _this = this;
            var idSinav = this.divid.split("-")[1];
            if (this.ist == idSinav) {
                this.SelectedID = [];
                $.each(_this.sinifturu, function (a, el) {
                    var idSinifList = $.grep(_this.Liste, function (sel, b) {
                        return sel.AD.indexOf(el) > -1 || el == "Tümü";
                    });
                    $.each(idSinifList, function (b, sel) {
                        if (_this.SelectedID.indexOf(sel.ID_SINIF) == -1) {
                            _this.SelectedID.push(sel.ID_SINIF);
                        }
                    });

                    if (_this.sinifturu.length == a + 1) {
                        _this.$emit('onchange', _this.SelectedID, _this.divid);;
                    }
                });
                if (this.sinifturu.length == 0) {
                    this.$emit('onchange', this.SelectedID, this.divid);
                }
            }
        },
    },

    watch: {
        idsubelist() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        kursinif() {
            this.Yenile();
        },
        ist() {
            this.asd();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});


Vue.component("c-sinif-multi-donem", {

    props: ['controller', 'idsubelist', 'idkademe3', 'donem', 'eskidonemsinif', 'idset'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SUBEAD+' - '+u.AD}}</option>
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
            var _idkademe3 = this.idkademe3;
            var _donem = this.donem;
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBES: JSON.stringify(this.idsubelist), ID_KADEME3: _idkademe3, DONEM: _donem, ESKIDONEMSINIF: _eskidonemsinif };

            WebPost(this, this.controller, "SinifListelebyKullaniciMultiSubeDonem", p, '', '', function (data, parent) {
                parent.Liste = [];
                if (data.length > 0) {
                    parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar', SUBEAD: '' })
                    $.each(data, function (j, el) {
                        parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD })
                    });
                }
            })
        }
    },

    watch: {
        idsubelist() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        eskidonemsinif() {
            this.Yenile();
        },
        idset() {

            if (this.idset == undefined || this.idset == null || this.idset == true) {
                this.Yenile();
                this.SelectedID = [];
                this.tumu = false;
                this.OnChange();
            }
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinif-donem", {

    props: ['controller', 'idsube', 'idkademe3', 'donem'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Sınıf Seçiniz..</option>
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
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: _idsube, ID_KADEME3: _idkademe3, DONEM: this.donem };

            WebPost(this, this.controller, "SinifListelebyKullaniciDonem", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idkademe3() {
            this.Yenile();
        },
        idsube() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});



Vue.component("c-sinif-donem-alan-multi", {

    props: ['controller', 'idsubelist', 'idkademe3', 'donem', 'eskidonemsinif', 'sinifalan'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti" data-selected-text-format="count>2">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.SUBEAD+' - '+u.AD}}</option>
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
            // tümü seçilince
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
                return;
            }
            // tümü iptal
            if (this.tumu == true && this.SelectedID.indexOf(0) == -1) {
                this.tumu = false;
                this.SelectedID = [];
            }
            // tümü seçili iken bir tanesinin seçimi kaldırılırsa
            if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(0) > -1) {
                    this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
                }
            }

            this.$emit('onchange', this.SelectedID);

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
            var _idkademe3 = this.idkademe3;
            var _donem = this.donem;
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: JSON.stringify(this.idsubelist), ID_KADEME3: _idkademe3, DONEM: _donem, ESKIDONEMSINIF: _eskidonemsinif };

            WebPost(this, this.controller, "SinifListelebyKullaniciMultiSubeDonem", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar', SUBEAD: '' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD, SUBEAD: el.SUBEAD })
                });
            })
        },


        AlanSec() {
            var _this = this;
            this.SelectedID = [];
            $.each(_this.sinifalan, function (a, el) {
                var idSinifList = $.grep(_this.Liste, function (sel, b) {
                    return sel.AD.indexOf(el) > -1 || el == "Tümü";
                });
                $.each(idSinifList, function (b, sel) {
                    if (_this.SelectedID.indexOf(sel.ID_SINIF) == -1) {
                        _this.SelectedID.push(sel.ID_SINIF);
                    }
                });
                if (_this.sinifalan.indexOf("Tümü") > -1) {
                    _this.tumu = true;
                    _this.SelectedID = [];
                    for (i = 0; i < _this.Liste.length; i++) {
                        _this.SelectedID.push(_this.Liste[i].ID_SINIF);
                    }
                    _this.$emit('onchange', _this.SelectedID);
                }
                else if (_this.sinifalan.length == a + 1) {
                    _this.$emit('onchange', _this.SelectedID, _this.divid);;
                }
            });
            if (this.sinifalan.length == 0) {
                this.$emit('onchange', this.SelectedID, this.divid);
            }

        },
    },

    watch: {
        idsubelist() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        eskidonemsinif() {
            this.Yenile();
        },
        sinifalan() {
            this.AlanSec();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Dönem Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.DONEM" >{{u.ACIKLAMA}}</option>
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
        }
    },

    mounted() {
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu", {

    props: ['controller', 'sinavturu'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" >
                            <option value="0">Sınav Türü Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinav-turu-tc", {

    props: ['controller', 'tc', 'donem', 'idkademe3', 'eskidonemsinif', 'ogrencidonem'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz..." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, DONEM: this.donem, ID_KADEME3: this.idkademe3, ESKIDONEMSINIF: _eskidonemsinif, OGRENCIDONEM: this.ogrencidonem };
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
        eskidonemsinif() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınav Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav", {
    props: ['controller', 'idsinavturu', 'donem', 'idkademe3', 'eskidonemsinif', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
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
            var _idsinavturu = this.idsinavturu;
            var _donem = this.donem;
            var _ogrencidonem = this.ogrencidonem;
            var _idkademe3 = this.idkademe3;
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, DONEM: _donem, ID_KADEME3: _idkademe3, ESKIDONEMSINIF: _eskidonemsinif, OGRENCIDONEM: _ogrencidonem };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                parent.Liste = $.grep(data, function (n, i) {
                    return n.DURUM == 2;
                });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        eskidonemsinif() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-multi", {
    props: ['controller', 'idsinavturu', 'donem', 'idkademe3', 'eskidonemsinif', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },



        Yenile() {
            var _donem = this.donem;
            var _ogrencidonem = this.ogrencidonem;
            var _idkademe3 = this.idkademe3;
            var _idsinavturu = this.idsinavturu;
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, DONEM: _donem, ID_KADEME3: _idkademe3, ESKIDONEMSINIF: _eskidonemsinif, OGRENCIDONEM: _ogrencidonem };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                //parent.Liste = $.grep(data, function (n, i) {
                //        return n.DURUM == 2; 
                //});
                parent.Liste = [];
                parent.Liste.push({ ID_SINAV: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    if (el.DURUM == 2) {
                        parent.Liste.push({ ID_SINAV: el.ID_SINAV, AD: el.AD });
                    }
                });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
        eskidonemsinif() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-ders-multi", {
    props: ['controller', 'idsinavturu', 'idkademe3', 'donem', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID);
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
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },



        Yenile() {
            var _idkademe3 = this.idkademe3;
            var _idsinavturu = this.idsinavturu;
            var _ogrencidonem = this.ogrencidonem;
            var _donem = this.donem == undefined || this.donem == null ? '' : this.donem;

            var _this = this;
            _this.Liste = [];
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, ID_KADEME3: _idkademe3, DONEM: _donem, OGRENCIDONEM: _ogrencidonem };
            WebPost(this, this.controller, "SinavTuruDersleriListele", p, '', '', function (data, parent) {

                _this.Liste.push({ ID: 0, AD: 'Tümü' })
                $.each(JSON.parse(data)[0].t1, function (j, el) {
                    _this.Liste.push({ ID: el.ID, AD: el.AD });
                });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Admin için Dönem Seçimi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem-secim", {
    //props: ['controller', 'idsinavturu', 'donem', 'idkademe3'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Dönem Seçimi Türü...">
                            <option v-bind:value="false" selected>Yeni Dönem Sınıfları</option>
                            <option v-bind:value="true">Eski Dönem Sınıfları</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: false
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
//----------------------------------------------------------------------------------------------
//Admin için Dönem Seçimi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-alan-multi", {

    props: ['controller', 'yenile'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Alan Seçiniz..." id="sinifAlanMulti">
                           <option v-for="u in Liste" v-bind:value="u.ALAN">{{u.ALAN}}</option>
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

    mounted() {
        this.Yenile();
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf('Tümü') > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ALAN);
                }
                this.$emit('onchange', this.SelectedID);
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('Tümü') == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf('Tümü') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('Tümü') > -1) {
                    this.SelectedID.splice($.inArray('Tümü', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)

            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ALAN);
                }
                this.$emit('onchange', this.SelectedID);
            }

        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SinifAlanListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                parent.Liste.unshift({ ID_SINIFALAN: 0, ALAN: 'Tümü' });
            });
        }
    },

    watch: {
        yenile() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Sınav Puan Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-puan-turu", {
    props: ['controller', 'tc', 'idsinavturu'],
    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbPuanTuru" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVPUANTURU">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, ID_SINAVTURU: this.idsinavturu };
            WebPost(this, this.controller, "PuanTuruListebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
                parent.$nextTick(function () {
                    if (data != null && data.length > 0) {
                        parent.SelectedID = data[0].ID_SINAVPUANTURU;
                        parent.$emit('onchange', parent.SelectedID);
                    }
                });
                try {
                    vue.puanList = data;
                } catch (e) {
                    ListeTemizle(vue.puanList);
                }
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        tc() {
            this.Yenile();
        },
        idsinavturu() {
            this.Yenile();
        }
    },
});





Vue.component("c-sinav-turu-tc-multi", {

    props: ['controller', 'tc', 'donem', 'idkademe3', 'eskidonemsinif', 'ogrencidonem'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz..." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            tumu: false,
            SelectedID: 0,
            Liste: []
        }
    },

    methods: {
        OnChange: function () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAVTURU);
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
                    this.SelectedID.push(this.Liste[i].ID_SINAVTURU);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, DONEM: this.donem, ID_KADEME3: this.idkademe3, ESKIDONEMSINIF: _eskidonemsinif, OGRENCIDONEM: this.ogrencidonem };
            WebPost(this, this.controller, "SinavTuruListeleTc", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINAVTURU: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, AD: el.AD });
                });
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
        eskidonemsinif() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-multi-multitur", {
    props: ['controller', 'idsinavturu', 'donem', 'idkademe3', 'eskidonemsinif', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },



        Yenile() {
            var _donem = this.donem;
            var _ogrencidonem = this.ogrencidonem;
            var _idkademe3 = this.idkademe3;
            var _idsinavturu = JSON.stringify(this.idsinavturu);
            var _eskidonemsinif = this.eskidonemsinif == undefined ? false : this.eskidonemsinif;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURUS: _idsinavturu, DONEM: _donem, ID_KADEME3: _idkademe3, ESKIDONEMSINIF: _eskidonemsinif, OGRENCIDONEM: _ogrencidonem };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                //parent.Liste = $.grep(data, function (n, i) {
                //        return n.DURUM == 2; 
                //});
                parent.Liste = [];
                parent.Liste.push({ ID_SINAV: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    if (el.DURUM == 2) {
                        parent.Liste.push({ ID_SINAV: el.ID_SINAV, AD: el.AD });
                    }
                });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
        eskidonemsinif() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Sınav Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-ders-multi-multitur", {
    props: ['controller', 'idsinavturu', 'idkademe3', 'donem', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID);
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
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },



        Yenile() {
            var _idkademe3 = this.idkademe3;
            var _idsinavturu = JSON.stringify(this.idsinavturu);
            var _ogrencidonem = this.ogrencidonem;
            var _donem = this.donem == undefined || this.donem == null ? '' : this.donem;

            var _this = this;
            _this.Liste = [];
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURUS: _idsinavturu, ID_KADEME3: _idkademe3, DONEM: _donem, OGRENCIDONEM: _ogrencidonem };
            WebPost(this, this.controller, "SinavTuruDersleriListele", p, '', '', function (data, parent) {

                _this.Liste.push({ ID: 0, AD: 'Tümü' })
                $.each(JSON.parse(data)[0].t1, function (j, el) {
                    _this.Liste.push({ ID: el.ID, AD: el.AD });
                });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavturu() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        ogrencidonem() {
            this.Yenile();
        },
    },
});


//----------------------------------------------------------------------------------------------
//Sınav Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-assessment-sinav", {
    props: ['controller', 'donem', 'idkademe3', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ASSESSMENT">{{u.AD}}</option>
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME3: this.idkademe3};
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                parent.Liste.unshift({ ID_ASSESSMENT: 0, 'AD': 'Sınav Seçiniz' });
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        donem() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },
});