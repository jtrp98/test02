var report_txt = "";

$(function () {
    $('select[id*=sort_type]').change(function () {
        switch_sort($(this).val());
    });
    switch_sort($('select[id*=sort_type]').val());
});

function switch_sort(sort_type) {
    $("div[class*=sort_type]").hide();
    switch (sort_type) {
        case "0": $(".sort_type" + sort_type).show();  break;
        case "1": $(".sort_type" + sort_type).show();  break;
        case "2": $(".sort_type" + sort_type).show();  break;
        case "3": $(".sort_type" + sort_type).show();  break;
    }
}

function getSearch() {
    var report_type = 0;
    if ($('select[id*=sort_type]').val() === "0") {
        report_type = 0;
    }
    else {
        report_type = 1;
    }

    var dStart = "", dEnd = "";

    if ($('select[id*=sort_type]').val() === "1") {
        dStart = $("#select_month").val()  + "/1/" + ($("select[id*=select_year]").val());
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
    //else {
    //    if ($("#txtstart").val() !== "") {
    //        dStart = $("#txtstart").val().split('/')[1] + "/" + $("#txtstart").val().split('/')[0] + "/" + $("#txtstart").val().split('/')[2];
    //    }
    //    else {
    //        alert("กรุณาเลือกวันที่รายงาน");
    //    }
    //    if ($("#txtend").val() !== "") {
    //        dEnd = $("#txtend").val().split('/')[1] + "/" + $("#txtend").val().split('/')[0] + "/" + $("#txtend").val().split('/')[2];
    //    }
    //}
    Search = {
        "term_id": YTF.GetTermID(),
        "level2_id": null,
        "level_id": null,
        "sort_type": $('select[id*=sort_type]').val(),
        "dStart": dStart,
        "dEnd": dEnd,
        "student_id": null,
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

    //if (!getSearch()) return;

    //$("body").mLoading();
    if (getSearch() === true) {
        PageMethods.returnlist(Search, function (e) {
            reports_data = e;
            $("#" + table_name).html("");
            if (Search.report_type === 0) {
                $("#" + table_name)
                    .append("<thead><tr><th>ลำดับ<th>ชั้นเรียน<th>ครูประจำชั้น<th>ครูที่ปรึกษา 1<th>ครูที่ปรึกษา 2")
                    .append('<tbody>');
                $.each(e, function (e, s) {
                    $("#" + table_name + ' tbody').append("<tr><td class='center'>" + (e + 1)
                        + "<td class='center'>" + s.roomName
                        + "<td class=''>" + s.teacherHead
                        + "<td class=''>" + s.teacherAssistOne
                        + "<td class=''>");
                });
            } else {
                $("#" + table_name)
                    .append("<thead><tr><th>ลำดับ<th>ชั้นเรียน<th>ครูประจำชั้น<th>ครูที่ปรึกษา 1<th>ครูที่ปรึกษา 2<th>จำนวนครั้งที่ไม่ได้เช็คชื่อ")
                    .append('<tbody>');
                $.each(e, function (e, s) {
                    $("#" + table_name + ' tbody').append("<tr><td class='center'>" + (e + 1)
                        + "<td class='center'>" + s.roomName
                        + "<td class=''>" + s.teacherHead
                        + "<td class=''>" + s.teacherAssistOne
                        + "<td class=''>" + s.teacherAssistTwo
                        + "<td class='center'>" + s.dayCount);
                });
            }

            //$("body").mLoading('hide');
        }, function (e) {
            //window.location.reload();
            //$("body").mLoading('hide');
        });
    }
}


function ToExcel() {
    //$("body").mLoading('show');
    if (getSearch() === true) {
        var json = JSON.stringify(Search);
        var xhr = new XMLHttpRequest();
        xhr.open("POST", "/Report/Handles/Reports06_exportHandler.ashx", true);
        xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
        xhr.responseType = 'blob';
        xhr.onload = function () {
            saveAs(xhr.response, 'รายงานการไม่เช็คชื่อโรงเรียน ' + $("input[id*=hdfschoolname]").val() + report_txt + '.xls');
            //$("body").mLoading('hide');
        };
        xhr.onerror = function () {
            window.location.reload();
        };
        xhr.send(json);
    }
}

//$(document)
//    .ajaxStart(function () {
//        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
//    }).ajaxStop($.unblockUI);
