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
    'stopCountingActiveAjaxConnections': false
};
var tableName = '#StudentGradeInfo';
var table;
var response;
var optYear = [], optPlans = [];
var optTerm = [], optCourseCode = [];
$("#rowStudentGradeInfo").hide();
$(document).ready(function () {
    $("#btnSearch").attr("disabled", true);
    //Get Student Info
    $("#txtStudentCode").change(function () {
        //console.log($('#txtStudentCode').val());
        $("#rowStudentGradeInfo").hide();
        $("body").mLoading();
        $.ajax({
            url: "../../api/Common/GetStudentInfo?studentCode=" + $('#txtStudentCode').val(),
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != null) {
                    $("#btnSearch").removeAttr('disabled');
                    //console.log(response.FullName);
                    $("#hdnSId").val(response.SId);
                    $("#hdnNTSubLevel").val(response.NTSubLevel);
                    $("#hdnLevelName").val(response.LevelName);
                    $("#lblStudentName").text(response.FullName);
                    $("#lblClassLevel").text(response.RoomFullName);
                    $("#lblYear").text(response.NumberYear);
                    $("#lblTerm").text(response.STerm);
                    //console.log(response);
                }
                else
                {
                    $("#hdnSId").val("");
                    $("#hdnNTSubLevel").val("");
                    $("#hdnLevelName").val("");
                    $("#lblStudentName").text("");
                    $("#lblClassLevel").text("");
                    $("#lblYear").text("");
                    $("#lblTerm").text("");
                    //TO-DO : show error
                    console.log("No Student");
                    Swal.fire({
                        icon: 'error',
                        title: 'ไม่มีนักเรียน',
                        //text: 'Something went wrong!',                      
                    })
                    $("#btnSearch").attr("disabled", true);
                }
                $("body").mLoading("hide");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });
    });
    $(".selectpicker").selectpicker({
        liveSearch: true,
        maxOptions: 1
    });
    //Add new row in datatable
    $('#addRow').on('click', function () {
      
        //console.log("counter" + table.rows().count() + 1);
        //console.log(optYear);
        //console.log(optPlans);
        //console.log(optCourseCode);
        var newRow = {
            RowNumber: (table.rows().count() + 1),
            nGradeId: 0,
            nYear:0,
            nTerm:"",
            PlanId:0,
            sPlaneId:0,
            NumberYear:0,
            STerm:"",
            PlanName:"",
            CourseCode:"",
            CourseName:"",
            NCredit:"",
            CourseTotalHour:"",
            GetScore100: "",
            GetGradeLabel:"",
            IsScoreFromScoreEntryPage: false,
            OptYear: optYear,
            OptPlanName: [],
            OptCourseCode: [],
            OptTerm: [],
            IsSavedData: false,
            //Is
        };
        table.row.add(newRow).draw(true);
       
        table.rows().invalidate('data')
            .draw(false);
    });
});

//#1
function onSearch() {
    if ($("#hdnSId").val() != '') {
        //console.log($("#hdnSId").val());
        $("body").mLoading();
        if (table != undefined) {
            table.destroy();
            table.clear().draw();
        }
        //Fetch year information
        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/Common/GetYears";
        params['requestType'] = "GET";
        params['successCallbackFunction'] = GetStudentGradeInfo;
        doAjax(params);
    }
}

//#2
function GetStudentGradeInfo(result) {
    console.log("sid" + $("#hdnSId").val());
    var numberYear = Number($("#lblYear").text());
    //optYear = "";
    //optYear = optYear + "<option value='" + 0 + "'>เลือกปี</options>";
    optYear.splice(0, optYear.length);
    $.each(result, function (index, Value) {
        //console.log(Value);
        //optYear = optYear + "<option value='" + Value.NYear + "'>" + Value.NumberYear + "</options>";
        optYear.push({ Name: Value.NumberYear, Id: Value.NYear });
    });

    
    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/AssessmentScore/GetStudentGradeInfo?sId=" + $("#hdnSId").val();
    params['requestType'] = "GET";
    params['successCallbackFunction'] = CreatePageContent;
    doAjax(params);
}

//#3
function CreatePageContent(result) {
    $("#rowStudentGradeInfo").show();
    response = result;
    //console.log(response);
    console.log(response.data);
    console.log(response.column);
    //DataTable Code Starting
    table = $(tableName).DataTable({
        bAutoWidth: false,
        ordering: false,
        paging: false,
        searching: false,
        lengthChange: false,
        bInfo: false,
        scrollCollapse: true,
        fixedHeader: false,
        deferRender: true,
        data: response.data,
        columns: response.column,
        //rowGroup: {
        //    dataSrc: [6]
        //},
        fnRowCallback: function (nRow, aData, iDisplayIndex) {
            //decorateRow(nRow, (aData["nStudentStatus"] == null) ? 0 : aData["nStudentStatus"], iDisplayIndex);
            return nRow;
        },
        fnInitComplete: function () {
            // Event handler to be fired when rendering is complete (Turn off Loading gif for example)
            console.log('Datatable rendering complete');
            $("body").mLoading("hide");
           
        },
        drawCallback: function (settings) {
            //alert('DataTables has redrawn the table');
            $(".selectpicker").selectpicker({
                liveSearch: true,
                maxOptions: 1
            });
        },
        columnDefs: ColumnDef()
    });

    
}

