$(document).ready(function () {
    $(window).resize(function () {
        try {
            $('#tablobaslik').bootstrapTable('resetView');
        } catch (e) {

        }
    })
});

var session = null;
if ($.cookie("K") != null)
    session = JSON.parse($.cookie("K"));

//CikisYap();

function updateProgress(evt) {
    if (evt.lengthComputable) {
        var percentComplete = evt.loaded / evt.total;
    }
    else {
    }
    App.blockUI({ message: parseInt(percentComplete * 100) + "% (Tamamlanan)" });
}


function YardimGoster(ID_MENU) {
    var p = JSON.stringify({ TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, ID_MENUYARDIM: ID_MENU });
    SqlWebAction("POST", "Menu", "MenuYardimGetir", p, '', '', function (data) {
        var YARDIMHTML = data;
        $('#span_yardim').html(YARDIMHTML != null ? YARDIMHTML : '<span>Yardım metni hazırlanmamış!</span>');
        $('#yardim_modal').modal();
    });
}

function getDistinct(data, key) {
    var distList = [];
    $.each(data, function (j, item) {
        if (distList.indexOf(item[key]) == -1) {
            distList.push(item[key]);
        }
    });
    return distList;
}

function getDistinctOnlyTick(data, key, tickkey) {
    var distList = [];
    $.each(data, function (j, item) {
        if (distList.indexOf(item[key]) == -1 && item[tickkey] == true) {
            distList.push(item[key]);
        }
    });
    return distList;
}

function getDistinctTek(data) {
    var distList = [];
    $.each(data, function (j, item) {
        if (distList.indexOf(item) == -1) {
            distList.push(item);
        }
    });
    return distList;
}
function getObjects(obj, key, val) {
    var objects = [];
    for (var i in obj) {
        if (!obj.hasOwnProperty(i)) continue;
        if (typeof obj[i] == 'object') {
            objects = objects.concat(this.getObjects(obj[i], key, val));
        } else if (i == key && obj[key] == val) {
            objects.push(obj);
        }
    }
    return objects;
}

function DosyaYukle(dosya, id, tur, tamamlandi) {
    if (dosya && id != 0) {
        var formData = new FormData();
        formData.append('my_uploaded_file', dosya);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "FileUpload.ashx?Tur=" + tur + "&TCKIMLIKNO=" + session.TCKIMLIKNO + "&Oturum=" + session.OTURUM + "&ID=" + id, true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {// 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    App.unblockUI();
                    try {
                        if (tur == "OzelSayfa") {
                            console.log(xhr.response)
                            console.log(xhr.responseText)

                            tamamlandi(xhr.response);
                        }
                        else
                            tamamlandi(true);
                    } catch (e) {
                    }
                }
            }
        }
    }
    else {
        tamamlandi(false);
    }
}

function DosyaYukleZekaTest(dosya, tur, id, ID_ZEKATESTOGRENCIDOSYATUR, tamamlandi) {
    if (dosya) {
        var formData = new FormData();
        formData.append('my_uploaded_file', dosya);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "FileUpload.ashx?Tur=" + tur + "&TCKIMLIKNO=" + session.TCKIMLIKNO + "&Oturum=" + session.OTURUM + "&ID=" + id + '&ID_ZEKATESTOGRENCIDOSYATUR=' + ID_ZEKATESTOGRENCIDOSYATUR, true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {// 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    App.unblockUI();
                    try {
                        tamamlandi(xhr.responseText);
                    } catch (e) {
                        console.log(e);
                    }
                }
            }
        }
    }
    else {
        tamamlandi('');
    }
}

function DosyaYukleOdev(dosya, tur, tamamlandi) {
    if (dosya) {
        var formData = new FormData();
        formData.append('my_uploaded_file', dosya);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "FileUpload.ashx?Tur=" + tur + "&TCKIMLIKNO=" + session.TCKIMLIKNO + "&Oturum=" + session.OTURUM, true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {// 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    App.unblockUI();
                    try {
                        tamamlandi(xhr.responseText);
                    } catch (e) {
                        console.log(e);
                    }
                }
            }
        }
    }
    else {
        tamamlandi('');
    }
}

function SinavTaslakYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "SinavTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function CevapAnahtariTaslakYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "CevapAnahtariTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function HariciPuanSiraTaslakYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "HariciPuanSiraTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function ExcelVeriYukle(tur, dosya, tamamlandi, id = undefined) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "Helper/ExcelVeriYukle.ashx?Tur=" + tur + (id != undefined ? '&id=' + id : ''), true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function KarmaListeYukle(dosya, sinavTarih, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "KarmaListeYukle.ashx?tc=" + session.TCKIMLIKNO + "&oturum=" + session.OTURUM + "&tarih=" + sinavTarih, true);
        xhr.send(formData);

        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function AbideTaslakYukle(ID_ABIDESINAV, dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        formData.append("ID_ABIDESINAV", ID_ABIDESINAV)
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "AbideTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function AbideResimYukle(dosya, ID_ABIDESINAV, ID_ABIDESAYFATUR, filename, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        formData.append("ID_ABIDESINAV", ID_ABIDESINAV);
        formData.append("ID_ABIDESAYFATUR", ID_ABIDESAYFATUR);
        formData.append("filename", filename);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "AbideResimYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function YaziliTaslakYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "YaziliTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function EtkinlikYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "EtkinlikYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function UniteYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "UniteYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function SinavOptikYukle(dosya, optik, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        formData.append('optik', optik);
        var xhr = new XMLHttpRequest();
        // xhr.upload.addEventListener("progress", updateProgressOptik, false); // Upload
        xhr.open('POST', "SinavOptikUpload.ashx", true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function OptikYukle(tur, dosya, optik, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        formData.append('optik', optik);
        formData.append('tur', tur);
        var xhr = new XMLHttpRequest();
        xhr.open('POST', "SinavOptikUpload.ashx", true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function SinavExcelYukle(tc, oturum, dosya, tur, donem, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "SinavExcelUpload.ashx?Tur=" + tur + "&Donem=" + donem + "&Tc=" + tc + "&Oturum=" + oturum, true);
        xhr.send(formData);

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    App.unblockUI();
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function SqlWebAction(metod, controller, action, param, block_target, loading_message, complete) {


    if (block_target == '') {
        block_target = '#app';
    }

    if (loading_message == '') {
        loading_message = 'Yükleniyor...';
    }

    if (block_target != '-') {
        App.blockUI({
            target: block_target,
            boxed: true,
            message: loading_message
        });
    }


    $.ajax({
        type: metod,
        url: controller + "/" + action,
        data: param,
        async: true,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: complete,
        error: function (jqXHR, textStatus, errorThrown) {
            var r = jQuery.parseJSON(jqXHR.responseText);

            if (r.ExceptionMessage == "Oturum Sonlandırıldı!") {
                $.removeCookie("K");
                window.location.replace("/login.html");
            }
            else {
                Alert_Error(r.ExceptionMessage);
            }

        },
        complete: function () {
            if (block_target != '-')
                App.unblockUI(block_target);
        }
    })

}

function WebPost(parent, controller, action, params, block_target, loading_message, complete, raiserror) {
    if (block_target == '') {
        block_target = '#app';
    }

    if (loading_message == '') {
        loading_message = 'Yükleniyor...';
    }
    if (block_target != '-') {
        App.blockUI({
            target: block_target,
            boxed: true,
            message: loading_message
        });
    }

    parent.$http.post(controller + '/' + action, JSON.stringify(params)).then(res => {
        if (block_target != '-')
            App.unblockUI(block_target);
        complete(res.body, parent);
    },
        res => {
            if (res.body.ExceptionMessage == "Oturum Sonlandırıldı!") {
                $.removeCookie("K");
                window.location.replace("/login.html");
            }
            else {
                if (block_target != '-') {
                    App.unblockUI(block_target);
                }
                if (raiserror != undefined) {
                    complete(res.body, parent);
                }
                Alert_Error(res.body.ExceptionMessage);
            }
        });
}

function WebPostLink(parent, link, params, block_target, loading_message, complete, raiserror) {
    if (block_target == '') {
        block_target = '#app';
    }

    if (loading_message == '') {
        loading_message = 'Yükleniyor...';
    }
    if (block_target != '-') {
        App.blockUI({
            target: block_target,
            boxed: true,
            message: loading_message
        });
    }

    parent.$http.post(link, JSON.stringify(params)).then(res => {
        if (block_target != '-')
            App.unblockUI(block_target);
        complete(res.body, parent);
    },
        res => {
            if (res.body.ExceptionMessage == "Oturum Sonlandırıldı!") {
                $.removeCookie("K");
                window.location.replace("/login.html");
            }
            else {
                if (block_target != '-') {
                    App.unblockUI(block_target);
                }
                if (raiserror != undefined) {
                    complete(res.body, parent);
                }
                Alert_Error(res.body.ExceptionMessage);
            }
        });
}

function SqlWebActionSyncJSON(metod, controller, action, param, block_target, loading_message, complete) {

    if (block_target == '') {
        block_target = '#app';
    }

    if (loading_message == '') {
        loading_message = 'Yükleniyor...';
    }

    if (block_target != '-') {
        App.blockUI({
            target: block_target,
            boxed: true,
            message: loading_message
        });
    }

    $.ajax({
        type: metod,
        url: controller + "/" + action,
        data: JSON.stringify(param),
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: complete,
        error: function (jqXHR, textStatus, errorThrown) {
            var r = jQuery.parseJSON(jqXHR.responseText);

            if (r.ExceptionMessage == "Oturum Sonlandırıldı!" || r.ExceptionMessage == "Yetkisiz Giriş!") {
                $.removeCookie("K");
                window.location.replace("/login.html");
            }
            else {
                Alert_Error(r.ExceptionMessage);
            }

        },
        complete: function () {
            if (block_target != '-')
                App.unblockUI(block_target);
        }
    })

}

function WebPostResponsuz(controller, action, param) {

    $.ajax({
        type: "post",
        url: controller + "/" + action,
        data: JSON.stringify(param),
        async: true,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    })

}

function SqlWebActionSync(metod, controller, action, param, block_target, loading_message, complete) {

    if (block_target == '') {
        block_target = '#app';
    }

    if (loading_message == '') {
        loading_message = 'Yükleniyor...';
    }

    if (block_target != '-') {
        App.blockUI({
            target: block_target,
            boxed: true,
            message: loading_message
        });
    }


    $.ajax({
        type: metod,
        url: controller + "/" + action,
        data: param,
        async: false,
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: complete,
        error: function (jqXHR, textStatus, errorThrown) {
            var r = jQuery.parseJSON(jqXHR.responseText);

            if (r.ExceptionMessage == "Oturum Sonlandırıldı!" || r.ExceptionMessage == "Yetkisiz Giriş!") {
                $.removeCookie("K");
                window.location.replace("/login.html");
            }
            else {
                Alert_Error(r.ExceptionMessage);
            }

        },
        complete: function () {
            if (block_target != '-')
                App.unblockUI(block_target);
        }
    })

}

function isNumberKey(val, evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    var vallength = val.length;
    var c = 0;
    var sValue;

    for (var i = 0; i < vallength; i++) {
        sValue = val.substring(i, i + 1);
        if (sValue == "." || sValue == ",") // Virgül, Nokta
            c = c + 1;
    }

    if (c > 0 && (charCode == 44 || charCode == 46))
        return false;

    if (charCode == 44 || charCode == 46) // Virgül, Nokta
        return true;

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function isNumberKeyOnly(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode

    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

jQuery.fn.getCheckboxValues = function () {
    var values = "";
    var i = 0;
    this.each(function () {
        // push values into array
        values = values + $(this).val() + ",";
    });
    // return array of selected checkboxes
    return values.slice(0, -1);
}

function Left(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else
        return String(str).substring(0, n);
}

function Right(str, n) {
    if (n <= 0)
        return "";
    else if (n > String(str).length)
        return str;
    else {
        var iLen = String(str).length;
        return String(str).substring(iLen, iLen - n);
    }
}

function S4() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}

function guid() {
    return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
}

function Alert_Error(message) {
    bootbox.dialog({
        message: "<font color='red'>" + message + "</font>",
        title: "<font color='red'>Hata!</font>",
        buttons: {
            danger: {
                label: "Tamam!",
                className: "btn-danger",
                callback: function () {//App.unblockUI('-');
                    //Example.show("uh oh, look out!");
                }
            },
        }
    });
}

function Alert_Info(message) {
    bootbox.dialog({
        message: "<font color='green'>" + message + "</font>",
        title: "<font color='green'>İşlem Tamam</font>",
        buttons: {
            success: {
                label: "Tamam!",
                className: "btn-success",
                callback: function () {
                    //Example.show("great success");
                }
            },
        }
    });
}

function Alert_Info_Refresh(message) {
    bootbox.dialog({
        message: "<font color='green'>" + message + "</font>",
        title: "<font color='green'>İşlem Tamam</font>",
        buttons: {
            success: {
                label: "Tamam!",
                className: "btn-success",
                callback: function () {
                    location.reload();
                }
            },
        }
    });
}

function Alert_Warning(message, result) {
    bootbox.dialog({
        message: "<font color='#c49f47'>" + message + "</font>",
        title: "<font color='#c49f47'>Uyarı!</font>",
        buttons: {
            main: {
                label: "Tamam!",
                className: "btn-primary yellow",
                callback: result
                //function () {
                //    //Example.show("great success");
                //}
            },
        }
    });
}

function Alert_Basic(title, message) {
    bootbox.dialog({
        message: message,
        title: title,
        buttons: {
            main: {
                label: "Tamam",
                className: "btn-primary",
                callback: function () {
                    //Example.show("Primary button");
                }
            }
        }
    });
}

function Alert_BasicBtn(title, message, buttontext) {
    bootbox.dialog({
        message: message,
        title: title,
        buttons: {
            main: {
                label: buttontext,
                className: "btn-primary",
                callback: function () {
                    //Example.show("Primary button");
                }
            }
        }
    });
}

function Alert_Confirm(title, message, functionName) {
    bootbox.dialog({
        message: message,
        title: title,
        buttons: {
            main: {
                label: "Hayır!",
                className: "btn-default",
                callback: function () {
                    //Example.show("Primary button");
                }
            },
            success: {
                label: "Evet!",
                className: "btn-success",
                callback: function () {
                    functionName();
                }
            }
        }
    });
}

function Alert_OkConfirm(title, message, functionName) {
    bootbox.dialog({
        closeButton: false,
        message: message,
        title: title,
        buttons: {
            success: {
                label: "Tamam",
                className: "btn-success",
                callback: function () {
                    functionName();
                }
            }
        }
    });
}


function Alert_OkCancelConfirm(title, message, okFunc, cancelFunc) {
    bootbox.dialog({
        message: message,
        title: title,
        buttons: {
            main: {
                label: "Hayır!",
                className: "btn-default",
                callback: function () {
                    cancelFunc();
                }
            },
            success: {
                label: "Evet!",
                className: "btn-success",
                callback: function () {
                    okFunc();
                }
            }
        }
    });
}

function CikisYap() {
    $.removeCookie("K");
    window.location.replace("/login.html");

    /*
    window.open("https://interaktif.okyanuskoleji.k12.tr/", "_self");
    if (location.href.indexOf("https://pusulam/AnaSayfa.html") > -1) {
        //window.location.replace("/index.html");
        window.open("https://interaktif/", "_self");
    }
    if (location.href.indexOf("https://testpusulam.okyanuskoleji.k12.tr/AnaSayfa.html") > -1) {
        //window.location.replace("/index.html");
        window.open("https://testinteraktif.okyanuskoleji.k12.tr/", "_self");
    }
     */
}

function ErrorImage(image) {
    image.onerror = "";
    image.src = "/images/ErrorImage.png";
    return true;
}


var ProfilResim = null;
//ProfilResimGetir();
function ProfilResimGetir() {

    var p = JSON.stringify({ KID: session.IdKullanici, SES: session.Oturum });
    SqlWebActionSync("POST", "Profil", "ProfilBilgiGetir", p, '', '', function (data) {

        $.each(data, function (index, value) {

            ProfilResim = value.ProfilResim;

        });
    });
}

function getFile(val) {
    window.open("/FileUpload.ashx?Tur=Download&TCKIMLIKNO=" + session.TCKIMLIKNO + "&kOturum=" + session.Oturum + "&ID=" + val);
}

function goToScrollbyId(id) {

    // Scroll
    $('html,body').animate({
        scrollTop: $("#" + id).offset().top - 100
    },
        'slow');
}

function TarihFormat(tarih) {
    var yil = tarih.substring(0, 4);
    var ay = tarih.substring(5, 7);
    var gun = tarih.substring(8, 10);
    return gun + "." + ay + "." + yil;
}

function TarihSaatFormat(tarih) {
    var yil = tarih.substring(0, 4);
    var ay = tarih.substring(5, 7);
    var gun = tarih.substring(8, 10);
    var saat = tarih.substring(11, 13);
    var dk = tarih.substring(14, 16);
    return gun + "." + ay + "." + yil + ' ' + saat + ':' + dk;
}

function TarihFormatTers(tarih) {// 24.02.2017->2017-02-24T21:00:00.000Z
    var d = new Date(tarih.replace(/(\d{2}).(\d{2}).(\d{4})/, "$3/$2/$1"));
    return new Date(d.setDate(d.getDate() + 1));
}

function SaatFormat(saat) {
    return saat.substring(0, 5);
}

function TarihGetir() {
    var d = new Date();
    return Right("00" + (d.getDate()), 2) + "." + Right("00" + (d.getMonth() + 1), 2) + "." + d.getFullYear();
}

function Mobilmi() {
    var isMobile = false;
    // tüm mobil platformların kontrolü
    if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
        || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;
    return isMobile;
}

function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

//1         2   3       4           5               6   7       8               9   10      11      12          13      14      15       16     17          18      19      20      21
function AmchartOlustur(idchart, data, xmax, valueField, categoryField, color, xtitle, fixedposition, ymin, ymax, ytitle, grafiktitle, sign, exports, precision, xmin, labelangle, rotate, graphs, legend, depth3D) {
    if (labelangle == undefined) labelangle = 90;
    if (xmin == undefined) xmin = 0;
    if (ytitle == undefined) ytitle = "";
    if (ymax == undefined) ymax = 200;
    if (ymin == undefined) ymin = 0;
    if (grafiktitle == undefined) grafiktitle = "";
    if (sign == undefined) sign = "";
    if (exports == undefined) exports = false;
    if (rotate == undefined) rotate = false;
    if (precision == undefined) precision = -1;
    if (depth3D == undefined) depth3D = 20;

    if (graphs == undefined) {
        graphs = [{
            "balloonText": "[[category]]:[[balloonTextField]] <b>" + sign + "[[value]]</b>",
            "fillColorsField": color,
            "fillAlphas": 1,
            "lineAlpha": 0.1,
            "type": "column",
            "valueField": valueField
        }];
    }
    var chartozellikleri = {
        "theme": "light",
        "type": "serial",
        "startDuration": 1,
        "dataProvider": data,
        "precision": precision,//virgülden sonra karakter sayısı
        "mouseWheelZoomEnabled": true,
        "valueAxes": [
            {
                "minimum": xmin,
                "maximum": xmax,
                "position": "bottom",
                "title": xtitle
            },
            {
                "minimum": ymin,
                "maximum": ymax,
                "position": "left",
                "title": ytitle,
            }
        ],
        "graphs": graphs,
        "balloon": {
            "fixedPosition": fixedposition,
            "maxWidth": 100
        },
        "depth3D": depth3D,
        "angle": 30,
        "rotate": rotate,
        "categoryField": categoryField,
        "categoryAxis": {
            "gridPosition": "start",
            "labelRotation": labelangle
        },
        "export": {
            "enabled": exports
        },
        "titles": [{
            "text": grafiktitle,
            "size": 15
        }
        ]
    }
    if (legend != undefined) {
        if (legend) {
            chartozellikleri["legend"] = { "useGraphSettings": legend }
        }
    }

    var chart = AmCharts.makeChart(idchart, chartozellikleri);
}

function groupBy(list, keyGetter) {
    const map = new Map();
    list.forEach((item) => {
        const key = keyGetter(item);
        const collection = map.get(key);
        if (!collection) {
            map.set(key, [item]);
        } else {
            collection.push(item);
        }
    });
    return map;
}

function AmchartOlustur2(idchart, data, xmax, valueField, categoryField, color, xtitle, fixedposition, ymin, ymax, ytitle, grafiktitle, sign, exports, precision, xmin, labelangle, rotate, graphs, legend, depth3D) {
    if (labelangle == undefined)
        labelangle = 90;
    if (xmin == undefined)
        xmin = 0;
    if (ytitle == undefined)
        ytitle = "";
    if (ymax == undefined)
        ymax = 200;
    if (ymin == undefined)
        ymin = 0;
    if (grafiktitle == undefined)
        grafiktitle = "";
    if (sign == undefined)
        sign = "";
    if (exports == undefined)
        exports = false;
    if (rotate == undefined)
        rotate = false;
    if (precision == undefined)
        precision = -1;
    if (depth3D == undefined)
        depth3D = 20;
    if (graphs == undefined) {
        graphs = [{
            "balloonText": '<b>[[ders]]</b>',
            "fillColorsField": color,
            "fillAlphas": 1,
            "lineAlpha": 0.1,
            "type": "column",
            "title": "ders",
            "valueField": valueField
        },
        {
            "balloonText": '<b>[[ders]]</b>',
            "fillColorsField": color,
            "fillAlphas": 1,
            "lineAlpha": 0.1,
            "type": "column",
            "title": '[[ders]]',
            "valueField": "ders"
        }
        ];
    }
    var chartozellikleri = {
        "theme": "light",
        "type": "serial",
        "startDuration": 1,
        "dataProvider": data,
        "precision": precision,//virgülden sonra karakter sayısı
        "mouseWheelZoomEnabled": true,
        "valueAxes": [
            {
                "minimum": xmin,
                "maximum": xmax,
                "position": "bottom",
                "title": xtitle
            },
            {
                "minimum": ymin,
                "maximum": ymax,
                "position": "left",
                "title": ytitle,
            }
        ],

        "legend": {
            "horizontalGap": 10,
            "maxColumns": 1,
            "position": "right",
            "useGraphSettings": true,
            "markerSize": 10
        },
        "graphs": graphs,
        "balloon": {
            "fixedPosition": fixedposition,
            "maxWidth": 100
        },
        "depth3D": depth3D,
        "angle": 30,
        "rotate": rotate,
        "categoryField": 'dersler',
        "categoryAxis": {
            "gridPosition": "start",
            "labelRotation": labelangle
        },
        "export": {
            "enabled": exports
        },
        "titles": [{
            "text": grafiktitle,
            "size": 15
        }
        ]
    }
    if (legend != undefined) {
        if (legend) {
            chartozellikleri["legend"] = { "useGraphSettings": legend }
        }
    }

    var chart = AmCharts.makeChart(idchart, chartozellikleri);
}

function AmchartGrupluOlustur(idchart, data, xmax, valueFields, categoryField, color, xtitle, fixedposition, ymin, ymax, ytitle, grafiktitle, sign, exports, precision, xmin, labelangle, rotate, graphs, legend, depth3D) {
    if (labelangle == undefined) labelangle = 90;
    if (xmin == undefined) xmin = 0;
    if (ytitle == undefined) ytitle = "";
    if (ymax == undefined) ymax = 200;
    if (ymin == undefined) ymin = 0;
    if (grafiktitle == undefined) grafiktitle = "";
    if (sign == undefined) sign = "";
    if (exports == undefined) exports = false;
    if (rotate == undefined) rotate = false;
    if (precision == undefined) precision = -1;
    if (depth3D == undefined) depth3D = 20;



    if (graphs == undefined) {
        graphs = [];
        $.each(valueFields, (j, el) => {
            let dt = {
                "balloonText": el + ":[[value]]",
                "fillAlphas": 0.8,
                "id": "AmGraph-" + (j + 1),
                "lineAlpha": 0.2,
                "type": "column",
                "title": el,
                "valueField": el
            };
            graphs.push(dt);
        });
    }

    var chartozellikleri = {
        "pathToImages": "http://cdn.amcharts.com/lib/3/images/",
        "theme": "light",
        "type": "serial",
        "startDuration": 1,
        "dataProvider": data,
        "precision": precision,//virgülden sonra karakter sayısı
        "mouseWheelZoomEnabled": true,
        "valueAxes": [
            {
                "minimum": xmin,
                "maximum": xmax,
                "position": "bottom",
                "title": xtitle
            },
            {
                "minimum": ymin,
                "maximum": ymax,
                "position": "left",
                "title": ytitle,
            }
        ],
        "legend": {
            "horizontalGap": 10,
            "maxColumns": 1,
            "position": "right",
            "useGraphSettings": true,
            "markerSize": 10
        },
        "graphs": graphs,
        "balloon": {
            "fixedPosition": fixedposition,
            "maxWidth": 100
        },
        "depth3D": depth3D,
        "angle": 30,
        "rotate": rotate,
        "categoryField": categoryField,
        "categoryAxis": {
            "gridPosition": "start",
            "gridAlpha": 0,
            "position": "left"
        },
        "trendLines": [],
        "guides": [],
        "allLabels": [],
        "amExport": {
            "right": 20,
            "top": 20
        },
        "export": {
            "enabled": exports
        },
    };


    if (legend != undefined) {
        if (legend) {
            chartozellikleri["legend"] = { "useGraphSettings": legend }
        }
    }
    var chart = AmCharts.makeChart(idchart, chartozellikleri);
}


function AmChartPieOlustur(idkontrol, data, valuefield, titlefield, exports, title, legend, legendPosition) {
    let leg = {
        "position": "right",
        "marginRight": 100,
        "autoMargins": false
    };
    if (legendPosition != undefined) {
        leg.position = legendPosition
    }
    if (legend != true) {
        var chart = AmCharts.makeChart(idkontrol, {
            "type": "pie",
            "theme": "light",
            "titles": [{
                "text": title
            }],
            "dataProvider": data,
            "valueField": valuefield,
            "titleField": titlefield,
            "balloon": {
                "fixedPosition": true
            },
            "export": {
                "enabled": exports
            },
        });
    }
    else {
        var chart = AmCharts.makeChart(idkontrol, {
            "type": "pie",
            "theme": "light",
            "titles": [{
                "text": title
            }],
            "dataProvider": data,
            "valueField": valuefield,
            "titleField": titlefield,
            "balloon": {
                "fixedPosition": true
            },
            "export": {
                "enabled": exports
            },
            "legend": leg,
        });
    }
}


var divname = "";
function Glb_Print(divName, width) {
    if (width == undefined)
        width = "";
    divname = divName;
    $("#" + divName.toString()).css({ "width": "1000px" });

    setTimeout(window.print, 500);
    setTimeout(Glb_Duzelt, 1000);
}


function Glb_Duzelt() {
    $("#" + divname.toString()).css({ "width": "100%" });

}


function createBootstrapTable(analizdata, tableid, controlid, formatter, istisnabasliklar, sonucbulunamadialert, callback, sortable) {
    if (document.getElementById(controlid) != null && document.getElementById(controlid) != undefined) {
        document.getElementById(controlid).removeAttribute('disabled');
    }
    $("#" + tableid).bootstrapTable('removeAll');
    if (analizdata == undefined) {
        if (sonucbulunamadialert == undefined) {
            bootbox.alert("Sonuç bulunamadı.");
            return;
        }
        else if (sonucbulunamadialert === "") {
            return;
        }
        else {
            bootbox.alert(sonucbulunamadialert);
            return;
        }
    }
    if (sortable == undefined) {
        sortable = false;
    }
    if (analizdata.length == 0) {
        if (sonucbulunamadialert == undefined) {
            bootbox.alert("Sonuç bulunamadı.");
            return;
        }
        else if (sonucbulunamadialert === "") {
            return;
        }
        else {
            bootbox.alert(sonucbulunamadialert);
            return;
        }
    }
    if (istisnabasliklar == undefined) {
        istisnabasliklar = [];
    }
    var basliklar = Object.getOwnPropertyNames(analizdata[0]);
    var kolonlar = [];
    var satir = [];
    for (var i = 0; i < basliklar.length; i++) {
        if (jQuery.inArray(basliklar[i], istisnabasliklar) == -1) {
            if (formatter == undefined) {
                satir.push({
                    title: basliklar[i], field: basliklar[i], halign: "center", align: "center", sortable: sortable
                });
            }
            else {
                satir.push({
                    title: basliklar[i], field: basliklar[i], halign: "center", align: "center", sortable: sortable, formatter: formatter
                });
            }
        }
    }
    kolonlar.push(satir);
    $("#" + tableid).bootstrapTable('destroy').bootstrapTable({
        columns: kolonlar,
        data: analizdata,
        search: false,
        //fixedColumns: true,
        //fixedNumber: 2,
        rowStyle: function (row, index) {
            if (index % 2 === 0) {
                return { classes: 'satir1 stil' };
            }
            return { classes: 'satir2 stil' };
        },
        onLoadSuccess: function () {
            $("#" + tableid).bootstrapTable('resetView');
        },
    });

    if (callback != undefined) {
        callback();
    }
}

function ListeTemizle(item) {
    try {
        while (item.length > 0) { item.pop(); }
    } catch (e) {
        item.length = [];
    }
    return item;
}


function strToDateTurkish(date) {
    var d = undefined;
    date = date == undefined ? new Date() : date;

    if (date.indexOf(".") > -1) { d = date.split("."); }
    else if (date.indexOf("/") > -1) { d = date.split("/"); }
    else if (date.indexOf("-") > -1) { d = date.split("-"); }

    return d != undefined ? new Date(d[2], d[1] - 1, d[0]) : new Date();
}

function strToDate(date) { // 2019-5-15
    var d = undefined;

    if (date.indexOf(".") > -1) { d = date.split("."); }
    else if (date.indexOf("/") > -1) { d = date.split("/"); }
    else if (date.indexOf("-") > -1) { d = date.split("-"); }

    return d != undefined ? new Date(d[0], d[1] - 1, d[2]) : new Date();
}

function dateToStr(date) {
    return date.getDate() + '.' + (date.getMonth() + 1) + '.' + date.getFullYear()
}

function dateDiff(dt1, dt2) {
    /*
     * setup 'empty' return object
     */
    var ret = { days: 0, months: 0, years: 0 };

    /*
     * If the dates are equal, return the 'empty' object
     */
    if (dt1 == dt2 || dt1 == undefined || dt2 == undefined) return '';

    /*
     * ensure dt2 > dt1
     */
    if (dt1 > dt2) {
        var dtmp = dt2;
        dt2 = dt1;
        dt1 = dtmp;
    }

    /*
     * First get the number of full years
     */

    var year1 = dt1.getFullYear();
    var year2 = dt2.getFullYear();

    var month1 = dt1.getMonth();
    var month2 = dt2.getMonth();

    var day1 = dt1.getDate();
    var day2 = dt2.getDate();

    /*
     * Set initial values bearing in mind the months or days may be negative
     */

    ret['years'] = year2 - year1;
    ret['months'] = month2 - month1;
    ret['days'] = day2 - day1;

    /*
     * Now we deal with the negatives
     */

    /*
     * First if the day difference is negative
     * eg dt2 = 13 oct, dt1 = 25 sept
     */
    if (ret['days'] < 0) {
        /*
         * Use temporary dates to get the number of days remaining in the month
         */
        var dtmp1 = new Date(dt1.getFullYear(), dt1.getMonth() + 1, 1, 0, 0, -1);

        var numDays = dtmp1.getDate();

        ret['months'] -= 1;
        ret['days'] += numDays;

    }

    /*
     * Now if the month difference is negative
     */
    if (ret['months'] < 0) {
        ret['months'] += 12;
        ret['years'] -= 1;
    }

    return ret['years'] + " yıl " + ret['months'] + " ay " + ret["days"] + " gün";
}

function ExcelSutunListesi() {
    var k = [];

    for (var i = 65; i <= 90; i++) {
        k.push(String.fromCharCode(i));
    }
    for (var i = 65; i <= 65; i++) {
        for (var j = 65; j <= 90; j++) {
            k.push(String.fromCharCode(i) + String.fromCharCode(j));
        }
    }
    return k;
}



function dateToStr(date, format = 'dd.mm.yyyy') {

    var gun = Right('0' + date.getDate(), 2);
    var ay = Right('0' + (date.getMonth() + 1), 2);
    var yil = date.getFullYear();

    var result = gun + '.' + ay + '.' + yil;

    if (format == 'yyyy-mm-dd') {
        result = yil + '-' + ay + '-' + gun;
    }

    return result;
}

function ListeOlustur(tabloAdi, sort) {
    sort = sort == undefined ? [[0, 'asc']] : sort;

    //$('#UrunListesi').dataTable().fnClearTable(0);
    //$('#UrunListesi').dataTable().fnDraw();
    //$('#' + tabloAdi).DataTable().clear();
    $('#' + tabloAdi).DataTable().destroy();
    vue.$nextTick(() => {
        var tbl = $('#' + tabloAdi).DataTable(
            {
                "aaSorting": sort,
                language: {
                    "url": "./Utility/dil.json"
                    ,
                    "select": {
                        "rows": "%d satır seçildi."
                    }
                },
            });
    })
}


//$(document).ready(function () {
//    jQuery.extend(jQuery.fn.dataTableExt.oSort, {
//        "extract-date-pre": function (value) {
//            var date = value;
//            date = date.split('.');
//            return Date.parse(date[1] + '.' + date[0] + '.' + date[2])
//        },
//        "extract-date-asc": function (a, b) {
//            return ((a < b) ? -1 : ((a > b) ? 1 : 0));
//        },
//        "extract-date-desc": function (a, b) {
//            return ((a < b) ? 1 : ((a > b) ? -1 : 0));
//        }
//    });
//});

function ListeOlusturDate(tabloAdi, sort, column) {
    sort = sort == undefined ? [[0, 'asc']] : sort;
    $('#' + tabloAdi).DataTable().destroy();
    vue.$nextTick(() => {
        var tbl = $('#' + tabloAdi).DataTable(
            {
                "aaSorting": sort,
                columnDefs: column == undefined ? [] : column,
                language: {
                    "url": "./Utility/dil.json"
                    ,
                    "select": {
                        "rows": "%d satır seçildi."
                    }
                },
            });
    })
}

function myIP() {
    if (window.XMLHttpRequest) xmlhttp = new XMLHttpRequest();
    else xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");

    xmlhttp.open("GET", "https://api.ipify.org?format=jsonp&callback=data", false);
    xmlhttp.send();
    var ip = xmlhttp.responseText.toString().substring(12, xmlhttp.responseText.length - 4);
    return ip;
}


function UniteTaramaTaslakYukle(ID_UNITETARAMASINAV, dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        formData.append("ID_UNITETARAMASINAV", ID_UNITETARAMASINAV)
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "UniteTaramaTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function OzelKosullarTaslakYukle(dosya, tamamlandi) {
    if (dosya[0]) {
        var myFileList = dosya;
        var myFile = myFileList[0];
        var formData = new FormData();
        formData.append('my_uploaded_file', myFile);
        //formData.append("ID_UNITETARAMASINAV", ID_UNITETARAMASINAV)
        var xhr = new XMLHttpRequest();
        xhr.upload.addEventListener("progress", updateProgress, false); // Upload
        xhr.open('POST', "OzelKosullarTaslakYukle.ashx", true);
        xhr.send(formData);
        App.unblockUI();

        xhr.onreadystatechange = function () {
            if (xhr.readyState < 4) {
                // while waiting response from server
            }
            else if (xhr.readyState === 4) {               // 4 = Response from server has been completely loaded.
                if (xhr.status >= 200 && xhr.status < 300)  // http status between 200 to 299 are all successful
                {
                    try {
                        tamamlandi(xhr.responseText, parent);
                    } catch (e) {
                    }
                }
                App.unblockUI();
            }
        }
    }
    else {
        tamamlandi('', parent);
    }
}

function resize(img) {
    var actualHeight = img.height
    var actualWidth = img.width
    let maxHeight = 900.0
    let maxWidth = 1600.0
    var imgRatio = actualWidth / actualHeight
    let maxRatio = maxWidth / maxHeight
    var compressionQuality = 0.5

    if (actualHeight > maxHeight || actualWidth > maxWidth) {
        if (imgRatio < maxRatio) {
            //adjust width according to maxHeight
            imgRatio = maxHeight / actualHeight
            actualWidth = imgRatio * actualWidth
            actualHeight = maxHeight
        } else if (imgRatio > maxRatio) {
            //adjust height according to maxWidth
            imgRatio = maxWidth / actualWidth
            actualHeight = imgRatio * actualHeight
            actualWidth = maxWidth
        } else {
            actualHeight = maxHeight
            actualWidth = maxWidth
            compressionQuality = 1
        }
    }

    return { width: actualWidth, height: actualHeight }
}

function compress(e) {
    const fileName = e.target.files[0].name;
    const reader = new FileReader();
    reader.readAsDataURL(e.target.files[0]);
    reader.onload = event => {
        const img = new Image();
        img.src = event.target.result;
        img.onload = () => {
            var size = vue.resize(img);
            const width = size.width;
            const height = size.height;

            const elem = document.createElement('canvas');

            elem.width = width;
            elem.height = height;
            const ctx = elem.getContext('2d');
            // img.width and img.height will contain the original dimensions
            ctx.drawImage(img, 0, 0, width, height);
            //toBlob polyfill
            if (!HTMLCanvasElement.prototype.toBlob) {
                Object.defineProperty(HTMLCanvasElement.prototype, 'toBlob', {
                    value: function (callback, type, quality) {
                        var dataURL = this.toDataURL(type, quality).split(',')[1];
                        setTimeout(function () {
                            var binStr = atob(dataURL),
                                len = binStr.length,
                                arr = new Uint8Array(len);
                            for (var i = 0; i < len; i++) {
                                arr[i] = binStr.charCodeAt(i);
                            }
                            callback(new Blob([arr], { type: type || 'image/png' }));
                        });
                    }
                });
            }

            // toBlob usage
            ctx.canvas.toBlob(function (blob) {
                const file = new File([blob], fileName, {
                    type: 'image/jpeg',
                    lastModified: Date.now()
                });

                vue.FormGonder(file);
            }, 'image/jpeg', 0.85);
        },
            reader.onerror = error => console.log(error);
    };
}


function SHA1(msg) {
    function rotate_left(n, s) {
        var t4 = (n << s) | (n >>> (32 - s));
        return t4;
    };
    function lsb_hex(val) {
        var str = '';
        var i;
        var vh;
        var vl;
        for (i = 0; i <= 6; i += 2) {
            vh = (val >>> (i * 4 + 4)) & 0x0f;
            vl = (val >>> (i * 4)) & 0x0f;
            str += vh.toString(16) + vl.toString(16);
        }
        return str;
    };
    function cvt_hex(val) {
        var str = '';
        var i;
        var v;
        for (i = 7; i >= 0; i--) {
            v = (val >>> (i * 4)) & 0x0f;
            str += v.toString(16);
        }
        return str;
    };
    function Utf8Encode(string) {
        string = string.replace(/\r\n/g, '\n');
        var utftext = '';
        for (var n = 0; n < string.length; n++) {
            var c = string.charCodeAt(n);
            if (c < 128) {
                utftext += String.fromCharCode(c);
            }
            else if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
        }
        return utftext;
    };
    var blockstart;
    var i, j;
    var W = new Array(80);
    var H0 = 0x67452301;
    var H1 = 0xEFCDAB89;
    var H2 = 0x98BADCFE;
    var H3 = 0x10325476;
    var H4 = 0xC3D2E1F0;
    var A, B, C, D, E;
    var temp;
    msg = Utf8Encode(msg);
    var msg_len = msg.length;
    var word_array = new Array();
    for (i = 0; i < msg_len - 3; i += 4) {
        j = msg.charCodeAt(i) << 24 | msg.charCodeAt(i + 1) << 16 |
            msg.charCodeAt(i + 2) << 8 | msg.charCodeAt(i + 3);
        word_array.push(j);
    }
    switch (msg_len % 4) {
        case 0:
            i = 0x080000000;
            break;
        case 1:
            i = msg.charCodeAt(msg_len - 1) << 24 | 0x0800000;
            break;
        case 2:
            i = msg.charCodeAt(msg_len - 2) << 24 | msg.charCodeAt(msg_len - 1) << 16 | 0x08000;
            break;
        case 3:
            i = msg.charCodeAt(msg_len - 3) << 24 | msg.charCodeAt(msg_len - 2) << 16 | msg.charCodeAt(msg_len - 1) << 8 | 0x80;
            break;
    }
    word_array.push(i);
    while ((word_array.length % 16) != 14) word_array.push(0);
    word_array.push(msg_len >>> 29);
    word_array.push((msg_len << 3) & 0x0ffffffff);
    for (blockstart = 0; blockstart < word_array.length; blockstart += 16) {
        for (i = 0; i < 16; i++) W[i] = word_array[blockstart + i];
        for (i = 16; i <= 79; i++) W[i] = rotate_left(W[i - 3] ^ W[i - 8] ^ W[i - 14] ^ W[i - 16], 1);
        A = H0;
        B = H1;
        C = H2;
        D = H3;
        E = H4;
        for (i = 0; i <= 19; i++) {
            temp = (rotate_left(A, 5) + ((B & C) | (~B & D)) + E + W[i] + 0x5A827999) & 0x0ffffffff;
            E = D;
            D = C;
            C = rotate_left(B, 30);
            B = A;
            A = temp;
        }
        for (i = 20; i <= 39; i++) {
            temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0x6ED9EBA1) & 0x0ffffffff;
            E = D;
            D = C;
            C = rotate_left(B, 30);
            B = A;
            A = temp;
        }
        for (i = 40; i <= 59; i++) {
            temp = (rotate_left(A, 5) + ((B & C) | (B & D) | (C & D)) + E + W[i] + 0x8F1BBCDC) & 0x0ffffffff;
            E = D;
            D = C;
            C = rotate_left(B, 30);
            B = A;
            A = temp;
        }
        for (i = 60; i <= 79; i++) {
            temp = (rotate_left(A, 5) + (B ^ C ^ D) + E + W[i] + 0xCA62C1D6) & 0x0ffffffff;
            E = D;
            D = C;
            C = rotate_left(B, 30);
            B = A;
            A = temp;
        }
        H0 = (H0 + A) & 0x0ffffffff;
        H1 = (H1 + B) & 0x0ffffffff;
        H2 = (H2 + C) & 0x0ffffffff;
        H3 = (H3 + D) & 0x0ffffffff;
        H4 = (H4 + E) & 0x0ffffffff;
    }
    var temp = cvt_hex(H0) + cvt_hex(H1) + cvt_hex(H2) + cvt_hex(H3) + cvt_hex(H4);

    return temp.toLowerCase();
}
function xml2json(xml) {
    try {
        var obj = {};
        if (xml.children.length > 0) {
            for (var i = 0; i < xml.children.length; i++) {
                var item = xml.children.item(i);
                var nodeName = item.nodeName;

                if (typeof (obj[nodeName]) == "undefined") {
                    obj[nodeName] = this.xml2json(item);
                } else {
                    if (typeof (obj[nodeName].push) == "undefined") {
                        var old = obj[nodeName];

                        obj[nodeName] = [];
                        obj[nodeName].push(old);
                    }
                    obj[nodeName].push(this.xml2json(item));
                }
            }
        } else {
            obj = xml.textContent;
        }
        return obj;
    } catch (e) {
        console.log(e.message);
    }
}

function tur2eng(val) {
    var charMap = {
        Ç: 'C',
        Ö: 'O',
        Ş: 'S',
        İ: 'I',
        I: 'i',
        Ü: 'U',
        Ğ: 'G',
        ç: 'c',
        ö: 'o',
        ş: 's',
        ı: 'i',
        ü: 'u',
        ğ: 'g'
    };

    var str = val;
    str_array = str.split('');

    for (var i = 0, len = str_array.length; i < len; i++) {
        str_array[i] = charMap[str_array[i]] || str_array[i];
    }

    str = str_array.join('');

    var clearStr = str.replace(" ", "").replace("-", "").replace(/[^a-z0-9-.çöşüğı]/gi, "");

    return clearStr;
}

//#region ################ string şifreleme ve çözme metotları ###################
let Tarih = new Date();
let gun = (Tarih.getDate() < 10 ? "0" + Tarih.getDate() : Tarih.getDate());
let ay = ((Tarih.getMonth() + 1) < 10 ? "0" + (Tarih.getMonth() + 1) : (Tarih.getMonth() + 1))
let gunAyYıl = "." + gun + "." + ay + "." + Tarih.getFullYear();
crypt = {

    // (B1) THE SECRET KEY
    secret: "*Pusulam-" + gunAyYıl,

    // (B2) ENCRYPT
    encrypt: function (clear) {
        var cipher = CryptoJS.AES.encrypt(clear, crypt.secret);
        cipher = cipher.toString();
        return cipher;
    },

    // (B3) DECRYPT
    decrypt: function (cipher) {
        var decipher = CryptoJS.AES.decrypt(cipher, crypt.secret);
        decipher = decipher.toString(CryptoJS.enc.Utf8);
        return decipher;
    }
};

 
//#endreigon ################ eof şifreleme ve çözme metotları ################