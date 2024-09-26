var availableValuestudent = [];
var sPlaneID_Name = "";
var planeid_excel = 0;
var day_excel;
$(function () {
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    $('#ctl00_MainContent_ddlsublevel').change(function () {
        $('input[id*=txtSubLV2ID]').val("");
        getListSubLV2();
        getliststudent();
    });

    getListSubLV2();

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

    getListTrem();
    //$.get("/App_Logic/ReportLearnScanning.ashx", "", function (msg) {
    //    console.log(msg);
    //})

    $("#exportfile").click(function () {
        if (planeid_excel == 0) {
            var dt = new Date();
            $('#example').tableExport({
                type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าเรียน-เข้าสอน', fileName: "รายงานสรุปสถิติการเข้าเรียน-เข้าสอน " + " ปีการศึกษา "
                + $("select[id*=ddlyear] option:selected").text() + " เทอม " + $("select[id*=semister] option:selected").text() + " ชั้นเรียน " + $("select[id*=ddlsublevel] option:selected").text().trim() + "/" + $("select[id*=ddlSubLV2]").val()
                + " ประจำวันที่ " + day_excel + " " + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds()
            });
        }
        else if (sPlaneID_Name != "") {
            var dt = new Date();
            $('#example').tableExport({
                type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าเรียน-เข้าสอน', fileName: "รายงานสรุปสถิติการเข้าเรียน-เข้าสอน " + " ปีการศึกษา "
                + $("select[id*=ddlyear] option:selected").text() + " เทอม " + $("select[id*=semister] option:selected").text() + " ชั้นเรียน " + $("select[id*=ddlsublevel] option:selected").text().trim() + "/" + $("select[id*=ddlSubLV2]").val()
                + " รหัสวิชา " + sPlaneID_Name + " " + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds()
            });
        } else {
            var dt = new Date();
            $('#example').tableExport({
                type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าเรียน-เข้าสอน', fileName: "รายงานสรุปสถิติการเข้าเรียน-เข้าสอน " + " ปีการศึกษา "
                + $("select[id*=ddlyear] option:selected").text() + " เทอม " + $("select[id*=semister] option:selected").text() + " ชั้นเรียน " + $("select[id*=ddlsublevel] option:selected").text().trim() + "/" + $("select[id*=ddlSubLV2]").val() + " ระหว่างวันที่ " + $("#txtstart").val() + "ถึง " + $("#txtend").val() + " " + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds()
            });
        }


    });
    $("#ctl00_MainContent_ddlyear").change(function () {
        getListTrem();
    })

});

function getListSubLV2() {
    //                alert($('#ctl00_MainContent_ddlSubLV option:selected').val())
    var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
    var sSubLV = $('#ctl00_MainContent_ddlsublevel option:selected').text();
    $('select[id*=ddlSubLV2] option').remove();
    //$('select[id*=ddlSubLV2]')
    //    .append($("<option></option>")
    //        .attr("value", "")
    //        .text("ทั้งหมด"));
    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        success: function (msg) {

            $.each(msg, function (index) {
                $('select[id*=ddlSubLV2]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(msg[index].nTSubLevel2));
            });
        }
    });
}

function getListTrem() {
    var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
    var YearNumber = $('#ctl00_MainContent_ddlyear option:selected').text();
    $("#ctl00_MainContent_semister option").remove();
    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[id*=semister]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTerm)
                        .text(msg[index].sTerm));
            });
        }
    });
}

