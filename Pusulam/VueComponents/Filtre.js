//----------------------------------------------------------------------------------------------
//Şube Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sube", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE"  v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SUBE;
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("c-sube-tumu", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Şube </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE"  v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SUBE;
                        parent.OnChange();
                    } else {
                        parent.Liste.unshift({ ID_SUBE: 0, SUBENO: '0', AD: 'Tümü' });
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
Vue.component("c-sube-multi", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Şube </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE" v-bind:data-subtext="[u.SUBENO == 0 ? '' : u.SUBENO ]" :style="'font-weight:'+[u.ID_SUBE == 0 ? 'bold' : '']">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
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
                    this.SelectedID.push(this.Liste[i].ID_SUBE);
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
            WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_SUBE: 0, SUBENO: 0, AD: 'Tümünü Seç' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SUBE;
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
//----------------------------------------------------------------------------------------------
//Kullanıcı Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici-tipi", {

    props: ['controller', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Kullanıcı Tipi </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_KULLANICITIPI" >{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "KullaniciTipiListele", p, '', '', function (data, parent) {
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
Vue.component("c-kullanici-tipi-multi", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Kullanıcı Tipi </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_KULLANICITIPI" v-bind:data-subtext="u.SUBENO" :style="'font-weight:'+[u.ID_KULLANICITIPI == 0 ? 'bold' : '']">{{u.AD}}</option>
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
        }
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_KULLANICITIPI);
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
                    this.SelectedID.push(this.Liste[i].ID_KULLANICITIPI);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            ListeTemizle(this.Liste);
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "KullaniciTipiListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
                if (data != "[]") {
                    parent.Liste.unshift({ ID_KULLANICITIPI: 0, AD: 'Tümünü Seç' });
                }

                try {
                    if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                        parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                        parent.OnChange();
                    }
                    parent.ilkYukleme = false;
                } catch (e) { }
            })
        }
    },


    //watch: {
    //    idset() {
    //        this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    //        this.OnChange();
    //    },
    //},

    mounted() {
        this.GetData();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Kademe Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe", {

    props: ['controller', 'idset', 'idsubelist', 'idsube'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Kademe </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME" >{{u.AD}}</option>
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
                ID_SUBE: this.idsube
            };

            if (this.idsubelist != undefined && this.idsubelist.length == 0
                || this.idsube != undefined && this.idsube == 0
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);
            ListeTemizle(this.Liste)
            WebPost(this, this.controller, "KademeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME;
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
        idsube() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
Vue.component("c-kademe-multi", {
    props: ['controller', 'idset', 'idsubelist', 'disabled'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Kademe </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME" :style="'font-weight:'+[u.ID_KADEME == 0 ? 'bold' : '']">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
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
                    this.SelectedID.push(this.Liste[i].ID_KADEME);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SUBELIST: JSON.stringify(this.idsubelist) };
            if (this.idsubelist != undefined && this.idsubelist.length == 0) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);
            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "KademeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_KADEME: 0, AD: 'Tümünü Seç' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME;
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Kademe3 Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe3", {

    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idsube', 'idkademe', 'danisman'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3" >{{u.AD}}</option>
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
                ID_KADEMELIST: JSON.stringify(this.idkademelist),
                ID_SUBE: this.idsube,
                ID_KADEME: this.idkademe,
                DANISMAN: this.danisman == undefined || this.danisman == null ? false : this.danisman
            };

            if ((this.idkademelist != undefined && this.idkademelist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe != undefined && this.idkademe == 0)
                || (this.idsube != undefined && this.idsube == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "Kademe3Listele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME3;
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
        idkademelist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("c-kademe3-tumu", {

    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idsube', 'idkademe', 'danisman'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option value="0">Tümü</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3" >{{u.AD}}</option>
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
                ID_KADEMELIST: JSON.stringify(this.idkademelist),
                ID_SUBE: this.idsube,
                ID_KADEME: this.idkademe,
                DANISMAN: this.danisman == undefined || this.danisman == null ? false : this.danisman
            };

            if ((this.idkademelist != undefined && this.idkademelist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe != undefined && this.idkademe == 0)
                || (this.idsube != undefined && this.idsube == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "Kademe3Listele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME3;
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
        idkademelist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
Vue.component("c-kademe3-multi", {
    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idsube', 'idkademe', 'disabled', 'danisman'],
    template: `
                
                 <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Grup </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Grup Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME3" :style="'font-weight:'+[u.ID_KADEME3 == 0 ? 'bold' : '']">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_KADEME3);
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
                    this.SelectedID.push(this.Liste[i].ID_KADEME3);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEMELIST: JSON.stringify(this.idkademelist),
                ID_SUBE: this.idsube,
                ID_KADEME: this.idkademe,
                DANISMAN: this.danisman == undefined || this.danisman == null ? false : this.danisman
            };

            if ((this.idkademelist != undefined && this.idkademelist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe != undefined && this.idkademe == 0)
                || (this.idsube != undefined && this.idsube == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "Kademe3Listele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_KADEME3: 0, AD: 'Tümünü Seç' });
                    }
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KADEME3;
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
        idkademelist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Sınıf Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-sinif", {

    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idkademe3list', 'idsube', 'idkademe', 'idkademe3', 'idders', 'idderslist', 'dersvarsasadecekur', 'danisman', 'kurharic'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Sınıf </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF" >{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
            ilkYukleme: true,
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
                ID_KADEMELIST: JSON.stringify(this.idkademelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_DERSLIST: JSON.stringify(this.idderslist),
                ID_SUBE: this.idsube,
                ID_KADEME: this.idkademe,
                ID_KADEME3: this.idkademe3,
                ID_DERS: this.idders,
                DERSVARSASADECEKUR: this.dersvarsasadecekur,
                DANISMAN: this.danisman == undefined || this.danisman == null ? false : this.danisman,
                KURHARIC: this.kurharic
            };

            if ((this.idkademelist != undefined && this.idkademelist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idderslist != undefined && this.idderslist.length == 0)
                || (this.idkademe != undefined && this.idkademe == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
                || (this.idders != undefined && this.idders == 0)
            ) {
                return;
            }
            if (this.sonArama == JSON.stringify(p)) {
                return;
            }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste)

            WebPost(this, this.controller, "SinifListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINIF;
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
        idkademelist() {
            this.GetData();
        },
        idkademe3list() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe() {
            this.GetData();
        },
        idkademe3() {
            this.GetData();
        },
        idders() {
            this.GetData();
        },
        idderslist() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
Vue.component("c-sinif-multi", {
    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idkademe3list', 'idsinifalanlist', 'idsube', 'idkademe', 'idkademe3', 'idders', 'idderslist', 'dersvarsasadecekur', 'disabled', 'danisman', 'idsinifalan', 'donem'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Sınıf </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIF" :style="'font-weight:'+[u.ID_SINIF == 0 ? 'bold' : '']">{{u.SINIFSUBE}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
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
                    this.SelectedID.push(this.Liste[i].ID_SINIF);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },
        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                DONEM: this.donem,
                ID_SUBELIST: JSON.stringify(this.idsubelist),
                ID_KADEMELIST: JSON.stringify(this.idkademelist),
                ID_KADEME3LIST: JSON.stringify(this.idkademe3list),
                ID_SINIFALANLIST: JSON.stringify(this.idsinifalanlist),
                ID_DERSLIST: JSON.stringify(this.idderslist),
                ID_SUBE: this.idsube,
                ID_KADEME: this.idkademe,
                ID_KADEME3: this.idkademe3,
                ID_SINIFALAN: this.idsinifalan,
                ID_DERS: this.idders,
                DERSVARSASADECEKUR: this.dersvarsasadecekur,
                DANISMAN: this.danisman == undefined || this.danisman == null ? false : this.danisman
            };

            if ((this.idkademelist != undefined && this.idkademelist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkademe3list != undefined && this.idkademe3list.length == 0)
                || (this.idsinifalanlist != undefined && this.idsinifalanlist.length == 0)
                || (this.idderslist != undefined && this.idderslist.length == 0)
                || (this.idkademe != undefined && this.idkademe == 0)
                || (this.idsube != undefined && this.idsube == 0)
                || (this.idsinifalan != undefined && this.idsinifalan == 0)
                || (this.idkademe3 != undefined && this.idkademe3 == 0)
                || (this.idders != undefined && this.idders == 0)
            ) {
                return;
            }
            if (this.sonArama == JSON.stringify(p)) {
                return;
            }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "SinifListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_SINIF: 0, AD: 'Tümünü Seç', SINIFSUBE: 'Tümünü Seç' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINIF;
                        parent.OnChange();
                    }

                    try {
                        if ((parent.idset != undefined && parent.idset != null)) {
                            parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                            parent.OnChange();
                        }
                    } catch (e) { }
                }
            })
        },
    },

    watch: {
        idsubelist() {
            this.GetData();
        },
        idkademelist() {
            this.GetData();
        },
        idkademe3list() {
            this.GetData();
        },
        idsinifalanlist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkademe() {
            this.GetData();
        },
        idkademe3() {
            this.GetData();
        },
        idsinifalan() {
            this.GetData();
        },
        idders() {
            this.GetData();
        },
        idderslist() {
            this.GetData();
        },
        donem() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});



Vue.component("c-sinif-alan-multi", {

    props: ['controller', 'idset', 'disabled', 'yenile'],

    template: ` <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Sınıf Alan </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINIFALAN" :style="'font-weight:'+[u.ID_SINIFALAN == 0 ? 'bold' : '']">{{u.ALAN}}</option>
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
                ilkYukleme: true,
            }
        },

    mounted() {
        this.GetData();
        this.SelectedID = (this.idset == undefined || this.idset == null) ? [] : this.idset;
    },

    methods: {
        OnChange() {
            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                this.SelectedID = [];
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].ID_SINIFALAN);
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
                    this.SelectedID.push(this.Liste[i].ID_SINIFALAN);
                }
                this.$emit('onchange', this.SelectedID);
            }

        },

        GetData() {
            ListeTemizle(this.Liste);
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SinifAlanListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ ID_SINIFALAN: 0, ALAN: 'Tümünü Seç' });
                    }

                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SINIFALAN;
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
        }
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
        yenile() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Öğrenci Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci", {

    props: ['controller', 'idset', 'idsiniflist', 'idsinif'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğrenci </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true"  title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" v-bind:data-subtext="u.SINIFAD">{{u.ADSOYAD}}</option>
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

            if ((this.idsiniflist != undefined && this.idsiniflist.length == 0)
                || (this.idsinif != undefined && this.idsinif == 0)
            ) {
                return;
            }

            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIFLIST: JSON.stringify(this.idsiniflist), ID_SINIF: this.idsinif };
            WebPost(this, this.controller, "OgrenciListele", p, '', '', function (data, parent) {
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
        idsiniflist() {
            this.GetData();
        },
        idsinif() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

});
Vue.component("c-ogrenci-multi", {
    props: ['controller', 'idset', 'idsiniflist', 'idsinif', 'secili'],
    template: `
                <div class="form-md-line-input">
                   <label class ="control-label col-md-3">Öğrenci </label>
                   <div class ="col-md-9">
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" v-bind:data-subtext="u.SINIFAD">{{u.ADSOYAD}}</option>
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
            ListeTemizle(this.Liste);
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIFLIST: JSON.stringify(this.idsiniflist), ID_SINIF: this.idsinif };
            WebPost(this, this.controller, "OgrenciListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (data != "[]") {
                        parent.Liste.unshift({ TCKIMLIKNO: 0, ADSOYAD: 'Tümünü Seç' });
                    }

                    if (parent.secili) {
                        for (var i = 0; i < parent.Liste.length; i++) {
                            parent.SelectedID.push(parent.Liste[i].TCKIMLIKNO);
                        }
                        parent.tumu = true;
                        parent.OnChange();
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
        idsiniflist() {
            this.GetData();
        },

        idsinif() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
//----------------------------------------------------------------------------------------------
//Dönem Componentleri
//----------------------------------------------------------------------------------------------
Vue.component("c-donem", {

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
            WebPost(this, this.controller, "DonemListele", p, '', '', function (data, parent) {

                parent.Liste = JSON.parse(data);
                for (var i = 0; i < parent.Liste.length; i++) {
                    if (parent.Liste[i].AKTIF) {
                        parent.SelectedID = parent.Liste[i].DONEM;
                        $('.selectpicker').selectpicker('refresh');
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
//----------------------------------------------------------------------------------------------
//Kullanıcı Listesi Componentleri
//----------------------------------------------------------------------------------------------
Vue.component("c-kullanici", {

    props: ['controller', 'idset', 'idsubelist', 'idkullanicitipilist', 'idsube', 'idkullanicitipi'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Kişi </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_KULLANICITIPI" >{{u.ADSOYAD}}</option>
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
                ID_KULLANICITIPILIST: JSON.stringify(this.idkullanicitipilist),
                ID_SUBE: this.idsube,
                ID_KULLANICITIPI: this.idkullanicitipi,
            };

            if ((this.idkullanicitipilist != undefined && this.idkullanicitipilist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkullanicitipi != undefined && this.idkullanicitipi == 0)
                || (this.idsube != undefined && this.idsube == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "KullaniciListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_KULLANICITIPI;
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
        idkullanicitipilist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkullanicitipi() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});
Vue.component("c-kullanici-multi", {

    props: ['controller', 'idset', 'idsubelist', 'idkullanicitipilist', 'idsube', 'idkullanicitipi'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Kişi </label>
                    <div class ="col-md-9">                        
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" :style="'font-weight:'+[u.TCKIMLIKNO == '0' ? 'bold' : '']">{{u.ADSOYAD}}</option>
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
            tumu: false,
            ilkYukleme: true,
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
                ID_KULLANICITIPILIST: JSON.stringify(this.idkullanicitipilist),
                ID_SUBE: this.idsube,
                ID_KULLANICITIPI: this.idkullanicitipi,
            };

            if ((this.idkullanicitipilist != undefined && this.idkullanicitipilist.length == 0)
                || (this.idsubelist != undefined && this.idsubelist.length == 0)
                || (this.idkullanicitipi != undefined && this.idkullanicitipi == 0)
                || (this.idsube != undefined && this.idsube == 0)
            ) {
                return;
            }

            if (this.sonArama == JSON.stringify(p)) { return; }
            this.sonArama = JSON.stringify(p);

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "KullaniciListele", p, '', '', function (data, parent) {

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
        idkullanicitipilist() {
            this.GetData();
        },
        idsube() {
            this.GetData();
        },
        idkullanicitipi() {
            this.GetData();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});


//----------------------------------------------------------------------------------------------
//Performans Öğretmen Listele Componentleri
//----------------------------------------------------------------------------------------------


Vue.component("c-ogretmenlistesi", {

    props: ['controller', 'id_periyot'],
    template: `
                 <div class ="form-md-line-input">
                    <div class="row col-md-12">
                     <label class="control-label col-md-5">
                            Öğretmen Listesi
                     </label>
                    <div class ="col-md-6" name="ogretmen"> 
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Öğretmen Seçiniz...">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_OGRETMEN">{{u.OGRETMENAD}}</option>   
                        </select>
                    </div> 
                     <div class="col-md-6">
                    <input type="checkbox" id="checkbox" v-model="checked" @click="Listele()">
                     <label for="checkbox">Değerlendirilenler</label>
                    </div>
                   </div>
                </div>
                   
        `
    ,
    data: function () {
        return {
            SelectedID: 0,
            Liste: [],
            checked: false
        }
    },
    methods: {
        OnChange: function () {
            this.$emit('onchange', this.SelectedID)

        },
        Listele: function () {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_PERIYOT: this.id_periyot, DEGERLENEN: this.checked };

            WebPost(this, this.controller, "OgretmenListele", p, '', '', function (data, parent) {
                parent.Liste = data;

                parent.SelectedID = 0;
                parent.$emit('onchange', parent.SelectedID)
            });
        }
    },
    mounted() {
        this.Listele();

    },
    updated() {
        $('.selectpicker').selectpicker('refresh');

    }

});

//----------------------------------------------------------------------------------------------
//Performans Kategori Listele Componentleri
//----------------------------------------------------------------------------------------------

Vue.component("c-kategorilistesi", {

    props: ['controller', 'id_ogretmen'],
    template: `
                 <div class ="form-md-line-input">
                    <label class="control-label col-md-4">
                            Kategori Listesi
                     </label>
                    <div class ="col-md-7">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Kategori Seçiniz...">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_DEGERLENDIRMEKATEGORITANIM">{{u.KATEGORI}}</option>              
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
        }
    },
    mounted() {
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "KategoriListele", p, '', '', function (data, parent) {
            parent.Liste = data;
            parent.$emit('onchange', parent.SelectedID)
        })
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        id_ogretmen() {
            this.SelectedID = 0;
        },
    },

});

