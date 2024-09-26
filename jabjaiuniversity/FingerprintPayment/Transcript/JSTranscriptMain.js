var dataTestObjectArray = [];
var table;

$(document).ready(function () {
    ListddlYear();
    ListddlSubLV();

    $("#ddlSubLV").change(function () {
        ListddlClassroom();
    });
});

function ListddlYear() {
    $.ajax({
        url: "/App_Logic/ddl.ashx?mode=listYear",
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[id*=ddlYear]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nYear)
                        .text(msg[index].numberYear));
            });
        }
    });
}

function ListddlSubLV() {
    $.ajax({
        url: "/App_Logic/ddl.ashx?mode=listSubLV",
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[id*=ddlSubLV]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nSubLV)
                        .text(msg[index].shortNameSubLV));
            });
        }
    });
}

function ListddlClassroom() {

    var SubLVID = $('#ddlSubLV').val();

    $('#ddlClassroom').empty();

    $('select[id*=ddlClassroom]')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));

    $.ajax({
        url: "/App_Logic/ddl.ashx?mode=listClassroom&SubLVID=" + SubLVID,
        success: function (msg) {
            console.log(msg);
            $.each(msg, function (index) {
                $('select[id*=ddlClassroom]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLV)
                        .text(msg[index].nameClass));
            });
        }
    });
}

function ClickSearch() {

    var rowCount = $('#stdDetail >tbody >tr').length;
    if (rowCount !== 0) {
        table.clear();
        table.destroy();
    }

    callTabletest();
}

function GetDataSearch() {

    var nYear = $('#ddlYear').val();
    var nSubLV = $('#ddlSubLV').val();
    var nClassroom = $('#ddlClassroom').val();
    var std = $('#txtname').val();

    $.ajax({
        url: "/Transcript/LogicTranscriptMain.ashx?mode=Search&nYear="
            + nYear
            + "&nSubLV=" + nSubLV
            + "&nClassroom=" + nClassroom
            + "&sStd=" + std,
        dataType: "json",
        success: function (objjson) {

            $.each(objjson, function (index) {
                var newObject = {
                    noRow: objjson[index].noRow,
                    code: objjson[index].sStudentID,
                    fullName: objjson[index].fullName,
                    sID: objjson[index].sID,
                };


                dataTestObjectArray.push(newObject);

            });

            callTabletest();

        }
    });
}

function callTabletest() {


    var nYear = $('#ddlYear').val();
    var nSubLV = $('#ddlSubLV').val();
    var nClassroom = $('#ddlClassroom').val();
    var std = $('#txtname').val();
    table = $('#stdDetail').DataTable({

        // "serverSide": true,


        // processing: true,
        searching: false,
        lengthChange: false,
        info: false,
        paging: false,
        select: true,
        bSort: false,
        ajax: {
            url: "/Transcript/LogicTranscriptMain.ashx?mode=Search&nYear="
                + nYear
                + "&nSubLV=" + nSubLV
                + "&nClassroom=" + nClassroom
                + "&sStd=" + std,
            dataType: "json",
            dataSrc: ""
        },


        //"scrollY": "300px",
        //"scrollCollapse": true,
        //"ordering": true,
        //"select": true,
        // responsive: true,
        //data: dataTestObjectArray,
        //columns: [
        //    {
        //        title: "Name",
        //        render: function (data, type, full, meta) {

        //        return ("<button type='button' class='btn btn-info'><i class='fas fa-file-alt'></i></button>")

        "aoColumns": [
            {
                "title": "ลำดับ",
                "data": "noRow",
                "width": "10%",
                "className": 'dt-center'
            },
            {
                "title": "รหัสนักเรียน",
                "data": "sStudentID",
                "width": "20%",
            },
            {
                "title": "ชื่อ-สกุล",
                "data": "fullName",
                "width": "50%",
            },
            {
                "title": "",
                "data": "sID",
                "className": 'dt-center',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return ("<button type='button' class='btn btn-warning' onclick='ShowModalTranscriptNumber(" + data + ")'>ตั้งเลขที่เอกสาร <i class='fas fa-save'></i></button>")
                }
            },
            {
                "title": "",
                "data": "sID",
                "className": 'dt-center',
                orderable: false,
                "render": function (data, type, full, meta) {
                    return ("<button type='button' class='btn btn-warning' onclick='PrintThisStd(" + data + ")'>พิมพ์ใบ ปพ.1 <i class='fas fa-print'></i></button>")
                }
            }
        ]
    });
}


function PrintThisStd(id) {

    $('#ctl00_MainContent_stdId').val(id);
    $('#ctl00_MainContent_btnPrint').click();

}

var availableValuestudent = [];

$(document)
    .ajaxStart(function () {
        $('#loading').show();
    })
    .ajaxStop(function () {
        $('#loading').hide();
    });

$(function () {

    getlistemp();

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
            $("#txtId").val(ui.item.value);
            $("#txtCode").val(ui.item.code);
            $code = ui.item.code;
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("#txtId").val("");
            $("#txtCode").val("");

        }
    });

    $('#btnSaveTranscriptNumber').click(function () {

        var data = {
            "sid": $("#iptSetNumber").attr('data-sid'),
            "setNumber": $("#iptSetNumber").val(),
            "number": $("#iptNumber").val()
        }

        $("#modalWaitDialog").modal('show');

        $.ajax({
            async: false,
            type: "POST",
            url: "TranscriptMain.aspx/SaveTranscriptNumber",
            data: "{transcriptNumber:" + JSON.stringify(data) + "}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var title = "";
                var body = "";

                switch (response.d) {
                    case "success":
                        title = 'แจ้งข่าวสาร';
                        body = 'เปลี่ยนรหัสผ่านรายการนี้สำเร็จ';

                        $('#modalTranscriptNumber').modal('hide');

                        break;
                    case "error":
                        title = 'ข่าวสารข้อผิดพลาด';
                        body = 'เปลี่ยนรหัสผ่านรายการนี้ไม่สำเร็จ';

                        break;
                }

                $("#modalWaitDialog").modal('hide');

                $("#modalNotifyOnlyClose").find('.modal-title').text(title);
                $("#modalNotifyOnlyClose").find('.modal-body p').text(body);
                $("#modalNotifyOnlyClose").modal('show');
            },
            failure: function (response) {
                alert('การตอบสนองล้มเหลว!');

                $("#modalWaitDialog").modal('hide');
            },
            error: function (response) {
                alert('การตอบสนองผิดพลาด!');

                $("#modalWaitDialog").modal('hide');
            }
        });

        return false;
    });
});


function getlistemp() {
    availableValuestudent = [];
    $.ajax({
        url: "/App_Logic/autoCompleteName.ashx?mode=GetStdList",
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].Name,
                    value: objjson[index].StdId,
                    code: objjson[index].Code
                };
                availableValuestudent[index] = newObject;
            });
        }
    });
}

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

function ShowModalTranscriptNumber(sid) {
    $.ajax({
        async: false,
        type: "POST",
        url: "TranscriptMain.aspx/LoadTranscriptNumber",
        data: '{sid: ' + sid + '}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d != "error") {
                $("#iptSetNumber").val(response.d.setNumber).attr('data-sid', sid);
                $("#iptNumber").val(response.d.number);

                $('#modalTranscriptNumber').modal('show');
            }
        },
        failure: function (response) {
            alert('การตอบสนองล้มเหลว!');
        },
        error: function (response) {
            alert('การตอบสนองผิดพลาด!');
        }
    });
}

