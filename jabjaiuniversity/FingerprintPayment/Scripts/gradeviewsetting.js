var doAjax_params_default = {
    'url': null,
    'requestType': "GET",
    'contentType': 'application/json; charset=utf-8',
    'dataType': 'json',
    'data': {},
    'beforeSendCallbackFunction': null,
    'successCallbackFunction': null,
    'completeCallbackFunction': null,
    'errorCallBackFunction': null,
};
var tableName = '#dtGridViewSetting';
var table;
var tableRoomListBlock = $('#ClassRoomBlockList').DataTable();
var tableStudentBlockList;
$(document).ready(function () {
    LoadGradeViewSettingDetails();

    $("#btnClassRoomBlockDataSave").click(function () {
        console.log("Save Called");
        $("#ModalForBlockClass").modal("hide");
        $("body").mLoading("");
        var GradeViewRoomListBlockRequest = [];
        var rowCount = 0;
        tableRoomListBlock.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            GradeViewRoomListBlockRequest.push({ GradeViewSettingId: $("#gradeViewSettingId").val(), NTermSubLevel2: data.NTermSubLevel2, RoomListSettingId: data.RoomListSettingId, IsRoomBlocked: $($("input[name='chkClassRoom[]']")[rowCount]).is(":checked"), NTerm : $("#nTerm").val() })
            rowCount++;
        });


        $.ajax({
            url: "../../api/GradeViewSettings/GradeViewRoomListBlock",
            dataType: "json",
            type: "POST",
            data: JSON.stringify(GradeViewRoomListBlockRequest),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
               /* $("#ModalForBlockClass").modal("hide");*/
                $("body").mLoading("hide");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });

    });

    $("#btnStudentBlockDataSave").click(function () {
        console.log("Save Called");
        $("#ModalForBlockStudent").modal("hide");
        $("body").mLoading("");
        var GradeViewStudentBlockRequest = [];
        var rowCount = 0;
        tableStudentBlockList.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            GradeViewStudentBlockRequest.push({ SId: data.SId, GradeViewSettingId: $("#gradeViewSettingId").val(), StudentBlockListSettingId: data.StudentBlockListSettingId, IsStudentBlocked: $($("input[name='chkStudent[]']")[rowCount]).is(":checked"), NTerm: $("#nTerm").val() })
            rowCount++;
        });


        $.ajax({
            url: "../../api/GradeViewSettings/GradeViewStudentBlock",
            dataType: "json",
            type: "POST",
            data: JSON.stringify(GradeViewStudentBlockRequest),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                /* $("#ModalForBlockClass").modal("hide");*/
                $("body").mLoading("hide");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });

    });
});



function LoadGradeViewSettingDetails() {
    $("body").mLoading();
    if (table != undefined) {
        table.destroy();
        table.clear().draw();
    }
    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/GradeViewSettings/GetGradeViewSettingDetails";
    params['successCallbackFunction'] = bindGradeViewSettingDetails;
    doAjax(params);
}
var response;
var bindGradeViewSettingDetails = function BindGradeViewSettingDetails(data) {
    //table.destroy();
    //table.clear().draw();
    $(tableName + " tbody").empty();
    response = data;
    console.log(response);
    table = $(tableName).DataTable({
        bAutoWidth: false,
        ordering: false,
        paging: false,
        searching: false,
        lengthChange: false,
        bInfo: false,
        scrollCollapse: true,
        fixedHeader: false,
        keys: false,
        data: response.data,
        columns: response.column,
        //fnRowCallback: function (nRow, aData, iDisplayIndex) {
        //    decorateRow(nRow, (aData["nStudentStatus"] == null) ? 0 : aData["nStudentStatus"], iDisplayIndex);
        //    return nRow;
        //},
        fnInitComplete: function () {
            // Event handler to be fired when rendering is complete (Turn off Loading gif for example)
            console.log('Datatable rendering complete');
            $("body").mLoading('hide');

        },
        columnDefs: ColumnDef(response.GradeViewAutoBlock)
    });
}

