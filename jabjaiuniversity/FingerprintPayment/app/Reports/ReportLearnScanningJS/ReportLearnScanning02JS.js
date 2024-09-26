var availableValuestudent = [];
$studentid = "";
$(function () {
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    $('#ctl00_MainContent_ddlsublevel').change(function () {
        $('input[id*=txtSubLV2ID]').val("");
        getListSubLV2();
    });

    getListSubLV2();
    getListTrem();

    $('select[id*=ddlSubLV2]').change(function () {
        GetSchedule();
        getliststudent();
    });

    $("select[id*=semister]").change(function () {
        getplane();
    })

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
            $studentid = ui.item.studentid;
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#txtid").val("");
        }
    });


    $("#ctl00_MainContent_ddlyear").change(function () {
        getListTrem();
    })
    $("#exportfile").click(function () {
        if ($("#txtname").val() !== "") {
            var dt = new Date();
            $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการเข้าห้องเรียนรายบุคคล', fileName: "รายงานสรุปสถิติการเข้าห้องเรียนรายบุคคล " + $("#txtname").val() + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        }

    });
});

function getplane() {
    $('#plane option').remove();
    PageMethods.SearchPlane($("select[id*=semister]").val(), $("select[id*=ddlSubLV2]").val(), function (result) {
        result = $.parseJSON(result);
        $('select[id*=plane]')
            .append($("<option></option>")
                .attr("value", "")
                .text("ทั้งหมด"));
        $.each(result, function (index) {
            $('#plane').append($('<option>', {
                value: result[index].value,
                text: result[index].text
            }));
        });
    }, function (mgserror) {
        console.log(mgserror);
    })
}

