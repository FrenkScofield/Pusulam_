Vue.component("c-odev-tur", {
    props: ['controller', 'idset', 'idkademe'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ödev Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_ODEVTUR">{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME: this.idkademe };
            WebPost(this, this.controller, "OdevTurListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            });
        }
    },

    mounted() {
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },

        idkademe() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-tarih", {
    props: ['tarihid', 'label', 'idset', 'gun'],

    template: `
                     <div class ="form-md-line-input">
                        <label class ="control-label col-md-3" style="vertical-align:middle;">{{label}} </label>
                        <div class ="col-md-9">
                                <button :id="tarihid" type="button" class="dropdown-toggle btn grey-cararra" data-toggle="dropdown" role="button" title="Seçiniz.." style="width:100%;">
                                    <span :id='"txt_"+tarihid' class="filter-option pull-left">Seçiniz..</span>&nbsp;
                                    <span class="bs-caret" style="float:right;">
                                        <span class="caret"></span>
                                    </span>
                                </button>
                            </div>
                        </div>
        `
    ,

    data: function () {
        return {
            tarih: '',
        }
    },

    methods: {

    },

    mounted() {
        this.tarihid = this.tarihid == undefined ? 'btn_Tarih' : this.tarihid;

        $('#' + this.tarihid).datepicker({
            format: 'dd/mm/yyyy',
            language: 'tr',
            autoclose: true
        }).on(
            'changeDate', () => {
                var day1 = $('#' + this.tarihid).datepicker('getDate').getDate();
                var month1 = $('#' + this.tarihid).datepicker('getDate').getMonth() + 1;
                var year1 = $('#' + this.tarihid).datepicker('getDate').getFullYear();
                var fullDate = day1 + "." + month1 + "." + year1;
                $('#txt_' + this.tarihid).html(fullDate);
                this.tarih = new Date(year1, month1, day1);
                this.$emit('change-date', year1 + '-' + month1 + '-' + day1);
            }
        );

        this.$nextTick(function () {
            if (this.idset == undefined) {
                var today = new Date();
                var tomorrow = new Date();
                tomorrow.setDate(today.getDate() + parseInt(this.gun == null || this.gun == undefined ? 0 : this.gun ));

                $('#' + this.tarihid).datepicker('setDate', tomorrow);
                $('#' + this.tarihid).datepicker('update');
            }
        });
    },

    watch: {
        idset() {
            $('#' + this.tarihid).datepicker("setDate", strToDate(this.idset));
        }
    },

    updated() {
        $('#' + this.tarihid).datepicker('update');
    }
});

Vue.component("c-tarih-turkish", { //104 formatlı
    props: ['tarihid', 'label', 'idset', 'gun'],

    template: `
                     <div class ="form-md-line-input">
                        <label class ="control-label col-md-3" style="vertical-align:middle;">{{label}} </label>
                        <div class ="col-md-9">
                                <button :id="tarihid" type="button" class="dropdown-toggle btn grey-cararra" data-toggle="dropdown" role="button" title="Seçiniz.." style="width:100%;">
                                    <span :id='"txt_"+tarihid' class="filter-option pull-left">Seçiniz..</span>&nbsp;
                                    <span class="bs-caret" style="float:right;">
                                        <span class="caret"></span>
                                    </span>
                                </button>
                            </div>
                        </div>
        `
    ,

    data: function () {
        return {
            tarih: '',
        }
    },

    methods: {},

    mounted() {
        this.tarihid = this.tarihid == undefined ? 'btn_Tarih' : this.tarihid;

        $('#' + this.tarihid).datepicker({
            format: 'dd/mm/yyyy',
            language: 'tr',
            autoclose: true
        }).on(
            'changeDate', () => {
                var day1 = $('#' + this.tarihid).datepicker('getDate').getDate();
                var month1 = $('#' + this.tarihid).datepicker('getDate').getMonth() + 1;
                var year1 = $('#' + this.tarihid).datepicker('getDate').getFullYear();
                var fullDate = day1 + "." + month1 + "." + year1;

                $('#txt_' + this.tarihid).html(fullDate);
                this.tarih = new Date(year1, month1, day1);

                this.$emit('change-date', year1 + '-' + month1 + '-' + day1);
                this.$emit('input', year1 + '-' + month1 + '-' + day1);

            }
        );

        this.$nextTick(function () {
            if (this.idset == undefined) {
                var today = new Date();
                var tomorrow = new Date();
                tomorrow.setDate(today.getDate() + parseInt(this.gun == undefined ? 0 : this.gun));

                $('#' + this.tarihid).datepicker('setDate', tomorrow);
                $('#' + this.tarihid).datepicker('update');
            }
            else {
                $('#' + this.tarihid).datepicker("setDate", strToDateTurkish(this.idset));
            }
        });
    },

    watch: {
        idset() {
            console.log(this.idset);
            $('#' + this.tarihid).datepicker("setDate", strToDateTurkish(this.idset));
        }
    },

    updated() {
        $('#' + this.tarihid).datepicker('update');
    }
});