function ColumnDef(gradeViewAutoBlock) {
    var opts = "";
    opts = "<option value=1>100</options>";
    opts = opts + "<option value=0>50</options>";
    var columnDef = [
        {  //Term
            width: "5%",
            render: function (data, type, row, meta) {
                return data;
            },
            targets: 5
        },
        { // Mid Term
            width:"5%",
            render: function (data, type, row, meta) {
                //var gradeViewFor100 = '<select style="width:55px !important" onchange="GradeViewOnChange(this, ' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">' + opts + '</select>';
                //var data = "value=" + ((data == true) ? "1" : "0") + "";
                //console.log("GradeView100 " + data);
                //return gradeViewFor100.replace(data, data + ' selected ');

                return '<input type="checkbox" id="chkMidTerm" ' + ((data == true) ? 'checked' : '') + ' name="chkMidTerm" onchange="MidTermOnChange(this, ' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">';
                
            },
            targets: 6
        },
        { //Final Term
            width: "5%",
            render: function (data, type, row, meta) {
                //var gradeViewFor100 = '<select style="width:55px !important" onchange="GradeViewOnChange(this, ' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">' + opts + '</select>';
                //var data = "value=" + ((data == true) ? "1" : "0") + "";
                //console.log("GradeView100 " + data);
                //return gradeViewFor100.replace(data, data + ' selected ');

                return '<input type="checkbox" id="chkFinalTerm" ' + ((data == true) ? 'checked' : '') + ' name="chkFinalTerm" onchange="FinalTermOnChange(this, ' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">';

            },
            targets: 7
        },
        {
            width: "5%",
            render: function (data, type, row, meta) {
                var button = "";

                button += '<div class="dropdown" >';
                button += '    <button class="btn btn-primary dropdown-toggle success" type="button" data-toggle="dropdown">';
                button += 'บัญชีดำ <span class="caret" />';
                button += '    </button>';
                button += '    <ul class="dropdown-menu" style="font-size:24px">';
                
                console.log(gradeViewAutoBlock);
                if (!gradeViewAutoBlock) {
                    button += '        <li><a onclick="BlockClass(' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">ห้องบล็อก</a></li>';
                    button += '<li><a class="lnkBlockStudent" onclick="BlockStudent(' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">บล็อกนักเรียน</a></li>';
                }
                button += '    </ul>';
                button += '</div>';

                //button += '&nbsp;&nbsp;<button type="button" class="btn btn-success" onclick="BlockClass(' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">ห้องบล็อก</button>'
                //button += '&nbsp;&nbsp;<button type="button" class="btn btn-success" onclick="BlockStudent(' + row["GradeViewSettingId"] + ',\'' + row["nTerm"] + '\',' + row["nYear"] + ')">บล็อกนักเรียน</button>'
                return button;
                
            },
            targets: 8
        },

    ];

    return columnDef;
}

function OpenGradeViewFor100() {
    $("body").mLoading();

    $.ajax({
        url: "../../api/GradeViewSettings/GetGradeViewFor100",
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            console.log(response);
            if (response.GradeViewFor100) {
                $("#GradeViewFor100").prop("checked", true);
            }
            else {
                $("#GradeViewFor50").prop("checked", true);
            }

            if (response.GradeViewAutoBlock) {
                $("#GradeViewAutoBlock").prop("checked", true);
            }
            else {
                $("#GradeViewAutoBlock").prop("checked", false);
            }


            $("#GradeViewFor100Setting").modal({ backdrop: 'static', keyboard: false });
            $("body").mLoading("hide");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    });
   
}



function UpdateGradeViewFor100(gradeView100) {
    
    $("body").mLoading();
    //var gradeViewFor50 = $(control).is(":checked");
    //console.log(gradeViewFor50);

    var confirmText = '<span style="text-align:center"><h2>ท่านต้องการเปลี่ยนรูปแบบการแสดงผลใช่หรือไม่</h2></span>';
    $.confirm({
        boxWidth: '30%',
        useBootstrap: false,
        title: '',
        content: confirmText,
        buttons: {
            cancel: {
                text: '<i class="" style="background-color: #007bff !important;"></i>ยกเลิก',
                btnClass: 'btn-red',
                action: function () {
                    //Cancel - Show the existing value
                    if (gradeView100 == 1) {
                        $("#GradeViewFor50").prop("checked", true);
                    }
                    else {
                        $("#GradeViewFor100").prop("checked", true);
                    }

                    $('#GradeViewFor100Setting').modal('hide');
                    
                    $("body").mLoading('hide');
                }
            },
            confirm: {
                text: '<i class="" style="background-color: #007bff;"></i>ยืนยัน',
                btnClass: 'btn-blue',
                action: function () {

                    var gradeViewSettingRequest = { GradeViewFor100: (gradeView100 == 1)?true:false };
                    var params = $.extend({}, doAjax_params_default);

                    params['url'] = "../../api/GradeViewSettings/UpdateGradeViewFor100";
                    params['data'] = JSON.stringify(gradeViewSettingRequest);
                    params['requestType'] = "POST";
                    doAjax(params);
                    $("body").mLoading('hide');
                }
            }
        },
    });
}