//#4
//DataTable Column Definition
function ColumnDef() {

    var getGradeValues = [
        { Value: "", Text: "เลือกเกรด" },
        { Value: "4.0", Text: "4.0" },
       /* { Value: "4", Text: "4" },*/
        { Value: "3.5", Text: "3.5" },
        { Value: "3.0", Text: "3.0" },
       /* { Value: "3", Text: "3" },*/
        { Value: "2.5", Text: "2.5" },
        { Value: "2.0", Text: "2.0" },
        /*{ Value: "2", Text: "2" },*/
        { Value: "1.5", Text: "1.5" },
        { Value: "1.0", Text: "1.0" },
        { Value: "0", Text: "0" },
      /*  { Value: "1", Text: "1" },*/
        { Value: "ร", Text: "ร" },
        { Value: "มส", Text: "มส" },
        { Value: "มก", Text: "มก" },
        { Value: "ผ", Text: "ผ" },
        { Value: "มผ", Text: "มผ" },
        { Value: "อื่นๆ", Text: "อื่นๆ" },
        { Value: "ขร", Text: "ขร" },
        { Value: "ขส", Text: "ขส" },
        { Value: "ท", Text: "ท" },
        { Value: "ดีเยี่ยม", Text: "ดีเยี่ยม" },
        { Value: "ดี", Text: "ดี" },
        { Value: "พอใช้", Text: "พอใช้" },
        { Value: "ปรับปรุง", Text: "ปรับปรุง" },

    ];
    var opts = "";
    $.each(getGradeValues, function (index, Value) {
        opts = opts + "<option value='" + Value.Value + "'>" + Value.Text + "</options>";
    });


    var getNCreditValues = [
        { Value: "0", Text: "0" },
        { Value: "0.25", Text: "0.25" },
        { Value: "0.5", Text: "0.5" },
        { Value: "0.75", Text: "0.75" },
        { Value: "1", Text: "1" },
        { Value: "1.5", Text: "1.5" },
         { Value: "2", Text: "2" },
        { Value: "2.5", Text: "2.5" },
        { Value: "3", Text: "3" },
        { Value: "3.5", Text: "3.5" },
        { Value: "4", Text: "4" },
        { Value: "4.5", Text: "4.5" },
        { Value: "5", Text: "5" },
        { Value: "5.5", Text: "5.5" },
        { Value: "6", Text: "6" },
        { Value: "6.5", Text: "6.5" },
        { Value: "7", Text: "7" },
        { Value: "7.5", Text: "7.5" },
        { Value: "8", Text: "8" },
        { Value: "8.5", Text: "8.5" },
        { Value: "9", Text: "9" },
        { Value: "9.5", Text: "9.5" },
        { Value: "10", Text: "10" },
    ];

    var optsNCredit = "";
    $.each(getNCreditValues, function (index, Value) {
        optsNCredit = optsNCredit + "<option value='" + Value.Value + "'>" + Value.Text + "</options>";
    });

    var columnDef = [
        {
            width: "30px", targets: 0, //RowNumber
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                return meta.row + 1;
            }
        },
        //targets 1 to 5 hidden
        {
            width: "8%", targets: 6, //Year
            //visible: false,
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                data = (data == 0) ? '' : data;
                //console.log("Year" + data);
                if (row["IsSavedData"] == true) {
                    return data;
                }
                else if (row["OptYear"].length > 0) {

                    console.log(row["OptYear"]);
                    var opt = "<option value='" + 0 + "'>เลือกปี</options>";
                    $.each(optYear, function (index, Value) {
                        var selected = (data == Value.Name) ? "selected" : "";
                        opt = opt + "<option " + selected +" value='" + Value.Id + "'>" + Value.Name + "</options>";
                    });

                    return '<select class="selectpicker" data-style="select-with-transition" data-size="5" title="เลือกปี" onchange="YearOnChange(this,' + meta.row + ',' + $("#hdnNTSubLevel").val() + ',\'' + $("#hdnLevelName").val() + '\')" >' + opt + '</select >';
                }
                else
                {
                    return "";
                }

              
            }
            
        },
        
        {
            //width: "10%",
            targets: 7, //Plan Name
            render: function (data, type, row, meta) {
                //console.log("PlanName" + data);
                //console.log("row[OptPlanName]" + row["OptPlanName"]);
                //console.log("data" + data);
                
                //console.log("meta.settings.aoColumns[meta.col].data" + meta.settings.aoColumns[meta.col].data);

                if (row["IsSavedData"] == true) {
                    return data;
                }
                else if (row["OptPlanName"] != undefined && row["OptPlanName"].length > 0) {

                    console.log(row["OptPlanName"]);
                    var opt = "<option value='" + 0 + "'>เลือกแผน</options>";
                    $.each(optPlans, function (index, Value) {
                        var selected = (data == Value.Name) ? "selected" : "";
                        opt = opt + "<option " + selected + " value='" + Value.Id + "'>" + Value.Name + "</options>";
                    });

                    return '<select  class="selectpicker" data-style="select-with-transition"  title="เลือกแผน" data-size="5" onchange="PlanOnChange(this,' + meta.row + ')">' + opt + '</select >';
                }
                else {
                    return "";
                }
            }

        },
        {
            //width: "25%",
            targets: 8, //Course Code
            render: function (data, type, row, meta) {
                
                //data = (data == null || isNaN(data)) ? '' : data;
                //console.log("CourseCode" + row["CourseCode"]);
               
                if (row["IsSavedData"] == true) {
                    return ((data == null) ? "" : data)  + " " + ((row["CourseName"] == null) ? "" : row["CourseName"]);
                }
                else if (row["OptCourseCode"] != undefined && row["OptCourseCode"].length > 0) {

                    var opt = "<option value='" + 0 + "'>เลือกหัวข้อ</options>";
                    $.each(optCourseCode, function (index, Value) {
                        var selected = (data == Value.Name) ? "selected" : "";
                        opt = opt + "<option " + selected + " value='" + Value.Id + "'>" + Value.Name + "</options>";
                    });

                    return '<select class="selectpicker" data-style="select-with-transition" style="height:100px !important" title =""เลือกหัวข้อ" onchange="SubjectOnChange(this,' + meta.row + ')" >' + opt  + '</select >';
                }
                else {
                    return "";
                }
            }

        },
        //{
        //    width: "7%", targets: 9, //Course Name
        //    render: function (data, type, row, meta) {
        //        data = (data == null || isNaN(data)) ? '' : data;
        //        return data;
        //    }

        //},
        {
            width: "8%", targets: 9, //Term
            render: function (data, type, row, meta) {
               
                data = (data == null) ? '' : data;
                if (row["IsSavedData"] == true) {
                    console.log("Term" + data);
                    return data;
                }
                else if (row["OptTerm"] != undefined && row["OptTerm"].length > 0) {
                    console.log("Term Data" + data);
                    console.log("row[STerm]" + row["STerm"]);
                    var opt = "<option value='" + 0 + "'>เลือกหัวข้อ</options>";
                    $.each(optTerm, function (index, Value) {
                        var selected = (data == Value.Name || row["STerm"] == Value.Name) ? "selected" : "";
                        
                        opt = opt + "<option " + selected + " value='" + Value.Id + "'>" + Value.Name + "</options>";
                    });

                    return '<select class="selectpicker" data-style="select-with-transition" title ="เลือกระยะ" onchange="TermOnChange(this,' + meta.row + ')" >' + opt + '</select >';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "80px", targets: 10, //NCredit
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {

                    return data;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    var nCredit = '<select class="selectpicker" data-style="select-with-transition" onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ',this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')">' + optsNCredit + '</select>';
                    
                    var data = "value='" + data + "'";
                    return nCredit.replace(data, data + ' selected ');
                    //return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data + '\')" onkeypress="return isNumberKey(event, this)" value="' + rowValue + '" />';
                }
                else {
                    return "";
                }


            }

        },
        {
            width: "5%", targets: 11, //Course Total Hour
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;
                //return data;

                //data = (data == null || isNaN(data)) ? '' : data;

                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ', this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  onkeypress="return isNumberKey(event, this)" value="' + rowValue + '"  />';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "65px", targets: 12, //Get Score
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;

                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ', this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  onkeypress="return isNumberKey(event, this)" value="' + rowValue + '"  />';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "100px", targets: 13, //Get Grade Label
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;
                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    var gradeLabel = '<select class="selectpicker" data-style="select-with-transition" onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ',this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')">' + opts + '</select>';
                    data = (data == "4" || data == "3" || data == "2" || data == "1") ? (data + ".0") : data;
                    var data = "value='" + data + "'";
                    return gradeLabel.replace(data, data + ' selected ');
                    //return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data + '\')" onkeypress="return isNumberKey(event, this)" value="' + rowValue + '" />';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "65px", targets: 14, //GetBehaviorLabel
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;
                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ',this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  onkeypress="return isNumberKey(event, this)" value="' + rowValue + '" />';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "65px", targets: 15, //GetReadWrite
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;
                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    return '<input type="text" oncopy="return false" class="AssessmentScoreBox" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ',this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')" onkeypress="return isNumberKey(event, this)" value="' + rowValue + '" />';
                }
                else {
                    return "";
                }
            }

        },
        {
            width: "65px", targets: 16, //GetSamattana
            render: function (data, type, row, meta) {
                //data = (data == null || isNaN(data)) ? '' : data;
                var rowValue = (data == null) ? "" : data;
                if (row["IsSavedData"] == true && row["IsScoreFromScoreEntryPage"] == true) {
                    return rowValue;
                }
                else if (row["nTerm"] != "" && row["nTerm"] != undefined) {
                    return '<input type="text" oncopy="return false" onpaste="return false" class="AssessmentScoreBox" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onchange="ScoreOnChange(this, \'' + row["nTerm"] + '\',' + row["sPlaneId"] + ',' + row["nGradeId"] + ',' + meta.row + ',this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  onkeypress="return isNumberKey(event, this)" value="' + rowValue + '" />';
                }
                else {
                    return "";
                }
            }
        },
        {
            width: "7%", targets: 17, //Delete Row
            render: function (data, type, row, meta) {
                if (row["IsScoreFromScoreEntryPage"] == true) {
                    return "";
                }
                else {
                    return '<div><span class="material-icons" style="color:red" onclick="DeleteRow(' + meta.row + ',' + row["nGradeId"] + ',' + row["NumberYear"] + ')">clear</span>&nbsp;</div>';
                }
                
            }
        },
        {
            width: "7%", targets: 23, className: "DeleteScoreForAllStudent", //Delete Row
            render: function (data, type, row, meta) {
                if (row["IsScoreFromScoreEntryPage"] == true) {
                    return "";
                }
                else {
                    return '<div><span class="material-icons" style="color:red" onclick="DeleteAllStudentsGrade(' + meta.row + ',' + row["nGradeId"] + ',' + row["NumberYear"] + ')">clear</span>&nbsp;</div>';
                }

            }
        },
    ];

    return columnDef;
}

