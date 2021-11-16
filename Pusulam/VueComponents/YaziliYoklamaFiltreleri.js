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

//----------------------------------------------------------------------------------------------
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-multi", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Şube Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
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
        OnChange () {
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

//----------------------------------------------------------------------------------------------
//kademe3 Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3-multisube", {

    props: ['controller', 'idsube'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsube = JSON.stringify(this.idsube);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  SUBELER: _idsube };
            WebPost(this, this.controller, "Kademe3ListeleMultiSube", p, '', '', function (data, parent) {
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

    props: ['controller', 'idsube', 'idkademe3'],

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

//----------------------------------------------------------------------------------------------
//Sınıf Multi Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif-multi", {

    props: ['controller', 'idsube', 'idkademe3'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
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
        },

        Yenile() {
            var _idsube = JSON.stringify(this.idsube);
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SUBES: _idsube, ID_KADEME3: _idkademe3 };
            WebPost(this, this.controller, "SinifListeleMultiSube", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tüm Sınıflar' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD })
                });
                console.log(data);
                try {
                    vue.ID_KADEME = data[0].ID_KADEME
                } catch (e) {

                }
                //parent.Liste.push= data;



            })
        }
    },

    watch: {
        idsube() {
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

//----------------------------------------------------------------------------------------------
//Öğrenci Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci", {

    props: ['controller', 'idsinif'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
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

    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            var _idsinif = this.idsinif;
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINIF: _idsinif };

            WebPost(this, this.controller, "OgrenciListelebyKullanici", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idsinif() {
            this.Yenile();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Öğrenci Veli Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-veli", {

    props: ['controller', 'tc'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM  };

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

//----------------------------------------------------------------------------------------------
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="DonemPicker">
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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM  };
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
//Yazılı Yoklama Componenti MultiSelect
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili", {
    props: ['controller', 'donem', 'tc'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange"   title="Seçiniz...">
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var _idsinavturu = this.idsinavturu;
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  DONEM: _donem, TC_OGRENCI: _tc };
            WebPost(this, this.controller, "YaziliYoklamaSinavListesibyOgrenci", p, '', '', function (data, parent) {
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
        donem() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Yazılı Yoklama Componenti Sınıf Bazlı
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili-genel", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'idsube', 'idsinif', 'idkademe3'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var _idsinif = JSON.stringify(this.idsinif);
            var _idsube = JSON.stringify(this.idsube);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERS: this.idders, SUBELER: _idsube, SINIFLAR: _idsinif, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "YaziliYoklamaSinavListesi", p, '', '', function (data, parent) {
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
        donem() {
            this.Yenile();
        },
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        idsube() {
            this.Yenile();
        },
        idsinif() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Yazılı Yoklama Componenti Sınıf Bazlı
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili-genel-multi-ders", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'idkademe3'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
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
        OnChange () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            var _idders = JSON.stringify(this.idders);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERSLER: _idders, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "YaziliYoklamaSinavListesi", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINAV: 0, AD: 'Tüm Sınavlar' })
                var parseddata = JSON.parse(data);
                $.each(parseddata, function (j, el) {
                    parent.Liste.push({ ID_SINAV: el.ID_SINAV, AD: el.AD })
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
        donem() {
            this.Yenile();
        },
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Puan Aralığı Componenti 
//----------------------------------------------------------------------------------------------
Vue.component("c-puan", {
    props: ['idkademe'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option v-for="u in Liste" v-bind:value="u.value">{{u.text}}</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: '',
            Liste:[],
        }
    },

    methods: {
        OnChange () {
            this.$emit('onchange', this.SelectedID);
        },
        Yenile() {
            this.Liste = [];
            if (this.idkademe == 5 || this.idkademe == undefined) {
                this.Liste.push({ "value": "0-100", "text": "0 - 100" });
                this.Liste.push({ "value": "85.00-100.00", "text": "85,00 - 100,00" });
                this.Liste.push({ "value": "70.00-84.99", "text": "70,00 - 84,99" });
                this.Liste.push({ "value": "60.00-69.99", "text": "60,00 - 69,99" });
                this.Liste.push({ "value": "50.00-59.99", "text": "50,00 - 59,99" });
                this.Liste.push({ "value": "0-49.99", "text": "0 - 49,99" });
            }
            else {
                this.Liste.push({ "value": "0-100", "text": "0 - 100" });
                this.Liste.push({ "value": "85.00-100.00", "text": "85,00 - 100,00" });
                this.Liste.push({ "value": "70.00-84.99", "text": "70,00 - 84,99" });
                this.Liste.push({ "value": "55.00-69.99", "text": "55,00 - 69,99" });
                this.Liste.push({ "value": "45.00-54.99", "text": "45,00 - 54,99" });
                this.Liste.push({ "value": "0-44.99", "text": "0 - 44,99" });
            }
        }

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        idkademe() {
            this.Yenile();
        },
    },
});

//----------------------------------------------------------------------------------------------
//Puan Aralığı Componenti 
//----------------------------------------------------------------------------------------------
Vue.component("c-tc", {
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="1">TC\'li</option>
                            <option value="0">TC\'siz</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: 1
        }
    },

    methods: {
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

//----------------------------------------------------------------------------------------------
//kademe3 Listesi Şubesiz Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3-subesiz", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
    },

    mounted () {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "Kademe3ListeleSubesiz", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Gruba Göre Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu", {
    props: ['controller', 'idkademe3'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz.." >
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" selected="true">{{u.KOD}}</option>
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
            this.$emit('onchange', this.SelectedID);
        },
        Yenile: function () {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavTuruListeleGrubaGore", p, '', '', function (data, parent) {
                parent.Liste = [];
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, KOD: el.KISAAD })
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
        }
    }
});

//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Gruba Göre Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-multi", {
    props: ['controller', 'idsinavturu', 'donem', 'idkademe3'],
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
        OnChange () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
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
                    this.SelectedID.push(this.Liste[i].ID_SINAV);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            var _idsinavturu = new Array();
            _idsinavturu.push(this.idsinavturu);
            var _donem = this.donem;
            var _tc = this.tc;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURUS: JSON.stringify(_idsinavturu), DONEM: _donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListelebyGrupCoklu", p, '#FiltreDivOgrenci', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_SINAV: 0, AD: 'Tüm Sınavlar' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINAV: el.ID_SINAV, AD: el.AD })
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
        idkademe3() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        }
    },
});
























