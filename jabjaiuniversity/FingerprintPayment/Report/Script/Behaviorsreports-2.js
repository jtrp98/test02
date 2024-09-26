var reports_data = [];
var availableValuestudent = [];
var report_txt = "";

$(function () {
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });
    document.getElementById('txtname').addEventListener('input', function () {
        if (this.value === '') {
            $("#txtid").val("");
        }
    }, false);

    $('select[id$=ddlsublevel]').change(function () {
        $('input[id*=txtSubLV2ID]').val("");
        getListSubLV2();
        getliststudent();
    });

    getListSubLV2();

    $('select[id$=ddlSubLV2]').change(function () {
        getliststudent();
    });

    $("select[id*=ddlyear]").change(function () {
        getListTrem();
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
        //source: function (request, response) {
        //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
        //    response(results.slice(0, 10));
        //},
        source: lightwell,
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

    function switch_sort(sort_type) {
        switch (sort_type) {
            case "0": $(".sort_type").hide(); $($(".sort_type")[2]).show(); break;
            case "1": $(".sort_type").hide(); $($(".sort_type")[0]).show(); break;
            case "2": $(".sort_type").hide(); $($(".sort_type")[1]).show(); break;
            case "3": $(".sort_type").hide(); $($(".sort_type")[2]).show(); $($(".sort_type")[3]).show(); break;
        }
    }

    function lightwell(request, response) {
        function hasMatch(s) {
            return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
        }
        var i, l, obj, matches = [];

        if (request.term === "") {
            response([]);
            return;
        }

        for (i = 0, l = availableValuestudent.length; i < l; i++) {
            obj = availableValuestudent[i];
            if (obj.label !== null && obj.code !== null) {
                if (hasMatch(obj.label) || hasMatch(obj.code)) {
                    matches.push(obj);
                }
            }
        }
        response(matches.slice(0, 10));
    }

    getListTrem();

    $("#exportfile").click(function () {
        var dt = new Date();
        $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานคะแนนพฤติกรรม', fileName: "รายงานคะแนนพฤติกรรม_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
    });
});

function Getreports(table_name, export_file) {
    if (getSearch() === true) {
        $("body").mLoading();
        PageMethods.returnlist(Search, function (e) {
            reports_data = e;
            //Reports_03.reports_data = e;
            $("#" + table_name).css("font-size", "16px");
            renderHTML(table_name);
            $("body").mLoading('hide');
        }, function (e) {
            $("body").mLoading('hide');
        });
    }
}

function getSearch() {
    var report_type = 0;
    var dStart = "", dEnd = "";
    Search = {
        "term_id": $('select[id*=semister]').val(), "level2_id": $('select[id*=ddlSubLV2]').val(), "level_id": $('select[id*=ddlsublevel]').val(),
        "sort_type": $('select[id*=sort_type]').val(), "dStart": dStart, "dEnd": dEnd,
        "student_id": $("#txtid").val(), "report_type": report_type, "year_Id": $('select[id*=ddlyear]').val()
    };
    return true;
}

function renderHTML(table_name) {
    $("#" + table_name + " tbody").html("");
    $("#" + table_name + " thead").html("");
    var tableHeader = $("#" + table_name + " thead");
    var tableBody = $("#" + table_name + " tbody");

    var headerStyle = "text-align: center;font-size:24px;rgb(51, 122, 183);background-color: rgb(51, 122, 183);color: rgb(255, 255, 255);"
    tableHeader.append(
        RenderRows({
            rowtype: "header", data: [
                { "text": "ลำดับ", style: headerStyle },
                { "text": "รหัสนักเรียน", style: headerStyle },
                { "text": "ชื่อ-นามสกุล", style: headerStyle },
                { "text": "คะแนนปัจจุบัน", style: headerStyle },
                { "text": "", style: headerStyle }
            ]
        }));

    $.each(reports_data, function (index, data) {
        let styleCss = "";
        if (data.Alert === true) {
            styleCss = "background-color: #EED5D2;";
        }

        tableBody.append(
            RenderRows({
                rowtype: "row", style: styleCss, data: [
                    { "text": (index + 1), style: "text-align: center;" },
                    { "text": data.studentId, style: "text-align: center;" },
                    { "text": data.studentName },
                    { "text": data.Score, style: "text-align: center;" },
                    { "text": "<a href=\"/Report/Behaviorsreportsdetail-2.aspx?userid=" + data.userId + "&termid=" + Search.term_id + "&yearid=" + Search.year_Id + "\" target=\"_blank\">ดูรายละเอียด</a>", style: "text-align: center;" }
                ]
            }));
    });
    $("body").mLoading('hide');
}

function getListSubLV2() {
    var SubLVID = $('select[id$=ddlsublevel] option:selected').val();
    var sSubLV = $('select[id$=ddlsublevel] option:selected').text();
    $('select[id*=ddlSubLV2] option').remove();
    $('select[id*=ddlSubLV2]')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));
    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        success: function (msg) {

            $.each(msg, function (index) {
                $('select[id*=ddlSubLV2]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(msg[index].nTSubLevel2));
            });
            getliststudent();
        }
    });
}

function getListTrem() {
    var YearID = $('select[id$=ddlyear] option:selected').val();
    var YearNumber = $('select[id$=ddlyear] option:selected').text();
    $("select[id$=semister] option").remove();
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
            $('select[id$=ddlsublevel] option:selected').val() + "&nsublevel=" + $('select[id*=ddlSubLV2] option:selected').val(),
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName,
                    value: objjson[index].sID,
                    code: objjson[index].studentid,
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function export_excel() {
    $("body").mLoading('show');
    var json = JSON.stringify(Search);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Report/Handles/Behaviorsreports2_exportHandler.ashx", true);
    xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhr.responseType = 'blob';
    xhr.onload = function () {
        $("body").mLoading('hide');
        saveAs(xhr.response, 'รายงานคะแนนพฤติกรรม ' + $("input[id*=hdfschoolname]").val() + report_txt + '.xls');
    };
    xhr.onerror = function () {
        window.location.reload();
    };
    xhr.send(json);
}
