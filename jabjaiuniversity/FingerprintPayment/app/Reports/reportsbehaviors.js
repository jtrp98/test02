var availableValuestudent = [];

$(function () {
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });
    document.getElementById('txtname').addEventListener('input', function () {
        if (this.value === '') {
            $("#txtid").val("");
        }
    }, false);

    $('#ctl00_MainContent_ddlsublevel').change(function () {
        $('input[id*=txtSubLV2ID]').val("");
        getListSubLV2();
        getliststudent();
    });

    getListSubLV2();

    $('#ctl00_MainContent_ddlSubLV2').change(function () {
        getliststudent();
    });

    $('select[id*=sort_type]').change(function () {
        switch_sort($(this).val());
    });
    switch_sort($('select[id*=sort_type]').val());

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
            if (hasMatch(obj.label) || hasMatch(obj.code)) {
                matches.push(obj);
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

function getListSubLV2() {
    //                alert($('#ctl00_MainContent_ddlSubLV option:selected').val())
    var SubLVID = $('#ctl00_MainContent_ddlsublevel option:selected').val();
    var sSubLV = $('#ctl00_MainContent_ddlsublevel option:selected').text();
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
                    value: objjson[index].sID,
                    code: objjson[index].studentid,
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

function Reports01() {
    var dStart = "", dEnd = "";
    if ($('select[id*=sort_type]').val() === "1") {
        dStart = $("#select_month").val() + "/1/" + ($("select[id*=ddlyear] option:selected").text() - 543)
    }
    else if ($('select[id*=sort_type]').val() === "2") {
        var i = 1;
    } else {
        if ($("#txtstart").val() !== "") {
            dStart = $("#txtstart").val().split('/')[1] + "/" + $("#txtstart").val().split('/')[0] + "/" + $("#txtstart").val().split('/')[2];
        }
        else {
            alert("กรุณาเลือกวันที่รายงาน");
        }
        if ($("#txtend").val() !== "") {
            dEnd = $("#txtend").val().split('/')[1] + "/" + $("#txtend").val().split('/')[0] + "/" + $("#txtend").val().split('/')[2];
        }
    }
    Search = {
        "term_id": $('select[id*=semister]').val(), "level2_id": $('select[id*=ddlSubLV2]').val(),
        "sort_type": $('select[id*=sort_type]').val(), "dStart": dStart, "dEnd": dEnd,
        "student_id": $("#txtid").val()

    };

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

    Header.append("<tr><td style='text-align: center;font-size:26px;border-width:0px;font-weight: bold;'id='school' colspan=10>" + $("input[id*=hdfschoolname]").val() + "</th></tr>"
        + "<tr><td colspan=10 style='text-align: center;border-width:0px;font-size:24px;font-weight: bold;'>รายงานคะแนนพฤติกรรม</td></tr>"
        + `<tr><td colspan=10 style='text-align: right;font-size:20px;border-width:0px;font-weight: bold;'id='dayshort'>พิมพ์วันที่ :&nbsp;` + dt.toLocaleDateString()
        + `<tr><td colspan=10 style='text-align: right;font-size:20px;border-width:0px;font-weight: bold;'id='timetoday'>เวลา :&nbsp;` + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds()
        + `<tr style='font-size:20px;font-weight: bold;'><td colspan=4 style='text-align: right;width:20%;border-width:0px;'>ปีการศึกษา : </td><td style="width:30%;border-width:0px;" colspan=1>&nbsp;` + $("select[id*=ddlyear] option:selected").text()
        + `<td style='text-align: right;width:20%;border-width:0px;'>เทอม : </td><td colspan=3 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=semister] option:selected").text()
        + `<tr style='font-size:20px;font-weight: bold;'><td colspan=4 style='text-align: right;width:20%;border-width:0px;'>ระดับชั้นเรียน : </td><td style="width:30%;border-width:0px;" colspan=1>&nbsp;` + $("select[id*=ddlsublevel] option:selected").text()
        + `<td style='text-align: right;width:20%; border-width:0px;'>ชั้นเรียน : </td><td colspan=3 style="width:30%;border-width:0px;">&nbsp;` + $("select[id*=ddlSubLV2] option:selected").text()
        + `<tr ><td  colspan=10 ><br></td>`);

    HtmlTable.append("<tr id='headder' style='font-size:18px;text-align: center; background-color: rgb(51, 122, 183); color: rgb(255, 255, 255);'>"
        + "<td id='headder' style='width:100px;font-weight: bold;' >ลำดับ"
        + "<td id='headder' style='width:100px;font-weight: bold;' >วันที่"
        + "<td id='headder' style='width:100px;font-weight: bold;' >ชื่อ"
        + "<td id='headder' style='width:100px;font-weight: bold;' >นามสกุล"
        + "<td id='headder' style='width: 100px;font-weight: bold;'>ชื่อความประพฤติ"
        + "<td id='headder' style='font-weight: bold;'>ประเภท"
        + "<td id='headder' style='font-weight: bold;'>คะแนน"
        + "<td id='headder' style='font-weight: bold;'>คะแนนคงเหลือ"
        + "<td id='headder' style='font-weight: bold;'>ชื่อ - นามสกุลผู้บันทึก"
        + "<td id='headder' style='width:15%;font-weight: bold;' >หมายเหตุ"
        + "<td id='headder' style='font-weight: bold;'>"
    );

    PageMethods.returnlist(Search, function (Obj) {
        $.each(Obj, function (index) {

            HtmlTable.append("<tr><td  class='center' >" + (index + 1)
                + "<td  class='center' >" + Obj[index].day
                + "<td  class='center' >" + Obj[index].studentname
                + "<td  class='center' >" + Obj[index].studentlastname
                + "<td style=\"word-break: break-word;\" >" + Obj[index].behaviorname
                + "<td  class='center' >" + Obj[index].behaviortype
                + "<td  class='center' >" + Obj[index].score
                + "<td  class='center' >" + Obj[index].residualscore
                + "<td  >" + Obj[index].teachername + " " + Obj[index].teacherlastname
                + "<td  >" + (Obj[index].note === null ? "" : Obj[index].note)
                + "<td  class='center' >"
                + (Obj[index].status === "delete" ? "" : "<div class='btn btn-danger' onclick=\"CancelScore(" + Obj[index].BehaviorHistoryId + ");\">ยกเลิกรายการ</div>")
            );
        });
        $("body").mLoading('hide');

    })
}

function sortlevel() {
    var level = $("select[id*=ddlsublevel] option:selected").val() === null ? "" : $("select[id*=ddlsublevel] option:selected").val();
    var level2 = $("select[id*=ddlSubLV2] option:selected").val() === null ? "" : $("select[id*=ddlSubLV2] option:selected").val();
    var sort = "&level2=" + level2 + "&level=" + level;
    return sort;
}

function sortday() {
    var start = $("#txtstart").val();
    var end = $("#txtend").val();
    var sort = "";
    if (start !== "") sort += "&day=" + start
    if (end !== "") sort += "&end=" + end;
    return sort;
}

function sortuser() {
    var userid = $("#txtid").val();
    var sort = "&studentid=" + userid;
    return sort;
}


function CancelScore(BehaviorsrId) {
    $.confirm({
        title: 'ยืนยันการลบข้อมูล',
        content: 'ท่านต้องลบรายการคะแนนพฤติกรรมนี้หรือไม่',
        buttons: {
            'ตกลง': {
                text: 'ตกลง',
                btnClass: 'btn-danger',
                keys: ['enter'],
                action: function () {
                    PageMethods.CancelScore(BehaviorsrId, function (response) {
                        Reports01();
                    });
                }
            },
            'ยกเลิก': function () {
                //$.alert('Canceled!');
            }
        }
    });
}