
document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var setId = document.getElementsByName('outfitSetId')[0].value;
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialDate: '2023-07-26',
        initialView: 'dayGridMonth',
        validRange: function (nowDate) {

            // Convert nowDate to a Moment.js object
            var nowDate = new Date();
            var endDate = new Date();

            return {
                start: nowDate,
                end: endDate.setMonth(endDate.getMonth() + 4)
            };
        },
        selectable: true,
        timeZone: 'UTC',
        allDayDefault: true,
        selectOverlap: function (event) {
            return event.rendering === 'background';
        },
        select: function (info) {
            // Update the datetime input field with the selected date's start time
            var selectedStartDate = new Date(info.start);
            var selectedEndDate = new Date(info.end);
            selectedEndDate.setMinutes(selectedEndDate.getMinutes() - 1); // Adjust for end date on the correct day

            document.getElementById('fromDate').value = selectedStartDate.toISOString().slice(0, 16);
            document.getElementById('toDate').value = selectedEndDate.toISOString().slice(0, 16);
            var yourForm = document.getElementsByName('cartPreOrderForm')[0];
            yourForm.checkValidity();
        },
        events: '/calendar/PopulateCalendar/'+ setId
    });
    calendar.render();
});
