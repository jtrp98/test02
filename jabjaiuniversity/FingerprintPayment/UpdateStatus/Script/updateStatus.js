var availableValuestudent = [];

$(function () {
    //$(".btn").click(function () {
    //    $(".btn").removeClass("active");
    //    $(this).addClass("active");
    //});
    //$('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    //getliststudent();

    //$('#txtname').autocomplete({
    //    width: 300,
    //    max: 10,
    //    delay: 100,
    //    minLength: 1,
    //    autoFocus: true,
    //    cacheLength: 1,
    //    scroll: true,
    //    highlight: false,
    //    //source: function (request, response) {
    //    //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
    //    //    response(results.slice(0, 10));
    //    //},
    //    source: lightwell,
    //    select: function (event, ui) {
    //        event.preventDefault();
    //        $("#txtname").val(ui.item.label);
    //        $("#txtid").val(ui.item.value);
    //        $studentid = ui.item.code;
    //    },
    //    focus: function (event, ui) {
    //        event.preventDefault();
    //        $("#txtid").val("");
    //    }
    //});

    $("#btnSearch").click(function () {
        var studentId = SAC.GetStudentID();
        if (studentId === "") {
            Swal.fire({
                icon: 'warning',
                /*title: `<strong >แจ้งข่าวสาร</strong>`,*/
                html: `ขออภัยค่ะ<br/>กรุณากรอกข้อมูลนักเรียนที่ต้องการค้นหา`,
                showDenyButton: true,
                showCancelButton: false,
                confirmButtonText: 'ตกลง',
                denyButtonText: `ยกเลิก`,
            })
            return;
        }

        $("body").mLoading();
        PageMethods.getStudentData(studentId, YTF.GetTermID(), function (obj) {
            $("#student_Name").html(obj["student_Name"]);
            $("#student_Code").html(obj["student_Code"]);
            $("#Identification").html(obj["Level"]);
            $("#birthDay").html(obj["birthDay"]);
            $("#father_Name").html(obj["father_Name"]);
            $("#Phone").html(obj["Phone"]);
            $("#mother_Name").html(obj["mother_Name"]);
            $("#Mobile").html(obj["Mobile"]);
            $('#termNo').html(YTF.GetTermText());
            $('#yearNo').html(YTF.GetYearNo());

            //var date = new Date();
            //moment(dStart, 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY'),
            $("#txtdaystart").val(moment().add(543, 'years').format('DD/MM/YYYY'));
            $("#txtdayend").val(moment().add(543, 'years').format('DD/MM/YYYY'));

            var logTime = obj["logTime"];
            var Total = logTime["Status_0"] + logTime["Status_1"] + logTime["Status_2"] + logTime["Status_3"] + logTime["Status_4"] + logTime["Status_5"] + logTime["Status_6"];
            $("#Status_0").html(logTime["Status_0"]);
            $("#Status_1").html(logTime["Status_1"]);
            $("#Status_2").html(logTime["Status_2"]);
            $("#Status_3").html(logTime["Status_3"]);
            $("#Status_4").html(logTime["Status_4"]);
            $("#Status_5").html(logTime["Status_5"]);
            $("#Status_6").html(logTime["Status_6"]);

            $("#percent_0").html(percent(Total, logTime["Status_0"]));
            $("#percent_1").html(percent(Total, logTime["Status_1"]));
            $("#percent_2").html(percent(Total, logTime["Status_2"]));
            $("#percent_3").html(percent(Total, logTime["Status_3"]));
            $("#percent_4").html(percent(Total, logTime["Status_4"]));
            $("#percent_5").html(percent(Total, logTime["Status_5"]));
            $("#percent_6").html(percent(Total, logTime["Status_6"]));

            data = [
                {
                    label: "ตรงเวลา",
                    data: logTime["Status_0"],
                    color: "#0dc742"
                }, {
                    label: "สาย",
                    data: logTime["Status_1"],
                    color: "#ff9966"
                }, {
                    label: "ขาด",
                    data: logTime["Status_2"],
                    color: "#ff6666"
                }, {
                    label: "กิจกรรม",
                    data: logTime["Status_3"],
                    color: "#3399cc"
                }, {
                    label: "ลากิจ",
                    data: logTime["Status_4"],
                    color: "#9933cc"
                }, {
                    label: "ลาป่วย",
                    data: logTime["Status_5"],
                    color: "#cc66cc"
                }, {
                    label: "ไม่ได้เช็คชื่อ",
                    data: logTime["Status_6"],
                    color: "#e8e8e8"
                }];

            $.plot($("#flot-pie-chart2"), data, {
                series: {
                    pie: {
                        show: true,
                        radius: 8 / 10,
                        label: {
                            show: false,
                            radius: 3 / 4,
                            background: {
                                opacity: 0.5
                            }
                        }
                    }
                },
                bars: { show: false },
                grid: {
                    hoverable: true,
                    labelMargin: 10
                },
                legend: { show: false },
                tooltip: true,
                tooltipOpts: {
                    content: "%p.2%, %s", // show percentages, rounding to 2 decimal places
                    shifts: {
                        x: 20,
                        y: 0
                    },
                    style: "font-size:30px;",
                    defaultTheme: false
                }
            });

            if (obj["Picture"] === "" || obj["Picture"] === null) {
                $("#Picture").html("<div class=\"col-lg-12 center\" style=\"padding: 10px;\">\
                    <i class=\"fa fa-4x fa-user\" style=\"background-color: #39c; width: 180px; height: 180px; padding: 30px; border-radius: 50%;\"></i>\
                </div>");
            }
            else {
                $("#Picture").html("<img src=\"" + obj["Picture"] + "\"  width=\"180\" height=\"180\" style=\"padding: 10px; border-radius: 50%;\">");
            }
            $("body").mLoading('hide');
        });
    });
});

