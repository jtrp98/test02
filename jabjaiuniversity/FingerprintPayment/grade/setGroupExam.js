
var tbGroupDetail;


$(function () {

    $("select[name*='ddlListGroup']").change(function (e) {
        //alert(this.id);
        var numElement = this.id.split("_");
        //  alert(numElement[1]);
        var length = $("select[name*='ddlListGroup']").length;
        //  console.log('length =' + length+1);

        for (var i = 1; i <= length; i++) {
            if (i != numElement[1]) {
                CheckedSelcetValues($("#" + this.id).val(), "select_" + i);
            }
        }
        add($("#" + this.id).val(), $("#" + this.id + " option:selected").text(), this.id, "add_" + numElement[1]);
    });

    $("select[name*='ddlSelectedGroup']").change(function (e) {

        var numElement = this.id.split("_");
        //  alert(numElement[1]);
        var length = $("select[name*='ddlSelectedGroup']").length;
        //  console.log('length =' + length+1);

        for (var i = 1; i <= length; i++) {
            if (i != numElement[1]) RollbackValues($("#" + this.id).val(), "select_" + i, this.id);
        }
        add($("#" + this.id).val(), $("#" + this.id + " option:selected").text(), this.id, "select_" + numElement[1]);
    });


    //this prototype,it's work!
    //$("#select_1").change(function (e) {
    //    for (var i = 1; i < 4; i++) {
    //        if(i!=1) CheckedSelcetValues($("#select_1").val(), "select_" + i);
    //    }
    //    add($("#select_1").val(), "select_1", "add_1");
    //});
    //$("#add_1").change(function (e) {
    //    for (var i = 1; i < 4; i++) {
    //       if(i!=1) RollbackValues($("#add_1").val(), "select_"+i, "add_1");
    //    }
    //    add($("#add_1").val(), "add_1", "select_1");
    //});

    $('#groupDetail tbody ').on('click', 'tr', function () {
        var data = tbGroupDetail.row(this).data();

        $('#txtGroupName_1').val(data.GroupExamName);
        $('#modalGroupname').html(data.GroupExamName);
    });

});

//$(document)
//    .ajaxStart(function () {
//        $('#loading').show();
//    })
//    .ajaxStop(function () {
//        $('#loading').hide();
//    });

function CheckedSelcetValues(v1, selectId) {
    var tmp = [];
    $.each(v1, function (e1, s1) {
        var checked = false;
        $.each($("#" + selectId + " option"), function (e2, s2) {
            //console.log(s1 === $(s2).val());
            if (s1 == $(s2).val()) checked = true;
        });
        $("#" + selectId + " option[value=" + s1 + "]").attr("disabled", checked);
        $("#" + selectId + " option[value=" + s1 + "]").css('background-color', 'WhiteSmoke');
    });
}

function add(v1, v1text, remove, whereAdd) {
    var tmp = [];
    $('#' + whereAdd + '').append($("<option></option>")
        .attr("value", v1)
        .text(v1text));

    //console.log(v1);

    $("#" + remove + " option:selected").remove();

}

function RollbackValues(v1, selectId, remove) {
    var tmp = [];
    $.each(v1, function (e1, s1) {
        var checked = false;
        $.each($("#" + selectId + " option"), function (e2, s2) {
            //console.log(s1 === $(s2).val());
            if (s1 == $(s2).val()) checked = false;
        });
        $("#" + selectId + " option[value=" + s1 + "]").attr("disabled", checked);
        $("#" + selectId + " option[value=" + s1 + "]").css('background-color', '');
    });

}

function scriptGenCollapseGroup(num) {
    document.write('<div class="panel-group">');
    document.write('<div class="panel panel-default">');
    document.write('<div class="panel-heading">');
    document.write('<h4 class="panel-title">');
    document.write('<a data-toggle="collapse" href="#collapse' + num + '" style="font-size: 24px;">การจัดการกลุ่ม</a> <asp: Label ID="headingGroupName_' + num + '" cssclass="" runat="server" ></asp: Label>');
    document.write('</h4>');
    document.write('</div>');
    document.write('<div id="collapse' + num + '" class="panel-collapse collapse">');
    document.write('<div class="panel-body">');
    document.write('<div class="row">');
    document.write('<div class="col-xs-12">');
    document.write('<div class="row my8">');
    document.write('<div class="col-xs-4 text-right">');
    document.write('ชื่อกลุ่ม');
    document.write('</div>');
    document.write('<div class="col-xs-6">');
    document.write('<input name="groupName" id="txtGroupName_' + num + '" class="form-control" />');
    document.write('</div>');
    document.write('</div>');
    document.write('<div class="row my8 px0">');
    document.write('<div class="col-xs-5 col-xs-offset-1 px0">');
    document.write('<select name="ddlListGroup" id="select_' + num + '" class="form-control" multiple="multiple" style="width: 250px;margin:auto; height: 500px;">');
    //document.write('<option value="AAAAAAAAA">AAAAAAAAA</option>');
    //document.write('<option value="BBBBBBBBB">BBBBBBBBB</option>');
    //document.write('<option value="CCCCCCCCC">CCCCCCCCC</option>');
    //document.write('<option value="DDDDDDDDD">DDDDDDDDD</option>');
    //document.write('<option value="EEEEEEEEE">EEEEEEEEE</option>');
    //document.write('<option value="FFFFFFFFF">FFFFFFFFF</option>');
    document.write('</select>');
    document.write('</div>');
    document.write('<div class="col-xs-1 px0">');
    document.write('<br />');
    document.write('<br />');
    document.write('<label>เพิ่ม</label>');
    document.write('<br />');
    document.write('<label>=></label>');
    document.write('</div>');
    document.write('<div class="col-xs-4 px0">');
    document.write('<select name="ddlSelectedGroup" id="add_' + num + '" class="form-control" multiple="multiple" style="width: 250px;margin:auto; height: 500px;">');
    document.write('</select>');
    document.write('</div>');
    document.write('</div>');

    document.write('<label id=validateGroupValue style="color:red"></label>');
    document.write(' <div class="col-xs-offset-10 col-xs-2">');
    document.write('  <button type="button" class="btn btn-success" id="btnGroupCreate" onclick="clickSaveGroup()">สร้างกลุ่ม</button>');
    document.write(' </div>');
    document.write('  <div class="col-xs-offset-9 col-xs-3 right" style="padding-right:0px">');
    document.write('     <button type="button" class="btn btn-success" id="btnGroupEdit" onclick="clickSaveEditGroup()" style="display: none;">บันทึกการแก้ไข</button>');
    document.write('  </div>');
    document.write('</div>');
    document.write('</div>');
    document.write('</div>');
    document.write('</div>');
    document.write('</div>');
    document.write('</div>');
}

function GetddlListGroup() {

    var plantId = $('#ctl00_MainContent_planidtxt').val();
    var nterm = $('#ctl00_MainContent_termtxt').val();
    var gradeId = $('#ctl00_MainContent_gradeidtxt').val();
    $.ajax({
        url: "/grade/LogicGradeGroup.ashx?mode=GetddlListGroup&plantId=" + plantId
            + "&nterm=" + nterm
            + "&gradeid=" + gradeId,
        beforeSend: function () {
            $('#loading').show();
        },
        success: function (msg) {
            //console.log(msg);

            $.each(msg, function (index) {
                $("select[name*='ddlListGroup']")

                    .append($("<option></option>")
                        .attr("value", msg[index].colName)
                        .text(msg[index].colNameVal));
            });
            $('#loading').hide();

        }
    });
}

function clickSaveGroup() {

    //$('#select_1').val('nameGrade1').change();
    //$('#select_1').val('nameGrade2').change();
    var nterm = $('#ctl00_MainContent_termtxt').val();
    var gradeId = $('#ctl00_MainContent_gradeidtxt').val();
    var groupName = $('#txtGroupName_1').val();

    var colSelect = $('#add_1').children('option').map(function (i, e) {
        return e.value;
    }).get();

    //console.log(colSelect);


    if (colSelect.length >= 1 && groupName.trim().length >= 1) {

        $.ajax({
            url: "/grade/LogicGradeGroup.ashx?mode=CreateGroup&nterm=" + nterm
                + "&gradeid=" + gradeId
                + "&groupName=" + groupName
                + "&colSelect=" + colSelect,
            beforeSend: function () {
                $('#loading').show();
            },
            success: function (msg) {
                console.log('create success');
                $('#txtGroupName_1').val('');
                $('#add_1').empty();
                ShowGroup();
                $('#validateGroupValue').html('');
                $('#loading').hide();
            }
        });

    } else {
        $('#validateGroupValue').html('กรุณาระบุชื่อกลุ่ม / ช่องคะแนน ให้ครบถ้วน');

    }



}

function clickSaveEditGroup() {


    // alert('clickSaveEdit');


    var groupName = $('#txtGroupName_1').val();
    var groupId = $('#hiddenGroupId').val();
    var gradeId = $('#ctl00_MainContent_gradeidtxt').val();

    var colSelect = $('#add_1').children('option').map(function (i, e) {
        return e.value;
    }).get();

    //console.log(colSelect);

    if (colSelect.length >= 1 && groupName.trim().length >= 1) {

        $.ajax({
            url: "/grade/LogicGradeGroup.ashx?mode=EditGroup&groupId=" + groupId
                + "&groupName=" + groupName
                + "&gradeid=" + gradeId
                + "&colSelect=" + colSelect,
            beforeSend: function () {
                $('#loading').show();
            },
            success: function (msg) {
                console.log('edit success');
                $('#txtGroupName_1').val('');
                $('#add_1').empty();
                ShowGroup();
                $('#hiddenGroupId').val('');
                $('#btnGroupEdit').hide();
                $('#btnGroupCreate').show();
                $('#validateGroupValue').html('');
                $('#loading').hide();
            }
        });
    }
    else {
        $('#validateGroupValue').html('กรุณาระบุชื่อกลุ่ม / ช่องคะแนน ให้ครบถ้วน');
    }
}

function ShowGroup() {
    var rowCount = $('#groupDetail >tbody >tr').length;
    //console.log(rowCount);
    if (rowCount !== 0) {
        tbGroupDetail.clear();
        tbGroupDetail.destroy();
    }

    CallTableGroup();
}

function CallTableGroup() {

    //console.log(dataObjectArray);

    var plantId = $('#ctl00_MainContent_planidtxt').val();
    var nterm = $('#ctl00_MainContent_termtxt').val();
    var gradeId = $('#ctl00_MainContent_gradeidtxt').val();

    if (plantId.length > 0 && nterm.length > 0 && gradeId > 0) {
        tbGroupDetail = $('#groupDetail').DataTable({
            // "serverSide": true,
            // processing: true,
            searching: false,
            lengthChange: false,
            info: false,
            paging: false,
            //select: true,
            bSort: false,
            //"scrollY": "300px",
            //"scrollCollapse": true,
            //"ordering": true,
            //"select": true,
            // responsive: true,
            'rowsGroup': [1, 3],
            ajax: {
                url: "/grade/LogicGradeGroup.ashx?mode=ShowGroup&plantId=" + plantId
                    + "&nterm=" + nterm
                    + "&gradeid=" + gradeId,
                dataType: "json",
                dataSrc: ""
            },

            "aoColumns": [
                {
                    "title": "id",
                    "data": "GroupExamID",
                    "width": "10%",
                    "className": 'dt-center',
                    "visible": false,
                },
                {
                    "title": "ชื่อกลุ่ม",
                    "data": "GroupExamName",
                    "width": "30%",
                    "className": 'dt-center'
                },
                {
                    "title": "ช่องคะแนนที่เกี่ยวข้อง",
                    "data": "GradeColumnName",
                    "width": "60%",
                    "className": 'dt-center'
                },
                {
                    "title": "",
                    "data": "GroupExamID",
                    "className": 'dt-center',
                    orderable: false,
                    "render": function (data, type, full, meta) {
                        return ("<button type='button' class='btn btn-warning' name='btnEditOnRow' onclick='EditGroup(" + data + ")'><i class='fas fa-edit'></i></button> <button type ='button' class='btn btn-danger' data-toggle='modal' data-target='#moadlCheckDelete' data-backdrop='static' data-keyboard='false' onclick='openDialog(" + data + ")'><i class='fas fa-trash-alt'></i></button>");
                        //  <button type='button' class='btn btn-danger' onclick='DeleteGroup(" + data + ")'><i class='fas fa-trash-alt'></i></button>");

                        // return ("<button type='button' class='btn btn-warning' name='btnEditOnRow' onclick='EditGroup(\"" + data.GroupExamName + '\",' + data.GroupExamID + ")'><i class='fas fa-edit'></i></button> <button type='button' class='btn btn-danger' onclick='DeleteGroup(" + data.GroupExamID + ")'><i class='fas fa-trash-alt'></i></button>");

                    }
                }
            ]
        });
    }
    else {
        document.getElementById("btnOpenGroupSidebar").disabled = true;
        console.log('plantId =' + plantId + "\n"
            + 'nterm =' + nterm + "\n"
            + 'gradeId =' + gradeId
        );
    }
}

function openDialog(id) {
    $('#hiddenGroupId').val(id);
    //$('#txtGroupName_1').val('');
    //$('#select_1').empty();
    //$('#add_1').empty();
    //GetddlListGroup();
    EditGroup(id);


}

function ClickDeleteOK() {
    DeleteGroup();
}

function ClickDeleteCancle() {
    $('#txtGroupName_1').val('');
    $('#select_1').empty();
    $('#add_1').empty();
    GetddlListGroup();
    $('#btnGroupEdit').hide();
    $('#btnGroupCreate').show();
}

function DeleteGroup() {
    //alert(id);
    var id = $('#hiddenGroupId').val();

    $('#txtGroupName_1').val('');

    $.ajax({
        url: "/grade/LogicGradeGroup.ashx?mode=DeleteGroup&groupExamID=" + id,
        beforeSend: function () {
            $('#loading').show();
        },
        success: function (msg) {
            //console.log(msg);
            ShowGroup();
            $('#txtGroupName_1').val('');
            $('#select_1').empty();
            $('#add_1').empty();
            GetddlListGroup();
            $('#btnGroupEdit').hide();
            $('#btnGroupCreate').show();
            $('#loading').hide();
        }
    });

}

function EditGroup(id) {
    //alert(id);

    $('#hiddenGroupId').val(id);
    $('#btnGroupEdit').show();
    $('#btnGroupCreate').hide();

    var gradeId = $('#ctl00_MainContent_gradeidtxt').val();
    $('#select_1').empty();
    GetddlListGroup();
    $.ajax({
        url: "/grade/LogicGradeGroup.ashx?mode=GetSelectedList&groupExamID=" + id
            + "&gradeid=" + gradeId,
        beforeSend: function () {
            $('#loading').show();
        },
        success: function (msg) {

            //console.log(msg);

            $('#add_1').empty();
            $.each(msg, function (index) {
                $("select[name*='ddlSelectedGroup']")

                    .append($("<option></option>")
                        .attr("value", msg[index].colName)
                        .text(msg[index].colNameVal));
            });
            $('#loading').hide();
        }

    });

}

function hasClass(element, cls) {
    return (' ' + element.className + ' ').indexOf(' ' + cls + ' ') > -1;
}

function autosave(target, score, sid) {
    var autosavedata = document.getElementsByClassName("autosavedata");
    var autotext = document.getElementsByClassName("autotext");
    var gradeid = autosavedata[0].value;
    var idlv2 = autosavedata[1].value;
    var planid = autosavedata[2].value;
    var nterm = autosavedata[3].value;
    var nYear = autosavedata[4].value;
    var idlvn = autosavedata[5].value;
    var target1 = target.split('_');

    //alert("/App_Logic/gradeAutosave.ashx?gradeid=" + gradeid + "&target=" + target1[4] + "&sid=" + sid + "&score=" + score);
    if (gradeid != "") {
        $.ajax({
            url: "/App_Logic/gradeAutosave.ashx?gradeid=" + gradeid + "&target=" + target1[4] + "&sid=" + sid + "&score=" + score + "&idlv2=" + idlv2 + "&planid=" + planid + "&nterm=" + nterm + "&nYear=" + nYear + "&idlvn=" + idlvn,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
        });
        var d = new Date();
        var n = d.getHours();
        var n2 = d.getMinutes();
        n2 = (d.getMinutes() < 10 ? '0' : '') + d.getMinutes();
        n = (d.getHours() < 10 ? '0' : '') + d.getHours();

        var target2 = "";

        switch (target1[4]) {
            case "txtGrade1":
                target = "คะแนนเก็บ 1";
                break;
            case "txtGrade2":
                target = "คะแนนเก็บ 2";
                break;
            case "txtGrade3":
                target = "คะแนนเก็บ 3";
                break;
            case "txtGrade4":
                target = "คะแนนเก็บ 4";
                break;
            case "txtGrade5":
                target = "คะแนนเก็บ 5";
                break;
            case "txtGrade6":
                target = "คะแนนเก็บ 6";
                break;
            case "txtGrade7":
                target = "คะแนนเก็บ 7";
                break;
            case "txtGrade8":
                target = "คะแนนเก็บ 8";
                break;
            case "txtGrade9":
                target = "คะแนนเก็บ 9";
                break;
            case "txtGrade10":
                target = "คะแนนเก็บ 10";
                break;
            case "txtGrade11":
                target = "คะแนนเก็บ 11";
                break;
            case "txtGrade12":
                target = "คะแนนเก็บ 12";
                break;
            case "txtGrade13":
                target = "คะแนนเก็บ 13";
                break;
            case "txtGrade14":
                target = "คะแนนเก็บ 14";
                break;
            case "txtGrade15":
                target = "คะแนนเก็บ 15";
                break;
            case "txtGrade16":
                target = "คะแนนเก็บ 16";
                break;
            case "txtGrade17":
                target = "คะแนนเก็บ 17";
                break;
            case "txtGrade18":
                target = "คะแนนเก็บ 18";
                break;
            case "txtGrade19":
                target = "คะแนนเก็บ 19";
                break;
            case "txtGrade20":
                target = "คะแนนเก็บ 20";
                break;
            case "chewat1":
                target = "คะแนนเก็บ 21";
                break;
            case "chewat2":
                target = "คะแนนเก็บ 22";
                break;
            case "chewat3":
                target = "คะแนนเก็บ 23";
                break;
            case "chewat4":
                target = "คะแนนเก็บ 24";
                break;
            case "chewat5":
                target = "คะแนนเก็บ 25";
                break;
            case "chewat6":
                target = "คะแนนเก็บ 26";
                break;
            case "chewat7":
                target = "คะแนนเก็บ 27";
                break;
            case "chewat8":
                target = "คะแนนเก็บ 28";
                break;
            case "chewat9":
                target = "คะแนนเก็บ 29";
                break;
            case "chewat10":
                target = "คะแนนเก็บ 30";
                break;
            case "chewat11":
                target = "คะแนนเก็บ 31";
                break;
            case "chewat12":
                target = "คะแนนเก็บ 32";
                break;
            case "chewat13":
                target = "คะแนนเก็บ 33";
                break;
            case "chewat14":
                target = "คะแนนเก็บ 34";
                break;
            case "chewat15":
                target = "คะแนนเก็บ 35";
                break;
            case "chewat16":
                target = "คะแนนเก็บ 36";
                break;
            case "chewat17":
                target = "คะแนนเก็บ 37";
                break;
            case "chewat18":
                target = "คะแนนเก็บ 38";
                break;
            case "chewat19":
                target = "คะแนนเก็บ 39";
                break;
            case "chewat20":
                target = "คะแนนเก็บ 40";
                break;
            case "midscore1":
                target = "คะแนนกลางภาค 1";
                break;
            case "midscore2":
                target = "คะแนนกลางภาค 2";
                break;
            case "midscore3":
                target = "คะแนนกลางภาค 3";
                break;
            case "midscore4":
                target = "คะแนนกลางภาค 4";
                break;
            case "midscore5":
                target = "คะแนนกลางภาค 5";
                break;
            case "midscore6":
                target = "คะแนนกลางภาค 6";
                break;
            case "midscore7":
                target = "คะแนนกลางภาค 7";
                break;
            case "midscore8":
                target = "คะแนนกลางภาค 8";
                break;
            case "midscore9":
                target = "คะแนนกลางภาค 9";
                break;
            case "midscore10":
                target = "คะแนนกลางภาค 10";
                break;
            case "finalscore1":
                target = "คะแนนปลายภาค 1";
                break;
            case "finalscore2":
                target = "คะแนนปลายภาค 2";
                break;
            case "finalscore3":
                target = "คะแนนปลายภาค 3";
                break;
            case "finalscore4":
                target = "คะแนนปลายภาค 4";
                break;
            case "finalscore5":
                target = "คะแนนปลายภาค 5";
                break;
            case "finalscore6":
                target = "คะแนนปลายภาค 6";
                break;
            case "finalscore7":
                target = "คะแนนปลายภาค 7";
                break;
            case "finalscore8":
                target = "คะแนนปลายภาค 8";
                break;
            case "finalscore9":
                target = "คะแนนปลายภาค 9";
                break;
            case "finalscore10":
                target = "คะแนนปลายภาค 10";
                break;
            case "behave1":
                target = "คะแนนคุณลักษณะฯ 1";
                break;
            case "behave2":
                target = "คะแนนคุณลักษณะฯ 2";
                break;
            case "behave3":
                target = "คะแนนคุณลักษณะฯ 3";
                break;
            case "behave4":
                target = "คะแนนคุณลักษณะฯ 4";
                break;
            case "behave5":
                target = "คะแนนคุณลักษณะฯ 5";
                break;
            case "behave6":
                target = "คะแนนคุณลักษณะฯ 6";
                break;
            case "behave7":
                target = "คะแนนคุณลักษณะฯ 7";
                break;
            case "behave8":
                target = "คะแนนคุณลักษณะฯ 8";
                break;
            case "behave9":
                target = "คะแนนคุณลักษณะฯ 9";
                break;
            case "behave10":
                target = "คะแนนคุณลักษณะฯ 10";
                break;
            case "txtMidScore":
                target = "คะแนนกลางภาค";
                break;
            case "txtLateScore":
                target = "คะแนนปลายเทอม";
                break;
            case "txtGoodBehavior":
                target = "คะแนนคุณลักษณะฯ";
                break;
            case "txtGoodReading":
                target = "คะแนนอ่าน คิด วิเคราะห์";
                break;
            case "txtSamattana":
                target = "คะแนนสมรรถนะ";
                break;
        }

        autotext[0].value = "บันทึกอัตโนมัติช่อง" + target + " เสร็จสิ้น... เวลา " + n + ":" + n2;
    }
}

