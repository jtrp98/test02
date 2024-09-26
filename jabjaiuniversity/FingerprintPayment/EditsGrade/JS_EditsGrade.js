var availableValuestudent = [];
var dataRow;

var table;


//$(document)
//    .ajaxStart(function () {
//        $('#loading').show();
//    })
//    .ajaxStop(function () {
//        $('#loading').hide();
//    });

$(function () {

    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    getlistStd();

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

    ListddlYear();

    Search();

    $('#tb tbody').on('click', 'tr', function () {
        var data = table.row(this).data();
        // alert('You clicked on ' + data[2] + '\'s row');
        if (data != undefined) {
            console.log(data);
            $('#txtnumberYear').html(data.numberYear);
            $('#txtGradeDetailId').html(data.nGradeDetailId);
            $('#txtSubject').html(data.sPlaneName);
            $('#txtGradeCal').html(data.gradeCal);
            $('#inputGradeEdits').val(data.gradeSet);
            $('#GetScore100').val(data.getScore100);
            if (data.gradeSet !== "") document.getElementById("gradeNew").value = data.gradeSet;
            else document.getElementById("gradeNew").value = -1;
        }
        else {
            document.getElementById("gradeNew").value = -1
        }
    });

});

function isNumberKey(e, control) {
    var $this = $(control);
    var text = $(control).val();
    var controlName = $(control).attr('name');
    var keyPressed = 0;


    if (($(control).attr('name') != 'undefined' || $(control).attr('name') != null) && ((controlName == "GetSamattana" || controlName == "GetBehaviorLabel" || controlName == "GetReadWrite") && event.which == 46)) {
        event.preventDefault();
    }
    else if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    else if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {

            if ($(control).val().substring($(control).val().indexOf('.')).length > 3) {
                $(control).val($(control).val().substring(0, $(control).val().indexOf('.') + 3));
            }
        }, 1);
    }
    else if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(control)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
    else if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
        event.preventDefault();
    }
}

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
        }
    });
}

function getlistStd() {
    availableValuestudent = [];
    $.ajax({
        url: "/EditsGrade/LogicEditsGrade.ashx?mode=GetStdList",
        dataType: "json",
        success: function (objjson) {
            console.log(objjson);
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


function Search() {
    var rowCount = $('#tb >tbody >tr').length;
    //console.log(rowCount);
    if (rowCount !== 0) {
        table.clear();
        table.destroy();
    }

    CallTable();
}

function CallTable() {

    //console.log(dataObjectArray);
    var id = $("#txtId").val();
    var year = $("#ddlYear").val();


    //$.ajax({
    //    url: "/EditsGrade/LogicEditsGrade.ashx?mode=GetData&id=" + id
    //        + "&year=" + year,
    //    dataType: "json",
    //    success: function (objjson) {
    //        console.log(objjson);
    //        //$.each(objjson, function (index) {
    //        //    var newObject = {
    //        //        label: objjson[index].Name,
    //        //        value: objjson[index].StdId,
    //        //        code: objjson[index].Code
    //        //    };
    //        //    availableValuestudent[index] = newObject;
    //        //});
    //    },
    //    fail: function (objjson) {
    //        console.log('fail');
    //        console.log(objjson);
    //    },
    //    error: function (objjson) {
    //        console.log('error');
    //        console.log(objjson);
    //    }
    //});




    //if (plantId.length > 0 && nterm.length > 0 && gradeId > 0) {
        table = $('#tb').DataTable({
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
            'rowsGroup': [0],
            ajax: {
                url: "/EditsGrade/LogicEditsGrade.ashx?mode=GetData&id=" + id
                    + "&year=" + year,
                dataType: "json",
                dataSrc: ""
            },

            "aoColumns": [
                {
                    "title": "ปีการศึกษา",
                    "data": "numberYear",
                    "width": "10%",
                    "className": 'dt-center',
                    //"visible": false,
                },
                {
                    "title": "ภาคเรียน",
                    "data": "sTerm",
                    "width": "10%",
                    "className": 'dt-center'
                },
                {
                    "title": "ชื่อวิชา",
                    "data": "sPlaneName",
                    "width": "40%",
                    "className": 'dt-center'
                },
                {
                    "title": "เกรดจากการคำนวณ",
                    "data": "gradeCal",
                    "width": "15%",
                    "className": 'dt-center'
                },
                {
                    "title": "เกรดที่ทำการแก้ไข",
                    "data": "gradeSet",
                    "width": "15%",
                    "className": 'dt-center'
                },
                {
                    "title": "คะแนนที่ทำการแก้ไข",
                    "data": "getScore100",
                    "width": "15%",
                    "className": 'dt-center'
                },
                {
                    "title": " ",
                    "data": "nGradeDetailId",
                    "width": "10%",
                    "className": 'dt-center',
                    "render": function (data, type, full, meta) {
                        return ("<button type='button' class='btn btn-warning' name='btnEditOnRow' data-toggle='modal' data-target='#myModal' ><i class='fas fa-edit'></i></button>");
                    }
                }
            ]
        });
    //}
    //else alert('plantId =' + plantId + "\n"
    //    + 'nterm =' + nterm + "\n"
    //    + 'gradeId =' + gradeId
    //);
}

//function OpenDialog(data) {
//    $('#inputGradeEdits').val(data);
//}

function EditsGrade() {
    var numberYear = $('#txtnumberYear').html();
    var id = $('#txtGradeDetailId').html();
    var gradeCal = $('#txtGradeCal').html();
    var gradeSet = document.getElementById("gradeNew").value;
    var getScore100 = $('#GetScore100').val();
    
    //if (gradeSet == -1) {
    //    alert("กรุณาเลือกเกรดที่ทำการแก้ไข");
    //}
    //else
    if (getScore100 != "" && (gradeSet == -1 || gradeSet == ""))
    {
        alert("กรุณาเลือกเกรดที่ทำการแก้ไข");
    }
    else {
        $.ajax({
            url: "/EditsGrade/LogicEditsGrade.ashx?mode=EditsGrade&gradeDetail=" + id
                + "&gradeCal=" + gradeCal
                + "&gradeSet=" + gradeSet
                + "&numberYear=" + numberYear
                + "&getScore100=" + getScore100
            ,
            dataType: "json",
            success: function (objjson) {
                console.log(objjson);
                $.each(objjson, function (index) {
                    //var newObject = {
                    //    label: objjson[index].Name,
                    //    value: objjson[index].StdId,
                    //    code: objjson[index].Code
                    //};
                });
            }
        });
    
        $('#close').click();
        Search();
    }
}