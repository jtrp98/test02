var availableValuestudent = [];
$studentid = "";
var levelinClass = "";
var Reports_Data02 = [];
$(function () {
    //$('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });
    if (document.getElementById('txtname')) {
        document.getElementById('txtname').addEventListener('input', function () {
            if (this.value === '') {
                $("#txtid").val("");
            }
        }, false);

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
                $studentid = ui.item.code;
            },
            focus: function (event, ui) {
                event.preventDefault();
                $("#txtid").val("");
            }
        });
    }

    $('#ctl00_MainContent_ddlsublevel').change(function () {
        $('input[id*=txtSubLV2ID]').val("");
        getListSubLV2();
        getliststudent();
    });

    getListSubLV2();

    $('#ctl00_MainContent_ddlSubLV2').change(function () {
        GetSchedule();
        getliststudent();
    });



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

    getListTrem();
    $('.datepicker').datepicker({});

    $("select[id*=ddlyear]").change(function () {
        getListTrem();
    });

    $("#exportfile").click(function () {
        Reports_01.export_excel();
        //if ($("#txtname").val() != "") {
        //    var dt = new Date();
        //    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการมาโรงเรียน', fileName: "รายงานสรุปสถิติการมาโรงเรียน_" + $("#txtname").val() + "_" + dt.toLocaleDateString() + "_" + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        //} else if (levelinClass != 0) {
        //    var dt = new Date();
        //    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการมาโรงเรียน', fileName: "รายงานสรุปสถิติการมาโรงเรียน_" + $("input[id*=hdfschoolname]").val() + "_" + levelinClass.toString() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        //}
        //else {
        //    var dt = new Date();
        //    $('#example').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปสถิติการมาโรงเรียน', fileName: "รายงานสรุปสถิติการมาโรงเรียน_" + $("input[id*=hdfschoolname]").val() + "_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
        //}
    });

    $("select[id*=semister]").change(function () {
        setMaxMinDate();
    })

    $("#select_Report").change(function () {
        switch ($(this).val()) {
            case "0": Reports_01.RenderHtml02_01("example", false); break;
            case "1": Reports_01.RenderHtml02_02("example", false); break;
            case "2": Reports_01.RenderHtml02_03("example", false); break;
            case "3": Reports_01.RenderHtml02_04("example", false); break;
            case "4": Reports_01.RenderHtml02_05("example", false); break;
        }
    });

});

function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}
function w3_close() {
    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}

function getdata(data, key, valuse) {
    var result = [];
    $.each(data, function (e, s) {
        if (s[key].toString() === valuse)
            result = s;
    });
    return result;
}

var trem = [];
function getListSubLV2() {
    //alert($('#ctl00_MainContent_ddlSubLV option:selected').val())
    var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
    var sSubLV = $('#ctl00_MainContent_ddlsublevel option:selected').text();
    $('select[id*=ddlSubLV2] option').remove();
    $('select[id*=ddlSubLV2]')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));
    if (SubLVID) {
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
}

function setMaxMinDate() {
    var trem_data = getdata(trem, "nTerm", $("select[id*=semister]").val());
    $('.datepicker').datepicker("destroy");
    $('.datepicker').removeClass("hasDatepicker");
    $('.datepicker').val("");
    $('.datepicker').datepicker({
        minDate: new Date(trem_data.dStart), maxDate: new Date(trem_data.dEnd),
        defaultDate: new Date(trem_data.dStart)
    });
}

function getListTrem() {
    var YearID = $('#ctl00_MainContent_ddlyear option:selected').val();
    var YearNumber = $('#ctl00_MainContent_ddlyear option:selected').text();
    $("#ctl00_MainContent_semister option").remove();
    if (YearID) {
        $.ajax({
            url: "/App_Logic/dataGeneric.ashx?mode=listterm&id=" + YearID,
            success: function (msg) {
                trem = msg;
                $.each(msg, function (index) {
                    $('select[id*=semister]')
                        .append($("<option></option>")
                            .attr("value", msg[index].nTerm)
                            .text(msg[index].sTerm));
                });
                //setMaxMinDate();
            }
        });
    }
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
                    code: objjson[index].studentid,
                };
                availableValuestudent[index] = newObject;
            });
        }
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