var ExcelToJSON = function () {
    var maxtest = document.getElementsByClassName("maxtest");
    var maxtestcw = document.getElementsByClassName("maxtestcw");
    var maxtestb1 = document.getElementsByClassName("maxtestb1");
    var maxmidcw = document.getElementsByClassName("maxmidcw");
    var maxfinalcw = document.getElementsByClassName("maxfinalcw");
    var setnameb1 = document.getElementsByClassName("setnameb1");
    var setnameb2 = document.getElementsByClassName("setnameb2");
    var setnameb3 = document.getElementsByClassName("setnameb3");
    var setnameb4 = document.getElementsByClassName("setnameb4");
    var setnameb5 = document.getElementsByClassName("setnameb5");
    var setnameb6 = document.getElementsByClassName("setnameb6");
    var setnameb7 = document.getElementsByClassName("setnameb7");
    var setnameb8 = document.getElementsByClassName("setnameb8");
    var setnameb9 = document.getElementsByClassName("setnameb9");
    var setnameb10 = document.getElementsByClassName("setnameb10");
    var set1name = document.getElementsByClassName("set1name");
    var set2name = document.getElementsByClassName("set2name");
    var set3name = document.getElementsByClassName("set3name");
    var set4name = document.getElementsByClassName("set4name");
    var set5name = document.getElementsByClassName("set5name");
    var set6name = document.getElementsByClassName("set6name");
    var set7name = document.getElementsByClassName("set7name");
    var set8name = document.getElementsByClassName("set8name");
    var set9name = document.getElementsByClassName("set9name");
    var set10name = document.getElementsByClassName("set10name");
    var set11name = document.getElementsByClassName("set11name");
    var set12name = document.getElementsByClassName("set12name");
    var set13name = document.getElementsByClassName("set13name");
    var set14name = document.getElementsByClassName("set14name");
    var set15name = document.getElementsByClassName("set15name");
    var set16name = document.getElementsByClassName("set16name");
    var set17name = document.getElementsByClassName("set17name");
    var set18name = document.getElementsByClassName("set18name");
    var set19name = document.getElementsByClassName("set19name");
    var set20name = document.getElementsByClassName("set20name");
    var set1namecw = document.getElementsByClassName("set1namecw");
    var set2namecw = document.getElementsByClassName("set2namecw");
    var set3namecw = document.getElementsByClassName("set3namecw");
    var set4namecw = document.getElementsByClassName("set4namecw");
    var set5namecw = document.getElementsByClassName("set5namecw");
    var set6namecw = document.getElementsByClassName("set6namecw");
    var set7namecw = document.getElementsByClassName("set7namecw");
    var set8namecw = document.getElementsByClassName("set8namecw");
    var set9namecw = document.getElementsByClassName("set9namecw");
    var set10namecw = document.getElementsByClassName("set10namecw");
    var set11namecw = document.getElementsByClassName("set11namecw");
    var set12namecw = document.getElementsByClassName("set12namecw");
    var set13namecw = document.getElementsByClassName("set13namecw");
    var set14namecw = document.getElementsByClassName("set14namecw");
    var set15namecw = document.getElementsByClassName("set15namecw");
    var set16namecw = document.getElementsByClassName("set16namecw");
    var set17namecw = document.getElementsByClassName("set17namecw");
    var set18namecw = document.getElementsByClassName("set18namecw");
    var set19namecw = document.getElementsByClassName("set19namecw");
    var set20namecw = document.getElementsByClassName("set20namecw");
    var set1midcw = document.getElementsByClassName("set1midcw");
    var set2midcw = document.getElementsByClassName("set2midcw");
    var set3midcw = document.getElementsByClassName("set3midcw");
    var set4midcw = document.getElementsByClassName("set4midcw");
    var set5midcw = document.getElementsByClassName("set5midcw");
    var set6midcw = document.getElementsByClassName("set6midcw");
    var set7midcw = document.getElementsByClassName("set7midcw");
    var set8midcw = document.getElementsByClassName("set8midcw");
    var set9midcw = document.getElementsByClassName("set9midcw");
    var set10midcw = document.getElementsByClassName("set10midcw");
    var set1finalcw = document.getElementsByClassName("set1finalcw");
    var set2finalcw = document.getElementsByClassName("set2finalcw");
    var set3finalcw = document.getElementsByClassName("set3finalcw");
    var set4finalcw = document.getElementsByClassName("set4finalcw");
    var set5finalcw = document.getElementsByClassName("set5finalcw");
    var set6finalcw = document.getElementsByClassName("set6finalcw");
    var set7finalcw = document.getElementsByClassName("set7finalcw");
    var set8finalcw = document.getElementsByClassName("set8finalcw");
    var set9finalcw = document.getElementsByClassName("set9finalcw");
    var set10finalcw = document.getElementsByClassName("set10finalcw");
    var maxmidscore = document.getElementsByClassName("maxmidscore");
    var maxlatescore = document.getElementsByClassName("maxlatescore");

    var stdscore1 = document.getElementsByClassName("lockg1");
    var stdscore2 = document.getElementsByClassName("lockg2");
    var stdscore3 = document.getElementsByClassName("lockg3");
    var stdscore4 = document.getElementsByClassName("lockg4");
    var stdscore5 = document.getElementsByClassName("lockg5");
    var stdscore6 = document.getElementsByClassName("lockg6");
    var stdscore7 = document.getElementsByClassName("lockg7");
    var stdscore8 = document.getElementsByClassName("lockg8");
    var stdscore9 = document.getElementsByClassName("lockg9");
    var stdscore10 = document.getElementsByClassName("lockg10");
    var stdscore11 = document.getElementsByClassName("lockg11");
    var stdscore12 = document.getElementsByClassName("lockg12");
    var stdscore13 = document.getElementsByClassName("lockg13");
    var stdscore14 = document.getElementsByClassName("lockg14");
    var stdscore15 = document.getElementsByClassName("lockg15");
    var stdscore16 = document.getElementsByClassName("lockg16");
    var stdscore17 = document.getElementsByClassName("lockg17");
    var stdscore18 = document.getElementsByClassName("lockg18");
    var stdscore19 = document.getElementsByClassName("lockg19");
    var stdscore20 = document.getElementsByClassName("lockg20");

    var stdchewat1 = document.getElementsByClassName("lockcw1");
    var stdchewat2 = document.getElementsByClassName("lockcw2");
    var stdchewat3 = document.getElementsByClassName("lockcw3");
    var stdchewat4 = document.getElementsByClassName("lockcw4");
    var stdchewat5 = document.getElementsByClassName("lockcw5");
    var stdchewat6 = document.getElementsByClassName("lockcw6");
    var stdchewat7 = document.getElementsByClassName("lockcw7");
    var stdchewat8 = document.getElementsByClassName("lockcw8");
    var stdchewat9 = document.getElementsByClassName("lockcw9");
    var stdchewat10 = document.getElementsByClassName("lockcw10");
    var stdchewat11 = document.getElementsByClassName("lockcw11");
    var stdchewat12 = document.getElementsByClassName("lockcw12");
    var stdchewat13 = document.getElementsByClassName("lockcw13");
    var stdchewat14 = document.getElementsByClassName("lockcw14");
    var stdchewat15 = document.getElementsByClassName("lockcw15");
    var stdchewat16 = document.getElementsByClassName("lockcw16");
    var stdchewat17 = document.getElementsByClassName("lockcw17");
    var stdchewat18 = document.getElementsByClassName("lockcw18");
    var stdchewat19 = document.getElementsByClassName("lockcw19");
    var stdchewat20 = document.getElementsByClassName("lockcw20");

    var stdbehave1 = document.getElementsByClassName("behavetxt1");
    var stdbehave2 = document.getElementsByClassName("behavetxt2");
    var stdbehave3 = document.getElementsByClassName("behavetxt3");
    var stdbehave4 = document.getElementsByClassName("behavetxt4");
    var stdbehave5 = document.getElementsByClassName("behavetxt5");
    var stdbehave6 = document.getElementsByClassName("behavetxt6");
    var stdbehave7 = document.getElementsByClassName("behavetxt7");
    var stdbehave8 = document.getElementsByClassName("behavetxt8");
    var stdbehave9 = document.getElementsByClassName("behavetxt9");
    var stdbehave10 = document.getElementsByClassName("behavetxt10");

    var stdmid1 = document.getElementsByClassName("lockm1");
    var stdmid2 = document.getElementsByClassName("lockm2");
    var stdmid3 = document.getElementsByClassName("lockm3");
    var stdmid4 = document.getElementsByClassName("lockm4");
    var stdmid5 = document.getElementsByClassName("lockm5");
    var stdmid6 = document.getElementsByClassName("lockm6");
    var stdmid7 = document.getElementsByClassName("lockm7");
    var stdmid8 = document.getElementsByClassName("lockm8");
    var stdmid9 = document.getElementsByClassName("lockm9");
    var stdmid10 = document.getElementsByClassName("lockm10");
    var stdfinal1 = document.getElementsByClassName("lockf1");
    var stdfinal2 = document.getElementsByClassName("lockf2");
    var stdfinal3 = document.getElementsByClassName("lockf3");
    var stdfinal4 = document.getElementsByClassName("lockf4");
    var stdfinal5 = document.getElementsByClassName("lockf5");
    var stdfinal6 = document.getElementsByClassName("lockf6");
    var stdfinal7 = document.getElementsByClassName("lockf7");
    var stdfinal8 = document.getElementsByClassName("lockf8");
    var stdfinal9 = document.getElementsByClassName("lockf9");
    var stdfinal10 = document.getElementsByClassName("lockf10");
    var stdmidtotal = document.getElementsByClassName("lockmidterm");
    var stdfinaltotal = document.getElementsByClassName("lockfinalterm");
    var stdsamatanatotal = document.getElementsByClassName("samatscore");
    var stdreadwritetotal = document.getElementsByClassName("readscore");
    var stdbehavetotal = document.getElementsByClassName("goodbe");
    var stdSID = document.getElementsByClassName("stdSID");
    var stdidlist = document.getElementsByClassName("stdidlist");
    var loadstatus = document.getElementsByClassName("loadstatus");
    loadstatus[0].classList.remove('hidden');

    var idlist = [];
    for (var x = 0; x < stdidlist.length; x++)
        idlist.push(stdidlist[x].value);



    this.parseExcel = function (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });
            var check = 0;
            workbook.SheetNames.forEach(function (sheetName) {
                // Here is your object
                //alert(sheetName);
                var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                var json_object = JSON.stringify(XL_row_object);
                console.log(JSON.parse(json_object));
                var data = JSON.parse(json_object);
                var yyy = "จำนวนนักเรียน " + data.length;

                if (sheetName == "สำหรับdev1") {
                    check = 1;
                    if (data[0].fRatioQuiz % 5 == 0)
                        document.getElementById('ctl00_MainContent_scoreRatio').value = data[0].fRatioQuiz;
                    if (data[0].fRatioMidTerm % 5 == 0)
                        document.getElementById('ctl00_MainContent_midRatio').value = data[0].fRatioMidTerm;
                    if (data[0].fRatioLateTerm % 5 == 0)
                        document.getElementById('ctl00_MainContent_lastRatio').value = data[0].fRatioLateTerm;
                    if (data[0].fRatioQuizPass % 5 == 0)
                        document.getElementById('ctl00_MainContent_passRatio').value = data[0].fRatioQuizPass;


                    if (hasClass(maxmidscore[0], 'disable') == false) { maxmidscore[0].value = data[0].maxmidtotal; }
                    if (hasClass(maxlatescore[0], 'disable') == false) { maxlatescore[0].value = data[0].maxfinaltotal; }

                    if (hasClass(maxtest[0], 'disable') == false) { maxtest[0].value = data[0].maxGrade1; }
                    if (hasClass(maxtest[1], 'disable') == false) { maxtest[1].value = data[0].maxGrade2; }
                    if (hasClass(maxtest[2], 'disable') == false) { maxtest[2].value = data[0].maxGrade3; }
                    if (hasClass(maxtest[3], 'disable') == false) { maxtest[3].value = data[0].maxGrade4; }
                    if (hasClass(maxtest[4], 'disable') == false) { maxtest[4].value = data[0].maxGrade5; }
                    if (hasClass(maxtest[5], 'disable') == false) { maxtest[5].value = data[0].maxGrade6; }
                    if (hasClass(maxtest[6], 'disable') == false) { maxtest[6].value = data[0].maxGrade7; }
                    if (hasClass(maxtest[7], 'disable') == false) { maxtest[7].value = data[0].maxGrade8; }
                    if (hasClass(maxtest[8], 'disable') == false) { maxtest[8].value = data[0].maxGrade9; }
                    if (hasClass(maxtest[9], 'disable') == false) { maxtest[9].value = data[0].maxGrade10; }
                    if (hasClass(maxtest[10], 'disable') == false) { maxtest[10].value = data[0].maxGrade11; }
                    if (hasClass(maxtest[11], 'disable') == false) { maxtest[11].value = data[0].maxGrade12; }
                    if (hasClass(maxtest[12], 'disable') == false) { maxtest[12].value = data[0].maxGrade13; }
                    if (hasClass(maxtest[13], 'disable') == false) { maxtest[13].value = data[0].maxGrade14; }
                    if (hasClass(maxtest[14], 'disable') == false) { maxtest[14].value = data[0].maxGrade15; }
                    if (hasClass(maxtest[15], 'disable') == false) { maxtest[15].value = data[0].maxGrade16; }
                    if (hasClass(maxtest[16], 'disable') == false) { maxtest[16].value = data[0].maxGrade17; }
                    if (hasClass(maxtest[17], 'disable') == false) { maxtest[17].value = data[0].maxGrade18; }
                    if (hasClass(maxtest[18], 'disable') == false) { maxtest[18].value = data[0].maxGrade19; }
                    if (hasClass(maxtest[19], 'disable') == false) { maxtest[19].value = data[0].maxGrade20; }

                    if (hasClass(maxtestcw[0], 'disable') == false) { maxtestcw[0].value = data[0].maxCheewat1; }
                    if (hasClass(maxtestcw[1], 'disable') == false) { maxtestcw[1].value = data[0].maxCheewat2; }
                    if (hasClass(maxtestcw[2], 'disable') == false) { maxtestcw[2].value = data[0].maxCheewat3; }
                    if (hasClass(maxtestcw[3], 'disable') == false) { maxtestcw[3].value = data[0].maxCheewat4; }
                    if (hasClass(maxtestcw[4], 'disable') == false) { maxtestcw[4].value = data[0].maxCheewat5; }
                    if (hasClass(maxtestcw[5], 'disable') == false) { maxtestcw[5].value = data[0].maxCheewat6; }
                    if (hasClass(maxtestcw[6], 'disable') == false) { maxtestcw[6].value = data[0].maxCheewat7; }
                    if (hasClass(maxtestcw[7], 'disable') == false) { maxtestcw[7].value = data[0].maxCheewat8; }
                    if (hasClass(maxtestcw[8], 'disable') == false) { maxtestcw[8].value = data[0].maxCheewat9; }
                    if (hasClass(maxtestcw[9], 'disable') == false) { maxtestcw[9].value = data[0].maxCheewat10; }
                    if (hasClass(maxtestcw[10], 'disable') == false) { maxtestcw[10].value = data[0].maxCheewat11; }
                    if (hasClass(maxtestcw[11], 'disable') == false) { maxtestcw[11].value = data[0].maxCheewat12; }
                    if (hasClass(maxtestcw[12], 'disable') == false) { maxtestcw[12].value = data[0].maxCheewat13; }
                    if (hasClass(maxtestcw[13], 'disable') == false) { maxtestcw[13].value = data[0].maxCheewat14; }
                    if (hasClass(maxtestcw[14], 'disable') == false) { maxtestcw[14].value = data[0].maxCheewat15; }
                    if (hasClass(maxtestcw[15], 'disable') == false) { maxtestcw[15].value = data[0].maxCheewat16; }
                    if (hasClass(maxtestcw[16], 'disable') == false) { maxtestcw[16].value = data[0].maxCheewat17; }
                    if (hasClass(maxtestcw[17], 'disable') == false) { maxtestcw[17].value = data[0].maxCheewat18; }
                    if (hasClass(maxtestcw[18], 'disable') == false) { maxtestcw[18].value = data[0].maxCheewat19; }
                    if (hasClass(maxtestcw[19], 'disable') == false) { maxtestcw[19].value = data[0].maxCheewat20; }

                    if (hasClass(set1name[0], 'disable') == false) set1name[0].value = data[0].nameGrade1;
                    if (hasClass(set2name[0], 'disable') == false) set2name[0].value = data[0].nameGrade2;
                    if (hasClass(set3name[0], 'disable') == false) set3name[0].value = data[0].nameGrade3;
                    if (hasClass(set4name[0], 'disable') == false) set4name[0].value = data[0].nameGrade4;
                    if (hasClass(set5name[0], 'disable') == false) set5name[0].value = data[0].nameGrade5;
                    if (hasClass(set6name[0], 'disable') == false) set6name[0].value = data[0].nameGrade6;
                    if (hasClass(set7name[0], 'disable') == false) set7name[0].value = data[0].nameGrade7;
                    if (hasClass(set8name[0], 'disable') == false) set8name[0].value = data[0].nameGrade8;
                    if (hasClass(set9name[0], 'disable') == false) set9name[0].value = data[0].nameGrade9;
                    if (hasClass(set10name[0], 'disable') == false) set10name[0].value = data[0].nameGrade10;
                    if (hasClass(set11name[0], 'disable') == false) set11name[0].value = data[0].nameGrade11;
                    if (hasClass(set12name[0], 'disable') == false) set12name[0].value = data[0].nameGrade12;
                    if (hasClass(set13name[0], 'disable') == false) set13name[0].value = data[0].nameGrade13;
                    if (hasClass(set14name[0], 'disable') == false) set14name[0].value = data[0].nameGrade14;
                    if (hasClass(set15name[0], 'disable') == false) set15name[0].value = data[0].nameGrade15;
                    if (hasClass(set16name[0], 'disable') == false) set16name[0].value = data[0].nameGrade16;
                    if (hasClass(set17name[0], 'disable') == false) set17name[0].value = data[0].nameGrade17;
                    if (hasClass(set18name[0], 'disable') == false) set18name[0].value = data[0].nameGrade18;
                    if (hasClass(set19name[0], 'disable') == false) set19name[0].value = data[0].nameGrade19;
                    if (hasClass(set20name[0], 'disable') == false) set20name[0].value = data[0].nameGrade20;

                    if (hasClass(set1namecw[0], 'disable') == false) set1namecw[0].value = data[0].nameCheewat1;
                    if (hasClass(set2namecw[0], 'disable') == false) set2namecw[0].value = data[0].nameCheewat2;
                    if (hasClass(set3namecw[0], 'disable') == false) set3namecw[0].value = data[0].nameCheewat3;
                    if (hasClass(set4namecw[0], 'disable') == false) set4namecw[0].value = data[0].nameCheewat4;
                    if (hasClass(set5namecw[0], 'disable') == false) set5namecw[0].value = data[0].nameCheewat5;
                    if (hasClass(set6namecw[0], 'disable') == false) set6namecw[0].value = data[0].nameCheewat6;
                    if (hasClass(set7namecw[0], 'disable') == false) set7namecw[0].value = data[0].nameCheewat7;
                    if (hasClass(set8namecw[0], 'disable') == false) set8namecw[0].value = data[0].nameCheewat8;
                    if (hasClass(set9namecw[0], 'disable') == false) set9namecw[0].value = data[0].nameCheewat9;
                    if (hasClass(set10namecw[0], 'disable') == false) set10namecw[0].value = data[0].nameCheewat10;
                    if (hasClass(set11namecw[0], 'disable') == false) set11namecw[0].value = data[0].nameCheewat11;
                    if (hasClass(set12namecw[0], 'disable') == false) set12namecw[0].value = data[0].nameCheewat12;
                    if (hasClass(set13namecw[0], 'disable') == false) set13namecw[0].value = data[0].nameCheewat13;
                    if (hasClass(set14namecw[0], 'disable') == false) set14namecw[0].value = data[0].nameCheewat14;
                    if (hasClass(set15namecw[0], 'disable') == false) set15namecw[0].value = data[0].nameCheewat15;
                    if (hasClass(set16namecw[0], 'disable') == false) set16namecw[0].value = data[0].nameCheewat16;
                    if (hasClass(set17namecw[0], 'disable') == false) set17namecw[0].value = data[0].nameCheewat17;
                    if (hasClass(set18namecw[0], 'disable') == false) set18namecw[0].value = data[0].nameCheewat18;
                    if (hasClass(set19namecw[0], 'disable') == false) set19namecw[0].value = data[0].nameCheewat19;
                    if (hasClass(set20namecw[0], 'disable') == false) set20namecw[0].value = data[0].nameCheewat20;


                    if (hasClass(maxtestb1[0], 'disable') == false) maxtestb1[0].value = data[0].maxBehavior1;
                    if (hasClass(maxtestb1[1], 'disable') == false) maxtestb1[1].value = data[0].maxBehavior2;
                    if (hasClass(maxtestb1[2], 'disable') == false) maxtestb1[2].value = data[0].maxBehavior3;
                    if (hasClass(maxtestb1[3], 'disable') == false) maxtestb1[3].value = data[0].maxBehavior4;
                    if (hasClass(maxtestb1[4], 'disable') == false) maxtestb1[4].value = data[0].maxBehavior5;
                    if (hasClass(maxtestb1[5], 'disable') == false) maxtestb1[5].value = data[0].maxBehavior6;
                    if (hasClass(maxtestb1[6], 'disable') == false) maxtestb1[6].value = data[0].maxBehavior7;
                    if (hasClass(maxtestb1[7], 'disable') == false) maxtestb1[7].value = data[0].maxBehavior8;
                    if (hasClass(maxtestb1[8], 'disable') == false) maxtestb1[8].value = data[0].maxBehavior9;
                    if (hasClass(maxtestb1[9], 'disable') == false) maxtestb1[9].value = data[0].maxBehavior10;

                    if (hasClass(setnameb1[0], 'disable') == false) setnameb1[0].value = data[0].nameBehavior1;
                    if (hasClass(setnameb2[0], 'disable') == false) setnameb2[0].value = data[0].nameBehavior2;
                    if (hasClass(setnameb3[0], 'disable') == false) setnameb3[0].value = data[0].nameBehavior3;
                    if (hasClass(setnameb4[0], 'disable') == false) setnameb4[0].value = data[0].nameBehavior4;
                    if (hasClass(setnameb5[0], 'disable') == false) setnameb5[0].value = data[0].nameBehavior5;
                    if (hasClass(setnameb6[0], 'disable') == false) setnameb6[0].value = data[0].nameBehavior6;
                    if (hasClass(setnameb7[0], 'disable') == false) setnameb7[0].value = data[0].nameBehavior7;
                    if (hasClass(setnameb8[0], 'disable') == false) setnameb8[0].value = data[0].nameBehavior8;
                    if (hasClass(setnameb9[0], 'disable') == false) setnameb9[0].value = data[0].nameBehavior9;
                    if (hasClass(setnameb10[0], 'disable') == false) setnameb10[0].value = data[0].nameBehavior10;

                    if (hasClass(maxmidcw[0], 'disable') == false) maxmidcw[0].value = data[0].maxMid1;
                    if (hasClass(maxmidcw[1], 'disable') == false) maxmidcw[1].value = data[0].maxMid2;
                    if (hasClass(maxmidcw[2], 'disable') == false) maxmidcw[2].value = data[0].maxMid3;
                    if (hasClass(maxmidcw[3], 'disable') == false) maxmidcw[3].value = data[0].maxMid4;
                    if (hasClass(maxmidcw[4], 'disable') == false) maxmidcw[4].value = data[0].maxMid5;
                    if (hasClass(maxmidcw[5], 'disable') == false) maxmidcw[5].value = data[0].maxMid6;
                    if (hasClass(maxmidcw[6], 'disable') == false) maxmidcw[6].value = data[0].maxMid7;
                    if (hasClass(maxmidcw[7], 'disable') == false) maxmidcw[7].value = data[0].maxMid8;
                    if (hasClass(maxmidcw[8], 'disable') == false) maxmidcw[8].value = data[0].maxMid9;
                    if (hasClass(maxmidcw[9], 'disable') == false) maxmidcw[9].value = data[0].maxMid10;

                    if (hasClass(set1midcw[0], 'disable') == false) set1midcw[0].value = data[0].nameMid1;
                    if (hasClass(set2midcw[0], 'disable') == false) set2midcw[0].value = data[0].nameMid2;
                    if (hasClass(set3midcw[0], 'disable') == false) set3midcw[0].value = data[0].nameMid3;
                    if (hasClass(set4midcw[0], 'disable') == false) set4midcw[0].value = data[0].nameMid4;
                    if (hasClass(set5midcw[0], 'disable') == false) set5midcw[0].value = data[0].nameMid5;
                    if (hasClass(set6midcw[0], 'disable') == false) set6midcw[0].value = data[0].nameMid6;
                    if (hasClass(set7midcw[0], 'disable') == false) set7midcw[0].value = data[0].nameMid7;
                    if (hasClass(set8midcw[0], 'disable') == false) set8midcw[0].value = data[0].nameMid8;
                    if (hasClass(set9midcw[0], 'disable') == false) set9midcw[0].value = data[0].nameMid9;
                    if (hasClass(set10midcw[0], 'disable') == false) set10midcw[0].value = data[0].nameMid10;

                    if (hasClass(maxfinalcw[0], 'disable') == false) maxfinalcw[0].value = data[0].maxFinal1;
                    if (hasClass(maxfinalcw[1], 'disable') == false) maxfinalcw[1].value = data[0].maxFinal2;
                    if (hasClass(maxfinalcw[2], 'disable') == false) maxfinalcw[2].value = data[0].maxFinal3;
                    if (hasClass(maxfinalcw[3], 'disable') == false) maxfinalcw[3].value = data[0].maxFinal4;
                    if (hasClass(maxfinalcw[4], 'disable') == false) maxfinalcw[4].value = data[0].maxFinal5;
                    if (hasClass(maxfinalcw[5], 'disable') == false) maxfinalcw[5].value = data[0].maxFinal6;
                    if (hasClass(maxfinalcw[6], 'disable') == false) maxfinalcw[6].value = data[0].maxFinal7;
                    if (hasClass(maxfinalcw[7], 'disable') == false) maxfinalcw[7].value = data[0].maxFinal8;
                    if (hasClass(maxfinalcw[8], 'disable') == false) maxfinalcw[8].value = data[0].maxFinal9;
                    if (hasClass(maxfinalcw[9], 'disable') == false) maxfinalcw[9].value = data[0].maxFinal10;

                    if (hasClass(set1finalcw[0], 'disable') == false) set1finalcw[0].value = data[0].nameFinal1;
                    if (hasClass(set2finalcw[0], 'disable') == false) set2finalcw[0].value = data[0].nameFinal2;
                    if (hasClass(set3finalcw[0], 'disable') == false) set3finalcw[0].value = data[0].nameFinal3;
                    if (hasClass(set4finalcw[0], 'disable') == false) set4finalcw[0].value = data[0].nameFinal4;
                    if (hasClass(set5finalcw[0], 'disable') == false) set5finalcw[0].value = data[0].nameFinal5;
                    if (hasClass(set6finalcw[0], 'disable') == false) set6finalcw[0].value = data[0].nameFinal6;
                    if (hasClass(set7finalcw[0], 'disable') == false) set7finalcw[0].value = data[0].nameFinal7;
                    if (hasClass(set8finalcw[0], 'disable') == false) set8finalcw[0].value = data[0].nameFinal8;
                    if (hasClass(set9finalcw[0], 'disable') == false) set9finalcw[0].value = data[0].nameFinal9;
                    if (hasClass(set10finalcw[0], 'disable') == false) set10finalcw[0].value = data[0].nameFinal10;

                    for (var x = 0; x < 10; x++) {
                        if (maxfinalcw[x].value == 'undefined') maxfinalcw[x].value = "";
                        if (maxtestb1[x].value == 'undefined') maxtestb1[x].value = "";
                        if (maxmidcw[x].value == 'undefined') maxmidcw[x].value = "";
                    }

                    for (var y = 0; y < 20; y++) {
                        if (maxtest[x].value == 'undefined') maxtest[x].value = "";

                    }

                    if (set1name[0].value == 'undefined') set1name[0].value = "";
                    if (set2name[0].value == 'undefined') set2name[0].value = "";
                    if (set3name[0].value == 'undefined') set3name[0].value = "";
                    if (set4name[0].value == 'undefined') set4name[0].value = "";
                    if (set5name[0].value == 'undefined') set5name[0].value = "";
                    if (set6name[0].value == 'undefined') set6name[0].value = "";
                    if (set7name[0].value == 'undefined') set7name[0].value = "";
                    if (set8name[0].value == 'undefined') set8name[0].value = "";
                    if (set9name[0].value == 'undefined') set9name[0].value = "";
                    if (set10name[0].value == 'undefined') set10name[0].value = "";
                    if (set11name[0].value == 'undefined') set11name[0].value = "";
                    if (set12name[0].value == 'undefined') set12name[0].value = "";
                    if (set13name[0].value == 'undefined') set13name[0].value = "";
                    if (set14name[0].value == 'undefined') set14name[0].value = "";
                    if (set15name[0].value == 'undefined') set15name[0].value = "";
                    if (set16name[0].value == 'undefined') set16name[0].value = "";
                    if (set17name[0].value == 'undefined') set17name[0].value = "";
                    if (set18name[0].value == 'undefined') set18name[0].value = "";
                    if (set19name[0].value == 'undefined') set19name[0].value = "";
                    if (set20name[0].value == 'undefined') set20name[0].value = "";



                    if (setnameb1[0].value == 'undefined') setnameb1[0].value = "";
                    if (setnameb2[0].value == 'undefined') setnameb2[0].value = "";
                    if (setnameb3[0].value == 'undefined') setnameb3[0].value = "";
                    if (setnameb4[0].value == 'undefined') setnameb4[0].value = "";
                    if (setnameb5[0].value == 'undefined') setnameb5[0].value = "";
                    if (setnameb6[0].value == 'undefined') setnameb6[0].value = "";
                    if (setnameb7[0].value == 'undefined') setnameb7[0].value = "";
                    if (setnameb8[0].value == 'undefined') setnameb8[0].value = "";
                    if (setnameb9[0].value == 'undefined') setnameb9[0].value = "";
                    if (setnameb10[0].value == 'undefined') setnameb10[0].value = "";

                    if (set1midcw[0].value == 'undefined') set1midcw[0].value = "";
                    if (set2midcw[0].value == 'undefined') set2midcw[0].value = "";
                    if (set3midcw[0].value == 'undefined') set3midcw[0].value = "";
                    if (set4midcw[0].value == 'undefined') set4midcw[0].value = "";
                    if (set5midcw[0].value == 'undefined') set5midcw[0].value = "";
                    if (set6midcw[0].value == 'undefined') set6midcw[0].value = "";
                    if (set7midcw[0].value == 'undefined') set7midcw[0].value = "";
                    if (set8midcw[0].value == 'undefined') set8midcw[0].value = "";
                    if (set9midcw[0].value == 'undefined') set9midcw[0].value = "";
                    if (set10midcw[0].value == 'undefined') set10midcw[0].value = "";

                    if (set1finalcw[0].value == 'undefined') set1finalcw[0].value = "";
                    if (set2finalcw[0].value == 'undefined') set2finalcw[0].value = "";
                    if (set3finalcw[0].value == 'undefined') set3finalcw[0].value = "";
                    if (set4finalcw[0].value == 'undefined') set4finalcw[0].value = "";
                    if (set5finalcw[0].value == 'undefined') set5finalcw[0].value = "";
                    if (set6finalcw[0].value == 'undefined') set6finalcw[0].value = "";
                    if (set7finalcw[0].value == 'undefined') set7finalcw[0].value = "";
                    if (set8finalcw[0].value == 'undefined') set8finalcw[0].value = "";
                    if (set9finalcw[0].value == 'undefined') set9finalcw[0].value = "";
                    if (set10finalcw[0].value == 'undefined') set10finalcw[0].value = "";
                }
                if (sheetName == "สำหรับdev2") {
                    var message1 = '<h2><p class="centertext">';
                    var count = 0;
                    var notfound = 0;
                    var midcount = 0;
                    var finalcount = 0;
                    var behavecount = 0;
                    var cwcount = 0;
                    var readcount = 0;

                    for (var x = 0; x < data.length; x++) {
                        if (data[x].stdSID != 0) {
                            var sss = data[x].stdSID;
                            var xx = idlist.indexOf(sss);


                            if (xx != -1) {
                                count = count + 1;

                                if (hasClass(stdscore1[xx], 'disable') == false) { if (data[x].scoreGrade1 != null) stdscore1[xx].value = data[x].scoreGrade1; else stdscore1[xx].value = ""; }
                                if (hasClass(stdscore2[xx], 'disable') == false) { if (data[x].scoreGrade2 != null) stdscore2[xx].value = data[x].scoreGrade2; else stdscore2[xx].value = ""; }
                                if (hasClass(stdscore3[xx], 'disable') == false) { if (data[x].scoreGrade3 != null) stdscore3[xx].value = data[x].scoreGrade3; else stdscore3[xx].value = ""; }
                                if (hasClass(stdscore4[xx], 'disable') == false) { if (data[x].scoreGrade4 != null) stdscore4[xx].value = data[x].scoreGrade4; else stdscore4[xx].value = ""; }
                                if (hasClass(stdscore5[xx], 'disable') == false) { if (data[x].scoreGrade5 != null) stdscore5[xx].value = data[x].scoreGrade5; else stdscore5[xx].value = ""; }
                                if (hasClass(stdscore6[xx], 'disable') == false) { if (data[x].scoreGrade6 != null) stdscore6[xx].value = data[x].scoreGrade6; else stdscore6[xx].value = ""; }
                                if (hasClass(stdscore7[xx], 'disable') == false) { if (data[x].scoreGrade7 != null) stdscore7[xx].value = data[x].scoreGrade7; else stdscore7[xx].value = ""; }
                                if (hasClass(stdscore8[xx], 'disable') == false) { if (data[x].scoreGrade8 != null) stdscore8[xx].value = data[x].scoreGrade8; else stdscore8[xx].value = ""; }
                                if (hasClass(stdscore9[xx], 'disable') == false) { if (data[x].scoreGrade9 != null) stdscore9[xx].value = data[x].scoreGrade9; else stdscore9[xx].value = ""; }
                                if (hasClass(stdscore10[xx], 'disable') == false) { if (data[x].scoreGrade10 != null) stdscore10[xx].value = data[x].scoreGrade10; else stdscore10[xx].value = ""; }
                                if (hasClass(stdscore11[xx], 'disable') == false) { if (data[x].scoreGrade11 != null) stdscore11[xx].value = data[x].scoreGrade11; else stdscore11[xx].value = ""; }
                                if (hasClass(stdscore12[xx], 'disable') == false) { if (data[x].scoreGrade12 != null) stdscore12[xx].value = data[x].scoreGrade12; else stdscore12[xx].value = ""; }
                                if (hasClass(stdscore13[xx], 'disable') == false) { if (data[x].scoreGrade13 != null) stdscore13[xx].value = data[x].scoreGrade13; else stdscore13[xx].value = ""; }
                                if (hasClass(stdscore14[xx], 'disable') == false) { if (data[x].scoreGrade14 != null) stdscore14[xx].value = data[x].scoreGrade14; else stdscore14[xx].value = ""; }
                                if (hasClass(stdscore15[xx], 'disable') == false) { if (data[x].scoreGrade15 != null) stdscore15[xx].value = data[x].scoreGrade15; else stdscore15[xx].value = ""; }
                                if (hasClass(stdscore16[xx], 'disable') == false) { if (data[x].scoreGrade16 != null) stdscore16[xx].value = data[x].scoreGrade16; else stdscore16[xx].value = ""; }
                                if (hasClass(stdscore17[xx], 'disable') == false) { if (data[x].scoreGrade17 != null) stdscore17[xx].value = data[x].scoreGrade17; else stdscore17[xx].value = ""; }
                                if (hasClass(stdscore18[xx], 'disable') == false) { if (data[x].scoreGrade18 != null) stdscore18[xx].value = data[x].scoreGrade18; else stdscore18[xx].value = ""; }
                                if (hasClass(stdscore19[xx], 'disable') == false) { if (data[x].scoreGrade19 != null) stdscore19[xx].value = data[x].scoreGrade19; else stdscore19[xx].value = ""; }
                                if (hasClass(stdscore20[xx], 'disable') == false) { if (data[x].scoreGrade20 != null) stdscore20[xx].value = data[x].scoreGrade20; else stdscore20[xx].value = ""; }

                                if (hasClass(stdchewat1[xx], 'disable') == false) { if (data[x].scoreCheewat1 != null) { stdchewat1[xx].value = data[x].scoreCheewat1; if (Number(data[x].scoreCheewat1) > 0) cwcount++; } else stdchewat1[xx].value = ""; }
                                if (hasClass(stdchewat2[xx], 'disable') == false) { if (data[x].scoreCheewat2 != null) stdchewat2[xx].value = data[x].scoreCheewat2; else stdchewat2[xx].value = ""; }
                                if (hasClass(stdchewat3[xx], 'disable') == false) { if (data[x].scoreCheewat3 != null) stdchewat3[xx].value = data[x].scoreCheewat3; else stdchewat3[xx].value = ""; }
                                if (hasClass(stdchewat4[xx], 'disable') == false) { if (data[x].scoreCheewat4 != null) stdchewat4[xx].value = data[x].scoreCheewat4; else stdchewat4[xx].value = ""; }
                                if (hasClass(stdchewat5[xx], 'disable') == false) { if (data[x].scoreCheewat5 != null) stdchewat5[xx].value = data[x].scoreCheewat5; else stdchewat5[xx].value = ""; }
                                if (hasClass(stdchewat6[xx], 'disable') == false) { if (data[x].scoreCheewat6 != null) stdchewat6[xx].value = data[x].scoreCheewat6; else stdchewat6[xx].value = ""; }
                                if (hasClass(stdchewat7[xx], 'disable') == false) { if (data[x].scoreCheewat7 != null) stdchewat7[xx].value = data[x].scoreCheewat7; else stdchewat7[xx].value = ""; }
                                if (hasClass(stdchewat8[xx], 'disable') == false) { if (data[x].scoreCheewat8 != null) stdchewat8[xx].value = data[x].scoreCheewat8; else stdchewat8[xx].value = ""; }
                                if (hasClass(stdchewat9[xx], 'disable') == false) { if (data[x].scoreCheewat9 != null) stdchewat9[xx].value = data[x].scoreCheewat9; else stdchewat9[xx].value = ""; }
                                if (hasClass(stdchewat10[xx], 'disable') == false) { if (data[x].scoreCheewat10 != null) stdchewat10[xx].value = data[x].scoreCheewat10; else stdchewat10[xx].value = ""; }
                                if (hasClass(stdchewat11[xx], 'disable') == false) { if (data[x].scoreCheewat11 != null) stdchewat11[xx].value = data[x].scoreCheewat11; else stdchewat11[xx].value = ""; }
                                if (hasClass(stdchewat12[xx], 'disable') == false) { if (data[x].scoreCheewat12 != null) stdchewat12[xx].value = data[x].scoreCheewat12; else stdchewat12[xx].value = ""; }
                                if (hasClass(stdchewat13[xx], 'disable') == false) { if (data[x].scoreCheewat13 != null) stdchewat13[xx].value = data[x].scoreCheewat13; else stdchewat13[xx].value = ""; }
                                if (hasClass(stdchewat14[xx], 'disable') == false) { if (data[x].scoreCheewat14 != null) stdchewat14[xx].value = data[x].scoreCheewat14; else stdchewat14[xx].value = ""; }
                                if (hasClass(stdchewat15[xx], 'disable') == false) { if (data[x].scoreCheewat15 != null) stdchewat15[xx].value = data[x].scoreCheewat15; else stdchewat15[xx].value = ""; }
                                if (hasClass(stdchewat16[xx], 'disable') == false) { if (data[x].scoreCheewat16 != null) stdchewat16[xx].value = data[x].scoreCheewat16; else stdchewat16[xx].value = ""; }
                                if (hasClass(stdchewat17[xx], 'disable') == false) { if (data[x].scoreCheewat17 != null) stdchewat17[xx].value = data[x].scoreCheewat17; else stdchewat17[xx].value = ""; }
                                if (hasClass(stdchewat18[xx], 'disable') == false) { if (data[x].scoreCheewat18 != null) stdchewat18[xx].value = data[x].scoreCheewat18; else stdchewat18[xx].value = ""; }
                                if (hasClass(stdchewat19[xx], 'disable') == false) { if (data[x].scoreCheewat19 != null) stdchewat19[xx].value = data[x].scoreCheewat19; else stdchewat19[xx].value = ""; }
                                if (hasClass(stdchewat20[xx], 'disable') == false) { if (data[x].scoreCheewat20 != null) stdchewat20[xx].value = data[x].scoreCheewat20; else stdchewat20[xx].value = ""; }

                                if (hasClass(stdbehave1[xx], 'disable') == false) { if (data[x].scoreBehavior1 != null) { stdbehave1[xx].value = data[x].scoreBehavior1; behavecount++; } else stdbehave1[xx].value = ""; }
                                if (hasClass(stdbehave2[xx], 'disable') == false) { if (data[x].scoreBehavior2 != null) { stdbehave2[xx].value = data[x].scoreBehavior2; behavecount++; } else stdbehave2[xx].value = ""; }
                                if (hasClass(stdbehave3[xx], 'disable') == false) { if (data[x].scoreBehavior3 != null) { stdbehave3[xx].value = data[x].scoreBehavior3; behavecount++; } else stdbehave3[xx].value = ""; }
                                if (hasClass(stdbehave4[xx], 'disable') == false) { if (data[x].scoreBehavior4 != null) { stdbehave4[xx].value = data[x].scoreBehavior4; behavecount++; } else stdbehave4[xx].value = ""; }
                                if (hasClass(stdbehave5[xx], 'disable') == false) { if (data[x].scoreBehavior5 != null) { stdbehave5[xx].value = data[x].scoreBehavior5; behavecount++; } else stdbehave5[xx].value = ""; }
                                if (hasClass(stdbehave6[xx], 'disable') == false) { if (data[x].scoreBehavior6 != null) { stdbehave6[xx].value = data[x].scoreBehavior6; behavecount++; } else stdbehave6[xx].value = ""; }
                                if (hasClass(stdbehave7[xx], 'disable') == false) { if (data[x].scoreBehavior7 != null) { stdbehave7[xx].value = data[x].scoreBehavior7; behavecount++; } else stdbehave7[xx].value = ""; }
                                if (hasClass(stdbehave8[xx], 'disable') == false) { if (data[x].scoreBehavior8 != null) { stdbehave8[xx].value = data[x].scoreBehavior8; behavecount++; } else stdbehave8[xx].value = ""; }
                                if (hasClass(stdbehave9[xx], 'disable') == false) { if (data[x].scoreBehavior9 != null) { stdbehave9[xx].value = data[x].scoreBehavior9; behavecount++; } else stdbehave9[xx].value = ""; }
                                if (hasClass(stdbehave10[xx], 'disable') == false) { if (data[x].scoreBehavior10 != null) { stdbehave10[xx].value = data[x].scoreBehavior10; behavecount++; } else stdbehave10[xx].value = ""; }

                                if (hasClass(stdmid1[xx], 'disable') == false) { if (data[x].scoreMid1 != null) { stdmid1[xx].value = data[x].scoreMid1; midcount++; } else stdmid1[xx].value = ""; }
                                if (hasClass(stdmid2[xx], 'disable') == false) { if (data[x].scoreMid2 != null) { stdmid2[xx].value = data[x].scoreMid2; midcount++; } else stdmid2[xx].value = ""; }
                                if (hasClass(stdmid3[xx], 'disable') == false) { if (data[x].scoreMid3 != null) { stdmid3[xx].value = data[x].scoreMid3; midcount++; } else stdmid3[xx].value = ""; }
                                if (hasClass(stdmid4[xx], 'disable') == false) { if (data[x].scoreMid4 != null) { stdmid4[xx].value = data[x].scoreMid4; midcount++; } else stdmid4[xx].value = ""; }
                                if (hasClass(stdmid5[xx], 'disable') == false) { if (data[x].scoreMid5 != null) { stdmid5[xx].value = data[x].scoreMid5; midcount++; } else stdmid5[xx].value = ""; }
                                if (hasClass(stdmid6[xx], 'disable') == false) { if (data[x].scoreMid6 != null) { stdmid6[xx].value = data[x].scoreMid6; midcount++; } else stdmid6[xx].value = ""; }
                                if (hasClass(stdmid7[xx], 'disable') == false) { if (data[x].scoreMid7 != null) { stdmid7[xx].value = data[x].scoreMid7; midcount++; } else stdmid7[xx].value = ""; }
                                if (hasClass(stdmid8[xx], 'disable') == false) { if (data[x].scoreMid8 != null) { stdmid8[xx].value = data[x].scoreMid8; midcount++; } else stdmid8[xx].value = ""; }
                                if (hasClass(stdmid9[xx], 'disable') == false) { if (data[x].scoreMid9 != null) { stdmid9[xx].value = data[x].scoreMid9; midcount++; } else stdmid9[xx].value = ""; }
                                if (hasClass(stdmid10[xx], 'disable') == false) { if (data[x].scoreMid10 != null) { stdmid10[xx].value = data[x].scoreMid10; midcount++; } else stdmid10[xx].value = ""; }

                                if (hasClass(stdfinal1[xx], 'disable') == false) { if (data[x].scoreFinal1 != null) { stdfinal1[xx].value = data[x].scoreFinal1; finalcount++; } else stdfinal1[xx].value = ""; }
                                if (hasClass(stdfinal2[xx], 'disable') == false) { if (data[x].scoreFinal2 != null) { stdfinal2[xx].value = data[x].scoreFinal2; finalcount++; } else stdfinal2[xx].value = ""; }
                                if (hasClass(stdfinal3[xx], 'disable') == false) { if (data[x].scoreFinal3 != null) { stdfinal3[xx].value = data[x].scoreFinal3; finalcount++; } else stdfinal3[xx].value = ""; }
                                if (hasClass(stdfinal4[xx], 'disable') == false) { if (data[x].scoreFinal4 != null) { stdfinal4[xx].value = data[x].scoreFinal4; finalcount++; } else stdfinal4[xx].value = ""; }
                                if (hasClass(stdfinal5[xx], 'disable') == false) { if (data[x].scoreFinal5 != null) { stdfinal5[xx].value = data[x].scoreFinal5; finalcount++; } else stdfinal5[xx].value = ""; }
                                if (hasClass(stdfinal6[xx], 'disable') == false) { if (data[x].scoreFinal6 != null) { stdfinal6[xx].value = data[x].scoreFinal6; finalcount++; } else stdfinal6[xx].value = ""; }
                                if (hasClass(stdfinal7[xx], 'disable') == false) { if (data[x].scoreFinal7 != null) { stdfinal7[xx].value = data[x].scoreFinal7; finalcount++; } else stdfinal7[xx].value = ""; }
                                if (hasClass(stdfinal8[xx], 'disable') == false) { if (data[x].scoreFinal8 != null) { stdfinal8[xx].value = data[x].scoreFinal8; finalcount++; } else stdfinal8[xx].value = ""; }
                                if (hasClass(stdfinal9[xx], 'disable') == false) { if (data[x].scoreFinal9 != null) { stdfinal9[xx].value = data[x].scoreFinal9; finalcount++; } else stdfinal9[xx].value = ""; }
                                if (hasClass(stdfinal10[xx], 'disable') == false) { if (data[x].scoreFinal10 != null) { stdfinal10[xx].value = data[x].scoreFinal10; finalcount++; } else stdfinal10[xx].value = ""; }

                                if (hasClass(stdmidtotal[xx], 'disable') == false) { if (data[x].scoreMidTermSUM != null) stdmidtotal[xx].value = data[x].scoreMidTermSUM; else stdmidtotal[xx].value = ""; }
                                if (hasClass(stdfinaltotal[xx], 'disable') == false) { if (data[x].scoreFinalTermSUM != null) stdfinaltotal[xx].value = data[x].scoreFinalTermSUM; else stdfinaltotal[xx].value = ""; }
                                if (hasClass(stdsamatanatotal[xx], 'disable') == false) { if (data[x].scoreSamatana != null) stdsamatanatotal[xx].value = data[x].scoreSamatana; else stdsamatanatotal[xx].value = ""; }
                                if (data[x].getReadWrite != null) { stdreadwritetotal[xx].value = data[x].getReadWrite; readcount++; } else stdreadwritetotal[xx].value = "";
                                if (hasClass(stdbehavetotal[xx], 'disable') == false) { if (data[x].scoreBahaviorSUM != null) stdbehavetotal[xx].value = data[x].scoreBahaviorSUM; else stdbehavetotal[xx].value = ""; }
                                stdmidtotal[xx].value =
                                    Number(stdmid1[xx].value) +
                                    Number(stdmid2[xx].value) +
                                    Number(stdmid3[xx].value) +
                                    Number(stdmid4[xx].value) +
                                    Number(stdmid5[xx].value) +
                                    Number(stdmid6[xx].value) +
                                    Number(stdmid7[xx].value) +
                                    Number(stdmid8[xx].value) +
                                    Number(stdmid9[xx].value) +
                                    Number(stdmid10[xx].value);
                                stdfinaltotal[xx].value =
                                    Number(stdfinal1[xx].value) +
                                    Number(stdfinal2[xx].value) +
                                    Number(stdfinal3[xx].value) +
                                    Number(stdfinal4[xx].value) +
                                    Number(stdfinal5[xx].value) +
                                    Number(stdfinal6[xx].value) +
                                    Number(stdfinal7[xx].value) +
                                    Number(stdfinal8[xx].value) +
                                    Number(stdfinal9[xx].value) +
                                    Number(stdfinal10[xx].value);
                            }
                            else {
                                message1 = message1 + sss + '<br>';
                                notfound = notfound + 1;
                            }
                        }
                    }

                    if (readcount > 0)
                        document.getElementById("check3").checked = true;
                    if (cwcount > 0)
                        document.getElementById("ctl00_MainContent_check6").checked = true;
                    if (midcount > 0)
                        document.getElementById("ctl00_MainContent_check11").checked = true;
                    if (finalcount > 0)
                        document.getElementById("ctl00_MainContent_check12").checked = true;
                    if (behavecount > 0) {
                        document.getElementById("ctl00_MainContent_check4").checked = true;
                        document.getElementById("ctl00_MainContent_check5").checked = true;
                    }
                    message1 = message1 + "</p></h>";
                    var error1 = '<h2><p class="centertext">ไม่พบรหัสนักเรียนต่อไปนี้ในระบบ</p></h>';
                    var done1 = '<h2><p class="centertext">นำเข้าคะแนนเสร็จสิ้น<br>จำนวนนักเรียนที่นำเข้าคะแนนสำเร็จ : ' + count + ' คน </p>';
                    if (notfound == 0)
                        error1 = "";
                    if (count == 0)
                        done1 = "<h2>";

                    setTimeout(function () {
                        auto();
                        CompareDates(99999);
                        changename();
                        calmaxmid();
                        calmaxfinal();
                        setTimeout(function () {
                            loadstatus[0].classList.add('hidden');
                            bootbox.alert({
                                message: done1 + error1 + message1,
                                backdrop: true
                            });
                        }, 1000);
                    }, 2000);



                }



            })
            if (check == 0) {
                bootbox.alert({
                    message: '<h2><p class="centertext">กรุณาใช้แบบฟอร์มที่กำหนดให้เท่านั้น</p></h>',
                    backdrop: true
                });
                loadstatus[0].classList.add('hidden');
            }
        };


        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

