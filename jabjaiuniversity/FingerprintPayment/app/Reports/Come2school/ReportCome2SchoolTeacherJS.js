
var availableValuestudent = [];
var availableValueEmployees = [];
$(function () {
    document.getElementById('txtname').addEventListener('input', function () {
        if (this.value === '') {
            $("#txtid").val("");
        }
    }, false);
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });


    $('#ctl00_MainContent_ddlSubLV2').change(function () {
        GetSchedule();
    });

    $('#txtname').autocomplete({
        width: 300,
        max: 10,
        delay: 100,
        minLength: 1,
        autoFocus: true,
        cacheLength: 1,
        scroll: true,
        highlight: false,
        source: function (request, response) {
            var type = $("#ctl00_MainContent_dllTypeReport option:selected").val();
            var results;
            results = $.ui.autocomplete.filter(availableValuestudent, request.term);
            response(results.slice(0, 10));
        },
        select: function (event, ui) {
            event.preventDefault();
            $("#txtname").val(ui.item.label);
            $("#txtid").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#txtid").val("");
        }
    });
    //$.get("/App_Logic/ReportLearnScanning.ashx", "", function (msg) {
    //    console.log(msg);
    //})

    $("#exportfile").click(function () {
        if ($("#txtname").val() != "") {
            var dt = new Date();
            $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าทำงาน', fileName: "รายงานสรุปสถิติการเข้าทำงานของบุคลากร_" + $("#txtname").val() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        } else {
            var dt = new Date();
            $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าทำงาน', fileName: "รายงานสรุปสถิติการเข้าทำงานของบุคลากร_" + $("input[id*=hdfschoolname]").val() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        }


    });
    $.ajax({
        url: "/App_Logic/modalJSON.aspx?mode=teacher",
        dataType: "json",
        success: function (objjson) {
            availableValueEmployees = [];
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName + ' ' + objjson[index].sLastname,
                    value: objjson[index].sEmp
                };
                availableValueEmployees[index] = newObject;
            });
            console.log(objjson);
        }
    });

    $('input[id=txtname]').autocomplete({
        width: 300,
        max: 10,
        delay: 100,
        minLength: 1,
        autoFocus: true,
        cacheLength: 1,
        scroll: true,
        highlight: false,
        source: function (request, response) {
            var results;
            results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
            response(results.slice(0, 10));
        },
        select: function (event, ui) {
            event.preventDefault();
            $("input[id=txtname]").val(ui.item.label);
            $("input[id=txtid]").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("input[id=txtid]").val("");
        }
    });

    $("#backpage").click(function () {
        Reports01();
    });
});

