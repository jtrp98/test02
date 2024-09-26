var availableValueT = [""];
var availableTagsT = [""];
var addteacher = [""];
var availableTagsplane = [""];
var timetableid = "";
var _listperiod = [];
var para1var = getUrlParameter("id");
var scheduleid = 0;
function checkData() {
    var _ch = true;
    $.each(availableTagsplane, function (indexperiod) {
        var nperiod = _listperiod[_indexperiod].value;
        var nday = _indexday;

        if (availableTagsplane[indexperiod].nperiod === nperiod && availableTagsplane[indexperiod].nday == nday) {
            _ch = false;
        }
    });
    return _ch;
}

var _indexday;
var _indexperiod;
function adddata(indexday, indexperiod) {
    $("#myModalLabel").html("วัน " + _listday[indexday] + " คาบ " + _listperiod[indexperiod].label);
    _indexday = indexday;
    _indexperiod = indexperiod;
}
var indexedit = 0;

function Clear() {
    $("#myModalLabel").html("เพิ่มข้อมูล");
    $("#selectplane_chosen .chosen-single p").html("");
    $("#selectplane_chosen .chosen-single span").html("เลือกวิชาเรียน");
    $("#selectteacher_chosen .chosen-single p").html("");
    $("#selectteacher_chosen .chosen-single span").html("เลือกชื่อผู้สอน");
    $("#selectroom_chosen .chosen-single p").html("");
    $("#selectroom_chosen .chosen-single span").html("เลือกห้องเรียน");
    $("#teacher").val("");
    $("#teacherid").val("");
    $("input[id=add]").val("เพิ่ม");
    $("select[id*=ddlsPeriods]").prop("disabled", false);
    $("select[id*=number]").prop("disabled", false);
    $("#timein").val("");
    $("#timeout").val("");
    //$("#timeinstart").val("");
    //$("#timeinend").val("");
    //$("#timeoutstart").val("");
    //$("#timeoutend").val("");
    //$("#timehalf").val("");
    //$("#timelate").val("");
    $("#btndelete").addClass("hidden");
    $(".addvalues").removeClass("hidden");
    $("#lblday").html("");
    $("#lbltimein").html("");
    $("#lbltimeout").html("");
    disabledinput(false);
    //$("#active").prop("checked", false);
    scheduleid = 0;
    $("#add").addClass("disabled");
}
$(function () {
    $("#btndelete").click(function () {
        $.get("/App_Logic/deleteDataJSON.ashx?mode=delschedule&temp=" + scheduleid, "", function (result) {
            $('#myModal').modal('hide');
            ListTIme();
        });
    });

    $('.clockpicker').clockpicker();
    $('[data-toggle="tooltip"]').tooltip();

    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listvaluesperiod&id=" + getUrlParameter("id"),
        success: function (obj) {
            $.each(obj, function (index) {
                var newObject = {
                    label: obj[index].label + "<br/>" + obj[index].dStart + " - " + obj[index].dEnd,
                    value: obj[index].value
                };
                _listperiod[_listperiod.length] = newObject;
            });
        }
    });

    $('#saveteacher').click(function () {
        var idterm = getUrlParameter("idterm");
        var sid = getUrlParameter("id");
        var teacherid = $("#selectteacherclass_chosen .chosen-single p").html();
        var request = $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/App_Logic/dataGeneric.ashx?mode=insertteacher&id=" + sid + "&teacherid=" + teacherid + "&idterm=" + idterm,
            dataType: "json",
            success: function (msg) {
                timetableid = msg;
                $("#div2").css("display", "");
                $("#div1").css("display", "none");
            }
        });
        request.done(function (msg) {
            ListTIme();
        });

    });
    ListTIme();
    $("select[id*=ddlsPeriods]").change(function () {
        GetValuesPeriods();
    });

    $("#active").click(function () {
        var checked = !$("#active").is(":checked");
        disabledinput(checked);
    });

    $("#btndel").click(function () {
        var idterm = getUrlParameter("idterm");
        var id = getUrlParameter("id"); //getUrlParameter("id");/*getUrlParameter("idterm")*/
        $.get("/App_Logic/insertDataJSON.ashx?mode=teacher&id=" + id + "&idterm=" + idterm + "&teacherid=0", "", function () {
            $("#selectteacherclass_chosen .chosen-single p").html("");
            $("#selectteacherclass_chosen .chosen-single span").html("เลือกชื่อผู้สอน");
        });
    });
});

function disabledinput(checked) {
    $("#timeinstart").attr("disabled", checked);
    $("#timeinend").attr("disabled", checked);
    $("#timeoutstart").attr("disabled", checked);
    $("#timeoutend").attr("disabled", checked);
    $("#timehalf").attr("disabled", checked);
    $("#timelate").attr("disabled", checked);
}

var timelist = ["06:0", "06:1", "06:2", "06:3", "06:4", "06:5"
    , "07:0", "07:1", "07:2", "07:3", "07:4", "07:5"
    , "08:0", "08:1", "08:2", "08:3", "08:4", "08:5"
    , "09:0", "09:1", "09:2", "09:3", "09:4", "09:5"
    , "10:0", "10:1", "10:2", "10:3", "10:4", "10:5"
    , "11:0", "11:1", "11:2", "11:3", "11:4", "11:5"
    , "12:0", "12:1", "12:2", "12:3", "12:4", "12:5"
    , "13:0", "13:1", "13:2", "13:3", "13:4", "13:5"
    , "14:0", "14:1", "14:2", "14:3", "14:4", "14:5"
    , "15:0", "15:1", "15:2", "15:3", "15:4", "15:5"
    , "16:0", "16:1", "16:2", "16:3", "16:4", "16:5"
    , "17:0", "17:1", "17:2", "17:3", "17:4", "17:5"
    , "18:0", "18:1", "18:2", "18:3", "18:4", "18:5"
    , "19:0", "19:1", "19:2", "19:3", "19:4", "19:5"
    , "20:0", "20:1", "20:2", "20:3", "20:4", "20:5"
    , "21:0"];