function Reports01() {
    $(".w3-button").hide();
    var dt = new Date();
    var day = ("0" + (dt.getDate() + 1)).slice(-2) + "/" + ("0" + (dt.getMonth() + 1)).slice(-2) + "/" + dt.getFullYear();
    var daystart;
    var dayend;
    if ($("#txtstart").val() === "") {
        daystart = day;
    } else {
        daystart = new Date($("#txtstart").val().split('/')[2], $("#txtstart").val().split('/')[1] - 1, $("#txtstart").val().split('/')[0]);
    }

    if ($("#txtend").val() === "") {
        dayend = day;
    } else {
        dayend = new Date($("#txtend").val().split('/')[2], $("#txtend").val().split('/')[1] - 1, $("#txtend").val().split('/')[0]);
    }

    if ($("#txtid").val() !== "") {
        Reports01sortbystudent();
    }
    else if ($("select[id*=ddlSubLV2] option:selected").val() !== "") {
        if ($("#txtstart").val() !== "" && $("#txtend").val() !== "") {
            if (daystart < dayend) {
                Reports01notsort();
            } else if (daystart === dayend || $("#txtstart").val() === $("#txtend").val()) {
                Reports03($("#txtstart").val(), $("select[id*=ddlSubLV2] option:selected").val(), $("select[id*=ddlsublevel] option:selected").text(), $("select[id*=ddlSubLV2] option:selected").text());
            }
        }
        else if ($("#txtstart").val() !== "") {
            Reports03($("#txtstart").val(), $("select[id*=ddlSubLV2] option:selected").val(), $("select[id*=ddlsublevel] option:selected").text(), $("select[id*=ddlSubLV2] option:selected").text());
        }
        else {
            Reports03(day, $("select[id*=ddlSubLV2] option:selected").val(), $("select[id*=ddlsublevel] option:selected").text(), $("select[id*=ddlSubLV2] option:selected").text());
        }
    }
    else if ($("select[id*=ddlsublevel] option:selected").val() !== "") {
        if ($("#txtstart").val() !== "" && $("#txtend").val() !== "") {
            if (daystart < dayend) {
                Reports01notsort();
            } else if (daystart === dayend || $("#txtstart").val() === $("#txtend").val()) {
                Reports02($("#txtstart").val());
            }
        }
        else if ($("#txtstart").val() !== "") {
            Reports02($("#txtstart").val());
        }
        else {
            Reports02(day);
        }
    }
    else {
        Reports01notsort();
    }
}

function Reports01sortbysublevel() {

}

