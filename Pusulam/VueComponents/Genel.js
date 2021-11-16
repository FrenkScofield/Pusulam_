//----------------------------------------------------------------------------------------------
//Menü Componenti Test İçin Yapıldı
//----------------------------------------------------------------------------------------------
Vue.component("c-menu", {

    props: ['controller'],
    template: `

                <div class="form-group form-md-line-input">
                    <label class ="control-label col-md-3">Menü Listesi </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" data-live-search="true" multiple>
                            <option v-for="u in Liste" v-bind:value="u.ID_MENU" v-bind:data-subtext="u.KOD">{{u.AD}}</option>
                        </select>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            SelectedID: ['0'],
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
        WebPost(this, this.controller, "MenuGetir", p, '', '', function (data, parent) {
            parent.Liste = data;
        })
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }

});

//----------------------------------------------------------------------------------------------
//Yarıyıl Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-yariyil", {
    props: ['tumu'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Yarıyıl Seçiniz...">
                            <option value="0" v-if="tumu!=undefined">{{tumu}}</option>
                            <option value="1">1. DÖNEM</option>
                            <option value="2">2. DÖNEM</option>
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
//Ders Componenti (GENEL DERSLER)
//----------------------------------------------------------------------------------------------
Vue.component("c-genel-ders", {
    props: ['idkademe3', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz...">
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

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, DONEM: this.donem };
            WebPost(this, 'DersUnite', "GenelDersListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    watch: {
        idkademe3() {
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
//Tablo Componenti 
//----------------------------------------------------------------------------------------------
Vue.component("c-tablo", {
    props: ['tabloadi', 'gelendata', 'istisna'],
    name: this.tabloadi,
    template: `
                    <table :id="tabloadi">
                        <thead id="c-tablo-header">
                            <tr>
                                <th v-for="col in columns">{{col}}</th>
                            </tr>
                        </thead>
                        <tbody id="tb">
                            <tr v-for="row in rows" v-on:click="OnChange(row)" style="cursor:pointer">
                                <td v-for="col in columns" v-on:click="OnChangeTd(row,col)"><span v-html="row[col]"></span></td>
                            </tr>
                        </tbody>
                    </table>
        `
    ,
    data: function () {
        return {
            rows: [],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);

            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Tablo Componenti 
//----------------------------------------------------------------------------------------------
Vue.component("f-tablo", {
    props: ['tabloadi', 'gelendata', 'istisna'],
    name: this.tabloadi,
    template: `
                <div style="width:100%; overflow-x:auto; overflow-y:auto; max-height:300px;">
                    <table :id="tabloadi">
                        <thead id="c-tablo-header">
                            <tr>
                                <th v-for="col in columns">{{col}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="row in rows" v-on:click="OnChange(row)" style="cursor:pointer">
                                <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)"><span v-html="row[col]"></span></td>
                            </tr>
                        </tbody>
                    </table>
                </div>


        `
    ,
    data: function () {
        return {
            rows: [],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            //divFixedTableClear();
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
                this.$nextTick(function () {
                    //fixedTableCreateID(this.tabloadi);
                });
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-tablo-etut", {
    props: ['tabloadi', 'gelendata', 'istisna'],
    name: this.tabloadi,
    template: `
                <table :id="tabloadi">
                    <thead id="c-tablo-header">
                        <tr>
                            <th v-for="col in columns">{{col}}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="row in rows" v-on:click="OnChange(row)" style="cursor:pointer">
                            <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)">
                                <span v-if="row[col].indexOf('%')>-1 && row[col].replace('%','')<50" v-html="row[col]" class ="badge badge-warning"></span>
                                <span v-else-if="row[col].indexOf('%')>-1" v-html="row[col]" class ="badge" ></span>
                                <span v-else v-html="row[col]"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
        `
    ,
    data: function () {
        return {
            rows: [],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-tablo-multi", {
    props: ['tabloadi', 'gelendata', 'istisna', 'satir', 'arama'],
    name: this.tabloadi,
    template: `
                <div id="pageTable" v-if="rows.length>0">  
                    <div  class="pagination" v-if="arama==true">
                        <input type="textbox" id="txtSearch" v-model="search" v-on:keyup="fnSearch" />   
                    </div>
                    <table :id="tabloadi">
                        <thead>
                            <tr>
                                
                                <th v-for="col in columns" v-on:click="sortTable(col)">{{col}}
                                   <div class="arrow" v-if="col == sortColumn" v-bind:class="[ascending ? 'arrow_up' : 'arrow_down']"></div>
                                 </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="row in get_rows()" v-on:click="OnChange(row)" style="cursor:pointer">
                                <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)"><span v-html="row[col]"></span></td>
                            </tr>                           
                        </tbody>
                    </table>
                    <div class="pagination" v-if="elementsPerPage<rows.length">
                        <div class="number"
                            v-for="i in num_pages()"
                            v-bind:class="[i == currentPage ? 'active' : '']"
                            v-on:click="change_page(i)">{{i}}</div>
                    </div>
                </div>              
            `
    ,
    data: function () {
        return {
            rows: [],
            currentPage: 1,
            elementsPerPage: 10,
            ascending: false,
            sortColumn: '',
            search: '',
            rows3: []
        }
    },
    created() {
        this.rows3 = JSON.parse(JSON.stringify(this.gelendata));

        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
        if (this.satir > 0) {
            this.elementsPerPage = this.satir;
        }

    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
        "sortTable": function sortTable(col) {
            if (this.sortColumn === col) {
                this.ascending = !this.ascending;
            } else {
                this.ascending = true;
                this.sortColumn = col;
            }

            var ascending = this.ascending;

            this.rows.sort(function (a, b) {
                if (a[col] > b[col]) {
                    return ascending ? 1 : -1
                } else if (a[col] < b[col]) {
                    return ascending ? -1 : 1
                }
                return 0;
            })
        },
        "num_pages": function num_pages() {
            return Math.ceil(this.rows.length / this.elementsPerPage);
        },
        "get_rows": function get_rows() {
            var start = (this.currentPage - 1) * this.elementsPerPage;
            var end = start + this.elementsPerPage;
            return this.rows.slice(start, end);
        },
        "change_page": function change_page(page) {
            this.currentPage = page;
        },
        "fnSearch": function fnSearch() {
            var search = this.search;
            var kk = Object.keys(this.gelendata[0]);
            var rows2 = [];
            $.each(this.gelendata, function (n, el) {
                var isOk = false;
                $.each(kk, function (j, sel) {
                    if (el[sel].toString().toUpperCase().indexOf(search.toUpperCase()) > -1) {
                        isOk = true;
                        return false;
                    }
                });
                if (isOk == true) {
                    rows2.push(el);
                }
            });
            if (rows2.length == 0) {
                rows2.push(JSON.parse(JSON.stringify(this.gelendata[0])))
                $.each(kk, function (j, sel) {
                    rows2[0][sel] = null;
                });
            }
            this.rows = rows2;
            this.currentPage = 1;
        },
    },
    watch: {
        gelendata(oldVal, newVal) {
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-tablo-frekans", {
    props: ['tabloadi', 'gelendata', 'istisna', 'isfixed'],
    name: this.tabloadi,
    template: `
                <table :id="tabloadi" class="overflow-y overflow-x fixed-table">
                    <thead id="thead">
                        <tr>
                            <th v-for="col in columns">{{col}}</th>
                        </tr>
                    </thead>
                    <tbody id="tbody">
                        <tr v-for="row in rows" v-on:click="OnChange(row)" style="cursor:pointer">
                            <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)">
                                <span v-if="String(row[col]).indexOf('-')==0"   v-html="row[col]" class ="badge badge-danger"   ></span>
                                <span v-else                                    v-html="row[col]"                               ></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
        `
    ,
    data: function () {
        return {
            rows: [],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            //divFixedTableClear();
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
                this.$nextTick(function () {
                    fixedTableCreateID(this.tabloadi);
                });
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-tablo-check", {
    props: ['tabloadi', 'gelendata', 'istisna'],
    name: this.tabloadi,
    //v-on:click="OnChange(row)"
    template: `
                <table :id="tabloadi">
                    <thead id="c-tablo-header">
                        <tr>
                            <th v-for="col in columns">{{col}}</th>
                            <th>Seç</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(row,index) in rows"  style="cursor:pointer">
                            <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)">
                                <span v-if="row[col].indexOf('%')>-1 && row[col].replace('%','')<50" v-html="row[col]" class ="badge badge-warning"></span>
                                <span v-else-if="row[col].indexOf('%')>-1" v-html="row[col]" class ="badge" ></span>
                                <span v-else v-html="row[col]"></span>
                            </td>
                            <td>
                                <div style="height:34px; vertical-align:middle;">
                                    <div class="md-checkbox" style="margin:auto!important; height:20px; margin:0px; margin-top:6px; display:block;">
                                        <input type="checkbox" @change="chkChanged" :name="row.KOD" :id="'chk_'+index+'_'+row.KOD" class="md-check" v-bind:true-value="true" v-bind:false-value="false" />
                                        <label :for="'chk_'+index+'_'+row.KOD">
                                            <span></span>
                                            <span class="check"></span>
                                            <span class="box" style ="border-color:white !important"></span>
                                        </label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
        `
    ,
    data: function () {
        return {
            rows: [],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        chkChanged() {
            var selected = [];
            $('input:checked').each(function () {
                selected.push($(this).attr('name'));
            });
            this.$emit('onchangechk', selected)
        },

        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

Vue.component("c-tablo-ogr-check", {
    props: ['tabloadi', 'gelendata', 'istisna'],
    name: this.tabloadi,
    //v-on:click="OnChange(row)"
    template: `
                <table :id="tabloadi">
                    <thead id="c-tablo-header">
                        <tr>
                            <th v-for="col in columns">{{col}}</th>
                            <th>Seç</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(row,index) in rows"  style="cursor:pointer">
                            <td v-for="col in columns"  v-on:click="OnChangeTd(row,col)">
                                <span v-html="row[col]"></span>
                            </td>
                            <td>
                                <div style="height:34px; vertical-align:middle;">
                                    <div class="md-checkbox" style="margin:auto!important; height:20px; margin:0px; margin-top:6px; display:block;">
                                        <input  type="checkbox" 
                                                class="md-check" 
                                                @change="chkChanged" 
                                                :name="row.TCKIMLIKNO" 
                                                :id="'chk_'+index+'_'+row.TCKIMLIKNO" 
                                                v-model="row.SECILI" 
                                                v-bind:true-value="true" 
                                                v-bind:false-value="false" />
                                        <label :for="'chk_'+index+'_'+row.TCKIMLIKNO">
                                            <span></span>
                                            <span class="check"></span>
                                            <span class="box" style ="border-color:white !important"></span>
                                        </label>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
        `
    ,
    data: function () {
        return {
            rows: [],
            //selected:[],
        }
    },
    computed: {
        "columns": function columns() {
            if (this.rows.length == 0) {
                return [];
            }
            var kk = Object.keys(this.rows[0]);
            if (this.istisna.length > 0) {
                var _this = this;
                $.each(this.istisna, function (i, el) {
                    if (kk.indexOf(el) > -1)
                        kk.splice(kk.indexOf(el), 1);
                });
            }
            return kk;
        }
    },
    methods: {
        OnChange(item) {
            this.$emit('onchange', item)
        },
        chkChanged() {
            var selected = [];
            $('input:checked').each(function () {
                selected.push($(this).attr('name'));
            });
            this.$emit('onchangechk', selected)
        },

        OnChangeTd(row, col) {
            var item = [];
            item.push(row);
            item.push(col);
            this.$emit('onchangetd', item)
        },
    },
    created() {
        if (this.gelendata != undefined) {
            this.rows = this.gelendata;
        }
    },
    watch: {
        gelendata(oldVal, newVal) {
            if (this.gelendata != undefined) {
                this.rows = this.gelendata;
            }
        },
    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    }
});

//----------------------------------------------------------------------------------------------
//Ders Componenti (GENEL DERSLER)
//----------------------------------------------------------------------------------------------
Vue.component("c-genel-ders-multi", {
    props: ['controller', 'idkademe3', 'donem'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Ders Seçiniz..." multiple="multiple">
                            <option v-for="u in Liste" v-bind:value="u.ID_DERS">{{u.AD}}</option>
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
                    this.SelectedID.push(this.Liste[i].ID_DERS);
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
                    this.SelectedID.push(this.Liste[i].ID_DERS);
                }
                this.$emit('onchange', this.SelectedID);
            }
        },

        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_KADEME3: this.idkademe3, DONEM: this.donem };
            WebPost(this, this.controller, "GenelDersListele", p, '', '', function (data, parent) {
                parent.Liste = [];
                parent.Liste.push({ ID_DERS: 0, AD: 'Tüm Dersler' })
                $.each(data, function (j, el) {
                    parent.Liste.push({ ID_DERS: el.ID_DERS, AD: el.AD })
                });
            })
        }
    },

    watch: {
        idkademe3() {
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
//Rapor Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-rapor-tur", {
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Rapor Türü Seçiniz...">
                            <option value="PDF">Pdf</option>
                            <option value="XLS">Xls</option>
                            <option value="XLSX">Xlsx</option>
                            <option value="HTML">Html</option>
                        </select>
                    </div>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: ""
        }
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        }
    }
});

Vue.component("c-rapor-tur-buton", {
    props: ['colmd', 'disabled'],
    template: `
                <div class="form-md-line-input">
                    <div  v-bind:class="[colmd == undefined ? 'col-md-2' : 'col-md-'+colmd]">
                        <select class ="selectpicker form-control" v-model="SelectedID"  title="Çıktı Türü Seçiniz..." :disabled="disabled">
                            <option value="PDF">Pdf</option>
                            <option value="XLS">Xls</option>
                            <option value="XLSX">Xlsx</option>
                            <option value="HTML">Html</option>
                        </select>
                    </div>
                    <input type="button" class ="btn btn-success" @click="Yazdir" value="Yazdır" :disabled="disabled"/>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: ""
        }
    },
    methods: {
        //OnChange() { //@change="OnChange"
        //    this.$emit('onchange', this.SelectedID)
        //},
        Yazdir() {
            this.$emit('yazdir', this.SelectedID)
        }
    }
});