//#5
function YearOnChange(controlId, rowIndex, nTSublevel, levelName) {
    var nYear = $(controlId).val();
    var numberYear = $(controlId).find(":selected").text();
    console.log($(controlId).find(":selected").text())
    //Fetch year information
    $("body").mLoading();
    var responseData = $.grep(response.data, function (n) {
        return n.NumberYear == $(controlId).find(":selected").text();
    });
    var optPlansExist = [];
    $.each(responseData, function (index, Value) {
       
        var planId = optPlansExist.find(x => x.Id === Value.PlanId);
        if (planId == undefined && Value.PlanName != undefined && Value.PlanName != null && Value.PlanName != "") {
            optPlansExist.push({
                Name: Value.PlanName.split("-")[0], Id: Value.PlanId
            });
        }
    });

    /*if (responseData.length == 0) {*/
        $.ajax({
            url: "../../api/Plan/GetPlan?nYear=" + nYear + "&nTSublevel=" + nTSublevel + "&levelName=" + levelName,
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                console.log("Year On Change" + response);
                if (response != null && response != "") {

                    optPlans.splice(0, optPlans.length);
                    $.each(response, function (index, Value) {
                        if (Value.PlanName != undefined)
                            optPlans.push({ Name: Value.PlanName, Id: Value.PlanId });
                    });

                   
                    //optPlansExist - for checking teacher already entered score in any plan. 
                    //if plan not exist then add those plan in the dropdown list
                    $.each(optPlansExist, function (index, Value) {
                        if (Value.PlanName != undefined) {

                            var checkPlanExist = $.grep(optPlans, function (n) {
                                return n.PlanName == Value.PlanName && n.Id == Value.PlanId;
                            });
                            if (checkPlanExist.length == 0) {
                                optPlans.push({
                                    Name: Value.PlanName, Id: Value.PlanId
                                });
                            }
                        }
                    });
                 

                    console.log(optPlans);
                    //console.log(table.cell(rowIndex, 18).header().textContent);
                    table.cell(rowIndex, 2).data(nYear).draw(); //Year id
                    table.cell(rowIndex, 3).data(0).draw();
                    table.cell(rowIndex, 4).data(0).draw();
                    table.cell(rowIndex, 5).data("").draw();
                    table.cell(rowIndex, 6).data(numberYear).draw(); //Year
                    table.cell(rowIndex, 7).data("").draw();
                    table.cell(rowIndex, 19).data(optPlans).draw();
                    table.row(rowIndex).invalidate('data').draw(false);

                }
                else {
                    console.log("No Plan");
                }
                $("body").mLoading("hide");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });

    //}
    //else {
    //    table.cell(rowIndex, 2).data(nYear).draw(); //Year id
    //    table.cell(rowIndex, 3).data(0).draw();
    //    table.cell(rowIndex, 4).data(0).draw();
    //    table.cell(rowIndex, 5).data("").draw();
    //    table.cell(rowIndex, 6).data(numberYear).draw(); //Year
    //    table.cell(rowIndex, 7).data("").draw();
    //    table.cell(rowIndex, 19).data(optPlans).draw();
    //    table.row(rowIndex).invalidate('data').draw(false);
    //    $("body").mLoading("hide");
    //}
}
//#5.1
function PlanOnChange(controlId, rowIndex) {
    var planId = $(controlId).val();
    var planName = $(controlId).find(":selected").text();
    //Fetch year information
    $("body").mLoading();
    if (planId > 0) {


        $.ajax({
            url: "../../api/Plan/GetSubjectByPlanId?planId=" + planId,
            dataType: "json",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response != null) {
                    optCourseCode.splice(0, optCourseCode.length);
                    $.each(response, function (index, Value) {
                        optCourseCode.push({ Name: (Value.CourseCode + ' ' + Value.CourseName), Id: Value.PlanCourseId });
                    });

                    table.cell(rowIndex, 3).data(planId).draw();
                    table.cell(rowIndex, 4).data(0).draw(); //Subject
                    table.cell(rowIndex, 5).data("").draw(); //Term
                    table.cell(rowIndex, 7).data(planName).draw();
                    table.cell(rowIndex, 8).data("").draw();

                    table.cell(rowIndex, 20).data(optCourseCode).draw();
                    table.row(rowIndex).invalidate('data').draw(false);
                }
                else {
                    table.cell(rowIndex, 3).data(0).draw();
                    table.cell(rowIndex, 4).data(0).draw(); //Subject
                    table.cell(rowIndex, 5).data("").draw(); //Term
                    table.cell(rowIndex, 7).data("").draw();
                    table.cell(rowIndex, 8).data("").draw();
                    table.cell(rowIndex, 20).data(null).draw();

                    table.cell(rowIndex, 5).data("").draw();
                    table.cell(rowIndex, 9).data("").draw();
                    table.cell(rowIndex, 10).data("").draw();
                    table.cell(rowIndex, 11).data("").draw();
                    table.cell(rowIndex, 21).data(null).draw();
                    table.row(rowIndex).invalidate('data').draw(false);

                }
                $("body").mLoading("hide");
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                console.log(textStatus);
            }
        });
    }
    else {
        table.cell(rowIndex, 3).data(0).draw();
        table.cell(rowIndex, 4).data(0).draw(); //Subject
        table.cell(rowIndex, 5).data("").draw(); //Term
        table.cell(rowIndex, 7).data("").draw();
        table.cell(rowIndex, 8).data("").draw();
        table.cell(rowIndex, 20).data(null).draw();

        table.cell(rowIndex, 5).data("").draw();
        table.cell(rowIndex, 9).data("").draw();
        table.cell(rowIndex, 10).data("").draw();
        table.cell(rowIndex, 11).data("").draw();
        table.cell(rowIndex, 21).data(null).draw();
        table.row(rowIndex).invalidate('data').draw(false);
        $("body").mLoading("hide");
    }

}