//----------------------------------------------------------------------------------------------
//Performans Degerlendirme Listele Componentleri
//----------------------------------------------------------------------------------------------

Vue.component("c-degerlendirmelistesi", {

    props: ['controller', 'disabled'],
    template: `
                 <div class ="form-md-line-input">
                    <label class="control-label col-md-5">
                            Değerlendirme Tarihi
                     </label>
                    <div class ="col-md-7">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" :disabled="disabled" title="Değerlendirme Tarihi Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_DEGERLERDIRMEPERIYOD">{{u.TARIH}}</option>              
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
        WebPost(this, this.controller, "PeriyotTarihListele", p, '', '', function (data, parent) {
            parent.Liste = data;
            for (var i = 0; i < parent.Liste.length; i++) {
                if (parent.Liste[i].ID_DEGERLERDIRMEPERIYOD) {
                    parent.SelectedID = parent.Liste[i].ID_DEGERLERDIRMEPERIYOD;
                    parent.$emit('onchange', parent.SelectedID)
                }
            }
        })
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});
Vue.component("c-tarih", {
    props: ['tarih', 'lbl', 'trhid'],

    template: `
                    <div class="form-md-line-input">
                            <label class ="control-label col-md-3" style="padding-top: 5px;" v-if="lbl==''">Tarih </label>
                            <label class ="control-label col-md-3" style="padding-top: 5px;" v-else>{{lbl}}</label>
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

    data: {
        tarihid: "",
        trhid: "",
        lbl: "",
    },
    created() {
        //this.tarihid = Math.random().toString(36).substr(2, 9);
        this.tarihid = (this.trhid != "" && this.trhid != undefined)
            ? this.trhid
            : Math.random().toString(36).substr(2, 9);
    },

    mounted() {
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
                //this.tarihh = new Date(year1, month1, day1);
                this.$emit('change-date', year1 + '-' + month1 + '-' + day1, this.tarihid);
            }
        );



        this.$nextTick(function () {
            try {
                $('#' + this.tarihid).datepicker('setDate', this.tarih == '' || this.tarih == undefined ? new Date() : strToDate(this.tarih));
            } catch (e) {
                $('#' + this.tarihid).datepicker('setDate', new Date());
            }
            $('#' + this.tarihid).datepicker('update');
        });
    },

    methods: {},

    updated() {
        $('#' + this.tarihid).datepicker('update');
    }
});