function endfunction() {

    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON();
    xl2json.parseExcel(files[0]);
}

function handleFileSelect(evt) {

    var files = evt.target.files; // FileList object
    var xl2json = new ExcelToJSON();
    xl2json.parseExcel(files[0]);
}

$(document).ready(function () {
    $('.js-example-basic-multiple4').select2();
});

function addword4() {
    var from = document.getElementsByClassName("classchoose");
    var data = $('.js-example-basic-multiple4').select2('data')

    var mName = document.getElementsByClassName("modalName");
    var multiClass = document.getElementsByClassName("editmulti4");
    var mStart = document.getElementsByClassName("modalStart");
    var mEnd = document.getElementsByClassName("modalEnd");
    var mType = document.getElementsByClassName("modalType");
    var mColor = document.getElementsByClassName("modalColor");
    var mWho = document.getElementsByClassName("modalWho");
    var x;
    //alert(data.length);
    multiClass[0].value = "";
    for (x = 0; x < Number(data.length); x++) {
        multiClass[0].value = multiClass[0].value + "/" + data[x].id;
    }

}

function editPillbox() {

    var editmulti = document.getElementsByClassName("editmulti");
    var check8 = document.getElementsByClassName("check8");
    var data = $('.js-example-basic-multiple4').select2('data');
    var xxxx = [];
    var fields = editmulti[0].value.split('/');
    if (editmulti[0].value != "")
        document.getElementById("ctl00_MainContent_check8").checked = true;
    for (var x = 0; x < fields.length - 1; x++) {
        xxxx.push(fields[x]);
    }
    $('.js-example-basic-multiple4').val(xxxx).trigger("change");
}

$(document).ready(function () {
    $.protip();
    $("#content-overlay").show();

    GetddlListGroup();
    ShowGroup();

    $('#mainDiv').keypress(function (event) {
        document.getElementById("btnOpenGroupSidebar").disabled = true;


    });

});

var i = 1;
$('#image').click(function () {
});
function myFunction() {
    document.getElementById(i).classList.remove('hidden');
    i = i + 1;
}

function modeString(array) {
    if (array.length == 0)
        return null;

    var modeMap = {},
        maxEl = array[0],
        maxCount = 1;

    if (array.length > 1) {
        for (var i = 0; i < array.length; i++) {
            var el = array[i];

            if (modeMap[el] == null)
                modeMap[el] = 1;
            else
                modeMap[el]++;

            if (modeMap[el] > maxCount) {
                maxEl = el;
                maxCount = modeMap[el];
            }
            else if (modeMap[el] == maxCount) {
                if (Number(maxEl) < Number(el))
                    maxEl = el;
                else maxEl = maxEl;

                maxCount = modeMap[el];
            }
        }
    }
    else {
        maxEl = array[0];
    }

    return maxEl;
}

function focusbox(id, row) {
    var focus1 = document.getElementsByClassName("focus1");
    var focus2 = document.getElementsByClassName("focus2");
    var xx = document.getElementsByClassName("xx");
    var yy = document.getElementsByClassName("yy");
    var up = document.getElementsByClassName("fup");
    var fnow = document.getElementsByClassName("fnow");
    var page = document.getElementsByClassName("fpage");
    var down = document.getElementsByClassName("fdown");
    var left = document.getElementsByClassName("fleft");
    var right = document.getElementsByClassName("fright");
    var AutoCompleteTextBox = document.getElementsByClassName("AutoCompleteTextBox");
    var AutoCompleteTextBoxg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
    var AutoCompleteTextBoxg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
    var AutoCompleteTextBoxg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
    var AutoCompleteTextBox2 = document.getElementsByClassName("AutoCompleteTextBox2");
    focus1[0].value = id;
    focus2[0].value = row;
    var x = id;
    var y = row;

    x = (x % 5);
    page[0].value = Math.floor(id / 5);
    xx[0].value = x;
    yy[0].value = y;
    fnow[0].value = (x * y) + (5 * (y - 1) - (x * (y - 1)));
    var z = fnow[0].value;

    up[0].value = Number(fnow[0].value) - 5;
    down[0].value = Number(fnow[0].value) + 5;
    left[0].value = Number(fnow[0].value) - 1;
    right[0].value = Number(fnow[0].value) + 1;

    var divide = Math.floor(y / x);
    var mod = y % x;

    if (page[0].value == 10 || page[0].value == 12) {
        fnow[0].value = (x * y) + (2 * (y - 1) - (x * (y - 1)));
        up[0].value = Number(fnow[0].value) - 2;
        down[0].value = Number(fnow[0].value) + 2;
        left[0].value = Number(fnow[0].value) - 1;
        right[0].value = Number(fnow[0].value) + 1;
    }
}

$(document).ready(function () {
    $("input:text").focus(function () { $(this).select(); });
});

function checkmax(namemax, valuemax, length, lockvalue, popupvalue) {
    var lockvalue = document.getElementsByClassName(lockvalue);
    var namemax = document.getElementsByClassName(namemax);
    var popupnomax = document.getElementsByClassName("popupnomax");

    var nomax = 0;
    if (Number(namemax[valuemax].value) != 0) {

        var z = 0;
        for (var y = 0; y < length; y++) {

            if (lockvalue[y].value == "")
                z++;
            if (z == length) {
                nomax++;
            }
        }
    }

    return nomax;
}

function nomax() {
    var popupnomax = document.getElementsByClassName("popupnomax");

    if (popupnomax[0].value != "1") {
        popupnomax[0].value = "1";
        bootbox.alert({
            message: '<h2>กรุณากรอกช่องคะแนนเต็มก่อนให้คะแนนนักเรียน</h>',
            backdrop: true
        });
    }
    setTimeout(function () {
        popupnomax[0].value = "0";
    }, 5000);
}

function calllock(id) {
    var txt = 0;
    for (var x = 0; (x < id.length) && (txt == 0); x++) {
        if (id[x].value != "")
            txt = 1;
    }
    return txt;
}

