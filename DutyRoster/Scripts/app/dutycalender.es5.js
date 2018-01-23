'use strict';

$(document).ready(function () {
    // page is now ready, initialize the calendar...
    $('#calendar').fullCalendar({
        // put your options and callbacks here
        theme: true,
        events: '/duty/get',
        type: 'GET',
        eventRender: function eventRender(event, element) {
            element.tooltip({
                placement: 'auto',
                title: event.name,
                trigger: 'hover'
            });
        }
    });
});

