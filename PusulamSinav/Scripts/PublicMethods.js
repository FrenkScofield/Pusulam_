$(document).ready(function () {
    $('.selectpicker').selectpicker({
        style: 'btn grey-cararra',
        //  size: this.attr("size") == undefined ? 4 : this.attr("size"),
        size: 'auto'
    });
});
var tableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
      , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta http-equiv="X-UA-Compatible" content="IE=EmulateIE9" charset="UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
      , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
      , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        console.log(name);
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.tHead.innerHTML + table.tBodies[0].innerHTML.toString().split('.').join(',') }
        console.log(ctx);
        window.location.href = uri + base64(format(template, ctx))
    }
})()

//var tableToExcel = (function () {
//    var uri = 'data:application/vnd.ms-excel;base64,'
//      , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
//      , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
//      , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
//    return function (table, name, fileName) {
//        if (!table.nodeType) table = document.getElementById(table)
//        var ctx = { worksheet: 'Sayfa1'/*name || 'Worksheet'*/, table: table.innerHTML }

//        var link = document.createElement("A");
//        console.log(uri + base64(format(template, ctx)));
//        link.href = uri + base64(format(template, ctx));
//        link.download = fileName || 'Workbook.xls';
//        link.target = '_blank';
//        document.body.appendChild(link);
//        link.click();
//        document.body.removeChild(link);
//    }
//})();