function lockpage() {
    var periodnow = document.getElementsByClassName("periodnow");
    var periodsubmit = document.getElementsByClassName("periodsubmit");
    var lock1 = document.getElementsByClassName("lock1");
    var lock2 = document.getElementsByClassName("lock2");
    var lock3 = document.getElementsByClassName("lock3");
    var lock4 = document.getElementsByClassName("lock4");
    var lockmid = document.getElementsByClassName("lockmid");
    var lockfinal = document.getElementsByClassName("lockfinal");
    var lockg1 = document.getElementsByClassName("lockg1");
    var lockg2 = document.getElementsByClassName("lockg2");
    var lockg3 = document.getElementsByClassName("lockg3");
    var lockg4 = document.getElementsByClassName("lockg4");
    var lockg5 = document.getElementsByClassName("lockg5");
    var lockg6 = document.getElementsByClassName("lockg6");
    var lockg7 = document.getElementsByClassName("lockg7");
    var lockg8 = document.getElementsByClassName("lockg8");
    var lockg9 = document.getElementsByClassName("lockg9");
    var lockg10 = document.getElementsByClassName("lockg10");
    var lockg11 = document.getElementsByClassName("lockg11");
    var lockg12 = document.getElementsByClassName("lockg12");
    var lockg13 = document.getElementsByClassName("lockg13");
    var lockg14 = document.getElementsByClassName("lockg14");
    var lockg15 = document.getElementsByClassName("lockg15");
    var lockg16 = document.getElementsByClassName("lockg16");
    var lockg17 = document.getElementsByClassName("lockg17");
    var lockg18 = document.getElementsByClassName("lockg18");
    var lockg19 = document.getElementsByClassName("lockg19");
    var lockg20 = document.getElementsByClassName("lockg20");
    var lockcw1 = document.getElementsByClassName("lockcw1");
    var lockcw2 = document.getElementsByClassName("lockcw2");
    var lockcw3 = document.getElementsByClassName("lockcw3");
    var lockcw4 = document.getElementsByClassName("lockcw4");
    var lockcw5 = document.getElementsByClassName("lockcw5");
    var lockcw6 = document.getElementsByClassName("lockcw6");
    var lockcw7 = document.getElementsByClassName("lockcw7");
    var lockcw8 = document.getElementsByClassName("lockcw8");
    var lockcw9 = document.getElementsByClassName("lockcw9");
    var lockcw10 = document.getElementsByClassName("lockcw10");
    var lockcw11 = document.getElementsByClassName("lockcw11");
    var lockcw12 = document.getElementsByClassName("lockcw12");
    var lockcw13 = document.getElementsByClassName("lockcw13");
    var lockcw14 = document.getElementsByClassName("lockcw14");
    var lockcw15 = document.getElementsByClassName("lockcw15");
    var lockcw16 = document.getElementsByClassName("lockcw16");
    var lockcw17 = document.getElementsByClassName("lockcw17");
    var lockcw18 = document.getElementsByClassName("lockcw18");
    var lockcw19 = document.getElementsByClassName("lockcw19");
    var lockcw20 = document.getElementsByClassName("lockcw20");
    var lockm1 = document.getElementsByClassName("lockm1");
    var lockm2 = document.getElementsByClassName("lockm2");
    var lockm3 = document.getElementsByClassName("lockm3");
    var lockm4 = document.getElementsByClassName("lockm4");
    var lockm5 = document.getElementsByClassName("lockm5");
    var lockm6 = document.getElementsByClassName("lockm6");
    var lockm7 = document.getElementsByClassName("lockm7");
    var lockm8 = document.getElementsByClassName("lockm8");
    var lockm9 = document.getElementsByClassName("lockm9");
    var lockm10 = document.getElementsByClassName("lockm10");
    var lockf1 = document.getElementsByClassName("lockf1");
    var lockf2 = document.getElementsByClassName("lockf2");
    var lockf3 = document.getElementsByClassName("lockf3");
    var lockf4 = document.getElementsByClassName("lockf4");
    var lockf5 = document.getElementsByClassName("lockf5");
    var lockf6 = document.getElementsByClassName("lockf6");
    var lockf7 = document.getElementsByClassName("lockf7");
    var lockf8 = document.getElementsByClassName("lockf8");
    var lockf9 = document.getElementsByClassName("lockf9");
    var lockf10 = document.getElementsByClassName("lockf10");
    var lockmidterm = document.getElementsByClassName("lockmidterm");
    var lockfinalterm = document.getElementsByClassName("lockfinalterm");
    var maxtest = document.getElementsByClassName("maxtest");
    var maxtestcw = document.getElementsByClassName("maxtestcw");
    var maxmidcw = document.getElementsByClassName("maxmidcw");
    var maxfinalcw = document.getElementsByClassName("maxfinalcw");
    var maxmidscore = document.getElementsByClassName("maxmidscore");
    var maxlatescore = document.getElementsByClassName("maxlatescore");
    var set1name = document.getElementsByClassName("set1name");
    var set2name = document.getElementsByClassName("set2name");
    var set3name = document.getElementsByClassName("set3name");
    var set4name = document.getElementsByClassName("set4name");
    var set5name = document.getElementsByClassName("set5name");
    var set6name = document.getElementsByClassName("set6name");
    var set7name = document.getElementsByClassName("set7name");
    var set8name = document.getElementsByClassName("set8name");
    var set9name = document.getElementsByClassName("set9name");
    var set10name = document.getElementsByClassName("set10name");
    var set11name = document.getElementsByClassName("set11name");
    var set12name = document.getElementsByClassName("set12name");
    var set13name = document.getElementsByClassName("set13name");
    var set14name = document.getElementsByClassName("set14name");
    var set15name = document.getElementsByClassName("set15name");
    var set16name = document.getElementsByClassName("set16name");
    var set17name = document.getElementsByClassName("set17name");
    var set18name = document.getElementsByClassName("set18name");
    var set19name = document.getElementsByClassName("set19name");
    var set20name = document.getElementsByClassName("set20name");
    var set1namecw = document.getElementsByClassName("set1namecw");
    var set2namecw = document.getElementsByClassName("set2namecw");
    var set3namecw = document.getElementsByClassName("set3namecw");
    var set4namecw = document.getElementsByClassName("set4namecw");
    var set5namecw = document.getElementsByClassName("set5namecw");
    var set6namecw = document.getElementsByClassName("set6namecw");
    var set7namecw = document.getElementsByClassName("set7namecw");
    var set8namecw = document.getElementsByClassName("set8namecw");
    var set9namecw = document.getElementsByClassName("set9namecw");
    var set10namecw = document.getElementsByClassName("set10namecw");
    var set11namecw = document.getElementsByClassName("set11namecw");
    var set12namecw = document.getElementsByClassName("set12namecw");
    var set13namecw = document.getElementsByClassName("set13namecw");
    var set14namecw = document.getElementsByClassName("set14namecw");
    var set15namecw = document.getElementsByClassName("set15namecw");
    var set16namecw = document.getElementsByClassName("set16namecw");
    var set17namecw = document.getElementsByClassName("set17namecw");
    var set18namecw = document.getElementsByClassName("set18namecw");
    var set19namecw = document.getElementsByClassName("set19namecw");
    var set20namecw = document.getElementsByClassName("set20namecw");
    var set1midcw = document.getElementsByClassName("set1midcw");
    var set2midcw = document.getElementsByClassName("set2midcw");
    var set3midcw = document.getElementsByClassName("set3midcw");
    var set4midcw = document.getElementsByClassName("set4midcw");
    var set5midcw = document.getElementsByClassName("set5midcw");
    var set6midcw = document.getElementsByClassName("set6midcw");
    var set7midcw = document.getElementsByClassName("set7midcw");
    var set8midcw = document.getElementsByClassName("set8midcw");
    var set9midcw = document.getElementsByClassName("set9midcw");
    var set10midcw = document.getElementsByClassName("set10midcw");
    var set1finalcw = document.getElementsByClassName("set1finalcw");
    var set2finalcw = document.getElementsByClassName("set2finalcw");
    var set3finalcw = document.getElementsByClassName("set3finalcw");
    var set4finalcw = document.getElementsByClassName("set4finalcw");
    var set5finalcw = document.getElementsByClassName("set5finalcw");
    var set6finalcw = document.getElementsByClassName("set6finalcw");
    var set7finalcw = document.getElementsByClassName("set7finalcw");
    var set8finalcw = document.getElementsByClassName("set8finalcw");
    var set9finalcw = document.getElementsByClassName("set9finalcw");
    var set10finalcw = document.getElementsByClassName("set10finalcw");

    var disablemid = document.getElementsByClassName("disablemid");
    var disablefinal = document.getElementsByClassName("disablefinal");
    var disableg1 = document.getElementsByClassName("disableg1");
    var disableg2 = document.getElementsByClassName("disableg2");
    var disableg3 = document.getElementsByClassName("disableg3");
    var disableg4 = document.getElementsByClassName("disableg4");
    var disableg5 = document.getElementsByClassName("disableg5");
    var disableg6 = document.getElementsByClassName("disableg6");
    var disableg7 = document.getElementsByClassName("disableg7");
    var disableg8 = document.getElementsByClassName("disableg8");
    var disableg9 = document.getElementsByClassName("disableg9");
    var disableg10 = document.getElementsByClassName("disableg10");
    var disableg11 = document.getElementsByClassName("disableg11");
    var disableg12 = document.getElementsByClassName("disableg12");
    var disableg13 = document.getElementsByClassName("disableg13");
    var disableg14 = document.getElementsByClassName("disableg14");
    var disableg15 = document.getElementsByClassName("disableg15");
    var disableg16 = document.getElementsByClassName("disableg16");
    var disableg17 = document.getElementsByClassName("disableg17");
    var disableg18 = document.getElementsByClassName("disableg18");
    var disableg19 = document.getElementsByClassName("disableg19");
    var disableg20 = document.getElementsByClassName("disableg20");
    var disablecw1 = document.getElementsByClassName("disablecw1");
    var disablecw2 = document.getElementsByClassName("disablecw2");
    var disablecw3 = document.getElementsByClassName("disablecw3");
    var disablecw4 = document.getElementsByClassName("disablecw4");
    var disablecw5 = document.getElementsByClassName("disablecw5");
    var disablecw6 = document.getElementsByClassName("disablecw6");
    var disablecw7 = document.getElementsByClassName("disablecw7");
    var disablecw8 = document.getElementsByClassName("disablecw8");
    var disablecw9 = document.getElementsByClassName("disablecw9");
    var disablecw10 = document.getElementsByClassName("disablecw10");
    var disablecw11 = document.getElementsByClassName("disablecw11");
    var disablecw12 = document.getElementsByClassName("disablecw12");
    var disablecw13 = document.getElementsByClassName("disablecw13");
    var disablecw14 = document.getElementsByClassName("disablecw14");
    var disablecw15 = document.getElementsByClassName("disablecw15");
    var disablecw16 = document.getElementsByClassName("disablecw16");
    var disablecw17 = document.getElementsByClassName("disablecw17");
    var disablecw18 = document.getElementsByClassName("disablecw18");
    var disablecw19 = document.getElementsByClassName("disablecw19");
    var disablecw20 = document.getElementsByClassName("disablecw20");
    var disablemid1 = document.getElementsByClassName("disablemid1");
    var disablemid2 = document.getElementsByClassName("disablemid2");
    var disablemid3 = document.getElementsByClassName("disablemid3");
    var disablemid4 = document.getElementsByClassName("disablemid4");
    var disablemid5 = document.getElementsByClassName("disablemid5");
    var disablemid6 = document.getElementsByClassName("disablemid6");
    var disablemid7 = document.getElementsByClassName("disablemid7");
    var disablemid8 = document.getElementsByClassName("disablemid8");
    var disablemid9 = document.getElementsByClassName("disablemid9");
    var disablemid10 = document.getElementsByClassName("disablemid10");
    var disablefinal1 = document.getElementsByClassName("disablefinal1");
    var disablefinal2 = document.getElementsByClassName("disablefinal2");
    var disablefinal3 = document.getElementsByClassName("disablefinal3");
    var disablefinal4 = document.getElementsByClassName("disablefinal4");
    var disablefinal5 = document.getElementsByClassName("disablefinal5");
    var disablefinal6 = document.getElementsByClassName("disablefinal6");
    var disablefinal7 = document.getElementsByClassName("disablefinal7");
    var disablefinal8 = document.getElementsByClassName("disablefinal8");
    var disablefinal9 = document.getElementsByClassName("disablefinal9");
    var disablefinal10 = document.getElementsByClassName("disablefinal10");

    if (Number(lock1[0].value) < Number(periodnow[0].value) && lock1[0].value != "") {
        var check = calllock(lockg1);
        if (check == 1 && lock1[0].value == "-1") {
            for (var x = 0; x < lockg1.length; x++) {
                lockg1[x].classList.add('disable');
                disableg1[x].classList.add('disable2');
            }
            maxtest[0].classList.add('disable');
            set1name[0].classList.add('disable');
        }

    }
    if (Number(lock1[1].value) < Number(periodnow[0].value) && lock1[1].value != "") {
        var check = calllock(lockg2);
        if (check == 1 && lock1[1].value == "-1") {
            for (var x = 0; x < lockg2.length; x++) {
                lockg2[x].classList.add('disable');
                disableg2[x].classList.add('disable2');
            }
            maxtest[1].classList.add('disable');
            set2name[0].classList.add('disable');
        }

    }
    if (Number(lock1[2].value) < Number(periodnow[0].value) && lock1[2].value != "") {
        var check = calllock(lockg3);
        if (check == 1 && lock1[2].value == "-1") {
            for (var x = 0; x < lockg3.length; x++) {
                lockg3[x].classList.add('disable');
                disableg3[x].classList.add('disable2');
            }
            maxtest[2].classList.add('disable');
            set3name[0].classList.add('disable');
        }

    }
    if (Number(lock1[3].value) < Number(periodnow[0].value) && lock1[3].value != "") {
        var check = calllock(lockg4);
        if (check == 1 && lock1[3].value == "-1") {
            for (var x = 0; x < lockg4.length; x++) {
                lockg4[x].classList.add('disable');
                disableg4[x].classList.add('disable2');
            }
            maxtest[3].classList.add('disable');
            set4name[0].classList.add('disable');
        }

    }
    if (Number(lock1[4].value) < Number(periodnow[0].value) && lock1[4].value != "") {
        var check = calllock(lockg5);
        if (check == 1 && lock1[4].value == "-1") {
            for (var x = 0; x < lockg5.length; x++) {
                lockg5[x].classList.add('disable');
                disableg5[x].classList.add('disable2');
            }
            maxtest[4].classList.add('disable');
            set5name[0].classList.add('disable');
        }

    }
    if (Number(lock1[5].value) < Number(periodnow[0].value) && lock1[5].value != "") {
        var check = calllock(lockg6);
        if (check == 1 && lock1[5].value == "-1") {
            for (var x = 0; x < lockg6.length; x++) {
                lockg6[x].classList.add('disable');
                disableg6[x].classList.add('disable2');
            }
            maxtest[5].classList.add('disable');
            set6name[0].classList.add('disable');
        }

    }
    if (Number(lock1[6].value) < Number(periodnow[0].value) && lock1[6].value != "") {
        var check = calllock(lockg7);
        if (check == 1 && lock1[6].value == "-1") {
            for (var x = 0; x < lockg7.length; x++) {
                lockg7[x].classList.add('disable');
                disableg7[x].classList.add('disable2');
            }
            maxtest[6].classList.add('disable');
            set7name[0].classList.add('disable');
        }

    }
    if (Number(lock1[7].value) < Number(periodnow[0].value) && lock1[7].value != "") {
        var check = calllock(lockg8);
        if (check == 1 && lock1[7].value == "-1") {
            for (var x = 0; x < lockg8.length; x++) {
                lockg8[x].classList.add('disable');
                disableg8[x].classList.add('disable2');
            }
            maxtest[7].classList.add('disable');
            set8name[0].classList.add('disable');
        }

    }
    if (Number(lock1[8].value) < Number(periodnow[0].value) && lock1[8].value != "") {
        var check = calllock(lockg9);
        if (check == 1 && lock1[8].value == "-1") {
            for (var x = 0; x < lockg9.length; x++) {
                lockg9[x].classList.add('disable');
                disableg9[x].classList.add('disable2');
            }
            maxtest[8].classList.add('disable');
            set9name[0].classList.add('disable');
        }

    }
    if (Number(lock1[9].value) < Number(periodnow[0].value) && lock1[9].value != "") {
        var check = calllock(lockg10);
        if (check == 1 && lock1[9].value == "-1") {
            for (var x = 0; x < lockg10.length; x++) {
                lockg10[x].classList.add('disable');
                disableg10[x].classList.add('disable2');
            }
            maxtest[9].classList.add('disable');
            set10name[0].classList.add('disable');
        }

    }
    if (Number(lock1[10].value) < Number(periodnow[0].value) && lock1[10].value != "") {
        var check = calllock(lockg11);
        if (check == 1 && lock1[10].value == "-1") {
            for (var x = 0; x < lockg11.length; x++) {
                lockg11[x].classList.add('disable');
                disableg11[x].classList.add('disable2');
            }
            maxtest[10].classList.add('disable');
            set11name[0].classList.add('disable');
        }

    }
    if (Number(lock1[11].value) < Number(periodnow[0].value) && lock1[11].value != "") {
        var check = calllock(lockg12);
        if (check == 1 && lock1[11].value == "-1") {
            for (var x = 0; x < lockg12.length; x++) {
                lockg12[x].classList.add('disable');
                disableg12[x].classList.add('disable2');
            }
            maxtest[11].classList.add('disable');
            set12name[0].classList.add('disable');
        }

    }
    if (Number(lock1[12].value) < Number(periodnow[0].value) && lock1[12].value != "") {
        var check = calllock(lockg13);
        if (check == 1 && lock1[12].value == "-1") {
            for (var x = 0; x < lockg13.length; x++) {
                lockg13[x].classList.add('disable');
                disableg13[x].classList.add('disable2');
            }
            maxtest[12].classList.add('disable');
            set13name[0].classList.add('disable');
        }

    }
    if (Number(lock1[13].value) < Number(periodnow[0].value) && lock1[13].value != "") {
        var check = calllock(lockg14);
        if (check == 1 && lock1[13].value == "-1") {
            for (var x = 0; x < lockg14.length; x++) {
                lockg14[x].classList.add('disable');
                disableg14[x].classList.add('disable2');
            }
            maxtest[13].classList.add('disable');
            set14name[0].classList.add('disable');
        }

    }
    if (Number(lock1[14].value) < Number(periodnow[0].value) && lock1[14].value != "") {
        var check = calllock(lockg15);
        if (check == 1 && lock1[14].value == "-1") {
            for (var x = 0; x < lockg15.length; x++) {
                lockg15[x].classList.add('disable');
                disableg15[x].classList.add('disable2');
            }
            maxtest[14].classList.add('disable');
            set15name[0].classList.add('disable');
        }

    }
    if (Number(lock1[15].value) < Number(periodnow[0].value) && lock1[15].value != "") {
        var check = calllock(lockg16);
        if (check == 1 && lock1[15].value == "-1") {
            for (var x = 0; x < lockg16.length; x++) {
                lockg16[x].classList.add('disable');
                disableg16[x].classList.add('disable2');
            }
            maxtest[15].classList.add('disable');
            set16name[0].classList.add('disable');
        }

    }
    if (Number(lock1[16].value) < Number(periodnow[0].value) && lock1[16].value != "") {
        var check = calllock(lockg17);
        if (check == 1 && lock1[16].value == "-1") {
            for (var x = 0; x < lockg17.length; x++) {
                lockg17[x].classList.add('disable');
                disableg17[x].classList.add('disable2');
            }
            maxtest[16].classList.add('disable');
            set17name[0].classList.add('disable');
        }

    }
    if (Number(lock1[17].value) < Number(periodnow[0].value) && lock1[17].value != "") {
        var check = calllock(lockg18);
        if (check == 1 && lock1[17].value == "-1") {
            for (var x = 0; x < lockg18.length; x++) {
                lockg18[x].classList.add('disable');
                disableg18[x].classList.add('disable2');
            }
            maxtest[17].classList.add('disable');
            set18name[0].classList.add('disable');
        }

    }
    if (Number(lock1[18].value) < Number(periodnow[0].value) && lock1[18].value != "") {
        var check = calllock(lockg19);
        if (check == 1 && lock1[18].value == "-1") {
            for (var x = 0; x < lockg19.length; x++) {
                lockg19[x].classList.add('disable');
                disableg19[x].classList.add('disable2');
            }
            maxtest[18].classList.add('disable');
            set19name[0].classList.add('disable');
        }

    }
    if (Number(lock1[19].value) < Number(periodnow[0].value) && lock1[19].value != "") {
        var check = calllock(lockg20);
        if (check == 1 && lock1[19].value == "-1") {
            for (var x = 0; x < lockg20.length; x++) {
                lockg20[x].classList.add('disable');
                disableg20[x].classList.add('disable2');
            }
            maxtest[19].classList.add('disable');
            set20name[0].classList.add('disable');
        }

    }


    if (Number(lock2[0].value) < Number(periodnow[0].value) && lock2[0].value != "") {
        var check = calllock(lockcw1);
        if (check == 1 && lock2[0].value == "-1") {
            for (var x = 0; x < lockcw1.length; x++) {
                lockcw1[x].classList.add('disable');
                disablecw1[x].classList.add('disable2');
            }
            maxtestcw[0].classList.add('disable');
            set1namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[1].value) < Number(periodnow[0].value) && lock2[1].value != "") {
        var check = calllock(lockcw2);
        if (check == 1 && lock2[1].value == "-1") {
            for (var x = 0; x < lockcw2.length; x++) {
                lockcw2[x].classList.add('disable');
                disablecw2[x].classList.add('disable2');
            }
            maxtestcw[1].classList.add('disable');
            set2namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[2].value) < Number(periodnow[0].value) && lock2[2].value != "") {
        var check = calllock(lockcw3);
        if (check == 1 && lock2[2].value == "-1") {
            for (var x = 0; x < lockcw3.length; x++) {
                lockcw3[x].classList.add('disable');
                disablecw3[x].classList.add('disable2');
            }
            maxtestcw[2].classList.add('disable');
            set3namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[3].value) < Number(periodnow[0].value) && lock2[3].value != "") {
        var check = calllock(lockcw4);
        if (check == 1 && lock2[3].value == "-1") {
            for (var x = 0; x < lockcw4.length; x++) {
                lockcw4[x].classList.add('disable');
                disablecw4[x].classList.add('disable2');
            }
            maxtestcw[3].classList.add('disable');
            set4namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[4].value) < Number(periodnow[0].value) && lock2[4].value != "") {
        var check = calllock(lockcw5);
        if (check == 1 && lock2[4].value == "-1") {
            for (var x = 0; x < lockcw5.length; x++) {
                lockcw5[x].classList.add('disable');
                disablecw5[x].classList.add('disable2');
            }
            maxtestcw[4].classList.add('disable');
            set5namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[5].value) < Number(periodnow[0].value) && lock2[5].value != "") {
        var check = calllock(lockcw6);
        if (check == 1 && lock2[5].value == "-1") {
            for (var x = 0; x < lockcw6.length; x++) {
                lockcw6[x].classList.add('disable');
                disablecw6[x].classList.add('disable2');
            }
            maxtestcw[5].classList.add('disable');
            set6namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[6].value) < Number(periodnow[0].value) && lock2[6].value != "") {
        var check = calllock(lockcw7);
        if (check == 1 && lock2[6].value == "-1") {
            for (var x = 0; x < lockcw7.length; x++) {
                lockcw7[x].classList.add('disable');
                disablecw7[x].classList.add('disable2');
            }
            maxtestcw[6].classList.add('disable');
            set7namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[7].value) < Number(periodnow[0].value) && lock2[7].value != "") {
        var check = calllock(lockcw8);
        if (check == 1 && lock2[7].value == "-1") {
            for (var x = 0; x < lockcw8.length; x++) {
                lockcw8[x].classList.add('disable');
                disablecw8[x].classList.add('disable2');
            }
            maxtestcw[7].classList.add('disable');
            set8namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[8].value) < Number(periodnow[0].value) && lock2[8].value != "") {
        var check = calllock(lockcw9);
        if (check == 1 && lock2[8].value == "-1") {
            for (var x = 0; x < lockcw9.length; x++) {
                lockcw9[x].classList.add('disable');
                disablecw9[x].classList.add('disable2');
            }
            maxtestcw[8].classList.add('disable');
            set9namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[9].value) < Number(periodnow[0].value) && lock2[9].value != "") {
        var check = calllock(lockcw10);
        if (check == 1 && lock2[9].value == "-1") {
            for (var x = 0; x < lockcw10.length; x++) {
                lockcw10[x].classList.add('disable');
                disablecw10[x].classList.add('disable2');
            }
            maxtestcw[9].classList.add('disable');
            set10namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[10].value) < Number(periodnow[0].value) && lock2[10].value != "") {
        var check = calllock(lockcw11);
        if (check == 1 && lock2[10].value == "-1") {
            for (var x = 0; x < lockcw11.length; x++) {
                lockcw11[x].classList.add('disable');
                disablecw11[x].classList.add('disable2');
            }
            maxtestcw[10].classList.add('disable');
            set11namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[11].value) < Number(periodnow[0].value) && lock2[11].value != "") {
        var check = calllock(lockcw12);
        if (check == 1 && lock2[11].value == "-1") {
            for (var x = 0; x < lockcw12.length; x++) {
                lockcw12[x].classList.add('disable');
                disablecw12[x].classList.add('disable2');
            }
            maxtestcw[11].classList.add('disable');
            set12namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[12].value) < Number(periodnow[0].value) && lock2[12].value != "") {
        var check = calllock(lockcw13);
        if (check == 1 && lock2[12].value == "-1") {
            for (var x = 0; x < lockcw13.length; x++) {
                lockcw13[x].classList.add('disable');
                disablecw13[x].classList.add('disable2');
            }
            maxtestcw[12].classList.add('disable');
            set13namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[13].value) < Number(periodnow[0].value) && lock2[13].value != "") {
        var check = calllock(lockcw14);
        if (check == 1 && lock2[13].value == "-1") {
            for (var x = 0; x < lockcw14.length; x++) {
                lockcw14[x].classList.add('disable');
                disablecw14[x].classList.add('disable2');
            }
            maxtestcw[13].classList.add('disable');
            set14namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[14].value) < Number(periodnow[0].value) && lock2[14].value != "") {
        var check = calllock(lockcw15);
        if (check == 1 && lock2[14].value == "-1") {
            for (var x = 0; x < lockcw15.length; x++) {
                lockcw15[x].classList.add('disable');
                disablecw15[x].classList.add('disable2');
            }
            maxtestcw[14].classList.add('disable');
            set15namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[15].value) < Number(periodnow[0].value) && lock2[15].value != "") {
        var check = calllock(lockcw16);
        if (check == 1 && lock2[15].value == "-1") {
            for (var x = 0; x < lockcw16.length; x++) {
                lockcw16[x].classList.add('disable');
                disablecw16[x].classList.add('disable2');
            }
            maxtestcw[15].classList.add('disable');
            set16namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[16].value) < Number(periodnow[0].value) && lock2[16].value != "") {
        var check = calllock(lockcw17);
        if (check == 1 && lock2[16].value == "-1") {
            for (var x = 0; x < lockcw17.length; x++) {
                lockcw17[x].classList.add('disable');
                disablecw17[x].classList.add('disable2');
            }
            maxtestcw[16].classList.add('disable');
            set17namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[17].value) < Number(periodnow[0].value) && lock2[17].value != "") {
        var check = calllock(lockcw18);
        if (check == 1 && lock2[17].value == "-1") {
            for (var x = 0; x < lockcw18.length; x++) {
                lockcw18[x].classList.add('disable');
                disablecw18[x].classList.add('disable2');
            }
            maxtestcw[17].classList.add('disable');
            set18namecw[0].classList.add('disable');
        }

    }
    if (Number(lock2[18].value) < Number(periodnow[0].value) && lock2[18].value != "") {
        var check = calllock(lockcw19);
        if (check == 1 && lock2[18].value == "-1") {
            for (var x = 0; x < lockcw19.length; x++) {
                lockcw19[x].classList.add('disable');
                disablecw19[x].classList.add('disable2');
            }
            maxtestcw[18].classList.add('disable');
            set19namecw[0].classList.add('disable');
        }

    }

    if (Number(lock2[19].value) < Number(periodnow[0].value) && lock2[19].value != "") {
        var check = calllock(lockcw20);
        if (check == 1 && lock2[19].value == "-1") {
            for (var x = 0; x < lockcw20.length; x++) {
                lockcw20[x].classList.add('disable');
                disablecw20[x].classList.add('disable2');
            }
            maxtestcw[19].classList.add('disable');
            set20namecw[0].classList.add('disable');
        }

    }

    if (Number(lock3[0].value) < Number(periodnow[0].value) && lock3[0].value != "") {
        var check = calllock(lockm1);
        if (check == 1 && lock3[0].value == "-1") {
            for (var x = 0; x < lockm1.length; x++) {
                lockm1[x].classList.add('disable');
                disablemid1[x].classList.add('disable2');
            }
            maxmidcw[0].classList.add('disable');
            set1midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[1].value) < Number(periodnow[0].value) && lock3[1].value != "") {
        var check = calllock(lockm2);
        if (check == 1 && lock3[1].value == "-1") {
            for (var x = 0; x < lockm2.length; x++) {
                lockm2[x].classList.add('disable');
                disablemid2[x].classList.add('disable2');
            }
            maxmidcw[1].classList.add('disable');
            set2midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[2].value) < Number(periodnow[0].value) && lock3[2].value != "") {
        var check = calllock(lockm3);
        if (check == 1 && lock3[2].value == "-1") {
            for (var x = 0; x < lockm3.length; x++) {
                lockm3[x].classList.add('disable');
                disablemid3[x].classList.add('disable2');
            }
            maxmidcw[2].classList.add('disable');
            set3midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[3].value) < Number(periodnow[0].value) && lock3[3].value != "") {
        var check = calllock(lockm4);
        if (check == 1 && lock3[3].value == "-1") {
            for (var x = 0; x < lockm4.length; x++) {
                lockm4[x].classList.add('disable');
                disablemid4[x].classList.add('disable2');
            }
            maxmidcw[3].classList.add('disable');
            set4midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[4].value) < Number(periodnow[0].value) && lock3[4].value != "") {
        var check = calllock(lockm5);
        if (check == 1 && lock3[4].value == "-1") {
            for (var x = 0; x < lockm5.length; x++) {
                lockm5[x].classList.add('disable');
                disablemid5[x].classList.add('disable2');
            }
            maxmidcw[4].classList.add('disable');
            set5midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[5].value) < Number(periodnow[0].value) && lock3[5].value != "") {
        var check = calllock(lockm6);
        if (check == 1 && lock3[5].value == "-1") {
            for (var x = 0; x < lockm6.length; x++) {
                lockm6[x].classList.add('disable');
                disablemid6[x].classList.add('disable2');
            }
            maxmidcw[5].classList.add('disable');
            set6midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[6].value) < Number(periodnow[0].value) && lock3[6].value != "") {
        var check = calllock(lockm7);
        if (check == 1 && lock3[6].value == "-1") {
            for (var x = 0; x < lockm7.length; x++) {
                lockm7[x].classList.add('disable');
                disablemid7[x].classList.add('disable2');
            }
            maxmidcw[6].classList.add('disable');
            set7midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[7].value) < Number(periodnow[0].value) && lock3[7].value != "") {
        var check = calllock(lockm8);
        if (check == 1 && lock3[7].value == "-1") {
            for (var x = 0; x < lockm8.length; x++) {
                lockm8[x].classList.add('disable');
                disablemid8[x].classList.add('disable2');
            }
            maxmidcw[7].classList.add('disable');
            set8midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[8].value) < Number(periodnow[0].value) && lock3[8].value != "") {
        var check = calllock(lockm9);
        if (check == 1 && lock3[8].value == "-1") {
            for (var x = 0; x < lockm9.length; x++) {
                lockm9[x].classList.add('disable');
                disablemid9[x].classList.add('disable2');
            }
            maxmidcw[8].classList.add('disable');
            set9midcw[0].classList.add('disable');
        }

    }
    if (Number(lock3[9].value) < Number(periodnow[0].value) && lock3[9].value != "") {
        var check = calllock(lockm10);
        if (check == 1 && lock3[9].value == "-1") {
            for (var x = 0; x < lockm10.length; x++) {
                lockm10[x].classList.add('disable');
                disablemid10[x].classList.add('disable2');
            }
            maxmidcw[9].classList.add('disable');
            set10midcw[0].classList.add('disable');
        }

    }

    if (Number(lock4[0].value) < Number(periodnow[0].value) && lock4[0].value != "") {
        var check = calllock(lockf1);
        if (check == 1 && lock4[0].value == "-1") {
            for (var x = 0; x < lockf1.length; x++) {
                lockf1[x].classList.add('disable');
                disablefinal1[x].classList.add('disable2');
            }
            maxfinalcw[0].classList.add('disable');
            set1finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[1].value) < Number(periodnow[0].value) && lock4[1].value != "") {
        var check = calllock(lockf2);
        if (check == 1 && lock4[1].value == "-1") {
            for (var x = 0; x < lockf2.length; x++) {
                lockf2[x].classList.add('disable');
                disablefinal2[x].classList.add('disable2');
            }
            maxfinalcw[1].classList.add('disable');
            set2finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[2].value) < Number(periodnow[0].value) && lock4[2].value != "") {
        var check = calllock(lockf3);
        if (check == 1 && lock4[2].value == "-1") {
            for (var x = 0; x < lockf3.length; x++) {
                lockf3[x].classList.add('disable');
                disablefinal3[x].classList.add('disable2');
            }
            maxfinalcw[2].classList.add('disable');
            set3finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[3].value) < Number(periodnow[0].value) && lock4[3].value != "") {
        var check = calllock(lockf4);
        if (check == 1 && lock4[3].value == "-1") {
            for (var x = 0; x < lockf4.length; x++) {
                lockf4[x].classList.add('disable');
                disablefinal4[x].classList.add('disable2');
            }
            maxfinalcw[3].classList.add('disable');
            set4finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[4].value) < Number(periodnow[0].value) && lock4[4].value != "") {
        var check = calllock(lockf5);
        if (check == 1 && lock4[4].value == "-1") {
            for (var x = 0; x < lockf5.length; x++) {
                lockf5[x].classList.add('disable');
                disablefinal5[x].classList.add('disable2');
            }
            maxfinalcw[4].classList.add('disable');
            set5finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[5].value) < Number(periodnow[0].value) && lock4[5].value != "") {
        var check = calllock(lockf6);
        if (check == 1 && lock4[5].value == "-1") {
            for (var x = 0; x < lockf6.length; x++) {
                lockf6[x].classList.add('disable');
                disablefinal6[x].classList.add('disable2');
            }
            maxfinalcw[5].classList.add('disable');
            set6finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[6].value) < Number(periodnow[0].value) && lock4[6].value != "") {
        var check = calllock(lockf7);
        if (check == 1 && lock4[6].value == "-1") {
            for (var x = 0; x < lockf7.length; x++) {
                lockf7[x].classList.add('disable');
                disablefinal7[x].classList.add('disable2');
            }
            maxfinalcw[6].classList.add('disable');
            set7finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[7].value) < Number(periodnow[0].value) && lock4[7].value != "") {
        var check = calllock(lockf8);
        if (check == 1 && lock4[7].value == "-1") {
            for (var x = 0; x < lockf8.length; x++) {
                lockf8[x].classList.add('disable');
                disablefinal8[x].classList.add('disable2');
            }
            maxfinalcw[7].classList.add('disable');
            set8finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[8].value) < Number(periodnow[0].value) && lock4[8].value != "") {
        var check = calllock(lockf9);
        if (check == 1 && lock4[8].value == "-1") {
            for (var x = 0; x < lockf9.length; x++) {
                lockf9[x].classList.add('disable');
                disablefinal9[x].classList.add('disable2');
            }
            maxfinalcw[8].classList.add('disable');
            set9finalcw[0].classList.add('disable');
        }

    }
    if (Number(lock4[9].value) < Number(periodnow[0].value) && lock4[9].value != "") {
        var check = calllock(lockf10);
        if (check == 1 && lock4[9].value == "-1") {
            for (var x = 0; x < lockf10.length; x++) {
                lockf10[x].classList.add('disable');
                disablefinal10[x].classList.add('disable2');
            }
            maxfinalcw[9].classList.add('disable');
            set10finalcw[0].classList.add('disable');
        }

    }

    if (Number(lockmid[0].value) < Number(periodnow[0].value) && lockmid[0].value != "") {
        var check = calllock(lockmidterm);
        if (check == 1 && lockmid[0].value == "-1") {
            for (var x = 0; x < lockmidterm.length; x++) {
                lockmidterm[x].classList.add('disable');
                disablemid[x].classList.add('disable2');
            }
            maxmidscore[0].classList.add('disable');
        }

    }
    if (Number(lockfinal[0].value) < Number(periodnow[0].value) && lockfinal[0].value != "") {
        var check = calllock(lockfinalterm);
        if (check == 1 && lockfinal[0].value == "-1") {
            for (var x = 0; x < lockfinalterm.length; x++) {
                lockfinalterm[x].classList.add('disable');
                disablefinal[x].classList.add('disable2');
            }
            maxlatescore[0].classList.add('disable');
        }

    }

}

