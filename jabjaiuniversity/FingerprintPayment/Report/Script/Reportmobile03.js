var reports_data = [];
var HeaderCSS = " text-align: center !important;vertical-align: middle !important;";
var HeaderCSS_1 = " text-align: center !important;vertical-align: middle !important;";
var RowsCSS = "text-align: center !important; vertical-align: middle !important;";
var RowsCSS_1 = "text-align: center !important; vertical-align: middle !important;";
var RowsCSS_2 = "vertical-align: middle !important;";

var Search = [];
//var Reports_03 = new reports_03();
$(function () {
    $('select[id*=sort_type]').change(function () {
        switch_sort($(this).val());
    });
    switch_sort($('select[id*=sort_type]').val());
});

function switch_sort(sort_type) {
    switch (sort_type) {
        case "0": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); $(".reports_type").hide(); break;
        case "1": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); $(".reports_type").show(); break;
        case "2": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); $(".reports_type").show(); break;
        case "3": $("div[class*=sort_type]").hide(); $(".sort_type" + sort_type).show(); $(".reports_type").show(); break;
    }
}

//function switchExport_excel() {
//    switch (sort_type) {
//        case "0": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $(".reports_type").hide(); break;
//        case "1": $(".sort_type").hide(); $($(".sort_type")[0]).show(); $(".reports_type").show(); break;
//        case "2": $(".sort_type").hide(); $($(".sort_type")[1]).show(); $(".reports_type").show(); break;
//        case "3": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $(".reports_type").show(); $($(".sort_type")[3]).show(); break;
//    }
//}

function getSearch() {
    var report_type = 0;
    if ($('select[id*=sort_type]').val() === "0") {
        report_type = 0;
    }
    else {
        if ($("#reports_type").val() === "0") report_type = 1;
        else if ($("#reports_type").val() === "1") report_type = 2;
        else report_type = 3;
    }
    var dStart, dEnd;
    if ($('select[id*=ddlSubLV2]').val() === "" && (report_type !== 2 && report_type !== 3)) {
        //alert("กรุณาเลือกชั้นเรียน");
        return false;
    }

    if ($('select[id*=sort_type]').val() === "1") {
        dStart = $("#select_month").val() + "/1/" + $("select[id*=select_year]").val();
        dEnd = dStart;
        report_txt += " ประจำเดือน " + $("select[id*=select_month] option:selected").text() + " ปี " + $("select[id*=select_year] option:selected").text();
    }
    else if ($('select[id*=sort_type]').val() === "2") {
        dStart = moment().format('MM/DD/YYYY');
        dEnd = dStart;
        report_txt += " ปีการศึกษา " + $("select[id*=ddlyear] option:selected").text() + " เทอม " + $("select[id*=semister] option:selected").text();
    } else if ($('select[id*=sort_type]').val() === "0") {
        dStart = $("#txtstart1").data("DateTimePicker").date().format('MM/DD/YYYY');
        dEnd = dStart;
    } else if ($('select[id*=sort_type]').val() === "3") {
        dStart = $("#txtstart3").data("DateTimePicker").date().format('MM/DD/YYYY');
        dEnd = $("#txtend3").data("DateTimePicker").date().format('MM/DD/YYYY');
    }

    //$("#txtdaystart").val();
    Search = {
        "term_id": YTF.GetTermID(),//$('select[id*=semister]').val(),
        "level2_id": LCF.GetClassID(),//$('select[id*=ddlSubLV2]').val(),
        "level_id": LCF.GetLevelID(),//$('select[id*=ddlsublevel]').val(),
        "sort_type": $('select[id*=sort_type]').val(),
        "dStart": dStart,
        "dEnd": dEnd,
        "student_id": SAC.GetStudentID(),//$("#txtid").val(),
        "report_type": report_type
    };
    return true;
}

function Getreports(table_name, export_file) {

    if ($('#aspnetForm').valid() == false) {
        //e.preventDefault();
        //e.stopPropagation();
        return false;
    }

    if (getSearch() === true) {
        $("body").mLoading();
        PageMethods.returnlist(Search, function (e) {
            reports_data = e;
            //Reports_03.reports_data = e;
            $("#" + table_name).css("font-size", "16px");

            let checkData = false;
            if (Search.report_type == "1" && e.reportsDatas !== undefined && e.reportsDatas.length > 0) checkData = true;
            if (Search.report_type != "1" && e[0].studentDatas !== undefined && e[0].studentDatas.length > 0) checkData = true;
            if (Search.report_type == "0") checkData = true;

            if (checkData == false) return;

            $('#remark1').hide();
            if (Search.sort_type === "0") {
                $("#btnExport_1").show();
                $("#btnExport_2").hide();
                $('#remark1').show();
                renderHTML_02(table_name, export_file);
            } else {
                if ($("#reports_type").val() === "1") {
                    $("#btnExport_2").show();
                    $("#btnExport_1").hide();
                    //$("#" + table_name).css("font-size", "24px");
                    renderHTML_03(table_name, export_file);
                } else if ($("#reports_type").val() === "0") {
                    renderHTML(table_name, export_file);
                    $("#btnExport_1").show();
                    $("#btnExport_2").hide();
                } else {
                    //alert("type3");
                    $("#btnExport_2").show();
                    $("#btnExport_1").hide();
                    //$("#" + table_name).css("font-size", "24px");
                    renderHTML_04type3(table_name, export_file);
                }
            }

            $("body").mLoading('hide');
        }, function (e) {
            $("body").mLoading('hide');
        });
    }
}