function UpdateGradeViewAutoBlock(control) {

    $("body").mLoading();
    var gradeViewAutoBlock = $(control).is(":checked");
    //console.log(gradeViewFor50);

    var confirmText = '<span style="text-align:center"><h2>ท่านต้องการเปลี่ยนรูปแบบการแสดงผลใช่หรือไม่</h2></span>';
    $.confirm({
        boxWidth: '30%',
        useBootstrap: false,
        title: '',
        content: confirmText,
        buttons: {
            cancel: {
                text: '<i class="" style="background-color: #007bff !important;"></i>ยกเลิก',
                btnClass: 'btn-red',
                action: function () {
                    //Cancel - Show the existing value
                    if (gradeViewAutoBlock == 1) {
                        $("#GradeViewAutoBlock").prop("checked", true);
                    }
                    else {
                        $("#GradeViewAutoBlock").prop("checked", true);
                    }

                    $('#GradeViewFor100Setting').modal('hide');

                    $("body").mLoading('hide');
                }
            },
            confirm: {
                text: '<i class="" style="background-color: #007bff;"></i>ยืนยัน',
                btnClass: 'btn-blue',
                action: function () {

                    var gradeViewSettingRequest = { GradeViewAutoBlock: (gradeViewAutoBlock == 1) ? true : false };
                    var params = $.extend({}, doAjax_params_default);

                    params['url'] = "../../api/GradeViewSettings/UpdateGradeViewAutoBlock";
                    params['data'] = JSON.stringify(gradeViewSettingRequest);
                    params['requestType'] = "POST";
                    doAjax(params);
                    $("body").mLoading('hide');
                    LoadGradeViewSettingDetails();
                }
            }
        },
    });
}


function BlockClass(gradeViewSettingId, nTerm, nYear) {
    $("#gradeViewSettingId").val(gradeViewSettingId);
    $("#nTerm").val(nTerm);
    $("body").mLoading();
    $("#ClassList option").remove();
    var opt = document.createElement("option");

    // Assign text and value to Option object
    opt.text = "กรุณาเลือก";
    opt.value = "";

    // Add an Option object to Drop Down List Box
    //document.getElementById('#ClassList').options.add(opt);
    $("#ClassList").append(opt);
    $.get("/api/Common/GetPrimaryLevelEducations", function (Result) {
        $.each(Result, function (index) {

            // Create an Option object       
            var opt = document.createElement("option");

            // Assign text and value to Option object
            opt.text = Result[index].SubLevel;
            opt.value = Result[index].NTSubLevel;
            
            // Add an Option object to Drop Down List Box
            $("#ClassList").append(opt);

        });
        $("#ModalForBlockClass").modal({ backdrop: 'static', keyboard: false });
        $("#btnClassRoomBlockDataSave").hide();
        $("body").mLoading('hide');
        $("#divClassRoomBlockList").hide();
    });
}

