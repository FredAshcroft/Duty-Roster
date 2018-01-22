$(document).ready(function () {
    $('#calendar').fullCalendar({
        theme: true,
        editable: false,
        allDaySlot: true,
        events: '/calendar/GetDuties',
        },
        // Day Editing - New Events
        dayClick: function (date, allDay, jsEvent, view) {
            var today = new Date();
            var daysForward = $('#AdvanceBookingDays').val();
            var cancellationDays = $('#CancellationDays').val();
            var cancelDays = new Date();
            var fwdDays = new Date();
            var millisecondOffset = daysForward * 24 * 60 * 60 * 1000;
            var cancelOffset = cancellationDays * 24 * 60 * 60 * 1000;
            fwdDays.setHours(0, 0, 0, 0);
            fwdDays.setTime(fwdDays.getTime() + millisecondOffset);
            cancelDays.setHours(0, 0, 0, 0);
            cancelDays.setTime(cancelDays.getTime() - cancelOffset);
            today.setHours(0, 0, 0, 0);
            if (date < today) {
                ShowAlert('Booking Error', $('#MemberName').val() + ' you should know that you cannot book a boat in the past');
            }
            else if ($('#CanBook').val() == 'False') {
                ShowAlert('Booking Error', 'Sorry ' + $('#MemberName').val() + ' you are not allowed to book boats yet please contact an administrator!');
            }
            else if (date > fwdDays) {
                ShowAlert('Booking Error', $('#MemberName').val() + ' you can only book boats up to ' + daysForward + ' days in advance');
            }
            else {
                $('#Date').val($.fullCalendar.formatDate(date, 'dd-MM-yyyy'));
                //ClearPopupFormValues();
                $('#newBookingForm').modal({
                    backdrop: true,
                    keyboard: true,
                    show: true
                });
                //$('#eventTitle').focus();
                //ShowEventPopup(date);
            }
        },
        ////Rendering events
        eventRender: function (event, element) {
            element.tooltip({
                placement: 'auto',
                title: event.description,
                trigger: 'hover'
            });
        }
    });
    $('#boats').hide();
})

function ShowAlert(title, message) {
    bootbox.alert(message);
}


function ShowEventPopup(date) {
    ClearPopupFormValues();
    $('#newBookingForm').show();
    $('#eventTitle').focus();
}

function ClearPopupFormValues() {
    $('#boats').hide();
    $('#btnShowBoats').show();
}