function renderHTML(table_name, export_file) {
    var ExportCSS = "border:.5pt solid windowtext;font-size:13px;";
    $("#" + table_name).html("");
    if (export_file === true) {
        HeaderCSS += ExportCSS;
        HeaderCSS_1 += ExportCSS;
        RowsCSS += ExportCSS;
        RowsCSS_1 += ExportCSS;
    }
    else {
        HeaderCSS = HeaderCSS.replaceAll(ExportCSS, "");
        HeaderCSS_1 = HeaderCSS_1.replaceAll(ExportCSS, "");
        RowsCSS = RowsCSS.replaceAll(ExportCSS, "");
        RowsCSS_1 = RowsCSS_1.replaceAll(ExportCSS, "");
    }

    $("#" + table_name).append("<thead>");
    var thead_01 = "";
    var thead_02 = "";
    var thead_03 = "";
    var Months = reports_data.headerReports;
    //Gen Header Table
    var Column_Index = 0;
    $.each(Months, function (Months_index, Months_values) {
        var Weeks = Months_values.weeks;
        var DaysLength = 0;
        $.each(Weeks, function (Weeks_index, Weeks_values) {
            var Days = Weeks_values.days;
            thead_02 += "<th colspan='" + Days.length + "' style='" + HeaderCSS + "'>" + Weeks_values.Weeks_Id +
                "</th>";
            $.each(Days, function (Days_index, Days_values) {
                DaysLength += 1;
                if (Days_values.Days_Status === "0") {
                    thead_03 += "<th style='" + HeaderCSS + " min-width: 50px; width: 50px;' column='" + Days_values.Days_Id
                        + "' HoliDay_Status=\"" + Days_values.HoliDay_Status
                        + "\" HoliDay_Name=\"" + Days_values.HoliDay_Name + "\" >" + Days_values.Days_Name +
                        "</th>";
                } else {
                    thead_03 += "<th style='" + HeaderCSS_1 + " min-width: 50px; width: 50px;' column='" + Days_values.Days_Id
                        + "' HoliDay_Status=\"" + Days_values.HoliDay_Status
                        + "\" HoliDay_Name=\"" + Days_values.HoliDay_Name + "\" >"
                        + Days_values.Days_Name +
                        "</th>";
                }
            });
        });
        thead_01 += "<th colspan='" + DaysLength + "' style='" + HeaderCSS + "'>" + Months_values.Month_Name +
            "</th>";
    });

    thead_01 = "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>ลำดับ</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>รหัสนักเรียน</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>ชื่อนักเรียน</th>"
        + "<th style='" + HeaderCSS + "'>เดือน</th>"
        + thead_01
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>มาเรียน(วัน)</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>กิจกรรม(วัน)</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>สาย(วัน)</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>ลาป่วย(วัน)</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>ลากิจ(วัน)</th>"
        + "<th rowspan=3 style='" + HeaderCSS + "min-width: 100px;'>ขาด(วัน)</th>";

    //Add Rows Table
    var Student = reports_data.reportsDatas;
    var RowsHtml = "";
    $("#" + table_name).append("<tbody>");
    $.each(Student, function (Student_Index, Student_Values) {
        //fix first row problem SB-8194
        if (Student_Index == 0 && Student.length > 1) {
            var style = "style='padding:0 !important;height:0px !important;line-height:0px !important;'"
            RowsHtml = "<tr class=dummyrow ><td " + style + ">";
            RowsHtml += "<td " + style + " >";
            RowsHtml += "<td colspan=2 " + style + " row='" + Student_Index + "'>";
            var Scan_data = Student_Values.scanDatas;
            $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
                postion = " row='" + Student_Index + "' column='" + Scan_data_Index + "' ";
                RowsHtml += "<td Days_Status=1  background-color: rgb(207, 207, 205);' " + postion + "  " + style + ">";
            });

            RowsHtml += `<td  ${style}>
                <td ${style}>
                <td ${style}>
                <td ${style}>
                <td ${style}>
                <td ${style}>`;

            $("#" + table_name + " tbody").append(RowsHtml);
            RowsHtml = '';
        }

        //end SB-8194
        RowsHtml = "<tr class=normalrow><td style='" + RowsCSS + "'>" + Student_Values.RowsIndex;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Student_Id;
        RowsHtml += "<td style='" + RowsCSS_2 + "' colspan=2 rows='" + (Student_Index + 1) + "' Student-Status=\"" + Student_Values.Student_Status + "\"" +
            " Day-Start=\"" + Student_Values.DayStart + "\"" +
            " Day-End=\"" + Student_Values.DayEnd + "\"" +
            " NoteIn=\"" + Student_Values.NoteIn + "\"" +
            " NoteOut=\"" + Student_Values.NoteOut + "\"" +
            " Student-Status2=\"" + GetRemark(Student_Values.Student_Status2) + "\"" +
            "  >" + Student_Values.Student_Name;
        var Scan_data = Student_Values.scanDatas;
        $.each(Scan_data, function (Scan_data_Index, Scan_data_Values) {
            postion = " row='" + (Student_Index + 1) + "' column='" + Scan_data_Index + "' ";

            let statusIn_export = "-";
            let statusOut_export = "-";

            switch (Scan_data_Values.Scan_Status.trim()) {
                case "0":
                case "1":
                    statusIn_export = "/"; break;
                case "3":
                    statusIn_export = "-"; break;
                default: statusIn_export = Scan_data_Values.Scan_Time; break;
            }

            if (Scan_data_Values.Days_Status === "0") {
                if (Scan_data_Values.Scan_Status.trim() === "1") {
                    RowsHtml += "<td style='" + RowsCSS_1 + " background-color: rgb(255, 242, 0);' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                } else if (Scan_data_Values.Scan_Status.trim() === "0" || Scan_data_Values.Scan_Status.trim() === "") {
                    RowsHtml += "<td style='" + RowsCSS + "' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                } else if (Scan_data_Values.Scan_Status.trim() === "8" || Scan_data_Values.Scan_Status.trim() === "6") {
                    RowsHtml += "<td style='" + HeaderCSS_1 + " background-color: rgb(207, 207, 205);' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                } else if (Scan_data_Values.Scan_Status.trim() === "10") {
                    RowsHtml += "<td style='" + RowsCSS + " background-color: rgb(255, 128, 255);' " + postion + ">ล";//+ Scan_data_Values.Scan_Time;
                } else if (Scan_data_Values.Scan_Status.trim() === "11") {
                    RowsHtml += "<td style='" + RowsCSS + " background-color: rgb(255, 128, 255);' " + postion + ">ป";// + Scan_data_Values.Scan_Time;
                } else if (Scan_data_Values.Scan_Status.trim() === "12") {
                    RowsHtml += "<td style='" + RowsCSS + " background-color: rgb(173,216,230);' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                } else if (Scan_data_Values.Scan_Status.trim() === "99") {
                    RowsHtml += "<td style='" + RowsCSS + " background-color: #449d44;' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                } else {
                    RowsHtml += "<td style='" + RowsCSS_1 + " background-color: rgb(236, 70, 72);' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
                }
            }
            else {
                RowsHtml += "<td Days_Status=1 style='" + HeaderCSS_1 + " background-color: rgb(207, 207, 205);' " + postion + ">" + (export_file == true ? statusIn_export : Scan_data_Values.Scan_Time);
            }
            //+ "(" + Scan_data_Values.Scan_Status + ")";
        });

        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_0;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_1;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_2;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_3;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_4;
        RowsHtml += "<td style='" + RowsCSS + "'>" + Student_Values.Sum_Status_5;

        $("#" + table_name + " tbody").append(RowsHtml);
    });

    if (export_file === true) {
        $("#" + table_name).append("<tfoot>");
        var Colspan = $("#" + table_name + " tbody tr:last td").length + 1;

        //Add Header Table
        $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:20px;'>" + $("input[id*=hdfschoolname]").val());

        switch ($('select[id*=sort_type]').val()) {
            case "0": $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:18px;'>รายงานสรุปการมาเรียนประจำวันที่ " + $("#txtstart1").val());
                break;
            case "1": $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:18px;'>รายงานสรุปการมาเรียนประจำเดือน " + $("#select_month option:selected").text() + " ปี " + $("select[id*=ddlyear] option:selected").text());
                break;
            case "2": $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:18px;'>รายงานสรุปการมาเรียนประจำภาคเรียนที่ " + $("select[id*=semister] option:selected").text() + " ปีการศึกษา " + $("select[id*=ddlyear] option:selected").text());
                break;
            case "3": $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:18px;'>รายงานสรุปการมาเรียนระหว่างวันที่ " + $("#txtstart3").val() + " ถึง " + $("#txtend3").val());
                break;
        }

        $("#" + table_name + " thead").append("<tr><th colspan='2' style='font-size:13px;text-align: right !important;'>ระดับการศึกษา :<td colspan='2' style='font-size:13px;vertical-align:middle;'>" + LCF.GetLevelText() + "<td colspan='" + (Colspan - 8) + "'><th colspan='1' style='font-size:13px;text-align: right !important;'>พิมพ์วันที่ :<td colspan='3' style='font-size:13px;text-align: left !important;vertical-align:middle;'>{day_print}");
        $("#" + table_name + " thead").append("<tr><th colspan='2' style='font-size:13px;text-align: right !important;'>ห้องเรียน  :<td colspan='2' style='font-size:13px;'>" + LCF.GetClassText() + "<td colspan='" + (Colspan - 8) + "'><th colspan='1' style='font-size:13px;text-align: right !important;'>เวลา :<td colspan='3' style='font-size:13px;text-align: left !important;'>{time_print}");
        $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "'>");
        //Add Footer Table
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + Colspan + "'>"); $("#" + table_name + " tfoot").append("<tr><td colspan='2' rowspan='11' style='vertical-align:top;font-size:13px;'>หมายเหตุ<td style='background-color: rgb(236, 70, 72);'><td style='font-size:13px;'>ขาดเรียน<td style='background-color: rgb(207, 207, 205);' style='font-size:13px;'><td colspan='2' style='font-size:13px;'>วันหยุดประจำสัปดาห์<td colspan='" + (Colspan - (8 + 11)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>ลงชื่อ..................................................ครูที่ปรึกษา<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>ลงชื่อ..................................................รองผู้อำนวยการ");
        $("#" + table_name + " tfoot").append("<tr><td style='background-color: rgb(255, 242, 0);' style='font-size:13px;'><td style='font-size:13px;'>มาสาย<td style='background-color: rgb(173,216,230);'><td colspan='2' style='font-size:13px;'>วันกิจกรรม<td colspan='" + (Colspan - 9) + "'>");
        $("#" + table_name + " tfoot").append("<tr><td style='background-color: rgb(255, 128, 255);' style='font-size:13px;'><td style='font-size:13px;'>ลา<td colspan='" + (Colspan - (8 + 8)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>(.................................................)<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>(.................................................)");
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + (Colspan - 1) + "'><tr><td colspan='7'><td colspan='" + (Colspan - (8 + 13)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>ลงชื่อ..................................................ครูที่ปรึกษา<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>ลงชื่อ..................................................ผู้อำนวยการโรงเรียน");
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + (Colspan - 1) + "'><tr><td colspan='7'><td colspan='" + (Colspan - (8 + 13)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>(.................................................)<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>(.................................................)");
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + (Colspan - 1) + "'><tr><td colspan='7'><td colspan='" + (Colspan - (8 + 13)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>ลงชื่อ..................................................หัวหน้าระดับ<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + (Colspan - 1) + "'><tr><td colspan='7'><td colspan='" + (Colspan - (8 + 13)) + "'><td colspan='5' style='font-size:13px;text-align: center !important;'>(.................................................)<td colspan=2><td colspan='5' style='font-size:13px;text-align: center !important;'>");
    }

    $("#" + table_name + " thead").append("<tr>" + thead_01);
    $("#" + table_name + " thead").append("<tr><th style='" + HeaderCSS + "'>สัปดาห์" + thead_02);
    $("#" + table_name + " thead").append("<tr><th style='" + HeaderCSS + "'>วันที่" + thead_03);


    //var startCol = +($("#" + table_name + " thead tr th[HoliDay_Status='false']:first").attr('column'));

    var StudentStatus = $("#" + table_name + " tbody tr.normalrow td[student-status='Q']");
    $.each(StudentStatus, function (index, s) {
        var columns = $("#" + table_name + " thead tr th[column]").length;

        var colstatr = parseInt($(s).attr("Day-End"));
        var rows = parseInt($(s).attr("rows"));
        var NoteOut = $(s).attr("noteout");
        var remark = $(s).attr("Student-Status2");
        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").attr("style", "line-height:0px !important;background-color:#fff;text-align:center;");
        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").attr("colspan", columns - colstatr/* - (startCol - 1 )*/);
        
        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").html(remark+" สาเหตุ : " + NoteOut);
        for (var i = colstatr + 1; i < columns; i++) {
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + i + "]").remove();
        }
    });

    StudentStatus = $("#" + table_name + " tbody tr.normalrow td[student-status='G']");
    $.each(StudentStatus, function (index, s) {
        var columns = $("#" + table_name + " thead tr th[column]").length;
        var colstatr = parseInt($(s).attr("Day-End"));
        var rows = parseInt($(s).attr("rows"));
        var NoteOut = $(s).attr("noteout");

        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").attr("style", "line-height:0px !important;background-color:#fff;text-align:center;");
        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").attr("colspan", columns - colstatr);
        //$("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").css("background-color", "#fff");
        $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + colstatr + "]").html("สำเร็จปีการศึกษา : " + NoteOut);
        for (var i = colstatr + 1; i < columns; i++) {
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + i + "]").remove();
        }
    });

    StudentStatus = $("#" + table_name + " tbody tr.normalrow td[student-status='C']");
    $.each(StudentStatus, function (index, s) {
        var columns = $("#" + table_name + " thead tr th[column]").length;
        var Day_In = parseInt($(s).attr("Day-Start"));
        var Day_Out = parseInt($(s).attr("Day-End"));
        var rows = parseInt($(s).attr("rows"));
        var NoteIn = $(s).attr("notein");
        var NoteOut = $(s).attr("noteout");

        //if 1 column make text unreadable
        if (Day_In > 0) {
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").attr("style", "line-height:0px !important;background-color:#fff;text-align:center;");
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").attr("colspan", Day_In);
            //$("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").css("background-color", "#fff");

            if (Day_In > 2) {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").html("ย้ายห้องมากจาก " + NoteIn);
                for (var i = 1; i < Day_In; i++) {
                    $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + i + "]").remove();
                }
            } else {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=0]").html(" ");
            }

        }


        //if 1 column make text unreadable
        if (Day_Out > 0) {
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").attr("style", "line-height:0px !important;background-color:#fff;text-align:center;");
            $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").attr("colspan", columns - Day_Out);
            //$("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").css("background-color", "#fff");

            if (Day_Out > 2) {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").html("ย้ายห้องไปยัง " + NoteOut);
                for (var j = Day_Out + 1; j < columns; j++) {
                    $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + j + "]").remove();
                }
            } else {
                $("#" + table_name + " tbody tr.normalrow td[row='" + rows + "'][column=" + Day_Out + "]").html(" ");
            }
           
        }
    });

    var HoliDay_Status = $("#" + table_name + " thead tr th[HoliDay_Status='true']");
    var rowspan = $("#" + table_name + " tbody tr").length;
    var arrRows = $("#" + table_name + " tbody tr.normalrow td:nth-child(3)[student-status!='0']")
        .map(function () { return +$(this).attr('rows'); }).get(); // ['1', '7']
    arrRows.push(rowspan);

    $.each(HoliDay_Status, function (index, s) {
        var column = $(this).attr("column");
        var HoliDay_Name = $(this).attr("HoliDay_Name");

        var rows_Index = 0;

        var _start = -1;
        var _end = -1;
        var _isWrite = false;
        for (var rows_Index = 0; rows_Index <= rowspan; rows_Index++) {
            //$("#" + table_name + " tbody tr td[row=" + (rows_Index +1 ) + "][column=" + (column - 1) + "]").attr('colspan');
            if (_start == -1) _start = rows_Index;
            if (_end == -1) _end = rows_Index;

            if (arrRows.includes(rows_Index)) {
                _end = rows_Index;

                if (_end == _start) continue;

                var _rowspan = (_end - _start);
                var rows = $("#" + table_name + " tbody tr td[row=" + _start + "][column=" + (column - 1) + "]");
                $(rows).attr("rowspan", _rowspan);
                $(rows).attr("style", "line-height:0px !important;max-width:50px;background-color:#fff;");

                if (_rowspan > 5 && _isWrite == false) {

                    $(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
                    _isWrite = true;

                }
                else {
                    $(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >&nbsp;");
                }

                if (_rowspan > 1)
                    for (var i = _start + 1; i < _end; i++) {
                        $("#" + table_name + " tbody tr td[row=" + i + "][column=" + (column - 1) + "]:not([colspan])").remove();
                    }

                _start = -1;
                _end = -1;
                //var rows = $("#" + table_name + " tbody tr td[row=" + rows_Index + "][column=" + (column - 1) + "]");

                //$("#" + table_name + " tbody tr.normalrow td[column=" + (column - 1) + "]:not([colspan])").remove();
                //$(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
                //$(rows).attr("rowspan", rowspan);
                //$(rows).css("max-width", "50px");
                //$(rows).css("background-color", "#fff");

                //if ((_end - _start)> 1) {

                //}
            }
        }

        //var rows = $("#" + table_name + " tbody tr.dummyrow td[row=" + rows_Index + "][column=" + (column - 1) + "]");

        //$("#" + table_name + " tbody tr.normalrow td[column=" + (column - 1) + "]:not([colspan])").remove();
        //$(rows).html("<div style=\"white-space:nowrap;mso-rotate:90;-webkit-transform: rotate(-90deg);-moz-transform: rotate(-90deg);-ms-transform: rotate(-90deg);-o-transform: rotate(-90deg);filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3);\" >" + HoliDay_Name);
        //$(rows).attr("rowspan", rowspan);
        //$(rows).css("max-width", "50px");
        //$(rows).css("background-color", "#fff");

    });

    if (!export_file) {
        // Freeze Header Table & Column
        $("#" + table_name).parent().freezeTable({ 'columnNum': 4 });
    }



}

function GetRemark(status) {

    switch (status) {
    
        case 1: return "จำหน่าย";
        case 2: return "ลาออก";
        case 3: return "พักการเรียน";
        case 4: return "สำเร็จการศึกษา";
        case 5: return "ขาดการติดต่อ";
        case 6: return "พ้นสภาพ";

        default: return "";
    }

}

function renderHTML_02(table_name, export_file) {
    var ExportCSS = "border:.5pt solid windowtext;font-size:13px;width: 150px;";
    $("#" + table_name).html("");
    $("#" + table_name).append("<thead>");
    var ScanIn_Status_0 = 0, ScanIn_Status_1 = 0, ScanIn_Status_2 = 0, ScanIn_Status_3 = 0, ScanIn_Status_4 = 0, ScanIn_Status_5 = 0, ScanIn_Status_6 = 0;
    if (export_file === true) {
        HeaderCSS += ExportCSS;
        HeaderCSS_1 += ExportCSS;
        RowsCSS += ExportCSS;
        RowsCSS_1 += ExportCSS;
    }
    else {
        HeaderCSS = HeaderCSS.replaceAll(ExportCSS, "");
        HeaderCSS_1 = HeaderCSS_1.replaceAll(ExportCSS, "");
        RowsCSS = RowsCSS.replaceAll(ExportCSS, "");
        RowsCSS_1 = RowsCSS_1.replaceAll(ExportCSS, "");
    }

    $("#" + table_name).append("<tbody>");
    $.each(reports_data, function (index, values) {
        var ScanIn_Status = "";
        var ScanOut_Status = "";

        var cssStudent = "";
        if (values.ScanIn_Status == "3") {
            cssStudent = "background-color:#FFE4E1;color:#black;";
        }

        switch (values.ScanOut_Status.trim()) {
            case "0": ScanOut_Status = "ตรงเวลา"; /*ScanOut_Status_0 += 1; */break;
            case "1": ScanOut_Status = "สาย"; /*ScanOut_Status_1 += 1;*/ break;
            case "2": ScanOut_Status = "ออกก่อนเวลา"; /*ScanOut_Status_1 += 1;*/ break;
            case "3": ScanOut_Status = "ขาด"; /*ScanOut_Status_2 += 1;*/ break;
            case "6": case "8": ScanOut_Status = "วันหยุด"; break;
            case "9": ScanOut_Status = "วันหยุด"; break;
            case "10": ScanOut_Status = "ลากิจ"; /*ScanOut_Status_4 += 1;*/ break;
            case "11": ScanOut_Status = "ลาป่วย"; /*ScanOut_Status_5 += 1;*/ break;
            case "12": ScanOut_Status = "กิจกรรม"; /*ScanOut_Status_6 += 1;*/ break;
            case "99": ScanOut_Status = "ไม่ได้เช็คชื่อ"; /*ScanOut_Status_6 += 1;*/ break;
            default: ScanOut_Status = "ลา"; /*ScanOut_Status_5 += 1;*/ break;
        }

        switch (values.ScanIn_Status.trim()) {
            case "0": ScanIn_Status = "ตรงเวลา"; ScanIn_Status_0 += 1; break;
            case "1": ScanIn_Status = "สาย"; ScanIn_Status_1 += 1; break;
            case "3": ScanIn_Status = "ขาด"; ScanIn_Status_2 += 1; break;
            case "6": case "8": ScanIn_Status = "วันหยุด"; ScanOut_Status = "วันหยุด"; break;
            case "9": ScanIn_Status = "วันหยุด"; break;
            case "10": ScanIn_Status = "ลากิจ"; ScanIn_Status_4 += 1; break;
            case "11": ScanIn_Status = "ลาป่วย"; ScanIn_Status_5 += 1; break;
            case "12": ScanIn_Status = "กิจกรรม"; ScanIn_Status_6 += 1; break;
            case "99": ScanIn_Status = "ไม่ได้เช็คชื่อ"; ScanIn_Status_6 += 1; break;
            default: ScanIn_Status = "ลา"; ScanIn_Status_5 += 1; break;
        }


        $("#" + table_name + " tbody").append("<tr style='" + cssStudent + "height: 60px;'><td style='" + RowsCSS + cssStudent + "'>" + (index + 1)
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.Student_Name
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.Student_Id
            + "<td style='" + RowsCSS + cssStudent + "'>" + ScanIn_Status
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.ScanIn_Time
            + "<td style='" + RowsCSS + cssStudent + "'>" + (values.Temperature || '-')
            + "<td style='" + RowsCSS + cssStudent + "'>" + ScanOut_Status
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.ScanOut_Time
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.TeacherName
            + "<td style='width:70px !important;" + RowsCSS + cssStudent + "'>" + ((values.FaceScanUrl != "") ? "<a style='width: 30px;height:30px;' target='_blank' class='popup-link' href='" + values.FaceScanUrl + "'><img width='30px' height='30px'  style='width: 30px;height:30px;' src='" + values.FaceScanUrl + "'></img></a><img width='7%' height='7%' style='padding:5% !important;' class='hidden' src='" + values.FaceScanUrl + "'></img>" : "")
            + "<td style='" + RowsCSS + cssStudent + "'>" + values.DeviceType + "");
    });

    if (export_file === true) {
        $("#" + table_name).append("<tfoot>");
        var Colspan = $("#" + table_name + " tbody tr:last td").length;
        var Rows = $("#" + table_name + " tbody tr").length;
        //Add Header Table
        $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:20px;'>" + $("input[id*=hdfschoolname]").val());

        $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "' style='font-size:18px;'>รายงานสรุปการมาเรียนประจำวันที่ " + $("#txtstart1").val());

        $("#" + table_name + " thead").append("<tr><th colspan='1' style='font-size:13px;text-align: right !important;'>ระดับการศึกษา :<td colspan='1' style='font-size:13px;vertical-align:middle;'>" + LCF.GetLevelText() + "<td colspan='" + (Colspan - 4) + "'><th colspan='1' style='font-size:13px;text-align: right !important;'>พิมพ์วันที่ :<td colspan='1' style='font-size:13px;text-align: left !important;vertical-align:middle;'>{day_print}");
        $("#" + table_name + " thead").append("<tr><th colspan='1' style='font-size:13px;text-align: right !important;'>ห้องเรียน  :<td colspan='1' style='font-size:13px;'>" + LCF.GetClassText() + "<td colspan='" + (Colspan - 4) + "'><th colspan='1' style='font-size:13px;text-align: right !important;'>เวลา :<td colspan='1' style='font-size:13px;text-align: left !important;'>{time_print}");
        $("#" + table_name + " thead").append("<tr><th colspan='" + Colspan + "'>");
        //Add Footer Table
        $("#" + table_name + " tfoot").append("<tr><td colspan='" + Colspan + "'>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='1'  style='font-size:13px;'>สรุป :<td  style='font-size:13px;'>มาเรียน<td  style='font-size:13px;'>" + (ScanIn_Status_0 + ScanIn_Status_6) + "<td  style='font-size:13px;'>คน " + percent(Rows, ScanIn_Status_0 + ScanIn_Status_6) + "<td><td><td>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='1'><td  style='font-size:13px;'>สาย<td  style='font-size:13px;'>" + ScanIn_Status_1 + "<td style='font-size:13px;'> คน " + percent(Rows, ScanIn_Status_1) + "<td><td><td>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='1'><td style='font-size:13px;'>ขาดเรียน<td  style='font-size:13px;'>" + ScanIn_Status_2 + "<td style='font-size:13px;'>คน " + percent(Rows, ScanIn_Status_2) + "<td><td><td>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='1'><td style='font-size:13px;'>ลา<td  style='font-size:13px;'>" + (ScanIn_Status_4 + ScanIn_Status_5) + "<td  style='font-size:13px;'>คน " + percent(Rows, (ScanIn_Status_4 + ScanIn_Status_5)) + "<td><td><td>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='1'><td style='font-size:13px;'>รวม<td  style='font-size:13px;'>" + Rows + "<td  style='font-size:13px;'>คน " + percent(Rows, (ScanIn_Status_0 + ScanIn_Status_1 + ScanIn_Status_2 + ScanIn_Status_4 + ScanIn_Status_5)) + "<td><td><td>");
        $("#" + table_name + " tfoot").append("<tr><td colspan='4'><td colspan='3' style='text-align: center !important;font-size:13px;'>ลงชื่อ.....................................................");
        $("#" + table_name + " tfoot").append("<tr><td colspan='4'><td colspan='3' style='text-align: center !important;font-size:13px;'>(.....................................................)");
        $("#" + table_name + " tfoot").append("<tr><td colspan='4'><td colspan='3' style='text-align: center !important;font-size:13px;'>ผู้รับรอง");
    }

    $("#" + table_name + " thead").append("<tr><th style='" + HeaderCSS + "'>ลำดับ<th style='" + HeaderCSS + "'>ชื่อ-นามสกุล<th style='" + HeaderCSS
        + "'>รหัสนักเรียน<th style='" + HeaderCSS + "'>สถานะการเข้าเรียน<th style='" + HeaderCSS + "'>เวลาเข้า<th style='" + HeaderCSS + "'>อุณหภูมิ<th style='" + HeaderCSS + "'>สถานะการเลิกเรียน<th style='" + HeaderCSS + "'>เวลาออก<th style='" + HeaderCSS + "'>บันทึกโดย<th style='width:70px !important;" + HeaderCSS + "'>รูป<th style='" + HeaderCSS + "'>หมายเหตุ");


    if (!export_file) {
        // Freeze Header Table & Column
        $("#" + table_name).parent().freezeTable({});

        $('.popup-link').magnificPopup({ type: 'image' });
    }

}