function getListSubLV2() {
    var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
    var sSubLV = $('#ctl00_MainContent_ddlsublevel option:selected').text();
    $('select[id*=ddlSubLV2] option').remove();
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
    }).done(function () {
        getplane();
        getliststudent();
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
            getplane();
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
                    value: objjson[index].sID,
                    studentid: objjson[index].studentid
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function reportHeader() {
    if ($("#txtid").val() === "") {
        $("#modalpopupdatabig").modal("show");
        $("#modalpopupdatabig-content").html("<h1>กรุณากรอกชื่อนักเรียน");
        $("#modalpopupdatabig-header").html("<h1>ประกาศจากระบบ");
    }
    if ($('#ctl00_MainContent_ddlSubLV2 option:selected').val() === "") {
        alert("");
    }
    var dt = new Date();

    var data = {
        "student_id": $("#txtid").val(), "status": "",
        "daystart": $("#txtstart").val(), "dayend": $("#txtend").val(),
        "term": $("select[id*=semister]").val(), "level2": $("select[id*=ddlSubLV2]").val(),
        "plane_id": $("#plane").val()
    };
    var Status_0 = 0, Status_3 = 0;
    var Status_1 = 0, Status_4 = 0;
    var Status_2 = 0, Status_5 = 0;
    PageMethods.SearchReports(data,
        function (result) {
            var Obj = $.parseJSON(result);
            $("#myTable").html("");
            var Header = $("#myHeader");
            Header.html("");
            var HtmlTable = $("#myTable");
            Header.append("<tr ><td style='font-weight: bold;text-align: center;font-size:26px;border-width:0px;'id='school' colspan=8>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
                + "<tr style='font-size:24px;font-weight: bold;'><td colspan=8 style='text-align: center;border-width:0px;'>รายงานสรุปสถิติการเข้าเรียนรายบุคคล</td></tr>"
                + `<tr><td colspan=8 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
                + `<tr><td colspan=8 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
                + `<tr style='font-size:20px;font-weight: bold;'><td colspan=1 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </td><td style='width:30%;border-width:0px;'>&nbsp;` + $("select[id*=ddlyear] option:selected").text()
                + `<td style='text-align: right;width:20%;border-width:0px;'>เทอม : </td><td colspan=5 style='width:30%;border-width:0px;'>&nbsp;` + $("select[id*=semister] option:selected").text()
                + `<tr style='font-weight: bold;font-size:20px;'><td colspan=1 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </td><td style='width:30%;border-width:0px;'>&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
                + `<td style='text-align: right;width:20%; border-width:0px;'>ชั้นเรียน : </td><td colspan=5 style='width:30%;border-width:0px;'>&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()
                + `<tr style='font-weight: bold;font-size:20px;'><td colspan=1 style='text-align: right;width:20%;border-width:0px;'>รหัสนักเรียน : </td><td style='width:30%;border-width:0px;'>&nbsp;` + $studentid
                + `<td style='text-align: right;width:20%; border-width:0px;'>ชื่อ-นามสกุล : </td><td colspan=5 style='width:30%;border-width:0px;'>&nbsp;` + $("#txtname").val()
                + `<tr ><td colspan=8><br></td>)`);

            HtmlTable.append("<tr id='headder' style='font-size:20px;font-weight: bold;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'><td id='headder' style='width:5%'>ลำดับที่<td id='headder' style='width:20%'>รหัสวิชา"
                + "<td id='headder' style='width:50%'>ชื่อวิชาเวลา<td id='headder' style='width:5%'>เข้าตรง"
                + "<td id='headder' style='width:5%'>สาย<td id='headder' style='width:5%'>ขาด<td id='headder' style='width:5%'>ลาป่วย<td id='headder' style='width:5%'>ลากิจ"
                + "<td id='headder' style='width:50%'>กิจกรรม");

            $.each(Obj, function (index, data) {

                Status_0 += Obj[index]["Status_0"];
                Status_1 += Obj[index]["Status_1"];
                Status_2 += Obj[index]["Status_2"];
                Status_3 += Obj[index]["Status_3"];
                Status_4 += Obj[index]["Status_4"];
                Status_5 += Obj[index]["Status_5"];

                HtmlTable.append("<tr style='text-align: center;'><td style='width:5%'>" + (index + 1) + "<td style='width:10%'>" + Obj[index]["courseCode"]
                    + "<td ><a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"\")'>" + Obj[index]["planename"]
                    + "<td style='width:10%' >" + (Obj[index]["Status_0"] === 0 ? Obj[index]["Status_0"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"0\")'>" + Obj[index]["Status_0"])
                    + "<td style='width:10%' >" + (Obj[index]["Status_1"] === 0 ? Obj[index]["Status_1"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"1\")'>" + Obj[index]["Status_1"])
                    + "<td style='width:10%' >" + (Obj[index]["Status_2"] === 0 ? Obj[index]["Status_2"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"2\")'>" + Obj[index]["Status_2"])
                    + "<td style='width:10%' >" + (Obj[index]["Status_3"] === 0 ? Obj[index]["Status_3"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"3\")'>" + Obj[index]["Status_3"])
                    + "<td style='width:10%' >" + (Obj[index]["Status_4"] === 0 ? Obj[index]["Status_4"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"4\")'>" + Obj[index]["Status_4"])
                    + "<td style='width:10%' >" + (Obj[index]["Status_5"] === 0 ? Obj[index]["Status_5"] : "<a onclick='GetDailyReport(\"" + Obj[index]["planeid"] + "\",\"5\")'>" + Obj[index]["Status_5"]));

                $("#status01").html(Status_0);
                $("#status02").html(Status_1);
                $("#status03").html(Status_2);
            });

            HtmlTable.append("<tr  style='font-weight: bold;'><td colspan='3' class='text-right'>รวม"
                + "<td style='text-align: center;'>" + Status_0
                + "<td style='text-align: center;'>" + Status_1
                + "<td style='text-align: center;'>" + Status_2
                + "<td style='text-align: center;'>" + Status_3
                + "<td style='text-align: center;'>" + Status_4
                + "<td style='text-align: center;'>" + Status_5);
        },
        function () {

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

function GetDailyReport(plane_id, status) {
    if ($('#ctl00_MainContent_ddlSubLV2 option:selected').val() === "") {
        alert("");
    }
    var nTerm = $("select[id*=semister]").val();
    var sublevel2 = $("select[id*=ddlSubLV2]").val();
    var daystart = $("#txtstart").val();
    var dayend = $("#txtend").val();
    var Status_0 = 0, Status_3 = 0;
    var Status_1 = 0, Status_4 = 0;
    var Status_2 = 0, Status_5 = 0;
    var dt = new Date();
    var Header = $("#myHeader");
    Header.html("");
    var HtmlTable = $("#myTable");
    HtmlTable.html("");
    Header.append("<tr><td style='text-align: center;font-size:26px;border-width:0px;font-weight: bold;'id='school' colspan=11>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + "<tr style='font-weight: bold;font-size:24px;><td colspan=11 style='text-align: center;border-width:0px;'>รายงานสรุปสถิติการเข้าเรียนรายวิชา (รายบุคคล)</td></tr>"
        + `<tr><td colspan=11 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><td colspan=11 style='font-weight: bold;text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr ><td colspan=1 style='text-align: right;width:20%;border-width:0px;font-weight: bold;'>ปีการศึกษา : </td><td style='font-weight: bold;width:30%;border-width:0px;' colspan=2>&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<td style='text-align: right;width:20%;border-width:0px;font-weight: bold;'>เทอม : </td><td colspan=7 style='width:30%;border-width:0px;font-weight: bold;'>&nbsp;` + $("select[id*=semister] option:selected").text()
        + `<tr style='font-size:20px;font-weight: bold;'><td colspan=1 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </td><td style="width:30%;border-width:0px;" colspan=2>&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
        + `<td style='text-align: right;width:20%; border-width:0px;'>ชั้นเรียน : </td><td colspan=7 style='width:30%;border-width:0px;'>&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()
        + `<tr style='font-size:20px;font-weight: bold;'><td colspan=1 style='text-align: right;width:20%;border-width:0px;'>รหัสนักเรียน : </td><td style="width:30%;border-width:0px;" colspan=2>&nbsp;` + $studentid
        + `<td style='text-align: right;width:20%;font-weight: bold; border-width:0px;'>ชื่อ-นามสกุล : </td><td colspan=7 style="width:30%;border-width:0px;">&nbsp;` + $("#txtname").val()
        + `<tr ><td colspan=11><br></td>)`);

    HtmlTable.append("<tr id='headder' style='font-size:20px;font-weight: bold;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'><td id='headder' style='width:5%'>ลำดับที่<td id='headder' style='width:15%'>รหัสวิชา"
        + "<td  id='headder'style='width:30%'>ชื่อวิชา<td id='headder' style='width:10%'>วันที่<td id='headder' style='width:10%'>เวลา<td id='headder' style='width:5%'>เข้าตรง"
        + "<td  id='headder' style='width:5%'>สาย<td id='headder' style='width:5%'>ขาด<td id='headder' style='width:5%'>ลาป่วย<td id='headder' style='width:5%'>ลากิจ"
        + "<td  id='headder'style='width:5%'>กิจกรรม");

    var data = {
        "student_id": $("#txtid").val(), "status": status,
        "daystart": $("#txtstart").val(), "dayend": $("#txtend").val(),
        "term": $("select[id*=semister]").val(), "level2": $("select[id*=ddlSubLV2]").val(),
        "plane_id": plane_id
    };

    PageMethods.SearchReports_Detail(data,
        function (result) {
            var Obj = $.parseJSON(result);
            var rowspan = Obj.length;
            $.each(Obj, function (index, data) {

                Status_0 += Obj[index]["Status_0"];
                Status_1 += Obj[index]["Status_1"];
                Status_2 += Obj[index]["Status_2"];
                Status_3 += Obj[index]["Status_3"];
                Status_4 += Obj[index]["Status_4"];
                Status_5 += Obj[index]["Status_5"];

                HtmlTable.append("<tr style='text-align: center;'><td >" + (index + 1)
                    + (index === 0 ? "<td rowspan=" + rowspan + ">" + Obj[index]["planeid"] + "<td rowspan=" + rowspan + ">" + Obj[index]["planename"] : "")
                    + "<td >" + Obj[index]["dScan"]
                    + "<td >" + Obj[index]["timescan"]
                    + "<td >" + Obj[index]["Status_0"]
                    + "<td >" + Obj[index]["Status_1"]
                    + "<td >" + Obj[index]["Status_2"]
                    + "<td >" + Obj[index]["Status_3"]
                    + "<td >" + Obj[index]["Status_4"]
                    + "<td >" + Obj[index]["Status_5"]);
            });
            HtmlTable.append("<tr  style='font-weight: bold;'><td colspan='5' class='text-right'>รวม"
                + "<td style='text-align: center;'>" + Status_0
                + "<td style='text-align: center;'>" + Status_1
                + "<td style='text-align: center;'>" + Status_2
                + "<td style='text-align: center;'>" + Status_3
                + "<td style='text-align: center;'>" + Status_4
                + "<td style='text-align: center;'>" + Status_5);
        });
}