//----------------------------------------------------------------------------------------------
//Yazılı Yoklama Componenti Sınıf Bazlı
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili-genel-multi-ders-yeni", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'idkademe3'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
        OnChange () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_YAZILI);
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
                    this.SelectedID.push(this.Liste[i].ID_YAZILI);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            var _idders = JSON.stringify(this.idders);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERSLER: _idders, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "YaziliYoklamaSinavListesiYeni", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_YAZILI: 0, AD: 'Tüm Sınavlar' })
                var parseddata = JSON.parse(data);
                $.each(parseddata, function (j, el) {
                    parent.Liste.push({ ID_YAZILI: el.ID_YAZILI, AD: el.AD })
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
        donem() {
            this.Yenile();
        },
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
});
Vue.component("c-yazili-genel-multi-ders-sinifs", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'siniflar'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
        OnChange () {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_YAZILI);
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
                    this.SelectedID.push(this.Liste[i].ID_YAZILI);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        Yenile() {
            var _idders = JSON.stringify(this.idders);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERSLER: _idders, SINIFLAR: JSON.stringify(this.siniflar) };
            WebPost(this, this.controller, "YaziliYoklamaSinifSinavListesi", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_YAZILI: 0, AD: 'Tüm Sınavlar' })
                var parseddata = JSON.parse(data);
                $.each(parseddata, function (j, el) {
                    parent.Liste.push({ ID_YAZILI: el.ID_YAZILI, AD: el.AD })
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
        donem() {
            this.Yenile();
        },
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        siniflar() {
            this.Yenile();
        },
    },
});

Vue.component("c-yazili-genel-multi-ders-sinifs-kos", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'siniflar'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
            var _idders = JSON.stringify(this.idders);

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERSLER: _idders, SINIFLAR: JSON.stringify(this.siniflar) };
            WebPost(this, this.controller, "YaziliYoklamaSinifSinavListesiKOS", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
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
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        siniflar() {
            this.Yenile();
        },
    },
});
//----------------------------------------------------------------------------------------------
//Yazılı Yoklama Componenti Sınıf Bazlı
//----------------------------------------------------------------------------------------------
Vue.component("c-yazili-genel-yeni", {
    props: ['controller', 'donem', 'yariyil', 'idders'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERS: this.idders};
            WebPost(this, this.controller, "YaziliYoklamaSinavListesiYeni", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
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
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
    },
});
Vue.component("c-yazili-genel-yeni-sinifs", {
    props: ['controller', 'donem', 'yariyil', 'idders', 'siniflar'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem, YARIYIL: this.yariyil, ID_DERS: this.idders, SINIFLAR: JSON.stringify(this.siniflar)};
            WebPost(this, this.controller, "YaziliYoklamaSinifSinavListesi", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
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
        yariyil() {
            this.Yenile();
        },
        idders() {
            this.Yenile();
        },
        siniflar() {
            this.Yenile();
        },
    },
});
Vue.component("c-yazili-ogrenci-kos", {
    props: ['controller', 'tcogrenci', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınav Seçiniz..." id="YaziliPicker">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_YAZILI">{{u.AD}}</option>
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
            this.SelectedID = 0;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tcogrenci, DONEM: this.donem };
            WebPost(this, this.controller, "YaziliYoklamaOgrenciKOSListesi", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
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
        tcogrenci() {
            this.Yenile();
        },

        donem() {
            this.Yenile();
        }
    },
});