function renderHTML_03(table_name) {
    var ExportCSS = "border:.5pt solid windowtext;";
    HeaderCSS = HeaderCSS.replaceAll(ExportCSS, "");
    HeaderCSS_1 = HeaderCSS_1.replaceAll(ExportCSS, "");
    RowsCSS = RowsCSS.replaceAll(ExportCSS, "");
    RowsCSS_1 = RowsCSS_1.replaceAll(ExportCSS, "");

    $("#" + table_name).html("");
    $("#" + table_name).append("<thead><tr>");
    //var tableHeader = $("#" + table_name + " thead tr");
    //$.each(["ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "วัน/เดือน/ปี", "สาย", "ขาด", "ลากิจ", "ลาป่วย"], function (e, s) {
    //    $(tableHeader).append("<th style='" + HeaderCSS + "'>" + s);
    //});
    $("#" + table_name).append(" <tbody>");
    var tableHeader = $("#" + table_name + " thead");
    var tableBody = $("#" + table_name + " tbody");
    var headerColumns = "<tr>";
    $.each(["ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "วัน/เดือน/ปี", "สาย", "ขาด", "ลากิจ", "ลาป่วย"], function (e1, s1) {
        headerColumns += "<th style='" + HeaderCSS + "'>" + s1;
    });

    $.each(reports_data, function (e, s) {
        tableHeader.append("<tr><th colspan='8' style='text-align:left;'> ห้อง " + s.level2name);
        tableHeader.append(headerColumns);

        $.each(s.studentDatas, function (index, values) {
            var sumStatus_0 = 0, sumStatus_1 = 0, sumStatus_2 = 0, sumStatus_3 = 0;
            var rowsIndex = 0;
            $.each(values.scanDatas, function (scanIndex, scanValuse) {
                if (jQuery.inArray(scanValuse.Scan_Status, ["3", "1", "10", "11"]) !== -1) {
                    if (rowsIndex === 0) {
                        tableBody.append("<tr><td style='text-align:center;'>" + (index + 1)
                            + "<td style='text-align:center;'>" + values.Student_Id
                            + "<td style='text-align:left;padding-left: 10px !important;'>" + values.Student_Name
                            + "<td style='text-align:center;' >" + scanValuse.Scan_Date
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "1" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "3" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "10" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "11" ? "1" : ""));
                    }
                    else {
                        tableBody.append("<tr><td colspan='3'><td style='text-align:center;'>" + scanValuse.Scan_Date
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "1" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "3" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "10" ? "1" : "")
                            + "<td style='text-align:center;'>" + (scanValuse.Scan_Status === "11" ? "1" : ""));
                    }
                    switch (scanValuse.Scan_Status) {
                        case "1": sumStatus_0 += 1; break;
                        case "3": sumStatus_1 += 1; break;
                        case "10": sumStatus_2 += 1; break;
                        case "11": sumStatus_3 += 1; break;
                    }
                    rowsIndex++;
                }
            });
            if (sumStatus_0 + sumStatus_1 + sumStatus_2 + sumStatus_3 === 0) {
                tableBody.append("<tr><td>" + (index + 1)
                    + "<td>" + values.Student_Id
                    + "<td>" + values.Student_Name
                    + "<td><td><td><td><td>");
            }

            tableBody.append("<tr><td colspan=4 style='text-align:right;'> รวม"
                + "<td style='text-align:center;'>" + sumStatus_0
                + "<td style='text-align:center;'>" + sumStatus_1
                + "<td style='text-align:center;'>" + sumStatus_2
                + "<td style='text-align:center;'>" + sumStatus_3);
        });
    });
    //$("body").mLoading('hide');
    $("#" + table_name).parent().freezeTable({});
}