function getliststudent() {
    availableValuestudent = [];
    $.ajax({
        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
        $('#ctl00_MainContent_ddlsublevel option:selected').val() + "&nsublevel=" + $('select[id*=ddlSubLV2] option:selected').val(),
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName,
                    value: objjson[index].sID
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function reportHeader() {
    if ($('#ctl00_MainContent_ddlSubLV2 option:selected').val() === "") {
        alert("");
    }
    var nTerm = $("select[id*=semister]").val();
    var planeid = $("select[id*=ddlPlaneID]").val();
    var sublevel2 = $("select[id*=ddlSubLV2]").val();
    var daystart = $("#txtstart").val();
    var dayend = $("#txtend").val();
    var Status_0 = 0;
    var Status_1 = 0;
    var Status_2 = 0;
    var dt = new Date();
    $.get("/App_Logic/ReportLearnScanning.ashx", { "mode": "reportmain", "nTerm": nTerm, "sublevel2": sublevel2, "day": daystart, "dayend": dayend }, function (Obj) {
        $("#myTable").html("");
        var JArryData = Obj["Data"];
        var HtmlTable = $("#myTable");

        var Header = $("#myHeader");
        Header.html("");
        HtmlTable.html("");
        Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;font-weight: bold;'id='school' colspan=10 >" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
            + "<tr><th colspan=10 style='text-align: center;border-width:0px;font-size:24px;font-weight: bold;'>สถิติการการเข้า-ออกห้องเรียน</th></tr>"
            + `<tr><th colspan=10 style='text-align: right;font-size:20px;border-width:0px;font-weight: bold;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
            + `<tr><th colspan=10 style='text-align: right;font-size:20px;border-width:0px;font-weight: bold;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
            + `<tr><th colspan=2 style="text-align: right;width:20%;border-width:0px;;font-size:20px;font-weight: bold;">ปีการศึกษา : </th><th style="width:30%;border-width:0px;font-size:20px;font-weight: bold;">&nbsp;` + $("select[id*=ddlyear] option:selected").text()
            + `<td style='text-align: right;width:20%;border-width:0px;;font-size:20px;font-weight: bold;'>เทอม : </td><td colspan=6 style="width:30%;border-width:0px;font-size:20px;font-weight: bold;">&nbsp;` + $("select[id*=semister] option:selected").text()
            + `<tr><th colspan=2 style="font-size:20px;text-align: right;width:20%;border-width:0px;font-weight: bold;">ระดับชั้นเรียน : </th><th style="width:30%;border-width:0px;font-size:20px;font-weight: bold;">&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
            + `<th style="text-align: right;width:20%; border-width:0px;font-size:20px;font-weight: bold;">ชั้นเรียน : </th><th colspan=6 style="width:30%;border-width:0px;font-size:20px;font-weight: bold;">&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()
        + `<tr><th  style="text-align: right; border-width:0px;"colspan=10><br></th>`);
        $.each(JArryData, function (indexData, data) {
            var JArryDate = Obj["Data"][indexData];
            JArryDate.DateScan;
            var JArry = JArryDate.DataScan;
            if (JArry.length > 0) {
                var SumStatus0_4_Day = 0;
                var SumStatus1_4_Day = 0;
                var SumStatus2_4_Day = 0;
                var SumStatus3_4_Day = 0;
                var SumStatus4_4_Day = 0;
                var SumStatus5_4_Day = 0;
                $.each(JArry, function (index, s) {
                    if (index === 0) {
                        //   HtmlTable.append("<tr><td class='center'>" + (index + 1));
                        HtmlTable.append("<tr id='headder' style='text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>< td id= 'headder'style= 'width:5%;' > ลำดับ <td id='headder' style='width:10%' >วันที่<td id='headder' style='width:10%'>รหัสวิชา"
                            + "<td id='headder'>ชื่อวิชา<td id='headder' style='width:120px;'>เวลา<td id='headder' style='width:100px;'>ตรงเวลา"
                            + "<td id='headder' style='width:100px;'>สาย<td id='headder' style='width:100px;'>ขาด<td id='headder' style='width:100px;'>ลากิจ<td id='headder' style='width:100px;'>ลาป่วย<td id='headder' style='width:100px;'>กิจกรรม");
                        HtmlTable.append("<tr><td rowspan='" + JArry.length + "'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``)'>" + JArryDate.DateScan
                            + "<td style='text-align: center;'>" + JArry[index]["courseCode"]
                            + "<td><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`)'>" + JArry[index]["sPlaneName"]
                            + "<td style='text-align: center;'>" + JArry[index]["ScheduleTime"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`0`)'>" + JArry[index]["Status_0"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`1`)'>" + JArry[index]["Status_1"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`3`)'>" + JArry[index]["Status_2"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`4`)'>" + JArry[index]["Status_3"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`5`)'>" + JArry[index]["Status_4"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`6`)'>" + JArry[index]["Status_5"]);
                    } else {

                        HtmlTable.append("<tr><td style='text-align: center;'>" + JArry[index]["courseCode"]
                            + "<td><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`)'>" + JArry[index]["sPlaneName"]
                            + "<td style='text-align: center;'>" + JArry[index]["ScheduleTime"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`0`)'>" + JArry[index]["Status_0"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`1`)'>" + JArry[index]["Status_1"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`3`)'>" + JArry[index]["Status_2"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`3`)'>" + JArry[index]["Status_3"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`3`)'>" + JArry[index]["Status_4"]
                            + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,`" + JArry[index]["sScheduleID"] + "`,`3`)'>" + JArry[index]["Status_5"]);
                    }
                    SumStatus0_4_Day += JArry[index]["Status_0"];
                    SumStatus1_4_Day += JArry[index]["Status_1"];
                    SumStatus2_4_Day += JArry[index]["Status_2"];
                    SumStatus3_4_Day += JArry[index]["Status_3"];
                    SumStatus4_4_Day += JArry[index]["Status_4"];
                    SumStatus5_4_Day += JArry[index]["Status_5"];
                });

                HtmlTable.append("<tr><td colspan='4' class='text-right'>รวม"
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`0`)'>" + SumStatus0_4_Day
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`1`)'>" + SumStatus1_4_Day
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`3`)'>" + SumStatus2_4_Day
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`4`)'>" + SumStatus3_4_Day
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`5`)'>" + SumStatus4_4_Day
                    + "<td style='text-align: center;'><a href='#' onclick='GetDailyReport(`dailyreport`,`" + JArryDate.DateScan + "`,``,`6`)'>" + SumStatus5_4_Day);

                Status_0 += SumStatus0_4_Day;
                Status_1 += SumStatus1_4_Day;
                Status_2 += SumStatus2_4_Day;
                //Status_3 += SumStatus3_4_Day;
                //Status_4 += SumStatus4_4_Day;
                //Status_5 += SumStatus5_4_Day;
            }
            $("#status01").html(Status_0);
            $("#status02").html(Status_1);
            $("#status03").html(Status_2);
            //$("#status04").html(Status_3);
            //$("#status05").html(Status_4);
            //$("#status06").html(Status_5);
        });
    });
}