//Branş Öğretmen
Vue.component("c-brans", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Branş </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="DERSAD" @change="OnChange" title="Seçiniz..." >
                            <option v-for="u in Liste" v-bind:value="u.DERSAD"  v-bind:data-subtext="u.DERSAD">{{u.DERSAD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            DERSAD: '0',
            Liste: []
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.DERSAD)
        },

        GetData() {
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "BransListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.DERSAD = parent.Liste[0].DERSAD;
                        parent.OnChange();
                    }
                }
            });
        }
    },

    mounted() {
        this.GetData();
        this.DERSAD = "0";
    },

    watch: {
        idset() {
            this.DERSAD = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        },
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});


//Eğitim Türü
Vue.component("c-egitim-turu-multi", {

    props: ['controller', 'idset'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Eğitim Türü </label>
                    <div class ="col-md-9">                        
                        <select multiple="true" class ="selectpicker form-control select2"  v-model="SelectedID" @change="OnChange" data-live-search="true" title="Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_EGITIMTURU">{{u.EGITIMTURU}}</option>
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
            tumu: false,
            ilkYukleme: true,
        }
    },
    methods: {
        OnChange() {

            if (this.tumu == false && this.SelectedID.indexOf(0) > -1) { // tümü seçilince
                this.tumu = true;
                ListeTemizle(this.SelectedID);
                for (i = 0; i < this.Liste.length; i++) {
                    this.SelectedID.push(this.Liste[i].EGITIMTURU);
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
                    this.SelectedID.push(this.Liste[i].EGITIMTURU);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        GetData() {
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
            };

            ListeTemizle(this.Liste);
            WebPost(this, this.controller, "EgitimTuruListesi", p, '', '', function (data, parent) {

                parent.Liste = JSON.parse(data);
                if (data != "[]") {
                    parent.Liste.unshift({ ID_EGITIMTURU: 0, EGITIMTURU: 'Tümünü Seç' });
                }
                if (parent.Liste.length == 1) {
                    parent.SelectedID = parent.Liste[0].ID_EGITIMTURU;
                    parent.OnChange();
                }

                try {
                    if (parent.ilkYukleme && (parent.idset != undefined && parent.idset != null)) {
                        parent.SelectedID = JSON.parse(JSON.stringify(parent.idset));
                        parent.OnChange();
                    }
                    parent.ilkYukleme = false;
                } catch (e) { }
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
        }

    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});


//Rehber Öğretmen
Vue.component("c-ders-personel", {
    props: ['controller', 'idset', 'id_sinif', 'id_kullanici_tipi', 'label'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">{{label}} </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." >
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" >{{u.ADSOYAD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            TCKIMLIKNO: '',
            Liste: [],
            SelectedID: '0'
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        GetData() {
            if (this.id_sinif >0) {
                var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: this.id_sinif, ID_KULLANICITIPI: this.id_kullanici_tipi };
                WebPost(this, this.controller, "SinifPersonel", p, '', '', function (data, parent) {
                    if (data != null) {
                        parent.Liste = JSON.parse(data);

                        if (parent.Liste.length > 0) {

                            var selected = parent.Liste.find(x => x.SELECTED == 1)
                            parent.SelectedID = selected.TCKIMLIKNO;
                            parent.OnChange();
                        }
                    }
                });
            }
            
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
        id_sinif() {
            this.GetData();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});



Vue.component("c-egitim-turu", {

    props: ['controller', 'idset'],

    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Eğitim Türü </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Eğitim Türü Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_EGITIMTURU">{{u.EGITIMTURU}}</option>
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
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {

            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "EgitimTuruListesi", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        }
    },

    watch: {
        idset() {
            this.SelectedID = (this.idset == undefined || this.idset == null) ? 0 : this.idset;
            this.OnChange();
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Öğretmen Listele 
//----------------------------------------------------------------------------------------------

Vue.component("c-ogretmenlist", {

    props: ['controller', 'idsinif', 'idsube', 'donem','idset','id_ogretmen'],

    template: `
            
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Ders Öğretmenleri </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Öğretmen Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_OGRETMEN">{{u.AdSoyad}}</option>
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
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },

        Yenile() {
            //var tc = (this.tcogretmen != undefined && this.tcogretmen != '') ? this.tcogretmen : session.TCKIMLIKNO;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINIF: 0, ID_SUBE: this.idsube, DONEM: this.donem,ID_DERS:0};
            WebPost(this, this.controller, "OgretmenListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
               
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
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Kitap Okuma Grup Seçiniz Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-grup-single", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Grup Seçiniz</label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID"  @change="OnChange" title="Seçiniz..." :disabled="disabled">
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE"  v-bind:data-subtext="u.SUBENO">{{u.AD}}</option>
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
            ListeTemizle(this.Liste)
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "SubeListele", p, '', '', function (data, parent) {
                if (data != null) {
                    parent.Liste = JSON.parse(data);
                    if (parent.Liste.length == 1) {
                        parent.SelectedID = parent.Liste[0].ID_SUBE;
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
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
});