function renderHTML_04type3(table_name) {
    var ExportCSS = "border:.5pt solid windowtext;";
    HeaderCSS = HeaderCSS.replaceAll(ExportCSS, "");
    HeaderCSS_1 = HeaderCSS_1.replaceAll(ExportCSS, "");
    RowsCSS = RowsCSS.replaceAll(ExportCSS, "");
    RowsCSS_1 = RowsCSS_1.replaceAll(ExportCSS, "");

    $("#" + table_name).html("");
    $("#" + table_name).append("<thead><tr>");
    //var tableHeader = $("#" + table_name + " thead tr");
    //$.each(["ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "วัน/เดือน/ปี", "สาย", "ขาด", "ลากิจ", "ลาป่วย"], function (e, s) {
    //    $(tableHeader).append("<th style='" + HeaderCSS + "'>" + s);
    //});
    $("#" + table_name).append(" <tbody>");
    var tableHeader = $("#" + table_name + " thead");
    var tableBody = $("#" + table_name + " tbody");
    var headerColumns = "<tr>";
    $.each(["ลำดับ", "เลขประจำตัว", "ชื่อ-นามสกุล", "สาย", "ขาด", "ขาดครึ่งวัน", "ลากิจ", "ลาป่วย", "ไม่ได้เช็คชื่อ", "ตัดคะแนนสาย/ขาด", "ตัดคะแนนอื่นๆ", "รวมคะแนน"], function (e1, s1) {
        headerColumns += "<th style='" + HeaderCSS + "'>" + s1;
    });

    $.each(reports_data, function (e, s) {
        tableHeader.append("<tr><th colspan='12' style='text-align:left;'> ห้อง " + s.level2name);
        tableHeader.append(headerColumns);

        $.each(s.studentDatas, function (index, values) {
            var sumStatus_0 = 0, sumStatus_1 = 0, sumStatus_1_1 = 0, sumStatus_2 = 0, sumStatus_3 = 0, sumStatus_4 = 0;
            var rowsIndex = 0;
            var behav, behavAuto, behavManual, ScoreAlert;
            let _Late = 0, _Absence = 0, _Absence_Half = 0, _Errand = 0, _Sick = 0, _UncheckName = 0;

            $.each(values.scanDatas, function (scanIndex, scanValuse) {
                if (jQuery.inArray(scanValuse.Scan_Status, ["3", "1", "10", "11"]) !== -1) {
                    //if (rowsIndex === 0) {
                    //    tableBody.append("<tr><td class='center'>" + (index + 1)
                    //        + "<td>" + values.Student_Id
                    //        + "<td>" + values.Student_Name);
                    //        //+ "<td class='center'>" + scanValuse.Scan_Date
                    //        //+ "<td class='center'>" + (scanValuse.Scan_Status === "1" ? "1" : "")
                    //        //+ "<td class='center'>" + (scanValuse.Scan_Status === "3" ? "1" : "")
                    //        //+ "<td class='center'>" + (scanValuse.Scan_Status === "10" ? "1" : "")
                    //        //+ "<td class='center'>" + (scanValuse.Scan_Status === "11" ? "1" : ""));
                    //}
                    //else {
                    //    tableBody.append("<tr><td colspan='3'><td class='center'>" + scanValuse.Scan_Date
                    //        + "<td class='center'>" + (scanValuse.Scan_Status === "1" ? "1" : "")
                    //        + "<td class='center'>" + (scanValuse.Scan_Status === "3" ? "1" : "")
                    //        + "<td class='center'>" + (scanValuse.Scan_Status === "10" ? "1" : "")
                    //        + "<td class='center'>" + (scanValuse.Scan_Status === "11" ? "1" : ""));
                    //}
                    switch (scanValuse.Scan_Status) {
                        case "1": sumStatus_0 += 1; break;
                        case "3": {
                            if (scanValuse.timeIn === "") {
                                sumStatus_1 += 1;
                            } else sumStatus_1_1 += 1;
                            break;
                        }
                        case "10": sumStatus_2 += 1; break;
                        case "11": sumStatus_3 += 1; break;
                        case "99": sumStatus_4 += 1; break;
                    }

                    behavAuto = values.behavAuto;
                    behavManual = values.behavManual;
                    behav = values.behaviorScore;
                    ScoreAlert = values.Alert;

                    _Late += scanValuse.Late;
                    _Absence += scanValuse.Absence;
                    _Absence_Half += scanValuse.Absence_Half;
                    _Errand += scanValuse.Errand;
                    _Sick += scanValuse.Sick;
                    _UncheckName += scanValuse.UncheckName;

                    //console.log(" _Late : " + _Late + " Late : " + scanValuse.Late + " Scan_Date : " + scanValuse.Scan_Date);

                    rowsIndex++;
                }
            });


            if (sumStatus_0 + sumStatus_1 + sumStatus_2 + sumStatus_3 + sumStatus_1_1 + sumStatus_4 === 0) {
                let cssStyle = "";
                if (values.StudentStatus == "2") cssStyle = "background-color: #BEBEBE; color: Black;";
                else if (ScoreAlert === true) {
                    cssStyle = "background-color: #EED5D2;";
                }
                tableBody.append("<tr style='" + cssStyle + "'><td style='text-align:center;'>" + (index + 1)
                    + "<td style='text-align:center;'>" + values.Student_Id
                    + "<td style='text-align:left;padding-left: 10px !important;'>" + values.Student_Name
                    + "<td style='text-align:center;'>" + sumStatus_0 + "/(" + _Late + ")"
                    + "<td style='text-align:center;'>" + sumStatus_1 + "/(" + _Absence + ")"
                    + "<td style='text-align:center;'>" + sumStatus_1_1 + "/(" + _Absence_Half + ")"
                    + "<td style='text-align:center;'>" + sumStatus_2 + "/(" + _Errand + ")"
                    + "<td style='text-align:center;'>" + sumStatus_3 + "/(" + _Sick + ")"
                    + "<td style='text-align:center;'>" + sumStatus_4 + "/(" + _UncheckName + ")"
                    + "<td style='text-align:center;'>0<td style='text-align:center;'>0<td style='text-align:center;'>0");
            } else {
                //tableBody.append("<tr>"/*<td colspan=4 class='right'> รวม"*/
                let cssStyle = "";
                if (values.StudentStatus == "2") cssStyle = "background-color: #BEBEBE; color: Black;";
                else if (ScoreAlert === true) {
                    cssStyle = "background-color: #EED5D2;";
                }
                tableBody.append("<tr style='" + cssStyle + "'><td style='text-align:center;'>" + (index + 1)
                    + "<td style='text-align:center;'>" + values.Student_Id
                    + "<td style='text-align:left;padding-left: 10px !important;'>" + values.Student_Name
                    + "<td style='text-align:center;'>" + sumStatus_0 + "/(" + _Late + ")"
                    + "<td style='text-align:center;'>" + sumStatus_1 + "/(" + _Absence + ")"
                    + "<td style='text-align:center;'>" + sumStatus_1_1 + "/(" + _Absence_Half + ")"
                    + "<td style='text-align:center;'>" + sumStatus_2 + "/(" + _Sick + ")"
                    + "<td style='text-align:center;'>" + sumStatus_3 + "/(" + _Errand + ")"
                    + "<td style='text-align:center;'>" + sumStatus_4 + "/(" + _UncheckName + ")"
                    + "<td style='text-align:center;'>" + behavAuto
                    + "<td style='text-align:center;'>" + behavManual
                    + "<td style='text-align:center;'>" + behav);
            }
        });
    });

    //$("body").mLoading('hide');
    $("#" + table_name).parent().freezeTable({});
}