//#5.1
function SubjectOnChange(controlId, rowIndex) {
    console.log("SubjectOnChange");
    var planCourseId = $(controlId).val();
    var subjectName = $(controlId).find(":selected").text();
    //Fetch year information
    $("body").mLoading();

    $.ajax({
        url: "../../api/Plan/GetPlanCourseDetails?planCoureId=" + planCourseId,
        dataType: "json",
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            if (response != null) {

                optTerm.splice(0, optTerm.length);
                if (response.PlanCourseTermDTOs.length > 0) {
                    if (response.PlanCourseTermDTOs.length > 1 && response.IsPrimary == true) { //For Primary show yearly option
                        optTerm.push({ Name: "รายปี", Id: "รายปี" });
                    }
                    $.each(response.PlanCourseTermDTOs, function (index, Value) {
                        optTerm.push({ Name: Value.TermNumber, Id: Value.NTerm });
                    });
                }

                //$.each(optTerm, function (index, Value) {

                //    console.log("optTerm" + Value.Name);
                //});
                //console.log("optTerm" + optTerm);
                //table.cell(rowIndex, 4).data(planCourseId).draw(); //Subject
                table.cell(rowIndex, 8).data(subjectName).draw();
                table.cell(rowIndex, 9).data("").draw();

                table.cell(rowIndex, 10).data(response.NCredit).draw();
                table.cell(rowIndex, 11).data(response.CourseTotalHour).draw();
                table.cell(rowIndex, 4).data(response.SPlaneId).draw();

                //table.cell(rowIndex, 5).data("รายปี").draw();
                //table.cell(rowIndex, 9).data("รายปี").draw();

                table.cell(rowIndex, 21).data(optTerm).draw();
                table.row(rowIndex).invalidate('data').draw(false);

            }
            else {
                table.cell(rowIndex, 4).data(0).draw(); //Subject
                table.cell(rowIndex, 8).data("").draw();
                table.cell(rowIndex, 9).data("").draw();

                table.cell(rowIndex, 10).data("").draw();
                table.cell(rowIndex, 11).data("").draw();
                table.cell(rowIndex, 5).data("").draw();
                table.cell(rowIndex, 9).data("").draw();
               

                table.cell(rowIndex, 21).data(null).draw();
                table.row(rowIndex).invalidate('data').draw(false);
            }
            $("body").mLoading("hide");
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            console.log(textStatus);
        }
    });



}