function ClassOnChange(control) {
    
    $("body").mLoading();
    var gradeViewSettingId = $("#gradeViewSettingId").val();
    var nTSubLevel = $("#ClassList").val();

    if (nTSubLevel != "") {
        $("#btnClassRoomBlockDataSave").show();

        $.get("/api/GradeViewSettings/GetRoomListForGradeViewBlock?gradeViewSettingId=" + gradeViewSettingId + "&nTSubLevel=" + nTSubLevel, function (Result) {
          
            console.log(Result.data);
            console.log(Result.column);
            //tableRoomListBlock.destroy();
            //tableRoomListBlock.clear().draw();
            if (tableRoomListBlock != undefined) {
                tableRoomListBlock.destroy();
                tableRoomListBlock.clear().draw();
            }
            
            $("#divClassRoomBlockList").show();
            if (Result.data.length > 0) {
               

                //$("#ClassRoomBlockList  tbody").empty();
                console.log("length" + Result.data.length);

                tableRoomListBlock = $('#ClassRoomBlockList').DataTable({
                    bFilter: false,
                    bSort: false,
                    bPaginate: false,
                    bAutoWidth: false,
                    ordering: false,
                    paging: false,
                    searching: false,
                    lengthChange: false,
                    bInfo: false,
                    scrollCollapse: true,
                    fixedHeader: false,
                    keys: false,
                    "sDom": 'Rlfrtlip',
                    'data': Result.data,
                    'columns': Result.column,

                    'columnDefs': [{
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-left',
                        'width': '5%',
                        'render': function (data, type, full, meta) {
                            return data;
                        }
                    },
                    {
                        'targets': 5,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-left',
                        'width': '75%',
                        'render': function (data, type, full, meta) {
                            var checked = (meta.settings.aoData[meta.row]._aData.IsRoomBlocked == true) ? "Checked" : "";
                            return '<input type="checkbox" class="chkClassRoom" ' + checked + ' name="chkClassRoom[]" value="'
                                + $('<div/>').text(data).html() + '">  ' + meta.settings.aoData[meta.row]._aData.RoomFullName
                        }
                    },

                    ],

                    fnInitComplete: function () {
                        $("body").mLoading('hide');
                        console.log("fnInitComplete");
                        var allCheckBoxSelected = true;
                        var rowCount = 0;
                        tableRoomListBlock.rows().every(function (rowIdx, tableLoop, rowLoop) {
                            var data = this.data();

                            if (!$($("input[name='chkClassRoom[]']")[rowCount]).is(":checked")) {
                                allCheckBoxSelected = false;
                            }
                            rowCount++;
                        });

                        if (allCheckBoxSelected) {
                            $("#ClassRoomSelectAll").prop('checked', this.checked);
                        }

                    }
                });

                console.log("End");
            }
            else {
                $("#btnClassRoomBlockDataSave").hide();
                $("body").mLoading('hide');
            }

        });

        $('#ClassRoomSelectAll').on('click', function () {
            // Check/uncheck all checkboxes in the table
            var rows = tableRoomListBlock.rows({ 'search': 'applied' }).nodes();
            $("input[name='chkClassRoom[]']", rows).prop('checked', this.checked);

            //if (!$("input[name='chkClassRoom[]']", rows).prop('disabled')) {
            //    $("input[name='chkClassRoom[]']", rows).prop('checked', this.checked);
            //}

        });


    }
    else {
        console.log(nTSubLevel);
        $("body").mLoading('hide');
        $("#divClassRoomBlockList").hide();
        $("#btnClassRoomBlockDataSave").hide();

    }


}

function BlockStudent(gradeViewSettingId, nTerm, nYear) {
    $("#gradeViewSettingId").val(gradeViewSettingId);
    $("#nTerm").val(nTerm);
    $("body").mLoading();
    $('#ClassListForStudentBlock').find('option').remove().end();
    $("#ClassListForStudentBlock option").remove();
    $("#StudentBlockRoomList option").remove();
    var opt = document.createElement("option");

    // Assign text and value to Option object
    opt.text = "กรุณาเลือก";
    opt.value = "";

    // Add an Option object to Drop Down List Box
    //document.getElementById('#ClassList').options.add(opt);
    $("#ClassListForStudentBlock").append(opt);
    $("#StudentBlockRoomList").append(opt);
    $.get("/api/Common/GetPrimaryLevelEducations", function (Result) {
        $.each(Result, function (index) {

            // Create an Option object       
            var opt = document.createElement("option");

            // Assign text and value to Option object
            opt.text = Result[index].SubLevel;
            opt.value = Result[index].NTSubLevel;

            // Add an Option object to Drop Down List Box
            $("#ClassListForStudentBlock").append(opt);

        });
        $("#ModalForBlockStudent").modal({ backdrop: 'static', keyboard: false });
        $("#btnStudentBlockDataSave").hide();
        $("body").mLoading('hide');
        $("#divStudentBlockList").hide();
    });
}

function StudentBlockClassOnChange(control) {
   
    $("body").mLoading();
    var nTSubLevel = $("#ClassListForStudentBlock").val();

    if (nTSubLevel != "") {
        $.get("/api/common/GetClassRoomDTOs?nTSubLevel=" + nTSubLevel, function (Result) {
            
            $("#StudentBlockRoomList option").remove();
            var opt = document.createElement("option");

            // Assign text and value to Option object
            opt.text = "กรุณาเลือก";
            opt.value = "";

            // Add an Option object to Drop Down List Box
            //document.getElementById('#ClassList').options.add(opt);
            $("#StudentBlockRoomList").append(opt);
            $.each(Result, function (index) {

                // Create an Option object       
                var opt = document.createElement("option");

                // Assign text and value to Option object
                opt.text = Result[index].RoomFullName;
                opt.value = Result[index].nTermSubLevel2;

                // Add an Option object to Drop Down List Box
                $("#StudentBlockRoomList").append(opt);
            });
            $("body").mLoading('hide');
        });
    }
    else {
        console.log(nTSubLevel);
        $("body").mLoading('hide');
        $("#divStudentBlockList").hide();
        $("#btnStudentBlockDataSave").hide();
    }
}