var report_txt = '';
function export_excel() {
    var dt = new Date();
    report_txt = '';
    if (Search.sort_type === "0") {
        renderHTML_02('table_exports', true);
    } else {
        renderHTML('table_exports', true);
    }

    //if (Search.sort_type === "0") {     
    //    renderHTML_02('table_exports', true);
    //} else {
    //    if ($("#reports_type").val() === "1") {           
    //        renderHTML_03('table_exports', true);
    //    } else if ($("#reports_type").val() === "0") {
    //        renderHTML('table_exports', true);           
    //    } else {            
    //        renderHTML_04type3('table_exports', true);
    //    }
    //}

    //$("#example .popup-link").attr('style', 'display:none');
    $("#export_excel").find('.popup-link').remove();
    //$("#export_excel").find('tr.dummyrow').remove();

    var param = { "filename": "filename02", "tabledata": $("#export_excel").html() };
    //$(param.tabledata).find('.popup-link')
    $.post("/export_excel.aspx", param, function (data) {
        report_txt += "_" + dt.toLocaleDateString() + " " + dt.getHours() + dt.getMinutes() + dt.getSeconds();
        downloadFile('รายงานการมาโรงเรียน ' + $("input[id*=hdfschoolname]").val() + report_txt + '.xls', 'data:application/xml;charset=utf-8;base64,', data);
    });

}

