//----------------------------------------------------------------------------------------------
//Kulüp Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kulup", {

    props: ['controller', 'id_kulup', 'kotali', 'ogrenci_tc', 'id_periyot'],

    template: `
                <div class="form-md-line-input">
                    <div class="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true">
                            <option value="0">Kulüp Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KULUP">{{u.KULUPADI}}</option>
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
        Listele() {
            //this.SelectedID = this.id_kulup;
            var p = {
                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                KOTALI: this.kotali,
                OGRENCI_TC: this.ogrenci_tc ?? session.TCKIMLIKNO,
                KulupPeriyotId: this.id_periyot
            };
            WebPost(this, this.controller, "KulupListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    mounted() {
        this.Listele();
    },


    updated() {
        $('.selectpicker').selectpicker('refresh');
    },
    watch: {
        id_kulup() {
            this.Listele();
        },
        ogrenci_tc() {
            this.Listele();
        },
    },

});
//----------------------------------------------------------------------------------------------
//Donem Listesi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-periyot", {

    props: ['controller'],

    template: `
                <div class="form-md-line-input">

                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Periyot Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KULUPPERIYOT" >{{u.PERIYOT}}</option>
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
        WebPost(this, this.controller, "Periyot", p, '', '', function (data, parent) {
            parent.Liste = data;            
            for (var i = 0; i < parent.Liste.length; i++) {
                //console.log("ID_KULUPPERIYOT=" + parent.Liste[i].ID_KULUPPERIYOT);
                //console.log("BAS_TARIH=" + parent.Liste[i].BAS_TARIH);
                //if (parent.Liste[i].AKTIF) {
                //parent.SelectedID = parent.Liste[i].ID_KULUPPERIYOT;
                    parent.$emit('onchange', parent.SelectedID)
                //}
            }
        })
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});


Vue.component("c-kademe3-multi", {
    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idsube', 'idkademe', 'disabled', 'danisman'],
    template: `
                
                 <div class="form-md-line-input">
                   <div class ="col-md-2">
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


Vue.component("c-sube-multi", {
    props: ['controller', 'idset', 'disabled'],
    template: `
                <div class="form-md-line-input">                  
                   <div class ="col-md-2">
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


Vue.component("c-sinif-multi", {
    props: ['controller', 'idset', 'idsubelist', 'idkademelist', 'idkademe3list', 'idsinifalanlist', 'idsube', 'idkademe', 'idkademe3', 'idders', 'idderslist', 'dersvarsasadecekur', 'disabled', 'danisman', 'idsinifalan', 'donem'],
    template: `
                <div class="form-md-line-input">                  
                   <div class ="col-md-2">
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