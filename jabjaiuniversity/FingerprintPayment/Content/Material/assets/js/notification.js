
function LoadMessageSystem() {
    // Initialize the object, before adding data to it.
    //  { } is declarative shorthand for new Object()
    var obj = {};
    obj.method = 'list';

    //In order to proper pass a json string, you have to use function JSON.stringfy
    var jsonData = JSON.stringify(obj);

    $.ajax({
        type: "POST",
        url: "/App_Logic/MessageSystemHandler.ashx",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var notiCountAll = 0;

            if (response.listData.length > 0) {
                var newNotiCount = 0;
                $(response.listData).each(function () {
                    $("#notiTab1, #notiTab21").append(`
<a class="dropdown-item d-flex align-items-center" href="#" data-id="`+ this.id + `" data-type="` + this.type + `">
    <div class="dropdown-list-image mr-3">
        <img class="" src="/images/School Bright logo only.png" style="width: 48px;" alt="">
        `+ (this.flag ? '<div class="status-indicator bg-danger"></div>' : '') + `
    </div>
    <div class="font-weight-bold">
        <div class="text-truncate">`+ this.title + `</div>
        <div class="text-truncate" style="font-size: .8rem; line-height: 2; font-weight: normal; margin-top: -2px;">`+ this.content + `</div>
        <div class="small text-gray-500" style="margin-top: 7px;">Administrator · `+ this.timeAgo + `</div>
    </div>
</a>`);
                    if (this.flag) newNotiCount++;
                });

                if (newNotiCount > 0) {
                    $('#noti1-badge').text(newNotiCount);
                    $('#noti1-badge').show();
                }

                notiCountAll += newNotiCount;
                $("#notiTab1, #notiTab21").append('<a class="dropdown-item d-block text-center small text-gray-500" href="UpdateLog.aspx" style="margin-top: 10px;">ดูทั้งหมด</a>');

                $(".dropdown-noti .tab-pane a.dropdown-item[href='#']").on("click", function () {
                    GetMessageSystem($(this).attr('data-id'), $(this).attr('data-type'));
                    return false;
                });
            }
            else {
                $("#notiTab1, #notiTab21").append('<a class="dropdown-item text-center small text-gray-500" href="#" style="display: block;">ไม่พบข่าวสารประกาศจากโรงเรียน</a>');
            }


            if (response.WorklistData.length > 0) {
                var newNotiCount = 0;
                $(response.WorklistData).each(function () {
                    $("#notiTab2, #notiTab22").append(`
<a class="dropdown-item d-flex align-items-center gotoclassonline" href="/ClassOnline/OnlineReport.aspx?id=`+ this.id + `" data-id="` + this.id + `">
    <div class="dropdown-list-image mr-3">
        <img class="" src="/images/School Bright logo only.png" style="width: 48px;" alt="">
        `+ (this.flag ? '<div class="status-indicator bg-danger"></div>' : '') + `
    </div>
    <div class="font-weight-bold">
        <div class="text-truncate">วิชา `+ this.subject + ' ' + this.code + `</div>
        <div class="text-truncate">`+ this.group + ' ' + this.homework + `</div>
        <div class="text-truncate">มีนักเรียน `+ this.count + ' คน ' + (this.type == 1 ? "ส่งการบ้าน" : "ส่งข้อความ") + `</div>
        <div class="small text-gray-500">Administrator · `+ this.timeAgo + `</div>
    </div>
</a>`);
                    if (this.flag) newNotiCount++;
                });

                if (newNotiCount > 0) {
                    $('#noti2-badge').text(newNotiCount);
                    $('#noti2-badge').show();
                }

                notiCountAll += newNotiCount;

                // $("#notiTab2").append('<a class="dropdown-item text-center small text-gray-500" href="UpdateLog.aspx">ดูทั้งหมด</a>');                                              
            }
            else {
                $("#notiTab2, #notiTab22").append('<a class="dropdown-item d-block text-center small text-gray-500" href="#">ไม่มีรายการแจ้งเตือน</a>');
            }

            if (notiCountAll > 0) {
                $('#notificationCount').text(notiCountAll);
                $('#notificationCount').show();
            }
        },
        failure: function (response) {
            console.log(response.d);
        },
        error: function (response) {
            console.log(response.d);
        }
    });
}

function UpdateLastNotification() {
    // Initialize the object, before adding data to it.
    //  { } is declarative shorthand for new Object()
    var obj = {};
    obj.method = 'update-last-noti';

    //In order to proper pass a json string, you have to use function JSON.stringfy
    var jsonData = JSON.stringify(obj);

    $.ajax({
        type: "POST",
        url: "/App_Logic/MessageSystemHandler.ashx",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $('#notificationCount').text('0');
            $('#notificationCount').hide();
        },
        failure: function (response) {
            console.log(response.d);
        },
        error: function (response) {
            console.log(response.d);
        }
    });
}


function GetMessageSystem(id, type) {
    if (!id) return false;
    // Initialize the object, before adding data to it.
    //  { } is declarative shorthand for new Object()
    var obj = {};
    obj.method = 'get';
    obj.id = id;
    obj.type = type;

    //In order to proper pass a json string, you have to use function JSON.stringfy
    var jsonData = JSON.stringify(obj);

    $.ajax({
        type: "POST",
        url: "/App_Logic/MessageSystemHandler.ashx",
        data: jsonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#modalNotifyOnlyClose").find('.modal-title').text('ข่าวสาร');
            if (response.success) {
                $("#modalNotifyOnlyClose").find('.modal-body').html(`
<div class="row">
    <div class="col-md-2 mb-2">
        <b>หัวข้อ:</b>
    </div>
    <div class="col-md-10 mb-10">
        `+ response.getData.title + `
    </div>
</div>
<div class="row">
    <div class="col-md-12 mb-12">
        <b>ข้อความ:</b>
    </div>
</div>
<div class="row">
    <div class="col-md-12 mb-12">
        `+ response.getData.message.replace(/\n/g, "<br />") + `
    </div>
</div>
<div class="row">
    <div class="col-md-2 mb-2">
        <b style="width: 88px; float: left;">วันที่บันทึก:</b>
    </div>
    <div class="col-md-10 mb-10">
        `+ response.getData.date + `
    </div>
</div>`);
            }
            else {
                $("#modalNotifyOnlyClose").find('.modal-body').text('โหลดข้อมูลรายการนี้ไม่สำเร็จ [' + response.message + ']');
            }
            $("#modalNotifyOnlyClose").modal('show');
        },
        failure: function (response) {
            console.log(response.d);
        },
        error: function (response) {
            console.log(response.d);
        }
    });
}