var timetable2 = new Array();
var starttime = 0;
var endtime = 0;

function TimeIndex(time) {
    var indexend = 0;
    $.each(timelist, function (timeindex) {
        if (time.indexOf(timelist[timeindex]) > -1) {
            indexend = timeindex;
        }
    })
    return indexend;
}

var planelist = [];
function ListTIme() {
    var idterm = getUrlParameter("idterm");
    var id = getUrlParameter("id"); //getUrlParameter("id");/*getUrlParameter("idterm")*/
    planelist = [];
    $("#tableplans tbody").html("");
    $.get("/App_Logic/dataJSONArray.ashx?mode=listschedule&sublevel2=" + id + "&nTerm=" + idterm, "", function () {
    }).done(function (ObjJson) {
        timetable2 = ObjJson;
        $(".coldata").detach();
        $.each(ObjJson, function (nday) {
            var time = ObjJson[nday].sort(function (obj1, obj2) {
                // Ascending: first age less than the previous
                return obj1.id - obj2.id;
            });
            //$("#row" + time[index]["name"]).html("");
            starttime = 0;
            $.each(time, function (index) {
                var dataplane = { "planeid": time[index]["planeid"], "planename": time[index]["planename"], "course_code": time[index]["course_code"] };
                if ($.grep(planelist, function (e) { return e.planename === time[index]["planename"]; }).length === 0) {
                    planelist.push(dataplane);
                }
                $.each(timelist, function (timeindex) {
                    if (time[index]["timestart"].indexOf(timelist[timeindex]) > -1) {
                        var indexend = TimeIndex(time[index]["timeend"]);
                        var indexstart = TimeIndex(time[index]["timestart"]);
                        if ((timeindex - starttime) > 0) {
                            $("#row" + time[index]["name"]).append("<td class='coldata' colspan=" + (timeindex - starttime) + ">");
                        }
                        var tooltip = "รหัสวิชา : " + time[index]["course_code"] + "<br/>"
                            + "ชื่อวิชา : " + time[index]["planename"] + "<br/>"
                            + "เวลา : " + time[index]["timestart"] + " - " + time[index]["timeend"];

                        $("#row" + time[index]["name"]).append("<td class='coldata' colspan=" + (indexend - indexstart) + " ><div class='divtooltip' style='text-align:center;' aria-hidden='true' data-toggle='modal' data-target='#myModal' data-html='true' title=\"" + tooltip + "\">" + time[index]["course_code"]);
                        starttime = indexend;
                    }
                });
                if ((time.length - 1) === index) $("#row" + time[index]["name"]).append("<td class='coldata' colspan=" + (timelist.length - starttime) + ">");
            });
            if (time.length === 0) {
                $("#row" + nday).append("<td class='coldata' colspan=" + (timelist.length - starttime) + ">");
            }
        });
        $(".divtooltip").tooltip({
            template: '<div class="tooltip"><div class="tooltip-arrow"></div><div class="tooltip-head"><h3><span class="glyphicon glyphicon-info-sign"></span> รายละเอียดวิชา</h3></div><div class="tooltip-inner"></div></div>'
        });

        var col1 = "", col2 = "";
        $.each(planelist, function (index) {
            if (index === (planelist.length - 1) && index % 2 === 0) {
                col1 = '<td colspan="1" style="padding-left: 10px; border-right: 0px; vertical-align:top;">' + planelist[index].course_code + '</td><td colspan="2" style="border-left: 0px; padding-left: 10px;">' + planelist[index].planename + '</td>';
                col2 = '<td colspan="1" style="padding-left: 10px; border-right: 0px; vertical-align:top;">&nbsp;</td><td colspan="2" style="border-left: 0px; padding-left: 10px;">&nbsp;</td>';
                $("#tableplans tbody").append('<tr>' + col1 + col2 + '</tr>');
            } else if (index % 2 === 0) {
                col1 = '<td colspan="1" style="padding-left: 10px; border-right: 0px; vertical-align:top;">' + planelist[index].course_code + '</td><td colspan="2" style="border-left: 0px; padding-left: 10px;">' + planelist[index].planename + '</td>';
            }
            else {
                col2 = '<td colspan="1" style="padding-left: 10px; border-right: 0px; vertical-align:top;">' + planelist[index].course_code + '</td><td colspan="2" style="border-left: 0px; padding-left: 10px;">' + planelist[index].planename + '</td>';
                $("#tableplans tbody").append('<tr>' + col1 + col2 + '</tr>');
            }
        });
    });
}

function getUrlParameter(sParam) {
    var sPageURL = decodeURIComponent(window.location.search.substring(1)),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : sParameterName[1];
        }
    }
}

function GetValuesPeriods() {
    var str = "";
    $("select[id*=ddlsPeriods] option:selected").each(function () {
        str = $(this).val() + " ";
    });

    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?id=" + str + "&mode=getperiod",
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                $("#Time").html(objjson[index].sTime);
                $("#StartTime").html(objjson[index].sStartTime);
                $("#EndTime").html(objjson[index].sEndTime);
            });
        }
    });
}

$(document).ready(function () {
    console.log("document loaded");
});

$(window).on("load", function () {
    console.log("window loaded");
});