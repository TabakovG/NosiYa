document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: '2023-07-26',
        headerToolbar: {
            start: 'title', // will normally be on the left. if RTL, will be on the right
            center: 'dayGridMonth,dayGridWeek,listDay',
            end: 'today,prev,next'
        }, // will normally be on the right. if RTL, will be on the left
        initialView: 'dayGridMonth',
        validRange: function (nowDate) {
            var nowDate = new Date();
            var endDate = new Date();
            return {
                start: nowDate,
                end: endDate.setMonth(endDate.getMonth() + 4)
            };
        },
        timeZone: 'UTC',
        allDayDefault: true,
        events: '/Admin/calendar/PopulateCalendarAll/',
        eventClick: function (info) {
            // Redirect to the details page for the clicked event
            window.location.href = '/Admin/Order/Details/' + info.event.id;
        }
    });
    calendar.render();
});