function Reports01sortbystudent() {
    $("body").mLoading();
    var dt = new Date();

    var HtmlTable = $("#myTable");
    var Header = $("#myHeader");
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

    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px 1px;'id='school' colspan=13>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + `<tr><th colspan=13 style='text-align: center;font-size:24px;border-width:0px 1px;'id='headdatail'>รายงานสรุปสถิติการมาโรงเรียน</th></tr> `
        + `<tr><th colspan=13 style='text-align: center;font-size:24px;border-width:0px 1px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>"`
        + `<tr><th colspan=13 style='text-align: right;font-size:20px;border-width:0px 1px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><th colspan=13 style='text-align: right;font-size:20px;border-width:0px 1px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </th><th style="width:30%;border-width:0px;" colspan=3>&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<th style="text-align: right;width:20%;border-width:0px;">เทอม : </th><th colspan=8 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </th><th style="width:30%;border-width:0px;" colspan=3>&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
        + `<th style="text-align: right;width:20%; border-width:0px;">ชั้นเรียน : </th><th colspan=7 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">รหัสนักเรียน : </th><th style="width:30%;border-width:0px;" colspan=3>&nbsp;` + $studentid
        + `<th style="text-align: right;width:20%; border-width:0px;">ชื่อ-นามสกุล : </th><th colspan=7 style="width:30%;border-width:0px;">&nbsp;` + $("#txtname").val()
        + `<tr><th colspan=13 style="border-width:0px;"><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'><td id='headder'rowspan='2' style='width:100px;' >ลำดับ"
        + "<td id='headder'rowspan='2'style='width:100px;'>ชั้นเรียน<td id='headder' rowspan=2>ชื่อ <td id='headder' rowspan=2>นามสกุล<td id='headder'rowspan='2'>วันที่<td id='headder'rowspan='2'>เวลา<td id='headder'colspan='3'style='width:15%'>สถานะ<td id='headder'rowspan='2'>เวลา<td id='headder'colspan='3'style='width:15%'>สถานะ");
    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด"
        + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด");

    $.get("/App_Logic/Report/ReportCome2School.ashx?mode=reportscome2school03user" + sortday() + sortuser(), {}, function (Obj) {
        if (Obj.status === "Session Time Out") location.reload();
        var sumallstudent4level = 0;
        var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0;
        var sumstudent4level = 0;
        var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;
        var sumstatus_34level = 0, sumstatus_44level = 0, sumstatus_54level = 0;

        $rowsindex = 1;
        var template = $('#templatesortbystudent').html();
        var data = [];
        Mustache.parse(template);   // optional, speeds up future uses
        var rendered = Mustache.render(template, {
            scandata: Obj,
            statusin_0: function () {
                return (this.female_statusin_0 + this.male_statusin_0) === 1 ? "X" : "";
            },
            statusin_1: function () {
                return (this.female_statusin_1 + this.male_statusin_1) === 1 ? "X" : "";
            },
            statusin_2: function () {
                var status = this.female_statusin_2 + this.male_statusin_2;
                return (status === 1 || status === "3" ? "X" : "");
            },
            statusout_0: function () {
                return (this.female_statusout_0 + this.male_statusout_0) === 1 ? "X" : "";
            },
            statusout_1: function () {
                return (this.female_statusout_1 + this.male_statusout_1) === 1 ? "X" : "";
            },
            statusout_2: function () {
                var status = this.female_statusout_2 + this.male_statusout_2;
                return (status === 1 || status === "3" ? "X" : "");
            },
            index: function () {
                return $rowsindex++;
            },
            sumstatus_04level: function () {
                return "";
            }
        });
        $("#target").show();
        $('#target').html(rendered);

        $.each(Obj, function (index) {
            var day = Obj[index].dayscan;
            var Objstudent = Obj[index].student;
            var rowspan = Obj.length;

            $.each(Objstudent, function (indexstudent) {
                var statusin_0 = 0, statusin_1 = 0, statusin_2 = 0;
                var statusout_0 = 0, statusout_1 = 0, statusout_2 = 0;
                statusin_0 = Objstudent[indexstudent].female_statusin_0 + Objstudent[indexstudent].male_statusin_0;
                statusin_1 = Objstudent[indexstudent].female_statusin_1 + Objstudent[indexstudent].male_statusin_1;
                statusin_2 = Objstudent[indexstudent].female_statusin_2 + Objstudent[indexstudent].male_statusin_2;

                statusout_0 = Objstudent[indexstudent].female_statusout_0 + Objstudent[indexstudent].male_statusout_0;
                statusout_1 = Objstudent[indexstudent].female_statusout_1 + Objstudent[indexstudent].male_statusout_1;
                statusout_2 = Objstudent[indexstudent].female_statusout_2 + Objstudent[indexstudent].male_statusout_2;

                var level2name = Objstudent[indexstudent].level2name;
                var HtmlRow = "";
                if (index === 0) {
                    HtmlRow = "<tr><td class='center'>" + (index + 1) + "<td class='center' rowspan='" + rowspan + "'>" + level2name;
                }
                else {
                    HtmlRow = "<tr><td class='center'>" + (index + 1);
                }
                HtmlRow += "<td>" + Objstudent[indexstudent].studentname
                    + "<td>" + Objstudent[indexstudent].studentlastname
                    + "<td class='center'>" + day
                    + "<td class='center'>" + Objstudent[indexstudent].timein
                    + "<td  class='center'>" + (statusin_0 === 1 ? "X" : "")
                    + "<td  class='center'>" + (statusin_1 === 1 ? "X" : "")
                    + "<td  class='center'>" + (statusin_2 === 1 || statusin_2 === "3" ? "X" : "")
                    + "<td class='center'>" + Objstudent[indexstudent].timeout
                    + "<td  class='center'>" + (statusout_0 === 1 ? "X" : "")
                    + "<td  class='center'>" + (statusout_1 === 1 ? "X" : "")
                    + "<td  class='center'>" + (statusout_2 === 1 ? "X" : "");

                sumstudent4level += 1;
                sumstatus_04level += statusin_0;
                sumstatus_14level += statusin_1;
                sumstatus_24level += statusin_2;
                sumstatus_34level += statusout_0;
                sumstatus_44level += statusout_1;
                sumstatus_54level += statusout_2;


                HtmlTable.append(HtmlRow);
            })
        });
        HtmlTable.append("<tr><td colspan=6 class='right'>รวม"
            + "<td class='center'>" + sumstatus_04level + percent(sumstudent4level, sumstatus_04level)
            + "<td class='center'>" + sumstatus_14level + percent(sumstudent4level, sumstatus_14level)
            + "<td class='center'>" + sumstatus_24level + percent(sumstudent4level, sumstatus_24level)
            + "<td>รวม<td class='center'>" + sumstatus_34level + percent(sumstudent4level, sumstatus_34level)
            + "<td class='center'>" + sumstatus_44level + percent(sumstudent4level, sumstatus_44level)
            + "<td class='center'>" + sumstatus_54level + percent(sumstudent4level, sumstatus_54level));

    }).done(function () {
        $("body").mLoading('hide');
    });
}

function Reports01notsort() {
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

    var Colspan = 14;
    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px 1px;'id='school' colspan=" + Colspan + ">" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + `<tr><th colspan=` + Colspan + ` style='text-align: center;font-size:24px;border-width:0px 1px;'id='headdatail'>รายงานสรุปสถิติการมาโรงเรียน</th></tr> `
        + `<tr><th colspan=` + Colspan + ` style='text-align: center;font-size:24px;border-width:0px 1px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>`
        + `<tr><th colspan=` + Colspan + ` style='text-align: right;font-size:20px;border-width:0px 1px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><th colspan=` + Colspan + ` style='text-align: right;font-size:20px;border-width:0px 1px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </th><th style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<th colspan=` + (Colspan - 6) + `>`
        + `<th style="text-align: right;width:20%;border-width:0px;">เทอม : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </th><th style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlsublevel option:selected').text() + `</th>`
        + `<th colspan=` + (Colspan - 6) + `>`
        + `<th style="text-align: right;width:20%; border-width:0px;">ชั้นเรียน : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlSubLV2 option:selected').text() + `</th>`
        //+ `<tr><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ชื่อ - นามสกุล : </th><th style="width:30%;border-width:0px;">&nbsp;ทั้งหมด</th>`
        //+ `<th style="text-align: right;width:20%; border-width:0px;">สถานะ : </th><th colspan=3 style="width:30%;border-width:0px;">&nbsp;ทั้งหมด</th>`
        + `<tr><th colspan=` + Colspan + ` style="border-width:0px;"><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:20px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder' rowspan='2'>ลำดับ<td id='headder'rowspan='2'>วันที่"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนทั้งหมด"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนตรงเวลาทั้งหมด"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนสายทั้งหมด"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนขาดทั้งหมด");

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม");
    var sum_status_0 = 0, sum_status_1 = 0, sum_status_2 = 0, student_number = 0;
    var female_status_0 = 0, female_status_1 = 0, female_status_2 = 0;
    var male_status_0 = 0, male_status_1 = 0, male_status_2 = 0;
    var female = 0, male = 0;
    $.get("/App_Logic/Report/ReportCome2School.ashx?mode=reportscome2school01user" + sortday() + sortlevel() + sortuser(), {}, function (Obj) {
        if (Obj.status === "Session Time Out") location.reload();
        $.each(Obj, function (index) {
            var status_0 = 0, status_1 = 0, status_2 = 0;
            status_0 = Obj[index].female_status_0 + Obj[index].male_status_0;
            status_1 = Obj[index].female_status_1 + Obj[index].male_status_1;
            status_2 = Obj[index].female_status_2 + Obj[index].male_status_2;

            male += (Obj[index].male_status_0 + Obj[index].male_status_1 + Obj[index].male_status_2);
            female += (Obj[index].female_status_0 + Obj[index].female_status_1 + Obj[index].female_status_2);

            HtmlTable.append("<tr><td class='center'>" + (index + 1) + "<td style='color:royalblue;' onclick='Reports02(" + '"' + Obj[index].day + '"' + ")' class='center'>" + Obj[index].day
                + "<td class='center'>" + (Obj[index].male_status_0 + Obj[index].male_status_1 + Obj[index].male_status_2) + "<td class='center'>" + (Obj[index].female_status_0 + Obj[index].female_status_1 + Obj[index].female_status_2) + "<td class='center'>" + Obj[index].studentnumber
                + "<td class='center'>" + Obj[index].male_status_0 + "<td class='center'>" + Obj[index].female_status_0 + "<td class='center'>" + status_0
                + "<td class='center'>" + Obj[index].male_status_1 + "<td class='center'>" + Obj[index].female_status_1 + "<td class='center'>" + status_1
                + "<td class='center'>" + Obj[index].male_status_2 + "<td class='center'>" + Obj[index].female_status_2 + "<td class='center'>" + status_2);

            student_number += Obj[index].studentnumber;
            sum_status_0 += status_0;
            sum_status_1 += status_1;
            sum_status_2 += status_2;

            female_status_0 += Obj[index].female_status_0;
            female_status_1 += Obj[index].female_status_1;
            female_status_2 += Obj[index].female_status_2;

            male_status_0 += Obj[index].male_status_0;
            male_status_1 += Obj[index].male_status_1;
            male_status_2 += Obj[index].male_status_2;


        });
        var Html = "";

        HtmlTable.append("<tr><td colspan=2>"
            + "<td class='center'>" + male + percentShowText(student_number, male)
            + "<td class='center'>" + female + percentShowText(student_number, female) + "<td class='center'>" + student_number
            + "<td class='center'>" + male_status_0 + percentShowText(student_number, male_status_0)
            + "<td class='center'>" + female_status_0 + percentShowText(student_number, female_status_0)
            + "<td class='center'>" + sum_status_0 + percentShowText(student_number, sum_status_0)
            + "<td class='center'>" + male_status_1 + percentShowText(student_number, male_status_1)
            + "<td class='center'>" + female_status_1 + percentShowText(student_number, female_status_1)
            + "<td class='center'>" + sum_status_1 + percentShowText(student_number, sum_status_1)
            + "<td class='center'>" + male_status_2 + percentShowText(student_number, male_status_2)
            + "<td class='center'>" + female_status_2 + percentShowText(student_number, female_status_2)
            + "<td class='center'>" + sum_status_2 + percentShowText(student_number, sum_status_2));
        HtmlTable.append(Html);

        $("body").mLoading('hide');
    });
}

function percent(maxuser, countstatus) {
    if (maxuser === 0) return 0
    else return ((100 * countstatus) / maxuser).toFixed(2);
}

function percentShowText(maxuser, countstatus) {
    return " ( " + ((100 * countstatus) / maxuser).toFixed(2) + " ) %";
}

var ReportDate = "";
function Reports02(day) {
    $(".w3-button").show();
    ReportDate = day;
    var dStart = "", dEnd = "";
    $("body").mLoading();
    dStart = getDate(day);

    Search = {
        "term_id": $('select[id*=semister]').val(), "level_id": $('select[id*=ddlsublevel]').val(),
        "sort_type": "0", "dStart": dStart, "dEnd": dEnd
    };

    PageMethods.report02UsersView02(Search, function (response) {
        if (response.status === "Session Time Out") location.reload();
        Reports_01.reports_data = response;
        if ($("#select_Report").val() === "0") {
            Reports_01.RenderHtml02_01("example", false);
        } else if ($("#select_Report").val() === "1") {
            Reports_01.RenderHtml02_02("example", false);
        } else if ($("#select_Report").val() === "2") {
            Reports_01.RenderHtml02_03("example", false);
        } else if ($("#select_Report").val() === "3") {
            Reports_01.RenderHtml02_04("example", false);
        } else if ($("#select_Report").val() === "4") {
            Reports_01.RenderHtml02_05("example", false);
        }
    });
}

function Reports03(day, level2, levelname, level2name) {
    $("body").mLoading();

    var dt = new Date();
    var Header = $("#myHeader");
    var HtmlTable = $("#myTable");
    Header.html("");
    HtmlTable.html("");
    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px 1px;'id='school' colspan=12>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px 1px;'id='headdatail'>รายงานสรุปสถิติการมาโรงเรียน</th></tr> `
        + `<tr><th colspan=12 style='text-align: center;font-size:24px;border-width:0px 1px;'id='dayfall'>ประจำวันที่ ` + day + `</th></tr>"`
        + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px 1px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><th colspan=12 style='text-align: right;font-size:20px;border-width:0px 1px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </th><th style="width:30%;border-width:0px;" colspan=2>&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<th style="text-align: right;width:20%;border-width:0px;">เทอม : </th><th colspan=7 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text()
        + `<tr style="font-size:20px;"><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ระดับชั้น : </th><th style="width:30%;border-width:0px;" colspan=2>&nbsp;` + levelname + `</th>`
        + `<th style="text-align: right;width:20%; border-width:0px;">ชั้นเรียน : </th><th colspan=7 style="width:30%;border-width:0px;">&nbsp;` + level2name + `</th>`
        //+ `<tr><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ชื่อ - นามสกุล : </th><th style="width:30%;border-width:0px;">&nbsp;ทั้งหมด</th>`
        //+ `<th style="text-align: right;width:20%; border-width:0px;">สถานะ : </th><th colspan=3 style="width:30%;border-width:0px;">&nbsp;ทั้งหมด</th>`
        + `<tr><th colspan=13 style="border-width:0px;"><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'><td id='headder'rowspan='2' style='width:100px;' >ลำดับ"
        + "<td id='headder'rowspan='2'style='width:100px;'>ชั้นเรียน<td id='headder' rowspan=2>ชื่อ <td id='headder' rowspan=2>นามสกุล<td id='headder'rowspan='2'>เวลา<td id='headder'colspan='3'style='width:15%'>สถานะ<td id='headder'rowspan='2'>เวลา<td id='headder'colspan='3'style='width:15%'>สถานะ");
    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด"
        + "<td id='headder'style='width:5%'>ตรงเวลา<td id='headder'style='width:5%'>สาย<td id='headder'style='width:5%'>ขาด");

    $.get("/App_Logic/Report/ReportCome2School.ashx?mode=reportscome2school03user&day=" + day + "&level2=" + level2 + sortuser(), {}, function (Obj) {
        if (Obj.status === "Session Time Out") location.reload();
        var sumallstudent4level = 0;
        var sumallstatus_0 = 0, sumallstatus_1 = 0, sumallstatus_2 = 0;
        var sumstudent4level = 0;
        var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;
        var sumstatus_34level = 0, sumstatus_44level = 0, sumstatus_54level = 0;

        var female_statusin_0 = 0, female_statusin_1 = 0, female_statusin_2 = 0;
        var female_statusout_0 = 0, female_statusout_1 = 0, female_statusout_2 = 0;

        var male_statusin_0 = 0, male_statusin_1 = 0, male_statusin_2 = 0;
        var male_statusout_0 = 0, male_statusout_1 = 0, male_statusout_2 = 0;
        var female = 0, male = 0;
        $.each(Obj, function (index) {
            var day = Obj[index].dayscan;
            var Objstudent = Obj[index].student;
            var rowspan = Objstudent.length;

            $.each(Objstudent, function (indexstudent) {
                var statusin_0 = 0, statusin_1 = 0, statusin_2 = 0;
                var statusout_0 = 0, statusout_1 = 0, statusout_2 = 0;
                statusin_0 = Objstudent[indexstudent].female_statusin_0 + Objstudent[indexstudent].male_statusin_0;
                statusin_1 = Objstudent[indexstudent].female_statusin_1 + Objstudent[indexstudent].male_statusin_1;
                statusin_2 = Objstudent[indexstudent].female_statusin_2 + Objstudent[indexstudent].male_statusin_2;

                statusout_0 = Objstudent[indexstudent].female_statusout_0 + Objstudent[indexstudent].male_statusout_0;
                statusout_1 = Objstudent[indexstudent].female_statusout_1 + Objstudent[indexstudent].male_statusout_1;
                statusout_2 = Objstudent[indexstudent].female_statusout_2 + Objstudent[indexstudent].male_statusout_2;

                female_statusin_0 += Objstudent[indexstudent].female_statusin_0;
                female_statusin_1 += Objstudent[indexstudent].female_statusin_1;
                female_statusin_2 += Objstudent[indexstudent].female_statusin_2;

                male_statusin_0 += Objstudent[indexstudent].male_statusin_0;
                male_statusin_1 += Objstudent[indexstudent].male_statusin_1;
                male_statusin_2 += Objstudent[indexstudent].male_statusin_2;

                female_statusout_0 += Objstudent[indexstudent].female_statusout_0;
                female_statusout_1 += Objstudent[indexstudent].female_statusout_1;
                female_statusout_2 += Objstudent[indexstudent].female_statusout_2;

                male_statusout_0 += Objstudent[indexstudent].male_statusout_0;
                male_statusout_1 += Objstudent[indexstudent].male_statusout_1;
                male_statusout_2 += Objstudent[indexstudent].male_statusout_2;

                if ((Objstudent[indexstudent].female_statusin_0 + Objstudent[indexstudent].female_statusin_1 + Objstudent[indexstudent].female_statusin_2) > 0) female += 1;
                else male += 1;

                var level2name = Objstudent[indexstudent].level2name;
                levelinClass = level2name;
                var HtmlRow = "";
                if (indexstudent === 0) {
                    HtmlRow = "<tr><td class='center'>" + (indexstudent + 1) + "<td class='center' rowspan='" + rowspan + "'>" + level2name;
                }
                else {
                    HtmlRow = "<tr><td class='center'>" + (indexstudent + 1);
                }
                HtmlRow += "<td>" + Objstudent[indexstudent].studentname
                    + "<td>" + Objstudent[indexstudent].studentlastname
                    + "<td class='center'>" + Objstudent[indexstudent].timein
                    + "<td class='center'>" + (statusin_0 === 1 ? "X" : "")
                    + "<td class='center'>" + (statusin_1 === 1 ? "X" : "")
                    + "<td class='center'>" + (statusin_2 >= 1 || statusin_2 === "3" ? "X" : "")
                    + "<td class='center'>" + Objstudent[indexstudent].timeout
                    + "<td class='center'>" + (statusout_0 === 1 ? "X" : "")
                    + "<td class='center'>" + (statusout_1 === 1 ? "X" : "")
                    + "<td class='center'>" + (statusout_2 === 1 ? "X" : "");

                sumstudent4level += 1;
                sumstatus_04level += statusin_0;
                sumstatus_14level += statusin_1;
                sumstatus_24level += statusin_2;
                sumstatus_34level += statusout_0;
                sumstatus_44level += statusout_1;
                sumstatus_54level += statusout_2;
                HtmlTable.append(HtmlRow);
            })
        });

        var Html = "";
        Html = String.format("<tr id='total' style='font-weight: bold; border:0px;' rowspan=4><td class='right'>สรุป<td colspan=11>มาเรียน {0} คน {1} ชาย {2} คน {3} หญิง {4} คน {5}"
            + "<tr style='font-weight: bold; border:0px;'><td colspan=1><td colspan=11>สาย {6} คน {7} ชาย {8} คน {9} หญิง {10} คน {11}"
            + "<tr style='font-weight: bold; border:0px;'><td colspan=1><td colspan=11>ขาดเรียน {12} คน {13} ชาย {14} คน {15} หญิง {16} คน {17}",
            sumstatus_04level, percent(sumstudent4level, sumstatus_04level), male_statusin_0, percent(sumstudent4level, male_statusin_0), female_statusin_0, percent(sumstudent4level, female_statusin_0),
            sumstatus_14level, percent(sumstudent4level, sumstatus_14level), male_statusin_1, percent(sumstudent4level, male_statusin_1), female_statusin_1, percent(sumstudent4level, female_statusin_1),
            sumstatus_24level, percent(sumstudent4level, sumstatus_24level), male_statusin_2, percent(sumstudent4level, male_statusin_2), female_statusin_2, percent(sumstudent4level, female_statusin_2));
        HtmlTable.append(Html);

        //HtmlTable.append("<tr  style='font-weight: bold;'><td><td><td><td><td class='right'>รวม"
        //    + "<td class='center'>" + sumstatus_04level + percent(sumstudent4level, sumstatus_04level)
        //    + "<td class='center'>" + sumstatus_14level + percent(sumstudent4level, sumstatus_14level)
        //    + "<td class='center'>" + sumstatus_24level + percent(sumstudent4level, sumstatus_24level)
        //    + "<td>รวม<td class='center'>" + sumstatus_34level + percent(sumstudent4level, sumstatus_34level)
        //    + "<td class='center'>" + sumstatus_44level + percent(sumstudent4level, sumstatus_44level)
        //    + "<td class='center'>" + sumstatus_54level + percent(sumstudent4level, sumstatus_54level));
        $("body").mLoading('hide');
    });
}

function sortlevel() {
    var level = $("select[id*=ddlsublevel] option:selected").val() === null ? "" : $("select[id*=ddlsublevel] option:selected").val();
    var level2 = $("select[id*=ddlSubLV2] option:selected").val() === null ? "" : $("select[id*=ddlSubLV2] option:selected").val();
    var sort = "&level2=" + level2 + "&level=" + level;
    return sort;
}

function sortday() {
    var trem_data = getdata(trem, "nTerm", $("select[id*=semister]").val());
    var maxDate = new Date(trem_data.dEnd);
    var start = $("#txtstart").val();
    var end = $("#txtend").val();
    var sort = "";

    if (start !== "") {
        sort += "&day=" + start
    }
    else if (maxDate <= new Date()) {
        sort += "&day=" + trem_data.dEnd;
    }

    if (end !== "") {
        sort += "&end=" + end;
    }
    else if (maxDate <= new Date()) {
        sort += "&end=" + trem_data.dEnd;
    }

    return sort;
}

function sortuser() {
    var userid = $("#txtid").val();
    var sort = "&userid=" + userid;
    return sort;
}

function Reports02_2() {
    $("body").mLoading();
    var dt = new Date();
    var Header = $("#myHeader");
    var HtmlTable = $("#myTable");
    Header.html("");
    HtmlTable.html("");
    $rows = 1;
    var Colspan = 11;
    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan='" + Colspan + "'>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + `<tr><th colspan="` + Colspan + `" style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'>รายงานสรุปสถิติการมาโรงเรียน</th></tr> `
        + `<tr><th colspan="` + Colspan + `" style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'>ประจำวันที่ ` + ReportDate + `</th></tr>"`
        + `<tr><th colspan="` + Colspan + `" style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><th colspan="` + Colspan + `" style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style='font-size:20px;'><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<th colspan="` + (Colspan - 8) + `">`
        + `<th style="text-align: right;width:20%;border-width:0px;" colspan=2 >เทอม : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text() + `<th colspan="` + (Colspan - 4) + `">`
        + `<tr style='font-size:20px;'><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlsublevel option:selected').text() + `</th>`
        + `<th colspan="` + (Colspan - 8) + `">`
        + `<th style="text-align: right;width:20%; border-width:0px;" colspan=2>ชั้นเรียน : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlSubLV2 option:selected').text() + `</th>`
        + `<tr><th colspan="` + Colspan + `" style="border-width:0px;"><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder' rowspan='2'>ลำดับ"
        + "<td id='headder' rowspan='2'>ชั้นเรียน"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนทั้งหมด"
        + "<td id='headder' colspan='3'>มาเรียน"
        + "<td id='headder' colspan='3'>ไม่มาเรียน");
    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม");

    $.each(Reports_Data02, function (index) {
        var day = Reports_Data02[index].dayscan;
        var Objlevel = Reports_Data02[index].level;
        $.each(Objlevel, function (indexlevel) {
            var levelname = Objlevel[indexlevel].levelname;
            var Objlevel2 = Objlevel[indexlevel].level2
            var sumstudent4level = 0;
            var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

            var sum_m = 0, sum_f = 0, sum_m_0 = 0, sum_m_1 = 0, sum_f_0 = 0, sum_f_1 = 0;

            $.each(Objlevel2, function (indexlevel2, valuesLevel2) {
                var status_0 = 0, status_1 = 0, status_2 = 0;
                var _m = 0, _m_0 = 0, _m_1 = 0;
                var _f = 0, _f_0 = 0, _f_1 = 0;

                sum_m_0 += _m_0 = valuesLevel2.male_status_0 + valuesLevel2.male_status_1;
                sum_m_1 += _m_1 = valuesLevel2.male_status_2;
                sum_f_0 += _f_0 = valuesLevel2.female_status_0 + valuesLevel2.female_status_1;
                sum_f_1 += _f_1 = valuesLevel2.female_status_2;

                sum_m += _m = valuesLevel2.male_status_0 + valuesLevel2.male_status_1 + valuesLevel2.male_status_2;
                sum_f += _f = valuesLevel2.female_status_0 + valuesLevel2.female_status_1 + valuesLevel2.female_status_2;


                var HtmlRow = "";
                HtmlRow += "<tr><td>" + ($rows++)
                    + "<td class='center'>" + valuesLevel2.level2name
                    + "<td class='center'>" + _m + "<td class='center'>" + _f + "<td class='center'>" + (_m + _f)
                    + "<td class='center'>" + _m_0 + "<td class='center'>" + _f_0 + "<td class='center'>" + (_f_0 + _m_0)
                    + "<td class='center'>" + _m_1 + "<td class='center'>" + _f_1 + "<td class='center'>" + (_f_1 + _m_1)


                HtmlTable.append(HtmlRow);
            })

            HtmlTable.append("<tr><td class='right' colspan='2'>รวม"
                + "<td class='center'>" + sum_m
                + "<td class='center'>" + sum_f
                + "<td class='center'>" + (sum_f + sum_m)
                + "<td class='center'>" + sum_m_0
                + "<td class='center'>" + sum_f_0
                + "<td class='center'>" + (sum_f_0 + sum_m_0)
                + "<td class='center'>" + sum_m_1
                + "<td class='center'>" + sum_f_1
                + "<td class='center'>" + (sum_m_1 + sum_f_1));

            HtmlTable.append("<tr><td class='right' colspan='2'>คิดเป็นร้อยละ"
                + "<td class='center'>" + percent((sum_f + sum_m), sum_m)
                + "<td class='center'>" + percent((sum_f + sum_m), sum_f)
                + "<td class='center'>" + percent((sum_f + sum_m), (sum_m + sum_f))
                + "<td class='center'>" + percent((sum_f + sum_m), sum_m_0)
                + "<td class='center'>" + percent((sum_f + sum_m), sum_f_0)
                + "<td class='center'>" + percent((sum_f + sum_m), (sum_f_0 + sum_m_0))
                + "<td class='center'>" + percent((sum_f + sum_m), sum_m_1)
                + "<td class='center'>" + percent((sum_f + sum_m), sum_f_1)
                + "<td class='center'>" + percent((sum_f + sum_m), (sum_f_1 + sum_m_1)));
        });
    });
    $("body").mLoading('hide');
}

