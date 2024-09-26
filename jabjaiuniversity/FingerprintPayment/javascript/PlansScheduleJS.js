///// <reference path="~/Scripts/jquery-3.1.1.js" />

//import { fail } from "assert";
//import { BADFLAGS } from "dns";

var availableValueT = [""];
var availableTagsT = [""];
var addteacher = [""];
var availableTagsplane = [""];
var timetableid = "";
var _listperiod = [];
var para1var = getUrlParameter("id");
var para2var = getUrlParameter("idterm");
var scheduleid = 0;
function checkData() {
    //$('.chosen-select').chosen({ allow_single_deselect: true, allow_single_deselect: true });
    var _ch = true;
    $.each(availableTagsplane, function (indexperiod) {
        var nperiod = _listperiod[_indexperiod].value;
        var nday = _indexday;

        if (availableTagsplane[indexperiod].nperiod === nperiod && availableTagsplane[indexperiod].nday === nday) {
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

function editdata(id) {
    Clear();
    scheduleid = id;
    $("#btndelete").removeClass("hidden");
    $("#selectday").addClass("disabled");
    $("body").mLoading();
    $.get("/App_Logic/dataJSONArray.ashx", { "scheduleid": scheduleid, "mode": "schedule" }, function (result) {
        $.each(result, function (index) {
            $("#myModalLabel").html("แก้ไขข้อมูล");
            $("#timein").val(result["tstart"]);
            $("#timeout").val(result["tend"]);
            //$("#timeinstart").val(result["timestart_in"]);
            //$("#timeinend").val(result["timestart_out"]);
            //$("#timeoutstart").val(result["timeend_in"]);
            //$("#timeoutend").val(result["timeend_out"]);
            //$("#timehalf").val(result["timehalf"]);
            //$("#timelate").val(result["timelate"]);
            $("#selectday").val(result["day"]);
            $("#lblday").html($("#selectday option:selected").text());
            $("#lbltimein").html(result["tstart"]);
            $("#lbltimeout").html(result["tend"]);
            $(".addvalues").addClass("hidden");
            //$("#active").prop("checked", result["active"]);
            disabledinput(!$("#active").is(":checked"));
            $("#calculate").bootstrapToggle(result["calculate"] === true ? "on" : "off");

            if (result["planeid"] !== null) {
                $("#selectplane").val(result["planeid"]);
                $('#selectplane').trigger('chosen:updated');
            }
            if (result["roomid"] !== null) {
                $("#selectroom_chosen .chosen-single span").html(result["rooms"]);
                $("#selectroom").val(result["rooms"]);
                $('#selectroom').trigger('chosen:updated');
            }
            if (result["teacherid"] !== null) {
                $("#selectteacher_chosen .chosen-single span").html(result["teachername"]);
                $("#selectteacher_chosen .chosen-single p").html(result["teacherid"]);
                $("#selectteacher").val(result["teacherid"]);
                $('#selectteacher').trigger('chosen:updated');
            }
            $('.chosen-single').trigger('chosen:updated');
            $("#myModal").modal("show");
            $("body").mLoading('hide');
        });
        GetTeachersForAPlanCourse();
    });
}

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
    disabledinput(true);
    //$("#active").prop("checked", false);
    scheduleid = 0;
    $('#calculate').bootstrapToggle('on');
    $("#add").removeClass("disabled");
}

function GetTeachersForAPlanCourse() {
       var requestteacher = $.ajax({
           url: "/App_Logic/modalJSON.aspx?mode=PlanCourseTeacher&nTermSubLevel2=" + para1var + "&nTerm=" + para2var + "&sPlaneId=" + $("#selectplane").val() + "&nPlaneDay=" + $("#selectday").val() + "&scheduleid=" + scheduleid,
        success: function (msg) {
            var i = 0;
            var objjson = $.parseJSON(msg);
            $('#selectteacher').children().remove().end();
            $("#selectteacher_chosen .chosen-single p").html("");
            $("#selectteacher_chosen .chosen-single span").html("เลือกชื่อผู้สอน");
            var selected = "";
           

            $('#selectteacher').append($('<option>', {
                value: "",
                text: "เลือกชื่อผู้สอน",

            }));

            $.each(objjson, function (index) {
              
                var newObject = {
                    label: objjson[index].SName + ' ' + objjson[index].SLastname,
                    value: objjson[index].SEmp
                };

                addteacher[index] = newObject;
                if (objjson[index].IsTimeTableScheduled) {
                    selected = objjson[index].SEmp;
                }
               
                $('#selectteacher').append($('<option>', {
                    value: objjson[index].SEmp,
                    text: objjson[index].SName + ' ' + objjson[index].SLastname,

                }));

            });
            $('#selectteacher').val(selected);                                  //delete current options
            $('#selectteacher').trigger('chosen:updated');
        }
    }).done(function (msg) {
        $('#selectteacherclass').chosen();
        $('#selectteacher').chosen();
        $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
    });
}

$(function () {
    jQuery.validator.addMethod("timeformat", function (value, element) {
        var rxDatePattern = /^(\d{1,2}):(\d{1,2})$/; //Declare Regex
        var dtArray = value.match(rxDatePattern);
        if (dtArray === null) return false;
        else return true;
    }, "รูปแบบเวลาไม่ถูกต้อง");
    jQuery.validator.addMethod("timelength", function (value, element) {
        var rxDatePattern = /^(\d{1,2}):(\d{1,2})$/; //Declare Regex
        var dtArray = value.match(rxDatePattern);
        if (dtArray[1] >= 6 && dtArray[1] <= 21) return true;
        else return false;
    }, "รูปแบบเวลาไม่ถูกต้อง");
    $("#btndelete").click(function () {
      
       $.confirm({
            title: '<h2>คำเตือน!</h>',
           content: '<h2>การลบวิชาเรียนออกจากตารางสอนจะลบข้อมูลการเช็คชื่อในวิชานี้ด้วยทั้งหมด <br> หากท่านต้องการเก็บข้อมูลสถิติการเช็คชื่อไว้ เราแนะนำให้แก้ไขสลับชื่อวิชาแทน</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> ใช่',
                    action: function () {
                        $.get("/App_Logic/deleteDataJSON.ashx?mode=delschedule&temp=" + scheduleid, "", function (result) {
                            $('#myModal').modal('hide');
                            ListTIme();
                        });
                    }
                }
            },
        });
    });

    $.validator.addMethod("requiredIfChecked", function (val, ele, arg) {
        if ($("#active").is(":checked") && ($.trim(val) === '')) { return false; }
        return true;
    }, "This field is required if startClientFromWeb is checked...");

    $("#aspnetForm").validate({
        rules: {
            timein: {
                "required": true,
                timeformat: true,
                timelength: true,
            },
            timeout: {
                "required": true,
                timeformat: true,
                timelength: true,
            }
            //timeinstart: { requiredIfChecked: true },
            //timeinend: { requiredIfChecked: true },
            //timeoutstart: { requiredIfChecked: true },
            //timeoutend: { requiredIfChecked: true },
            //timelate: { requiredIfChecked: true },
            //timehalf: { requiredIfChecked: true }
        },
        messages: {
            timein: {
                required: "กรุณากรอกช่วงเวลา",
                timeformat: "รูปแบบเวลาไม่ถูกต้อง ตย. 08:00",
                timelength: "สามารถจัดตรารรางสอนได้ตั้ง 06:00 - 21:00 น",
            },
            timeout: {
                required: "กรุณากรอกช่วงเวลา",
                timeformat: "รูปแบบเวลาไม่ถูกต้อง ตย. 08:00",
                timelength: "สามารถจัดตรารรางสอนได้ตั้ง 06:00 - 21:00 น",
            }
            //timeinstart: "กรุณากรอกช่วงเวลา",
            //timeinend: "กรุณากรอกช่วงเวลา",
            //timeoutstart: "กรุณากรอกช่วงเวลา",
            //timeoutend: "กรุณากรอกช่วงเวลา",
            //timelate: "กรุณากรอกนาทีสาย",
            //timehalf: "กรุณากรอกเวลาแบ่งคาบ"
        },
        tooltip_options: {
            timeinstart: { placement: 'right', trigger: 'focus' },
            timeinend: { placement: 'right', html: true },
            //timein: { placement: 'right', html: true },
            //timeout: { placement: 'right', html: true },
            //timeoutstart: { placement: 'right', html: true },
            //timeoutend: { placement: 'right', html: true },
            //timelate: { placement: 'right', html: true },
            //timehalf: { placement: 'right', html: true }
        },
        submitHandler: function (e) {
            $("body").mLoading();
            SaveData();
            $("body").mLoading('hide');
        }
    });

    //option A
    $("#aspnetForm").submit(function (e) {
        //alert('submit intercepted');
        e.preventDefault(e);
    });

    $("#add").click(function () {
        //$(this).addClass("disabled");
    });

    $('.clockpicker').clockpicker();
    $('[data-toggle="tooltip"]').tooltip();
    GetHomeTeachersForAClass("");
    GetTeachersForAPlanCourse();
    $("#selectplane").change(function () {
        GetTeachersForAPlanCourse();
    });

    function GetHomeTeachersForAClass() {
        var requestteacher = $.ajax({
            url: "/App_Logic/modalJSON.aspx?mode=HomeTeacher&nTermSubLevel2=" + para1var + "&nTerm=" + para2var + "&sPlaneId=" + $("#selectplane").val(),
            success: function (msg) {
                var i = 0;
                var objjson = $.parseJSON(msg);
                $('#selectteacherclass').children().remove().end();
                $.each(objjson, function (index) {
                        $('#selectteacherclass').append($('<option>', {
                            value: objjson[index].SEmp,
                            text: objjson[index].SName + ' ' + objjson[index].SLastname
                        }));
                });
                $("#selectteacherclass").chosen().change(function () {
                    var idterm = getUrlParameter("idterm");
                    var id = getUrlParameter("id"); //getUrlParameter("id");/*getUrlParameter("idterm")*/
                    var teacherid = $(this).val();
                    $.get("/App_Logic/insertDataJSON.ashx?mode=teacher&id=" + id + "&idterm=" + idterm + "&teacherid=" + teacherid, "", function () {

                    });
                });
            }
        }).done(function (msg) {
           // $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
            $("#selectteacherclass_chosen .chosen-single span").html($("#ctl00_MainContent_txtaddteacher").val());
            $("#selectteacherclass_chosen .chosen-single p").html($('#ctl00_MainContent_txtaddteacherid').val());
        });
    }
    

    PageMethods.getplane(para1var, para2var,
        function (response) {
            response = $.parseJSON(response);
            $.each(response, function (index) {
                $('#selectplane').append($('<option>', {
                    value: response[index].value,
                    text: response[index].name
                }));
            }).d;
            $('#selectplane').chosen();
            $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
        },
        function (respones) {

        }
    );

    //$.ajax({
    //    url: "/App_Logic/dataGeneric.ashx?id=" + para1var + "&mode=listplane",
    //    dataType: "json",
    //    success: function (objjson) {
    //        $.each(objjson, function (index) {
    //            $('#selectplane').append($('<option>', {
    //                value: objjson[index].value,
    //                text: objjson[index].value + ' ' + objjson[index].name
    //            }));
    //        });
    //    }
    //}).done(function (msg) {
    //    $('#selectplane').chosen();
    //    $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
    //});

    $.ajax({
        url: "/App_Logic/modalJSON.aspx?mode=itemclass",
        dataType: "json",
        success: function (objjson) {
            $('#selectroom').append($('<option>', {
                value: "",
                text: "เลือกห้องเรียน",

            }));
            $.each(objjson, function (index) {
                $('#selectroom').append($('<option>', {
                    value: objjson[index].sClassID,
                    text: objjson[index].sClass
                }));
            });
        }
    }).done(function (msg) {
        $('#selectroom').chosen();
        $('.chosen-select-deselect').chosen({ allow_single_deselect: true });
    });

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
    });
    return indexend;
}