//#5.1
function TermOnChange(controlId, rowIndex) {
    var nTerm = $(controlId).val();
    //Fetch year information
    //$("body").mLoading();
    var sTerm = $(controlId).find(":selected").text();
    console.log(sTerm);
    console.log(nTerm);
    /*table.cell(rowIndex, 21).data(nTerm).draw();*/
    table.cell(rowIndex, 5).data(nTerm).draw();
    table.cell(rowIndex, 9).data(sTerm).draw();
    table.row(rowIndex).invalidate('data').draw(false);

    console.log("ONTermChange sTerm Value-" + table.cell(rowIndex, 9).data());
}

//#5.1
function BindTerms(result) {
   
}

//#6
function DeleteRow(rowIndex, ngradeId, numberYear) {

    Swal.fire({
        title: 'คุณต้องการลบ',
        icon: 'question',

        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,


        confirmButtonText: 'ใช่',
        //confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText: 'ยกเลิก',
        //cancelButtonAriaLabel: 'Thumbs down'
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {

            //console.log($(rowIndex));
            $("body").mLoading();
            table.row(rowIndex).remove().draw(true);
            table.rows().invalidate('data')
                .draw(false);
           

            //var ngradeId = table.cell(rowIndex, 1).data();
            var sId = $("#hdnSId").val();
            if (ngradeId > 0 && sId > 0) {
                var commonRequest = { nGradeId: ngradeId, SId: sId, NumberYear: numberYear };

                $.ajax({
                    url: "../../api/AssessmentScore/DeleteStudentScore",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(commonRequest),
                    success: function (response) {
                        $("body").mLoading('hide');
                        //CreateLog("", "", "DeleteSubmit", "");
                        if (response = "Success") {
                            Swal.fire({
                                icon: 'success',
                                title: 'ลบสำเร็จ',
                                //text: 'Something went wrong!',                      
                            })
                        }
                        //RedirectToMainPage();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("body").mLoading('hide');
                        Swal.fire({
                            icon: 'error',
                            title: 'ข้อผิดพลาด',
                            text: 'บางอย่างผิดพลาด', //Something went wrong
                            //footer: '<a href>Why do I have this issue?</a>'
                        })
                        //ShowPageError();
                    }
                });
            }
            else {
                $("body").mLoading('hide');
            }

        } else if (result.isDenied) {

        }
    })


    
}

