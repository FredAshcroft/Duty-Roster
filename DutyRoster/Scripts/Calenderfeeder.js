$(document).ready(function () {
    // page is now ready, initialize the calendar...
    $('#calendar').fullCalendar({
        // put your options and callbacks here
        events: '/calendar/GetDuties',
        eventClick: function (event, element) {
            // Display the modal and set the values to the event values.
            $('#title').html(event.title);
            $('#starts-at').html(event.Description);
            $('#ends-at').html(event.end);
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