function StudentBlockRoomOnChange(control) {
  
    $("body").mLoading();
    var gradeViewSettingId = $("#gradeViewSettingId").val();
    var nTSubLevel = $("#ClassListForStudentBlock").val();
    var nTSubLevel = $("#ClassListForStudentBlock").val();
    var nTermSubLevel2 = $("#StudentBlockRoomList").val();
    var nTerm = $("#nTerm").val();

    if (nTSubLevel != "" && nTermSubLevel2 != "") {
        $("#btnStudentBlockDataSave").show();
        $.get("/api/GradeViewSettings/GetStudentsForGradeViewBlock?gradeViewSettingId=" + gradeViewSettingId + "&nTSubLevel=" + nTSubLevel + "&nTermSubLevel2=" + nTermSubLevel2 + "&nTerm=" + nTerm, function (Result) {

        if (tableStudentBlockList != undefined) {
            tableStudentBlockList.destroy();
            tableStudentBlockList.clear().draw();
        }
        $("#divStudentBlockList").show();
           
               
        console.log("length" + Result.data.length);
        console.log(Result.data);
        console.log(Result.column);
        tableStudentBlockList = $('#StudentBlockList').DataTable({
                  
            bAutoWidth: false,
            ordering: false,
            paging: false,
            searching: false,
            lengthChange: false,
            bInfo: false,
            scrollCollapse: true,
            fixedHeader: false,
            deferRender: true,
            'data': Result.data,
            'columns': Result.column,

            'columnDefs': [{
                'targets': 0,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-left',
                'width': '5%',
                'render': function (data, type, full, meta) {
                    return data;
                }
            },
            {
                'targets': 5,
                'searchable': false,
                'orderable': false,
                'className': 'dt-body-left',
                'width': '30%',
                'render': function (data, type, full, meta) {
                    var checked = (meta.settings.aoData[meta.row]._aData.IsStudentBlocked == true) ? "Checked" : "";
                    return '<input type="checkbox" class="chkStudent" ' + checked + ' name="chkStudent[]" value="'
                        + $('<div/>').text(data).html() + '">  ' + meta.settings.aoData[meta.row]._aData.SStudentId
                }
            },
            {
                'targets': 6,
                'width': '60%',
            }
            ],

            fnInitComplete: function () {
                $("body").mLoading('hide');
                console.log("fnInitComplete");
            }
        });
        console.log("End");

        if (Result.data.length == 0)
            $("#btnStudentBlockDataSave").hide();
        });

        $('#StudentSelectAll').on('click', function () {
            // Check/uncheck all checkboxes in the table
            var rows = tableStudentBlockList.rows({ 'search': 'applied' }).nodes();
            $("input[name='chkStudent[]']", rows).prop('checked', this.checked);
        });

    }
    else {
        console.log(nTSubLevel);
        $("body").mLoading('hide');
        $("#divStudentBlockList").hide();
        $("#btnStudentBlockDataSave").hide();
    }


}

function FinalTermOnChange(control, gradeViewSettingId, nTerm, nYear) {
    var isFinalTermChecked = $(control).is(":checked");
    var confirmText = (isFinalTermChecked) ? '<span style="text-align:center"><h2>เมื่อกดอนุมัติแล้ว  ผู้ปกครองจะสามารถตรวจสอบ</h2><span><span style="text-align:center"><h2>ผลการเรียนผ่านแอปพลิเคชั่น School  Bright ได้ทันที</h></span><br><b>ท่านต้องการทำการอนุมัติ ใช่หรือไม่?</b></span>' : '<span style="text-align:center"><h2>ท่านต้องการทำการยกเลิกการอนุมัติผลการเรียนผ่านแอปพลิเคชั่น School  Bright ใช่หรือไม่</h2></span>';
    $.confirm({
        boxWidth: '30%',
        useBootstrap: false,
        title: '',
        content: confirmText,
        buttons: {
            cancel: {
                text: '<i class="" style="background-color: #007bff !important;"></i>ยกเลิก',
                btnClass: 'btn-red',
                action: function () {

                    if (isFinalTermChecked) {
                        $(control).prop('checked', false);
                    }
                    else {
                        $(control).prop('checked', true);
                    }
                    $("body").mLoading('hide');
                }
            },
            confirm: {
                text: '<i class="" style="background-color: #007bff;"></i>ยืนยัน',
                btnClass: 'btn-blue',
                action: function () {
                    var gradeViewSettingRequest = { GradeViewSettingId: gradeViewSettingId, nTerm: nTerm, nYear: nYear, IsTermApproved: isFinalTermChecked, IsFinalTermApproved: isFinalTermChecked};
                    var params = $.extend({}, doAjax_params_default);

                    params['url'] = "../../api/GradeViewSettings/UpdateFinalTermApprovedStatus";
                    params['data'] = JSON.stringify(gradeViewSettingRequest);
                    params['requestType'] = "POST";
                    params['successCallbackFunction'] = ApproveTermResponse;
                    doAjax(params);
                }
            }
        },
    });
}