Vue.component("c-rapor-tur-buton-yazi", {
    props: ['butonyazi'],
    template: `
                <div class="form-md-line-input">
                    <div class="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Çıktı Türü Seçiniz...">
                            <option value="PDF">Pdf</option>
                            <option value="XLS">Xls</option>
                            <option value="XLSX">Xlsx</option>
                            <option value="HTML">Html</option>
                        </select>
                    </div>
                    <input type="button" class ="btn btn-success" @click="Yazdir"  v-bind:value="butonyazi2"/>
                </div>
        `
    ,

    data: function () {
        return {
            SelectedID: "",
            butonyazi2: "",
        }
    },
    created() {
        this.butonyazi2 = this.butonyazi == undefined ? "Yazdır" : this.butonyazi;
    },
    methods: {
        OnChange() {
            this.$emit('onchange', this.SelectedID)
        },
        Yazdir() {
            this.$emit('yazdir', this.SelectedID)
        }
    },
});

//----------------------------------------------------------------------------------------------
//Rapor Türü Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-siralama", {
    props: ['controller', 'idsinavturu'],
    template: `
                <div class="form-md-line-input">
                    <div class ="col-md-2">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" id="cmbPuanTuru" title="Sıralama Kriterini Seçiniz...">
                            <option v-for="u in Liste" v-bind:value="u.ID_SINAVPUANTURU">{{u.AD}}</option>
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
        Yenile() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_SINAVTURU: this.idsinavturu };
            WebPost(this, this.controller, "PuanTuruListebyOgrenci", p, '', '', function (data, parent) {
                data.unshift({ ID_SINAVPUANTURU: 0, AD: "Net", KOD: "NET" });
                parent.Liste = data;
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
        idsinavturu() {
            this.Yenile();
        }
    },
});

//----------------------------------------------------------------------------------------------
//Tarih Seçici Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-tarih", {
    props: ['controller', 'tarih'],

    template: `
                <div class ="form-md-line-input">
                    <div class ="col-md-2">
                        <div class ="dropdown-toggle btn grey-cararra" data-toggle="dropdown" role="button" title="Tarih Seçiniz.." style="width:100%; padding:0px;">
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
//Sınıf Öğrenci Componenti (Multi Sınıf)
//----------------------------------------------------------------------------------------------
Vue.component("c-ogrenci-multisinif", {
    props: ['controller', 'idsiniflist'],

    template: `
                <div class="form-md-line-input">
                    <label class ="control-label col-md-3">Öğrenci </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO">{{u.AD + " " + u.SOYAD}}</option>
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
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, SINIFLAR: JSON.stringify(this.idsiniflist) };
            WebPost(this, this.controller, "OgrenciListele", p, '', '', function (data, parent) {
                parent.Liste = data;
            })
        }
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    watch: {
        idsiniflist() {
            this.Yenile();
        }
    },
});



Vue.component('c-accordion', {

    props: ['controller', 'name', 'accordionid'],

    template: `
    <div>
        <div class="portlet box green-haze dd-item" style="border:none">
            <div class="portlet-title dd-handle" data-toggle='collapse' :data-target="'#filters'+accordionid" style="cursor:pointer;">
                <div class="caption">
                    <i class="icon-magnifier"></i>
                    <span class="caption-subject bold uppercase"> {{name}} </span>
                    <span class="caption-helper"></span>
                </div>
                <div class='actions'>
                    <a href='javascript:;' class='btn btn-default btn-sm' style='border:none;'>
                        <i class='fa fa-chevron-down'></i>
                    </a>
                </div>
            </div>
            <div class="portlet-body form collapse in" :id="'filters'+accordionid">
                <form role="form" style="padding:12px 20px">
                    <div class="form-body form-group row" style="line-height:34px;">
                        <slot></slot>
                    </div>
                </form>
            </div>
        </div>

    </div>`,

    data: function () {
        return {}
    },

    methods: {},

    updated() { },

    watch: {},

})

//----------------------------------------------------------------------------------------------
//Yetkili Olduğu Kademeler Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-kademe-yetki", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height:32px;">
                    <label class="control-label col-md-2" style="vertical-align:middle;">Kademe </label>
                    <div class="col-md-10">
                        <select class="form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_KADEME">{{u.AD}}</option>
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
        var parent = this;
        var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
        WebPost(this, this.controller, "KademeYetkiListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    }
});

//----------------------------------------------------------------------------------------------
//Müdür Olduğu Şubeler Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-mudur-sube", {
    props: ['controller'],
    template: `
                <div class="form-md-line-input" style="line-height:32px;">
                    <label class="control-label col-md-2" style="vertical-align:middle;">Şube </label>
                    <div class="col-md-10">
                        <select class="form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_SUBE">{{u.AD}}</option>
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
        WebPost(this, this.controller, "MudurSubeListele", p, '', '', function (data, parent) {
            parent.Liste = JSON.parse(data);
        })
    }
});
//----------------------------------------------------------------------------------------------
//Genel Kurul Periyot Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-genel-kurul-periyot", {
    props: ['controller', 'donem'],
    template: `
                <div class="form-md-line-input" style="line-height:32px;">
                    <label class="control-label col-md-2" style="vertical-align:middle;">Periyot </label>
                    <div class="col-md-10">
                        <select class="form-control" v-model="SelectedID" @change="OnChange">
                            <option value="0">Seçiniz..</option>
                            <option v-for="u in Liste" v-bind:value="u.ID_PERIYOT">{{u.BASLANGIC + ' - ' + u.BITIS}}</option>
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
            var parent = this;
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, DONEM: this.donem };
            WebPost(this, this.controller, "PeriyotListele", p, '', '', function (data, parent) {
                parent.Liste = JSON.parse(data);
            })
        }
    },
    watch: {
        donem() {
            this.Yenile();
        }
    }
});