function DeleteAllStudentsGrade(rowIndex, ngradeId, numberYear) {

    Swal.fire({
        title: 'คุณต้องการลบนักเรียนทั้งหมด',
        icon: 'question',

        showCloseButton: true,
        showCancelButton: true,
        focusConfirm: false,


        confirmButtonText: 'ใช่',
        //confirmButtonAriaLabel: 'Thumbs up, great!',
        cancelButtonText: 'ยกเลิก',
        //cancelButtonAriaLabel: 'Thumbs down'
    }).then((result) => {
        /* Read more about isConfirmed, isDenied below */
        if (result.isConfirmed) {

            //console.log($(rowIndex));
            $("body").mLoading();
            table.row(rowIndex).remove().draw(true);
            table.rows().invalidate('data')
                .draw(false);


            //var ngradeId = table.cell(rowIndex, 1).data();
            var sId = $("#hdnSId").val();
            if (ngradeId > 0 && sId > 0) {
                var commonRequest = { nGradeId: ngradeId, NumberYear: numberYear };

                $.ajax({
                    url: "../../api/AssessmentScore/DeleteStudentScore",
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(commonRequest),
                    success: function (response) {
                        $("body").mLoading('hide');
                        //CreateLog("", "", "DeleteSubmit", "");
                        if (response = "Success") {
                            Swal.fire({
                                icon: 'success',
                                title: 'ลบสำเร็จ',
                                //text: 'Something went wrong!',                      
                            })
                        }
                        //RedirectToMainPage();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("body").mLoading('hide');
                        Swal.fire({
                            icon: 'error',
                            title: 'ข้อผิดพลาด',
                            text: 'บางอย่างผิดพลาด', //Something went wrong
                            //footer: '<a href>Why do I have this issue?</a>'
                        })
                        //ShowPageError();
                    }
                });
            }
            else {
                $("body").mLoading('hide');
            }

        } else if (result.isDenied) {

        }
    })



}