var ApproveTermResponse = function (response) {
    $.confirm({
        title: false,
        content: '<img src="../../images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">อัปเดตสำเร็จ</h1>',
        theme: 'material',
        type: 'blue',
        buttons: {
            "<span style=\"font-size: 20px;\">ปิด</span>": {
                btnClass: 'btn-primary',
                keys: ['enter', 'shift'],
                action: function () {
                    $("body").mLoading('hide');
                    LoadGradeViewSettingDetails();
                }
            }
        }
    });
}

function MidTermOnChange(control, gradeViewSettingId, nTerm, nYear) {
    $("body").mLoading();
    var isMidTermChecked = $(control).is(":checked");
    console.log(isMidTermChecked);
   
    var confirmText = (isMidTermChecked) ? '<span style="text-align:center"><h2>เมื่อกดอนุมัติแล้ว  ผู้ปกครองจะสามารถตรวจสอบ</h2><span><span style="text-align:center"><h2>ผลการเรียนผ่านแอปพลิเคชั่น School  Bright ได้ทันที</h></span><br><b>ท่านต้องการทำการอนุมัติ ใช่หรือไม่?</b></span>' : '<span style="text-align:center"><h2>ท่านต้องการทำการยกเลิกการอนุมัติผลการเรียนผ่านแอปพลิเคชั่น School  Bright ใช่หรือไม่</h2></span>';
    $.confirm({
        boxWidth: '30%',
        useBootstrap: false,
        title: '',
        content: confirmText,
        buttons: {
            cancel: {
                text: '<i class="" style="background-color: #007bff !important;"></i>ยกเลิก',
                btnClass: 'btn-red',
                action: function () {
                   
                    if (isMidTermChecked) {
                        $(control).prop('checked', false);
                    }
                    else {
                        $(control).prop('checked', true);
                    }
                    $("body").mLoading('hide');
                }
            },
            confirm: {
                text: '<i class="" style="background-color: #007bff;"></i>ยืนยัน',
                btnClass: 'btn-blue',
                action: function () {
                   
                    var gradeViewSettingRequest = { GradeViewSettingId: gradeViewSettingId, nTerm: nTerm, nYear: nYear, IsMidTermApproved: isMidTermChecked };
                    var params = $.extend({}, doAjax_params_default);

                    params['url'] = "../../api/GradeViewSettings/UpdateMidTermApprovedStatus";
                    params['data'] = JSON.stringify(gradeViewSettingRequest);
                    params['requestType'] = "POST";
                    params['successCallbackFunction'] = ApproveTermResponse;
                    doAjax(params);
                }
            }
        },
    });
}

function doAjax(doAjax_params) {

    var url = doAjax_params['url'];
    var requestType = doAjax_params['requestType'];
    var contentType = doAjax_params['contentType'];
    var dataType = doAjax_params['dataType'];
    var data = doAjax_params['data'];
    var beforeSendCallbackFunction = doAjax_params['beforeSendCallbackFunction'];
    var successCallbackFunction = doAjax_params['successCallbackFunction'];
    var completeCallbackFunction = doAjax_params['completeCallbackFunction'];
    var errorCallBackFunction = doAjax_params['errorCallBackFunction'];

    //make sure that url ends with '/'
    /*if(!url.endsWith("/")){
     url = url + "/";
    }*/

    $.ajax({
        url: url,
        crossDomain: true,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
            if (typeof beforeSendCallbackFunction === "function") {

                beforeSendCallbackFunction();
            }
        },
        success: function (data, textStatus, jqXHR) {
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Ajax Error" + errorThrown)
            if (typeof errorCallBackFunction === "function") {
                errorCallBackFunction(errorThrown);
            }

        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}