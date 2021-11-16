//----------------------------------------------------------------------------------------------
//Abide Sınav Componenti (GENEL DERSLER)
//----------------------------------------------------------------------------------------------
Vue.component("c-unite-tarama-sinav", {
    props: ['controller', 'donem', 'idkademe3', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_UNITETARAMASINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                if (data != null && data != undefined) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = []
                }
            })
        }
    },

    watch: {
        donem() {
            this.Yenile();
        },
        idkademe3() {
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
Vue.component("c-unite-tarama-sinav2", {
    props: ['controller', 'donem', 'idkademe3', 'idset'],
    template: `
                <div class="form-md-line-input">
                <div class="col-md-2">
                   
                    
                        <select class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_UNITETARAMASINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                if (data != null && data != undefined) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = []
                }
            })
        }
    },

    watch: {
        donem() {
            this.Yenile();
        },
        idkademe3() {
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

//----------------------------------------------------------------------------------------------
//Sınıf Dönem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-donem", {

    props: ['controller', 'idsube', 'idkademe3', 'donem', 'idset'],

    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class="col-md-9">
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

Vue.component("c-unite-tarama-sinav-ogrenci", {
    props: ['controller', 'donem', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_UNITETARAMASINAV">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, O_TCKIMLIKNO: this.tc };
            WebPost(this, this.controller, "SinavListelebyOgrenci", p, '', '', function (data, parent) {
                if (data != null && data != undefined) {
                    parent.Liste = JSON.parse(data);
                } else {
                    parent.Liste = []
                }
            })
        }
    },

    watch: {
        donem() {
            this.Yenile();
        },
        tc() {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-unite-tarama-kitapcik", {
    props: ['controller', 'idabidesinav', 'ders', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="idabidekitapcik" @change="OnChange">
                            <option value="0">Seçiniz...</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_UNITETARAMAKITAPCIK">{{u.AD}} Kitapçığı</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            idabidekitapcik: 0,
            Liste: []
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.idabidekitapcik)
        },

        Yenile() {
            if (this.idabidesinav > 0 && this.ders != '' && this.tc != '') {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_UNITETARAMASINAV: this.idabidesinav, DERS: this.ders, O_TCKIMLIKNO: this.tc };
                WebPost(this, this.controller, "KitapcikGetir", p, '', '', function (data, parent) {
                    if (data != null && data != undefined) {
                        parent.Liste = JSON.parse(data);
                        parent.idabidekitapcik = 0;
                        for (var i = 0; i < parent.Liste.length; i++) {
                            if (parent.Liste[i].SECILI) {
                                parent.idabidekitapcik = parent.Liste[i].ID_UNITETARAMAKITAPCIK;
                                parent.$emit('onchange', parent.idabidekitapcik)
                            }
                        }

                        if (parent.Liste.length == 1) {
                            parent.idabidekitapcik = parent.Liste[0].ID_UNITETARAMAKITAPCIK;
                            parent.$emit('onchange', parent.idabidekitapcik)
                        }
                    } else {
                        parent.Liste = []
                    }
                })
            }
        }
    },

    watch: {
        ders: function (yeni, eski) {
            this.Yenile();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
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
                tomorrow.setDate(today.getDate() + parseInt(this.gun));

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

Vue.component("c-sinav-multi", {
    props: ['controller', 'donem', 'idkademe3', 'idset'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_UNITETARAMASINAV">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_UNITETARAMASINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_UNITETARAMASINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {           
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {              
                parent.Liste = [];
                parent.Liste.push({ ID_UNITETARAMASINAV: 0, AD: 'Tüm Sınavlar' })
                $.each(JSON.parse(data), function (j, el) {                    
                   parent.Liste.push({ ID_UNITETARAMASINAV: el.ID_UNITETARAMASINAV, AD: el.AD })
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
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        }
    },
});