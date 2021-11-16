//----------------------------------------------------------------------------------------------
//Yaş Aralığı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih-araligi", {
    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Yaş Aralığı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTYASARALIGI">{{u.YASAY1}} Ay - {{u.YASAY2}} Ay</option>
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
        WebPost(this, this.controller, "YasAraligiListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Tkt Kategoriler Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kategori", {
    props: ['controller', 'idtktyasaraligi'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Kategori </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTKATEGORI">{{u.AD}}</option>
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
        this.Yenile();
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var idtktyasaraligi = this.idtktyasaraligi;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "KategoriListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Tkt Puan Aralığı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-puan-araligi", {
    props: ['controller', 'idtktkategori'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Puan Aralığı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTPUAN" v-bind:data-subtext="u.HARF">{{u.SEVIYE}}</option>
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
            var _idtktkategori = this.idtktkategori;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_TKTKATEGORI: _idtktkategori };
            WebPost(this, this.controller, "PuanAraligiListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idtktkategori() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Upgrade Sınav Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-listesi", {
    props: ['controller'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Etkinlik </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTSINAV">{{u.SINAVAD}}</option>
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
        WebPost(this, this.controller, "UpgradeSinavListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});



//----------------------------------------------------------------------------------------------
//Upgrade Sınav Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-listesi-multi", {
    props: ['controller', 'idtktsinav'],

    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Etkinlik </label>
                    <div class ="col-md-9">
                        <select multiple='multiple' class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title='SEÇİNİZ...'>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTSINAV">{{u.SINAVAD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_TKTSINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_TKTSINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_TKTSINAV: this.idtktsinav };
        WebPost(this, this.controller, "UpgradeSinavListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_TKTSINAV: 0, SINAVAD: 'Tümü' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_TKTSINAV: el.ID_TKTSINAV, SINAVAD: el.SINAVAD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Tarih Seçici Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih", {
    props: ['controller'],

    template: `
                    <div class="form-md-line-input">
                            <label class ="control-label col-md-3">Tarih </label>
                            <div class ="col-md-9">
                                <button id="btn_Tarih" type="button" class="dropdown-toggle btn grey-cararra" data-toggle="dropdown" role="button" title="Seçiniz.." style="width:100%;">
                                    <span id="txt_Tarih" class="filter-option pull-left">Seçiniz..</span>&nbsp;
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
            Tarih: ''
        }
    },

    methods: {

    },

    mounted() {
        $('#btn_Tarih').datepicker({
            format: 'dd/mm/yyyy',
            language: 'tr',
            autoclose: true
        }).on(
            'changeDate', () => {
                var day1 = $("#btn_Tarih").datepicker('getDate').getDate();
                var month1 = $("#btn_Tarih").datepicker('getDate').getMonth() + 1;
                var year1 = $("#btn_Tarih").datepicker('getDate').getFullYear();
                var fullDate = day1 + "/" + month1 + "/" + year1;
                $('#txt_Tarih').html(fullDate);
                this.Tarih = new Date(year1, month1, day1);
                this.$emit('change-date', year1 + '-' + month1 + '-' + day1);
            }
        );

        $('#btn_Tarih').datepicker('setDate', new Date());
        $('#btn_Tarih').datepicker('update');
    },

    updated() {
        $('#btn_Tarih').datepicker('update');
    }
});

Vue.component("c-tarih-id", {
    props: ['tarihid', 'tarih'],

    template: `
                    <div class="form-md-line-input">
                            <div class ="col-md-2">
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
            $('#' + this.tarihid).datepicker('setDate', new Date());
            $('#' + this.tarihid).datepicker('update');
        });
    },

    watch: {
        tarih() {
            $('#' + this.tarihid).datepicker("setDate", strToDate(this.tarih));
        }
    },

    updated() {
        $('#' + this.tarihid).datepicker('update');
    }
});

//----------------------------------------------------------------------------------------------
//TKT Test Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tkttest-listesi", {
    props: ['controller', 'ortalama'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Test </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTTEST">{{u.AD}}</option>
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
        WebPost(this, this.controller, "TKTTestListele", p, '', '', function (data, parent) {
            if (parent.ortalama) {
                data.push({ ID_TKTTEST: 4, AD: 'Ortalamaya Göre' });
            }            
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
Vue.component("c-tkttest-listesi-multi", {
    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Test </label>
                    <div class ="col-md-9">
                        <select multiple='multiple' class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title='SEÇİNİZ...'>
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTTEST">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_TKTTEST);
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
                    this.SelectedID.push(this.Liste[i].ID_TKTTEST);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "TKTTestListele", p, '', '', function (data, parent) {
            parent.Liste = [];
            parent.Liste.push({ ID_TKTTEST: 0, AD: 'Tümü' })
            $.each(data, function (j, el) {
                parent.Liste.push({ ID_TKTTEST: el.ID_TKTTEST, AD: el.AD })
            });
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Tkt Ogrenci Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci", {
    props: ['controller', "id_sube", "id_sinavgrup", "id_sinif", 'id_tkttest'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Öğrenci </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD}}</option>
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
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBE: this.id_sube,
                ID_TKTTEST: this.id_tkttest,
                ID_SINAVGRUP: this.id_sinavgrup,
                ID_SINIF: this.id_sinif
            };
            WebPost(this, this.controller, "TKTOgrenciListele", p, '#filters', 'Yükleniyor..', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        id_tkttest() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-ogrenci-id_subes", {
    props: ['controller', "id_sube", "id_sinavgrup", "id_sinif", 'id_tkttest', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Öğrenci </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title='SEÇİNİZ...' data-live-search="true">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD}}</option>
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
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBES: JSON.stringify(this.id_sube),
                //ID_TKTTESTS: JSON.stringify(this.id_tkttest),
                ID_TKTTEST: this.id_tkttest,
                ID_SINAVGRUPS: JSON.stringify(this.id_sinavgrup),
                ID_SINIFS: JSON.stringify(this.id_sinif),
                DONEM: this.donem
            };
            WebPost(this, this.controller, "TKTOgrenciListele", p, '#filters', 'Yükleniyor..', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        id_tkttest() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller', 'baslik'],

    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" v-if="baslik != undefined">{{baslik}}</label>
                    <label class="control-label col-md-3" v-else>Dönem</label>
                    <div class="col-md-9">
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
//Tkt Donem Componenti
//----------------------------------------------------------------------------------------------
//<select  multiple="true"  class ="selectpicker form-control select2" v-model="SelectedID" @change="OnChange">
Vue.component("c-tkt-donem", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Dönem </label>
                    <div class ="col-md-9">
                        <select id="cmb_donem" multiple="true" class="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTDONEM">{{u.DONEM}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            Liste: []
        }
    },

    methods: {
        OnChange() {
            var Liste = this.Liste;
            var index = this.GetIndex(this.SelectedID, Liste);
            this.SelectedID = [];
            for (var i = index; i < Liste.length; i++) {
                this.SelectedID.push(Liste[i].ID_TKTDONEM);
            }
            this.$emit('onchange', this.SelectedID)
        },

        GetIndex(id, Liste) {
            for (var i = 0; i < Liste.length; i++) {
                if (Liste[i].ID_TKTDONEM == id) {
                    return i;
                }
            }
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "DonemListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Upgrade Grup Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-upgrade-grup", {
    props: ['controller'],
    template: `

                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SATISTURU">{{u.ACIKLAMA}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "GrupListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});


//----------------------------------------------------------------------------------------------
//Tkt Donem Componenti - Tekli
//----------------------------------------------------------------------------------------------
Vue.component("c-donem-tek", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Dönem </label>
                    <div class ="col-md-9">
                        <select id="cmb_donem" class="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_TKTDONEM">{{u.DONEM}}</option>
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "DonemListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});


//----------------------------------------------------------------------------------------------
//Tarih Aralık Detaylı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih-aralik-detayli", {
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3">Tarih </label>
                    <div class="col-md-9">
                        <div id="reportrange" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc; width: 100%;">
                            <i class="fa fa-calendar"></i>&nbsp;
                            <span></span> <i class="fa fa-caret-down"></i>
                        </div>
                    </div>
                </div>
        `,

    mounted() {
        this.$nextTick(function () {
            var x = moment("01-09-" + moment().year() + "", "DD-MM-YYYY") > moment() ? moment("01-09-" + (moment().year() - 1) + "", "DD-MM-YYYY") : moment("01-09-" + moment().year() + "", "DD-MM-YYYY");
            $('#reportrange').daterangepicker({
                "timePicker24Hour": true,
                "opens": "left",
                "applyClass": "btn btn-xs btn-default",
                "cancelClass": "btn btn-xs btn-link",
                ranges: {
                    'Bugün': [moment(), moment()],
                    'Dün': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Son 7 gün': [moment().subtract(6, 'days'), moment()],
                    'Son 30 gün': [moment().subtract(29, 'days'), moment()],
                    'Bu ay': [moment().startOf('month'), moment().endOf('month')],
                    'Bu yıl': [moment().startOf('year'), moment().endOf('year')],
                    'Bu dönem': [x, moment()],
                },
                "locale": {
                    "format": "DD/MM/YYYY",
                    "separator": " - ",
                    "applyLabel": "Uygula",
                    "cancelLabel": "Vazgeç",
                    "fromLabel": "Dan",
                    "toLabel": "a",
                    "customRangeLabel": "Seç",
                    "daysOfWeek": [
                        "Pt",
                        "Sl",
                        "Çr",
                        "Pr",
                        "Cm",
                        "Ct",
                        "Pz"
                    ],
                    "monthNames": [
                        "Ocak",
                        "Şubat",
                        "Mart",
                        "Nisan",
                        "Mayıs",
                        "Haziran",
                        "Temmuz",
                        "Ağustos",
                        "Eylül",
                        "Ekim",
                        "Kasım",
                        "Aralık"
                    ],
                    "firstDay": 1
                }
            }, this.cb);

            this.cb(moment(), moment());
        });
    },

    methods: {
        cb(start, end) {
            $('#reportrange span').html(start.format('DD.MM.YYYY') + ' - ' + end.format('DD.MM.YYYY'));
            vue.START = moment(start).format('YYYY-MM-DD');
            vue.END = moment(end).format('YYYY-MM-DD');
            this.$forceUpdate();
        }
    }
});