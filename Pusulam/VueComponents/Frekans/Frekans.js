
Vue.component("f-donem", {

    props: ['controller', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Dönem
                        </div>
                        <div>
                            <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Dönem Seçiniz...">
                                <option v-for="u in Liste" v-bind:value="u.DONEM">{{u.ACIKLAMA}}</option>
                            </select>
                        </div>
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});

Vue.component("c-frekans-donem", {

    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Dönem </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
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
        },

        GetData() {
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "FrekansDonemListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                for (var i = 0; i < parent.Liste.length; i++) {
                    if (parent.Liste[i].AKTIF) {
                        parent.SelectedID = parent.Liste[i].DONEM;
                        parent.$emit('onchange', parent.SelectedID)
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

Vue.component("f-sube", {

    props: ['controller', 'idset'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Şube
                        </div>
                        <div>
                            <select class ="selectpicker form-control" multiple="true"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Şube Seçiniz...">                          
                                <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
                            </select>
                        </div>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            tumu: false,
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
        },
        GetData() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
                //parent.Liste = data;

                parent.tumu = false;

                parent.Liste = [];
                parent.Liste.push({ ID_SUBE: 0, AD: 'Tümü', SUBENO: 0 });

                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_SUBE: el.ID_SUBE, AD: el.AD, SUBENO: el.SUBENO })
                });

                if ((parent.idset != undefined && parent.idset != null)) {
                    parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                    parent.OnChange();
                }

            })
        }
    },

    mounted() {
        this.GetData();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("f-kademe-multi", {

    props: ['controller', 'idset', 'idsubes'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Kademe
                        </div>
                        <div>
                            <select class ="selectpicker form-control" multiple="true"  v-model="SelectedID" @change="OnChange" title="Kademe Seçiniz...">                          
                                <option v-for="u in Liste" v-bind:value="u.ID_KADEME">{{u.AD}}</option>
                            </select>
                        </div>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: [],
            tumu: false,
            Liste: []
        }
    },

    methods: {

        OnChange() {


            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
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
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            if (this.idsubes.length == 0) {
                return;
            }

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBEs: JSON.stringify(this.idsubes) };
            WebPost(this, this.controller, "KullaniciKademeListele", p, '', '', function (data, parent) {
                //parent.Liste = data;

                parent.tumu = false;

                parent.Liste = [];
                parent.Liste.push({ ID_KADEME: 0, AD: 'Tümü' });

                $.each(JSON.parse(data), function (j, el) {
                    parent.Liste.push({ ID_KADEME: el.ID_KADEME, AD: el.AD })
                });

                if ((parent.idset != undefined && parent.idset != null)) {
                    parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                    parent.OnChange();
                }

            })
        }
    },

    mounted() {
        this.GetData();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    },

    watch: {
        idsubes() {
            this.GetData();
        },
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("f-kademe3-multi", {

    props: ['controller', 'idsube', 'lise', 'idkademes', 'idset'],
    template: `

                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Grup
                        </div>
                        <div>
                            <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" title="Grup Seçiniz..">
                                <option v-for="u in Liste" v-bind:value="u.ID_SINAVGRUP">{{u.GRUP}}</option>
                            </select>
                        </div>
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

            if (this.idsube.length == 0 || this.idkademes.length == 0) {
                return;
            }
            var _this = this;

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, SUBELER: JSON.stringify(this.idsube), ID_KADEMEs: JSON.stringify(this.idkademes) };
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


                parent.tumu = false;

                parent.Liste = [];
                parent.Liste.push({ ID_SINAVGRUP: 0, GRUP: 'Tümü' })
                $.each(list, function (j, el) {
                    parent.Liste.push({ ID_SINAVGRUP: el.ID_SINAVGRUP, GRUP: el.GRUP })

                });


                if ((parent.idset != undefined && parent.idset != null)) {
                    parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                    parent.OnChange();
                }



            })
        }
    },

    mounted() {
        this.Yenile();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
    },

    watch: {
        idsube() {
            this.Yenile();
        },
        idkademes() {
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

Vue.component("c-frekans-sinav-turu", {

    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınav Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU">{{u.AD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "FrekansSinavTuruListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);                
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("f-sinav-turu-multi", {

    props: ['controller', 'idkademe3', 'idset'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Sınav Türü
                        </div>
                        <div>
                            <select class ="selectpicker form-control" multiple="true" v-model="SelectedID" @change="OnChange" id="SinavTuru" title="Sınav Türü Seçiniz.." >
                                <option v-for="u in Liste" v-bind:value="u.ID_SINAVTURU" >{{u.AD}}</option>
                            </select>
                        </div>
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
            //if ((this.idset != undefined && this.idset != null)) {

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
            //}
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


            if (this.idkademe3.length == 0) {
                return;
            }

            if (this.idkademe3 == undefined) {
                this.idkademe3 = 0;
            }


            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3) };
            WebPost(this, this.controller, "SinavTuruListele", p, '', '', function (data, parent) {




                parent.Liste = [];

                parent.tumu = false;

                if (data.length > 0) {

                    parent.Liste.push({ ID_SINAVTURU: -1, AD: 'Tümü' });

                    $.each(data, function (j, el) {
                        parent.Liste.push({ ID_SINAVTURU: el.ID_SINAVTURU, AD: el.AD })
                    });

                    if ((parent.idset != undefined && parent.idset != null)) {

                        parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));

                        parent.$emit('onchange', parent.idset);

                        //parent.OnChange();
                    }
                }

            })
        },
    },

    mounted() {
        this.Yenile();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    },


    watch: {
        idkademe3() {
            this.Yenile();
        },
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

Vue.component("f-frekans-ders-multi", {
    props: ['controller', 'idkademe3', 'idsinavturu', 'idset'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <div style="margin-bottom: 10px;">
                            Ders
                        </div>
                        <div>
                            <select class ="selectpicker form-control" multiple="true"  v-model="SelectedID" @change="OnChange" title="Ders Seçiniz..." id="frekans-ders">
                               <option v-for="u in Liste" v-bind:value="u.AD">{{u.AD}}</option>
                            </select>
                        </div>
                    </div>
                </div>
        `
    ,

    data:
        function () {
            return {
                SelectedID: [],
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


            if (this.idsinavturu.length == 0 || this.idkademe3.length == 0) {
                return;
            }


            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3), ID_SINAVTURU: JSON.stringify(this.idsinavturu) };


            if (JSON.stringify(this.idsinavturu) == '[-1]' || (JSON.stringify(this.idkademe3) == '[0]')) {
                return;
            }
            else {
                WebPost(this, this.controller, "FrekansDersListele", p, '', '', function (data, parent) {
                    //parent.Liste = JSON.parse(data)[0].t5;
                    //parent.Liste.unshift({ ID_DERS: 0, AD: 'Tümü' });
                    console.log(JSON.parse(data)[0])
                    console.log(JSON.parse(data)[0].t5)
                    if (JSON.parse(data)[0].t5.length > 0) {

                        parent.tumu = false;

                        parent.Liste = [];
                        parent.Liste.push({ ID_DERS: 0, AD: 'Tümü' });

                        $.each(JSON.parse(data)[0].t5, function (j, el) {
                            parent.Liste.push({ ID_DERS: el.ID_DERS, AD: el.AD })
                        });


                        if ((parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));

                            parent.$emit('onchange', parent.idset);
                            //parent.OnChange();
                        }

                    }
                });
            }
        }
    },

    //mounted() {
    //    this.Yenile();
    //    this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    //},

    watch: {
        idkademe3() {
            if (JSON.stringify(this.idkademe3) != '[0]') {
                this.Yenile();
            }
        },
        idsinavturu() {

            if (JSON.stringify(this.idsinavturu) != '[-1]') {
                this.Yenile();
            }
        },

        Liste() {
            this.Liste = (this.Liste == undefined || this.Liste == null) ? [] : this.Liste;
        },
        SelectedID() {
            this.SelectedID = (this.SelectedID == undefined || this.SelectedID == null) ? [] : this.SelectedID;
        },

        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.$emit('onchange', this.idset)
            //this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});



Vue.component("f-frekans-ders-multi-yan", {
    props: ['controller', 'idkademe3', 'idsinavturu', 'idset'],

    template: `
                <div class="form-md-line-input">
                    <div >
                        <label class ="control-label col-md-3" style="vertical-align:middle;">Ders </label>
                        <div class ="col-md-9">
                            <select class ="selectpicker form-control" multiple="true"  v-model="SelectedID" @change="OnChange" title="Ders Seçiniz..." id="frekans-ders">
                               <option v-for="u in Liste" v-bind:value="u.AD">{{u.AD}}</option>
                            </select>
                        </div>
                    </div>
                </div>
        `
    ,

    data:
        function () {
            return {
                SelectedID: [],
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


            if (this.idsinavturu.length == 0 || this.idkademe3.length == 0) {
                return;
            }


            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: JSON.stringify(this.idkademe3), ID_SINAVTURU: JSON.stringify(this.idsinavturu) };


            if (JSON.stringify(this.idsinavturu) == '[-1]' || (JSON.stringify(this.idkademe3) == '[0]')) {
                return;
            }
            else {

                this.tumu = false;
                ListeTemizle(this.Liste);

                WebPost(this, this.controller, "FrekansDersListele", p, '', '', function (data, parent) {
                    if (JSON.parse(data)[0].t5.length > 0) {
                        parent.Liste.push({ ID_DERS: 0, AD: 'Tümü' });

                        $.each(JSON.parse(data)[0].t5, function (j, el) {
                            parent.Liste.push({ ID_DERS: el.ID_DERS, AD: el.AD })
                        });


                        if ((parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));

                            parent.$emit('onchange', parent.idset);
                            //parent.OnChange();
                        }

                    }
                });
            }
        }
    },

    //mounted() {
    //    this.Yenile();
    //    this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    //},

    watch: {
        idkademe3() {
            if (JSON.stringify(this.idkademe3) != '[0]') {
                this.Yenile();
            }
        },
        idsinavturu() {

            if (JSON.stringify(this.idsinavturu) != '[-1]') {
                this.Yenile();
            }
        },

        Liste() {
            this.Liste = (this.Liste == undefined || this.Liste == null) ? [] : this.Liste;
        },
        SelectedID() {
            this.SelectedID = (this.SelectedID == undefined || this.SelectedID == null) ? [] : this.SelectedID;
        },

        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
            this.$emit('onchange', this.idset)
            //this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
