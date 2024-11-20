document.addEventListener('DOMContentLoaded', function () {
    let eventArr = [];
    let eventsTable = document.getElementById("ClassTable");

    // Lấy tất cả các dòng trong bảng
    let trElems = eventsTable.getElementsByTagName("tr");

    // Lặp qua từng dòng trong bảng
    for (let i = 1; i < trElems.length; i++) { // Bắt đầu từ 1 để bỏ qua dòng tiêu đề
        let tdElems = trElems[i].getElementsByTagName("td");
        let eventObj = {
            id: tdElems[0].innerText,
            title: tdElems[1].innerText,
            daysOfWeek: tdElems[2].innerText,
            startTime: tdElems[3].innerText,
            endTime: tdElems[4].innerText,
            startRecur: tdElems[5].innerText,
            endRecur: tdElems[6].innerText,
            description: tdElems[7].innerText
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
        navLinks: true, // có thể nhấp vào tên ngày/tuần để chuyển đến các chế độ xem
        editable: true,
        selectable: true,
        selectMirror: true,
        dayMaxEvents: true, // cho phép hiển thị "more" khi có quá nhiều sự kiện trong một ngày
        events: eventArr,
        locale: 'vi', // Sử dụng locale tiếng Việt
        buttonText: {
            today: 'Hôm nay',
            month: 'Tháng',
            week: 'Tuần',
            day: 'Ngày',
            list: 'Danh sách'
        eventClick: function (calEvent, jsEvent, view) {
            $('#myModal #eventTitle').text(calEvent.title);
            var $description = $('<div>');
            $description.append($('<p>').html('<b>Bắt đầu: </b>' + calEvent.startTime + '<b> - Kết thúc: </b>' + calEvent.endTime));
            $description.append($('<p>').html('<b>Mô tả: </b>' + calEvent.description));
            $('#myModal #pDetails').empty().html($description);
            $('#myModal').modal();
        },
        });
    calendar.render();
});