function percent(total, values) {
    return ((values * 100) / parseFloat(total)).toFixed(2);
}

function getliststudent() {
    availableValuestudent = [];
    $.ajax({
        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=&nsublevel=",
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName,
                    value: objjson[index].sID,
                    code: objjson[index].studentid
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function lightwell(request, response) {
    function hasMatch(s) {
        if (s === null) s = "";
        return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
    }
    var i, l, obj, matches = [];

    if (request.term === "") {
        response([]);
        return;
    }

    for (i = 0, l = availableValuestudent.length; i < l; i++) {
        obj = availableValuestudent[i];
        if (hasMatch(obj.label) || hasMatch(obj.code)) {
            matches.push(obj);
        }
    }
    response(matches.slice(0, 10));
}

function UpdateStudntStatus(logStatus, timetype) {
    var selectType = $("#selectType").val();
    var studentId = SAC.GetStudentID();
    if (selectType == "1") {
        if (studentId === "") {
            //$.confirm({
            //    title: false,
            //    content: '<img src="/images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><h1 class="text-danger text-center">ขออภัยค่ะ <br/>กรุณากรอกข้อมูลนักเรียนที่ต้องการค้นหา</h1>',
            //    theme: 'material',
            //    columnClass: 'col-md-5 col-md-offset-4',
            //    type: 'red',
            //    buttons: {
            //        "<span style=\"font-size: 20px;\">ปิด</span>": {
            //            keys: ['enter', 'shift'],
            //            btnClass: 'btn-primary',
            //            action: function () {
            //                //console.log(result);
            //            }
            //        }
            //    }
            //});
            Swal.fire({
                icon: 'error',
                title: 'ขออภัยค่ะ ',
                html: '<span >กรุณากรอกข้อมูลนักเรียนที่ต้องการค้นหา!</span>',
                confirmButtonText: `ปิด`,
            })
            return;
        }
    }

    Swal.fire({
        icon: 'warning',
        title: `<strong >แจ้งข่าวสาร</strong>`,
        html: `
<span stylex="font-size:1.55em">โปรดทราบ ! <strong>คุณ ${_username}</strong>
<br/>เมื่อทำการปรับสถานะใหม่อัตโนมัติแล้ว จะไม่สามารถย้อนกลับได้ 
<br/>และหากต้องการให้บริษัทย้อนข้อมูล จะมีค่าใช้จ่ายเกิดขึ้นครั้งละ 1,000 บาท
<br/><br/>โปรดตรวจสอบให้แน่ใจอีกครั้งว่าไม่เกิดผลกระทบกับหน่วยงานอื่นๆในโรงเรียน
<br/><br/><strong>คุณต้องการยืนยันการปรับสถานะใหม่หรือไม่</strong></span>`,
        showDenyButton: true,
        showCancelButton: false,
        confirmButtonText: 'ยืนยัน',
        denyButtonText: `ยกเลิก`,
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {

            var level2 = "";

            switch (selectType) {

                case "1":
                    level2 = "none";
                    break;
                case "2":
                    var x = $("#ddlclass").val();
                    if (x)
                        level2 = x.join();
                    break;
                case "3":
                    level2 = "";
                    break;

                default:
            }

            $("body").mLoading();

            $("#txtdaystart").trigger('change');
            $("#txtdayend").trigger('change');

            var data = {
                "student_id": studentId,
                "dStart": $('#txtdaystart').data("DateTimePicker").date().format('MM/DD/YYYY'),
                //moment($("#txtdaystart").val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY'), 
                "dEnd": $('#txtdayend').data("DateTimePicker").date().format('MM/DD/YYYY'),
                //moment($("#txtdayend").val(), 'DD/MM/YYYY').add(-543, 'years').format('MM/DD/YYYY'),
                "tType": timetype,
                "selectType": selectType,
                "logStatus": logStatus,
                "level2str": level2,
                'term_id': YTF.GetTermID()
            };

            if (selectType == "1") {
                PageMethods.UpdateStudentStatus(data, function (e) {
                    //$.confirm({
                    //    title: false,
                    //    content: '<img src="/images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">ทำการบันทึกข้อมูลเรียบร้อยแล้ว</h1>',
                    //    theme: 'material',
                    //    type: 'blue',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\">ปิด</span>": {
                    //            btnClass: 'btn-primary',
                    //            keys: ['enter', 'shift'],
                    //            action: function () {
                    //            }
                    //        }
                    //    }
                    //});

                    Swal.fire({
                        icon: 'success',
                        title: 'ทำการบันทึกข้อมูลเรียบร้อยแล้ว',
                        confirmButtonText: `ปิด`,
                    })

                    console.log(e);
                    //$("#txtid").val("");
                    //$("#txtdayend").val("");
                    //$("#txtdaystart").val("");
                    $(".output").html("");
                    $("span[id*=percent]").html("0");
                    $("span[id*=Status]").html("0");
                    $("#flot-pie-chart2").html("");
                    //$("#txtname").val("");
                    $("#Picture").html("<div class=\"col-lg-12 center\" style=\"padding: 10px;\">\
                    <i class=\"fa fa-4x fa-user\" style=\"background-color: #39c; width: 180px; height: 180px; padding: 30px; border-radius: 50%;\"></i>\
                </div>");
                    $("body").mLoading('hide');
                    $("#txtname").focus();
                });
            } else {
                PageMethods.UpdateStudentStatus2(data, function (e) {
                    //$.confirm({
                    //    title: false,
                    //    content: '<img src="/images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">ทำการบันทึกข้อมูลเรียบร้อยแล้ว</h1>',
                    //    theme: 'material',
                    //    type: 'blue',
                    //    buttons: {
                    //        "<span style=\"font-size: 20px;\">ปิด</span>": {
                    //            btnClass: 'btn-primary',
                    //            keys: ['enter', 'shift'],
                    //            action: function () {
                    //            }
                    //        }
                    //    }
                    //});
                    Swal.fire({
                        icon: 'success',
                        title: 'ทำการบันทึกข้อมูลเรียบร้อยแล้ว',
                        confirmButtonText: `ปิด`,
                    })

                    console.log(e);
                    //$("#txtdayend").val("");
                    //$("#txtdaystart").val("");
                    //$("#txtdaystart").val("");
                    //$(".output").html("");
                    //$("span[id*=percent]").html("0");
                    //$("span[id*=Status]").html("0");
                    // $("#flot-pie-chart2").html("");
                    $("body").mLoading('hide');

                });
            }


        } else if (result.isDenied) {
            //Swal.fire('Changes are not saved', '', 'info')
            return;
        }
    })



}