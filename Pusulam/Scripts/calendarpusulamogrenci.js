var bilgi = "";
console.log("hop burdayim");
var item;
$(document).ready(function () {
    console.log("ready!");
    item = this;
});
var Calendar = function () {
    return {
        init: function () {
            Calendar.initCalendar();
        },

        initCalendar: function () {
            if (!jQuery().fullCalendar) {
                console.log("fullcalendar yok");
                return;
            }

            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();

            var h = {};

            if ($('#calendar').parents(".portlet").width() <= 720) {
                $('#calendar').addClass("mobile");
                h = {
                    left: 'title, prev, next',
                    center: '',
                    right: 'today,month,agendaWeek,agendaDay'
                };
            } else {
                $('#calendar').removeClass("mobile");
                h = {
                    left: 'title',
                    center: '',
                    right: 'prev,next,today,month,agendaWeek,agendaDay'
                };
            }

            var initDrag = function (el) {
                // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
                // it doesn't need to have a start or end
                var eventObject = {
                    title: $.trim(el.text()) // use the element's text as the event title
                };
                // store the Event Object in the DOM element so we can get to it later
                el.data('eventObject', eventObject);
                // make the event draggable using jQuery UI
                el.draggable({
                    zIndex: 999,
                    revert: true, // will cause the event to go back to its
                    revertDuration: 0 //  original position after the drag
                });
            };

            var addEvent = function (title) {

                title = title.length === 0 ? "Untitled Event" : title;
                var html = $('<div class="external-event label label-default">' + title + '</div>');
                jQuery('#event_box').append(html);
                initDrag(html);
            };

            $('#external-events div.external-event').each(function () {
                // initDrag($(this));
            });

            $('#event_add').unbind('click').click(function () {
                var title = $('#event_title').val();
                addEvent(title);
            });


            $('#calendar').fullCalendar('destroy'); // destroy the calendar
            $('#calendar').fullCalendar({ //re-initialize the calendar
                header: h,
                defaultView: 'month', // change default view with available options from http://arshaw.com/fullcalendar/docs/views/Available_Views/ 
                slotMinutes: 15,
                editable: false,
                droppable: false, // this allows things to be dropped onto the calendar !!!
                drop: function (date, allDay) { // this function is called when something is dropped

                    // retrieve the dropped element's stored Event Object
                    var originalEventObject = $(this).data('eventObject');
                    // we need to copy it, so that multiple events don't have a reference to the same object
                    var copiedEventObject = $.extend({}, originalEventObject);

                    // assign it the date that was reported
                    copiedEventObject.start = date;
                    copiedEventObject.allDay = allDay;
                    copiedEventObject.className = $(this).attr("data-class");

                    // render the event on the calendar
                    // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
                    $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

                    // is the "remove after drop" checkbox checked?
                    if ($('#drop-remove').is(':checked')) {
                        // if so, remove the element from the "Draggable Events" list
                        $(this).remove();
                    }
                },
                eventClick: function (event, element) {

                    EtkinlikIncele(event.id);
                },
                events: function (start, end, callback) {
                    var baslangic = $.fullCalendar.formatDate(start, "dd/MM/yyyy");
                    var regex = /&#39;/g;

                    var p = { TCKIMLIKNO: session.TCKIMLIKNO, OTURUM: session.OTURUM, baslangic: baslangic };
                    //WebPost(item, "AkademikTakvim", "Takvim_EtkinlikListOgrenci", p, '', '', function (data, parent) {
                    SqlWebAction("POST", "AkademikTakvim", 'Takvim_EtkinlikListOgrenci', JSON.stringify(p), '#test', '', function (data, parent) {
                        bilgi = JSON.parse(data);
                        source = [{}];
                        var veri = "";
                        $.each(bilgi, function (j) {
                            source.push({
                                id: this.ID_ETKINLIK,
                                title: this.ETKINLIK.replace("'", regex),
                                start: new Date(this.BASTARIH.split('/')[0], (this.BASTARIH.split('/')[1] - 1), this.BASTARIH.split('/')[2]),
                                end: new Date(this.BITTARIH.split('/')[0], (this.BITTARIH.split('/')[1] - 1), this.BITTARIH.split('/')[2]),
                                color: this.RENK
                            });
                            veri += "<tr><td onclick='EtkinlikIncele(" + this.ID_ETKINLIK + ");' style=' cursor:pointer;'>" + this.ACIKLAMA + "</td></tr>";
                            if (bilgi.length == (j + 1)) {
                                callback(source);
                                $("#tbody1").html(veri);
                            }
                        })
                    });
                }
            });
        }
    };
}();
Calendar.init();


function EtkinlikIncele(ids) {
    $("#btnEtkinlikSil").hide();

    var index = 0;
    for (var i = 0; i < bilgi.length; i++) {
        if (bilgi[i].ID_ETKINLIK == ids) {
            index = i;              //burada aradýðýmý buldum
            i = bilgi.length;     //döngü bitsin diye i yi son deðere ata
        }
    }
    ids = index;
    var bast = new Date(bilgi[ids].BASTARIH);
    var bitt = new Date(bilgi[ids].BITTARIH);
    $("#btnEtkinlikSil").hide();
    $("#ModalEtkinlikIncele").modal("toggle");
    $("#txtInceleEtkinlik").val(bilgi[ids].ETKINLIK + ' (' + bilgi[ids].GRUP + ')');
    $("#txtInceleAciklama").html(bilgi[ids].ACIKLAMA);
    $("#YetkiText").html(bilgi[ids].KullaniciTipi + "  " + bilgi[ids].GRUP);
    $("#dpInceleBasTarih").val(bast.getDate() + "." + (bast.getMonth() + 1) + "." + bast.getFullYear());
    $("#dpInceleBitTarih").val(bitt.getDate() + "." + (bitt.getMonth() + 1) + "." + bitt.getFullYear());
    $("#btnEtkinlikSil").unbind("click");
    $("#btnEtkinlikSil").attr("onclick", "btnEtkinlikSil(" + bilgi[ids].ID_ETKINLIK + ")");

}