Vue.component("c-saat", {
    props: ['id', 'label', 'idset'],

    template: `
                    <div class ="form-md-line-input">
                        <label class="control-label col-md-3">{{label}}</label>
                        <div class="col-md-9">
                            <input type="text" class="form-control" :id="id" v-model="saat">
                        </div>
                    </div>
        `
    ,

    data: function () {
        return {
            saat: '',
        }
    },

    methods: {

    },

    mounted() {
        var parent = this;
        $('#' + this.id).timepicker({ showMeridian: false });

        $('#' + this.id).timepicker().on('changeTime.timepicker', function (e) {
            parent.$emit('change-time', e.time.value);
            parent.$emit('input', e.time.value);

        });

        this.$nextTick(function () {
            if (parent.idset != undefined) {
                $('#' + parent.id).timepicker('setTime', parent.idset);
            }
        });

    },

    watch: {
        idset() {
            $('#' + this.id).timepicker('setTime', this.idset);
        }
    }
});

Vue.component("c-morpa-ders", {
    props: ['controller', 'idset', 'idkademe3'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Morpa Ders </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_MORPADERS">{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "MorpaDersListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            });
        }
    },

    mounted() {
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
        idkademe3() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-sinif-odev-veren", {
    props: ['controller', 'idset', 'idsinif'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ödev Veren </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: '',
            Liste: []
        }
    },
    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: this.idsinif };
            WebPost(this, this.controller, "OdevVerenListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            });
        }
    },

    mounted() {
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },

        idsinif() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("c-ogrenci-veli", {
    props: ['controller'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğrenci </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Öğrenci Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD + ' '+u.SOYAD}}</option>
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
    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };

        WebPost(this, this.controller, "OgrenciListelebyVeli", p, '', '', function (data, parent) {
            parent.Liste = data;
            parent.$nextTick(function () {
                if (parent.Liste.length == 1) {
                    parent.SelectedID = data[0].TCKIMLIKNO;
                    this.$emit('onchange', parent.SelectedID)
                }
            });
        })
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
    },


    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-ogrenci-odev-ders", {
    props: ['controller', 'idset', 'tcogrenci', 'donem'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            if (this.donem != '' && this.tcogrenci != '') {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tcogrenci, DONEM: this.donem };
                WebPost(this, this.controller, "OgrenciOdevDersListele", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                });
            }
        }
    },

    mounted() {
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
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

//----------------------------------------------------------------------------------------------
//Öğretmen Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogretmen", {

    props: ['controller', 'idset', 'idsubelist', 'idkademe3list', 'idderslist', 'idsube', 'idkademe', 'idders', 'donem'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğretmen </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." data-live-search="true">
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
            sonArama: undefined,
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
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_DERSLIST: JSON.stringify(this.idderslist),
                ID_SUBE: this.idsube,
                ID_KADEME3: this.idkademe3,
                ID_DERS: this.idders,
                DONEM: this.donem
            };

            if ((this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idderslist != undefined && this.idderslist.length == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
                || (this.idders != undefined && this.idders == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "OgretmenListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].TCKIMLIKNO;
                        parent.OnChange();
                    }
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
        idsubelist() {
            this.GetData();
        },
        idkademe3list() {
            this.GetData();
        },
        idderslist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe3() {
            this.GetData();
        },
        idders() {
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
Vue.component("c-ogretmen-multi", {
    props: ['controller', 'idset', 'idsubelist', 'idkademe3list', 'idderslist', 'idsube', 'idkademe3', 'idders', 'disabled', 'donem'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Öğretmen </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" :style="'font-weight:'+[u.TCKIMLIKNO == 0 ? 'bold' : '']">{{u.ADSOYAD}}</option>
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
            ilkYukleme: true,
            sonArama: undefined,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
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
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_DERSLIST: JSON.stringify(this.idderslist),
                ID_SUBE: this.idsube,
                ID_KADEME3: this.idkademe3,
                ID_DERS: this.idders,
                DONEM: this.donem
            };

            if ((this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idderslist != undefined && this.idderslist.length == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
                || (this.idders != undefined && this.idders == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "OgretmenListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ TCKIMLIKNO: 0, ADSOYAD: 'Tümünü Seç' });
                    }
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].TCKIMLIKNO;
                        parent.OnChange();
                    }

                    try {
                        if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                        parent.ilkYukleme = false;
                    } catch (e) { }
                }
            })
        }
    },

    mounted() {
        this.GetData();
    },

    watch: {
        idsubelist() {
            this.GetData();
        },
        idkademe3list() {
            this.GetData();
        },
        idderslist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe3() {
            this.GetData();
        },
        idders() {
            this.GetData();
        },
        donem() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ders", {
    props: ['controller', 'idset', 'idsube', 'idkademe3', 'idsubelist', 'idkademe3list', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
            sonArama: undefined
        }
    },
    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_SUBE: this.idsube,
                ID_KADEME3: this.idkademe3
            };

            if ((this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "DersListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            });
        }
    },

    mounted() {
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },

        idkademe3() {
            this.GetData();
        },

        idsube() {
            this.GetData();
        },

        idkademe3list() {
            this.GetData();
        },

        idsubelist() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("c-ders-multi", {
    props: ['controller', 'idset', 'idsube', 'idkademe3', 'idsubelist', 'idkademe3list', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Ders </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
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
            ilkYukleme: true,
            sonArama: undefined,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_DERS);
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
                    this.SelectedID.push(this.Liste[i].ID_DERS);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_SUBE: this.idsube,
                ID_KADEME3: this.idkademe3
            };

            if ((this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "DersListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_DERS: 0, AD: 'Tümünü Seç' });
                    }
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_DERS;
                        parent.OnChange();
                    }

                    try {
                        if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                        parent.ilkYukleme = false;
                    } catch (e) { }
                }
            })
        }
    },

    mounted() {
        this.GetData();
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },

        idkademe3() {
            this.GetData();
        },

        idsube() {
            this.GetData();
        },

        idkademe3list() {
            this.GetData();
        },

        idsubelist() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-morpa-materyal-multi", {
    props: ['controller', 'idset', 'idmorpaders', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Morpa Materyal </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_MORPAMATERYAL">{{u.AD}}</option>
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
            ilkYukleme: true,
            sonArama: undefined,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_MORPAMATERYAL);
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
                    this.SelectedID.push(this.Liste[i].ID_MORPAMATERYAL);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_MORPADERS: this.idmorpaders,
                PASIFGORUNSUN: false
            };

            if ((this.idmorpaders != undefined && this.idmorpaders.length == 0)) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "MorpaMateryalListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_MORPAMATERYAL: 0, AD: 'Tümünü Seç' });
                    }
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_MORPAMATERYAL;
                        parent.OnChange();
                    }

                    try {
                        if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                        parent.ilkYukleme = false;
                    } catch (e) { }
                }
            })
        }
    },

    mounted() {
        this.GetData();
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.OnChange();
        },

        idmorpaders() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-morpa-kazanim-multi", { // mete hocanın isteğiyle tekli seçim oldu.
    props: ['controller', 'idset', 'idmorpaders', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Morpa Kazanım </label>
                   <div class ="col-md-9">
                        <select class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_MORPAKAZANIM" v-bind:data-subtext="u.KOD">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 0,//[],
            Liste: [],
            tumu: false,
            ilkYukleme: true,
            sonArama: undefined,
        }
    },

    methods: {
        OnChange() {
            var arr = [];
            arr.push(this.SelectedID);
            this.$emit('onchange', arr);
            //if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
            //    this.tumu = true;
            //    ListeTemizle(this.SelectedID);
            //    for (i = 0; i < this.Liste.length; i++) {
            //        this.SelectedID.push(this.Liste[i].ID_MORPAKAZANIM);
            //    }
            //    this.$emit('onchange', this.SelectedID)
            //    return;
            //}

            //if (this.tumu == true && this.SelectedID.indexOf(0) == -1) { // tümü iptal
            //    this.tumu = false;
            //    ListeTemizle(this.SelectedID);
            //}
            //if (this.tumu == true && this.SelectedID.indexOf(0) > -1) {
            //    this.tumu = false;
            //    if (this.SelectedID.indexOf(0) > -1) {
            //        this.SelectedID.splice($.inArray(0, this.SelectedID), 1);
            //    }
            //}
            //this.$emit('onchange', this.SelectedID)
            //if (this.Liste.length - this.SelectedID.length == 1) {
            //    this.tumu = true;
            //    ListeTemizle(this.SelectedID);
            //    for (i = 0; i < this.Liste.length; i++) {
            //        this.SelectedID.push(this.Liste[i].ID_MORPAKAZANIM);
            //    }
            //    this.$emit('onchange', this.SelectedID);
            //}
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_MORPADERS: this.idmorpaders,
                PASIFGORUNSUN: false
            };

            if ((this.idmorpaders != undefined && this.idmorpaders.length == 0)) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "MorpaKazanimListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    //if (data != "[]") {
                    //    parent.Liste.unshift({ ID_MORPAKAZANIM: 0, AD: 'Tümünü Seç' });
                    //}
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_MORPAKAZANIM;
                        parent.OnChange();
                    }

                    try {
                        if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset[0]));
                            parent.OnChange();
                        }
                        parent.ilkYukleme = false;
                    } catch (e) {
                    }
                }
            })
        }
    },

    mounted() {
        this.GetData();
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0/*[]*/ : this.idset;
            this.OnChange();
        },

        idmorpaders() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-brans-multi", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Branş </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.BRANS">{{u.BRANS}}</option>
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
            ilkYukleme: true,
            sonArama: undefined,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf('Tümünü Seç') > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].BRANS);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf('Tümünü Seç') == -1) { // tümü iptal
                this.tumu = false;
                ListeTemizle(this.SelectedID);
            }
            if (this.tumu == true && this.SelectedID.indexOf('Tümünü Seç') > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf('Tümünü Seç') > -1) {
                    this.SelectedID.splice($.inArray('Tümünü Seç', this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].BRANS);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            //if (this.sonArama == JSON.stringify(p)) {
            //    return;
            //}
            //this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "BransListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ BRANS: 'Tümünü Seç' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].BRANS;
                        parent.OnChange();
                    }

                    try {
                        if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                        parent.ilkYukleme = false;
                    } catch (e) { }
                }
            });
        },
    },

    mounted() {
        this.GetData();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-sinav-turu", {
    props: ['controller', 'idkademe3', 'donem', 'idset', 'sinavdurum'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: -1,
            Liste: [],
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            ListeTemizle(this.Liste);
            if (this.idkademe3 > 0) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, DONEM: this.donem, SINAVDURUM: this.sinavdurum };
                WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINAVTURU;
                        parent.OnChange();
                    }
                })
            }
        },
    },

    mounted() {
        this.Yenile();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-sinav", {
    props: ['controller', 'idkademe3', 'donem', 'idsinavturu', 'onlinesinav', 'idset'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: -1,
            Liste: [],
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            ListeTemizle(this.Liste);
            if (this.idkademe3 > 0 && this.idsinavturu > 0 && this.donem > 0) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, DONEM: this.donem, ID_SINAVTURU: this.idsinavturu, ONLINESINAV: this.onlinesinav };
                WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINAV;
                        parent.OnChange();
                    }
                })
            }
        },
    },

    mounted() {
        this.Yenile();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idsinavturu() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-ders-multi2", {
    props: ['controller', 'idsinavturu', 'idkademe3', 'donem', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                 <label class ="control-label col-md-3" style="vertical-align:middle;">Ders </label>
                    <div class="col-md-9">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURU: _idsinavturu, ID_KADEME3: _idkademe3, DONEM: _donem, OGRENCIDONEM: _ogrencidonem };
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












Vue.component("c-sinav-ogrencilist", {

    props: ['controller', 'idset',  'idsinav'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğrenci </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true"  title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.ADSOYAD}}</option>
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

            if ((this.idSinav != undefined && this.idSinav == 0) 
            ) {
                return;
            }
  
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAV: JSON.stringify(this.idsinav)};
            WebPost(this, this.controller, "SinavDisOgrenciListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].TCKIMLIKNO;
                        parent.OnChange();
                    }
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
        idsinav() {

            this.GetData();
        },
     
  
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
 