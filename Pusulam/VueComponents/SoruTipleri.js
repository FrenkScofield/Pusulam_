//----------------------------------------------------------------------------------------------
//Çözümler Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-cozumler", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <!--Çözümler Baslangic-->
                    <h3>Çözümler</h3>
                    <div v-for="(u,index) in sorupaket.CozumListesi">
                        <div class ="form-group form-md-line-input">
                            <label class ="control-label col-md-1">Çözüm {{index+1}}</label>
                            <div class ="col-md-2" v-if="u.ID_COZUM==0">
                                <select class ="selectpicker form-control" v-model="u.ID_COZUMTUR" @change="CozumYenile">
                                    <option v-for="c in CozumTurListesi" :value="c.ID_COZUMTUR">{{c.AD}}</option>
                                </select>
                            </div>
                            <div class ="col-md-1">
                                <a @click="CozumSil(index)">Çözüm Sil</a>
                            </div>
                        </div>
                        <div class ="form-group form-md-line-input" v-if="u.ID_COZUMTUR==22">
                            <div class ="col-md-1"></div>
                            <div class ="col-md-9">
                                <textarea :id="'EditorCozum-'+sorupaket.SORUNO+'-'+(index+1)">
                                    {{u.COZUMHTML}}
                                </textarea>
                            </div>
                        </div>
                        <div class ="form-group form-md-line-input" v-if="u.ID_COZUMTUR==23">
                            <div class ="col-md-1"></div>

                            <div class ="col-md-9">
                                <img :src="u.COZUMHTML" height="240px" width="340px" v-if="u.ID_SORU>0">
                                <div class ="fileinput fileinput-new" data-provides="fileinput"  v-if="u.ID_SORU==0">
                                    <div class ="fileinput-new thumbnail" style="width: auto; height: auto;">
                                        <img :src="u.COZUMHTML" alt="" />
                                    </div>
                                    <div class ="fileinput-preview fileinput-exists thumbnail" style="max-width: 400px; max-height: 400px;"> </div>
                                    <div>
                                        <span class ="btn default btn-file">
                                            <span class ="fileinput-new"> Resim Seç</span>
                                            <span class ="fileinput-exists"> Değiştir </span>
                                            <input type="file" name="..." :id="'Resim-'+sorupaket.SORUNO+'-'+u.SIRA">
                                        </span>
                                        <a href="javascript:;" class ="btn red fileinput-exists" data-dismiss="fileinput"> Sil </a>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>
                    <div class ="col-md-offset-9">
                        <a @click="CozumEkle">Yeni Çözüm Ekle</a>
                    </div>
                </div>
        `
    ,
    data: function () {
        return {
            CozumTurListesi: []
        }
    },

    mounted() {
        this.CozumTurGetir();
        this.CozumYenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    methods: {

        CozumTurGetir() {
            var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM };
            WebPost(this, this.controller, "CozumTurGetir", p, '', '', function (data, parent) {
                parent.CozumTurListesi = data;
            })
        },

        CozumSil(index) {
            this.sorupaket.CozumListesi.splice(index, 1);
            this.CozumYenile();
        },

        CozumEkle() {
            var vue = this;
            if (this.sorupaket.CozumListesi == null) this.sorupaket.CozumListesi = [];
            this.sorupaket.CozumListesi.push({ ID_SORU: 0, ID_COZUM: 0, ID_COZUMTUR: 22, COZUMHTML: '', SIRA: vue.sorupaket.CozumListesi.length + 1 });
            this.CozumYenile();
        },

        CozumYenile() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CozumListesi != null) {
                    vue.sorupaket.CozumListesi.forEach(function (val, index, arr) {
                        var editorID = 'EditorCozum-' + vue.sorupaket.SORUNO + "-" + (index + 1);
                        var editor = CKEDITOR.instances[editorID];
                        if (editor) {
                            editor.destroy(true);
                        }
                        if (val.ID_COZUMTUR == 22) {
                            CKEDITOR.replace(editorID, { height: '100px' });
                            //Editor değiştiğinde model güncellensin
                            CKEDITOR.instances[editorID].on('change', function () {
                                val.COZUMHTML = CKEDITOR.instances[editorID].getData();
                            });
                        }
                    })
                }
            })
        }
    }

});

//----------------------------------------------------------------------------------------------
//Doğru Yanlış Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-dogruyanlis", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    </br>
                    <!--Cevap Listesi Baslangic-->
                    <h3>Cevaplar</h3>
                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi">
                        <label class ="control-label col-md-1">{{u.CEVAPNO}}- </label>
                        <div class ="col-md-9">
                            <textarea v-bind:id="'EditorCevapDY-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                {{u.CEVAPHTML}}
                            </textarea>
                        </div>
                        <div class ="col-md-2">
                            <div class ="form-group form-md-radios">
                                <div class ="md-radio-list col-md-12">
                                    <div class ="md-radio">
                                        <input type="radio" :id="'rdDogru-'+sorupaket.SORUNO+'-'+u.CEVAPNO" :name="'radioGrup-'+sorupaket.SORUNO+'-'+u.CEVAPNO" class ="md-radiobtn" value="1" v-model="u.DEGER">
                                        <label :for="'rdDogru-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                            <span class ="inc"></span>
                                            <span class ="check"></span>
                                            <span class ="box"></span> Doğru
                                        </label>
                                    </div>
                                    <div class ="md-radio">
                                        <input type="radio" :id="'rdYanlis-'+sorupaket.SORUNO+'-'+u.CEVAPNO" :name="'radioGrup-'+sorupaket.SORUNO+'-'+u.CEVAPNO" class ="md-radiobtn" value="0" v-model="u.DEGER">
                                        <label :for="'rdYanlis-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                            <span></span>
                                            <span class ="check"></span>
                                            <span class ="box"></span> Yanlış
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--Cevap Listesi Bitis-->
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
            CEVAPSAYISI: 2
        }
    },

    mounted() {
        var vue = this;
        var editorName = "EditorSoru-" + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorName);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorName].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorName].getData();
        });
        //Ekleme ise cevap oluştur, düzenleme ise cevap yenile
        if (this.sorupaket.Soru.ID_SORU == 0) this.CevapOlustur();
        else this.CevapYenile();

    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    methods: {

        CevapOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi != null && vue.sorupaket.CevapListesi.length != vue.CEVAPSAYISI) {
                    vue.sorupaket.CevapListesi.splice(0, vue.sorupaket.CevapListesi.length);
                    for (var i = 0; i < vue.CEVAPSAYISI; i++) {
                        vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: (i + 1), CEVAPHTML: "", DEGER: 1 });
                    }
                    vue.CevapYenile();
                }
            })
        },

        CevapYenile() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi != null) {
                    vue.sorupaket.CevapListesi.forEach(function (val, index, arr) {
                        var editorID = 'EditorCevapDY-' + vue.sorupaket.SORUNO + '-' + (val.CEVAPNO);
                        var editor = CKEDITOR.instances[editorID];
                        if (editor) {
                            editor.destroy(true);
                        }
                        CKEDITOR.replace(editorID, { height: '100px' });
                        //Editor değiştiğinde model güncellensin
                        CKEDITOR.instances[editorID].on('change', function () {
                            val.CEVAPHTML = CKEDITOR.instances[editorID].getData();
                        });
                    })
                }
            })
        }

    }

});

//----------------------------------------------------------------------------------------------
//Test Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-test", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    <br />
                    <!--Cevap Listesi Baslangic-->
                    <h3>Cevaplar</h3>
                    <div class ="form-group form-md-line-input" v-if="sorupaket.Soru.ID_SORU==0">
                        <label class ="control-label col-md-1">Seçenek Sayısı </label>
                        <div class ="col-md-1">
                            <select class ="selectpicker form-control" v-model="CEVAPSAYISI" @change="CevapOlustur">
                                <option value=2>2</option>
                                <option value=3>3</option>
                                <option value=4>4</option>
                                <option value=5>5</option>
                            </select>
                        </div>
                        <div class ="col-md-8">
                            Seçenek Sayısı Değiştiğinde Cevap İçerikleri Sıfırlanır !
                         </div>
                    </div>
                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi">
                        <div class ="col-md-1">
                            <div class ="form-group form-md-radios">
                                <div class ="md-radio-list col-md-12">
                                    <div class ="md-radio">
                                        <input type="radio" :id="'rdTest-'+sorupaket.SORUNO+'-'+u.CEVAPNO" :name="'radioGrupTest-'+sorupaket.SORUNO" class ="md-radiobtn" value="1" v-model="u.DEGER" @click="RadioChanged(u)">
                                        <label :for="'rdTest-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                            <span class ="inc"></span>
                                            <span class ="check"></span>
                                            <span class ="box"></span> <b>{{SECENEKLER[u.CEVAPNO-1]}}</b>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class ="col-md-9">
                            <textarea v-bind:id="'EditorCevapTest-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                {{u.CEVAPHTML}}
                            </textarea>
                        </div>

                    </div>
                    <!--Cevap Listesi Bitis-->
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
            CEVAPSAYISI: 2,
            SECENEKLER: ['A', 'B', 'C', 'D', 'E']

        }
    },

    mounted() {
        var vue = this;
        var editorName = 'EditorSoru-' + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorName);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorName].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorName].getData();
        });
        //Ekleme ise cevap oluştur, düzenleme ise cevap yenile
        if (this.sorupaket.Soru.ID_SORU == 0) this.CevapOlustur();
        else this.CevapYenile();

    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    methods: {

        RadioChanged(cevap) {
            if (this.sorupaket.CevapListesi != null) {
                this.sorupaket.CevapListesi.forEach(function (val, index) {
                    val.DEGER = 0;
                })
            }
            cevap.DEGER = 1;
        },

        CevapOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi.length != vue.CEVAPSAYISI) {
                    vue.sorupaket.CevapListesi.splice(0, vue.sorupaket.CevapListesi.length);
                    for (var i = 0; i < vue.CEVAPSAYISI; i++) {
                        vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: (i + 1), CEVAPHTML: "", DEGER: "0" });
                    }
                    vue.CevapYenile();
                }
            })
        },

        CevapYenile() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi != null) {
                    vue.sorupaket.CevapListesi.forEach(function (val, index, arr) {
                        var editorID = 'EditorCevapTest-' + vue.sorupaket.SORUNO + '-' + (val.CEVAPNO);
                        var editor = CKEDITOR.instances[editorID];
                        if (editor) {
                            editor.destroy(true);
                        }
                        CKEDITOR.replace(editorID, { height: '100px' });
                        //Editor değiştiğinde model güncellensin
                        CKEDITOR.instances[editorID].on('change', function () {
                            val.CEVAPHTML = CKEDITOR.instances[editorID].getData();
                        });

                    })
                }
            })
        }

    }

});

//----------------------------------------------------------------------------------------------
//Açık Uçlu Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-acikuclu", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    <br />
                    <!--Cevap Listesi Baslangic-->
                    <h3>Cevap</h3>
                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi">
                        <div class ="col-md-10">
                            <input type="text" class ="form-control" v-model="u.DEGER" :id="'txtAcikUcluCevap-'+sorupaket.SORUNO" maxlength="10" />
                            <div class ="form-control-focus"> </div>
                            <span class ="help-block">10 Karakter Girebilirsiniz.</span>
                        </div>
                    </div>
                    <!--Cevap Listesi Bitis-->
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
        }
    },

    mounted() {
        var vue = this;
        var editorName = 'EditorSoru-' + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorName);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorName].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorName].getData();
        });
        //Ekleme ise cevap oluştur, düzenleme ise cevap yenile
        if (this.sorupaket.Soru.ID_SORU == 0) this.CevapOlustur();
    },

    methods: {

        CevapOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                vue.sorupaket.CevapListesi.splice(0, vue.sorupaket.CevapListesi.length);
                vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: 1, CEVAPHTML: "", DEGER: "" });
            })
        }

    }

});

//----------------------------------------------------------------------------------------------
//Boşluk Doldurma Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-boslukdoldurma", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    <br />
                    <!--Cevap Listesi Baslangic-->
                    <h3>Cevaplar</h3>
                    <div class ="form-group form-md-line-input" v-if="sorupaket.Soru.ID_SORU==0">
                        <label class ="control-label col-md-1">Boşluk Sayısı </label>
                        <div class ="col-md-1">
                            <select class ="selectpicker form-control" v-model="CEVAPSAYISI" @change="CevapOlustur">
                                <option v-for="n in 10" :value="n">{{n}}</option>
                            </select>
                        </div>
                        <div class ="col-md-8">
                            Seçenek Sayısı Değiştiğinde Cevap İçerikleri Sıfırlanır !
                         </div>
                    </div>
                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi">
                        <label class ="control-label col-md-1">{{u.CEVAPNO}}. Boşluk </label>
                        <div class ="col-md-9">
                            <input type="text" class ="form-control" v-model="u.DEGER" :id="'txtAcikUcluCevap-'+sorupaket.SORUNO" />
                            <div class ="form-control-focus"> </div>
                            <span class ="help-block"></span>
                        </div>
                    </div>
                    <!--Cevap Listesi Bitis-->
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
            CEVAPSAYISI: 2
        }
    },

    mounted() {
        var vue = this;
        var editorname = 'EditorSoru-' + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorname);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorname].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorname].getData();
        });
        //Ekleme ise cevap oluştur, düzenleme ise cevap yenile
        if (this.sorupaket.Soru.ID_SORU == 0) this.CevapOlustur();
    },

    methods: {

        CevapOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi.length != vue.CEVAPSAYISI) {
                    vue.sorupaket.CevapListesi.splice(0, vue.sorupaket.CevapListesi.length);
                    for (var i = 0; i < vue.CEVAPSAYISI; i++) {
                        vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: (i + 1), CEVAPHTML: "", DEGER: "" });
                    }
                }
            })
        }

    }

});

//----------------------------------------------------------------------------------------------
//Eşleştirme Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-eslestirme", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    <br />
                    <!--Cevap Listesi Baslangic-->
                    <h3>Cevaplar</h3>
                    <div class ="form-group form-md-line-input" v-if="sorupaket.Soru.ID_SORU==0">
                        <label class ="control-label col-md-1">Seçenek Sayısı </label>
                        <div class ="col-md-1">
                            <select class ="selectpicker form-control" v-model="CEVAPSAYISI" @change="CevapOlustur">
                                <option v-for="n in 10" :value="n">{{n}}</option>
                            </select>
                        </div>
                        <div class ="col-md-8">
                            Seçenek Sayısı Değiştiğinde Cevap İçerikleri Sıfırlanır !
                         </div>
                    </div>

                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi" v-if="u.DEGER!=''">
                        <div>
                            <label class ="control-label col-md-1"><b>{{u.CEVAPNO}}-</b> </label>
                            <div class ="col-md-10">
                                <textarea :id="'EditorCevapSol-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                    {{u.CEVAPHTML}}
                                </textarea>
                            </div>
                            <div class ="col-md-1">
                                <select class ="selectpicker form-control" v-model="u.DEGER">
                                    <option v-for="n in CEVAPSAYISI" :value="SECENEKLER[n]">{{SECENEKLER[n]}}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <br /><hr /><br />
                    <div class ="form-group form-md-line-input" v-for="u in sorupaket.CevapListesi" v-if="u.DEGER==''">
                            <label class ="control-label col-md-1"><b>{{SECENEKLER[u.CEVAPNO]}}-</b> </label>
                            <div class ="col-md-10">
                                <textarea :id="'EditorCevapSag-'+sorupaket.SORUNO+'-'+u.CEVAPNO">
                                    {{u.CEVAPHTML}}
                                </textarea>
                            </div>
                        </div>
                    <!--Cevap Listesi Bitis-->
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
            CEVAPSAYISI: 2,
            SECENEKLER: ['', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J']
        }
    },

    mounted() {
        var vue = this;
        var editorName = "EditorSoru-" + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorName);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorName].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorName].getData();
        });
        //Ekleme ise cevap oluştur, düzenleme ise cevap yenile
        if (this.sorupaket.Soru.ID_SORU == 0) this.CevapOlustur();
        else this.CevapYenile();

    },
    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    methods: {

        CevapOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi.length != vue.CEVAPSAYISI) {
                    vue.sorupaket.CevapListesi.splice(0, vue.sorupaket.CevapListesi.length);
                    for (var i = 0; i < vue.CEVAPSAYISI; i++) {
                        vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: (i + 1), CEVAPHTML: "", DEGER: vue.SECENEKLER[i + 1] });
                    }
                    for (var i = 0; i < vue.CEVAPSAYISI; i++) {
                        vue.sorupaket.CevapListesi.push({ ID_SORU: vue.sorupaket.Soru.ID_SORU, CEVAPNO: (i + 1), CEVAPHTML: "", DEGER: '' });
                    }
                    vue.CevapYenile();
                }
            })
        },

        CevapYenile() {
            var vue = this;
            Vue.nextTick(function () {
                if (vue.sorupaket.CevapListesi != null) {
                    vue.sorupaket.CevapListesi.forEach(function (val, index, arr) {
                        var editorID = 'EditorCevapSol-' + vue.sorupaket.SORUNO + '-' + (val.CEVAPNO);
                        if (index >= arr.length / 2) editorID = 'EditorCevapSag-' + vue.sorupaket.SORUNO + '-' + (val.CEVAPNO);
                        var editor = CKEDITOR.instances[editorID];
                        if (editor) {
                            editor.destroy(true);
                        }

                        CKEDITOR.replace(editorID, { height: '100px' });
                        //Editor değiştiğinde model güncellensin
                        CKEDITOR.instances[editorID].on('change', function () {
                            val.CEVAPHTML = CKEDITOR.instances[editorID].getData();
                        });

                    })
                }
            })
        }

    }

});

//----------------------------------------------------------------------------------------------
//Klasik Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-klasik", {

    props: ['controller', 'sorupaket'],
    template: `
                    <div>
                    <h3>Soru Metni</h3>

                    <textarea :id="'EditorSoru-'+sorupaket.SORUNO" v-for="u in sorupaket.SoruHtmlListesi" v-if="u.ID_TASLAKTUR==20">
                        {{u.SORUHTML}}
                    </textarea>
                    <br />
                     <c-soru-cozumler :controller="controller" :sorupaket="sorupaket">
                     </c-soru-cozumler>
                </div>
        `
    ,
    data: function () {
        return {
        }
    },

    mounted() {
        var vue = this;
        var editorName = 'EditorSoru-' + this.sorupaket.SORUNO;
        CKEDITOR.replace(editorName);
        //Editor değiştiğinde model güncellensin
        CKEDITOR.instances[editorName].on('change', function () {
            vue.sorupaket.SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorName].getData();
        });
    },

    methods: {

    }

});


//----------------------------------------------------------------------------------------------
//Paragraf Soru Tipi Componenti
//----------------------------------------------------------------------------------------------
Vue.component("c-soru-paragraf", {

    props: ['controller', 'sorupaketlist'],
    template: `
                    <div>
                        <h3>Paragraf Sorusu</h3>
                        <div class ="form-group form-md-line-input" v-if="sorupaketlist[0].Soru.ID_SORU<=0">
                            <label class ="control-label col-md-1">Soru Sayısı </label>
                            <div class ="col-md-1">
                                <select class ="selectpicker form-control" v-model="SORUSAYISI" @change="SoruOlustur">
                                    <option value=2>2</option>
                                    <option value=3>3</option>
                                    <option value=4>4</option>
                                    <option value=5>5</option>
                                    <option value=6>6</option>
                                    <option value=7>7</option>
                                    <option value=8>8</option>
                                </select>
                            </div>
                            <div class ="col-md-8">
                            Soru Sayısı Değiştiğinde Soru İçerikleri Sıfırlanır !
                            </div>
                        </div>

                       <div v-for="(u,index) in sorupaketlist">
                           <textarea id="EditorSoruParagraf" v-for="j in u.SoruHtmlListesi" v-if="u.Soru.ID_USTSORU==0 && j.ID_TASLAKTUR==20">
                               {{j.SORUHTML}}
                           </textarea>

                           <div v-if="u.Soru.ID_USTSORU!=0">
                                    <div class ="form-group form-md-line-input">
                                        <h1 class="col-md-1">{{index}}.Soru</h1>
                                        <div class ="margin-top-15 col-md-3">
                                            <c-soru-turu :controller="controller" :soru="u.Soru.ID_SORUTUR" @onchange="(...args)=>SoruTuruSelected(u,...args)"></c-soru-turu>
                                        </div>
                                    </div>

                                    <c-soru-klasik           v-if="u.Soru.ID_SORUTUR==1" :controller="controller" :sorupaket="u"></c-soru-klasik>
                                    <c-soru-acikuclu         v-if="u.Soru.ID_SORUTUR==2" :controller="controller" :sorupaket="u"></c-soru-acikuclu>
                                    <c-soru-dogruyanlis      v-if="u.Soru.ID_SORUTUR==3" :controller="controller" :sorupaket="u"></c-soru-dogruyanlis>
                                    <c-soru-test             v-if="u.Soru.ID_SORUTUR==4" :controller="controller" :sorupaket="u"></c-soru-test>
                                    <c-soru-eslestirme       v-if="u.Soru.ID_SORUTUR==5" :controller="controller" :sorupaket="u"></c-soru-eslestirme>
                                    <c-soru-boslukdoldurma   v-if="u.Soru.ID_SORUTUR==6" :controller="controller" :sorupaket="u"></c-soru-boslukdoldurma>
                                   </br><hr /></br>
                           </div>
                         </div>
                   </div>
        `
    ,
    data: function () {
        return {
            SORUSAYISI: 2
        }
    },

    mounted() {
        if (this.sorupaketlist == null || this.sorupaketlist[0].Soru.ID_SORU<=0) this.SoruOlustur();
        else this.SoruYenile();
    },

    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

    methods: {

        SoruTuruSelected(uSoruPaket, ID_SORUTUR) {
            uSoruPaket.Soru.ID_SORUTUR = ID_SORUTUR;
        },

        SoruOlustur() {
            var vue = this;
            Vue.nextTick(function () {
                vue.sorupaketlist.splice(0, vue.sorupaketlist.length);
                vue.sorupaketlist.push(
                        SoruPaket = {
                            SORUNO: 1,
                            CevapListesi: [],
                            CozumListesi: [],
                            Soru: {
                                ID_SORU: -1,
                                ID_USTSORU: 0,
                                ID_SORUTUR: 7,
                                KOD: "",
                                ID_SINAVGRUP: 0,
                                ID_DERS: 0,
                                KONU: "",
                                //ID_DERSUNITE: 0,
                                //ID_KONU: 0,
                                //ID_ALTKONU: 0,
                                //ID_KAZANIM: 0,
                                ID_ZORLUKTUR: 0
                            },
                            SoruHtmlListesi: [{ ID_SORU: 0, ID_TASLAKTUR: 20, SORUHTML: "" }]
                        }
                    );

                for (var i = 0; i < vue.SORUSAYISI; i++) {
                    vue.sorupaketlist.push(
                        SoruPaket = {
                            SORUNO: vue.sorupaketlist.length + 1,
                            CevapListesi: [],
                            CozumListesi: [],
                            Soru: {
                                ID_SORU: 0,
                                ID_USTSORU: -1,
                                ID_SORUTUR: 0,
                                KOD: "",
                                ID_SINAVGRUP: 0,
                                ID_DERS: 0,
                                KONU,
                                //ID_DERSUNITE: 0,
                                //ID_KONU: 0,
                                //ID_ALTKONU: 0,
                                //ID_KAZANIM: 0,
                                ID_ZORLUKTUR: 0
                            },
                            SoruHtmlListesi: [{ ID_SORU: 0, ID_TASLAKTUR: 20, SORUHTML: "" }]
                        }
                    )
                }
                vue.SoruYenile();
            })
        },

        SoruYenile() {
            var vue = this;
            Vue.nextTick(function () {
                var editorID = 'EditorSoruParagraf'
                var editor = CKEDITOR.instances[editorID];
                if (editor) {
                    editor.destroy(true);
                }
                CKEDITOR.replace(editorID);
                //Editor değiştiğinde model güncellensin
                CKEDITOR.instances[editorID].on('change', function () {
                    vue.sorupaketlist[0].SoruHtmlListesi[0].SORUHTML = CKEDITOR.instances[editorID].getData();
                });
            })
        }
    }

});