function print(id) {


    var full = window.location.href;
    var half = full.split('?');
    var url = full.split('.aspx');
    var split = half[1].split('&');
    var year = split[0].split('=');
    var idlv = split[1];
    var idlv2 = split[2];
    var term = split[3].split('=');
    var id2 = split[4].split('=');
    var mode = split[5].split('=');

    var loc = "";

    if (id == "1") {
        loc = url[0] + "iFrame.aspx?" + half[1] + "&print=1";
        document.getElementById('list1').src = loc;
    }
    if (id == "2") {
        loc = url[0] + "iFrame.aspx?" + half[1] + "&print=2";
        document.getElementById('list2').src = loc;
    }
    if (id == "3") {
        loc = url[0] + "iFrame.aspx?" + half[1] + "&print=3";
        document.getElementById('list3').src = loc;
    }
    if (id == "4") {
        loc = url[0] + "iFrame.aspx?" + half[1] + "&print=4";
        document.getElementById('list4').src = loc;
    }
    if (id == "5") {
        loc = url[0] + "iFrame.aspx?" + half[1] + "&print=5";
        document.getElementById('list5').src = loc;
    }


}

function keyUpdate(keyEvent, down) {
    var textBoxesg1 = document.getElementsByClassName("AutoCompleteTextBox");
    var textBoxesg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
    var textBoxesg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
    var textBoxesg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
    var cw1 = document.getElementsByClassName("chewut1");
    var cw2 = document.getElementsByClassName("chewut2");
    var cw3 = document.getElementsByClassName("chewut3");
    var cw4 = document.getElementsByClassName("chewut4");
    var bh1 = document.getElementsByClassName("behavepage1");
    var bh2 = document.getElementsByClassName("behavepage2");
    var textBoxes2 = document.getElementsByClassName("AutoCompleteTextBox2");
    var up = document.getElementsByClassName("fup");
    var page = document.getElementsByClassName("fpage");
    var down = document.getElementsByClassName("fdown");
    var left = document.getElementsByClassName("fleft");
    var right = document.getElementsByClassName("fright");
    var page12 = document.getElementsByClassName("page12");

    var p = page[0].value;
    var up2 = up[0].value;
    var down2 = down[0].value;
    var left2 = left[0].value;
    var right2 = right[0].value;
    // down is a boolean, whether the key event is keydown (true) or keyup (false)
    keyEvent.preventDefault(); // prevent screen from going crazy while i press keys.
    console.log(keyEvent.keyCode)
    switch (keyEvent.keyCode) {

        case 38:  // up key.
            if (p == 0) textBoxesg1[up2].focus();
            else if (p == 1) textBoxesg2[up2].focus();
            else if (p == 2) textBoxesg3[up2].focus();
            else if (p == 3) textBoxesg4[up2].focus();
            else if (p == 6) cw1[up2].focus();
            else if (p == 7) cw2[up2].focus();
            else if (p == 8) cw3[up2].focus();
            else if (p == 9) cw4[up2].focus();
            else if (p == 4) bh1[up2].focus();
            else if (p == 5) bh2[up2].focus();
            else if (p == 10) textBoxes2[up2].focus();
            else if (p == 12) page12[up2].focus();
            break;

        case 40: // down key
            if (p == 0) textBoxesg1[down2].focus();
            else if (p == 1) textBoxesg2[down2].focus();
            else if (p == 2) textBoxesg3[down2].focus();
            else if (p == 3) textBoxesg4[down2].focus();
            else if (p == 6) cw1[down2].focus();
            else if (p == 7) cw2[down2].focus();
            else if (p == 8) cw3[down2].focus();
            else if (p == 9) cw4[down2].focus();
            else if (p == 4) bh1[down2].focus();
            else if (p == 5) bh2[down2].focus();
            else if (p == 10) textBoxes2[down2].focus();
            else if (p == 12) page12[down2].focus();
            break;

        case 37: // left arrow.
            if (p == 0) textBoxesg1[left2].focus();
            else if (p == 1) textBoxesg2[left2].focus();
            else if (p == 2) textBoxesg3[left2].focus();
            else if (p == 3) textBoxesg4[left2].focus();
            else if (p == 6) cw1[left2].focus();
            else if (p == 7) cw2[left2].focus();
            else if (p == 8) cw3[left2].focus();
            else if (p == 9) cw4[left2].focus();
            else if (p == 4) bh1[left2].focus();
            else if (p == 5) bh2[left2].focus();
            else if (p == 10) textBoxes2[left2].focus();
            else if (p == 12) page12[left2].focus();
            break;

        case 39: // right arrow.
            if (p == 0) textBoxesg1[right2].focus();
            else if (p == 1) textBoxesg2[right2].focus();
            else if (p == 2) textBoxesg3[right2].focus();
            else if (p == 3) textBoxesg4[right2].focus();
            else if (p == 6) cw1[right2].focus();
            else if (p == 7) cw2[right2].focus();
            else if (p == 8) cw3[right2].focus();
            else if (p == 9) cw4[right2].focus();
            else if (p == 4) bh1[right2].focus();
            else if (p == 5) bh2[right2].focus();
            else if (p == 10) textBoxes2[right2].focus();
            else if (p == 12) page12[right2].focus();
            break;


    }

}

document.addEventListener("keydown", function (event) {

    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40)
        keyUpdate(event, false);
});

function changebutton(id) {
    var btnok = document.getElementsByClassName("btnok");
    var btnerror = document.getElementsByClassName("btnerror");
    var viplogin = document.getElementsByClassName("viplogin");

    if (viplogin[0].value != "2") {
        if (id == "9999") {
            btnok[0].classList.remove('hidden');
            btnerror[0].classList.add('hidden');
        }
        else {
            //btnok[0].classList.add('hidden');
            //btnerror[0].classList.remove('hidden');
        }
    }

}

function autobehave() {
    changebutton(9999);
    var maxtestb1 = document.getElementsByClassName("maxtestb1");
    var behavetxt1 = document.getElementsByClassName("behavetxt1");
    var behavetxt2 = document.getElementsByClassName("behavetxt2");
    var behavetxt3 = document.getElementsByClassName("behavetxt3");
    var behavetxt4 = document.getElementsByClassName("behavetxt4");
    var behavetxt5 = document.getElementsByClassName("behavetxt5");
    var behavetxt6 = document.getElementsByClassName("behavetxt6");
    var behavetxt7 = document.getElementsByClassName("behavetxt7");
    var behavetxt8 = document.getElementsByClassName("behavetxt8");
    var behavetxt9 = document.getElementsByClassName("behavetxt9");
    var behavetxt10 = document.getElementsByClassName("behavetxt10");
    var goodbe = document.getElementsByClassName("goodbe");
    var behavedisable = document.getElementsByClassName("behavedisable");

    var maxb1 = maxtestb1[0].value;
    var maxb2 = maxtestb1[1].value;
    var maxb3 = maxtestb1[2].value;
    var maxb4 = maxtestb1[3].value;
    var maxb5 = maxtestb1[4].value;
    var maxb6 = maxtestb1[5].value;
    var maxb7 = maxtestb1[6].value;
    var maxb8 = maxtestb1[7].value;
    var maxb9 = maxtestb1[8].value;
    var maxb10 = maxtestb1[9].value;

    for (var i = 0; i < behavetxt1.length; i++) {
        if (Number(behavetxt1[i].value) > Number(maxb1)) {
            changebutton(i);
            behavetxt1[i].classList.add('txtglow');
        }
        if (Number(behavetxt1[i].value) <= Number(maxb1)) {
            behavetxt1[i].classList.remove('txtglow');
        }
        if (Number(behavetxt2[i].value) > Number(maxb2)) {
            changebutton(i);
            behavetxt2[i].classList.add('txtglow');
        }
        if (Number(behavetxt2[i].value) <= Number(maxb2)) {
            behavetxt2[i].classList.remove('txtglow');
        }
        if (Number(behavetxt3[i].value) > Number(maxb3)) {
            changebutton(i);
            behavetxt3[i].classList.add('txtglow');
        }
        if (Number(behavetxt3[i].value) <= Number(maxb3)) {
            behavetxt3[i].classList.remove('txtglow');
        }
        if (Number(behavetxt4[i].value) > Number(maxb4)) {
            changebutton(i);
            behavetxt4[i].classList.add('txtglow');
        }
        if (Number(behavetxt4[i].value) <= Number(maxb4)) {
            behavetxt4[i].classList.remove('txtglow');
        }
        if (Number(behavetxt5[i].value) > Number(maxb5)) {
            changebutton(i);
            behavetxt5[i].classList.add('txtglow');
        }
        if (Number(behavetxt5[i].value) <= Number(maxb5)) {
            behavetxt5[i].classList.remove('txtglow');
        }
        if (Number(behavetxt6[i].value) > Number(maxb6)) {
            changebutton(i);
            behavetxt6[i].classList.add('txtglow');
        }
        if (Number(behavetxt6[i].value) <= Number(maxb6)) {
            behavetxt6[i].classList.remove('txtglow');
        }
        if (Number(behavetxt7[i].value) > Number(maxb7)) {
            changebutton(i);
            behavetxt7[i].classList.add('txtglow');
        }
        if (Number(behavetxt7[i].value) <= Number(maxb7)) {
            behavetxt7[i].classList.remove('txtglow');
        }
        if (Number(behavetxt8[i].value) > Number(maxb8)) {
            changebutton(i);
            behavetxt8[i].classList.add('txtglow');
        }
        if (Number(behavetxt8[i].value) <= Number(maxb8)) {
            behavetxt8[i].classList.remove('txtglow');
        }
        if (Number(behavetxt9[i].value) > Number(maxb9)) {
            changebutton(i);
            behavetxt9[i].classList.add('txtglow');
        }
        if (Number(behavetxt9[i].value) <= Number(maxb9)) {
            behavetxt9[i].classList.remove('txtglow');
        }
        if (Number(behavetxt10[i].value) > Number(maxb10)) {
            changebutton(i);
            behavetxt10[i].classList.add('txtglow');
        }
        if (Number(behavetxt10[i].value) <= Number(maxb10)) {
            behavetxt10[i].classList.remove('txtglow');
        }
    }

    if (behavedisable[0].value == 1) {
        for (var i = 0; i < behavetxt1.length; i++) {
            var array = [];

            if (behavetxt1[i].value != "") array.push(behavetxt1[i].value);
            if (behavetxt2[i].value != "") array.push(behavetxt2[i].value);
            if (behavetxt3[i].value != "") array.push(behavetxt3[i].value);
            if (behavetxt4[i].value != "") array.push(behavetxt4[i].value);
            if (behavetxt5[i].value != "") array.push(behavetxt5[i].value);
            if (behavetxt6[i].value != "") array.push(behavetxt6[i].value);
            if (behavetxt7[i].value != "") array.push(behavetxt7[i].value);
            if (behavetxt8[i].value != "") array.push(behavetxt8[i].value);
            if (behavetxt9[i].value != "") array.push(behavetxt9[i].value);
            if (behavetxt10[i].value != "") array.push(behavetxt10[i].value);

            if (array.length > 1)
                goodbe[i].value = modeString(array);
            else if (array.length == 1) goodbe[i].value = array[0];
            else goodbe[i].value = "";

        }
    }

}

function cleartext(id) {

    var goodbe = document.getElementsByClassName("goodbe");
    var readscore = document.getElementsByClassName("readscore");
    var behavetxt1 = document.getElementsByClassName("behavetxt1");
    var behavetxt2 = document.getElementsByClassName("behavetxt2");
    var behavetxt3 = document.getElementsByClassName("behavetxt3");
    var behavetxt4 = document.getElementsByClassName("behavetxt4");
    var behavetxt5 = document.getElementsByClassName("behavetxt5");
    var behavetxt6 = document.getElementsByClassName("behavetxt6");
    var behavetxt7 = document.getElementsByClassName("behavetxt7");
    var behavetxt8 = document.getElementsByClassName("behavetxt8");
    var behavetxt9 = document.getElementsByClassName("behavetxt9");
    var behavetxt10 = document.getElementsByClassName("behavetxt10");
    var behavesid = document.getElementsByClassName("behavesid");
    var samatscore = document.getElementsByClassName("samatscore");
    if (id == "1") {
        for (var i = 0; i < goodbe.length; i++) {
            goodbe[i].value = "";
        }
        for (var i = 0; i < behavesid.length; i++) {
            goodbe[i].value = "";
            behavetxt1[i].value = "";
            behavetxt2[i].value = "";
            behavetxt3[i].value = "";
            behavetxt4[i].value = "";
            behavetxt5[i].value = "";
            behavetxt6[i].value = "";
            behavetxt7[i].value = "";
            behavetxt8[i].value = "";
            behavetxt9[i].value = "";
            behavetxt10[i].value = "";
        }
    }
    else if (id == "2") {
        for (var i = 0; i < readscore.length; i++) {
            readscore[i].value = "";
        }
    }
    else if (id == "3") {
        for (var i = 0; i < readscore.length; i++) {
            samatscore[i].value = "";
        }
    }


}

function calmaxmid() {

    var maxmidcw = document.getElementsByClassName("maxmidcw");
    var maxmidscore = document.getElementsByClassName("maxmidscore");

    maxmidscore[0].value =
        Number(maxmidcw[0].value) + Number(maxmidcw[1].value) +
        Number(maxmidcw[2].value) + Number(maxmidcw[3].value) +
        Number(maxmidcw[4].value) + Number(maxmidcw[5].value) +
        Number(maxmidcw[6].value) + Number(maxmidcw[7].value) +
        Number(maxmidcw[8].value) + Number(maxmidcw[9].value);


}

function calmaxfinal() {

    var maxfinalcw = document.getElementsByClassName("maxfinalcw");
    var maxfinalscore = document.getElementsByClassName("maxlatescore");

    maxfinalscore[0].value =
        Number(maxfinalcw[0].value) + Number(maxfinalcw[1].value) +
        Number(maxfinalcw[2].value) + Number(maxfinalcw[3].value) +
        Number(maxfinalcw[4].value) + Number(maxfinalcw[5].value) +
        Number(maxfinalcw[6].value) + Number(maxfinalcw[7].value) +
        Number(maxfinalcw[8].value) + Number(maxfinalcw[9].value);
}

function calmidscore(index) {

    index = index - 1;

    var midscore1 = document.getElementsByClassName("midscore1");
    var midscore2 = document.getElementsByClassName("midscore2");
    var midscore3 = document.getElementsByClassName("midscore3");
    var midscore4 = document.getElementsByClassName("midscore4");
    var midscore5 = document.getElementsByClassName("midscore5");
    var midscore6 = document.getElementsByClassName("midscore6");
    var midscore7 = document.getElementsByClassName("midscore7");
    var midscore8 = document.getElementsByClassName("midscore8");
    var midscore9 = document.getElementsByClassName("midscore9");
    var midscore10 = document.getElementsByClassName("midscore10");
    var summidbox = document.getElementsByClassName("summidbox");

    summidbox[index].value =
        Number(midscore1[index].value) + Number(midscore6[index].value) +
        Number(midscore2[index].value) + Number(midscore7[index].value) +
        Number(midscore3[index].value) + Number(midscore8[index].value) +
        Number(midscore4[index].value) + Number(midscore9[index].value) +
        Number(midscore5[index].value) + Number(midscore10[index].value);
    index = index + 1;
    CompareDates(index);
}

function calfinalscore(index) {

    index = index - 1;

    var finalscore1 = document.getElementsByClassName("finalscore1");
    var finalscore2 = document.getElementsByClassName("finalscore2");
    var finalscore3 = document.getElementsByClassName("finalscore3");
    var finalscore4 = document.getElementsByClassName("finalscore4");
    var finalscore5 = document.getElementsByClassName("finalscore5");
    var finalscore6 = document.getElementsByClassName("finalscore6");
    var finalscore7 = document.getElementsByClassName("finalscore7");
    var finalscore8 = document.getElementsByClassName("finalscore8");
    var finalscore9 = document.getElementsByClassName("finalscore9");
    var finalscore10 = document.getElementsByClassName("finalscore10");
    var sumfinalbox = document.getElementsByClassName("sumfinalbox");

    sumfinalbox[index].value =
        Number(finalscore1[index].value) + Number(finalscore6[index].value) +
        Number(finalscore2[index].value) + Number(finalscore7[index].value) +
        Number(finalscore3[index].value) + Number(finalscore8[index].value) +
        Number(finalscore4[index].value) + Number(finalscore9[index].value) +
        Number(finalscore5[index].value) + Number(finalscore10[index].value);
    index = index + 1;
    CompareDates(index);
}

function nextbutton(id) {
   
    //var left1 = document.getElementsByClassName("left1");
    //var left2 = document.getElementsByClassName("left2");
    //var left3 = document.getElementsByClassName("left3");
    //var left4 = document.getElementsByClassName("left4");
    //var left5 = document.getElementsByClassName("left5");
    //var left6 = document.getElementsByClassName("left6");
    //var left7 = document.getElementsByClassName("left7");
    //var left8 = document.getElementsByClassName("left8");
    //var left9 = document.getElementsByClassName("left9");
    //var right1 = document.getElementsByClassName("right1");
    //var right2 = document.getElementsByClassName("right2");
    //var right3 = document.getElementsByClassName("right3");
    //var right4 = document.getElementsByClassName("right4");
    //var right5 = document.getElementsByClassName("right5");
    //var right6 = document.getElementsByClassName("right6");
    //var right7 = document.getElementsByClassName("right7");
    //var right8 = document.getElementsByClassName("right8");
    //var right9 = document.getElementsByClassName("right9");
    var scorebox = document.getElementsByClassName("scorebox");

    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0].split('=');
    var idlv = split[1];
    var idlv2 = split[2];
    var term = split[3].split('=');
    var id2 = split[4].split('=');
    var mode = split[5].split('=');

    //left1[0].classList.remove('active');
    //left2[0].classList.remove('active');
    //left3[0].classList.remove('active');
    //left4[0].classList.remove('active');
    //left5[0].classList.remove('active');
    //left6[0].classList.remove('active');
    //left7[0].classList.remove('active');
    //left8[0].classList.remove('active');
    //left9[0].classList.remove('active');
    //right1[0].classList.remove('active');
    //right2[0].classList.remove('active');
    //right3[0].classList.remove('active');
    //right4[0].classList.remove('active');
    //right5[0].classList.remove('active');
    //right6[0].classList.remove('active');
    //right7[0].classList.remove('active');
    //right8[0].classList.remove('active');
    //right9[0].classList.remove('active');

    //left1[0].classList.add('hidden');
    //left2[0].classList.add('hidden');
    //left3[0].classList.add('hidden');
    //left4[0].classList.add('hidden');
    //left5[0].classList.add('hidden');
    //left6[0].classList.add('hidden');
    //left7[0].classList.add('hidden');
    //left8[0].classList.add('hidden');
    //left9[0].classList.add('hidden');
    //right1[0].classList.add('hidden');
    //right2[0].classList.add('hidden');
    //right3[0].classList.add('hidden');
    //right4[0].classList.add('hidden');
    //right5[0].classList.add('hidden');
    //right6[0].classList.add('hidden');
    //right7[0].classList.add('hidden');
    //right8[0].classList.add('hidden');
    //right9[0].classList.add('hidden');


    if (id == "1") {

        if (mode[1] != "EN")
            scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 1-20";
        else scorebox[0].value = "Exercise 1-20";
        //right1[0].classList.remove('hidden');
    }
    //else if (id == "2") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 6-10";
    //    else scorebox[0].value = "Exercise 6-10";

    //    left1[0].classList.remove('hidden');
    //    right2[0].classList.remove('hidden');
    //}
    //else if (id == "3") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 11-15";
    //    else scorebox[0].value = "Exercise 11-15";

    //    left2[0].classList.remove('hidden');
    //    right1[0].classList.remove('hidden');
    //}
    //else if (id == "4") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 6-10";
    //    else scorebox[0].value = "Exercise 6-10";

    //    left1[0].classList.remove('hidden');
    //    right2[0].classList.remove('hidden');
    //}
    //else if (id == "5") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 11-15";
    //    else scorebox[0].value = "Exercise 11-15";

    //    left2[0].classList.remove('hidden');
    //    right3[0].classList.remove('hidden');
    //}
    //else if (id == "6") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 16-20";
    //    else scorebox[0].value = "Exercise 16-20";

    //    left3[0].classList.remove('hidden');
    //}
    else if (id == "7") {
        if (mode[1] != "EN")
            scorebox[0].value = "คุณลักษณะอันพึงประสงค์ 1-10";
        else scorebox[0].value = "Behavior 1-10";

        //right4[0].classList.remove('hidden');
    }
    //else if (id == "8") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คุณลักษณะอันพึงประสงค์ 6-10";
    //    else scorebox[0].value = "Behavior 6-10";

    //    left4[0].classList.remove('hidden');
    //}
    else if (id == "9") {
        if (mode[1] != "EN")
            scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 21-40";
        else scorebox[0].value = "Exercise 21-40";

        //right5[0].classList.remove('hidden');
    }
    //else if (id == "10") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 26-30";
    //    else scorebox[0].value = "Exercise 26-30";

    //    left5[0].classList.remove('hidden');
    //    right6[0].classList.remove('hidden');
    //}
    //else if (id == "11") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 31-35";
    //    else scorebox[0].value = "Exercise 31-35";

    //    left6[0].classList.remove('hidden');
    //    right5[0].classList.remove('hidden');
    //}
    //else if (id == "12") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 26-30";
    //    else scorebox[0].value = "Exercise 26-30";

    //    left5[0].classList.remove('hidden');
    //    right6[0].classList.remove('hidden');
    //}
    //else if (id == "13") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 31-35";
    //    else scorebox[0].value = "Exercise 31-35";

    //    left6[0].classList.remove('hidden');
    //    right7[0].classList.remove('hidden');
    //}
    //else if (id == "14") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 36-40";
    //    else scorebox[0].value = "Exercise 36-40";

    //    left7[0].classList.remove('hidden');
    //}
    else if (id == "21") {
        if (mode[1] != "EN")
            scorebox[0].value = "กลางภาค 1-10";
        else scorebox[0].value = "Mid Term 1-10";

        //right8[0].classList.remove('hidden');
    }
    else if (id == "22") {
        if (mode[1] != "EN")
            scorebox[0].value = "ปลายภาค 1-10";
        else scorebox[0].value = "Final Term 1-10";

        //right9[0].classList.remove('hidden');
    }
    //else if (id == "23") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "กลางภาค 6-10";
    //    else scorebox[0].value = "Mid Term 6-10";

    //    left8[0].classList.remove('hidden');
    //}
    //else if (id == "24") {
    //    if (mode[1] != "EN")
    //        scorebox[0].value = "ปลายภาค 6-10";
    //    else scorebox[0].value = "Final Term 6-10";

    //    left9[0].classList.remove('hidden');
    //}
}

