Vue.component('c-online-ogretmen-listesi', {
     template: `
                <div class ="form-md-line-input">
                    <label class ="control-label col-md-3" style="vertical-align:middle;">Öğretmen </label>
                    <div class ="col-md-9">
                        <select class ="selectpicker form-control" v-model="SelectedID" @change="OnChange" title="Seçiniz..." data-live-search="true">
                            <option v-for="u in Liste" v-bind:value="u.TCKIMLIKNO" >{{u.AD + ' '+ u.SOYAD}}</option>
                        </select>
                    </div>
                </div>
        `,
    data: function () {
        return {
            SelectedID: 0,
            Liste : []
        }
    },
    methods: {

        Listele() {
            var p = {

                TCKIMLIKNO: session.TCKIMLIKNO,
                OTURUM: session.OTURUM,
                ID_SUBE: this.idsube
            };
            //ListeTemizle(this.LISTE);
            WebPost(this, this.controller, "SinifOnlineDersOgretmenListele", p, '', '', function (data, parent) {

               

                if (data != '[]')
                    parent.Liste = JSON.parse(data);

            });
        },
        OnChange() {

            this.$emit('onchange', this.SelectedID)
        }

    },
    mounted() {
        this.Listele();

    },
    watch: {
        idsube() {
            this.Listele();
        }
    },
    props: {
        idsube: [String, Number],
        controller : ''
    },


    updated() {
        $('.selectpicker').selectpicker('refresh');
    },

})