function Reports01() {
    $("#backpage").addClass("hidden");
    $("body").mLoading();
    var dt = new Date();
    var Header = $("#myHeader");
    var HtmlTable = $("#myTable");
    Header.html("");
    HtmlTable.html("");
    var day = "";
    if ($("#txtstart").val() !== "" && $("#txtend").val() !== "") {
        day = $("#txtstart").val() + " ถึงวันที่ " + $("#txtend").val();
    }
    else if ($("#txtstart").val()) {
        day = $("#txtstart").val();
    }
    else {
        day = dt.toLocaleDateString();
    }



    if ($("#txtid").val() !== "") {

        Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=12>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
            + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'>รายงานสรุปสถิติการเข้าทำงาน</th></tr> `
            + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>"`
            + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
            + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
            + `<tr><th  style="text-align: right; border-width:0px;"colspan=12><br></th>`);

        HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
            + "<td id='headder' rowspan='2'style='width:5%;'>ลำดับ<td  id='headder' style='width:10%'rowspan='2'>วันที่<td  id='headder' style='width:10%'rowspan='2'>ประเภทบุคลากร"
            + "<td id='headder'rowspan='2'>ชื่อ - นามสกุล<td id='headder'rowspan='2'>เวลาเข้างาน<td id='headder'colspan='5'style='width:15%'>สถานะ</td><td id='headder'rowspan='2'>เวลาเลิกงาน<td id='headder'colspan='5'style='width:15%'>สถานะ</td>");

        HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
            + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด<td id='headder'style='width:5%'>กิจ<td id='headder'style='width:5%'>ป่วย"
            + "<td id='headder'style='width:5%'>ตรง(หลัง)เวลา<td id='headder'style='width:5%'>ก่อนเวลา<td id='headder'style='width:5%'>ขาด<td id='headder'style='width:5%'>กิจ<td id='headder'style='width:5%'>ป่วย");

        $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school02teacher&" + sortday() + sortuser() + sortstatus(), {}, function (Obj) {
            var sumallstudent4level = 0;
            var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0, sumallstatus_3 = 0, sumallstatus_4 = 0;
            var sumallstatusout_0 = 0, sumallstatusout_1 = 0, sumallstatusout_2 = 0, sumallstatusout_3 = 0, sumallstatusout_4 = 0;
            $.each(Obj, function (indexlevel) {
                var HtmlRow = "";
                HtmlRow += "<tr><td class='center' >" + (indexlevel + 1)
                    + "<td class='center'>" + Obj[indexlevel].day
                    + "<td class='center'>" + Obj[indexlevel].employesstype
                    + "<td class='center'>" + Obj[indexlevel].employessname
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeinscan : "วันหยุด")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_0 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_1 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_2 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_3 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_4 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeoutscan : "วันหยุด")
                    + "<td class='center'>" + (Obj[indexlevel].statusOut_0 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusOut_1 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusOut_2 == "1" && Obj[indexlevel].statusIn_9 === 0 ? "X" : ""
                    + "<td class='center'>" + (Obj[indexlevel].statusOut_3 == "1" ? "X" : "")
                    + "<td class='center'>" + (Obj[indexlevel].statusOut_4 == "1" ? "X" : "")

                    );

                sumallstudent4level += 1;
                sumallstatus_0 += Obj[indexlevel].statusIn_0;
                sumallstatus_1 += Obj[indexlevel].statusIn_1;
                sumallstatus_2 += Obj[indexlevel].statusIn_2;
                sumallstatus_3 += Obj[indexlevel].statusIn_3;
                sumallstatus_4 += Obj[indexlevel].statusIn_4;
                sumallstatusout_0 += Obj[indexlevel].statusOut_0;
                sumallstatusout_1 += Obj[indexlevel].statusOut_1;
                sumallstatusout_2 += (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].statusOut_2 : 0);
                sumallstatusout_3 += Obj[indexlevel].statusOut_3;
                sumallstatusout_4 += Obj[indexlevel].statusOut_4;
                HtmlTable.append(HtmlRow);
            })
            HtmlTable.append("<tr><td colspan=4 class='right'>รวมทั้งสิ้น<td class='center'>" + sumallstudent4level
                + "<td class='center'>" + sumallstatus_0 + "<td class='center'>" + sumallstatus_1
                + "<td class='center'>" + sumallstatus_2
                + "<td class='center'>" + sumallstatus_3
                + "<td class='center'>" + sumallstatus_4
                + "<td class='center'>" + sumallstudent4level
                + "<td class='center'>" + sumallstatusout_0 + "<td class='center'>" + sumallstatusout_1
                + "<td class='center'>" + sumallstatusout_2
                + "<td class='center'>" + sumallstatusout_3
                + "<td class='center'>" + sumallstatusout_4
            );
            $("body").mLoading('hide');
        });
    } else {


        Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=6>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
         + `<tr><th colspan=6 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'>รายงานสรุปสถิติการเข้าทำงาน</th></tr> `
         + `<tr><th colspan=6 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>"`
         + `<tr><th colspan=6 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
         + `<tr><th colspan=6 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
         + `<tr><th  style="text-align: right; border-width:0px;"colspan=7><br></th>`);

        HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'><td id='headder' rowspan='2'>ลำดับ<td id='headder'rowspan='2'>วันที่"
            + "<td id='headder' rowspan='2'>จำนวนพนักงาน(คน)<td id='headder'colspan='3'>สถานะ");
        HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
            + "<td id='headder'>ตรงเวลา<td id='headder'>สาย<td id='headder'>ขาด");

        $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school01teacher" + sortday() + sortuser() + sortstatus(), {}, function (Obj) {
            var sumallstudent4level = 0;
            var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0;
            $.each(Obj, function (index) {
                HtmlTable.append("<tr><td class='center'>" + (index + 1) + "<td style='color:royalblue;' onclick='Reports02(" + '"' + Obj[index].day + '"' + ")' class='center'>" + Obj[index].day + "<td class='center'>" + Obj[index].employessnumber + "<td class='center'>" + Obj[index].status_0 + "<td class='center'>" + Obj[index].status_1 + "<td class='center'>" + Obj[index].status_2);
                sumallstudent4level += Obj[index].employessnumber;
                sumallstatus_0 += Obj[index].status_0;
                sumallstatus_1 += Obj[index].status_1;
                sumallstatus_2 += Obj[index].status_2;
            });
            HtmlTable.append("<tr><td><td class='right'>รวมทั้งสิ้น<td class='center'>" + sumallstudent4level
              + "<td class='center'>" + sumallstatus_0 + "<td class='center'>" + sumallstatus_1
              + "<td class='center'>" + sumallstatus_2);
            $("body").mLoading('hide');
        });
    }

}