function changename(id) {

    var name1 = document.getElementsByClassName("test1name");
    var name2 = document.getElementsByClassName("test2name");
    var name3 = document.getElementsByClassName("test3name");
    var name4 = document.getElementsByClassName("test4name");
    var name5 = document.getElementsByClassName("test5name");
    var name6 = document.getElementsByClassName("test6name");
    var name7 = document.getElementsByClassName("test7name");
    var name8 = document.getElementsByClassName("test8name");
    var name9 = document.getElementsByClassName("test9name");
    var name10 = document.getElementsByClassName("test10name");
    var name11 = document.getElementsByClassName("test11name");
    var name12 = document.getElementsByClassName("test12name");
    var name13 = document.getElementsByClassName("test13name");
    var name14 = document.getElementsByClassName("test14name");
    var name15 = document.getElementsByClassName("test15name");
    var name16 = document.getElementsByClassName("test16name");
    var name17 = document.getElementsByClassName("test17name");
    var name18 = document.getElementsByClassName("test18name");
    var name19 = document.getElementsByClassName("test19name");
    var name20 = document.getElementsByClassName("test20name");
    var midname1 = document.getElementsByClassName("testmid1name");
    var midname2 = document.getElementsByClassName("testmid2name");
    var midname3 = document.getElementsByClassName("testmid3name");
    var midname4 = document.getElementsByClassName("testmid4name");
    var midname5 = document.getElementsByClassName("testmid5name");
    var midname6 = document.getElementsByClassName("testmid6name");
    var midname7 = document.getElementsByClassName("testmid7name");
    var midname8 = document.getElementsByClassName("testmid8name");
    var midname9 = document.getElementsByClassName("testmid9name");
    var midname10 = document.getElementsByClassName("testmid10name");
    var finalname1 = document.getElementsByClassName("testfinal1name");
    var finalname2 = document.getElementsByClassName("testfinal2name");
    var finalname3 = document.getElementsByClassName("testfinal3name");
    var finalname4 = document.getElementsByClassName("testfinal4name");
    var finalname5 = document.getElementsByClassName("testfinal5name");
    var finalname6 = document.getElementsByClassName("testfinal6name");
    var finalname7 = document.getElementsByClassName("testfinal7name");
    var finalname8 = document.getElementsByClassName("testfinal8name");
    var finalname9 = document.getElementsByClassName("testfinal9name");
    var finalname10 = document.getElementsByClassName("testfinal10name");
    var set1 = document.getElementsByClassName("set1name");
    var set2 = document.getElementsByClassName("set2name");
    var set3 = document.getElementsByClassName("set3name");
    var set4 = document.getElementsByClassName("set4name");
    var set5 = document.getElementsByClassName("set5name");
    var set6 = document.getElementsByClassName("set6name");
    var set7 = document.getElementsByClassName("set7name");
    var set8 = document.getElementsByClassName("set8name");
    var set9 = document.getElementsByClassName("set9name");
    var set10 = document.getElementsByClassName("set10name");
    var set11 = document.getElementsByClassName("set11name");
    var set12 = document.getElementsByClassName("set12name");
    var set13 = document.getElementsByClassName("set13name");
    var set14 = document.getElementsByClassName("set14name");
    var set15 = document.getElementsByClassName("set15name");
    var set16 = document.getElementsByClassName("set16name");
    var set17 = document.getElementsByClassName("set17name");
    var set18 = document.getElementsByClassName("set18name");
    var set19 = document.getElementsByClassName("set19name");
    var set20 = document.getElementsByClassName("set20name");
    var setb1 = document.getElementsByClassName("setnameb1");
    var setb2 = document.getElementsByClassName("setnameb2");
    var setb3 = document.getElementsByClassName("setnameb3");
    var setb4 = document.getElementsByClassName("setnameb4");
    var setb5 = document.getElementsByClassName("setnameb5");
    var setb6 = document.getElementsByClassName("setnameb6");
    var setb7 = document.getElementsByClassName("setnameb7");
    var setb8 = document.getElementsByClassName("setnameb8");
    var setb9 = document.getElementsByClassName("setnameb9");
    var setb10 = document.getElementsByClassName("setnameb10");
    var nameb1 = document.getElementsByClassName("testb1name");
    var nameb2 = document.getElementsByClassName("testb2name");
    var nameb3 = document.getElementsByClassName("testb3name");
    var nameb4 = document.getElementsByClassName("testb4name");
    var nameb5 = document.getElementsByClassName("testb5name");
    var nameb6 = document.getElementsByClassName("testb6name");
    var nameb7 = document.getElementsByClassName("testb7name");
    var nameb8 = document.getElementsByClassName("testb8name");
    var nameb9 = document.getElementsByClassName("testb9name");
    var nameb10 = document.getElementsByClassName("testb10name");

    var namecw1 = document.getElementsByClassName("testcw1name");
    var namecw2 = document.getElementsByClassName("testcw2name");
    var namecw3 = document.getElementsByClassName("testcw3name");
    var namecw4 = document.getElementsByClassName("testcw4name");
    var namecw5 = document.getElementsByClassName("testcw5name");
    var namecw6 = document.getElementsByClassName("testcw6name");
    var namecw7 = document.getElementsByClassName("testcw7name");
    var namecw8 = document.getElementsByClassName("testcw8name");
    var namecw9 = document.getElementsByClassName("testcw9name");
    var namecw10 = document.getElementsByClassName("testcw10name");
    var namecw11 = document.getElementsByClassName("testcw11name");
    var namecw12 = document.getElementsByClassName("testcw12name");
    var namecw13 = document.getElementsByClassName("testcw13name");
    var namecw14 = document.getElementsByClassName("testcw14name");
    var namecw15 = document.getElementsByClassName("testcw15name");
    var namecw16 = document.getElementsByClassName("testcw16name");
    var namecw17 = document.getElementsByClassName("testcw17name");
    var namecw18 = document.getElementsByClassName("testcw18name");
    var namecw19 = document.getElementsByClassName("testcw19name");
    var namecw20 = document.getElementsByClassName("testcw20name");
    var setcw1 = document.getElementsByClassName("set1namecw");
    var setcw2 = document.getElementsByClassName("set2namecw");
    var setcw3 = document.getElementsByClassName("set3namecw");
    var setcw4 = document.getElementsByClassName("set4namecw");
    var setcw5 = document.getElementsByClassName("set5namecw");
    var setcw6 = document.getElementsByClassName("set6namecw");
    var setcw7 = document.getElementsByClassName("set7namecw");
    var setcw8 = document.getElementsByClassName("set8namecw");
    var setcw9 = document.getElementsByClassName("set9namecw");
    var setcw10 = document.getElementsByClassName("set10namecw");
    var setcw11 = document.getElementsByClassName("set11namecw");
    var setcw12 = document.getElementsByClassName("set12namecw");
    var setcw13 = document.getElementsByClassName("set13namecw");
    var setcw14 = document.getElementsByClassName("set14namecw");
    var setcw15 = document.getElementsByClassName("set15namecw");
    var setcw16 = document.getElementsByClassName("set16namecw");
    var setcw17 = document.getElementsByClassName("set17namecw");
    var setcw18 = document.getElementsByClassName("set18namecw");
    var setcw19 = document.getElementsByClassName("set19namecw");
    var setcw20 = document.getElementsByClassName("set20namecw");
    var setmid1 = document.getElementsByClassName("set1midcw");
    var setmid2 = document.getElementsByClassName("set2midcw");
    var setmid3 = document.getElementsByClassName("set3midcw");
    var setmid4 = document.getElementsByClassName("set4midcw");
    var setmid5 = document.getElementsByClassName("set5midcw");
    var setmid6 = document.getElementsByClassName("set6midcw");
    var setmid7 = document.getElementsByClassName("set7midcw");
    var setmid8 = document.getElementsByClassName("set8midcw");
    var setmid9 = document.getElementsByClassName("set9midcw");
    var setmid10 = document.getElementsByClassName("set10midcw");
    var setfinal1 = document.getElementsByClassName("set1finalcw");
    var setfinal2 = document.getElementsByClassName("set2finalcw");
    var setfinal3 = document.getElementsByClassName("set3finalcw");
    var setfinal4 = document.getElementsByClassName("set4finalcw");
    var setfinal5 = document.getElementsByClassName("set5finalcw");
    var setfinal6 = document.getElementsByClassName("set6finalcw");
    var setfinal7 = document.getElementsByClassName("set7finalcw");
    var setfinal8 = document.getElementsByClassName("set8finalcw");
    var setfinal9 = document.getElementsByClassName("set9finalcw");
    var setfinal10 = document.getElementsByClassName("set10finalcw");

    midname1[0].value = setmid1[0].value;
    midname2[0].value = setmid2[0].value;
    midname3[0].value = setmid3[0].value;
    midname4[0].value = setmid4[0].value;
    midname5[0].value = setmid5[0].value;
    midname6[0].value = setmid6[0].value;
    midname7[0].value = setmid7[0].value;
    midname8[0].value = setmid8[0].value;
    midname9[0].value = setmid9[0].value;
    midname10[0].value = setmid10[0].value;
    finalname1[0].value = setfinal1[0].value;
    finalname2[0].value = setfinal2[0].value;
    finalname3[0].value = setfinal3[0].value;
    finalname4[0].value = setfinal4[0].value;
    finalname5[0].value = setfinal5[0].value;
    finalname6[0].value = setfinal6[0].value;
    finalname7[0].value = setfinal7[0].value;
    finalname8[0].value = setfinal8[0].value;
    finalname9[0].value = setfinal9[0].value;
    finalname10[0].value = setfinal10[0].value;

    nameb1[0].value = setb1[0].value;
    nameb2[0].value = setb2[0].value;
    nameb3[0].value = setb3[0].value;
    nameb4[0].value = setb4[0].value;
    nameb5[0].value = setb5[0].value;
    nameb6[0].value = setb6[0].value;
    nameb7[0].value = setb7[0].value;
    nameb8[0].value = setb8[0].value;
    nameb9[0].value = setb9[0].value;
    nameb10[0].value = setb10[0].value;

    name1[0].value = set1[0].value;
    name2[0].value = set2[0].value;
    name3[0].value = set3[0].value;
    name4[0].value = set4[0].value;
    name5[0].value = set5[0].value;
    name6[0].value = set6[0].value;
    name7[0].value = set7[0].value;
    name8[0].value = set8[0].value;
    name9[0].value = set9[0].value;
    name10[0].value = set10[0].value;
    name11[0].value = set11[0].value;
    name12[0].value = set12[0].value;
    name13[0].value = set13[0].value;
    name14[0].value = set14[0].value;
    name15[0].value = set15[0].value;
    name16[0].value = set16[0].value;
    name17[0].value = set17[0].value;
    name18[0].value = set18[0].value;
    name19[0].value = set19[0].value;
    name20[0].value = set20[0].value;

    namecw1[0].value = setcw1[0].value;
    namecw2[0].value = setcw2[0].value;
    namecw3[0].value = setcw3[0].value;
    namecw4[0].value = setcw4[0].value;
    namecw5[0].value = setcw5[0].value;
    namecw6[0].value = setcw6[0].value;
    namecw7[0].value = setcw7[0].value;
    namecw8[0].value = setcw8[0].value;
    namecw9[0].value = setcw9[0].value;
    namecw10[0].value = setcw10[0].value;
    namecw11[0].value = setcw11[0].value;
    namecw12[0].value = setcw12[0].value;
    namecw13[0].value = setcw13[0].value;
    namecw14[0].value = setcw14[0].value;
    namecw15[0].value = setcw15[0].value;
    namecw16[0].value = setcw16[0].value;
    namecw17[0].value = setcw17[0].value;
    namecw18[0].value = setcw18[0].value;
    namecw19[0].value = setcw19[0].value;
    namecw20[0].value = setcw20[0].value;

    var el1 = $('.test1name');
    var el2 = $('.test2name');
    var el3 = $('.test3name');
    var el4 = $('.test4name');
    var el5 = $('.test5name');
    var el6 = $('.test6name');
    var el7 = $('.test7name');
    var el8 = $('.test8name');
    var el9 = $('.test9name');
    var el10 = $('.test10name');
    var el11 = $('.test11name');
    var el12 = $('.test12name');
    var el13 = $('.test13name');
    var el14 = $('.test14name');
    var el15 = $('.test15name');
    var el16 = $('.test16name');
    var el17 = $('.test17name');
    var el18 = $('.test18name');
    var el19 = $('.test19name');
    var el20 = $('.test20name');
    var elb1 = $('.testb1name');
    var elb2 = $('.testb2name');
    var elb3 = $('.testb3name');
    var elb4 = $('.testb4name');
    var elb5 = $('.testb5name');
    var elb6 = $('.testb6name');
    var elb7 = $('.testb7name');
    var elb8 = $('.testb8name');
    var elb9 = $('.testb9name');
    var elb10 = $('.testb10name');
    var elc1 = $('.testcw1name');
    var elc2 = $('.testcw2name');
    var elc3 = $('.testcw3name');
    var elc4 = $('.testcw4name');
    var elc5 = $('.testcw5name');
    var elc6 = $('.testcw6name');
    var elc7 = $('.testcw7name');
    var elc8 = $('.testcw8name');
    var elc9 = $('.testcw9name');
    var elc10 = $('.testcw10name');
    var elc11 = $('.testcw11name');
    var elc12 = $('.testcw12name');
    var elc13 = $('.testcw13name');
    var elc14 = $('.testcw14name');
    var elc15 = $('.testcw15name');
    var elc16 = $('.testcw16name');
    var elc17 = $('.testcw17name');
    var elc18 = $('.testcw18name');
    var elc19 = $('.testcw19name');
    var elc20 = $('.testcw20name');

    var open = document.getElementsByClassName("protip-show");

    // Shows tooltip with title: "My new title"
    if (id == 1) {
        el1.protipShow({
            title: set1[0].value,
            trigger: 'hover'
        });
        open[0].classList.remove('protip-show');
    }

    else if (id == 2) {
        el2.protipShow({
            title: set2[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 3) {
        el3.protipShow({
            title: set3[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 4) {
        el4.protipShow({
            title: set4[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 5) {
        el5.protipShow({
            title: set5[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 6) {
        el6.protipShow({
            title: set6[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 7) {
        el7.protipShow({
            title: set7[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 8) {
        el8.protipShow({
            title: set8[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 9) {
        el9.protipShow({
            title: set9[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 10) {
        el10.protipShow({
            title: set10[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 11) {
        el11.protipShow({
            title: set11[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 12) {
        el12.protipShow({
            title: set12[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 13) {
        el13.protipShow({
            title: set13[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 14) {
        el14.protipShow({
            title: set14[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 15) {
        el15.protipShow({
            title: set15[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 16) {
        el16.protipShow({
            title: set16[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 17) {
        el17.protipShow({
            title: set17[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 18) {
        el18.protipShow({
            title: set18[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 19) {
        el19.protipShow({
            title: set19[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 20) {
        el20.protipShow({
            title: set20[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 41) {
        elb1.protipShow({
            title: setb1[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 42) {
        elb2.protipShow({
            title: setb2[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 43) {
        elb3.protipShow({
            title: setb3[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 44) {
        elb4.protipShow({
            title: setb4[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 45) {
        elb5.protipShow({
            title: setb5[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 46) {
        elb6.protipShow({
            title: setb6[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 47) {
        elb7.protipShow({
            title: setb7[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 48) {
        elb8.protipShow({
            title: setb8[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 49) {
        elb9.protipShow({
            title: setb9[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 50) {
        elb10.protipShow({
            title: setb10[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 21) {
        elc1.protipShow({
            title: setcw1[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 22) {
        elc2.protipShow({
            title: setcw2[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 23) {
        elc3.protipShow({
            title: setcw3[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 24) {
        elc4.protipShow({
            title: setcw4[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 25) {
        elc5.protipShow({
            title: setcw5[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 26) {
        elc6.protipShow({
            title: setcw6[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 27) {
        elc7.protipShow({
            title: setcw7[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 28) {
        elc8.protipShow({
            title: setcw8[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 29) {
        elc9.protipShow({
            title: setcw9[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 30) {
        elc10.protipShow({
            title: setcw10[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 31) {
        elc11.protipShow({
            title: setcw11[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 32) {
        elc12.protipShow({
            title: setcw12[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 33) {
        elc13.protipShow({
            title: setcw13[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 34) {
        elc14.protipShow({
            title: setcw14[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 35) {
        elc15.protipShow({
            title: setcw15[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 36) {
        elc16.protipShow({
            title: setcw16[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 37) {
        elc17.protipShow({
            title: setcw17[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 38) {
        elc18.protipShow({
            title: setcw18[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 39) {
        elc19.protipShow({
            title: setcw19[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }
    else if (id == 40) {
        elc20.protipShow({
            title: setcw20[0].value,
            trigger: 'hover'
        }); open[0].classList.remove('protip-show');
    }


}

function changeddl() {

    var ddl1 = document.getElementsByClassName("ddl1");
    var ddl2 = document.getElementsByClassName("ddl2");
    var ddl3 = document.getElementsByClassName("ddl3");
    var ddl4 = document.getElementsByClassName("ddl4");
    var ddl1set = document.getElementsByClassName("ddl1set");
    var ddl2set = document.getElementsByClassName("ddl2set");
    var ddl3set = document.getElementsByClassName("ddl3set");
    var ddl4set = document.getElementsByClassName("ddl4set");


    if (Number(ddl1set[0].value) == 0) ddl1[0].selectedIndex = 1;
    if (Number(ddl1set[0].value) == 5) ddl1[0].selectedIndex = 2;
    if (Number(ddl1set[0].value) == 10) ddl1[0].selectedIndex = 3;
    if (Number(ddl1set[0].value) == 15) ddl1[0].selectedIndex = 4;
    if (Number(ddl1set[0].value) == 20) ddl1[0].selectedIndex = 5;
    if (Number(ddl1set[0].value) == 25) ddl1[0].selectedIndex = 6;
    if (Number(ddl1set[0].value) == 30) ddl1[0].selectedIndex = 7;
    if (Number(ddl1set[0].value) == 35) ddl1[0].selectedIndex = 8;
    if (Number(ddl1set[0].value) == 40) ddl1[0].selectedIndex = 9;
    if (Number(ddl1set[0].value) == 45) ddl1[0].selectedIndex = 10;
    if (Number(ddl1set[0].value) == 50) ddl1[0].selectedIndex = 11;
    if (Number(ddl1set[0].value) == 55) ddl1[0].selectedIndex = 12;
    if (Number(ddl1set[0].value) == 60) ddl1[0].selectedIndex = 13;
    if (Number(ddl1set[0].value) == 65) ddl1[0].selectedIndex = 14;
    if (Number(ddl1set[0].value) == 70) ddl1[0].selectedIndex = 15;
    if (Number(ddl1set[0].value) == 75) ddl1[0].selectedIndex = 16;
    if (Number(ddl1set[0].value) == 80) ddl1[0].selectedIndex = 17;
    if (Number(ddl1set[0].value) == 85) ddl1[0].selectedIndex = 18;
    if (Number(ddl1set[0].value) == 90) ddl1[0].selectedIndex = 19;
    if (Number(ddl1set[0].value) == 95) ddl1[0].selectedIndex = 20;
    if (Number(ddl1set[0].value) == 100) ddl1[0].selectedIndex = 21;


    if (Number(ddl2set[0].value) == 0) ddl2[0].selectedIndex = 1;
    if (Number(ddl2set[0].value) == 5) ddl2[0].selectedIndex = 2;
    if (Number(ddl2set[0].value) == 10) ddl2[0].selectedIndex = 3;
    if (Number(ddl2set[0].value) == 15) ddl2[0].selectedIndex = 4;
    if (Number(ddl2set[0].value) == 20) ddl2[0].selectedIndex = 5;
    if (Number(ddl2set[0].value) == 25) ddl2[0].selectedIndex = 6;
    if (Number(ddl2set[0].value) == 30) ddl2[0].selectedIndex = 7;
    if (Number(ddl2set[0].value) == 35) ddl2[0].selectedIndex = 8;
    if (Number(ddl2set[0].value) == 40) ddl2[0].selectedIndex = 9;
    if (Number(ddl2set[0].value) == 45) ddl2[0].selectedIndex = 10;
    if (Number(ddl2set[0].value) == 50) ddl2[0].selectedIndex = 11;
    if (Number(ddl2set[0].value) == 55) ddl2[0].selectedIndex = 12;
    if (Number(ddl2set[0].value) == 60) ddl2[0].selectedIndex = 13;
    if (Number(ddl2set[0].value) == 65) ddl2[0].selectedIndex = 14;
    if (Number(ddl2set[0].value) == 70) ddl2[0].selectedIndex = 15;
    if (Number(ddl2set[0].value) == 75) ddl2[0].selectedIndex = 16;
    if (Number(ddl2set[0].value) == 80) ddl2[0].selectedIndex = 17;
    if (Number(ddl2set[0].value) == 85) ddl2[0].selectedIndex = 18;
    if (Number(ddl2set[0].value) == 90) ddl2[0].selectedIndex = 19;
    if (Number(ddl2set[0].value) == 95) ddl2[0].selectedIndex = 20;
    if (Number(ddl2set[0].value) == 100) ddl2[0].selectedIndex = 21;


    if (Number(ddl3set[0].value) == 0) ddl3[0].selectedIndex = 1;
    if (Number(ddl3set[0].value) == 5) ddl3[0].selectedIndex = 2;
    if (Number(ddl3set[0].value) == 10) ddl3[0].selectedIndex = 3;
    if (Number(ddl3set[0].value) == 15) ddl3[0].selectedIndex = 4;
    if (Number(ddl3set[0].value) == 20) ddl3[0].selectedIndex = 5;
    if (Number(ddl3set[0].value) == 25) ddl3[0].selectedIndex = 6;
    if (Number(ddl3set[0].value) == 30) ddl3[0].selectedIndex = 7;
    if (Number(ddl3set[0].value) == 35) ddl3[0].selectedIndex = 8;
    if (Number(ddl3set[0].value) == 40) ddl3[0].selectedIndex = 9;
    if (Number(ddl3set[0].value) == 45) ddl3[0].selectedIndex = 10;
    if (Number(ddl3set[0].value) == 50) ddl3[0].selectedIndex = 11;
    if (Number(ddl3set[0].value) == 55) ddl3[0].selectedIndex = 12;
    if (Number(ddl3set[0].value) == 60) ddl3[0].selectedIndex = 13;
    if (Number(ddl3set[0].value) == 65) ddl3[0].selectedIndex = 14;
    if (Number(ddl3set[0].value) == 70) ddl3[0].selectedIndex = 15;
    if (Number(ddl3set[0].value) == 75) ddl3[0].selectedIndex = 16;
    if (Number(ddl3set[0].value) == 80) ddl3[0].selectedIndex = 17;
    if (Number(ddl3set[0].value) == 85) ddl3[0].selectedIndex = 18;
    if (Number(ddl3set[0].value) == 90) ddl3[0].selectedIndex = 19;
    if (Number(ddl3set[0].value) == 95) ddl3[0].selectedIndex = 20;
    if (Number(ddl3set[0].value) == 100) ddl3[0].selectedIndex = 21;

    if (Number(ddl4set[0].value) == 0) ddl4[0].selectedIndex = 1;
    if (Number(ddl4set[0].value) == 5) ddl4[0].selectedIndex = 2;
    if (Number(ddl4set[0].value) == 10) ddl4[0].selectedIndex = 3;
    if (Number(ddl4set[0].value) == 15) ddl4[0].selectedIndex = 4;
    if (Number(ddl4set[0].value) == 20) ddl4[0].selectedIndex = 5;
    if (Number(ddl4set[0].value) == 25) ddl4[0].selectedIndex = 6;
    if (Number(ddl4set[0].value) == 30) ddl4[0].selectedIndex = 7;
    if (Number(ddl4set[0].value) == 35) ddl4[0].selectedIndex = 8;
    if (Number(ddl4set[0].value) == 40) ddl4[0].selectedIndex = 9;
    if (Number(ddl4set[0].value) == 45) ddl4[0].selectedIndex = 10;
    if (Number(ddl4set[0].value) == 50) ddl4[0].selectedIndex = 11;
    if (Number(ddl4set[0].value) == 55) ddl4[0].selectedIndex = 12;
    if (Number(ddl4set[0].value) == 60) ddl4[0].selectedIndex = 13;
    if (Number(ddl4set[0].value) == 65) ddl4[0].selectedIndex = 14;
    if (Number(ddl4set[0].value) == 70) ddl4[0].selectedIndex = 15;
    if (Number(ddl4set[0].value) == 75) ddl4[0].selectedIndex = 16;
    if (Number(ddl4set[0].value) == 80) ddl4[0].selectedIndex = 17;
    if (Number(ddl4set[0].value) == 85) ddl4[0].selectedIndex = 18;
    if (Number(ddl4set[0].value) == 90) ddl4[0].selectedIndex = 19;
    if (Number(ddl4set[0].value) == 95) ddl4[0].selectedIndex = 20;
    if (Number(ddl4set[0].value) == 100) ddl4[0].selectedIndex = 21;
}

function setupddl() {

    var setup1 = document.getElementsByClassName("setup1");
    var setup2 = document.getElementsByClassName("setup2");
    var setup3 = document.getElementsByClassName("setup3");
    var setup4 = document.getElementsByClassName("setup4");
    var setup5 = document.getElementsByClassName("setup5");
    var setup6 = document.getElementsByClassName("setup6");
    var setup7 = document.getElementsByClassName("setup7");
    var setup8 = document.getElementsByClassName("setup8");
    var setup9 = document.getElementsByClassName("setup9");
    var setup11 = document.getElementsByClassName("setup11");
    var setup12 = document.getElementsByClassName("setup12");

    var autoddl1 = document.getElementsByClassName("autoddl1");
    var autoddl2 = document.getElementsByClassName("autoddl2");
    var autoddl3 = document.getElementsByClassName("autoddl3");
    var autoddl4 = document.getElementsByClassName("autoddl4");
    var autoddl5 = document.getElementsByClassName("autoddl5");
    var autoddl6 = document.getElementsByClassName("autoddl6");
    var autoddl7 = document.getElementsByClassName("autoddl7");
    var autoddl8 = document.getElementsByClassName("autoddl8");
    var autoddl9 = document.getElementsByClassName("autoddl9");
    var autoddl11 = document.getElementsByClassName("autoddl11");
    var autoddl12 = document.getElementsByClassName("autoddl12");

    var check2 = document.getElementsByClassName("check2");
    var check3 = document.getElementsByClassName("check3");
    var check4 = document.getElementsByClassName("check4");
    var check5 = document.getElementsByClassName("check5");
    var check6 = document.getElementsByClassName("check6");
    var check7 = document.getElementsByClassName("check7");
    var check8 = document.getElementsByClassName("check8");
    var check9 = document.getElementsByClassName("check9");
    var check10 = document.getElementsByClassName("check10");

    if (setup1[0].value == "0")
        document.getElementById("check2").checked = false;
    else if (setup1[0].value == "1")
        document.getElementById("check2").checked = true;

    if (setup2[0].value == "0")
        document.getElementById("check3").checked = false;
    else if (setup2[0].value == "1")
        document.getElementById("check3").checked = true;

    if (setup3[0].value == "0")
        document.getElementById("ctl00_MainContent_check4").checked = false;
    else if (setup3[0].value == "1")
        document.getElementById("ctl00_MainContent_check4").checked = true;

    if (setup4[0].value == "0")
        document.getElementById("ctl00_MainContent_check5").checked = false;
    else if (setup4[0].value == "1")
        document.getElementById("ctl00_MainContent_check5").checked = true;

    if (setup5[0].value == "0")
        document.getElementById("ctl00_MainContent_check6").checked = false;
    else if (setup5[0].value == "1")
        document.getElementById("ctl00_MainContent_check6").checked = true;

    if (setup6[0].value == "0")
        document.getElementById("check7").checked = false;
    else if (setup6[0].value == "1")
        document.getElementById("check7").checked = true;



    if (setup8[0].value == "0")
        document.getElementById("check9").checked = false;
    else if (setup8[0].value == "1")
        document.getElementById("check9").checked = true;

    if (setup9[0].value == "0")
        document.getElementById("check10").checked = false;
    else if (setup9[0].value == "1")
        document.getElementById("check10").checked = true;

    if (setup11[0].value == "0")
        document.getElementById("ctl00_MainContent_check11").checked = false;
    else if (setup11[0].value == "1")
        document.getElementById("ctl00_MainContent_check11").checked = true;

    if (setup12[0].value == "0")
        document.getElementById("ctl00_MainContent_check12").checked = false;
    else if (setup12[0].value == "1")
        document.getElementById("ctl00_MainContent_check12").checked = true;
}

function auto(id) {

    var maxtestb1 = document.getElementsByClassName("maxtestb1");
    var goodbe = document.getElementsByClassName("goodbe");
    var read = document.getElementsByClassName("readscore");
    var greencog = document.getElementsByClassName("greencog");
    var yellowcog = document.getElementsByClassName("yellowcog");
    var redcog = document.getElementsByClassName("redcog");
    var cog1 = document.getElementsByClassName("cog1");
    var cog2 = document.getElementsByClassName("cog2");
    var blue3 = document.getElementsByClassName("bluebutton3");
    var blue = document.getElementsByClassName("bluebutton");
    var behavedisable = document.getElementsByClassName("behavedisable");
    var subcheckbox = document.getElementsByClassName("subcheckbox");
    var editform1 = document.getElementsByClassName("editform1");
    var editform2 = document.getElementsByClassName("editform2");
    var editform3 = document.getElementsByClassName("editform3");
    var editgrade = document.getElementsByClassName("editgrade");
    var cen2 = document.getElementsByClassName("cen2");
    var cen3 = document.getElementsByClassName("cen3");
    var cen4 = document.getElementsByClassName("cen4");
    var nobehave = document.getElementsByClassName("nobehave");
    var gradetxt2 = document.getElementsByClassName("gradetxt2");
    var samatscore = document.getElementsByClassName("samatscore");
    var cen5 = document.getElementsByClassName("cen5");
    var w40 = document.getElementsByClassName("w40");

    var ddl1 = document.getElementsByClassName("autoddl1");
    var ddl2 = document.getElementsByClassName("autoddl2");
    var ddl3 = document.getElementsByClassName("autoddl3");
    var ddl4 = document.getElementsByClassName("autoddl4");
    var ddl5 = document.getElementsByClassName("autoddl5");
    var ddl6 = document.getElementsByClassName("autoddl6");
    var ddl7 = document.getElementsByClassName("autoddl7");
    var ddl8 = document.getElementsByClassName("autoddl8");
    var ddl9 = document.getElementsByClassName("autoddl9");
    var ddl11 = document.getElementsByClassName("autoddl11");
    var ddl12 = document.getElementsByClassName("autoddl12");

    var check3 = document.getElementsByClassName("check3");
    var check4 = document.getElementsByClassName("check4");
    var check5 = document.getElementsByClassName("check5");
    var check6 = document.getElementsByClassName("check6");
    var check7 = document.getElementsByClassName("check7");
    var check8 = document.getElementsByClassName("check8");
    var check9 = document.getElementsByClassName("check9");
    var check10 = document.getElementsByClassName("check10");
    var check11 = document.getElementsByClassName("check11");
    var check12 = document.getElementsByClassName("check12");

    var samatscore = document.getElementsByClassName("samatscore");

    var maxmidscore = document.getElementsByClassName("maxmidscore");
    var summidbox = document.getElementsByClassName("summidbox");
    var maxlatescore = document.getElementsByClassName("maxlatescore");
    var sumfinalbox = document.getElementsByClassName("sumfinalbox");

    if (check5[0].checked == true) {
        ddl4[0].value = "1";
    }
    else if (check5[0].checked == false) {
        ddl4[0].value = "0";
    }

    if (id == "8") {
        var elm2 = document.getElementById("ctl00_MainContent_check4");
        var elm = document.getElementById("ctl00_MainContent_check5");
        if (elm2.checked == true) {
            maxtestb1[0].value = "3";
            maxtestb1[1].value = "3";
            maxtestb1[2].value = "3";
            maxtestb1[3].value = "3";
            maxtestb1[4].value = "3";
            maxtestb1[5].value = "3";
            maxtestb1[6].value = "3";
            maxtestb1[7].value = "3";
            if (elm.checked == false)
                elm.checked = !elm.checked;
        }
        else {
            maxtestb1[0].value = "";
            maxtestb1[1].value = "";
            maxtestb1[2].value = "";
            maxtestb1[3].value = "";
            maxtestb1[4].value = "";
            maxtestb1[5].value = "";
            maxtestb1[6].value = "";
            maxtestb1[7].value = "";
            if (elm.checked == true)
                elm.checked = !elm.checked;
        }

    }
    if (id == "9") {
        //document.getElementById("check5").checked = false;
    }

    if (check4[0].checked == true) {
        ddl3[0].value = "1";
        blue[4].classList.remove('hidden');
        //subcheckbox[0].classList.remove('hidden');
    }
    else if (check4[0].checked == false) {
        ddl3[0].value = "0";
        blue[4].classList.add('hidden');
        //subcheckbox[0].classList.add('hidden');
    }

    if (check5[0].checked == false) {
        behavedisable[0].value = "0";
        for (var i = 0; i < goodbe.length; i++) {
            goodbe[i].classList.remove('disable');
        }
    }
    if (check5[0].checked == true) {
        behavedisable[0].value = "1";
        for (var i = 0; i < read.length; i++) {
            goodbe[i].classList.add('disable');
        }
    }

    if (check3[0].checked == true) {
        ddl2[0].value = "1";
        cog2[0].value = "0";
        for (var i = 0; i < goodbe.length; i++) {
            read[i].classList.remove('disable');
        }
    }
    else if (check3[0].checked == false) {
        ddl2[0].value = "0";
        cog2[0].value = "1";
        for (var i = 0; i < read.length; i++) {
            read[i].classList.add('disable');
        }
    }



    if (check6[0].checked == true) {
        ddl5[0].value = "1";
        blue[1].classList.remove('hidden');
    }
    else if (check6[0].checked == false) {
        ddl5[0].value = "0";
        blue[1].classList.add('hidden');
    }

    if (check11[0].checked == true) {
        ddl11[0].value = "1";
        maxmidscore[0].classList.add('disable');
        for (var i = 0; i < read.length; i++) {
            summidbox[i].classList.add('disable');
        }
        blue[2].classList.remove('hidden');
    }
    else if (check11[0].checked == false) {
        ddl11[0].value = "0";
        maxmidscore[0].classList.remove('disable');
        for (var i = 0; i < read.length; i++) {
            summidbox[i].classList.remove('disable');
        }
        blue[2].classList.add('hidden');
    }

    if (check12[0].checked == true) {
        ddl12[0].value = "1";
        maxlatescore[0].classList.add('disable');
        for (var i = 0; i < read.length; i++) {
            sumfinalbox[i].classList.add('disable');
        }
        blue[3].classList.remove('hidden');
    }
    else if (check12[0].checked == false) {
        ddl12[0].value = "0";
        maxlatescore[0].classList.remove('disable');
        for (var i = 0; i < read.length; i++) {
            sumfinalbox[i].classList.remove('disable');
        }
        blue[3].classList.add('hidden');
    }

    if (check8[0].checked == true) {
        ddl7[0].value = "1";
    }
    else if (check8[0].checked == false) {
        ddl7[0].value = "0";
    }


    if (check9[0].checked == true) {
        ddl8[0].value = "1";
        editform2[0].classList.add('hidden');
        for (var i = 0; i < w40.length; i++) {
            editgrade[i].classList.add('hidden');
            //gradetxt2[i].value = "";
        }
    }
    else if (check9[0].checked == false) {
        ddl8[0].value = "0";
        editform2[0].classList.remove('hidden');
        for (var i = 0; i < w40.length; i++) {
            editgrade[i].classList.remove('hidden');
        }
    }

    if (check10[0].checked == true) {
        ddl9[0].value = "1";
        editform3[0].classList.add('hidden');
        for (var i = 0; i < w40.length; i++) {
            cen5[i].classList.add('hidden');
            samatscore[i].value = "";
        }
    }
    else if (check10[0].checked == false) {
        ddl9[0].value = "0";
        editform3[0].classList.remove('hidden');
        for (var i = 0; i < w40.length; i++) {
            cen5[i].classList.remove('hidden');
        }
    }

    if (check7[0].checked == true) {
        ddl6[0].value = "1";
        editform1[0].classList.add('hidden');
        editform1[1].classList.add('hidden');
        nobehave[0].value = "1";
        for (var i = 0; i < cen3.length; i++) {
            cen3[i].classList.add('hidden');
        }

        for (var i = 0; i < cen2.length; i++) {
            cen2[i].classList.add('cen2alt');
        }
        for (var i = 0; i < cen4.length; i++) {
            cen4[i].classList.add('cen4alt');
        }
        var cen2alt = document.getElementsByClassName("cen2alt");
        var cen4alt = document.getElementsByClassName("cen4alt");

        for (var i = 0; i < cen2alt.length; i++) {
            cen2alt[i].classList.remove('cen2');
        }
        for (var i = 0; i < cen4alt.length; i++) {
            cen4alt[i].classList.remove('cen4');
        }

    }
    else if (check7[0].checked == false) {
        ddl6[0].value = "0";
        editform1[0].classList.remove('hidden');
        editform1[1].classList.remove('hidden');
        nobehave[0].value = "0";
        for (var i = 0; i < cen3.length; i++) {
            cen3[i].classList.remove('hidden');
        }

        var cen2alt = document.getElementsByClassName("cen2alt");
        var cen4alt = document.getElementsByClassName("cen4alt");

        for (var i = 0; i < cen2alt.length; i++) {
            cen2alt[i].classList.add('cen2');
        }
        for (var i = 0; i < cen4alt.length; i++) {
            cen4alt[i].classList.add('cen4');
        }

        var cen2new = document.getElementsByClassName("cen2");
        var cen4new = document.getElementsByClassName("cen4");

        for (var i = 0; i < cen2new.length; i++) {
            cen2new[i].classList.remove('cen2alt');
        }
        for (var i = 0; i < cen4new.length; i++) {
            cen4new[i].classList.remove('cen4alt');
        }

    }

    if (id == 1) {
        greencog[0].classList.add('hidden');
        redcog[0].classList.remove('hidden');
        blue3[0].classList.remove('hidden');
        cog1[0].value = "1";
        for (var i = 0; i < goodbe.length; i++) {
            goodbe[i].classList.add('disable');
        }
    }

    if (id == 2) {
        yellowcog[0].classList.remove('hidden');
        redcog[0].classList.add('hidden');
        cog1[0].value = "0";
        for (var i = 0; i < read.length; i++) {
            goodbe[i].classList.remove('disable');
        }
    }

    if (id == 7) {
        greencog[0].classList.remove('hidden');
        blue3[0].classList.add('hidden');
        yellowcog[0].classList.add('hidden');
        cog1[0].value = "0";
        for (var i = 0; i < read.length; i++) {
            goodbe[i].classList.remove('disable');
        }
    }

    if (id == 4) {
        cog2[0].value = "1";
        greencog[1].classList.add('hidden');
        redcog[1].classList.remove('hidden');
        for (var i = 0; i < read.length; i++) {
            read[i].classList.add('disable');
        }
    }

    if (id == 5) {
        cog2[0].value = "0";
        greencog[1].classList.remove('hidden');
        redcog[1].classList.add('hidden');
        for (var i = 0; i < goodbe.length; i++) {
            read[i].classList.remove('disable');
        }
    }
}

function ddlshare() {

    var editmulti = document.getElementsByClassName("editmulti");
    var check8 = document.getElementsByClassName("check8");

    var classchoose2 = document.getElementsByClassName("classchoose2");

    var maxmidscore = document.getElementsByClassName("maxmidscore");
    var summidbox = document.getElementsByClassName("summidbox");
    var maxlatescore = document.getElementsByClassName("maxlatescore");
    var sumfinalbox = document.getElementsByClassName("sumfinalbox");


    if (check8[0].checked == true) {
        classchoose2[0].classList.remove('hidden');
    }
    else if (check8[0].checked == false) {
        classchoose2[0].classList.add('hidden');

    }


}

function editddl() {

    var special = document.getElementsByClassName("special");
    var useryear = document.getElementsByClassName("useryear");
    var userterm = document.getElementsByClassName("userterm");
    var userplan = document.getElementsByClassName("userplan");
    var siduser = document.getElementsByClassName("siduser");

    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0].split('=');
    var idlv = split[1];
    var idlv2 = split[2].split('=');
    var term = split[3].split('=');
    var id = split[4].split('=');

    $.get("/App_Logic/bp5GradeRegister.ashx?term=" + term[1] + "&year=" + year[1] + "&id=" + id[1] + "&idlv2=" + idlv2[1], function (Result) {
        $.each(Result, function (index) {

            for (var x = 0; x < siduser.length; x++) {
                if (Result[index].ddlother != "-1" && siduser[x].value == Result[index].sID) {
                    if (Result[index].ddlother == "1")
                        special[x].selectedIndex = Result[index].ddlother;
                    else if (Result[index].ddlother == "2")
                        special[x].selectedIndex = Result[index].ddlother;
                    else if (Result[index].ddlother == "3")
                        special[x].selectedIndex = Result[index].ddlother;
                    else if (Result[index].ddlother == "4")
                        special[x].selectedIndex = Result[index].ddlother;
                    else if (Result[index].ddlother == "5")
                        special[x].selectedIndex = Result[index].ddlother;
                    else if (Result[index].ddlother == "6")
                        special[x].selectedIndex = 9;
                    else if (Result[index].ddlother == "7")
                        special[x].selectedIndex = 6;
                    else if (Result[index].ddlother == "8")
                        special[x].selectedIndex = 7;
                    else if (Result[index].ddlother == "9")
                        special[x].selectedIndex = 8;
                    else if (Result[index].ddlother == "10")
                        special[x].selectedIndex = 10;
                    else if (Result[index].ddlother == "11")
                        special[x].selectedIndex = 11;
                    else if (Result[index].ddlother == "12")
                        special[x].selectedIndex = 12;
                    else if (Result[index].ddlother == "13")
                        special[x].selectedIndex = 13;
                    else special[x].selectedIndex = 0;
                }
            }



        });
    });
}

function ddlcopy(id) {

    var copy1 = document.getElementsByClassName("copy1");
    var copy2 = document.getElementsByClassName("copy2");
    var copy3 = document.getElementsByClassName("copy3");
    var ddlcopy1 = document.getElementsByClassName("ddlcopy1");
    var ddlcopy2 = document.getElementsByClassName("ddlcopy2");
    var ddlcopy3 = document.getElementsByClassName("ddlcopy3");
    var special = document.getElementsByClassName("special");
    var maxtestb1 = document.getElementsByClassName("maxtestb1");

    var setb1 = document.getElementsByClassName("setnameb1");
    var setb2 = document.getElementsByClassName("setnameb2");
    var setb3 = document.getElementsByClassName("setnameb3");
    var setb4 = document.getElementsByClassName("setnameb4");
    var setb5 = document.getElementsByClassName("setnameb5");
    var setb6 = document.getElementsByClassName("setnameb6");
    var setb7 = document.getElementsByClassName("setnameb7");
    var setb8 = document.getElementsByClassName("setnameb8");
    var setb9 = document.getElementsByClassName("setnameb9");
    var setb10 = document.getElementsByClassName("setnameb10");
    var nameb1 = document.getElementsByClassName("testb1name");
    var nameb2 = document.getElementsByClassName("testb2name");
    var nameb3 = document.getElementsByClassName("testb3name");
    var nameb4 = document.getElementsByClassName("testb4name");
    var nameb5 = document.getElementsByClassName("testb5name");
    var nameb6 = document.getElementsByClassName("testb6name");
    var nameb7 = document.getElementsByClassName("testb7name");
    var nameb8 = document.getElementsByClassName("testb8name");
    var nameb9 = document.getElementsByClassName("testb9name");
    var nameb10 = document.getElementsByClassName("testb10name");

    var behavetxt1 = document.getElementsByClassName("behavetxt1");
    var behavetxt2 = document.getElementsByClassName("behavetxt2");
    var behavetxt3 = document.getElementsByClassName("behavetxt3");
    var behavetxt4 = document.getElementsByClassName("behavetxt4");
    var behavetxt5 = document.getElementsByClassName("behavetxt5");
    var behavetxt6 = document.getElementsByClassName("behavetxt6");
    var behavetxt7 = document.getElementsByClassName("behavetxt7");
    var behavetxt8 = document.getElementsByClassName("behavetxt8");
    var behavetxt9 = document.getElementsByClassName("behavetxt9");
    var behavetxt10 = document.getElementsByClassName("behavetxt10");
    var goodbe = document.getElementsByClassName("goodbe");
    var readscore = document.getElementsByClassName("readscore");
    var behavesid = document.getElementsByClassName("behavesid");

    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0].split('=');
    var idlv = split[1];
    var idlv2 = split[2].split('=');
    var term = split[3].split('=');

    var useryear = year[1];
    var userterm = term[1];
    var useridlv2 = idlv2[1];
    var samatscore = document.getElementsByClassName("samatscore");

    copy1[0].value = ddlcopy1[0].value;
    copy2[0].value = ddlcopy2[0].value;
    copy3[0].value = ddlcopy3[0].value;

    if (id == 3) {
        $.get("/App_Logic/bp5ImportScore.ashx?mode=1&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy1[0].value, function (Result) {
            $.each(Result, function (index) {

                maxtestb1[0].value = Result[index].behave1max;
                maxtestb1[1].value = Result[index].behave2max;
                maxtestb1[2].value = Result[index].behave3max;
                maxtestb1[3].value = Result[index].behave4max;
                maxtestb1[4].value = Result[index].behave5max;
                maxtestb1[5].value = Result[index].behave6max;
                maxtestb1[6].value = Result[index].behave7max;
                maxtestb1[7].value = Result[index].behave8max;
                maxtestb1[8].value = Result[index].behave9max;
                maxtestb1[9].value = Result[index].behave10max;

                setb1[0].value = Result[index].behave1name;
                setb2[0].value = Result[index].behave2name;
                setb3[0].value = Result[index].behave3name;
                setb4[0].value = Result[index].behave4name;
                setb5[0].value = Result[index].behave5name;
                setb6[0].value = Result[index].behave6name;
                setb7[0].value = Result[index].behave7name;
                setb8[0].value = Result[index].behave8name;
                setb9[0].value = Result[index].behave9name;
                setb10[0].value = Result[index].behave10name;
            });
        });

        $.get("/App_Logic/bp5ImportScore.ashx?mode=2&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy1[0].value, function (Result) {
            $.each(Result, function (index) {
                for (var x = 0; x < behavesid.length; x++) {
                    if (Result[index].sID == behavesid[x].value) {
                        behavetxt1[x].value = Result[index].behave1;
                        behavetxt2[x].value = Result[index].behave2;
                        behavetxt3[x].value = Result[index].behave3;
                        behavetxt4[x].value = Result[index].behave4;
                        behavetxt5[x].value = Result[index].behave5;
                        behavetxt6[x].value = Result[index].behave6;
                        behavetxt7[x].value = Result[index].behave7;
                        behavetxt8[x].value = Result[index].behave8;
                        behavetxt9[x].value = Result[index].behave9;
                        behavetxt10[x].value = Result[index].behave10;
                        goodbe[x].value = Result[index].behaveTotal;
                    }
                }


            });
        });
    }

    if (id == 4) {
        $.get("/App_Logic/bp5ImportScore.ashx?mode=3&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy2[0].value, function (Result) {
            $.each(Result, function (index) {
                for (var x = 0; x < behavesid.length; x++) {
                    if (Result[index].sID == behavesid[x].value) {
                        readscore[x].value = Result[index].readingTotal;
                    }
                }

            });
        });
    }

    if (id == 5) {

        $.get("/App_Logic/bp5ImportScore.ashx?mode=4&term=" + userterm + "&year=" + useryear + "&idlv2=" + useridlv2 + "&id=" + copy3[0].value, function (Result) {
            $.each(Result, function (index) {
                for (var x = 0; x < behavesid.length; x++) {
                    if (Result[index].sID == behavesid[x].value) {
                        samatscore[x].value = Result[index].samattanaTotal;
                    }
                }

            });
        });
    }

    setTimeout(function () {
        changename();
    }, 1000);

}

function isNumeric(n) {
    if (n == "")
        return true;
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function calallscore(index) {

    var textBoxesg1 = document.getElementsByClassName("AutoCompleteTextBox");
    var textBoxesg2 = document.getElementsByClassName("AutoCompleteTextBoxg2");
    var textBoxesg3 = document.getElementsByClassName("AutoCompleteTextBoxg3");
    var textBoxesg4 = document.getElementsByClassName("AutoCompleteTextBoxg4");
    var textBoxes2 = document.getElementsByClassName("AutoCompleteTextBox2");
    var chewut1 = document.getElementsByClassName("chewut1");
    var chewut2 = document.getElementsByClassName("chewut2");
    var chewut3 = document.getElementsByClassName("chewut3");
    var chewut4 = document.getElementsByClassName("chewut4");

    var mid = document.getElementsByClassName("midratio");
    var late = document.getElementsByClassName("lateratio");
    var gradebox = document.getElementsByClassName("gradetxt");
    var special = document.getElementsByClassName("special");
    var maxtest = document.getElementsByClassName("maxtest");
    var maxtestcw = document.getElementsByClassName("maxtestcw");
    var reading = document.getElementsByClassName("readscore");
    var check2 = document.getElementsByClassName("check2");

    var maxscoreall = document.getElementsByClassName("maxscoreall");
    var scoresumall = document.getElementsByClassName("scoresumall");
    var maxmidscore = document.getElementsByClassName("maxmidscore");
    var maxlatescore = document.getElementsByClassName("maxlatescore");

    var lockg1 = document.getElementsByClassName("lockg1");
    var lockg2 = document.getElementsByClassName("lockg2");
    var lockg3 = document.getElementsByClassName("lockg3");
    var lockg4 = document.getElementsByClassName("lockg4");
    var lockg5 = document.getElementsByClassName("lockg5");
    var lockg6 = document.getElementsByClassName("lockg6");
    var lockg7 = document.getElementsByClassName("lockg7");
    var lockg8 = document.getElementsByClassName("lockg8");
    var lockg9 = document.getElementsByClassName("lockg9");
    var lockg10 = document.getElementsByClassName("lockg10");
    var lockg11 = document.getElementsByClassName("lockg11");
    var lockg12 = document.getElementsByClassName("lockg12");
    var lockg13 = document.getElementsByClassName("lockg13");
    var lockg14 = document.getElementsByClassName("lockg14");
    var lockg15 = document.getElementsByClassName("lockg15");
    var lockg16 = document.getElementsByClassName("lockg16");
    var lockg17 = document.getElementsByClassName("lockg17");
    var lockg18 = document.getElementsByClassName("lockg18");
    var lockg19 = document.getElementsByClassName("lockg19");
    var lockg20 = document.getElementsByClassName("lockg20");
    var lockcw1 = document.getElementsByClassName("lockcw1");
    var lockcw2 = document.getElementsByClassName("lockcw2");
    var lockcw3 = document.getElementsByClassName("lockcw3");
    var lockcw4 = document.getElementsByClassName("lockcw4");
    var lockcw5 = document.getElementsByClassName("lockcw5");
    var lockcw6 = document.getElementsByClassName("lockcw6");
    var lockcw7 = document.getElementsByClassName("lockcw7");
    var lockcw8 = document.getElementsByClassName("lockcw8");
    var lockcw9 = document.getElementsByClassName("lockcw9");
    var lockcw10 = document.getElementsByClassName("lockcw10");
    var lockcw11 = document.getElementsByClassName("lockcw11");
    var lockcw12 = document.getElementsByClassName("lockcw12");
    var lockcw13 = document.getElementsByClassName("lockcw13");
    var lockcw14 = document.getElementsByClassName("lockcw14");
    var lockcw15 = document.getElementsByClassName("lockcw15");
    var lockcw16 = document.getElementsByClassName("lockcw16");
    var lockcw17 = document.getElementsByClassName("lockcw17");
    var lockcw18 = document.getElementsByClassName("lockcw18");
    var lockcw19 = document.getElementsByClassName("lockcw19");
    var lockcw20 = document.getElementsByClassName("lockcw20");
    var lockm1 = document.getElementsByClassName("lockm1");
    var lockm2 = document.getElementsByClassName("lockm2");
    var lockm3 = document.getElementsByClassName("lockm3");
    var lockm4 = document.getElementsByClassName("lockm4");
    var lockm5 = document.getElementsByClassName("lockm5");
    var lockm6 = document.getElementsByClassName("lockm6");
    var lockm7 = document.getElementsByClassName("lockm7");
    var lockm8 = document.getElementsByClassName("lockm8");
    var lockm9 = document.getElementsByClassName("lockm9");
    var lockm10 = document.getElementsByClassName("lockm10");
    var lockf1 = document.getElementsByClassName("lockf1");
    var lockf2 = document.getElementsByClassName("lockf2");
    var lockf3 = document.getElementsByClassName("lockf3");
    var lockf4 = document.getElementsByClassName("lockf4");
    var lockf5 = document.getElementsByClassName("lockf5");
    var lockf6 = document.getElementsByClassName("lockf6");
    var lockf7 = document.getElementsByClassName("lockf7");
    var lockf8 = document.getElementsByClassName("lockf8");
    var lockf9 = document.getElementsByClassName("lockf9");
    var lockf10 = document.getElementsByClassName("lockf10");
    var lockmidterm = document.getElementsByClassName("lockmidterm");
    var lockfinalterm = document.getElementsByClassName("lockfinalterm");


    if (index == 99999 || index == null) {
        var maxscore = 0;
        for (var x = 0; x < 20; x++) {
            maxscore = maxscore + Number(maxtest[x].value);
            maxscore = maxscore + Number(maxtestcw[x].value);
        }
        maxscore = maxscore + Number(maxlatescore[0].value);
        maxscore = maxscore + Number(maxmidscore[0].value);
        maxscoreall[0].value = maxscore;
    }

    if (index == 99999) {
        for (var x = 0; x < scoresumall.length; x++) {
            var y = 0;
            y = y + Number(lockg1[x].value);
            y = y + Number(lockg2[x].value);
            y = y + Number(lockg3[x].value);
            y = y + Number(lockg4[x].value);
            y = y + Number(lockg5[x].value);
            y = y + Number(lockg6[x].value);
            y = y + Number(lockg7[x].value);
            y = y + Number(lockg8[x].value);
            y = y + Number(lockg9[x].value);
            y = y + Number(lockg10[x].value);
            y = y + Number(lockg11[x].value);
            y = y + Number(lockg12[x].value);
            y = y + Number(lockg13[x].value);
            y = y + Number(lockg14[x].value);
            y = y + Number(lockg15[x].value);
            y = y + Number(lockg16[x].value);
            y = y + Number(lockg17[x].value);
            y = y + Number(lockg18[x].value);
            y = y + Number(lockg19[x].value);
            y = y + Number(lockg20[x].value);
            y = y + Number(lockcw1[x].value);
            y = y + Number(lockcw2[x].value);
            y = y + Number(lockcw3[x].value);
            y = y + Number(lockcw4[x].value);
            y = y + Number(lockcw5[x].value);
            y = y + Number(lockcw6[x].value);
            y = y + Number(lockcw7[x].value);
            y = y + Number(lockcw8[x].value);
            y = y + Number(lockcw9[x].value);
            y = y + Number(lockcw10[x].value);
            y = y + Number(lockcw11[x].value);
            y = y + Number(lockcw12[x].value);
            y = y + Number(lockcw13[x].value);
            y = y + Number(lockcw14[x].value);
            y = y + Number(lockcw15[x].value);
            y = y + Number(lockcw16[x].value);
            y = y + Number(lockcw17[x].value);
            y = y + Number(lockcw18[x].value);
            y = y + Number(lockcw19[x].value);
            y = y + Number(lockcw20[x].value);
            y = y + Number(lockmidterm[x].value);
            y = y + Number(lockfinalterm[x].value);

            scoresumall[x].value = y;
        }
    }
    else if (index != null) {
        var x = index - 1;
        var y = 0;
        y = y + Number(lockg1[x].value);
        y = y + Number(lockg2[x].value);
        y = y + Number(lockg3[x].value);
        y = y + Number(lockg4[x].value);
        y = y + Number(lockg5[x].value);
        y = y + Number(lockg6[x].value);
        y = y + Number(lockg7[x].value);
        y = y + Number(lockg8[x].value);
        y = y + Number(lockg9[x].value);
        y = y + Number(lockg10[x].value);
        y = y + Number(lockg11[x].value);
        y = y + Number(lockg12[x].value);
        y = y + Number(lockg13[x].value);
        y = y + Number(lockg14[x].value);
        y = y + Number(lockg15[x].value);
        y = y + Number(lockg16[x].value);
        y = y + Number(lockg17[x].value);
        y = y + Number(lockg18[x].value);
        y = y + Number(lockg19[x].value);
        y = y + Number(lockg20[x].value);
        y = y + Number(lockcw1[x].value);
        y = y + Number(lockcw2[x].value);
        y = y + Number(lockcw3[x].value);
        y = y + Number(lockcw4[x].value);
        y = y + Number(lockcw5[x].value);
        y = y + Number(lockcw6[x].value);
        y = y + Number(lockcw7[x].value);
        y = y + Number(lockcw8[x].value);
        y = y + Number(lockcw9[x].value);
        y = y + Number(lockcw10[x].value);
        y = y + Number(lockcw11[x].value);
        y = y + Number(lockcw12[x].value);
        y = y + Number(lockcw13[x].value);
        y = y + Number(lockcw14[x].value);
        y = y + Number(lockcw15[x].value);
        y = y + Number(lockcw16[x].value);
        y = y + Number(lockcw17[x].value);
        y = y + Number(lockcw18[x].value);
        y = y + Number(lockcw19[x].value);
        y = y + Number(lockcw20[x].value);
        y = y + Number(lockmidterm[x].value);
        y = y + Number(lockfinalterm[x].value);

        scoresumall[x].value = y;
    }

}



function checkstatus() {

    var stdout1 = document.getElementsByClassName("stdout1");
    var stdout2 = document.getElementsByClassName("stdout2");
    var stdout3 = document.getElementsByClassName("stdout3");
    var stdout4 = document.getElementsByClassName("stdout4");
    var stdout5 = document.getElementsByClassName("stdout5");
    var stdout6 = document.getElementsByClassName("stdout6");
    var stdout7 = document.getElementsByClassName("stdout7");
    var stdout8 = document.getElementsByClassName("stdout8");
    var stdout9 = document.getElementsByClassName("stdout9");
    var stdout10 = document.getElementsByClassName("stdout10");
    var stdout11 = document.getElementsByClassName("stdout11");
    var stdout12 = document.getElementsByClassName("stdout12");
    var stdout13 = document.getElementsByClassName("stdout13");
    var stdout14 = document.getElementsByClassName("stdout14");
    var stdout15 = document.getElementsByClassName("stdout15");
    var stdout16 = document.getElementsByClassName("stdout16");
    var stdout17 = document.getElementsByClassName("stdout17");
    var stdout18 = document.getElementsByClassName("stdout18");
    var stdout19 = document.getElementsByClassName("stdout19");
    var stdout20 = document.getElementsByClassName("stdout20");
    var stdout21 = document.getElementsByClassName("stdout21");
    var stdout22 = document.getElementsByClassName("stdout22");
    var stdout23 = document.getElementsByClassName("stdout23");
    var stdout24 = document.getElementsByClassName("stdout24");
    var stdout25 = document.getElementsByClassName("stdout25");
    var stdout26 = document.getElementsByClassName("stdout26");
    var stdout27 = document.getElementsByClassName("stdout27");
    var stdout28 = document.getElementsByClassName("stdout28");
    var stdout29 = document.getElementsByClassName("stdout29");
    var stdout30 = document.getElementsByClassName("stdout30");
    var stdout31 = document.getElementsByClassName("stdout31");
    var stdout32 = document.getElementsByClassName("stdout32");
    var stdout33 = document.getElementsByClassName("stdout33");
    var stdout34 = document.getElementsByClassName("stdout34");
    var stdout35 = document.getElementsByClassName("stdout35");
    var stdout36 = document.getElementsByClassName("stdout36");
    var stdout37 = document.getElementsByClassName("stdout37");
    var stdout38 = document.getElementsByClassName("stdout38");
    var stdout39 = document.getElementsByClassName("stdout39");
    var stdout40 = document.getElementsByClassName("stdout40");
    var stdout41 = document.getElementsByClassName("stdout41");
    var stdout42 = document.getElementsByClassName("stdout42");
    var stdout43 = document.getElementsByClassName("stdout43");
    var stdout44 = document.getElementsByClassName("stdout44");
    var stdout45 = document.getElementsByClassName("stdout45");
    var stdout46 = document.getElementsByClassName("stdout46");
    var stdout47 = document.getElementsByClassName("stdout47");
    var stdout48 = document.getElementsByClassName("stdout48");
    var stdout49 = document.getElementsByClassName("stdout49");
    var stdout50 = document.getElementsByClassName("stdout50");
    var stdout51 = document.getElementsByClassName("stdout51");
    var stdout52 = document.getElementsByClassName("stdout52");
    var stdout53 = document.getElementsByClassName("stdout53");
    var stdout54 = document.getElementsByClassName("stdout54");
    var stdout55 = document.getElementsByClassName("stdout55");
    var stdout56 = document.getElementsByClassName("stdout56");
    var stdout57 = document.getElementsByClassName("stdout57");
    var stdout58 = document.getElementsByClassName("stdout58");
    var stdout59 = document.getElementsByClassName("stdout59");
    var stdout60 = document.getElementsByClassName("stdout60");
    var stdout61 = document.getElementsByClassName("stdout61");
    var stdout62 = document.getElementsByClassName("stdout62");
    var stdout63 = document.getElementsByClassName("stdout63");
    var stdout64 = document.getElementsByClassName("stdout64");
    var stdout65 = document.getElementsByClassName("stdout65");
    var stdout66 = document.getElementsByClassName("stdout66");
    var stdout67 = document.getElementsByClassName("stdout67");
    var stdout68 = document.getElementsByClassName("stdout68");
    var stdout69 = document.getElementsByClassName("stdout69");
    var stdout70 = document.getElementsByClassName("stdout70");
    var stdout71 = document.getElementsByClassName("stdout71");
    var stdout72 = document.getElementsByClassName("stdout72");
    var stdout73 = document.getElementsByClassName("stdout73");
    var stdout74 = document.getElementsByClassName("stdout74");
    var stdout75 = document.getElementsByClassName("stdout75");
    var stdout76 = document.getElementsByClassName("stdout76");
    var stdout77 = document.getElementsByClassName("stdout77");
    var stdout78 = document.getElementsByClassName("stdout78");
    var stdout79 = document.getElementsByClassName("stdout79");
    var stdout80 = document.getElementsByClassName("stdout80");
    var stdout81 = document.getElementsByClassName("stdout81");
    var stdidlist = document.getElementsByClassName("stdidlist");
    var stdstatus = document.getElementsByClassName("stdstatus");

    var stdscore1 = document.getElementsByClassName("lockg1");
    var stdscore2 = document.getElementsByClassName("lockg2");
    var stdscore3 = document.getElementsByClassName("lockg3");
    var stdscore4 = document.getElementsByClassName("lockg4");
    var stdscore5 = document.getElementsByClassName("lockg5");
    var stdscore6 = document.getElementsByClassName("lockg6");
    var stdscore7 = document.getElementsByClassName("lockg7");
    var stdscore8 = document.getElementsByClassName("lockg8");
    var stdscore9 = document.getElementsByClassName("lockg9");
    var stdscore10 = document.getElementsByClassName("lockg10");
    var stdscore11 = document.getElementsByClassName("lockg11");
    var stdscore12 = document.getElementsByClassName("lockg12");
    var stdscore13 = document.getElementsByClassName("lockg13");
    var stdscore14 = document.getElementsByClassName("lockg14");
    var stdscore15 = document.getElementsByClassName("lockg15");
    var stdscore16 = document.getElementsByClassName("lockg16");
    var stdscore17 = document.getElementsByClassName("lockg17");
    var stdscore18 = document.getElementsByClassName("lockg18");
    var stdscore19 = document.getElementsByClassName("lockg19");
    var stdscore20 = document.getElementsByClassName("lockg20");

    var stdchewat1 = document.getElementsByClassName("lockcw1");
    var stdchewat2 = document.getElementsByClassName("lockcw2");
    var stdchewat3 = document.getElementsByClassName("lockcw3");
    var stdchewat4 = document.getElementsByClassName("lockcw4");
    var stdchewat5 = document.getElementsByClassName("lockcw5");
    var stdchewat6 = document.getElementsByClassName("lockcw6");
    var stdchewat7 = document.getElementsByClassName("lockcw7");
    var stdchewat8 = document.getElementsByClassName("lockcw8");
    var stdchewat9 = document.getElementsByClassName("lockcw9");
    var stdchewat10 = document.getElementsByClassName("lockcw10");
    var stdchewat11 = document.getElementsByClassName("lockcw11");
    var stdchewat12 = document.getElementsByClassName("lockcw12");
    var stdchewat13 = document.getElementsByClassName("lockcw13");
    var stdchewat14 = document.getElementsByClassName("lockcw14");
    var stdchewat15 = document.getElementsByClassName("lockcw15");
    var stdchewat16 = document.getElementsByClassName("lockcw16");
    var stdchewat17 = document.getElementsByClassName("lockcw17");
    var stdchewat18 = document.getElementsByClassName("lockcw18");
    var stdchewat19 = document.getElementsByClassName("lockcw19");
    var stdchewat20 = document.getElementsByClassName("lockcw20");

    var stdbehave1 = document.getElementsByClassName("behavetxt1");
    var stdbehave2 = document.getElementsByClassName("behavetxt2");
    var stdbehave3 = document.getElementsByClassName("behavetxt3");
    var stdbehave4 = document.getElementsByClassName("behavetxt4");
    var stdbehave5 = document.getElementsByClassName("behavetxt5");
    var stdbehave6 = document.getElementsByClassName("behavetxt6");
    var stdbehave7 = document.getElementsByClassName("behavetxt7");
    var stdbehave8 = document.getElementsByClassName("behavetxt8");
    var stdbehave9 = document.getElementsByClassName("behavetxt9");
    var stdbehave10 = document.getElementsByClassName("behavetxt10");

    var stdmid1 = document.getElementsByClassName("lockm1");
    var stdmid2 = document.getElementsByClassName("lockm2");
    var stdmid3 = document.getElementsByClassName("lockm3");
    var stdmid4 = document.getElementsByClassName("lockm4");
    var stdmid5 = document.getElementsByClassName("lockm5");
    var stdmid6 = document.getElementsByClassName("lockm6");
    var stdmid7 = document.getElementsByClassName("lockm7");
    var stdmid8 = document.getElementsByClassName("lockm8");
    var stdmid9 = document.getElementsByClassName("lockm9");
    var stdmid10 = document.getElementsByClassName("lockm10");
    var stdfinal1 = document.getElementsByClassName("lockf1");
    var stdfinal2 = document.getElementsByClassName("lockf2");
    var stdfinal3 = document.getElementsByClassName("lockf3");
    var stdfinal4 = document.getElementsByClassName("lockf4");
    var stdfinal5 = document.getElementsByClassName("lockf5");
    var stdfinal6 = document.getElementsByClassName("lockf6");
    var stdfinal7 = document.getElementsByClassName("lockf7");
    var stdfinal8 = document.getElementsByClassName("lockf8");
    var stdfinal9 = document.getElementsByClassName("lockf9");
    var stdfinal10 = document.getElementsByClassName("lockf10");
    var stdmidtotal = document.getElementsByClassName("lockmidterm");
    var stdfinaltotal = document.getElementsByClassName("lockfinalterm");
    var stdsamatanatotal = document.getElementsByClassName("samatscore");
    var stdreadwritetotal = document.getElementsByClassName("readscore");
    var stdbehavetotal = document.getElementsByClassName("goodbe");
    var special = document.getElementsByClassName("special");

    for (var x = 0; x < stdidlist.length; x++) {
        if (stdstatus[x].value == 1 || stdstatus[x].value == 2 || stdstatus[x].value == 3 || stdstatus[x].value == 5) {
            stdout1[x].classList.add('disable');
            stdout1[x].classList.add('disable3');
            stdout2[x].classList.add('disable');
            stdout2[x].classList.add('disable3');
            stdout3[x].classList.add('disable');
            stdout3[x].classList.add('disable3');
            stdout4[x].classList.add('disable');
            stdout4[x].classList.add('disable3');
            stdout5[x].classList.add('disable');
            stdout5[x].classList.add('disable3');
            stdout6[x].classList.add('disable');
            stdout6[x].classList.add('disable3');
            stdout7[x].classList.add('disable');
            stdout7[x].classList.add('disable3');
            stdout8[x].classList.add('disable');
            stdout8[x].classList.add('disable3');
            stdout9[x].classList.add('disable');
            stdout9[x].classList.add('disable3');
            stdout10[x].classList.add('disable');
            stdout10[x].classList.add('disable3');
            stdout11[x].classList.add('disable');
            stdout11[x].classList.add('disable3');
            stdout12[x].classList.add('disable');
            stdout12[x].classList.add('disable3');
            stdout13[x].classList.add('disable');
            stdout13[x].classList.add('disable3');
            stdout14[x].classList.add('disable');
            stdout14[x].classList.add('disable3');
            stdout15[x].classList.add('disable');
            stdout15[x].classList.add('disable3');
            stdout16[x].classList.add('disable');
            stdout16[x].classList.add('disable3');
            stdout17[x].classList.add('disable');
            stdout17[x].classList.add('disable3');
            stdout18[x].classList.add('disable');
            stdout18[x].classList.add('disable3');
            stdout19[x].classList.add('disable');
            stdout19[x].classList.add('disable3');
            stdout20[x].classList.add('disable');
            stdout20[x].classList.add('disable3');
            stdout21[x].classList.add('disable');
            stdout21[x].classList.add('disable3');
            stdout22[x].classList.add('disable');
            stdout22[x].classList.add('disable3');
            stdout23[x].classList.add('disable');
            stdout23[x].classList.add('disable3');
            stdout24[x].classList.add('disable');
            stdout24[x].classList.add('disable3');
            stdout25[x].classList.add('disable');
            stdout25[x].classList.add('disable3');
            stdout26[x].classList.add('disable');
            stdout26[x].classList.add('disable3');
            stdout27[x].classList.add('disable');
            stdout27[x].classList.add('disable3');
            stdout28[x].classList.add('disable');
            stdout28[x].classList.add('disable3');
            stdout29[x].classList.add('disable');
            stdout29[x].classList.add('disable3');
            stdout30[x].classList.add('disable');
            stdout30[x].classList.add('disable3');
            stdout31[x].classList.add('disable');
            stdout31[x].classList.add('disable3');
            stdout32[x].classList.add('disable');
            stdout32[x].classList.add('disable3');
            stdout33[x].classList.add('disable');
            stdout33[x].classList.add('disable3');
            stdout34[x].classList.add('disable');
            stdout34[x].classList.add('disable3');
            stdout35[x].classList.add('disable');
            stdout35[x].classList.add('disable3');
            stdout36[x].classList.add('disable');
            stdout36[x].classList.add('disable3');
            stdout37[x].classList.add('disable');
            stdout37[x].classList.add('disable3');
            stdout38[x].classList.add('disable');
            stdout38[x].classList.add('disable3');
            stdout39[x].classList.add('disable');
            stdout39[x].classList.add('disable3');
            stdout40[x].classList.add('disable');
            stdout40[x].classList.add('disable3');
            stdout41[x].classList.add('disable');
            stdout41[x].classList.add('disable3');
            stdout42[x].classList.add('disable');
            stdout42[x].classList.add('disable3');
            stdout43[x].classList.add('disable');
            stdout43[x].classList.add('disable3');
            stdout44[x].classList.add('disable');
            stdout44[x].classList.add('disable3');
            stdout45[x].classList.add('disable');
            stdout45[x].classList.add('disable3');
            stdout46[x].classList.add('disable');
            stdout46[x].classList.add('disable3');
            stdout47[x].classList.add('disable');
            stdout47[x].classList.add('disable3');
            stdout48[x].classList.add('disable');
            stdout48[x].classList.add('disable3');
            stdout49[x].classList.add('disable');
            stdout49[x].classList.add('disable3');
            stdout50[x].classList.add('disable');
            stdout50[x].classList.add('disable3');
            stdout51[x].classList.add('disable');
            stdout51[x].classList.add('disable3');
            stdout52[x].classList.add('disable');
            stdout52[x].classList.add('disable3');
            stdout53[x].classList.add('disable');
            stdout53[x].classList.add('disable3');
            stdout54[x].classList.add('disable');
            stdout54[x].classList.add('disable3');
            stdout55[x].classList.add('disable');
            stdout55[x].classList.add('disable3');
            stdout56[x].classList.add('disable');
            stdout56[x].classList.add('disable3');
            stdout57[x].classList.add('disable');
            stdout57[x].classList.add('disable3');
            stdout58[x].classList.add('disable');
            stdout58[x].classList.add('disable3');
            stdout59[x].classList.add('disable');
            stdout59[x].classList.add('disable3');
            stdout60[x].classList.add('disable');
            stdout60[x].classList.add('disable3');
            stdout61[x].classList.add('disable');
            stdout61[x].classList.add('disable3');
            stdout62[x].classList.add('disable');
            stdout62[x].classList.add('disable3');
            stdout63[x].classList.add('disable');
            stdout63[x].classList.add('disable3');
            stdout64[x].classList.add('disable');
            stdout64[x].classList.add('disable3');
            stdout65[x].classList.add('disable');
            stdout65[x].classList.add('disable3');
            stdout66[x].classList.add('disable');
            stdout66[x].classList.add('disable3');
            stdout67[x].classList.add('disable');
            stdout67[x].classList.add('disable3');
            stdout68[x].classList.add('disable');
            stdout68[x].classList.add('disable3');
            stdout69[x].classList.add('disable');
            stdout69[x].classList.add('disable3');
            stdout70[x].classList.add('disable');
            stdout70[x].classList.add('disable3');
            stdout71[x].classList.add('disable');
            stdout71[x].classList.add('disable3');
            stdout72[x].classList.add('disable');
            stdout72[x].classList.add('disable3');
            stdout73[x].classList.add('disable');
            stdout73[x].classList.add('disable3');
            stdout74[x].classList.add('disable');
            stdout74[x].classList.add('disable3');
            stdout75[x].classList.add('disable');
            stdout75[x].classList.add('disable3');
            stdout76[x].classList.add('disable');
            stdout76[x].classList.add('disable3');
            stdout77[x].classList.add('disable');
            stdout77[x].classList.add('disable3');
            stdout78[x].classList.add('disable');
            stdout78[x].classList.add('disable3');
            stdout79[x].classList.add('disable');
            stdout79[x].classList.add('disable3');
            stdout80[x].classList.add('disable');
            stdout80[x].classList.add('disable3');
            stdout81[x].classList.add('disable');
            stdout81[x].classList.add('disable3');

            stdscore1[x].classList.add('disable');
            stdscore2[x].classList.add('disable');
            stdscore3[x].classList.add('disable');
            stdscore4[x].classList.add('disable');
            stdscore5[x].classList.add('disable');
            stdscore6[x].classList.add('disable');
            stdscore7[x].classList.add('disable');
            stdscore8[x].classList.add('disable');
            stdscore9[x].classList.add('disable');
            stdscore10[x].classList.add('disable');
            stdscore11[x].classList.add('disable');
            stdscore12[x].classList.add('disable');
            stdscore13[x].classList.add('disable');
            stdscore14[x].classList.add('disable');
            stdscore15[x].classList.add('disable');
            stdscore16[x].classList.add('disable');
            stdscore17[x].classList.add('disable');
            stdscore18[x].classList.add('disable');
            stdscore19[x].classList.add('disable');
            stdscore20[x].classList.add('disable');

            stdchewat1[x].classList.add('disable');
            stdchewat2[x].classList.add('disable');
            stdchewat3[x].classList.add('disable');
            stdchewat4[x].classList.add('disable');
            stdchewat5[x].classList.add('disable');
            stdchewat6[x].classList.add('disable');
            stdchewat7[x].classList.add('disable');
            stdchewat8[x].classList.add('disable');
            stdchewat9[x].classList.add('disable');
            stdchewat10[x].classList.add('disable');
            stdchewat11[x].classList.add('disable');
            stdchewat12[x].classList.add('disable');
            stdchewat13[x].classList.add('disable');
            stdchewat14[x].classList.add('disable');
            stdchewat15[x].classList.add('disable');
            stdchewat16[x].classList.add('disable');
            stdchewat17[x].classList.add('disable');
            stdchewat18[x].classList.add('disable');
            stdchewat19[x].classList.add('disable');
            stdchewat20[x].classList.add('disable');

            stdbehave1[x].classList.add('disable');
            stdbehave2[x].classList.add('disable');
            stdbehave3[x].classList.add('disable');
            stdbehave4[x].classList.add('disable');
            stdbehave5[x].classList.add('disable');
            stdbehave6[x].classList.add('disable');
            stdbehave7[x].classList.add('disable');
            stdbehave8[x].classList.add('disable');
            stdbehave9[x].classList.add('disable');
            stdbehave10[x].classList.add('disable');

            stdmid1[x].classList.add('disable');
            stdmid2[x].classList.add('disable');
            stdmid3[x].classList.add('disable');
            stdmid4[x].classList.add('disable');
            stdmid5[x].classList.add('disable');
            stdmid6[x].classList.add('disable');
            stdmid7[x].classList.add('disable');
            stdmid8[x].classList.add('disable');
            stdmid9[x].classList.add('disable');
            stdmid10[x].classList.add('disable');

            stdfinal1[x].classList.add('disable');
            stdfinal2[x].classList.add('disable');
            stdfinal3[x].classList.add('disable');
            stdfinal4[x].classList.add('disable');
            stdfinal5[x].classList.add('disable');
            stdfinal6[x].classList.add('disable');
            stdfinal7[x].classList.add('disable');
            stdfinal8[x].classList.add('disable');
            stdfinal9[x].classList.add('disable');
            stdfinal10[x].classList.add('disable');

            stdmidtotal[x].classList.add('disable');
            stdfinaltotal[x].classList.add('disable');
            stdsamatanatotal[x].classList.add('disable');
            stdreadwritetotal[x].classList.add('disable');
            stdbehavetotal[x].classList.add('disable');
            special[x].classList.add('disable');

        }
    }
}

function start() {
    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0];
    var idlv = split[1];
    var idlv2 = split[2];
    var term = split[3];
    var id2 = split[4];
    var mode = split[5].split('=');

    //var t0 = performance.now();
    setupddl();
    changeddl();
    changename();
    editddl();
    autobehave();
    CompareDates(99999);
    auto();
    lockpage();
    checkstatus();
    editPillbox();


    //var t1 = performance.now();
    //console.log("Call to doSomething took " + (t1 - t0) + " milliseconds.");
    var cog1 = document.getElementsByClassName("cog1");
    var cog2 = document.getElementsByClassName("cog2");
    var check5 = document.getElementsByClassName("check5");
    var scorebox = document.getElementsByClassName("scorebox");
    var btnok = document.getElementsByClassName("btnok");
    var btnerror = document.getElementsByClassName("btnerror");
    var btnback = document.getElementsByClassName("btnback");
    var lastbtn = document.getElementsByClassName("lastbtn");

    var periodnow = document.getElementsByClassName("periodnow");
    var periodsubmit = document.getElementsByClassName("periodsubmit");
    periodsubmit[0].value = periodnow[0].value;

    var periodclass = document.getElementsByClassName("periodclass");
    var period = document.getElementsByClassName("period");
    if (period[0].textContent.length < 5)
        periodclass[0].classList.add('hidden');

    var viplogin = document.getElementsByClassName("viplogin");
    if (viplogin[0].value == "2") {
        //btnok[0].classList.add('hidden');
        //btnerror[0].classList.add('hidden');
    }

    check5[0].checked == true;
    cog1[0].value = "0";
    cog2[0].value = "1";

    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0];
    var idlv = split[1];
    var idlv2 = split[2];
    var term = split[3];
    var id2 = split[4];
    var mode = split[5].split('=');

    if (mode[1] == "3") {
        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 1-5";
        btnok[0].value = "พิมพ์ ป.พ. 5";
        btnerror[0].value = "พิมพ์ ป.พ. 5";
        btnback[0].value = "ย้อนกลับ";
        lastbtn[0].value = "บันทึกครั้งสุดท้าย";
        nextpage();
    }
    else if (mode[1] != "EN") {
        scorebox[0].value = "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่ 1-5";
        btnok[0].value = "บันทึกร่าง";
        btnerror[0].value = "บันทึกร่าง";
        btnback[0].value = "ย้อนกลับ";
        lastbtn[0].value = "บันทึกครั้งสุดท้าย";
    }
    else {
        scorebox[0].value = "Exercise 1-5";
        btnok[0].value = "Submit";
        btnerror[0].value = "Submit";
        btnback[0].value = "Back";
        lastbtn[0].value = "Last Submit";
    }

    var name1 = document.getElementsByClassName("test1name");
    var name2 = document.getElementsByClassName("test2name");
    var name3 = document.getElementsByClassName("test3name");
    var name4 = document.getElementsByClassName("test4name");
    var name5 = document.getElementsByClassName("test5name");
    var name6 = document.getElementsByClassName("test6name");
    var name7 = document.getElementsByClassName("test7name");
    var name8 = document.getElementsByClassName("test8name");
    var name9 = document.getElementsByClassName("test9name");
    var name10 = document.getElementsByClassName("test10name");
    var name11 = document.getElementsByClassName("test11name");
    var name12 = document.getElementsByClassName("test12name");
    var name13 = document.getElementsByClassName("test13name");
    var name14 = document.getElementsByClassName("test14name");
    var name15 = document.getElementsByClassName("test15name");
    var name16 = document.getElementsByClassName("test16name");
    var name17 = document.getElementsByClassName("test17name");
    var name18 = document.getElementsByClassName("test18name");
    var name19 = document.getElementsByClassName("test19name");
    var name20 = document.getElementsByClassName("test20name");
    var set1 = document.getElementsByClassName("set1name");
    var set2 = document.getElementsByClassName("set2name");
    var set3 = document.getElementsByClassName("set3name");
    var set4 = document.getElementsByClassName("set4name");
    var set5 = document.getElementsByClassName("set5name");
    var set6 = document.getElementsByClassName("set6name");
    var set7 = document.getElementsByClassName("set7name");
    var set8 = document.getElementsByClassName("set8name");
    var set9 = document.getElementsByClassName("set9name");
    var set10 = document.getElementsByClassName("set10name");
    var set11 = document.getElementsByClassName("set11name");
    var set12 = document.getElementsByClassName("set12name");
    var set13 = document.getElementsByClassName("set13name");
    var set14 = document.getElementsByClassName("set14name");
    var set15 = document.getElementsByClassName("set15name");
    var set16 = document.getElementsByClassName("set16name");
    var set17 = document.getElementsByClassName("set17name");
    var set18 = document.getElementsByClassName("set18name");
    var set19 = document.getElementsByClassName("set19name");
    var set20 = document.getElementsByClassName("set20name");
    var setb1 = document.getElementsByClassName("setnameb1");
    var setb2 = document.getElementsByClassName("setnameb2");
    var setb3 = document.getElementsByClassName("setnameb3");
    var setb4 = document.getElementsByClassName("setnameb4");
    var setb5 = document.getElementsByClassName("setnameb5");
    var setb6 = document.getElementsByClassName("setnameb6");
    var setb7 = document.getElementsByClassName("setnameb7");
    var setb8 = document.getElementsByClassName("setnameb8");
    var setb9 = document.getElementsByClassName("setnameb9");
    var setb10 = document.getElementsByClassName("setnameb10");
    var nameb1 = document.getElementsByClassName("testb1name");
    var nameb2 = document.getElementsByClassName("testb2name");
    var nameb3 = document.getElementsByClassName("testb3name");
    var nameb4 = document.getElementsByClassName("testb4name");
    var nameb5 = document.getElementsByClassName("testb5name");
    var nameb6 = document.getElementsByClassName("testb6name");
    var nameb7 = document.getElementsByClassName("testb7name");
    var nameb8 = document.getElementsByClassName("testb8name");
    var nameb9 = document.getElementsByClassName("testb9name");
    var nameb10 = document.getElementsByClassName("testb10name");

    var namecw1 = document.getElementsByClassName("testcw1name");
    var namecw2 = document.getElementsByClassName("testcw2name");
    var namecw3 = document.getElementsByClassName("testcw3name");
    var namecw4 = document.getElementsByClassName("testcw4name");
    var namecw5 = document.getElementsByClassName("testcw5name");
    var namecw6 = document.getElementsByClassName("testcw6name");
    var namecw7 = document.getElementsByClassName("testcw7name");
    var namecw8 = document.getElementsByClassName("testcw8name");
    var namecw9 = document.getElementsByClassName("testcw9name");
    var namecw10 = document.getElementsByClassName("testcw10name");
    var namecw11 = document.getElementsByClassName("testcw11name");
    var namecw12 = document.getElementsByClassName("testcw12name");
    var namecw13 = document.getElementsByClassName("testcw13name");
    var namecw14 = document.getElementsByClassName("testcw14name");
    var namecw15 = document.getElementsByClassName("testcw15name");
    var namecw16 = document.getElementsByClassName("testcw16name");
    var namecw17 = document.getElementsByClassName("testcw17name");
    var namecw18 = document.getElementsByClassName("testcw18name");
    var namecw19 = document.getElementsByClassName("testcw19name");
    var namecw20 = document.getElementsByClassName("testcw20name");
    var setcw1 = document.getElementsByClassName("set1namecw");
    var setcw2 = document.getElementsByClassName("set2namecw");
    var setcw3 = document.getElementsByClassName("set3namecw");
    var setcw4 = document.getElementsByClassName("set4namecw");
    var setcw5 = document.getElementsByClassName("set5namecw");
    var setcw6 = document.getElementsByClassName("set6namecw");
    var setcw7 = document.getElementsByClassName("set7namecw");
    var setcw8 = document.getElementsByClassName("set8namecw");
    var setcw9 = document.getElementsByClassName("set9namecw");
    var setcw10 = document.getElementsByClassName("set10namecw");
    var setcw11 = document.getElementsByClassName("set11namecw");
    var setcw12 = document.getElementsByClassName("set12namecw");
    var setcw13 = document.getElementsByClassName("set13namecw");
    var setcw14 = document.getElementsByClassName("set14namecw");
    var setcw15 = document.getElementsByClassName("set15namecw");
    var setcw16 = document.getElementsByClassName("set16namecw");
    var setcw17 = document.getElementsByClassName("set17namecw");
    var setcw18 = document.getElementsByClassName("set18namecw");
    var setcw19 = document.getElementsByClassName("set19namecw");
    var setcw20 = document.getElementsByClassName("set20namecw");

    var el1 = $('.test1name');
    var el2 = $('.test2name');
    var el3 = $('.test3name');
    var el4 = $('.test4name');
    var el5 = $('.test5name');
    var el6 = $('.test6name');
    var el7 = $('.test7name');
    var el8 = $('.test8name');
    var el9 = $('.test9name');
    var el10 = $('.test10name');
    var el11 = $('.test11name');
    var el12 = $('.test12name');
    var el13 = $('.test13name');
    var el14 = $('.test14name');
    var el15 = $('.test15name');
    var el16 = $('.test16name');
    var el17 = $('.test17name');
    var el18 = $('.test18name');
    var el19 = $('.test19name');
    var el20 = $('.test20name');
    var elb1 = $('.testb1name');
    var elb2 = $('.testb2name');
    var elb3 = $('.testb3name');
    var elb4 = $('.testb4name');
    var elb5 = $('.testb5name');
    var elb6 = $('.testb6name');
    var elb7 = $('.testb7name');
    var elb8 = $('.testb8name');
    var elb9 = $('.testb9name');
    var elb10 = $('.testb10name');
    var elc1 = $('.testcw1name');
    var elc2 = $('.testcw2name');
    var elc3 = $('.testcw3name');
    var elc4 = $('.testcw4name');
    var elc5 = $('.testcw5name');
    var elc6 = $('.testcw6name');
    var elc7 = $('.testcw7name');
    var elc8 = $('.testcw8name');
    var elc9 = $('.testcw9name');
    var elc10 = $('.testcw10name');
    var elc11 = $('.testcw11name');
    var elc12 = $('.testcw12name');
    var elc13 = $('.testcw13name');
    var elc14 = $('.testcw14name');
    var elc15 = $('.testcw15name');
    var elc16 = $('.testcw16name');
    var elc17 = $('.testcw17name');
    var elc18 = $('.testcw18name');
    var elc19 = $('.testcw19name');
    var elc20 = $('.testcw20name');


    // Shows tooltip with title: "My new title"

    el1.protipShow({
        title: set1[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el2.protipShow({
        title: set2[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el3.protipShow({
        title: set3[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el4.protipShow({
        title: set4[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el5.protipShow({
        title: set5[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el6.protipShow({
        title: set6[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el7.protipShow({
        title: set7[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el8.protipShow({
        title: set8[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el9.protipShow({
        title: set9[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el10.protipShow({
        title: set10[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el11.protipShow({
        title: set11[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el12.protipShow({
        title: set12[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el13.protipShow({
        title: set13[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el14.protipShow({
        title: set14[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el15.protipShow({
        title: set15[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el16.protipShow({
        title: set16[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el17.protipShow({
        title: set17[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el18.protipShow({
        title: set18[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el19.protipShow({
        title: set19[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    el20.protipShow({
        title: set20[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb1.protipShow({
        title: setb1[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb2.protipShow({
        title: setb2[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb3.protipShow({
        title: setb3[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb4.protipShow({
        title: setb4[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb5.protipShow({
        title: setb5[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb6.protipShow({
        title: setb6[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb7.protipShow({
        title: setb7[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb8.protipShow({
        title: setb8[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb9.protipShow({
        title: setb9[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elb10.protipShow({
        title: setb10[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc1.protipShow({
        title: setcw1[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc2.protipShow({
        title: setcw2[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc3.protipShow({
        title: setcw3[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc4.protipShow({
        title: setcw4[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc5.protipShow({
        title: setcw5[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc6.protipShow({
        title: setcw6[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc7.protipShow({
        title: setcw7[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc8.protipShow({
        title: setcw8[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc9.protipShow({
        title: setcw9[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc10.protipShow({
        title: setcw10[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc11.protipShow({
        title: setcw11[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc12.protipShow({
        title: setcw12[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc13.protipShow({
        title: setcw13[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc14.protipShow({
        title: setcw14[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc15.protipShow({
        title: setcw15[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc16.protipShow({
        title: setcw16[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc17.protipShow({
        title: setcw17[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc18.protipShow({
        title: setcw18[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc19.protipShow({
        title: setcw19[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    elc20.protipShow({
        title: setcw20[0].value,
        trigger: 'hover',
        scheme: 'black'
    });
    var protipshow = document.getElementsByClassName("protip-show");

    while (protipshow.length != 0) {
        protipshow[0].classList.remove('protip-show');
    }

    setTimeout(function () {
        removeprotip();
    }, 1);

    if (mode[1] != "3") {
        $('#loading').hide();
    }

}

function removeprotip() {
    var protipshow = document.getElementsByClassName("protip-show");

    while (protipshow.length != 0) {
        protipshow[0].classList.remove('protip-show');
    }

}

$(window).on('load', function () {
    start();
});

function changepage(value) {
    var full = window.location.href;
    var half = full.split('?');
    var split = half[1].split('&');
    var year = split[0];
    var idlv = split[1];
    var idlv2 = split[2];
    var term = split[3];
    var id = split[4];

    if (value == 1) {

        window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id + "&mode=EN");
    }
    if (value == 2) {

        window.location = ("Webform2.aspx?" + year + "&" + idlv + "&" + idlv2 + "&" + term + "&" + id + "&mode=1");
    }

}

function popup() {

    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the image and insert it inside the modal - use its "alt" text as a caption
    var img = document.getElementById('myImg');
    var modalImg = document.getElementById("img01");
    var captionText = document.getElementById("caption");
    modal.style.display = "block";
    modalImg.src = "https://i.imgur.com/4xpYhQI.png";
    captionText.innerHTML = "ตัวอย่างการกรอกข้อมูล";
}

function popup2() {

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close2")[0];
    var modal = document.getElementById('myModal');
    // When the user clicks on <span> (x), close the modal
    modal.style.display = "none";
}

function mdown() {
    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close2")[0];
    var modal = document.getElementById('myModal');
    // When the user clicks on <span> (x), close the modal
    modal.style.display = "none";
}

function w3_open() {
    document.getElementById("mySidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function OpenGroupSidebar() {
    document.getElementById("groupSidebar").style.display = "block";
    document.getElementById("myOverlay").style.display = "block";
}

function w3_close() {

    document.getElementById("mySidebar").style.display = "none";
    document.getElementById("groupSidebar").style.display = "none";
    document.getElementById("myOverlay").style.display = "none";
}