function GetSchedule() {
    var sublevel2 = $('#ctl00_MainContent_ddlSubLV2 option:selected').val();
    var nTerm = $('#ctl00_MainContent_semister option:selected').val();
    $("#ctl00_MainContent_ddlPlaneID option").remove();
    $.get("/App_Logic/dataJSONArray.ashx"
        , { "mode": "grouplistschedule", sublevel2: sublevel2, nTerm: nTerm }
        , function (Obj) {
            $.each(Obj, function (index) {
                $('select[id*=ddlPlaneID]')
                    .append($("<option></option>")
                        .attr("value", Obj[index].id)
                        .text(Obj[index].name));
            });
        });
}

function GetDailyReport(mode, day, planeid, status) {
    planeid_excel = planeid;
    day_excel = day;
    var nTerm = $("select[id*=semister]").val();
    var sublevel2 = $("select[id*=ddlSubLV2]").val();
    var Status_0 = 0;
    var Status_1 = 0;
    var Status_2 = 0;
    var dt = new Date();
    $.get("/App_Logic/ReportLearnScanning.ashx", { "mode": mode, "nTerm": nTerm, "sublevel2": sublevel2, "day": day, "planeid": planeid, "status": status }, function (Obj) {
        $("#myTable").html("");
        var JArryData = Obj["Data"];
        var HtmlTable = $("#myTable");
        var Header = $("#myHeader");
        Header.html("");
        HtmlTable.html("");
        Header.append("<tr><td style='font-weight: bold;text-align: center;font-size:26px;border-width:0px;'id='school' colspan=7>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
            + "<tr><td colspan=7 style='font-weight: bold;text-align: center;border-width:0px;font-size:24px;'>รายงานการเข้า-ออกห้องเรียนประจำวันที่ " + day + "</td></tr>"
            + `<tr><td colspan=7 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
            + `<tr><td colspan=7 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
            + `<tr><td colspan=2 style="font-weight: bold;text-align: right;border-width:0px;font-size:20px;">ปีการศึกษา : </td><td style="font-weight: bold;border-width:0px;font-size:20px;">&nbsp;` + $("select[id*=ddlyear] option:selected").text()
            + `<td style="font-weight: bold;text-align: right;border-width:0px;font-size:20px;">เทอม : </td><td colspan=3 style="font-weight: bold;border-width:0px;font-size:20px;">&nbsp;` + $("select[id*=semister] option:selected").text()
            + `<tr><td colspan=2 style="font-weight: bold;text-align: right;width:20%;border-width:0px;font-size:20px;">ระดับชั้นเรียน : </td><td style="font-weight: bold;border-width:0px;font-size:20px;">&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
            + `<td style="font-weight: bold;text-align: right;border-width:0px;font-size:20px;">ชั้นเรียน : </td><td colspan=3 style="font-weight: bold;border-width:0px;font-size:20px;">&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()

            + `<tr><th  style="font-weight: bold;text-align: right; border-width:0px;"colspan=7><br></th>`);
        $.each(JArryData, function (indexData, data) {
            var JArryDate = Obj["Data"][indexData];

            JArryDate.DateScan;
            var JArry = JArryDate.DataScan;
            if (JArry.length > 0) {
                $.each(JArry, function (index, s) {
                    sPlaneID_Name = s["courseCode"] + " " + s["sPlaneName"] + " ประจำวันที่ " + day;
                    var JArryTeacher = s["TeacherScan"];
                    var JArryUser = s["UserScan"];
                    var rowspan = JArryTeacher.length + JArryUser.length;
                    //$.each(JArryTeacher, function (TeacherIndex, TeacherData) {
                    //    var txtStatusIN = "", txtStatusOUT = "", StyleStatus_In = "", StyleStatus_Out = "";
                    //    switch (TeacherData["Status_IN"].trim()) {
                    //        case "0":
                    //            txtStatusIN = "ตรงเวลา";
                    //            Status_0 += 1;
                    //            StyleStatus_In = "background-color: #3c763d;color: white;";
                    //            break;
                    //        case "1":
                    //            txtStatusIN = "สาย";
                    //            Status_1 += 1;
                    //            StyleStatus_In = "background-color: #8a6d3b;color: white;";
                    //            break;
                    //        case "3":
                    //            txtStatusIN = "ขาด";
                    //            Status_2 += 1;
                    //            StyleStatus_In = "background-color: #a94442;color: white;";
                    //            break;
                    //        case "4":
                    //            txtStatusIN = "ลากิจ";
                    //            Status_0 += 1;
                    //            StyleStatus_In = "background-color: #3c763d;color: white;";
                    //            break;
                    //        case "5":
                    //            txtStatusIN = "ลาป่วย";
                    //            Status_0 += 1;
                    //            StyleStatus_In = "background-color: #3c763d;color: white;";
                    //            break;
                    //        case "6":
                    //            txtStatusIN = "กิจกรรม";
                    //            Status_0 += 1;
                    //            StyleStatus_In = "background-color: #3c763d;color: white;";
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    switch (TeacherData["Status_OUT"].trim()) {
                    //        case "0":
                    //            txtStatusOUT = "ตรงเวลา";
                    //            StyleStatus_Out = "background-color: #3c763d;color: white;";
                    //            break;
                    //        case "2":
                    //            txtStatusOUT = "ออกก่อน";
                    //            StyleStatus_Out = "background-color: #8a6d3b;color: white;";
                    //            break;
                    //        case "3":
                    //            txtStatusOUT = "ขาด";
                    //            StyleStatus_Out = "background-color: #a94442;color: white;";
                    //            break;
                    //        default:
                    //            break;
                    //    }

                    //    HtmlTable.append("<tr id='headder' style='text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>< td id= 'headder'style= 'width:5%;' > ลำดับ<td id='headder' style='width:20%;'>ชื่อวิชา"
                    //        + "<td id='headder' style='width:10%;'>เวลา<td>ชื่อ - นามสกุล"
                    //        + "<td id='headder' style='width:10%;'>เวลาเข้า<td id='headder' style='width:10%;'>สถานะ<td id='headder' style='width:10%;'>เวลาออก<td id='headder' style='width:10%;'>สถานะ");
                    //    //HtmlTable.append("<tr><td rowspan='" + rowspan + "'>" + s["sPlaneID"] + "<br/>" + s["sPlaneName"]
                    //    //        + "<td rowspan='" + rowspan + "'>" + s["ScheduleTime"]
                    //    //        + "<td>" + TeacherData["sName"]
                    //    //        + "<td style='text-align: center;'>" + (TeacherData["Time_IN"] === null ? "-" : TeacherData["Time_IN"].split('.')[0])
                    //    //        + "<td style='text-align: center;" + StyleStatus_In + "'>" + txtStatusIN
                    //    //        + "<td style='text-align: center;'>" + (TeacherData["Time_OUT"] === null ? "-" : TeacherData["Time_OUT"].split('.')[0])
                    //    //        + "<td style='text-align: center;" + StyleStatus_Out + "'>" + txtStatusOUT);
                    //});

                    $.each(JArryUser, function (UserIndex, UserData) {
                        var txtStatusIN = "", txtStatusOUT = "", StyleStatus_In = "", StyleStatus_Out = "";

                        switch (UserData["Status_IN"].trim()) {
                            case "0":
                                txtStatusIN = "ตรงเวลา";
                                Status_0 += 1;
                                StyleStatus_In = "background-color: #3c763d;color: white;";
                                break;
                            case "1":
                                txtStatusIN = "สาย";
                                Status_1 += 1;
                                StyleStatus_In = "background-color: #8a6d3b;color: white;";
                                break;
                            case "3":
                                txtStatusIN = "ขาด";
                                Status_2 += 1;
                                StyleStatus_In = "background-color: #a94442;color: white;";
                                break;
                            case "4":
                                txtStatusIN = "ลากิจ";
                                Status_0 += 1;
                                StyleStatus_In = "background-color: #e8ccec;color: white;";
                                break;
                            case "5":
                                txtStatusIN = "ลาป่วย";
                                Status_0 += 1;
                                StyleStatus_In = "background-color: #0393c9;color: white;";
                                break;
                            case "6":
                                txtStatusIN = "กิจกรรม";
                                Status_0 += 1;
                                StyleStatus_In = "background-color: #99bfe6;color: white;";
                                break;
                            default:
                                break;
                        }

                        switch (UserData["Status_OUT"].trim()) {
                            case "0":
                                txtStatusOUT = "ตรงเวลา";
                                StyleStatus_Out = "background-color: #3c763d;color: white;";
                                break;
                            case "2":
                                txtStatusOUT = "ออกก่อน";
                                StyleStatus_Out = "background-color: #8a6d3b;color: white;";
                                break;
                            case "3":
                                txtStatusOUT = "ขาด";
                                StyleStatus_Out = "background-color: #a94442;color: white;";
                                break;
                            default:
                                break;
                        }
                        //if (JArryTeacher.length === 0 && UserIndex === 0) {
                        if (UserIndex === 0) {
                            HtmlTable.append("<tr id='headder' style='text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>< td id= 'headder'style= 'width:5%;' > ลำดับ<td id='headder' style='width:20%;'>ชื่อวิชา"
                                + "<td id='headder' style='width:10%;'>เวลา<td>ชื่อ - นามสกุล"
                                + "<td id='headder' style='width:10%;'>เวลาเข้า<td id='headder' style='width:10%;'>สถานะ<td id='headder' style='width:10%;'>เวลาออก<td id='headder' style='width:10%;'>สถานะ");
                            HtmlTable.append("<tr><td rowspan='" + rowspan + "'>" + s["courseCode"] + "<br/>" + s["sPlaneName"]
                                + "<td rowspan='" + rowspan + "'>" + s["ScheduleTime"]
                                + "<td>" + UserData["sName"]
                                + "<td style='text-align: center;'>" + (UserData["Time_IN"] === null ? "-" : UserData["Time_IN"].split('.')[0])
                                + "<td style='text-align: center;" + StyleStatus_In + "'>" + txtStatusIN
                                + "<td style='text-align: center;'>" + (UserData["Time_OUT"] === null ? "-" : UserData["Time_OUT"].split('.')[0])
                                + "<td style='text-align: center;" + StyleStatus_Out + "'>" + txtStatusOUT);
                        }
                        else {
                            HtmlTable.append("<tr>"
                                + "<td>" + UserData["sName"]
                                + "<td style='text-align: center;'>" + (UserData["Time_IN"] === null ? "-" : UserData["Time_IN"].split('.')[0])
                                + "<td style='text-align: center;" + StyleStatus_In + "'>" + txtStatusIN
                                + "<td style='text-align: center;'>" + (UserData["Time_OUT"] === null ? "-" : UserData["Time_OUT"].split('.')[0])
                                + "<td style='text-align: center;" + StyleStatus_Out + "'>" + txtStatusOUT);
                        }
                    });
                });
            }
            $("#status01").html(Status_0);
            $("#status02").html(Status_1);
            $("#status03").html(Status_2);
        });
    });
}
