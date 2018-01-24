$(document).ready(function () {
    // page is now ready, initialize the calendar...
    $('#calendar').fullCalendar({
        // put your options and callbacks here
        events: '/calendar/GetDuties',
        eventClick: function (event, element) {
            // Display the modal and set the values to the event values.
            $('#title').html(event.title);
            $('#starttime').html(event.starttime);
            $('#endtime').html(event.endtime);
            $('#dutyId').val(event.Id);
            $('.modal').modal('show');

        },
        eventRender: function (event, element) {
            element.tooltip({
                placement: 'auto',
                title: event.Description,
                trigger: 'hover'
            });
        }
    });
});

$('#volunteerBtn').click(function () {

    var dataRow = {
        'dutyId': $('#dutyId').val()
    };

    $.ajax({
        type: 'POST',
        data: dataRow,
        url: '/calendar/volunteer/',      
        success: function (response) {
            if (response === 'True') {
                $('#calendar').fullCalendar('refetchEvents');
                ShowAlert('', 'Volunteered for duty');
            }
            else {
                ShowAlert('', 'Error, Could Not Volunteer you for that duty!');
            }
        }
    });
});