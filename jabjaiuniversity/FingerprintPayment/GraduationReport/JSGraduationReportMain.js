var dataTestObjectArray = [];
var table;

$(document).ready(function () {
    ListddlYear();

    ListddlSubLV();

    $("#ddlYear").change(function () {
        ListddlTerm();
    });

});

function ListddlYear() {
    $.ajax({
        url: "/App_Logic/ddl.ashx?mode=listYear",
        success: function (msg) {
            //console.log(msg);
            $.each(msg, function (index) {
                $('select[id*=ddlYear]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nYear)
                        .text(msg[index].numberYear));
            });
            ListddlTerm();
        }
    });
}

function ListddlTerm() {
    var year = $('#ddlYear option:selected').text();

    $('#ddlTerm').empty();
    $.ajax({
        url: "/App_Logic/ddlTerm.ashx?year=" + year,
        success: function (msg) {
            //console.log(msg);
            $.each(msg, function (index) {
                $('select[id*=ddlTerm]')
                    .append($("<option></option>")
                        .attr("value", msg[index].value)
                        .text(msg[index].name));
            });
        }
    });
}

function ListddlSubLV() {
    $.ajax({
        url: "/App_Logic/ddl.ashx?mode=listSubLV",
        success: function (msg) {
            console.log(msg);
            $.each(msg, function (index) {
                if (msg[index].shortNameSubLV == "ป.6" || msg[index].shortNameSubLV == "ม.3" || msg[index].shortNameSubLV == "ม.6" || msg[index].shortNameSubLV == "P.6" || msg[index].shortNameSubLV == "M.3" || msg[index].shortNameSubLV == "M.6") {
                    var name = "";
                    if (msg[index].shortNameSubLV == "ป.6" || msg[index].shortNameSubLV == "P.6") name = "ประถมศึกษาปีที่ 6";
                    else if (msg[index].shortNameSubLV == "ม.3" || msg[index].shortNameSubLV == "M.3") name = "มัธยมศึกษาปีที่ 3";
                    else if (msg[index].shortNameSubLV == "ม.6" || msg[index].shortNameSubLV == "M.6") name = "มัธยมศึกษาปีที่ 6";
                    $('select[id*=ddlSubLV]')
                        .append($("<option></option>")
                            .attr("value", msg[index].nSubLV)
                            .text(name));
                }

            });
        }
    });
}

$(document)
    .ajaxStart(function () {
        $('#loading').show();
    })
    .ajaxStop(function () {
        $('#loading').hide();
    });

function ClickPrint() {

    //var ddlYear = $('#ddlYear').val();
    //var ddlTerm = $('#ddlTerm').val();
    //var ddlSubLV = $('#ddlSubLV').val();
    var year = $("#ddlYear option:selected").text();
    var term = $("#ddlTerm option:selected").text();
    var ddlSubLVFilter = $("#ddlSubLV option:selected").text();

    var nYear = $("#ddlYear").val();
    var SubLV = $("#ddlSubLV").val();


    if (ddlSubLVFilter == "ประถมศึกษาปีที่ 6") window.open("../GraduationReport/GraduationReportPrint.aspx?LV=P6&year=" + year + "&term=" + term + "&nYear=" + nYear + "&SubLV=" + SubLV);
    else if (ddlSubLVFilter == "มัธยมศึกษาปีที่ 3")
        window.open("../GraduationReport/GraduationReportPrint_M.aspx?LV=M3&year=" + year + "&term=" + term + "&nYear=" + nYear + "&SubLV=" + SubLV);
    else if (ddlSubLVFilter == "มัธยมศึกษาปีที่ 6")
        window.open("../GraduationReport/GraduationReportPrint_M.aspx?LV=M6&year=" + year + "&term=" + term + "&nYear=" + nYear + "&SubLV=" + SubLV);
    else alert('กรุณาเลือกชั้นปีที่สำเร็จการศึกษา');
}