function SaveData() {
    var idterm = getUrlParameter("idterm");
    var id = getUrlParameter("id"); //getUrlParameter("id");/*getUrlParameter("idterm")*/
    $("#add").addClass("disabled");
    var JSONData = {
        "nPlaneDay": $("#selectday").val(), "tStart": $("#timein").val(), "tEnd": $("#timeout").val(),
        "dTimeStart_IN": ""/* $("#timeinstart").val()*/, "dTimeStart_OUT": "" /*$("#timeinend").val()*/,
        "dTimeEnd_OUT": ""/* $("#timeoutend").val()*/, "dTimeEnd_IN": "" /*$("#timeoutstart").val()*/,
        "nTimeLate": ""/*$("#timelate").val()*/, "dTimeHalf": ""/* $("#timehalf").val()*/,
        "sClassID": $("#selectroom").val(),
        "sPlaneID": $("#selectplane").val(), "sEmp": $("#selectteacher").val(),
        "idterm": idterm, "id": id, "scheduleid": scheduleid, "active": false/*/$("#active").is(":checked")/*/,
        "calculate": $("#calculate").prop("checked")
    };

    $.getJSON("/App_Logic/dataJSONArray.ashx?mode=getschedule&idterm=" + idterm + "&id=" + id + "&scheduleid=" + scheduleid +
        "&nPlaneDay=" + $("#selectday").val() + "&tStart=" + $("#timein").val() + "&tEnd=" + $("#timeout").val() + "&sPlaneID=" + $("#selectplane").val(), function (Obj) {
        }).done(function (ObjJson) {
            if (ObjJson.length > 0) {
                $("body").mLoading('hide');
                alert("ไม่สามารถเพิ่มคาบในช่วงเวลานี้ได้");
                console.log(ObjJson);
                $("#add").removeClass("disabled");
            }
            else {
                $('#myModal').modal('hide');
                $.post("/App_Logic/insertDataJSON.ashx", JSONData, function () {
                }).done(function (result) {
                    $("body").mLoading('hide');
                    ListTIme();
                    return false;
                });
            }
        });
}
var planelist = [];
function ListTIme() {
    $("body").mLoading();
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
            var lastSubjectTimeStarted = "";
            var lastSubjectScheduleId = "";
            $.each(time, function (index) {
                var dataplane = { "planeid": time[index]["planeid"], "planename": time[index]["planename"], "course_code": time[index]["course_code"] }
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
                        var tooltip = "รหัสวิชา : " + time[index]["course_code"].replace(/"/g, "") + "<br/>"
                            + "ชื่อวิชา : " + time[index]["planename"] + "<br/>"
                            + "เวลา : " + time[index]["timestart"] + " - " + time[index]["timeend"];
                        if (lastSubjectTimeStarted != time[index]["timestart"]) {
                            $("#row" + time[index]["name"]).append("<td class='coldata' colspan=" + (indexend - indexstart) + " ><div class='divtooltip' id=" + time[index]["scheduleid"] +" style='text-align:center;' aria-hidden='true' data-html='true' title=\"" + tooltip + "\" onclick='editdata(" + time[index]["scheduleid"] + ")'>" + (time[index]["course_code"]).replace(/"/g, ""));
                            starttime = indexend;
                        }
                        else if (lastSubjectTimeStarted == time[index]["timestart"]) {
                            $("#" + lastSubjectScheduleId).parent("td").append("<div class='divtooltip' id=" + time[index]["scheduleid"] + " style='text-align:center;' aria-hidden='true' data-html='true' title=\"" + tooltip + "\" onclick='editdata(" + time[index]["scheduleid"] + ")'>" + (time[index]["course_code"]).replace(/"/g, ""));
                        }
                        lastSubjectTimeStarted = time[index]["timestart"];
                        lastSubjectScheduleId = time[index]["scheduleid"];
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

        $("body").mLoading('hide');
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

function DeleteExtractedDataFromOCR(rowIndex) {
    table.row(rowIndex).remove().draw(true);
    table.rows().invalidate('data')
        .draw(false);
}
function TeacherExtractDataOnChange(controlId, rowIndex, columnIndex, oldValue) {
    console.log($(controlId).val());
    table.cell(rowIndex, columnIndex).data($(controlId).val()).draw();
    table.row(rowIndex).invalidate('data').draw(false);
    $('.chosen-select').chosen({ allow_single_deselect: true });
}
function CourseCodeExtractDataOnChange(controlId, rowIndex, columnIndex, oldValue) {
    $("body").mLoading();
    if ($(controlId).val().indexOf("--") != "-1") {
        var subject = $(controlId).val().split("--");
        table.cell(rowIndex, columnIndex).data(subject[0]).draw();
        //console.log("Subject" + subject);
        var planTeachers = [];
        PageMethods.GetTeachersByCourseCode(para1var, para2var, subject[0],
            function (responseTeachers) {
                responseTeachers = $.parseJSON(responseTeachers);
                console.log(responseTeachers);
                planTeachers.splice(0, planTeachers.length);
                $.each(responseTeachers, function (index, Value) {
                    planTeachers.push({ sEmp: Value.SEmp, teacherFullName: Value.TeacherFullName });
                });
                table.cell(rowIndex, 8).data(planTeachers).draw();
                table.cell(rowIndex, 5).data("พบ").draw(); // Match or Not Match
                console.log("CourseCodeExtractDataOnChange done");
                table.row(rowIndex).invalidate('data').draw(false);
                $("body").mLoading('hide');
        });
    }

   
    //console.log($(controlId).val());

    //console.log(columnIndex);
    //console.log(oldValue);
}

function ExtractDataOnChange(controlId, rowIndex, columnIndex, oldValue) {

    table.cell(rowIndex, columnIndex).data($(controlId).val()).draw();
    table.row(rowIndex).invalidate('data').draw(false);
    //console.log($(controlId).val());
    $('.clockpicker').clockpicker();
    //console.log(columnIndex);
    //console.log(oldValue);
   
}
var table;

$(document).ready(function () {
   
    var nTerm = getUrlParameter("idterm");
    var nTermSubLevel2 = getUrlParameter("id");
    console.log("document loaded");
    
    var schoolId = 0;
    //Enable and Disable Import Button
    $.ajax({
        url: "/api/TimeTable/CheckTimeTableExist?nTermSubLevel2=" + nTermSubLevel2 + "&nTerm=" + nTerm,
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response)
            if (response == 0) {
                $("#import").hide();
            }
            else {
                schoolId = response;
                $("#import").show();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    });

    //Import Dialog Button Functionality
    $(".input-file").before(
        function () {
            if (!$(this).prev().hasClass('input-ghost')) {
                var element = $("<input type='file' id='ImportFile' accept='.pdf,.xlsx' class='input-ghost' style='visibility:hidden; height:0'>");
                element.attr("name", $(this).attr("name"));
                element.change(function () {
                    element.next(element).find('input').val((element.val()).split('\\').pop());
                });
                $(this).find("button.btn-choose").click(function () {
                    element.click();
                });
                $(this).find("button.btn-reset").click(function () {
                    element.val(null);
                    $(this).parents(".input-file").find('input').val('');
                    $("#InValidData").html("");
                });
                $(this).find('input').css("cursor", "pointer");
                $(this).find('input').mousedown(function () {
                    $(this).parents('.input-file').prev().click();
                    return false;
                });
                return element;
            }
        }
    );

    function isJsonString(str) {
        try {
            JSON.parse(str);
        } catch (e) {
            return false;
        }
        return true;
    }

    function addKeyValue(obj, key, data) {
        obj[key] = data;
    }

    

    table = $('#ExtractedData').DataTable();
    $("#SaveExtractedData").click(function () {
        console.log("SaveExtractedData");
        $("body").mLoading();
        var timeTable = [];
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            timeTable.push(data);
        });

        var importTimeTableRequest = { TimeTable: timeTable, nTermSubLevel2: nTermSubLevel2, nTerm: nTerm };
        $.ajax({
            url: '/api/TimeTable/ImportTimeTableFromPdf',
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(importTimeTableRequest),
            success: function (result) {
                console.log(result);
                var content = '';
                if ($.isArray(result)) {
                    $.each(result, function (index, data) {
                        content += '<div class="col-xs-12" style="padding: 3px;">' +
                            '<label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">' +
                            data.Day + '</label>' +
                            '<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">' +
                            '<label class="control-label" style="font-size: 170%">' +
                            data.CourseCode + '</label>' +
                            '</div>' +
                            '</div >';
                    });

                    content = '<div class="col-xs-12" style="padding: 3px;"><label id="" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label alert-danger ErrorMessage" style="width:100%;font-size: 170%">ข้อมูลไม่ถูกต้อง</label></div>' +
                        content;
                    $("#InValidExtractData").html(content);
                    $("body").mLoading('hide');
                }
                else {
                    $("#InValidExtractData").html("");
                    $("#ShowExtractedDataFromFile").modal("hide");
                    window.location.reload();
                    //$("body").mLoading('hide');
                }
                $("body").mLoading('hide');
            },
            error: function (err) {
                console.log(err.statusText);
            },
        });
    });
   
    function ShowExtractedDataFromOCR(response) {
        addKeyValue(response, 'edit', 'edit');
        console.log(response);
        table.destroy();
        table.clear().draw();
        //console.log(response[1].planCourseTeacherDTOs);
        PageMethods.GetPlanCourse(para1var, para2var,
            function (responseSubject) {
                //console.log(responseSubject);
                responseSubject = $.parseJSON(responseSubject);

                table = $('#ExtractedData').DataTable({
                    bFilter: false,
                    bSort: false,
                    bPaginate: false,
                    //scrollY: '500px',
                    //scrollCollapse: true,
                    "sDom": 'Rlfrtlip',

                    'data': response,
                    'columns': [
                        { 'data': 'day' },
                        { 'data': 'course_code' },
                        { 'data': 'time_start' },
                        { 'data': 'time_end' },
                        { 'data': 'teacher_name' },
                        { 'data': 'isValidData' },
                        { 'data': 'edit' },
                        { 'data': 'user_name', 'visible': false },
                        { 'data': 'planCourseTeacherDTOs', 'visible': false },
                       
                    ],
                    'columnDefs': [
                        {
                            'targets': 1, //Course Code
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {

                                var $select = $("<select style='width:200px' data-placeholder=\"เลือกวิชาเรียน\" class=\"chosen-select\" onFocus=(this.oldValue=this.value) onchange=\"CourseCodeExtractDataOnChange(this, " + meta.row + ", " + meta.col + ", this.oldValue)\"></select>", {
                                    "id": row[0] + meta.row + "CourseCode",
                                    "value": data
                                });
                                var isDataMatching = false;
                                $.each(responseSubject, function (index) {
                                    var $option = $("<option></option>", {
                                        "text": responseSubject[index].name,
                                        "value": responseSubject[index].name
                                    });

                                    if (responseSubject[index].name.indexOf(data) != "-1" && data != "-" && data != "--") {
                                        isDataMatching = true;
                                        $option.attr("selected", "selected");
                                    }
                                    
                                    $select.append($option);
                                });

                                if (!isDataMatching) {
                                    var $option = $("<option></option>", {
                                        "text": data,
                                        "value": data
                                    });
                                    $option.attr("selected", "selected");
                                    $select.append($option);
                                }
                                return $select.prop("outerHTML");

                                //return '<input type="text" onFocus=(this.oldValue=this.value) value="' + data + '" onchange="ExtractDataOnChange(this, ' + meta.row +',' + meta.col + ', this.oldValue)"/>';
                            }
                        },
                        {
                            'targets': 2, //Time In
                            'searchable': false,
                            'orderable': false,
                            'width': "50px",
                            'render': function (data, type, full, meta) {
                                var content = '<div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;"><label id = "lbltimein" ></label>' +
                                    '<div class="input-group clockpicker addvalues" style="width:90px !important;" data-placement="left" data-align="top" data-autoclose="true" >' +
                                    '<input type="text" onFocus=(this.oldValue=this.value) value="' + data + '" class="form-control mon clock-box" id="ExtractedTimeIn" onchange="ExtractDataOnChange(this, ' + meta.row + ',' + meta.col + ', this.oldValue)" name="ExtractedTimeIn" placeholder="00:00" />' +
                                    '<span class="input-group-addon">' +
                                    '<span class="glyphicon glyphicon-time"></span>' +
                                    '</span>' +
                                    '</div></div>';
                                return content;
                                //return '<input type="text" style="width:50px" onFocus=(this.oldValue=this.value) value="' + data + '" onchange="ExtractDataOnChange(this, ' + meta.row + ',' + meta.col + ', this.oldValue)"/>';
                            }
                        },
                        {
                            'targets': 3,  //Timeout
                            'searchable': false,
                            'orderable': false,
                            'width': "50px",
                            'render': function (data, type, full, meta) {

                                var content = '<div class="col-lg-12 col-md-12 col-sm-12" style="vertical-align: text-bottom;"><label id = "lbltimein" ></label>' +
                                    '<div class="input-group clockpicker addvalues" style="width:90px !important;" data-placement="left" data-align="top" data-autoclose="true" >' +
                                    '<input type="text" onFocus=(this.oldValue=this.value) value="' + data + '" class="form-control mon clock-box" id="ExtractedTimeout" onchange="ExtractDataOnChange(this, ' + meta.row + ',' + meta.col + ', this.oldValue)" name="ExtractedTimeout" placeholder="00:00" />' +
                                    '<span class="input-group-addon">' +
                                    '<span class="glyphicon glyphicon-time"></span>' +
                                    '</span>' +
                                    '</div></div>';
                                return content; //'<input type="text" style="width:50px" onFocus=(this.oldValue=this.value) value="' + data + '" onchange="ExtractDataOnChange(this, ' + meta.row + ',' + meta.col + ', this.oldValue)"/>';
                            }
                        },
                        {
                            'targets': 4, //Teacher Name
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, row, meta) {
                             
                               /* console.log(response[meta.row].planCourseTeacherDTOs);*/
                                //onchange=\"CourseCodeExtractDataOnChange(this, " + meta.row + ", " + meta.col + ", this.oldValue)\"
                                var $select = $("<select style='width:100px' class=\"chosen-select\" onFocus=(this.oldValue=this.value) onchange=\"TeacherExtractDataOnChange(this, " + meta.row + ", " + meta.col + ", this.oldValue)\"></select>", {
                                    "id": row[0] + meta.row + "planTeacher",
                                    "value": data
                                });
                               
                                if (row["planCourseTeacherDTOs"] != "null" && row["planCourseTeacherDTOs"] != null && row["planCourseTeacherDTOs"] != undefined) {
                                    var planTeachers = row["planCourseTeacherDTOs"];

                                    var $option = $("<option></option>", {
                                        "text": "เลือกชื่อผู้สอน",
                                        "value": ""
                                    });
                                    $select.append($option);
                                    $.each(planTeachers, function (index) {
                                        var $option = $("<option></option>", {
                                            "text": planTeachers[index].teacherFullName,
                                            "value": planTeachers[index].teacherFullName
                                        });

                                        if (planTeachers[index].teacherFullName != undefined && planTeachers[index].teacherFullName.indexOf(data) != "-1" && data != "") {
                                            $option.attr("selected", "selected");
                                        }

                                        $select.append($option);
                                    });
                                }
                                else {
                                    var $option = $("<option></option>", {
                                        "text": "เลือกชื่อผู้สอน",
                                        "value": ""
                                    });
                                    $select.append($option);
                                }


                                
                                return $select.prop("outerHTML");
                                //return '<input type="text" onFocus=(this.oldValue=this.value) value="' + data + '" onchange="ExtractDataOnChange(this, ' + meta.row + ',' + meta.col + ', this.oldValue)"/>';
                            }
                        },
                        {
                            'targets': 5, //Match or Not Match
                            'searchable': false,
                            'orderable': false,
                            'render': function (data, type, full, meta) {

                                data = (data == "โมฆะ") ? "ไม่พบ" : data;
                                return (data == "ไม่พบ") ? '<span style="color:red">' + data + '</span>' : data;
                            }
                        },
                        {
                            'targets': 6, //Delete
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-left',
                            'width': '5%',
                            'render': function (data, type, full, meta) {
                                return '<div class="glyphicon glyphicon-remove" style="font-size: 70%; cursor: pointer; color: red; font-size: 70%" onclick="DeleteExtractedDataFromOCR(' + meta.row + ')"></div >';
                            }
                        }],
                    fnInitComplete: function () {
                        $('.clockpicker').clockpicker();
                        $("body").mLoading('hide');
                        console.log("fnInitComplete");
                        $('.chosen-select').chosen({ allow_single_deselect: true });
                    }

                });
            }
        );
       
       
        
    }
   
    $('#ImportFile:file').change(function () {

        $("body").mLoading();
        console.log("File Changed");
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var files = this.files;

            // Create FormData object
            var fileData = new FormData();

            var isExcelFile = true;
            var isImageFile = false;
            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileData.append(files[i].name, files[i]);
                console.log(files[i].name.toLowerCase());
                if (files[i].name.toLowerCase().indexOf(".pdf") != -1) {
                    isExcelFile = false;  //pdf file
                }

                if (files[i].name.toLowerCase().indexOf(".pdf") == -1 && files[i].name.toLowerCase().indexOf(".xlsx") == -1) {
                    isImageFile = true;
                }
            }
            console.log("Image File" + isImageFile);
            if (!isImageFile) {
                console.log(isExcelFile);
                var url = '/api/TimeTable/ImportTimeTable?&nTermSubLevel2=' + nTermSubLevel2 + "&nTerm=" + nTerm;
                if (!isExcelFile) {
                    url = 'https://grade.schoolbright.co/api/TimeTable/ImportTimeTable?&nTermSubLevel2=' + nTermSubLevel2 + "&nTerm=" + nTerm + "&schoolid=" + schoolId;
                    //url = 'https://localhost:44396/api/TimeTable/ImportTimeTable?&nTermSubLevel2=' + nTermSubLevel2 + "&nTerm=" + nTerm + "&schoolid=" + schoolId;
                }
                $.ajax({
                    //url: '/api/TimeTable/ImportTimeTable?&nTermSubLevel2=' + nTermSubLevel2 + "&nTerm=" + nTerm,
                    url: url,
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    crossDomain: !isExcelFile,

                    data: fileData,
                    success: function (result) {
                        console.log(result);
                        if (isExcelFile) {
                            var content = '';
                            if ($.isArray(result)) {
                                $.each(result, function (index, data) {
                                    content += '<div class="col-xs-12" style="padding: 3px;">' +
                                        '<label class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label" style="font-size: 170%">' +
                                        data.Day + '</label>' +
                                        '<div class="col-lg-8 col-md-8 col-sm-8 col-xs-8 control-input">' +
                                        '<label class="control-label" style="font-size: 170%">' +
                                        data.CourseCode + '</label>' +
                                        '</div>' +
                                        '</div >';
                                });

                                content = '<div class="col-xs-12" style="padding: 3px;"><label id="" class="col-lg-4 col-md-4 col-sm-4 col-xs-4 control-label alert-danger ErrorMessage" style="width:100%;font-size: 170%">ข้อมูลไม่ถูกต้อง</label></div>' +
                                    content;
                                $("#InValidData").html(content);
                                $("body").mLoading('hide');
                            }
                            else {
                                $("#InValidData").html("");
                                $("#ImportModal").modal("hide");
                                window.location.reload();
                                //$("body").mLoading('hide');
                            }
                        }
                        else { //Except Excel File
                            if (typeof result != "string") {
                                $("#InValidData").html("");
                                $("#ImportModal").modal("hide");
                                $("#ShowExtractedDataFromFile").modal("show");
                                ShowExtractedDataFromOCR(result);
                            }
                            else {
                                $("#InValidData").html("Please try after some time");
                                console.log("COR Error");
                                $("body").mLoading('hide');
                            }
                        }

                    },
                    error: function (err) {
                        console.log(err.statusText); //Not able to read file. Please check file quality or make sure file pattern is supported.
                        $("#InValidData").html("<span style='color:red;font-weight:bold;font-size:16px'>ไม่สามารถอ่านไฟล์ได้ กรุณาตรวจสอบคุณภาพภาพของไฟล์หรือรูปแบบของไฟล์ให้ถูกต้อง</span>");
                        $("body").mLoading('hide');
                    },
                    xhr: function () {
                        var xhr = new window.XMLHttpRequest();

                        // Upload progress
                        xhr.upload.addEventListener("progress", function (evt) {
                            if (evt.lengthComputable) {
                                var percentComplete = evt.loaded / evt.total;
                                //Do something with upload progress
                                console.log(percentComplete);
                            }
                            if (evt.lengthComputable) {
                                var percentComplete = evt.loaded / evt.total;
                                console.log(Math.round(percentComplete * 100) + "%");
                            }
                        }, false);

                        // Download progress
                        xhr.addEventListener("progress", function (evt) {
                            if (evt.lengthComputable) {
                                var percentComplete = evt.loaded / evt.total;
                                // Do something with download progress
                                console.log(percentComplete);
                            }
                        }, false);

                        return xhr;
                    }
                });
            }
            else {
                console.log("Wrong Excel");
                $("#InValidData").html("<span style='color:red;font-weight:bold'>อนุญาตให้ใช้ไฟล์ PDF และ Excel เท่านั้น</b>");
                $("body").mLoading('hide');
                return false;
            }
        } else {
            console.log("FormData is not supported.");
        }
    });
});

$(window).on("load", function () {
    console.log("window loaded");
});