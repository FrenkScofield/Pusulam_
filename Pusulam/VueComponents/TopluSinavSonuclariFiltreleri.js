//----------------------------------------------------------------------------------------------
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------

Vue.component("c-sinav-turu-tc", {

    props: ['controller', 'tc', 'donem', 'idkademe3'],

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
//Şube Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube-multi-tumu", {

    props: ['controller', 'full'],

    template: `
                <div class="form-md-line-input" v-if="!full">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true"  title="Şube Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
                    <div class='col-sm-9' v-else-if="full">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true"  title="Şube Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
                        </select>
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
            //parent.Liste = data;
            parent.Liste = [];
            parent.Liste.push({ ID_SUBE: 0, AD: 'Tümü' })
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
Vue.component("c-kademe3-multisube", {

    props: ['controller', 'idsube', 'full'],
    template: `

                <div class="form-md-line-input" v-if="!full">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Grup Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
                        </select>
                    </div>
                </div>
                    <div class="col-sm-9" v-else-if="full">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Grup Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
                        </select>
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
            var _idsube=JSON.stringify(this.idsube);
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
Vue.component("c-sinif-multi-multisube", {

    props: ['controller', 'idsube', 'idkademe3', 'full'],

    template: `
                <div class="form-md-line-input" v-if="!full">
                    <div class="col-md-2">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
                    </div>
                </div>

                <div class="col-sm-9" v-else-if="full">
                        <select multiple="multiple" class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Sınıf Seçiniz..." id="sinifMulti">
                           <option v-for="u in Liste" v-bind:value="u.ID_SINIF">{{u.AD}}</option>
                        </select>
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
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
                parent.Liste.push({ ID_SINIF: 0, AD: 'Tümü' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SINIF: el.ID_SINIF, AD: el.AD })
                });
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
//Öğrenci multi Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-multi-multisinif", {

    props: ['controller', 'idsinif'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select  multiple="multiple"  class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" title="Öğrenci Seçiniz..">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD + ' '+u.SOYAD}}</option>
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
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
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
                    this.SelectedID.push(this.Liste[i].TCKIMLIKNO);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var _idsinif = JSON.stringify(this.idsinif);
            var _idkademe3 = this.idkademe3;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  SINIFLAR: _idsinif };

            WebPost(this, this.controller, "OgrenciListeleMultiSinif", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ TCKIMLIKNO: 0, AD: 'Tümü',SOYAD:'' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ TCKIMLIKNO: el.TCKIMLIKNO, AD: el.AD ,SOYAD:el.SOYAD})
                });
                //parent.Liste = data;
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
//Sınav Türü Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-turu", {

    props: ['controller'],

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
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM  };
        WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {
            parent.Liste = data;
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
    props: ['controller', 'idsinavturu', 'donem','idkademe3'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Sınav Seçiniz</option>
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

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM,  ID_SINAVTURU: _idsinavturu, DONEM: _donem, ID_KADEME3: this.idkademe3 };
            WebPost(this, this.controller, "SinavListele", p, '', '', function (data, parent) {
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
        idsinavturu() {
            this.Yenile();
        },
        donem() {
            this.Yenile();
        },
        idkademe3() {
            this.Yenile();
        },
    },
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
//----------------------------------------------------------------------------------------------
//Sınav Puan Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-puan-turu", {
    props: ['controller', 'tc', 'idsinavturu'],
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
        OnChange () {
            this.$emit('onchange', this.SelectedID)
        },
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, TC_OGRENCI: this.tc, ID_SINAVTURU: this.idsinavturu };
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
        idsinavturu() {
            this.Yenile();
        }
    },
});
//----------------------------------------------------------------------------------------------
//Sınav Ders Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinav-ders", {
    props: ['dersler'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-12">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="Dersler">
                            <option value="0">Toplam Net</option>
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
        OnChange () {
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
            console.log(this.dersler)
            this.Liste = this.dersler;
        },
    },
});