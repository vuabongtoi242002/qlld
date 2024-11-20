document.addEventListener('DOMContentLoaded', function () {
    let eventArr = [];

    let eventsTable = document.getElementById("EventTable");

    let trElems = eventsTable.getElementsByTagName("tr");
    for (let tr of trElems) {
        let tdElems = tr.getElementsByTagName("td");
        let eventObj = {
            id: tdElems[0].innerText,
            title: tdElems[1].innerText,
            start: tdElems[2].innerText
        };
        eventArr.push(eventObj);
    }

    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        headerToolbar: {
            left: 'prevYear,prev,next,nextYear today',
            center: 'title',
            right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
        },
        initialView: 'dayGridMonth',
        initialDate: new Date(),
        navLinks: true, // can click day/week names to navigate views
        editable: true,
        dayMaxEvents: true, // allow "more" link when too many events
        events: eventArr,
    });
    calendar.render();
});