function export_excel2() {
    //$("body").mLoading('show');
    var json = JSON.stringify({ search: Search });
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Report/Reportmobile03.aspx/export_data", true);
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhr.responseType = 'blob';
    xhr.onload = function () {
        saveAs(xhr.response, 'รายงานการมาโรงเรียน ' + $("input[id*=hdfschoolname]").val() + report_txt + '.xls');
        //$("body").mLoading('hide');
    };
    xhr.send(json);
}

function export_pdf2() {
    // $("body").mLoading('show');
    var json = JSON.stringify(Search);
    var req = new XMLHttpRequest();
    req.open("POST", "/Report/Handles/Reports03_pdfHandler.ashx", true);
    req.responseType = "blob";
    req.onreadystatechange = function () {
        if (req.readyState === 4 && req.status === 200) {

            // test for IE

            if (typeof window.navigator.msSaveBlob === 'function') {
                window.navigator.msSaveBlob(req.response, "PdfName-" + new Date().getTime() + ".pdf");
            } else {
                var blob = req.response;
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(blob);
                link.download = 'รายงานการมาโรงเรียน ' + $("input[id*=hdfschoolname]").val() + report_txt + ".pdf";

                // append the link to the document body

                document.body.appendChild(link);

                link.click();
                // $("body").mLoading('hide');
            }
        }
    };
    req.send(json);
}

function TimeFormat(d) {
    return (d / 60).toFixed(0) + ":" + (d % 60)
}
function percent(maxuser, countstatus) {
    if (maxuser === 0) return 0
    else return ((100 * countstatus) / maxuser).toFixed(2);
}