function Reports02(day) {
    $("#backpage").removeClass("hidden");
    $("body").mLoading();
    var dt = new Date();
    var Header = $("#myHeader");
    var HtmlTable = $("#myTable");
    Header.html("");
    HtmlTable.html("");
    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan=11>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
              + `<tr><th colspan=11 style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'>รายงานสรุปสถิติการเข้าทำงาน</th></tr> `
              + `<tr><th colspan=11 style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>"`
              + `<tr><th colspan=11 style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
              + `<tr><th colspan=11 style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
              + `<tr><th  style="text-align: right; border-width:0px;"colspan=7><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
                     + "<td id= 'headder' rowspan= '2' style= 'width:5%;' > ลำดับ <td  id= 'headder' style= 'width:10%'rowspan= '2' > ประเภทบุคลากร"
                     + "<td id='headder'rowspan='2'>ชื่อ - นามสกุล<td id='headder'rowspan='2'>เวลาเข้างาน<td id='headder'colspan='5'style='width:15%'>สถานะ</td><td id='headder'rowspan='2'>เวลาเลิกงาน<td id='headder'colspan='5'style='width:15%'>สถานะ</td>");
    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด<td id='headder'style='width:5%'>กิจ<td id='headder'style='width:5%'>ป่วย"
        + "<td id='headder'style='width:5%'>ตรง(หลัง)เวลา<td id='headder'style='width:5%'>ก่อนเวลา<td id='headder'style='width:5%'>ขาด<td id='headder'style='width:5%'>กิจ<td id='headder'style='width:5%'>ป่วย");

    $.get("/App_Logic/Report/ReportCome2Schoolteacher.ashx?mode=reportscome2school02teacher&day=" + day + sortuser() + sortstatus(), {}, function (Obj) {
        reports_data = Obj;

        var sumallstudent4level = 0;
        var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0, sumallstatus_3 = 0, sumallstatus_4 = 0;
        var sumallstatusout_0 = 0, sumallstatusout_1 = 0, sumallstatusout_2 = 0, sumallstatusout_3 = 0, sumallstatusout_4 = 0;
        $.each(Obj, function (indexlevel) {
            var HtmlRow = "";
            HtmlRow += "<tr><td class='center' >" + (indexlevel + 1)
                + "<td class='center'>" + Obj[indexlevel].employesstype
                + "<td class='center'>" + Obj[indexlevel].employessname
                + "<td class='center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeinscan : "วันหยุด")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_0 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_1 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_2 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_3 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_4 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusIn_9 === 0 ? Obj[indexlevel].timeoutscan : "วันหยุด")
                + "<td class='center'>" + (Obj[indexlevel].statusOut_0 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusOut_1 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusOut_2 == "1" && Obj[indexlevel].statusIn_9 === 0 ? "X" : ""
                + "<td class='center'>" + (Obj[indexlevel].statusOut_3 == "1" ? "X" : "")
                + "<td class='center'>" + (Obj[indexlevel].statusOut_4 == "1" ? "X" : ""));

            sumallstudent4level += 1;
            sumallstatus_0 += Obj[indexlevel].statusIn_0;
            sumallstatus_1 += Obj[indexlevel].statusIn_1;
            sumallstatus_2 += Obj[indexlevel].statusIn_2;
            sumallstatus_3 += Obj[indexlevel].statusIn_3;
            sumallstatus_4 += Obj[indexlevel].statusIn_4;
            sumallstatusout_0 += Obj[indexlevel].statusOut_0;
            sumallstatusout_1 += Obj[indexlevel].statusOut_1;
            sumallstatusout_2 += Obj[indexlevel].statusOut_2;
            sumallstatusout_3 += Obj[indexlevel].statusOut_3;
            sumallstatusout_4 += Obj[indexlevel].statusOut_4;
            HtmlTable.append(HtmlRow);
        })
        HtmlTable.append("<tr><td><td><td class='right'>รวมทั้งสิ้น<td class='center'>" + sumallstudent4level
            + "<td class='center'>" + sumallstatus_0 + "<td class='center'>" + sumallstatus_1
            + "<td class='center'>" + sumallstatus_2
            + "<td class='center'>" + sumallstatus_3
            + "<td class='center'>" + sumallstatus_4
            + "<td class='center'>" + sumallstudent4level
            + "<td class='center'>" + sumallstatusout_0 + "<td class='center'>" + sumallstatusout_1
            + "<td class='center'>" + sumallstatusout_2
            + "<td class='center'>" + sumallstatusout_3
            + "<td class='center'>" + sumallstatusout_4
        );
        $("body").mLoading('hide');
    });
}

function sortday() {
    var start = $("#txtstart").val();
    var end = $("#txtend").val();
    var sort = "&day=" + start + "&end=" + end;
    return sort;
}
function sortuser() {
    var userid = $("#txtid").val();
    var sort = "&userid=" + userid;
    return sort;
}

function sortstatus() {
    var status = $("select[id*=status] option:selected").val();
    var sort = "&status=" + status;
    return sort;
}