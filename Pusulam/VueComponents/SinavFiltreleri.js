//----------------------------------------------------------------------------------------------
//Sınav Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavturu", {
    props: ['controller', 'id_sinavturu'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="id_sinavturu" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" v-bind:data-subtext="u.KISAAD">{{u.AD}}</option>
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

    methods: {
        OnChange() {
            this.$emit('onchange', this.id_sinavturu)
        }
    },

    mounted() {
        var idtktyasaraligi = this.idtktyasaraligi;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Sınav Tercihleri Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavtercihleri", {
    props: ['controller', 'listidsinavtercihi'],
    template: `
                
                     <div class ="form-md-line-input">
                        <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Tercihleri </label>
                        <div class ="col-md-9">
                            <select class ="mt-multiselect btn btn-default text-left" v-model='listidsinavtercihi' multiple="multiple" data-label="left" data-width="100%" data-filter="true" data-action-  onchange="true" style="100%!important;">                           
                                <option value="1">Özel</option>
                                <option value="2">Frekans Hesapla</option>
                                <option value="3">İnteraktif Puanı Gizle</option>
                                <option value="4">Yükleme Kapat</option>
                                <option value="5">Cevap Anahtarı Yükleme Kapat</option>
                                <option value="6">Online Sınav</option>
                                <option value="7">Sıralamayı Gizle</option>
                            </select>
                        </div>
                    </div>
               
        `
    ,
    methods: {
        OnChange() {
            listidsinavtercihi = $('.mt-multiselect').val();
            this.$emit('onchange', listidsinavtercihi);
        }
    },

    mounted() {
        var parent = this;
        $('.mt-multiselect').multiselect({
            nonSelectedText: 'Seçiniz..',
            buttonWidth: '100%',
            allSelectedText: false,
            nSelectedText: 'Seçili',
            onChange: function (element, checked) {
                parent.OnChange();
            },
            buttonClass: 'dropdown-toggle btn grey-cararra'
        });
        $('.mt-multiselect').val(this.listidsinavtercihi);
        $('.mt-multiselect').multiselect("refresh");
    },

    updated() {
        $('.mt-multiselect').multiselect("refresh");
    }
});
//----------------------------------------------------------------------------------------------
//Sınav Grup Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinavgrup", {
    props: ['controller', 'id_kademe3'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedId" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            Liste: [],
            SelectedId: 0,
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedId)
        }
    },

    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "SinavGrupListele", p, '', '', function (data, parent) {
            parent.Liste = data;

            parent.SelectedId = 0;

            try {
                parent.SelectedId = parent.id_kademe3 != undefined ? parent.id_kademe3 : 0;
                parent.OnChange();
            } catch (e) {
            }
        })
    },
    watch: {
        id_kademe3() {
            this.SelectedId = this.id_kademe3 != undefined ? this.id_kademe3 : 0;
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Tarih Seçici Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih", {
    props: ['controller', 'tarih'],

    template: `
                    <div class ="form-md-line-input">
                            <label class="control-label col-md-3">Tarih </label>
                            <div class ="col-md-9">
                                <div class ="dropdown-toggle btn grey-cararra" data-toggle="dropdown" role="button" title="Seçiniz.." style="width:100%; padding:0px;">
                                    <input type="text" id="btn_Tarih" style="cursor:pointer; text-align:left; width:100%; background:transparent; border:none; padding-left:14px; padding-right:14px; padding-top:8px; padding-bottom:7px;" v-model="tarih" readonly="true" placeholder="Seçiniz..">
                                    <span class="bs-caret" style="position:absolute; right:12px; top:5px;">
                                        <span class ="caret"></span>
                                    </span>
                                </div>
                            </div>
                        </div>
        `
    ,

    methods: {

    },

    mounted() {
        $('#btn_Tarih').datepicker({
            format: 'dd/mm/yyyy',
            language: 'tr'
        }).on(
            'changeDate', () => {
                var day1 = $("#btn_Tarih").datepicker('getDate').getDate();
                var month1 = $("#btn_Tarih").datepicker('getDate').getMonth() + 1;
                var year1 = $("#btn_Tarih").datepicker('getDate').getFullYear();
                var fullDate = day1 + "/" + month1 + "/" + year1;
                $('#txt_Tarih').html(fullDate);
                this.Tarih = new Date(year1, month1, day1);
                this.tarih = day1 + '/' + month1 + '/' + year1;
                this.$emit('change-date', this.tarih);
            }
        );
    },

    updated() {
    }
});
//----------------------------------------------------------------------------------------------
//Yanlış Sayısı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-yanlissayisi", {
    props: ['controller', 'yanlissayisi'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Yanlış Sayısı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="yanlissayisi" @change="OnChange">
                            <option value="0">0</option>
                            <option v-for="u in 10" v-bind:value="u">{{u}}</option>
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

    methods: {
        OnChange() {
            this.$emit('onchange', this.yanlissayisi)
        }
    },

    mounted() {
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Kitapçık Sayısı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kitapciksayisi", {
    props: ['controller', 'kitapciksayisi'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Kitapçık Sayısı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="kitapciksayisi" @change="OnChange">
                            <option v-for="u in maxKitapcik" v-bind:value="u">{{u}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            Liste: [],
            maxKitapcik: 5
        }
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.kitapciksayisi)
        }
    },

    mounted() {
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Dönem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" :disabled="disabled">
                            <option value="0">Seçiniz..</option>
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

Vue.component("c-donem-yan", {

    props: ['controller'],
    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-2">
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
//Sınav Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav", {
    props: ['controller', 'id_kademe3', 'id_sinavturu', 'donem', 'idsinav'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
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
            if (this.SelectedID == undefined && this.idsinav > 0)
                this.SelectedID = this.idsinav;
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.id_kademe3, ID_SINAVTURU: this.id_sinavturu, DONEM: this.donem };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            });
        }
    },

    mounted() {

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe3() {
            this.Yenile();
        },
        id_sinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idsinav() {
            this.SelectedID = this.idsinav;
            this.$emit('onchange', this.idsinav);
        },
    },
});
Vue.component("c-sinav-donem", {
    props: ['controller', 'id_kademe3', 'id_sinavturu', 'donem', 'idsinav','cevapanahtariduzenle'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
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
            if (this.SelectedID == undefined && this.idsinav > 0)
                this.SelectedID = this.idsinav;
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            var _this = this;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.id_kademe3, ID_SINAVTURU: this.id_sinavturu, DONEM: this.donem };
            WebPost(this, this.controller, "SinavListeleKademeDonem", p, '', '', function (data, parent) {
                parent.Liste = data;
                if (_this.cevapanahtariduzenle == true) {
                    parent.Liste = $.grep(parent.Liste, (el) => {
                        return el.ONLINESINAV == false;
                    })
                }
            });
        }
    },

    mounted() {

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe3() {
            this.Yenile();
        },
        id_sinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idsinav() {
            this.SelectedID = this.idsinav;
            this.$emit('onchange', this.idsinav);
        },
    },
});
Vue.component("c-sinav-bursluluk-multi", {
    props: ['controller', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAV">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', $.grep(this.SelectedID, function (el) { return el != 0; }));
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
            this.$emit('onchange', $.grep(this.SelectedID, function (el) { return el != 0; }));

            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', $.grep(this.SelectedID, function (el) { return el != 0; }));
            }

        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
            WebPost(this, this.controller, "SinavListeleBursluluk", p, '', '', function (data, parent) {
                vue.TUMSINAVLIST = JSON.parse(data);
                parent.Liste = JSON.parse(data);
                parent.Liste.unshift({ AD: "Tümü", ID_SINAV: 0, ID_KADEME3: 0 })
            });
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe3() {
            this.Yenile();
        },
        id_sinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idsinav() {
            this.SelectedID = this.idsinav;
            this.$emit('onchange', this.idsinav);
        },
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c_ders", {
    props: ['controller', 'id_kademe3'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders</label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        DersListesi() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.id_kademe3 };
            WebPost(this, this.controller, "KademeDersGetir", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        //Sayfa ilk açıldığında

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe3() {
            this.DersListesi();
        },
    },
});
Vue.component("c-ders-yan", {
    props: ['controller', 'idkademe3'],
    template: `
                <div class="form-md-line-input col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
                            <option value="0">Seçiniz..</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        DersListesi() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "KademeDersGetir", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.DersListesi();
        //Sayfa ilk açıldığında

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idkademe3() {
            this.DersListesi();
        },
    },
});
Vue.component("c-ders-st", {
    props: ['controller', 'idsinavturu', 'idkademe3', 'idsiniflist', 'donem', 'ogrencidonem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="true" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Ders Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID">{{u.AD}}</option>
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
            var _this = this;
            _this.Liste = [];
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURU: _idsinavturu, ID_KADEME3: _idkademe3, ID_SINIFs: JSON.stringify(this.idsiniflist), DONEM: this.donem, OGRENCIDONEM: this.ogrencidonem };
            WebPost(this, this.controller, "SinavTuruDersleriListele", p, '', '', function (data, parent) {
                //_this.Liste = JSON.parse(data)[0].t1;
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
        idsiniflist() {
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
//Sınav Puan Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-puan-turu", {
    props: ['controller', 'tc', 'idsinav'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Puan Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbPuanTuru">
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, ID_SINAV: this.idsinav };
            WebPost(this, this.controller, "PuanTuruListebyOgrenci", p, '', '', function (data, parent) {
                parent.Liste = data;
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
        idsinav() {
            this.Yenile();
        }
    },
});

//----------------------------------------------------------------------------------------------
//Sınav Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-ders", {
    props: ['dersler', 'ilksecenek', 'sifirla'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler" :title="this.ilksecenek" >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVDERS">{{u.DERSAD}}</option>
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

    },

    mounted() {
        //    this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        dersler() {
            this.Liste = this.dersler;
        },
        sifirla() {
            this.Liste = [];
            this.SelectedID = 0;
            this.OnChange();
        },
    },
});
Vue.component("c-sinav-ders2", {
    props: ['dersler', 'ilksecenek', 'sifirla'],
    template: `
                <div class="form-md-line-input">
                        <label class ="control-label col-md-3" style="vertical-align:middle;">Ders</label>
                        <div class ="col-md-9">
                            <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler" :title="this.ilksecenek" >
                                <option v-for="u in Liste" v-bind:value="u.ID_SINAVDERS">{{u.DERSAD}}</option>
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

    },

    mounted() {
        //    this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        dersler() {
            this.Liste = this.dersler;
        },
        sifirla() {
            this.Liste = [];
            this.SelectedID = 0;
            this.OnChange();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Oturum Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-oturum", {
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Oturum </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler">
                            <option value="1">1. Oturum</option>
                            <option value="2">2. Oturum</option>
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
//----------------------------------------------------------------------------------------------
//Sınav Ders Soru Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-ders-soru", {
    props: ['controller', 'idsinavders'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Soru Seçiniz">
                            <option v-for="u in Liste" v-bind:value="u.SORUNO_A">{{u.SORUNO_A}}</option>
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
            if (this.idsinavders == 0 || this.idsinavders == undefined) {
                this.Liste = [];
                return;
            }
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVDERS: this.idsinavders, SINAVSORUISLEM: true };
            WebPost(this, this.controller, "SinavDersSorulariListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].t1;
            });
        }
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsinavders() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Soru İşlem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-islem", {

    props: ['controller'],
    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="İşlem Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SORUISLEM">{{u.ACIKLAMA}}</option>
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
        WebPost(this, this.controller, "SoruIslemListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data)[0].t1;
        })
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//CHECKBOX Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-checkbox", {
    props: ['degisken'],
    template: `
        <div >
            <div v-for="u in Liste">
                <label class ="control-label col-md-9" style="vertical-align:middle;">{{u.AD}}</label>
                <div class ="col-md-3" style="height:34px; vertical-align:middle;">
                    <div class ="md-checkbox" style="margin:auto!important; width:20px!important; height:20px; margin:0px; margin-top:6px; display:block;" >
                        <input type="checkbox" v-model="u.SELECTED" :id="u.VALUE" class ="md-check" v-bind:true-value="true" v-bind:false-value="false" @change="OnChange"  />
                        <label :for="u.VALUE">
                            <span></span>
                            <span class ="check"></span>
                            <span class ="box"></span>
                        </label>
                    </div>
                </div>
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
    mounted() {
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.Liste)
        },
        Yenile() {
            this.Liste = this.degisken;
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        degisken() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu-multi", {

    props: ['controller', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz.." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
            idkademe3: 0,
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAVTURU);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
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
            if (this.idkademe3 == undefined) {
                this.idkademe3 = 0;
            }

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3) };
            WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {

                parent.Liste = [];
                parent.Liste.push({ ID_SINAVTURU: -1, AD: 'Tümü' });

                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, AD: el.AD })
                });

            })
        },
    },

    mounted() {
        this.Yenile();
    },


    watch: {
        idkademe3() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-sinav-turu-yazili", {

    props: ['controller', 'idkademe3', 'donem', 'ogrencidonem'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control"  v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz.." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
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
            //if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
            //    this.tumu = true;
            //    this.SelectedID = [];
            //    for (i = 0; i < this.Liste.length; i++) {
            //        this.SelectedID.push(this.Liste[i].ID_SINAVTURU);
            //    }
            //    this.$emit('onchange', this.SelectedID)
            //    return;
            //}

            //if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
            //    this.tumu = false;
            //    this.SelectedID = [];
            //}
            //if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
            //    this.tumu = false;
            //    if (this.SelectedID.indexOf(-1) > -1) {
            //        this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
            //    }
            //}
            this.$emit('onchange', this.SelectedID)
            //if (this.Liste.length - this.SelectedID.length == 1) {
            //    this.tumu = true;
            //    this.SelectedID = [];
            //    for (i = 0; i < this.Liste.length; i++) {
            //        this.SelectedID.push(this.Liste[i].ID_SINAVTURU);
            //    }
            //    this.$emit('onchange', this.SelectedID);
            //}
        },
        Yenile() {
            if (this.idkademe3 == undefined) {
                this.idkademe3 = 0;
            }
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, DONEM: this.donem, OGRENCIDONEM: this.ogrencidonem };
            WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {

                parent.Liste = data;
                //parent.Liste = [];
                ////parent.Liste.push({ ID_SINAVTURU: -1, AD: 'Tümü' });

                //$.each(data, function (j, el) {
                //    parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, AD: el.AD })
                //});

            })
        },
    },

    mounted() {
        this.Yenile();
    },


    watch: {
        idkademe3() {
            this.Yenile();
        },
        ogrencidonem() {
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
//----------------------------------------------------------------------------------------------
//Frekans Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-frekans-ders", {
    props: ['controller', 'idkademe3', 'idsinavturu'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz..." id="frekans-ders">
                           <option v-for="u in Liste" v-bind:value="u.AD">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
            return {
                SelectedID: 0,
                Liste: []
            }
        },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID);
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3), ID_SINAVTURU: JSON.stringify(this.idsinavturu) };

            WebPost(this, this.controller, "FrekansDersListele", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_DERS: 0, AD: 'Tümü' })
                $.each(JSON.parse(data)[0].t5, function (j, el) {
                    parent.Liste.push({ ID_DERS: el.ID_SINIF, AD: el.AD })
                });
            })
        }
    },

    watch: {
        idkademe3() {
            this.Yenile();
        },
        idsinavturu() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("c-frekans-ders-multi", {
    props: ['controller', 'idkademe3', 'idsinavturu'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true"  v-model="SelectedID" @change="OnChange" title="Ders Seçiniz..." id="frekans-ders">
                           <option v-for="u in Liste" v-bind:value="u.AD">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
            return {
                SelectedID: 0,
                Liste: [],
                tumu: false,
            }
        },

    methods: {

        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf("Tümü") > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].AD);
                }
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf("Tümü") == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf("Tümü") > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf("Tümü") > -1) {
                    this.SelectedID.splice($.inArray("Tümü", this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].AD);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3), ID_SINAVTURU: JSON.stringify(this.idsinavturu) };

            WebPost(this, this.controller, "FrekansDersListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].t5;
                parent.Liste.unshift({ ID_DERS: 0, AD: 'Tümü' });
                //parent.Liste = [];
                //parent.Liste.push({ ID_DERS: 0, AD: 'Tümü' })
                //$.each(JSON.parse(data)[0].t5, function (j, el) {
                //    parent.Liste.push({ ID_DERS: el.ID_SINIF, AD: el.AD })
                //});
            })
        }
    },

    watch: {
        idkademe3() {
            this.Yenile();
        },
        idsinavturu() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Kampüs Branş Öğretmen Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-ogretmen", {
    props: ['controller', 'idsube', 'idders'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Öğretmen Seçiniz...">
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
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            console.log("sube ogr")
            console.log(this.idsube)
            console.log(this.idders)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube, ID_DERS: this.idders };
            WebPost(this, this.controller, "SubeOgretmenListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].TblListeOgretmen;;
            });
        }
    },

    mounted() {

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idders() {
            this.Yenile();
        },
        idsube() {
            this.Yenile();
        },
    },
});
Vue.component("c-sube-ogretmen-yan", {
    props: ['controller', 'idsube', 'idders'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Öğretmen Seçiniz..."  data-live-search="true">
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
            var _this = this;
            vue.ogretmenAdSoyad = $.grep(this.Liste, function (el, j) {
                return el.TCKIMLIKNO == _this.SelectedID
            })[0].ADSOYAD;
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBE: this.idsube, ID_DERS: this.idders };
            WebPost(this, this.controller, "SubeOgretmenListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data)[0].TblListeOgretmen;;
            });
        }
    },

    mounted() {

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idders() {
            this.Yenile();
        },
        idsube() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Gelişim Filtre Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-gelisim-multi", {

    props: ['controller', 'tc', 'donem'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" id="cmbFiltre" title="Filtre Türü Seçiniz.." >
                            <optgroup v-for="list in Liste">
                                <option v-for="u in list" v-bind:value="u.AD" v-bind:deger="u.DEGER" >{{u.AD.replace(/D-/g, '').replace(/P-/g, '') }}</option>
                            </optgroup>
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
            donem: ''
        }
    },

    methods: {
        OnChange() {
            var _this = this;
            var result = this.Liste.DERS //$.grep(this.Liste, function (n, i) { return n.ID.includes("D-") });
            if (this.tumu == false && this.SelectedID.indexOf("TÜMÜ") > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                $.each(result, function (j, el) {
                    _this.SelectedID.push(el.AD.toString());
                });
                this.$emit('onchange', this.SelectedID)
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf("TÜMÜ") == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf("TÜMÜ") > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf("TÜMÜ") > -1) {
                    this.SelectedID.splice($.inArray("TÜMÜ", this.SelectedID), 1);
                }
            }
            this.$emit('onchange', this.SelectedID)

            if (result.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                $.each(result, function (j, el) {
                    _this.SelectedID.push(el.AD.toString());
                });
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            console.log(this.idkademe3)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, DONEM: this.donem };
            WebPost(this, this.controller, "GelisimFiltreList", p, '', '', function (data, parent) {

                parent.Liste = _.groupBy(JSON.parse(data)[0].GELISIMFILTRE, function (d) { return d.DEGER });

            })
        },
    },

    mounted() {
        this.Yenile();
    },


    watch: {
        tc() {
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


//----------------------------------------------------------------------------------------------
//gelisimraporu Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-gelisimraporu-tur", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" title="Rapor Seçiniz..">
                            <option v-for="item in Liste" v-bind:value="item.ID">{{item.AD}}</option>
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
            var list = [];
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.SelectedID.sort(function (a, b) { return a - b });
                this.$emit('onchange', this.SelectedID);
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.SelectedID.sort(function (a, b) { return a - b });
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.SelectedID.sort(function (a, b) { return a - b });
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            this.Liste = [];
            this.Liste.push({ ID: -1, AD: 'Tümü' });
            this.Liste.push({ ID: 1, AD: 'Ders Öğretmen' });
            this.Liste.push({ ID: 2, AD: 'Yazılı Yoklama Sonuçları (1. Dönem)' });
            this.Liste.push({ ID: 3, AD: 'Yazılı Yoklama Sonuçları (2. Dönem)' });
            this.Liste.push({ ID: 4, AD: 'Deneme Sonuçları' });
            this.Liste.push({ ID: 5, AD: 'Gelişim Analizleri' });
            this.Liste.push({ ID: 6, AD: 'Net Ortalamaları Karşılaştırma' });
            this.Liste.push({ ID: 7, AD: '%50 Altındaki Kazanımlar Tablosu' });
            this.Liste.push({ ID: 8, AD: 'Ödev Raporu' });
            if (this.controller == "OgrenciBelgeGelisimRaporuOO") {
                this.Liste.push({ ID: 9, AD: 'Öğrenci Değerlendirme Raporu (Yorumlu)' });
            }
            this.Liste.push({ ID: 14, AD: 'Öğrenci Değerlendirme Raporu (Yorumsuz)' });
            this.Liste.push({ ID: 10, AD: 'Etüt Raporu' });
            this.Liste.push({ ID: 11, AD: 'Abide Dersler Grafiği' });
            this.Liste.push({ ID: 12, AD: 'LUS Raporu' });
            this.Liste.push({ ID: 13, AD: 'KOS Raporu' });
        },
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
Vue.component("c-gelisimraporu-tur-ls", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" title="Rapor Seçiniz..">
                            <option v-for="item in Liste" v-bind:value="item.ID">{{item.AD}}</option>
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
            var list = [];
            if (this.tumu == false && this.SelectedID.indexOf(-1) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.SelectedID.sort(function (a, b) { return a - b });
                this.$emit('onchange', this.SelectedID);
                return;
            }

            if (this.tumu == true && this.SelectedID.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.SelectedID = [];
            }
            if (this.tumu == true && this.SelectedID.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.SelectedID.indexOf(-1) > -1) {
                    this.SelectedID.splice($.inArray(-1, this.SelectedID), 1);
                }
            }
            this.SelectedID.sort(function (a, b) { return a - b });
            this.$emit('onchange', this.SelectedID)
            if (this.Liste.length - this.SelectedID.length == 1) {
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID);
                }
                this.SelectedID.sort(function (a, b) { return a - b });
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            this.Liste = [];
            this.Liste.push({ ID: -1, AD: 'Tümü' });
            this.Liste.push({ ID: 1, AD: 'Ders Öğretmen' });
            this.Liste.push({ ID: 2, AD: 'Yazılı Yoklama Sonuçları (1. Dönem)' });
            this.Liste.push({ ID: 3, AD: 'Yazılı Yoklama Sonuçları (2. Dönem)' });
            this.Liste.push({ ID: 4, AD: 'Deneme Sonuçları' });
            this.Liste.push({ ID: 5, AD: 'Gelişim Analizleri' });
            //this.Liste.push({ ID: 6, AD: 'Net Ortalamaları Karşılaştırma' });
            this.Liste.push({ ID: 7, AD: '%50 Altındaki Kazanımlar Tablosu' });
            this.Liste.push({ ID: 8, AD: 'Ödev Raporu' });
            if (this.controller == "OgrenciBelgeGelisimRaporuOO" || this.controller == "OgrenciBelgeGelisimRaporuLS") {
                this.Liste.push({ ID: 9, AD: 'Öğrenci Değerlendirme Raporu (Yorumlu)' });
            }
            this.Liste.push({ ID: 14, AD: 'Öğrenci Değerlendirme Raporu' });
            this.Liste.push({ ID: 10, AD: 'Etüt Raporu' });
            //this.Liste.push({ ID: 11, AD: 'Abide Dersler Grafiği' });
            //this.Liste.push({ ID: 12, AD: 'LUS Raporu' });
            //this.Liste.push({ ID: 13, AD: 'KOS Raporu' });
        },
    },

    mounted() {
        this.Yenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});
//----------------------------------------------------------------------------------------------
//Bursluluk Dosya Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-bursluluk-dosya", {

    props: ['controller', 'donem', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dosya </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_BURSLULUKDOSYA" v-bind:data-subtext="u.DONEM">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
            WebPost(this, this.controller, "BurslulukDosyaListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        donem() {
            this.Yenile();
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },

});

Vue.component("c-bursluluk-dosya-yan", {

    props: ['controller', 'donem', 'idset'],
    template: `
                <div class ="form-md-line-input">                    
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_BURSLULUKDOSYA" v-bind:data-subtext="u.DONEM">{{u.AD}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
            WebPost(this, this.controller, "BurslulukDosyaListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        donem() {
            this.Yenile();
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },

});
//----------------------------------------------------------------------------------------------
//Bursluluk Dosya Sınav Tarih - Seans Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-bursluluk-tarih-seans", {

    props: ['controller', 'idburslulukdosya', 'idset'],
    template: `
                <div class ="form-md-line-input">                    
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="{SINAVTARIH:u.SINAVTARIH , SEANS:u.SEANS}" v-bind:data-subtext="u.SEANS">{{u.SINAVTARIH}}</option>
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
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_BURSLULUKDOSYA: this.idburslulukdosya };
            WebPost(this, this.controller, "BurslulukDosyaTarihSeansListe", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        idburslulukdosya() {
            this.Yenile();
        },
        idset() {
            this.Yenile();

            this.SelectedID = (this.idset == undefined || this.idset == null) ? this.SelectedID = 0 : this.idset;

            this.OnChange();
        },
    },

});

//----------------------------------------------------------------------------------------------
//EXCEL SUTUN Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-excel-sutun", {
    props: ['liste', 'count', 'baslik'],
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
            liste: [],
            SelectedID: undefined,
            count: 0,
        }
    },

    methods: {},

    mounted() { },

    watch: {
        count() {
            //if (this.idset == null) {
            //    this.$emit('onchange', null, undefined);
            //}

            this.SelectedID = this.count == 0 ? undefined : this.SelectedID

            //this.SelectedID = (this.idset == undefined || this.idset == null) ? undefined : this.idset;
            //this.$emit('onchange', undefined, this.idset);


            //this.$emit('onchange', undefined, undefined);

        },
        liste() {
            console.log(this.baslik)
        },

        SelectedID(val, old) {


            //console.log(" " )
            //console.log("idset : " + this.idset)
            //console.log("SelectedID : " + this.SelectedID)
            //console.log("SelectedIDold : " + old)

            if (old == undefined) {
                this.count++;
            }

            this.$emit('onchange', old, val);
        },

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});


//----------------------------------------------------------------------------------------------
//Tyt Seçim Tür Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tyt-secim-tur", {
    props: ['controller', 'id_sinav'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Tyt Seçim Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_TYTSECIMTUR">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data:
        function () {
            return {
                SelectedID: 0,
                Liste: []
            }
        },

    methods: {
        OnChange() {
            this.$emit('onchange', getObjects(this.Liste, 'ID_TYTSECIMTUR', this.SelectedID)[0]);
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAV: this.id_sinav };

            WebPost(this, this.controller, "TytSecimTurListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                for (var i = 0; i < parent.Liste.length; i++) {
                    if (parent.Liste[i].SELECTED == 1) {
                        parent.$emit('onchange', parent.Liste[i]);
                        parent.SelectedID = parent.Liste[i].ID_TYTSECIMTUR;
                    }
                }
            })
        }
    },

    watch: {
        id_sinav() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});



//Onlne Test Sinav Listele

Vue.component("c-test-sinav-abidik", {

    props: ['controller', 'idkademe3'],
    template: `
                <div class="form-md-line-input">
                     <div class ="col-md-3">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
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

    mounted: function () {
        this.Yenile();
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            debugger
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavBilgiGetir", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idkademe3() {
            this.Yenile();
        }
    }
});


/*SINAV DERS ÜNİTE*/
Vue.component("c_dersunite", {
    props: ['controller', 'id_ders','id_kademe3'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Konu Seçiniz</label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DERSUNITE">{{u.AD}}</option>
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
        DersListesi() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.id_kademe3, ID_DERS: this.id_ders};
            WebPost(this, this.controller, "HaftaninKazanimi", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        //Sayfa ilk açıldığında

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_ders() {
            this.DersListesi();
        }
        
    },
});


Vue.component("c-sinav-donem-unite", {
    props: ['controller', 'id_kademe3', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <label class="control-label col-md-3" style="vertical-align:middle;">Sınav </label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
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
                  
            if (this.SelectedID == undefined)
                this.SelectedID = this.Liste[0].ID_UNITETARAMASINAV;
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            var _this = this;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.id_kademe3, DONEM: this.donem };
            WebPost(this, this.controller, "UniteSinavListeleKademeDonem", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            });
        }
    },

    mounted() {

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_kademe3() {
            this.Yenile();
        },
        id_sinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idsinav() {
            this.SelectedID = this.idsinav;
            this.$emit('onchange', this.idsinav);
        },
    },
});