function Reports02_1() {
    var dt = new Date();
    var Header = $("#myHeader");
    var HtmlTable = $("#myTable");
    Header.html("");
    HtmlTable.html("");
    var Colspan = 14;
    Header.append("<tr><th style='text-align: center;font-size:26px;border-width:0px;'id='school' colspan='" + Colspan + "'>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + `<tr><th colspan="` + Colspan + `" style='text-align: center;font-size:24px;border-width:0px;'id='headdatail'>รายงานสรุปสถิติการมาโรงเรียน</th></tr> `
        + `<tr><th colspan="` + Colspan + `" style='text-align: center;font-size:24px;border-width:0px;'id='dayfall'>ประจำวันที่ ` + ReportDate + `</th></tr>"`
        + `<tr><th colspan="` + Colspan + `" style='text-align: right;font-size:20px;border-width:0px;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><th colspan="` + Colspan + `" style='text-align: right;font-size:20px;border-width:0px;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style='font-size:20px;'><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ปีการศึกษา : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<th colspan="` + (Colspan - 8) + `">`
        + `<th style="text-align: right;width:20%;border-width:0px;" colspan=2 >เทอม : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text() + `<th colspan="` + (Colspan - 4) + `">`
        + `<tr style='font-size:20px;'><th colspan=2 style="text-align: right;width:20%;border-width:0px;">ระดับชั้นเรียน : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlsublevel option:selected').text() + `</th>`
        + `<th colspan="` + (Colspan - 8) + `">`
        + `<th style="text-align: right;width:20%; border-width:0px;" colspan=2>ชั้นเรียน : </th><th colspan=2 style="width:30%;border-width:0px;">&nbsp;` + $('#ctl00_MainContent_ddlSubLV2 option:selected').text() + `</th>`
        + `<tr><th colspan="` + Colspan + `" style="border-width:0px;"><br></th>`);

    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder' rowspan='2'>ลำดับ"
        + "<td id='headder' rowspan='2'>ชั้นเรียน"
        + "<td id='headder' colspan='3'>จำนวนนักเรียนทั้งหมด"
        + "<td id='headder' colspan='3'>มาเรียน"
        + "<td id='headder' colspan='3'>สาย"
        + "<td id='headder' colspan='3'>ขาด");
    HtmlTable.append("<tr id='headder' style='font-weight: bold; font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม"
        + "<td id='headder'>ชาย<td id='headder'>หญิง<td id='headder'>รวม");
    $rows = 1;
    $.each(Reports_Data02, function (index) {
        var day = Reports_Data02[index].dayscan;
        var Objlevel = Reports_Data02[index].level;
        var _f_0 = 0, _f_1 = 0, _f_2 = 0;
        var _m_0 = 0, _m_1 = 0, _m_2 = 0;
        var sum_f = 0, sum_m = 0, _m = 0, _f = 0;
        var sum_f_0 = 0, sum_f_1 = 0, sum_f_2 = 0;
        var sum_m_0 = 0, sum_m_1 = 0, sum_m_2 = 0;

        $.each(Objlevel, function (indexlevel, valueslevel) {
            var levelname = valueslevel.levelname;
            var Objlevel2 = valueslevel.level2
            var sumstudent4level = 0;
            var sumstatus_04level = 0, sumstatus_14level = 0, sumstatus_24level = 0;

            $.each(Objlevel2, function (indexlevel2, valueslevel2) {

                sum_m_0 += _m_0 = valueslevel2.male_status_0;
                sum_m_1 += _m_1 = valueslevel2.male_status_1;
                sum_m_2 += _m_2 = valueslevel2.male_status_2;
                sum_f_0 += _f_0 = valueslevel2.female_status_0;
                sum_f_1 += _f_1 = valueslevel2.female_status_1;
                sum_f_2 += _f_2 = valueslevel2.female_status_2;

                sum_m += _m = valueslevel2.male_status_0 + valueslevel2.male_status_1 + valueslevel2.male_status_2;
                sum_f += _f = valueslevel2.female_status_0 + valueslevel2.female_status_1 + valueslevel2.female_status_2;
                var HtmlRow = "";
                HtmlRow += "<tr><td>" + ($rows++)
                    + "<td class='center'>" + Objlevel2[indexlevel2].level2name
                    + "<td class='center'>" + _m
                    + "<td class='center'>" + _f
                    + "<td class='center'>" + (_m + _f)
                    + "<td class='center'>" + _m_0 + "<td class='center'>" + _f_0 + "<td class='center'>" + (_f_0 + _m_0)
                    + "<td class='center'>" + _m_1 + "<td class='center'>" + _f_1 + "<td class='center'>" + (_f_1 + _m_1)
                    + "<td class='center'>" + _m_2 + "<td class='center'>" + _f_2 + "<td class='center'>" + (_f_2 + _m_2)

                HtmlTable.append(HtmlRow);
            })
            HtmlTable.append("<tr><td class='right' colspan='2'>รวม"
                + "<td class='center'>" + sum_m
                + "<td class='center'>" + sum_f
                + "<td class='center'>" + (sum_m + sum_f)
                + "<td class='center'>" + sum_m_0
                + "<td class='center'>" + sum_f_0
                + "<td class='center'>" + (sum_m_0 + sum_f_0)
                + "<td class='center'>" + sum_m_1
                + "<td class='center'>" + sum_f_1
                + "<td class='center'>" + (sum_m_1 + sum_f_1)
                + "<td class='center'>" + sum_m_2
                + "<td class='center'>" + sum_f_2
                + "<td class='center'>" + (sum_m_2 + sum_f_2));

            HtmlTable.append("<tr><td class='right' colspan='2'>คิดเป็นร้อยละ"
                + "<td class='center'>" + percent(sum_m + sum_f, sum_m)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_f)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_m + sum_f)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_m_0)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_f_0)
                + "<td class='center'>" + percent(sum_m + sum_f, (sum_m_0 + sum_f_0))
                + "<td class='center'>" + percent(sum_m + sum_f, sum_m_1)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_f_1)
                + "<td class='center'>" + percent(sum_m + sum_f, (sum_m_1 + sum_f_1))
                + "<td class='center'>" + percent(sum_m + sum_f, sum_m_2)
                + "<td class='center'>" + percent(sum_m + sum_f, sum_f_2)
                + "<td class='center'>" + percent(sum_m + sum_f, (sum_m_2 + sum_f_2)));
        });
    });
    $("body").mLoading('hide');
}