function ScoreOnChange(controlId, nTerm, sPlaneId, nGradeId, rowIndex, oldValue, columnName) {
    console.log(table.cell(rowIndex, 22).data());
    var newValue = $(controlId).val();
    console.log("NewValue" + newValue);
    console.log("nTerm" + nTerm);
    console.log("sPlaneId" + sPlaneId);
    console.log("nGradeId" + nGradeId);
    console.log("rowIndex" + rowIndex);
    console.log("oldValue" + oldValue);
    console.log("columnName" + columnName);

    var IsGetScore100 = false;
    var IsGetGradeLabel = false;
    var IsGetBehaviorLabel = false;
    var IsGetReadWrite = false;
    var IsGetSamattana = false;
    var IsNCredit = false;
    var IsCourseTotalHour = false;

    var isvalidData = true;
    if (columnName == "GetScore100") {
        if (newValue > 100) {
            isvalidData = false;
        }
        else {
            table.cell(rowIndex, 12).data(newValue).draw();
            IsGetScore100 = true;
        }
    }
    else if (columnName == "GetGradeLabel") {
        table.cell(rowIndex, 13).data(newValue).draw();
        IsGetGradeLabel = true;
    }
    else if (columnName == "GetBehaviorLabel") {
        if (newValue > 3) {
            isvalidData = false;
        }
        else {
            table.cell(rowIndex, 14).data(newValue).draw();
            IsGetBehaviorLabel = true;
        }
    }
    else if (columnName == "GetReadWrite") {
        if (newValue > 3) {
            isvalidData = false;
        }
        else {
            table.cell(rowIndex, 15).data(newValue).draw();
            IsGetReadWrite = true;
        }
    }
    else if (columnName == "GetSamattana") {
        if (newValue > 3) {
            isvalidData = false;
        }
        else {
            table.cell(rowIndex, 16).data(newValue).draw();
            IsGetSamattana = true;
        }
    }
    else if (columnName == "NCredit") {
        table.cell(rowIndex, 10).data(newValue).draw();
        IsNCredit = true;
    }
    else if (columnName == "CourseTotalHour") {
        if ($.isNumeric(newValue)) {
            console.log(newValue);
            table.cell(rowIndex, 11).data(newValue).draw();
            IsCourseTotalHour = true;
        }
        else {
            console.log("isvalidData");
            isvalidData = false;
        }
    }

    table.row(rowIndex).invalidate('data').draw(false);
    if (isvalidData) {
        var idlist = [];
        table.rows().every(function (rowIdx, tableLoop, rowLoop) {
            var data = this.data();
            if (sPlaneId == data["sPlaneId"] && nTerm == data["nTerm"] && data["IsSavedData"] == true) {
                idlist.push(1);
            }
        });
    }
    if (idlist.length > 1) {
        isvalidData = false;
    }
    console.log(isvalidData);
    console.log(table.cell(rowIndex, 22).data());
    console.log(table.cell(rowIndex, 17).data());
   

    if (isvalidData && (table.cell(rowIndex, 17).data() == false || table.cell(rowIndex, 17).data() == null)) {
        if (table.cell(rowIndex, 22).data() == false) {
            table.cell(rowIndex, 22).data(true).draw();
            table.row(rowIndex).invalidate('data').draw(false);
        }

        //var ngradeId = table.cell(rowIndex, 1).data();
        var sId = $("#hdnSId").val();
        if (nTerm != "รายปี") {
            var studentAssessmentScoreUpdateRequest = {
                sID: sId, sPlaneID: sPlaneId, nGradeId: nGradeId,
                nTerm: nTerm,
                GetScore100: (IsGetScore100) ? newValue : "",
                nYear: table.cell(rowIndex, 2).data(),
                GetGradeLabel: (IsGetGradeLabel) ? newValue : "",
                GetBehaviorLabel: (IsGetBehaviorLabel) ? newValue : "",
                GetReadWrite: (IsGetReadWrite) ? newValue : "",
                GetSamattana: (IsGetSamattana) ? newValue : "",
                NCredit: (IsNCredit) ? newValue : null,
                IsNCredit: IsNCredit,
                IsGetScore100: IsGetScore100,
                IsGetGradeLabel: IsGetGradeLabel,
                IsGetBehaviorLabel: IsGetBehaviorLabel,
                IsGetReadWrite: IsGetReadWrite,
                IsGetSamattana: IsGetSamattana,
                IsCourseTotalHour: IsCourseTotalHour,
                CourseTotalHour: (IsCourseTotalHour) ? newValue : null,
                MethodName: "UpdateStudentsGradeTransferDetails",
                PlanId: table.cell(rowIndex, 3).data()
            };


            $.ajax({
                url: "../../api/AssessmentScore/UpdateStudentsGradeTransferDetails",
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(studentAssessmentScoreUpdateRequest),
                success: function (response) {
                    if (response > 0 && nGradeId == 0) {
                        if (nTerm != "รายปี") {
                            table.cell(rowIndex, 1).data(response).draw();
                            table.row(rowIndex).invalidate('data').draw(false);
                        }
                        else {
                            var params = $.extend({}, doAjax_params_default);
                            params['url'] = "../../api/AssessmentScore/GetStudentGradeInfo?sId=" + $("#hdnSId").val();
                            params['requestType'] = "GET";
                            params['successCallbackFunction'] = CreatePageContent;
                            doAjax(params);
                        }
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //$("body").mLoading('hide');
                    Swal.fire({
                        icon: 'error',
                        title: 'ข้อผิดพลาด',
                        text: 'บางอย่างผิดพลาด', //Something went wrong
                        //footer: '<a href>Why do I have this issue?</a>'
                    })
                    //ShowPageError();
                }
            });
        }
        else {  //Yearly
            $.ajax({
                url: "../../api/Common/GetTerms?nYear=" + table.cell(rowIndex, 2).data(),
                dataType: "json",
                type: "GET",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    console.log("Year On Change" + response);
                    if (response != null && response != "") {
                        $.each(response, function (index, Value) {
                            var studentAssessmentScoreUpdateRequest = {
                                sID: sId, sPlaneID: sPlaneId, nGradeId: nGradeId,
                                nTerm: Value.nTerm,
                                GetScore100: (IsGetScore100) ? newValue : "",
                                nYear: table.cell(rowIndex, 2).data(),
                                GetGradeLabel: (IsGetGradeLabel) ? newValue : "",
                                GetBehaviorLabel: (IsGetBehaviorLabel) ? newValue : "",
                                GetReadWrite: (IsGetReadWrite) ? newValue : "",
                                GetSamattana: (IsGetSamattana) ? newValue : "",
                                NCredit: (IsNCredit) ? newValue : null,
                                IsNCredit: IsNCredit,
                                IsGetScore100: IsGetScore100,
                                IsGetGradeLabel: IsGetGradeLabel,
                                IsGetBehaviorLabel: IsGetBehaviorLabel,
                                IsGetReadWrite: IsGetReadWrite,
                                IsGetSamattana: IsGetSamattana,
                                IsCourseTotalHour: IsCourseTotalHour,
                                CourseTotalHour: (IsCourseTotalHour) ? newValue : null,
                                MethodName: "UpdateStudentsGradeTransferDetails",
                                PlanId: table.cell(rowIndex, 3).data()
                            };


                            $.ajax({
                                url: "../../api/AssessmentScore/UpdateStudentsGradeTransferDetails",
                                type: "POST",
                                contentType: 'application/json; charset=utf-8',
                                dataType: 'json',
                                data: JSON.stringify(studentAssessmentScoreUpdateRequest),
                                success: function (response) {
                                    if (response > 0 && nGradeId == 0) {
                                        if (Value.sTerm == "1") {
                                            table.cell(rowIndex, 1).data(response).draw();
                                            table.cell(rowIndex, 5).data(Value.nTerm).draw();
                                            table.cell(rowIndex, 9).data("1").draw();
                                            table.row(rowIndex).invalidate('data').draw(false);
                                        }
                                        else if (Value.sTerm == "2") {
                                            var newRow = {
                                                RowNumber: (table.rows().count() + 1),
                                                nGradeId: 0,
                                                nYear: table.cell(rowIndex, 2).data(),
                                                nTerm: Value.nTerm,
                                                PlanId: table.cell(rowIndex, 3).data(),
                                                sPlaneId: table.cell(rowIndex, 4).data(),
                                                NumberYear: table.cell(rowIndex, 6).data(),
                                                STerm: "2",
                                                PlanName: table.cell(rowIndex, 7).data(),
                                                CourseCode: table.cell(rowIndex, 8).data(),
                                                CourseName: "",
                                                NCredit: table.cell(rowIndex,10).data(),
                                                CourseTotalHour: table.cell(rowIndex, 11).data(),
                                                GetScore100: table.cell(rowIndex, 12).data(),
                                                GetGradeLabel: table.cell(rowIndex, 13).data(),
                                                IsScoreFromScoreEntryPage: false,
                                                OptYear: [],
                                                OptPlanName: [],
                                                OptCourseCode: [],
                                                OptTerm: [],
                                                IsSavedData: true,
                                                //Is
                                            };
                                            table.row.add(newRow).draw(true);

                                            table.rows().invalidate('data')
                                                .draw(false);

                                            table.cell((rowIndex + 1), 1).data(response).draw();
                                            table.row((rowIndex + 1)).invalidate('data').draw(false);
                                        }
                                    }
                                },
                                error: function (jqXHR, textStatus, errorThrown) {
                                    //$("body").mLoading('hide');
                                    Swal.fire({
                                        icon: 'error',
                                        title: 'ข้อผิดพลาด',
                                        text: 'บางอย่างผิดพลาด', //Something went wrong
                                        //footer: '<a href>Why do I have this issue?</a>'
                                    })
                                    //ShowPageError();
                                }
                            });
                        });
                    }
                    else {
                        
                    }
                   
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    console.log(textStatus);
                }
            });
        }
        //Call Save Function
    }
    else {
       // throw alert
        Swal.fire({
            icon: 'error',
            title: '',
            text: 'มีหัวเรื่องอยู่แล้ว', //Something went wrong
            //footer: '<a href>Why do I have this issue?</a>'
        })
    }

    
}

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
    var stopCountingActiveAjaxConnections = doAjax_params['stopCountingActiveAjaxConnections'];
    //make sure that url ends with '/'
    /*if(!url.endsWith("/")){
     url = url + "/";
    }*/

    $.ajax({
        url: url,
        crossDomain: false,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
           
            //requestCount++;
            if (typeof beforeSendCallbackFunction === "function") {

                beforeSendCallbackFunction();
            }
        },
        success: function (data, textStatus, jqXHR) {
            console.log("Success");
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Ajax Error" + errorThrown);
            if (errorThrown == "Request Timeout") {
                ShowPageSessionTimeOutAlert();
            }
            else {
                ShowPageError();
            }

        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}


function ShowPageSessionTimeOutAlert() {
    $.confirm({
        title: '<h2>คำเตือน !</h>',
        content: '<h2>โปรดรีเฟรชหน้านี้</h>',
        buttons: {

            confirm: {
                label: '<i class="fa fa-check"></i> ดำเนินการ',
                action: function () {
                    console.log("Action Clicked");
                    window.location.href = window.location.protocol + "//" + window.location.host + "/Default.aspx";
                }
            }
        },
    });
}

function ShowPageError() {
    $.confirm({
        title: '<h2>คำเตือน !</h>',
        content: '<h2>เครือข่ายอินเทอร์เน็ตของท่านไม่เสถียร กรุณาตรวจสอบการเชื่อมต่ออินเทอร์เน็ตและลองใหม่อีกครั้ง</h>',
        buttons: {

            confirm: {
                label: '<i class="fa fa-check"></i> ดำเนินการ',
                action: function () {
                    console.log("Action Clicked");
                    location.reload();
                }
            }
        },
    });
}