//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {

    props: ['controller', 'idkademe3', 'idyazili'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" v-if="idyazili==undefined">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
                        </select>
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" v-if="idyazili!=undefined&&idyazili==0">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3">{{u.AD}}</option>
                        </select>
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" v-if="idyazili!=undefined&&idyazili>0" disabled>
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

    mounted: function () {
        this.Yenile();
    },

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "Kademe3Listele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                if (parent.SelectedID == 0 && parent.idkademe3 != undefined) {
                    parent.SelectedID = parent.idkademe3;
                    vue.$forceUpdate();
                }
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

//----------------------------------------------------------------------------------------------
//kademe2 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe2", {

    props: ['controller', 'idkademe3', 'idkademe2temp'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Okul Tür </label>
                    <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control" v-model="idkademe2" @change="OnChange" title="Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME2">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            idkademe2: [],
            tumu: false,
            Liste: []
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.idkademe2.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.idkademe2 = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.idkademe2.push(this.Liste[i].ID_KADEME2);
                }
                this.$emit('onchange', this.idkademe2)
                return;
            }

            if (this.tumu == true && this.idkademe2.indexOf(-1) == -1) { // tümü iptal
                this.tumu = false;
                this.idkademe2 = [];
            }
            if (this.tumu == true && this.idkademe2.indexOf(-1) > -1) {
                this.tumu = false;
                if (this.idkademe2.indexOf(-1) > -1) {
                    this.idkademe2.splice($.inArray(-1, this.idkademe2), 1);
                }
            }
            this.$emit('onchange', this.idkademe2)
            if (this.Liste.length - this.idkademe2.length == 1) {
                this.tumu = true;
                this.idkademe2 = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.idkademe2.push(this.Liste[i].ID_KADEME2);
                }
                this.$emit('onchange', this.idkademe2);
            }
        },

        Yenile() {
            var _this = this;
            _this.Liste = [];

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "Kademe2Listele", p, '', '', function (data, parent) {
                _this.Liste.push({ ID_KADEME2: 0, AD: 'Tümü' })
                $.each(JSON.parse(data), function (j, el) {
                    _this.Liste.push({ ID_KADEME2: el.ID_KADEME2, AD: el.AD });
                });
                if (_this.idkademe2.length == 0) {
                    _this.idkademe2 = _this.idkademe2temp;
                }
            })
        }
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

//----------------------------------------------------------------------------------------------
//Tarih Seçici Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih", {
    props: ['tarih'],

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
//Yarıyıl Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-yariyil", {
    props: ['yariyil'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="yariyil" @change="OnChange" title="Seçiniz..." id="YARIYIL">
                            <option value="1">1. DÖNEM</option>
                            <option value="2">2. DÖNEM</option>
                        </select>
                    </div>
                </div>
        `
    ,

    methods: {
        OnChange() {
            this.$emit('onchange', this.yariyil)
        }
    }
});

//----------------------------------------------------------------------------------------------
//Dönem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
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

//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {

    props: ['controller'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class="col-md-9">
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
Vue.component("c-sube-kur", {

    props: ['controller'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class="col-md-9">
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
        WebPost(this, this.controller, "SubeKurListelebyKullanici", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Sınıf Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idsube', 'idkademe3'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class ="col-md-9">
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_KADEME3: _idkademe3 };

            WebPost(this, this.controller, "SinifListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
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
Vue.component("c-sinif-kur", {

    props: ['controller', 'idsube', 'idkademe3'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class ="col-md-9">
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBE: _idsube, ID_KADEME3: _idkademe3 };

            WebPost(this, this.controller, "SinifKurSinifListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
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

//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3kullanici", {

    props: ['controller', 'idsube'],
    template: `

                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Grup Seçiniz..</option>
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
            WebPost(this, this.controller, "Kademe3ListelebyKullanici", p, '', '', function (data, parent) {
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
Vue.component("c-kademe3kullanici-kur", {

    props: ['controller', 'idsube'],
    template: `

                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Grup Seçiniz..</option>
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
            WebPost(this, this.controller, "Kademe3KurListelebyKullanici", p, '', '', function (data, parent) {
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
//Sınav Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ders", {
    props: ['controller', 'idkademe3', 'idderstemp', 'idyazili'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders</label>
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="idders" @change="OnChange" id="dersselect" v-if="idyazili==undefined">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
                        </select>
                        <select class ="selectpicker form-control" v-model="idders" @change="OnChange" id="dersselect" v-if="idyazili!=undefined&&idyazili==0">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
                        </select>
                        <select class ="selectpicker form-control" v-model="idders" @change="OnChange" id="dersselect" v-if="idyazili!=undefined&&idyazili>0" disabled>
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            idders: 0,
            Liste: []
        }
    },

    methods: {
        OnChange() {
            if (this.idders == undefined && this.idderstemp > 0)
                this.idders = this.idderstemp;
            this.$emit('onchange', this.idders);
        },
        DersListesi() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "KademeYaziliDersGetir", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idkademe3() {
            this.DersListesi();
        },
        idderstemp() {
            this.idders = this.idderstemp;
            this.$emit('onchange', this.idders);
        },
    }
});

//----------------------------------------------------------------------------------------------
//Soru Sayısı Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sorusayisi", {
    props: ['sorusayisi', 'idyazili'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Soru Sayısı</label>
                    <div class="col-md-9">
                        <select class="selectpicker form-control" v-model="sorusayisi" @change="OnChange" v-if="idyazili==undefined">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in 100" v-bind:value="u">{{u}}</option>
                        </select>
                        <select class="selectpicker form-control" v-model="sorusayisi" @change="OnChange" v-if="idyazili!=undefined&&idyazili==0">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in 100" v-bind:value="u">{{u}}</option>
                        </select>
                        <select class="selectpicker form-control" v-model="sorusayisi" @change="OnChange" v-if="idyazili!=undefined&&idyazili>0" disabled>
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in 100" v-bind:value="u">{{u}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    methods: {
        OnChange() {
            this.$emit('onchange', this.sorusayisi)
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

//----------------------------------------------------------------------------------------------
//Dönem Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili", {

    props: ['controller', 'idkademe3', 'yariyil', 'idsinif', 'donem'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Yazılı </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="yaziliselect">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.KOD}}</option>
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

        Yenile: function () {
            if (this.idkademe3 != 0 && this.yariyil != 0) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, YARIYIL: this.yariyil, ID_SINIF: this.idsinif, DONEM: this.donem };
                WebPost(this, this.controller, "YaziliListeleSelect", p, '', '', function (data, parent) {
                    if (data != null) {
                        parent.Liste = JSON.parse(data);
                    } else {
                        parent.Liste = [];
                    }
                });
            }
        }
    },

    updated() {
        $('#yaziliselect').selectpicker('refresh');
    },

    watch: {
        idkademe3() {
            this.Yenile();
        },

        yariyil() {
            this.Yenile();
        },

        idsinif() {
            this.Yenile();
        },

        donem() {
            this.Yenile();
        }
    }

});

//----------------------------------------------------------------------------------------------
//yazılı türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-yazilituru", {
    props: ['controller', 'id_yaziliturutemp'],
    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Yazılı Turu </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILITURU">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "YaziliTuruListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                if (parent.SelectedID == 0 && parent.id_yaziliturutemp != undefined) {
                    parent.SelectedID = parent.id_yaziliturutemp;
                    vue.$forceUpdate();
                }
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        id_yaziliturutemp() {
            this.Yenile();
        }
    }
});

 