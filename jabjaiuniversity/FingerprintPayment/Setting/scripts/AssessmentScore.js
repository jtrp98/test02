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
    'stopCountingActiveAjaxConnections':false
};
var tableName = '#AssessmentScoreGrid';
var table = $(tableName).DataTable(), toggleGroupColumns;
var languageCode = "TH"; //default language code
var response;
var htmlTemplate;
var visbleColumnList = [];
var assessmentColumn = [];
var IsImportFromExcel = false;
var studentResignedRows = [];
var IsGradeDetailAvailable = false;
var IsUserAllowedToEditRatio = false;
var activeAjaxConnections = 0;
var requestCount = 0;
var maxMidTermFirstControlId = "", maxFinalTermFirstControlId = "", maxBeforeMidTermTotalFirstControlId = "", maxAfterMidTermTotalFirstControlId = "";
var AssessmentMaxScoreValidationList = [];
var IsNewAssessmentCreated = false;
var IsRequestForCurrentAcademicYear = false;
//Text for Language Specific
var languageContentSource = [{
    LanguageCode: "EN", SlNOHeaderText: "No.", StudentCodeHeaderText: "Student Code", StudentNameHeaderText: "Student Name", TestScoreHeaderText: "Test Score", SumOfAllScoresHeaderText: "Score SUM", Total100PerHeaderText: "Total 100%", Total50PerHeaderText: "Total 50", Term2Total100PerHeaderText: "Term 2 - Total 100%", Term2Total50PerHeaderText: "Term 2 - Total 100%", Term1AndTerm2ScoreTotalHeaderText: "Term 1 And Term 2 Average Score", GradeHeaderText: "Grade", DesirableFeatureHeaderText: "Behavior", ReadWriteHeaderText: "Read, think, analyze and write.", PerformanceScoreHeaderText: "Performance Score", OtherAssessmentsHeaderText: "Other Assessments", MidTermHeaderText: "Mid Term", FinalHeaderText: "Final", FullScoreHeaderText: "Full Score", lblCourseName: "Course", lblSubLevel: "Classroom", lblYearHeader: "Year", lblHomeRoomTeacher: "Homeroom's teacher", lblClassTeacher: "Lecturer's teacher", lblSubmitPeriod: "Score Submit Period", lblTabBeforeMidTerm: "Exercise 1-20", lblTabMidTerm: "Mid Term", lblTabAfterMidTerm: "Measure Unit", lblTabFinal: "Final", lblTabBehavior: "Behavior", lblTabReadWrite: "Read, think, analyze and write.", lblTabSamattana: "Performance Score", lnkSetting: "Setting", lnkImportFromExcel: "Import scores from Excel", lblScoreRatioTitle: "Score Ratio", lblScorePercentage: "Exercise", lblBeforeMidTermPercentage: "Exercise", lblAfterMidTermPercentage: "Exercise", lblMidTermPercentage: "Mid Term", lblFinalTermPercentage: "Final Term", lblPassingCutOffPercentage: "% Cut off for passing any Exercise", lblEditForm: "Edit form", lblGradeDicimal: "Show dicimal.", lblGradeAutoReadScore: "Disable automatic calculation on Reading, Thinking tab.", lblGradeCloseBehaviorReadWrite: "Close Behavior and Reading, Writing, Thinking tab.", lblCloseGradeTab: "Close Grade tab.", lblGradeCloseSamattana: "Close Capability tab.", lblGradeShareData: "Enable automatic sharing course's information to others. ", BeforeMidTermTypeHeaderText: "Exercise", MidTermTypeHeaderText: "Mid Term", AfterMidTermTypeHeaderText: "Exercise", FinalTermTypeHeaderText: "Final Term", BehaviourTypeHeaderText: "Behavior", InValidMaxScore: "Invalid Max Score. Some student mark is more than the maximum mark. Please correct the student mark.", CloseText: "Close", BeforeAfterMidTermHeaderText: "Total Assessment Score", InValidScore: "Invalid Score. Student mark is more than the maximum mark. Please correct the student mark.", InValidBehaviour: "Invalid input, Please use 0 1 2 3 number only", ErrorCorrectionRequest: "Please correct the privious error before continue next step", btnManage: "Manage", lblGradeAddBehavior: "Add Additional Behavior tab.", lblGradeAddSamattana: "Add Additional Samattana tab.", lblGradeAutoBehaviorScore: "Enable automatic calculation on Behavior tab.", lblOptionMid: "Add Additional Midterm score tab.", lblOptionFinal: "Add Additional Finalterm score tab.", lblGradeAddCheewat: "Add Measure Unit score tab.", btnCancel: "Cancel", AssessmentNameDialogHeader: "Edit Name", btnClose: "Close", DeleteConfirmationDialogHeaderText: "Once deleted, values cannot be collected.Are you sure you want to delete the value?", DeleteGetReadWriteErrorMessage: "Turn off the system for calculating scores, read, analyze and write automatically Is Enabled. So Can't Delete", DeleteGetBehaviorLabelErrorMessage: "Add a topic, sub-score of the desired feature Is Enabled. So Can't Delete", Yes: "Yes", btnFinalSubmit: "Last Submit", btnDeleteSubmit: "Delete Score", btnDraftSave: "Submit", btnImportConfirm: "Confirm", lblLastUpdated: "Last updated date: ", lnkPrint: "Export To Excel", lnkLog:"Activity log", ScoreProportionValidationMessage: "Please enter score proportion", InValidBehaviourMaxScore: "Invalid Max Score. Max score should not be more than 3", lblAssessmentSharedFromOtherRoomHeader: "Data imported from the room", lblAssessmentSharingTo: "Sharing to", lblSelectedRoomforSharing: "Selected Room", ReadWriteTypeHeaderText: "Read, think, analyze and write", SamattanaTypeHeaderText: "Performance Score", InValidReadWriteMaxScore: "Invalid Max Score. Max score should not be more than 3", InValidSamattanaMaxScore: "Invalid Max Score. Max score should not be more than 3", lblShowFullScore100Percentage:"Full Score - 100%"
},
{
    LanguageCode: "TH", SlNOHeaderText: "ลำดับ", StudentCodeHeaderText: "รหัสนักเรียน", StudentNameHeaderText: "ชื่อ - นามสกุล", TestScoreHeaderText: "คะแนนสอบ",
    SumOfAllScoresHeaderText: "ผลรวมคะแนนทั้งหมด", Total100PerHeaderText: "รวม 100% ภาคเรียนที่ 1", Total50PerHeaderText: "รวม 50 ภาคเรียนที่ 1", Term2Total100PerHeaderText: "รวม 100% ภาคเรียนที่ 2", Term2Total50PerHeaderText: "รวม 50 ภาคเรียนที่ 2", Term1AndTerm2ScoreTotalHeaderText: "รวม 100% เฉลี่ย 2 ภาคเรียน", GradeHeaderText: "เกรด", DesirableFeatureHeaderText: "คุณลักษณะ อันพึงประสงค์",    ReadWriteHeaderText: "อ่าน คิด วิเคราะห์และเขียน", PerformanceScoreHeaderText: "สมรรถนะ", OtherAssessmentsHeaderText: "ผลประเมินอื่นๆ",
    MidTermHeaderText: "กลางภาค", FinalHeaderText: "ปลายภาค", FullScoreHeaderText: "คะแนนเต็ม", lblCourseName: "วิชา", lblSubLevel: "ชั้นเรียน", lblYearHeader: "ปีการศึกษา", lblHomeRoomTeacher: "ครูประจำชั้น", lblClassTeacher: "ครูผู้สอน", lblSubmitPeriod: "ช่วงเวลาที่เปิดให้บันทึกคะแนน", lblTabBeforeMidTerm: "คะแนนเก็บ", lblTabMidTerm: "กลางภาค", lblTabAfterMidTerm: "คะแนนเก็บหลังกลางภาค", lblTabFinal: "ปลายภาค", lblTabBehavior: "คุณลักษณะฯ", lblTabReadWrite: "อ่าน คิด วิเคราะห์และเขียน", lblTabSamattana: "สมรรถนะ", lnkSetting: "ตั้งค่า", lnkImportFromExcel: "นำเข้าคะแนนจาก Excel", lblScoreRatioTitle: "สัดส่วนคะแนน", lblScorePercentage: "คะแนนเก็บ", lblBeforeMidTermPercentage: "คะแนนเก็บ", lblAfterMidTermPercentage: "คะแนนเก็บหลังกลางภาค", lblMidTermPercentage: "กลางภาค", lblFinalTermPercentage: "ปลายภาค", lblPassingCutOffPercentage: "% ตัดผ่านของคะแนนเก็บแต่ละจุด", lblEditForm: "แก้ไขฟอร์ม", lblGradeDicimal: "แสดงจุดทศนิยม", lblGradeAutoReadScore: "เพิ่มหัวข้อคะแนนหน่วยย่อยอ่าน คิด วิเคราะห์และเขียน (ฟังชั่นคำนวนอัติโนมัติจะใช้งานไม่ได้)", lblGradeCloseBehaviorReadWrite: "ปิดหัวข้อคุณลักษณะฯ และอ่าน คิด วิเคราะห์ฯ", lblCloseGradeTab: "ปิดหัวข้อเกรด", lblGradeCloseSamattana: "ปิดหัวข้อสมรรถนะ", lblGradeShareData: "เปิดการใช้ข้อมูลชื่อและคะแนนเต็มของหน่วยต่างๆร่วมกับห้องอื่น",
    BeforeMidTermTypeHeaderText: "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่", MidTermTypeHeaderText: "กลางภาค", AfterMidTermTypeHeaderText: "คะแนนเก็บ/หน่วยชี้วัด หน่วยที่", FinalTermTypeHeaderText: "ปลายภาค", BehaviourTypeHeaderText: "คุณลักษณะอันพึงประสงค์", InValidMaxScore: "คะแนนเต็มสามารถสร้างได้สูงสุด 100 คะแนนต่อช่อง และห้ามต่ำกว่าคะแนนของนักเรียน", CloseText: "ปิด", BeforeAfterMidTermHeaderText: "ผลรวมคะแนนเก็บ", InValidScore: "คะแนนไม่ถูกต้อง คะแนนต้องไม่เกินคะแนนเต็มที่ตั้งไว้ โปรดแก้ไขและลองใหม่อีกครั้ง", InValidBehaviour: "ไม่สามารถกรอกตัวเลขมากกว่า 3 ได้ กรุณากรอกค่า 0 1 2 3 เท่านั้น", ErrorCorrectionRequest: "โปรดแก้ไขข้อผิดพลาดที่แจ้งก่อนหน้า ก่อนดำเนินการต่อไป", btnManage: "จัดการ", lblGradeAddBehavior: "เพิ่มหัวข้อคะแนนหน่วยย่อยของคุณลักษณะอันพึงประสงค์", lblGradeAutoBehaviorScore: "เปิดระบบคำนวนคะแนนคุณลักษณะอันพึงประสงค์ แบบอัตโนมัติ", lblOptionMid: "เพิ่มหัวข้อคะแนนกลางภาค",
    lblOptionFinal: "เพิ่มหัวข้อคะแนนปลายภาค", lblGradeAddSamattana: "เพิ่มหัวข้อสมรรถนะ", lblGradeAddCheewat: "เพิ่มหัวข้อคะแนนเก็บหลังกลางภาค", btnCancel: "ย้อนกลับ", AssessmentNameDialogHeader: "แก้ไขชื่อ", btnClose: "ปิด", DeleteConfirmationDialogHeaderText: "เมื่อลบแล้ว ไม่สามารถเก็บค่าได้ คุณแน่ใจหรือว่าต้องการลบค่า", DeleteGetReadWriteErrorMessage: "ปิดระบบเพื่อคำนวณคะแนนอ่านวิเคราะห์และเขียนโดยอัตโนมัติ ดังนั้นไม่สามารถลบได้", DeleteGetBehaviorLabelErrorMessage: "เพิ่มหัวข้อคะแนนย่อยของคุณสมบัติที่ต้องการเปิดใช้งาน ดังนั้นไม่สามารถลบได้", Yes: "ใช่", btnFinalSubmit: "บันทึกครั้งสุดท้าย", btnDeleteSubmit: "ลบคะแนน", btnDraftSave: "บันทึกร่าง", btnImportConfirm: "ยืนยัน", lblLastUpdated: "วันที่อัพเดทล่าสุด: ", lnkPrint: "ส่งออกไปยัง Excel", lnkLog:"Activity Log", ScoreProportionValidationMessage: "โปรดป้อนสัดส่วนคะแนน", InValidBehaviourMaxScore: "คะแนนสูงสุดไม่ถูกต้อง คะแนนสูงสุดไม่ควรเกิน 3", lblAssessmentSharedFromOtherRoomHeader: "ข้อมูลนำเข้ามาจากห้อง", lblAssessmentSharingTo: "กรุณาคลิกตรงนี้เลือกห้องเรียน", lblSelectedRoomforSharing: "ห้องที่เลือก", ReadWriteTypeHeaderText: "อ่าน คิด วิเคราะห์และเขียน", SamattanaTypeHeaderText: "สมรรถนะ", InValidReadWriteMaxScore: "คะแนนสูงสุดไม่ถูกต้อง คะแนนสูงสุดไม่ควรเกิน 3", InValidSamattanaMaxScore: "คะแนนสูงสุดไม่ถูกต้อง คะแนนสูงสุดไม่ควรเกิน 3", lblShowFullScore100Percentage: " คะแนนเต็ม  100 คะแนน"
}];

var languageContent = $.grep(languageContentSource, function (n) {
    return n.LanguageCode == languageCode; //fetch text based on language code
});
var isPageError = false;
var errorControlId, scoreMidTermColumnIndex, scoreFinalTermColumnIndex, TotalScoreColumnIndex, TotalFor100PercentageColumnIndex, Term2TotalFor100PercentageColumnIndex, Term1AndTerm2ScoreTotalColumnIndex, Term1AndTerm2GradeLabelColumnIndex, beforeAfterMidTermTotalScoreColumnIndex, getGradeLabelColumnIndex, getBehaviorLabelColumnIndex, getReadWriteColumnIndex, getSamattanaColumnIndex, getSpecialColumnIndex, getBehaviorTotalColumnIndex, getReadWriteTotalColumnIndex, getSamattanaTotalColumnIndex, sPlaneIDColumnIndex, nStudentStatusColumnIndex, scoreBeforeMidTermColumnIndex, scoreAfterMidTermColumnIndex, TotalFor50PercentageColumnIndex, Term2TotalFor50PercentageColumnIndex;

var nGradeId;
var nTermSubLevel2;
var nTerm;
var LevelName; var roomListForSharingAssessment;

//var initialFRatioMidTerm;
//var initialFRatioLateTerm;
//var initialFRatioQuizPass;
var checkScoreEntered = false;

$(document).ready(function () {

    $("body").mLoading();
    $("#myTab").hide();
    //$("#ddlScorePercentage").hide();
    $("#ddlBeforeMidTermPercentage").hide();
    $("#ddlAfterMidTermPercentage").hide();
    $("#ddlMidTermPercentage").hide();
    $("#ddlFinalTermPercentage").hide();
    $("#ddlPassingCutOffPercentage").hide();
    $("div.dropdown > button").hide();
    $("#btnCancel").hide();
    $("#btnFinalSubmit").hide();
    $("#btnDeleteSubmit").hide();
    $("#btnDraftSave").hide();
    $("#lblLastUpdated").hide();
    $("#lblLastUpdatedValue").hide();
    //initialFRatioMidTerm = $("#ddlMidTermPercentage").val();
    //initialFRatioLateTerm = $("#ddlFinalTermPercentage").val();
    //initialFRatioQuizPass = $("#ddlScorePercentage").val();
    //Load the data table content
    GetTableContent();

    //Header AssessmentName Image Click
    $(document).on("click", ".OpenAssessmentNameDialog", function () {
        var assessmentId = $(this).data('id');
        $(".modal-body #AssessmentId").val(assessmentId);
        $(".modal-body #AssessmentNameOldValue").val($("span[name=" + assessmentId + "]").html());
        $("#AssessmentName").focus();
        $("#AssessmentName").val($("span[name=" + assessmentId + "]").html());
        $("#AssessmentName").focus();
    });

    //Modal Dialog - Assessment Name Change - Update to Table Header/TAssessment
    $("#AssessmentName").keyup(function () {
        $("span[name=" + $("#AssessmentId").val() + "]").html($("#AssessmentName").val())
    });

    $("#AssessmentName").bind("paste", function (e) {
        // access the clipboard using the api
        console.log(e.originalEvent.clipboardData.getData('text'));
        var pastedData = e.originalEvent.clipboardData.getData('text/plain');
        //$("#AssessmentName").val(pastedData);
        console.log(pastedData);
    });

    $(document).on("click", "#lnkLog", function () {
        $("body").mLoading();

        var gradeLogRequest = {
            CourseCode: response.GradeDTO.CourseCode,
            SubLevel: response.GradeDTO.SubLevel,
            nTSubLevel2: response.GradeDTO.nTSubLevel2,
            CourseName: response.GradeDTO.CourseName,
            NumberYear: getUrlParameter("year"),
            sTerm: getUrlParameter("term")
            
        };

        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/GetGradeLog";
        params['data'] = JSON.stringify(gradeLogRequest);
        params['requestType'] = "POST";
        params['successCallbackFunction'] = ShowGradeLog;
        doAjax(params);

       
    });
});
var logDataTable;
function ShowGradeLog(logResponse) {
    $("#DisplayLog").modal({ backdrop: 'static', keyboard: false });
    if (logDataTable != undefined) {
        logDataTable.destroy();
        logDataTable.clear().draw();
    }
    logDataTable = $("#GradeLog").DataTable({
        bAutoWidth: false,
        ordering: false,
        paging: false,
        searching: false,
        lengthChange: false,
        bInfo: false,
        scrollCollapse: true,
        fixedHeader: false,
        deferRender: true,
        data: logResponse.data,
        columns: logResponse.column,
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
            
        },
        /*columnDefs: GradeLogColumnDef()*/
    });
    $("body").mLoading('hide');
}

function OpenAssessmentDefaultValueDialogClick(assessmentId, assessmentTypeName) {
   
    $(".modal-body #AssessmentIdDefaultValue").val(assessmentId);
    $(".modal-body #AssessmentTypeName").val(assessmentTypeName);
    $("#AssessmentDefaultValue").focus();
    $("#AssessmentDefaultValue").val("");
}

function GetCurrentDateTime(date) {
    var datetime = "";
    if (languageCode == "EN") {
        var currentdate = (date == "") ? new Date() : date;
        datetime = "" + new Date(currentdate).getDate() + "/"
            + (new Date(currentdate).getMonth() + 1) + "/"
            + new Date(currentdate).getFullYear() + " "
            + formatAMPM(currentdate) ;
        $("#lblLastUpdatedValue").html(datetime);
    }
    else {
        var request = { UpdatedDate: (date == "") ? null : date };

        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/ConvertDateTimeToThai";
        params['data'] = JSON.stringify(request);
        params['requestType'] = "POST";
        datetime = params['successCallbackFunction'] = ConvertDateTimeToThai;
        doAjax(params);
    }
}

function formatAMPM(date) {
    var hours = new Date(date).getHours();
    var minutes = new Date(date).getMinutes();
    var seconds = new Date(date).getSeconds()
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ':' + seconds + ' ' + ampm;
    return strTime;
}

function ConvertDateTimeToThai(data) {
    $("#lblLastUpdatedValue").html(data);
}


function GetTableContent() {
    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../Grade/AssessmentScoreTableHeaderTemplate.html"; //Get the Datatable Header content and generate the header column dynamically
    params['dataType'] = "";
    params['successCallbackFunction'] = BindStudentAssessmentScore;
    doAjax(params);
}

var BindStudentAssessmentScore = function BindStudentAssessmentScore(data) {
    htmlTemplate = data;
    var assessmentScoreRequest = { nTSubLevel: getUrlParameter("idlv"), sPlaneID: getUrlParameter("id"), nTermSubLevel2: getUrlParameter("idlv2"), sTerm: getUrlParameter("term"), NumberYear: getUrlParameter("year"), PlanCourseId: getUrlParameter("PlanCourseId")};

    //Get the datatable rows value
    $.ajax({
        ContentType: "application/json; charset=utf-8",
        url: "../../api/AssessmentScore/GetStudentAssessmentScore",
        type: "Post",
        data: assessmentScoreRequest,
        headers: {
            '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val()
        },
        success: function (responseScore) {
            response = responseScore;
            toggleGroupColumns = $.grep(response.ToggleGroupColumns, function (n) {
                return n.AssessmentTypeName !== "";
            });

            GetDefaultAssessmentColumnList();
            CreatePageContent(response, htmlTemplate);
        },
        error: function (error) {
            ShowPageError();
        }
    });
}

//Update the Header with propert text based on language selection
function CreatePageContent(response, htmlTemplate) {
    $("#myTab").show();
    //$("#ddlScorePercentage").show();
    $("#ddlBeforeMidTermPercentage").show();
    $("#ddlAfterMidTermPercentage").show();
    $("#ddlMidTermPercentage").show();
    $("#ddlFinalTermPercentage").show();
    $("#ddlPassingCutOffPercentage").show();
    $("div.dropdown > button").show();
    $("#btnCancel").show();
    $("#btnFinalSubmit").show();

    if (response.GradeDTO.GradeRole == 1) {
        $("#btnDeleteSubmit").show();
    }

    IsNewAssessmentCreated = response.IsNewAssessmentCreated;
    IsRequestForCurrentAcademicYear = response.IsRequestForCurrentAcademicYear;

    //Hide Import Score From Excel For Previouse Academic Year
    if (!response.IsRequestForCurrentAcademicYear && IsGradeDetailAvailable)
    {
        $("#ImportFromExcelLink").hide();
    }
    
    $("#btnDraftSave").show();
    $("#lblLastUpdated").show();
    $("#lblLastUpdatedValue").show();

    table.destroy();
    table.clear().draw();
    $(tableName + " thead").empty();
    $(tableName + " tbody").empty();

    LevelName = response.LevelName;

    //Bind The Page Header Details / 
    BindData(response.GradeDTO);

    GetColumnIndex(response);

    //Header Content Update
    var headerText = GetHeaderContent(htmlTemplate, response);
    $(headerText).appendTo(tableName + '>thead');

    response.column[4].data = (languageCode == "EN") ? "StudentNameEn" : "StudentName";
    response.column[4].className = (languageCode == "EN") ? "StudentNameEn" : "StudentName";
    response.column[getBehaviorLabelColumnIndex].visible = $("#chkGradeCloseBehaviorReadWrite").is(':checked') ? false : true;
    response.column[getReadWriteColumnIndex].visible = $("#chkGradeCloseBehaviorReadWrite").is(':checked') ? false : true;
    //If not Mathayom
    if (LevelName != null && LevelName != "" && LevelName != "ปวช." && LevelName != "ปวส." && LevelName != "มัธยมศึกษา") {
        response.column[Term1AndTerm2GradeLabelColumnIndex].visible = $("#chkCloseGradeTab").is(':checked') ? false : true;
        response.column[TotalFor50PercentageColumnIndex].visible = $("#chkShowFullScore100Percentage").is(':checked') ? false : true;
        response.column[Term2TotalFor50PercentageColumnIndex].visible = $("#chkShowFullScore100Percentage").is(':checked') ? false : true;
    }
    else {
        response.column[getGradeLabelColumnIndex].visible = $("#chkCloseGradeTab").is(':checked') ? false : true;
    }
    response.column[getSamattanaColumnIndex].visible = $("#chkGradeCloseSamattana").is(':checked') ? false : true;

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
        //keys: false,
        //fixedColumns: {
        //    leftColumns: 5,
        //    rightColumns: 0
        //},
        data: response.data,
        columns: response.column,
        fnRowCallback: function (nRow, aData, iDisplayIndex) {
            decorateRow(nRow, (aData["nStudentStatus"] == null) ? 0 : aData["nStudentStatus"], iDisplayIndex);
            return nRow;
        },
        fnInitComplete: function () {
            // Event handler to be fired when rendering is complete (Turn off Loading gif for example)
            console.log('Datatable rendering complete');

            $('[data-toggle="tooltip"]').tooltip({
                trigger: 'hover',
                html: true
            });

            EnableDisableBehaviourTab(false);
            EnableDisableAfterMidTermTab(false);

            //Enable Disbale the ReadWrite and Performance Tab
            EnableDisableReadWriteTab(false);
            EnableDisableSamattanaTab(false);

            if (IsImportFromExcel)
            {
                IsImportFromExcel = false;
                UpdateStudentsGradeValueInDataTableAndDB();
            }
            




            $("body").mLoading("hide");
        },
        columnDefs: ColumnDef()
    });

    VerifyTotalUpdatedCorrectly();
    

    //ToggleTabColumn(table);
    HighLightAllTotalScores();

    if (response.IsGetBehaviorLabelLocked) {
        $("#GetBehaviorLabelImportIcon").hide();
        $("#GetBehaviorLabelDeleteIcon").hide();
    }
    if (response.IsGetReadWriteLocked) {
        $("#GetReadWriteImportIcon").hide();
        $("#GetReadWriteDeleteIcon").hide();
    }
    if (response.IsGetSamattanaLocked) {
        $("#GetSamattanaImportIcon").hide();
        $("#GetSamattanaDeleteIcon").hide();
    }

    var difference = $(assessmentColumn).not(response.LockedAssessmentColumns).get();
    if (difference.length == 0) {
        $("#btnFinalSubmit").hide();
        $("#ImportFromExcelLink").hide();
        $("#btnDraftSave").hide();
        $("#btnDeleteSubmit").hide();
    }
}

function VerifyTotalUpdatedCorrectly() {
    console.log("VerifyTotalUpdatedCorrectly");
    $("body").mLoading();
    if (maxBeforeMidTermTotalFirstControlId != "" && $(maxBeforeMidTermTotalFirstControlId) != 'undefined') {
        console.log("maxBeforeMidTermTotalIssue");
        $('#TabBeforeMidTerm a').trigger('click');
        $(maxBeforeMidTermTotalFirstControlId).trigger("change");
        maxBeforeMidTermTotalFirstControlId = "";
    }
    if (maxAfterMidTermTotalFirstControlId != "" && $(maxAfterMidTermTotalFirstControlId) != 'undefined') {
        console.log("maxafterMidtermtotalissue");
        $('#TabAfterMidTerm a').trigger('click');
        $(maxAfterMidTermTotalFirstControlId).trigger("change");
        maxAfterMidTermTotalFirstControlId = "";
    }
    if (maxMidTermFirstControlId != "" && $(maxMidTermFirstControlId) != 'undefined') {
        console.log("maxmidtermtotalissue");
        $('#TabMidTerm a').trigger('click');
        $(maxMidTermFirstControlId).trigger("change");
        maxMidTermFirstControlId = "";
    }
    if (maxFinalTermFirstControlId != "" && $(maxFinalTermFirstControlId) != 'undefined') {
        console.log("maxfinaltermtotalissue");
        $('#TabFinal a').trigger('click');
        $(maxFinalTermFirstControlId).trigger("change");
        maxFinalTermFirstControlId = "";
    }

    var rowIndex = 0;

    //console.log(toggleGroupColumns);
    //console.log(response.column[5].data);
    //console.log(response.column[35].data);
    //console.log(response.column[25].data);
    //console.log(response.column[55].data);

    //$("input[name=" + response.column[5].data + "]").trigger("change");
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
       
        var data = this.data();
        //console.log(data[0]);
        var scoreAfterMidTerm, scoreMidTerm, scoreFinalTerm, scoreBeforeMidTerm;
        scoreBeforeMidTerm = (data.ScoreBeforeMidTerm == null || data.ScoreBeforeMidTerm == "") ? "0" : data.ScoreBeforeMidTerm;
        scoreAfterMidTerm = (data.ScoreAfterMidTerm == null || data.ScoreAfterMidTerm == "") ? "0" : data.ScoreAfterMidTerm;
        scoreMidTerm = (data.ScoreMidTerm == null || data.ScoreMidTerm == "") ? "0" : data.ScoreMidTerm;
        scoreFinalTerm = (data.ScoreFinalTerm == null || data.ScoreFinalTerm == "") ? "0" : data.ScoreFinalTerm;
        //console.log(scoreMidTerm);
        //console.log(GetSumOfStudentScoreForCorrection("Mid Term", rowIndex));
        var scoreBeforeMidTermCorrectValue = GetSumOfStudentScoreForCorrection("Before Mid Term", rowIndex);
        var scoreAfterMidTermCorrectValue = GetSumOfStudentScoreForCorrection("After Mid Term", rowIndex);
        var scoreMidTermCorrectValue = GetSumOfStudentScoreForCorrection("Mid Term", rowIndex);
        var scoreFinalTermCorrectValue = GetSumOfStudentScoreForCorrection("Final", rowIndex);
        if (parseFloat(scoreBeforeMidTerm).toFixed(2) != parseFloat(scoreBeforeMidTermCorrectValue).toFixed(2))
        {
            console.log("Before Mid Term Total Issue" + data.sStudentId);
            console.log(scoreBeforeMidTerm);
            console.log(scoreBeforeMidTermCorrectValue);
            $('#TabBeforeMidTerm a').trigger('click');
            var assessmentControlId = $("input[name=" + response.column[5].data + "]")[rowIndex-1];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");

            assessmentControlId = $("input[name=" + response.column[5].data + "]")[rowIndex];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");
        }

        if (parseFloat(scoreAfterMidTerm).toFixed(2) != parseFloat(scoreAfterMidTermCorrectValue).toFixed(2)) {
            console.log("After Mid Term Total Issue" + data.sStudentId);
            console.log(scoreAfterMidTerm);
            console.log(scoreAfterMidTermCorrectValue);
            $('#TabAfterMidTerm a').trigger('click');
            var assessmentControlId = $("input[name=" + response.column[35].data + "]")[rowIndex-1];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");

            var assessmentControlId = $("input[name=" + response.column[35].data + "]")[rowIndex];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");
        }

        if (parseFloat(scoreMidTerm).toFixed(2) != parseFloat(scoreMidTermCorrectValue).toFixed(2)) {
            console.log("Mid Term Total Issue" + data.sStudentId);
            console.log(scoreMidTerm);
            console.log(scoreMidTermCorrectValue);
            console.log(response.column[25].data);
            console.log("rowIndex" + rowIndex);
            $('#TabMidTerm a').trigger('click');
            var assessmentControlId = $("input[name=" + response.column[25].data + "]")[rowIndex-1];
            if (assessmentControlId != 'undefined') {
                $(assessmentControlId).trigger("change");
            }

            var assessmentControlId = $("input[name=" + response.column[25].data + "]")[rowIndex];
            if (assessmentControlId != 'undefined') {
                $(assessmentControlId).trigger("change");
            }

        }
        if (parseFloat(scoreFinalTerm).toFixed(2) != parseFloat(scoreFinalTermCorrectValue).toFixed(2)) {
            console.log("Final Term Total Issue" + data.sStudentId);
            console.log(scoreFinalTerm);
            console.log(scoreFinalTermCorrectValue);
            $('#TabFinal a').trigger('click');
            var assessmentControlId = $("input[name=" + response.column[55].data + "]")[rowIndex-1];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");

            var assessmentControlId = $("input[name=" + response.column[55].data + "]")[rowIndex];
            if (assessmentControlId != 'undefined')
                $(assessmentControlId).trigger("change");
        }
        rowIndex = rowIndex + 1;
    });

   
    $("body").mLoading("hide");
}

//Change the background color based on CutOff Mark
function decorateRow(row, nStudentStatus, iDisplayIndex) {

    if (nStudentStatus == 1 || nStudentStatus == 2 || nStudentStatus == 3 || nStudentStatus == 5 || nStudentStatus == 6 || nStudentStatus == 7) {
        $(row).css("background-color", "#fff3cd");
        if ($.inArray((iDisplayIndex + 1), studentResignedRows) == -1) {
            studentResignedRows.push(iDisplayIndex + 1);
        }
    }
    else {
        $(row).css("background-color", "");
    }
}

//DataTable Column Definition
function ColumnDef() {

    var getSpecialDDLValues = [
        { Value: "-1", Text: "ไม่มี" },
        { Value: "1", Text: "ร" },
        { Value: "2", Text: "มส" },
        { Value: "3", Text: "มก" },
        { Value: "4", Text: "ผ" },
        { Value: "5", Text: "มผ" },
        { Value: "6", Text: "อื่นๆ" },
        { Value: "7", Text: "ขร" },
        { Value: "8", Text: "ขส" },
        { Value: "9", Text: "ท" },
        { Value: "10", Text: "ดีเยี่ยม" },
        { Value: "11", Text: "ดี" },
        { Value: "12", Text: "พอใช้" },
        { Value: "13", Text: "ปรับปรุง" },

    ];
    var opts = "";
    $.each(getSpecialDDLValues, function (index, Value) {
        opts = opts + "<option value='" + Value.Value + "'>" + Value.Text +"</options>";
    });

    //Get the Assessment Column List for Adding Text Box
    assessmentColumn = [];
    $.each(toggleGroupColumns, function (index, Value) {
        $.each(Value.Columns, function (colIndex, colValue) {
            assessmentColumn.push(colValue);
        });
    });
    var columnDef = [
        { width: "30px", targets: 0 },
        { width: "7%", targets: 3 },
        {
            width: "200px",
            render: function (data, type, row, meta) {
                if (languageCode != "EN") {
                    return data;
                }
                else {
                    return row["StudentNameEn"];
                }
            },
            targets: 4,
        },
        {
            width: "65px", render: function (data, type, row, meta) {

                var isLocked = $.grep(response.LockedAssessmentColumns, function (n) {
                    return n == meta.col;
                });

                data = (data == null || isNaN(data)) ? '' : data;
              
                if (isLocked.length == 0 && row["nStudentStatus"] != 1 && row["nStudentStatus"] != 2 && row["nStudentStatus"] != 3 && row["nStudentStatus"] != 5 && row["nStudentStatus"] != 6 && row["nStudentStatus"] != 7 ) {
                    var className = "AssessmentScoreBox";
                    if (data != "" && $("#Max" + meta.settings.aoColumns[meta.col].data).val() != "" && $("#ddlPassingCutOffPercentage").val() > -1) {
                        var cutOfMark = (parseFloat($("#Max" + meta.settings.aoColumns[meta.col].data).val()) * parseFloat($("#ddlPassingCutOffPercentage").val())) / 100;
                        
                        if (data < cutOfMark) {
                            var className = "AssessmentScoreBox alert-warning";
                        }
                    }
                    var assessmentTypeName = GetAssessmentTypeName(meta.col);

                    var studentMaxScorelist = $.grep(AssessmentMaxScoreValidationList, function (n) {
                        return n.AssessmentId == meta.settings.aoColumns[meta.col].data && n.SId == row["sID"];
                    });

                    if (studentMaxScorelist.length == 0) {
                        AssessmentMaxScoreValidationList.push({ AssessmentId: meta.settings.aoColumns[meta.col].data, StudentMaxScore: data, SId: row["sID"]});
                    }
                    else if (studentMaxScorelist[0].StudentMaxScore < data && data != "") {
                        studentMaxScorelist[0].StudentMaxScore = data;
                    }

                    //meta.settings.aoColumns[meta.col].data  -- Assessment Id  - Column Name generated with Assessment Id
                    return '<input type="text" oncopy="return false" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '"  onFocus=(this.oldValue=this.value) onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data +'\')" onkeypress="return isNumberKey(event, this,\'' + assessmentTypeName + '\')" value="' + data + '" class="' + className + '" onchange="AssessmentScoreOnChange(this, ' + row["sID"] + ', ' + row["sPlaneID"] + ', ' + row["nGradeId"] + ', ' + meta.settings.aoColumns[meta.col].data + ', ' + row["RowNumber"] + ', ' + meta.col + ',\'' + assessmentTypeName + '\', this.oldValue)" />';
                }
                return data;
                
            },
            targets: assessmentColumn
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (!$("#chkGradeAddBehavior").is(':checked') && !response.IsGetBehaviorLabelLocked && row["nStudentStatus"] != 1 && row["nStudentStatus"] != 2 && row["nStudentStatus"] != 3 && row["nStudentStatus"] != 5 && row["nStudentStatus"] != 6 && row["nStudentStatus"] != 7) {
                    return '<input type="text" oncopy="return false" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '" onFocus=(this.oldValue=this.value) onkeypress="return isNumberKey(event, this, 0)" onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data +'\')" value="' + data + '" class="AssessmentScoreBox" onchange="CurricularScoreOnChange(this, ' + row["sID"] + ', ' + row["nGradeId"] + ', ' + row["RowNumber"] + ',  this.oldValue, \'' + meta.settings.aoColumns[meta.col].data + '\')"  />';
                }
                return data;
            },
            targets: getBehaviorLabelColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if ((!$("#chkGradeAutoReadScore").is(':checked') || response.IsGetReadWriteLocked) && response.Assessments.length == 70) {
                    return data;
                }
                else if ($("#chkGradeAutoReadScore").is(':checked') && row["nStudentStatus"] != 1 && row["nStudentStatus"] != 2 && row["nStudentStatus"] != 3 && row["nStudentStatus"] != 5 && row["nStudentStatus"] != 6 && row["nStudentStatus"] != 7 && response.Assessments.length == 70) {
                    return '<input type="text" oncopy="return false" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '" onFocus=(this.oldValue=this.value) onkeypress="return isNumberKey(event, this,0)" onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data + '\')" value="' + data + '" class="AssessmentScoreBox" onchange="CurricularScoreOnChange(this, ' + row["sID"] + ', ' + row["nGradeId"] + ', ' + row["RowNumber"] + ',  this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  />';

                }
                return data;
            },
            targets: getReadWriteColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (!$("#chkGradeAddSamattana").is(':checked') && !response.IsGetReadWriteLocked && row["nStudentStatus"] != 1 && row["nStudentStatus"] != 2 && row["nStudentStatus"] != 3 && row["nStudentStatus"] != 5 && row["nStudentStatus"] != 6 && row["nStudentStatus"] != 7) {
                    return '<input type="text" oncopy="return false" onpaste="return false" name="' + meta.settings.aoColumns[meta.col].data + '" onFocus=(this.oldValue=this.value) onkeypress="return isNumberKey(event, this,0)" onkeydown="OnKeyDownCheck(event, this,\'' + meta.settings.aoColumns[meta.col].data + '\',' + row["RowNumber"] + ',\'' + response.column[meta.col + 1].data + '\',\'' + response.column[meta.col - 1].data + '\')" value="' + data + '" class="AssessmentScoreBox" onchange="CurricularScoreOnChange(this, ' + row["sID"] + ', ' + row["nGradeId"] + ', ' + row["RowNumber"] + ',  this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')"  />';
                }
                return data;
            },
            targets: getSamattanaColumnIndex
        },
        {
            width: "50px",
            render: function (data, type, row, meta) {
                data = (data == null) ? 0 : data;

                if (response.IsGetSpecialLocked || row["nStudentStatus"] == 1 || row["nStudentStatus"] == 2 || row["nStudentStatus"] == 3 || row["nStudentStatus"] == 5 || row["nStudentStatus"] == 6 || row["nStudentStatus"] == 7) {
                    var getSpecialValue = $.grep(getSpecialDDLValues, function (n) {
                        return n.Value == data;
                    });
                    return (getSpecialValue.length > 0) ? getSpecialValue[0].Text : data;
                }
                else {
                    var otherAssessments = '<select style="width:45px !important" onchange="CurricularScoreOnChange(this, ' + row["sID"] + ', ' + row["nGradeId"] + ', ' + row["RowNumber"] + ', this.oldValue,\'' + meta.settings.aoColumns[meta.col].data + '\')">' + opts + '</select>';

                    var data = "value='" + data + "'";
                    return otherAssessments.replace(data, data + ' selected ');
                }
            },
            targets: getSpecialColumnIndex
        },
        {
           render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (data != '' && data != 'undefined' && data % 1 != 0) {
                    data = parseFloat(data).toFixed(2);
                } 
                return data;
            },

            targets: beforeAfterMidTermTotalScoreColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (data != '' && data != 'undefined' && data % 1 != 0) {
                    data = parseFloat(data).toFixed(2);
                }
                return data;
            },

            targets: scoreMidTermColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (data != '' && data != 'undefined' && data % 1 != 0) {
                    data = parseFloat(data).toFixed(2);
                }
                return data;
            },

            targets: scoreFinalTermColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (data != '' && data != 'undefined' && data % 1 != 0) {
                    data = parseFloat(data).toFixed(2);
                }
                return data;
            },

            targets: TotalScoreColumnIndex
        },
        {
            render: function (data, type, row, meta) {
                data = (data == null || isNaN(data)) ? '' : data;
                if (data != '' && data != 'undefined' && data % 1 != 0) {
                    data = parseFloat(data).toFixed(2);
                }

                var decimalValue = ($("#chkGradeDicimal").is(':checked')) ? 2 : 0;
                data = parseFloat(data).toFixed(decimalValue);

                //console.log(data);
                data = (data == "NaN") ? '' : data;
                return data;
            },

            targets: Term1AndTerm2ScoreTotalColumnIndex
        }
    ];

    return columnDef;
}

//Get the Column Index to update the value on change
function GetColumnIndex(response) {
    $.each(response.column, function (index, obj) {

        switch (obj.data) {
            case "BeforeAfterMidTermTotalScore":
                beforeAfterMidTermTotalScoreColumnIndex = index;
                break;
            case "ScoreMidTerm":
                scoreMidTermColumnIndex = index;
                break;
            case "ScoreFinalTerm":
                scoreFinalTermColumnIndex = index;
                break;
            case "TotalScore":
                TotalScoreColumnIndex = index;
                break;
            case "TotalFor100Percentage":
                TotalFor100PercentageColumnIndex = index;
                break;
            case "Term2TotalFor100Percentage":
                Term2TotalFor100PercentageColumnIndex = index;
                break;
            case "TotalFor50Percentage":
                TotalFor50PercentageColumnIndex = index;
                break;
            case "Term2TotalFor50Percentage":
                Term2TotalFor50PercentageColumnIndex = index;
                break;
            case "Term1AndTerm2ScoreTotal":
                Term1AndTerm2ScoreTotalColumnIndex = index;
                break;
            case "Term1AndTerm2GradeLabel":
                Term1AndTerm2GradeLabelColumnIndex = index;
                break;
            case "GetGradeLabel":
                getGradeLabelColumnIndex = index;
                break;
            case "GetBehaviorLabel":
                getBehaviorLabelColumnIndex = index;
                break;
            case "GetReadWrite":
                getReadWriteColumnIndex = index;
                break;
            case "GetSamattana":
                getSamattanaColumnIndex = index;
                break;
            case "GetSpecial":
                getSpecialColumnIndex = index;
                break;
            case "GetBehaviorTotal":
                getBehaviorTotalColumnIndex = index;
                break;
            case "GetReadWriteTotal":
                getReadWriteTotalColumnIndex = index;
                break;
            case "GetSamattanaTotal":
                getSamattanaTotalColumnIndex = index;
                break;
            case "sPlaneID":
                sPlaneIDColumnIndex = index;
                break;
            case "nStudentStatus":
                nStudentStatusColumnIndex = index;
                break;
            case "ScoreBeforeMidTerm":
                scoreBeforeMidTermColumnIndex = index;
                break;
            case "ScoreAfterMidTerm":
                scoreAfterMidTermColumnIndex = index;
                break;
                
            default:
        }
    });
}

function GetDefaultAssessmentColumnList() {
    var defaultassessmentTypeColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == "Before Mid Term";
    });
    $.each(defaultassessmentTypeColumns, function (index, Value) {
        visbleColumnList = Value.Columns.slice(0, 10);
    });
}

function GetAssessmentTypeName(columnIndex) {
    var assessmentTypeName = 0;
    $.each(toggleGroupColumns, function (index, Value) {
        $.each(Value.Columns, function (colIndex, colValue) {
            if (columnIndex == colValue) {
                assessmentTypeName = Value.AssessmentTypeName
            }
        });
    });
    return assessmentTypeName;
}

function AssessmentNameOnChange() {
    $("#AssessmentId").focus();
    $("span[name=" + $("#AssessmentId").val() + "]").html($("#AssessmentName").val());
    var assessmentUpdateRequest = {
        AssessmentId: $("#AssessmentId").val(), AssessmentName: $("span[name=" + $("#AssessmentId").val() + "]").html(),
        IsNewAssessmentCreated: IsNewAssessmentCreated,
        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear,
    };
    var params = $.extend({}, doAjax_params_default);

   
    params['url'] = "../../api/AssessmentScore/UpdateAssessmentName";
    params['data'] = JSON.stringify(assessmentUpdateRequest);
    params['requestType'] = "POST";
    doAjax(params);

    GetCurrentDateTime("");

    CreateLog($("#AssessmentNameOldValue").val(), $("span[name=" + $("#AssessmentId").val() + "]").html(), "AssessmentNameOnChange", "");
}

function AssessmentDefaultValueOnChange() {
    //$("#AssessmentId").focus();
    var assessmentId = $("#AssessmentIdDefaultValue").val();

    var assessmentTypeName = $("#assessmentTypeName").val();

    var assessmentMaxValue = $("#Max" + assessmentId).val();

    var newValue = $("#AssessmentDefaultValue").val();
    if (assessmentMaxValue == "") {
        OpenErrorInfoDialog(languageContent[0].InValidMaxScore);
    } else if (assessmentMaxValue < newValue) {
        OpenErrorInfoDialog(languageContent[0].InValidMaxScore);
    }
    else if (assessmentTypeName == "Characteristics" && newValue > 3) {
        OpenErrorInfoDialog(languageContent[0].InValidBehaviourMaxScore);
    }
    else if (assessmentTypeName == "ReadWrite" && newValue > 3) {
        OpenErrorInfoDialog(languageContent[0].InValidReadWriteMaxScore);
    }
    else if (assessmentTypeName == "Samattana" && newValue > 3) {
        OpenErrorInfoDialog(languageContent[0].InValidSamattanaMaxScore);
    }
    else {

        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>คะแนนจะถูกปรับ แน่ใจไหมว่าต้องการเปลี่ยนการตั้งค่า</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> ใช่',
                    action: function () {
                        var assessmentControls = $("input[name=" + assessmentId + "]");

                        $.each(assessmentControls, function (index, Value) {
                            //console.log(index);
                            //console.log(Value);
                            $(Value).val(newValue);
                            $(Value).trigger("change");
                        });
                    }
                }
            },
        });
        

        
    }
}

function getColumnNumberByName(name) {
    return table.column(name).index();
}

function AssessmentMaxScoreOnChange(controlId, assessmentId, columnIndex, assessmentTypeName, nGradeId, oldValue) {
    $.fn.ajax = AssessmentMaxScoreOnChangeFn(controlId, assessmentId, columnIndex, assessmentTypeName, nGradeId, oldValue);
}

function AssessmentMaxScoreOnChangeFn(controlId, assessmentId, columnIndex, assessmentTypeName, nGradeId, oldValue) {
    
    $("body").mLoading();
    //var ddlScorePercentage = $("#ddlScorePercentage").val();
    var ddlBeforeMidTermPercentage = $("#ddlBeforeMidTermPercentage").val();
    var ddlAfterMidTermPercentage = $("#ddlAfterMidTermPercentage").val();
    var ddlMidTermPercentage = $("#ddlMidTermPercentage").val();
    var ddlFinalTermPercentage = $("#ddlFinalTermPercentage").val();
    var FRatioQuizPass = $("#ddlPassingCutOffPercentage").val();
    if (((assessmentTypeName == "Samattana") || (assessmentTypeName == "ReadWrite") || (assessmentTypeName == "Characteristics") || (assessmentTypeName == "Before Mid Term" && ddlBeforeMidTermPercentage != "0" && ddlBeforeMidTermPercentage != "-1") || (assessmentTypeName == "After Mid Term" && ddlAfterMidTermPercentage != "0" && ddlAfterMidTermPercentage != "-1") || (assessmentTypeName == "Mid Term" && ddlMidTermPercentage != "0" && ddlMidTermPercentage != "-1") || (assessmentTypeName == "Final" && ddlFinalTermPercentage != "0" && ddlFinalTermPercentage != "-1"))) {
        if (!isPageError || (isPageError && errorControlId == controlId)) {

            var newMaxMark = ($(controlId).val() == "") ? 0 : parseFloat($(controlId).val());

            isPageError = false;
            var isValidMark = true;

            if (assessmentTypeName == "Characteristics" || (assessmentTypeName == "ReadWrite") || (assessmentTypeName == "Samattana")) {
                if (newMaxMark != 0 && $(controlId).val() != "" && $(controlId).val().length > 1) {
                    newMaxMark = parseFloat(trimNumber($(controlId).val()));
                    $(controlId).val(newMaxMark);
                }

                if (newMaxMark > 3)
                    isValidMark = false;
            }
            
            if (isValidMark) {

                if ($(controlId).val() == "") {  
                    console.log("Assessment Max Score Empty");
                    //Validation
                    table.column(columnIndex).data().each(function (value, index) {
                        if (newMaxMark < value)
                            isValidMark = false;
                    });
                }
                else { //This is for avoid cursor slow on changing assessment Max Score
                   
                    //Validate Assessment Max Score - Max score should not be more than the student score
                    var studentMaxScorelist = $.grep(AssessmentMaxScoreValidationList, function (n) {
                        return n.AssessmentId == assessmentId && newMaxMark < n.StudentMaxScore && n.StudentMaxScore != "";
                    });

                   
                    if (studentMaxScorelist.length > 0) {
                        isValidMark = false;
                        //table.column(columnIndex).data().each(function (value, index) {
                        //    if (newMaxMark < value)
                        //        isValidMark = false;
                        //});
                    }
                }

                if (newMaxMark > 100)
                    isValidMark = false;
            }

            if (isValidMark) {
                $(controlId).removeClass("Error");
                newMaxMark = $(controlId).val();
                //Updating in the object for Calculating the Total
                UpdateMaxScoreInJsonObject(assessmentId, newMaxMark);
               /* setTimeout(function () { UpdateAssessmentMaxScoreInDb(assessmentId, assessmentTypeName, nGradeId, newMaxMark);  }, 1000);*/
                UpdateAssessmentMaxScoreInDb(assessmentId, assessmentTypeName, nGradeId, newMaxMark);
                //UpdateAssessmentMaxScoreInDb(assessmentId, assessmentTypeName, nGradeId, newMaxMark);

                HighLightStudentsMarkIfBelowCutOff(columnIndex, newMaxMark, assessmentId, true);
                GetCurrentDateTime("");

                CreateLog(oldValue, newMaxMark, "AssessmentMaxScoreOnChange", "");

             
            }
            else {
                $("body").mLoading("hide");
                errorControlId = controlId;
                isPageError = true;
                $(controlId).addClass("Error");
                if (assessmentTypeName == "Characteristics" && newMaxMark > 3) {
                    OpenErrorInfoDialog(languageContent[0].InValidBehaviourMaxScore);
                }
                else if (assessmentTypeName == "ReadWrite" && newMaxMark > 3) {
                    OpenErrorInfoDialog(languageContent[0].InValidReadWriteMaxScore);
                }
                else if (assessmentTypeName == "Samattana" && newMaxMark > 3) {
                    OpenErrorInfoDialog(languageContent[0].InValidSamattanaMaxScore);
                }
                else {
                    OpenErrorInfoDialog(languageContent[0].InValidMaxScore);
                }
                
                $(controlId).focus();
            }
        }
        else {
            $("body").mLoading("hide");
            $(controlId).val(oldValue);
            OpenErrorInfoDialog(languageContent[0].ErrorCorrectionRequest);
            $(controlId).focus();
        }
    }
    else {
        $("body").mLoading("hide");
        $(controlId).val("");
        OpenErrorInfoDialog(languageContent[0].ScoreProportionValidationMessage);
    }
}

function trimNumber(s) {
    while (s.substr(0, 1) == '0' && s.length > 1) { s = s.substr(1, 9999); }
    return s;
}

function AssessmentScoreOnChange(controlId, sID, sPlaneID, nGradeId, assessmentId, rowNumber, selectedColIndex, assessmentTypeName, oldValue) {
    if (!isPageError || (isPageError && errorControlId == controlId)) {
        var newScore = ($(controlId).val() == "" || $(controlId).val() == 'undefined') ? 0 : parseFloat($(controlId).val());


        isPageError = false;
        var isValidMark = true;

        var assessmentMaxScoreValue = ($("#Max" + assessmentId).val() == "") ? 0 : parseFloat($("#Max" + assessmentId).val());

        if (newScore > assessmentMaxScoreValue || isNaN(newScore))
            isValidMark = false;

        if (isValidMark && (assessmentTypeName == "Characteristics" || assessmentTypeName == "ReadWrite" || assessmentTypeName == "Samattana") && newScore != 0) {
            if ($(controlId).val() != "" || $(controlId).val() != 'undefined' && $(controlId).val().length > 1 && $(controlId).val().substr(0, 1) == '0') {
                newScore = parseFloat(trimNumber($(controlId).val()));
                $(controlId).val(newScore);
            }
        }

        if (isValidMark) {

            var studentMaxScorelist = $.grep(AssessmentMaxScoreValidationList, function (n) {
                return n.AssessmentId == assessmentId && n.SId == sID
            });

            if (studentMaxScorelist.length == 0) {
                AssessmentMaxScoreValidationList.push({ AssessmentId: assessmentId, StudentMaxScore: newScore, SId: sID });
            }
            else if ($(controlId).val() == "" || (studentMaxScorelist[0].StudentMaxScore < newScore && studentMaxScorelist[0].StudentMaxScore != "")) {
                studentMaxScorelist[0].StudentMaxScore = newScore;
            }
           
            
            IsGradeDetailAvailable = true;
            $(controlId).removeClass("Error");
            newScore = $(controlId).val();

            table.cell(rowNumber - 1, selectedColIndex).data(newScore).draw();

            if (newScore != "" && $("#Max" + assessmentId).val() != "" && $("#ddlPassingCutOffPercentage").val() > -1) {
                var cutOfMark = (parseFloat($("#Max" + assessmentId).val()) * parseFloat($("#ddlPassingCutOffPercentage").val())) / 100;
               
                if (newScore < cutOfMark) {
                    $(controlId).addClass("alert-warning");
                }
                else {
                    $(controlId).removeClass("alert-warning");
                }
            }
            UpdateAssessmentScoreInDb(newScore, sID, sPlaneID, nGradeId, assessmentId, rowNumber, selectedColIndex, assessmentTypeName);
            GetCurrentDateTime("");
            CreateLog(oldValue, newScore, "AssessmentScoreOnChange", table.cell(rowNumber - 1, 3).data());

        }
        else {

            errorControlId = controlId;
            isPageError = true;
            $(controlId).removeClass("alert-warning");
            $(controlId).addClass("Error");
            OpenErrorInfoDialog(languageContent[0].InValidScore);
            $(controlId).focus();
        }
    }
    else {

        $(controlId).val(oldValue);
        OpenErrorInfoDialog(languageContent[0].ErrorCorrectionRequest);
        $(controlId).focus();
    }

}

function CurricularScoreOnChange(controlId, sID, nGradeId, rowNumber, oldValue, curricularName) {

    if (!isPageError || (isPageError && errorControlId == controlId)) {
        var newValue = $(controlId).val();

        isPageError = false;
        var isValidMark = true;

        if (newValue != "" && curricularName != 'GetSpecial' && newValue.length > 1) {
            newValue = trimNumber(newValue);
            $(controlId).val(newValue);
        }

        if (newValue > 3 && curricularName != 'GetSpecial')
            isValidMark = false;

        if (isValidMark) {
            UpdateCurricularValueInDb(newValue, sID, nGradeId, rowNumber, curricularName)
            GetCurrentDateTime("");
            CreateLog(oldValue, newValue, curricularName, table.cell(rowNumber - 1, 3).data());
        }
        else {

            errorControlId = controlId;
            isPageError = true;
            $(controlId).addClass("Error");
            OpenErrorInfoDialog(languageContent[0].InValidBehaviour);
        }
    }
    else {

        $(controlId).val(oldValue);
        OpenErrorInfoDialog(languageContent[0].ErrorCorrectionRequest);
        $(controlId).focus();
    }
}

function UpdateMaxScoreInJsonObject(assessmentId, newMaxMark) {

    jQuery.each(response.Assessments, function (i, val) {
        if (val.AssessmentId == assessmentId) {
            //update logic
            val.AssessmentMaxScore = newMaxMark;
        }
    });

}

function UpdateAssessmentMaxScoreInDb(assessmentId, assessmentTypeName, nGradeId, newMaxMark) {
    var beforeAfterMidTermMaxScoreTotal = $("#hdnBeforeAfterMidTermMaxScoreTotal").val();
    var beforeMidTermMaxScoreTotal = $("#hdnBeforeMidTermMaxScoreTotal").val();
    var afterMidTermMaxScoreTotal = $("#hdnAfterMidTermMaxScoreTotal").val();

    var midTermMaxScoreTotal = $("#hdnMidTermMaxScoreTotal").val();
    var finalTermMaxScoreTotal = $("#hdnFinalTermMaxScoreTotal").val();
    var behaviourMaxScoreTotal = GetSumOfMaxScore("Characteristics");
    var readWriteMaxScoreTotal = GetSumOfMaxScore("ReadWrite");
    var samattanaMaxScoreTotal = GetSumOfMaxScore("Samattana");
    var beforeMidTermMaxScoreTotal, afterMidTermMaxScoreTotal;

    if (assessmentTypeName == "Before Mid Term" || assessmentTypeName == "After Mid Term") {
        beforeMidTermMaxScoreTotal = GetSumOfMaxScore("Before Mid Term");
        afterMidTermMaxScoreTotal = GetSumOfMaxScore("After Mid Term");
        beforeAfterMidTermMaxScoreTotal = Number(beforeMidTermMaxScoreTotal) + Number(afterMidTermMaxScoreTotal);
        $("#hdnBeforeAfterMidTermMaxScoreTotal").val(beforeAfterMidTermMaxScoreTotal);

        $("#hdnBeforeMidTermMaxScoreTotal").val(beforeMidTermMaxScoreTotal);
        $("#hdnAfterMidTermMaxScoreTotal").val(afterMidTermMaxScoreTotal);
        $(".BeforeAfterMidTermMaxScoreBox").html(beforeAfterMidTermMaxScoreTotal);
    }
    else if (assessmentTypeName == "Mid Term") {
        midTermMaxScoreTotal = GetSumOfMaxScore(assessmentTypeName);

        $("#hdnMidTermMaxScoreTotal").val(midTermMaxScoreTotal);
        $(".MidTermMaxScoreBox").html(midTermMaxScoreTotal);
    }
    else if (assessmentTypeName == "Final") {
        finalTermMaxScoreTotal = GetSumOfMaxScore(assessmentTypeName);
        $("#hdnFinalTermMaxScoreTotal").val(finalTermMaxScoreTotal);
        $(".FinalTermMaxScoreBox").html(finalTermMaxScoreTotal);
    }
   
    beforeAfterMidTermMaxScoreTotal = (beforeAfterMidTermMaxScoreTotal == "") ? 0 : Number(beforeAfterMidTermMaxScoreTotal);
    midTermMaxScoreTotal = (midTermMaxScoreTotal == "") ? 0 : Number(midTermMaxScoreTotal);
    finalTermMaxScoreTotal = (finalTermMaxScoreTotal == "") ? 0 : Number(finalTermMaxScoreTotal);

    var maxTotalScore = beforeAfterMidTermMaxScoreTotal + midTermMaxScoreTotal + finalTermMaxScoreTotal;


    if (maxTotalScore != '' && maxTotalScore != 'undefined' && maxTotalScore % 1 != 0) {
        maxTotalScore = maxTotalScore.toFixed(2);
    } 

    $(".AssessmentMaxTotalScore").html(maxTotalScore);

    //TO DO - Update new value in the response.Assessments - Otherwise after max changed language change will reflect old value

    var assessmentUpdateRequest = {
        AssessmentId: assessmentId, AssessmentMaxScore: newMaxMark,
        MaxMidTerm: midTermMaxScoreTotal, MaxFinalTerm: finalTermMaxScoreTotal,
        MaxGradeTotal: beforeAfterMidTermMaxScoreTotal,
        MaxBeforeMidTermTotal: beforeMidTermMaxScoreTotal, MaxAfterMidTermTotal: afterMidTermMaxScoreTotal,
        MaxBehaviorTotal: behaviourMaxScoreTotal, NGradeId: nGradeId, MaxReadWriteScoreTotal: readWriteMaxScoreTotal,
        MaxPerformanceScoreTotal: samattanaMaxScoreTotal,
        IsNewAssessmentCreated: IsNewAssessmentCreated,
        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
    };

    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/AssessmentScore/UpdateAssessmentMaxScore";
    params['data'] = JSON.stringify(assessmentUpdateRequest);
    params['requestType'] = "POST";
    params['successCallbackFunction'] = UpdateAssessmentMaxScoreCallBack;
    doAjax(params);

    //activeAjaxConnections++;
    //setTimeout(function () { doAjaxSequencial(params); }, 4000);

    
}

function UpdateAssessmentMaxScoreCallBack(response) {
    if (response == "Success") {
        
        UpdateStudentsGradeValueInDataTableAndDBOnMaxScoreChanges();
    } else if (response == "InValidScore") {
        //Throw Error Message
        OpenErrorInfoDialog("พบข้อมูลที่กรอกไม่ถูกต้อง ทำให้ไม่สามารถใช้งานต่อได้ กรุณาติดต่อบริษัทเพื่อทำการแก้ไข");
    }
}

function EnableDisableBehaviourTab(onChangeEvent) {
    if ($("#chkGradeAddBehavior").is(':checked')) {
        $('#TabBehavior').show();
        if (onChangeEvent) {
            //$('#TabBehavior a').trigger('click');
        }
        $("#chkGradeAutoBehaviorScore").prop("checked", false);
    }
    else {
        $('#TabBehavior').hide();
        if (onChangeEvent) {
            $('#TabBeforeMidTerm a').trigger('click');
            $("#chkGradeAutoBehaviorScore").prop("checked", true);
        }
        //$("#GetBehaviorLabelDeleteIcon").show();
        //ToggleTabColumn(1);
    }
}

function EnableDisableSamattanaTab(onChangeEvent) {
   
    if ($("#chkGradeAddSamattana").is(':checked') && response.Assessments.length > 70) {
        $('#TabSamattana').show();
        if (onChangeEvent) {
            //$('#TabSamattana a').trigger('click');
        }
    }
    else {
        $('#TabSamattana').hide();
        if (onChangeEvent) {
            $('#TabBeforeMidTerm a').trigger('click');
        }
    }
}

function EnableDisableAfterMidTermTab(onChangeEvent) {
    if ($("#chkGradeAddCheewat").is(':checked')) {
        $('#TabAfterMidTerm').show();
        if (onChangeEvent) {
            //$('#TabAfterMidTerm a').trigger('click');
        }

    }
    else {
        $('#TabAfterMidTerm').hide();
        if (onChangeEvent) {
            $('#TabBeforeMidTerm a').trigger('click');
        }
        //ToggleTabColumn(1);
    }
}

function EnableDisableReadWriteTab(onChangeEvent)
{
    if ($("#chkGradeAutoReadScore").is(':checked') && response.Assessments.length > 70) {
        $('#TabReadWrite').show();
        if (onChangeEvent) {
            //$('#TabBeforeMidTerm a').trigger('click');
        }
    }
    else {
        $('#TabReadWrite').hide();
        if (onChangeEvent) {
            $('#TabBeforeMidTerm a').trigger('click');
        }
    }
}


function EnableDisableTotalFor50PercentageColumn(onChangeEvent) {
    if ($("#chkShowFullScore100Percentage").is(':checked')) {
        table.column(TotalFor100PercentageColumnIndex).visible(true);
        table.column(Term2TotalFor100PercentageColumnIndex).visible(true);
        table.column(TotalFor50PercentageColumnIndex).visible(false);
        table.column(Term2TotalFor50PercentageColumnIndex).visible(false);
    }
    else {
        table.column(TotalFor100PercentageColumnIndex).visible(false);
        table.column(Term2TotalFor100PercentageColumnIndex).visible(false);
        table.column(TotalFor50PercentageColumnIndex).visible(true);
        table.column(Term2TotalFor50PercentageColumnIndex).visible(true);
    }
    //if (onChangeEvent) {
    //    $('#TabBeforeMidTerm a').trigger('click');
    //}
}


function EnableDisableBehaviourAndReadWriteColumn(onChangeEvent) {
    if ($("#chkGradeCloseBehaviorReadWrite").is(':checked')) {
        table.column(getBehaviorLabelColumnIndex).visible(false);
        table.column(getReadWriteColumnIndex).visible(false);
        $('#TabBehavior').hide();
        if (onChangeEvent) {
            $('#TabBeforeMidTerm a').trigger('click');
        }
    }
    else {
        table.column(getBehaviorLabelColumnIndex).visible(true);
        table.column(getReadWriteColumnIndex).visible(true);
        EnableDisableBehaviourTab(false);
    }
}

function EnableDisableGradeColumn() {
    if (LevelName != null && LevelName != "" && LevelName != "ปวช." && LevelName != "ปวส." && LevelName != "มัธยมศึกษา") {
        if ($("#chkCloseGradeTab").is(':checked')) {
            table.column(Term1AndTerm2GradeLabelColumnIndex).visible(false);
        }
        else {
            table.column(Term1AndTerm2GradeLabelColumnIndex).visible(true);
        }
    }
    else {
        if ($("#chkCloseGradeTab").is(':checked')) {
            table.column(getGradeLabelColumnIndex).visible(false);
        }
        else {
            table.column(getGradeLabelColumnIndex).visible(true);
        }
    }
}

function EnableDisablePerformanceColumn() {
    if ($("#chkGradeCloseSamattana").is(':checked')) {
        table.columns(getSamattanaColumnIndex).visible(false);
    }
    else {
        table.columns(getSamattanaColumnIndex).visible(true);
    }
}

function UpdateGradePercentageInfo(control, controlName, OldValue) {
    var fRatioQuiz = GetFRatioQuiz();
    var midTermPercentage = ($("#ddlMidTermPercentage").val() != "-1") ? Number($("#ddlMidTermPercentage").val()) : 0;
    var finalTermPercentage = ($("#ddlFinalTermPercentage").val() != "-1") ? Number($("#ddlFinalTermPercentage").val()) : 0;
    var totalPercentage = Number(fRatioQuiz) + midTermPercentage + finalTermPercentage;
    if (totalPercentage > 100) {
        isPageError = true;
        OpenErrorInfoDialog("เปอร์เซ็นต์ไม่ควรเกิน 100");
        //$(control).val(oldValue);
        //OpenErrorInfoDialog(languageContent[0].ErrorCorrectionRequest);
        $(control).focus();
    }
    else {
        isPageError = false;
        UpdateGradeInfo(control, controlName, OldValue);
    }

}

function UpdateGradeBehaviorInfo(control, controlName, OldValue) {
    if ($("#chkGradeAddBehavior").is(':checked')) {
        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>คะแนนจะถูกปรับ แน่ใจไหมว่าต้องการเปลี่ยนการตั้งค่า</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> ใช่',
                    action: function () {
                        UpdateGradeInfo(control, controlName, OldValue);
                    }
                }
            },
        });
    }
    else {
        UpdateGradeInfo(control, controlName, OldValue);
    }
}

function UpdateGradeSamattanaInfo(control, controlName, OldValue) {
    if ($("#chkGradeAddSamattana").is(':checked') && response.Assessments.length > 70) {
        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>คะแนนจะถูกปรับ แน่ใจไหมว่าต้องการเปลี่ยนการตั้งค่า</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> ใช่',
                    action: function () {
                        UpdateGradeInfo(control, controlName, OldValue);
                    }
                }
            },
        });
    }
    else if (response.Assessments.length == 70) {
        //Show Alert TO-Do
        if ($("#chkGradeAddSamattana").is(':checked')) {
            $("#chkGradeAddSamattana").prop("checked", false);
        }
        else {
            $("#chkGradeAddSamattana").prop("checked", true);
        }

        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>ไม่ได้รับอนุญาต</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',

                },
            },
        });
    }
    else {
        UpdateGradeInfo(control, controlName, OldValue);
    }
}

function UpdateGradeInfo(control, controlName, OldValue) {
    $("body").mLoading();
    if (controlName == "chkGradeAddBehavior") {
        EnableDisableBehaviourTab(true);
    }
    else if (controlName == "chkGradeAddCheewat") {
        EnableDisableAfterMidTermTab(false);
    }
    else if (controlName == "chkGradeCloseBehaviorReadWrite") {
        EnableDisableBehaviourAndReadWriteColumn(true);
    }
    else if (controlName == "chkShowFullScore100Percentage" && (LevelName != null && LevelName != "" && LevelName != "ปวช." && LevelName != "ปวส." && LevelName != "มัธยมศึกษา")) {
        EnableDisableTotalFor50PercentageColumn(true);
    }
    else if (controlName == "chkCloseGradeTab") {
        EnableDisableGradeColumn();
    }
    else if (controlName == "chkGradeCloseSamattana") {
        EnableDisablePerformanceColumn(true);
    }
    //else if (controlName == "ddlScorePercentage") {
    //    response.GradeDTO.FRatioQuiz = $("#ddlScorePercentage").val();
    //}
    else if (controlName == "ddlBeforeMidTermPercentage") {
        response.GradeDTO.FRatioBeforeMidTerm = $("#ddlBeforeMidTermPercentage").val();
        response.GradeDTO.FRatioQuiz = GetFRatioQuiz();
    }
    else if (controlName == "ddlAfterMidTermPercentage") {
        response.GradeDTO.FRatioAfterMidTerm = $("#ddlAfterMidTermPercentage").val();
       
        response.GradeDTO.FRatioQuiz = GetFRatioQuiz();
    }
    else if (controlName == "ddlMidTermPercentage") {
        response.GradeDTO.FRatioMidTerm = $("#ddlMidTermPercentage").val();
       
    }
    else if (controlName == "ddlFinalTermPercentage") {
        response.GradeDTO.FRatioLateTerm = $("#ddlFinalTermPercentage").val();
    }
    else if (controlName == "ddlPassingCutOffPercentage") {
        response.GradeDTO.FRatioQuizPass = $("#ddlPassingCutOffPercentage").val();
    }
    else if (controlName == "chkGradeShareData") {
        if ($("#chkGradeShareData").is(':checked')) {
            $("#lblAssessmentSharingTo").html(languageContent[0].lblAssessmentSharingTo);
            //$("#lblSelectedRoomforSharing").html(languageContent[0].lblSelectedRoomforSharing);
            $("#AssessmentSharingToRoomList").show();
            $("#AssessmentSharingSelectedRoomList").show();
            $("body").mLoading();

            GetRoomListForSharingAssessments();
            
        }
        else {
            $("#AssessmentSharingToRoomList").hide();
            $("#AssessmentSharingSelectedRoomList").hide();
        }
    }

    if (controlName == "chkGradeAddSamattana" && response.Assessments.length > 70) {
        EnableDisableSamattanaTab(true);
    }
    else if (response.Assessments.length > 70 && (controlName == "chkGradeAutoReadScore")) {
        EnableDisableReadWriteTab(true);
    }
   
    //if (response.Assessments.length == 70 && (controlName == "chkGradeAddSamattana")) {
    //    //Show Alert TO-Do
    //    if ($("#chkGradeAddSamattana").is(':checked')) {
    //        $("#chkGradeAddSamattana").prop("checked", false);
    //    }
    //    else {
    //        $("#chkGradeAddSamattana").prop("checked", true);
    //    }

    //    $.confirm({
    //        title: '<h2>คำเตือน !</h>',
    //        content: '<h2>ไม่ได้รับอนุญาต</h>',
    //        buttons: {
    //            cancel: {
    //                label: '<i class="fa fa-times"></i> ยกเลิก',

    //            },
    //        },
    //    });
    //}
    //else {
        
    var assessmentUpdateRequest = {
        NGradeId: nGradeId, FRatioQuiz: GetFRatioQuiz(), FRatioBeforeMidTerm: $("#ddlBeforeMidTermPercentage").val(), FRatioAfterMidTerm: $("#ddlAfterMidTermPercentage").val(),
            FRatioMidTerm: $("#ddlMidTermPercentage").val(),
            FRatioLateTerm: $("#ddlFinalTermPercentage").val(),
            FRatioQuizPass: $("#ddlPassingCutOffPercentage").val(),
            GradeDicimal: $("#chkGradeDicimal").is(':checked') ? "1" : "0",
            GradeShowFullScore: $("#chkShowFullScore100Percentage").is(':checked') ? true : false,
            GradeAutoReadScore: $("#chkGradeAutoReadScore").is(':checked') ? "1" : "0",
            GradeCloseBehaviorReadWrite: $("#chkGradeCloseBehaviorReadWrite").is(':checked') ? "1" : "0",
            GradeAddCheewat: $("#chkGradeAddCheewat").is(':checked') ? "1" : "0",
            GradeAddBehavior: $("#chkGradeAddBehavior").is(':checked') ? "1" : "0",
            //GradeAddReadWrite: $("#chkGradeAddReadWrite").is(':checked') ? "1" : "0",
            GradeAddSamattana: $("#chkGradeAddSamattana").is(':checked') ? "1" : "0",
            GradeAutoBehaviorScore: $("#chkGradeAutoBehaviorScore").is(':checked') ? "1" : "0",
            OptionMid: $("#chkOptionMid").is(':checked') ? "1" : "0",
            OptionFinal: $("#chkOptionFinal").is(':checked') ? "1" : "0",
            GradeCloseGrade: $("#chkCloseGradeTab").is(':checked') ? "1" : "0",
            GradeShareData: $("#chkGradeShareData").is(':checked') ? "1" : "0",
            GradeCloseSamattana: $("#chkGradeCloseSamattana").is(':checked') ? "1" : "0",
            IsNewAssessmentCreated: IsNewAssessmentCreated,
            IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear,
        };

        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/UpdateScoreProportion";
        params['data'] = JSON.stringify(assessmentUpdateRequest);
        params['requestType'] = "POST";
        //params['successCallbackFunction'] = `your success callback function`
        doAjax(params);
   
        if (IsGradeDetailAvailable == true) {
            if (controlName != "ddlPassingCutOffPercentage") {
                console.log("UpdateStudentsGradeValueInDataTableAndDB");
              //  setTimeout(function () { UpdateStudentsGradeValueInDataTableAndDB(); }, 1000)
                UpdateStudentsGradeValueInDataTableAndDB();

            }
            else {
                HighLightStudentsMarkIfBelowCutOff(assessmentColumn, 0, 0, false);
            }
        }
        GetCurrentDateTime("");

        CreateLog(OldValue, $(control).val(), controlName, "");

    //}

    $("body").mLoading('hide');
}

function GetFRatioQuiz() {

    var fRatioQuiz = -1;
    if ($("#ddlBeforeMidTermPercentage").val() != "-1" && $("#ddlAfterMidTermPercentage").val() != "-1") {
        fRatioQuiz = Number($("#ddlBeforeMidTermPercentage").val()) + Number($("#ddlAfterMidTermPercentage").val());
    }
    else if ($("#ddlBeforeMidTermPercentage").val() != "-1") {
        fRatioQuiz = Number($("#ddlBeforeMidTermPercentage").val());
    }
    else if ($("#ddlAfterMidTermPercentage").val() != "-1") {
        fRatioQuiz = Number($("#ddlAfterMidTermPercentage").val());
    }
    return fRatioQuiz;
}

function GetRoomListForSharingAssessments() {
    //$("#txtSelectedRoomforSharing").val("");
    var commonRequest = { nTSubLevel: getUrlParameter("idlv"), sPlaneID: getUrlParameter("id"), nTermSubLevel2: getUrlParameter("idlv2"), sTerm: getUrlParameter("term"), NumberYear: getUrlParameter("year"), NTerm: nTerm };

    $.ajax({
        ContentType: "application/json; charset=utf-8",
        url: "../../api/AssessmentScore/GetRoomListForSharingAssessments",
        type: "Post",
        data: commonRequest,
        success: function (roomList) {
            UpdateRoomListForSharingAssessment(roomList);
            roomListForSharingAssessment = roomList;
            $("body").mLoading('hide');
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (errorThrown == "Request Timeout") {
                ShowPageSessionTimeOutAlert();
            }
            else {
                ShowPageError();
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

function HighLightStudentsMarkIfBelowCutOff(columnList, assessmentMaxScore, assessmentId, maxScoreOnChange) {
    //Score Changes and CutOff Percentage Change Update the background color
    table.columns(columnList).data().each(function (value, index) {
        var checkValueNotEmpty = $.grep(value, function (n) {
            return n != "";
        });
        if (checkValueNotEmpty.length > 0) {
            var assessmentControls = $("input[name=" + assessmentId + "]");
            
            var maxScore = assessmentMaxScore;
            if (!maxScoreOnChange) {
                var assessment = $.grep(response.Assessments, function (n) {
                    return n.AssessmentId == response.column[assessmentColumn[index]].data;
                });
                maxScore = (assessment.length > 0) ? assessment[0].AssessmentMaxScore : 0;
                assessmentControls = $("input[name=" + response.column[assessmentColumn[index]].data + "]");
            }

            if (maxScore != "" && $("#ddlPassingCutOffPercentage").val() > -1) {
                var cutOfMark = (parseFloat(maxScore) * parseFloat($("#ddlPassingCutOffPercentage").val())) / 100;
                $.each(checkValueNotEmpty, function (colIndex, colValue) {
                    if (colValue != null && colValue != "" && colValue < cutOfMark) {
                        $(assessmentControls[colIndex]).addClass("alert-warning");
                    }
                    else {
                        $(assessmentControls[colIndex]).removeClass("alert-warning");
                    }
                });
            }
            else {
                $.each(checkValueNotEmpty, function (colIndex, colValue) {
                    $(assessmentControls[colIndex]).removeClass("alert-warning");
                });
            }
        }
    });

    HighLightAllTotalScores();
}

function HighLightAllTotalScores() {
    var beforeAfterMidTermMaxScoreTotal = $(".BeforeAfterMidTermMaxScoreTotal").html();
    var midTermMaxScoreTotal = $(".MidTermMaxScoreTotal").html();
    var finalTermMaxScoreTotal = $(".FinalTermMaxScoreTotal").html();
    var maxTotalScore = $(".AssessmentMaxTotalScore").html();

    table.columns([beforeAfterMidTermTotalScoreColumnIndex]).data().each(function (value, index) {
        //console.log(value);
        $.each(value, function (colIndex, colValue) {
            if (colValue != null && colValue != "") {
               
                HighLightTotalScore(colValue, "BeforeAfterMidTermTotalScore", beforeAfterMidTermMaxScoreTotal, colIndex + 1);
            }
        });
    });

    table.columns([scoreMidTermColumnIndex]).data().each(function (value, index) {
        $.each(value, function (colIndex, colValue) {
            if (colValue != null && colValue != "") {
              
                HighLightTotalScore(colValue, "ScoreMidTerm", midTermMaxScoreTotal, colIndex + 1);

            }
        });
    });
    table.columns([scoreFinalTermColumnIndex]).data().each(function (value, index) {
        $.each(value, function (colIndex, colValue) {
            if (colValue != null && colValue != "") {
                //console.log(colIndex);
                //console.log(colValue);
                HighLightTotalScore(colValue, "ScoreFinalTerm", finalTermMaxScoreTotal, colIndex + 1);

            }
        });
    });
    table.columns([TotalScoreColumnIndex]).data().each(function (value, index) {
        $.each(value, function (colIndex, colValue) {
            if (colValue != null && colValue != "") {
              
                HighLightTotalScore(colValue, "TotalScore", maxTotalScore, colIndex + 1);

            }
        });
    });
}

//Update the Details in TGradeDetails Table (Student Grade Info)
function UpdateStudentsGradeValueInDataTableAndDBOnMaxScoreChanges() {
    var studentAssessmentScoreUpdateRequest = [];
    var rowCount = 0;
    var rowIndex = 0;
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        rowCount = rowCount + 1;
        var data = this.data();
        
        if ((data.BeforeAfterMidTermTotalScore != 'undefined' && data.BeforeAfterMidTermTotalScore != null && data.BeforeAfterMidTermTotalScore != "" && data.BeforeAfterMidTermTotalScore != "0") ||
            (data.ScoreAfterMidTerm != 'undefined' && data.ScoreAfterMidTerm != null && data.ScoreAfterMidTerm != "null" && data.ScoreAfterMidTerm != "" && data.ScoreAfterMidTerm != "0") ||
            (data.ScoreMidTerm != 'undefined' && data.ScoreMidTerm != null && data.ScoreMidTerm != "" && data.ScoreMidTerm != "0") ||
            (data.ScoreFinalTerm != 'undefined' && data.ScoreFinalTerm != "" && data.ScoreFinalTerm != null && data.ScoreFinalTerm != "0")) {
           
            var calculatedFields = DynamicCalculation((rowIndex + 1), data.BeforeAfterMidTermTotalScore, data.ScoreBeforeMidTerm, data.ScoreAfterMidTerm, data.ScoreMidTerm, data.ScoreFinalTerm);
            //var readingsore = calculatedFields.readingsore;
            var totalGrade = calculatedFields.totalGrade;
            var totalScoreFor100Percent = calculatedFields.totalScoreFor100Percent;
            var quizScoreFor100Percent = calculatedFields.quizScoreFor100Percent;
            var midTermScoreFor100Percent = calculatedFields.midTermScoreFor100Percent;
            var finalTermScorefor100Percent = calculatedFields.finalTermScorefor100Percent;
            var getBeforeQuiz100 = calculatedFields.getBeforeQuiz100;
            var getAfterQuiz100 = calculatedFields.getAfterQuiz100;

            var getReadWrite = data.GetReadWrite;
            var getSamattana = data.GetSamattana;
            var getBehaviorLabel = data.GetBehaviorLabel;
            getReadWrite = ReadWriteScoreAutoCalculation((rowIndex + 1), getReadWrite);
            if (response.Assessments.length > 70) {
                if ($("#chkGradeAutoReadScore").is(':checked')) {
                    getReadWrite = CalculateBehaviourScore('', 999, "ReadWrite", (rowIndex + 1))
                    table.cell(rowIndex, getReadWriteColumnIndex).data(getReadWrite).draw();
                }

                if ($("#chkGradeAddSamattana").is(':checked')) {
                    getSamattana = CalculateBehaviourScore('', 999, "Samattana", (rowIndex + 1))
                    table.cell(rowIndex, getSamattanaColumnIndex).data(getSamattana).draw();
                }

                if ($("#chkGradeAddBehavior").is(':checked')) {
                    getBehaviorLabel = CalculateBehaviourScore('', 999, "Characteristics", (rowIndex + 1))
                    table.cell(rowIndex, getBehaviorLabelColumnIndex).data(getBehaviorLabel).draw();
                }
            }


            studentAssessmentScoreUpdateRequest.push({
                sID: data.sID, sPlaneID: data.sPlaneID, nGradeId: data.nGradeId,
                GetReadWrite: getReadWrite, scoreBeforeAfterMidTerm: data.BeforeAfterMidTermTotalScore,
                ScoreMidTerm: data.ScoreMidTerm, ScoreFinalTerm: data.ScoreFinalTerm,
                GetScore100: Number.isNaN(totalScoreFor100Percent) ? 0 : totalScoreFor100Percent,
                GetGradeLabel: totalGrade, GetBeforeQuiz100: getBeforeQuiz100, GetAfterQuiz100: getAfterQuiz100,
                GetQuiz100: quizScoreFor100Percent, GetMid100: midTermScoreFor100Percent, GetFinal100: finalTermScorefor100Percent,
                GetBehaviorTotal: data.GetBehaviorTotal, getReadWriteTotal: data.GetReadWriteTotal, getSamattanaTotal: data.GetSamattanaTotal,
                GetBehaviorLabel: getBehaviorLabel,
                GetSamattana: getSamattana, SubmitPeriod: response.GradeDTO.PeriodNow,
                ScoreBeforeMidTerm: data.ScoreBeforeMidTerm,
                ScoreAfterMidTerm: data.ScoreAfterMidTerm, MethodName: "UpdateStudentsGradeValueInDataTableAndDB",
                IsNewAssessmentCreated: IsNewAssessmentCreated,
                IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
            });

            readingsore = table.cell(rowIndex, getSamattanaColumnIndex).invalidate().render();
            readingsore = table.cell(rowIndex, getBehaviorLabelColumnIndex).invalidate().render();
            readingsore = table.cell(rowIndex, getReadWriteColumnIndex).invalidate().render();
        }
        rowIndex = rowIndex + 1;
    });

    if (rowCount > 0) {
        UpdateStudentAssessmentScoreOnMaxScoreChanges(studentAssessmentScoreUpdateRequest);
    }
    else {
        table = $(tableName).DataTable();
        UpdateStudentsGradeValueInDataTableAndDBOnMaxScoreChanges();
    }

}

//Update the Details in TGradeDetails Table (Student Grade Info)
function UpdateStudentsGradeValueInDataTableAndDB() {
    var studentAssessmentScoreUpdateRequest = [];
    var rowCount = 0;
    var rowIndex = 0;
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        rowCount = rowCount + 1;
        var data = this.data();
       
        if ((data.BeforeAfterMidTermTotalScore != 'undefined' && data.BeforeAfterMidTermTotalScore != null && data.BeforeAfterMidTermTotalScore != "" && data.BeforeAfterMidTermTotalScore != "0") ||
            (data.ScoreAfterMidTerm != 'undefined' && data.ScoreAfterMidTerm != null && data.ScoreAfterMidTerm != "null" && data.ScoreAfterMidTerm != "" && data.ScoreAfterMidTerm != "0") ||
            (data.ScoreMidTerm != 'undefined' && data.ScoreMidTerm != null && data.ScoreMidTerm != "" && data.ScoreMidTerm != "0") ||
            (data.ScoreFinalTerm != 'undefined' && data.ScoreFinalTerm != "" && data.ScoreFinalTerm != null && data.ScoreFinalTerm != "0")) {

            var calculatedFields = DynamicCalculation((rowIndex + 1), data.BeforeAfterMidTermTotalScore, data.ScoreBeforeMidTerm, data.ScoreAfterMidTerm, data.ScoreMidTerm, data.ScoreFinalTerm);
            //var readingsore = calculatedFields.readingsore;
            var totalGrade = calculatedFields.totalGrade;
            var totalScoreFor100Percent = calculatedFields.totalScoreFor100Percent;
            var quizScoreFor100Percent = calculatedFields.quizScoreFor100Percent;
            var midTermScoreFor100Percent = calculatedFields.midTermScoreFor100Percent;
            var finalTermScorefor100Percent = calculatedFields.finalTermScorefor100Percent;
            var getBeforeQuiz100 = calculatedFields.getBeforeQuiz100;
            var getAfterQuiz100 = calculatedFields.getAfterQuiz100;

            var getReadWrite = data.GetReadWrite;
            var getSamattana = data.GetSamattana;
            var getBehaviorLabel = data.GetBehaviorLabel;
            getReadWrite = ReadWriteScoreAutoCalculation((rowIndex + 1), getReadWrite);
            if (response.Assessments.length > 70) {
                if ($("#chkGradeAutoReadScore").is(':checked')) {
                    getReadWrite = CalculateBehaviourScore('', 999, "ReadWrite", (rowIndex + 1))
                    table.cell(rowIndex, getReadWriteColumnIndex).data(getReadWrite).draw();
                }
                
                if ($("#chkGradeAddSamattana").is(':checked')) {
                    getSamattana = CalculateBehaviourScore('', 999, "Samattana", (rowIndex + 1))
                    table.cell(rowIndex, getSamattanaColumnIndex).data(getSamattana).draw();
                }
               
                if ($("#chkGradeAddBehavior").is(':checked')) {
                    getBehaviorLabel = CalculateBehaviourScore('', 999, "Characteristics", (rowIndex + 1))
                    table.cell(rowIndex, getBehaviorLabelColumnIndex).data(getBehaviorLabel).draw();
                }
            }


            studentAssessmentScoreUpdateRequest.push({
                sID: data.sID, sPlaneID: data.sPlaneID, nGradeId: data.nGradeId,
                GetReadWrite: getReadWrite, scoreBeforeAfterMidTerm: data.BeforeAfterMidTermTotalScore,
                ScoreMidTerm: data.ScoreMidTerm, ScoreFinalTerm: data.ScoreFinalTerm,
                GetScore100: Number.isNaN(totalScoreFor100Percent) ? 0 : totalScoreFor100Percent,
                GetGradeLabel: totalGrade, GetBeforeQuiz100: getBeforeQuiz100, GetAfterQuiz100: getAfterQuiz100,
                GetQuiz100: quizScoreFor100Percent, GetMid100: midTermScoreFor100Percent, GetFinal100: finalTermScorefor100Percent,
                GetBehaviorTotal: data.GetBehaviorTotal, getReadWriteTotal: data.GetReadWriteTotal, getSamattanaTotal: data.GetSamattanaTotal,
                GetBehaviorLabel: getBehaviorLabel,
                GetSamattana: getSamattana, SubmitPeriod: response.GradeDTO.PeriodNow,
                ScoreBeforeMidTerm: data.ScoreBeforeMidTerm,
                ScoreAfterMidTerm: data.ScoreAfterMidTerm, MethodName: "UpdateStudentsGradeValueInDataTableAndDB",
                IsNewAssessmentCreated: IsNewAssessmentCreated,
                IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
            });

            readingsore = table.cell(rowIndex, getSamattanaColumnIndex).invalidate().render();
            readingsore = table.cell(rowIndex, getBehaviorLabelColumnIndex).invalidate().render();
            readingsore = table.cell(rowIndex, getReadWriteColumnIndex).invalidate().render();
        }
        rowIndex = rowIndex + 1;
    });

    if (rowCount > 0) {
        
        UpdateStudentAssessmentScore(studentAssessmentScoreUpdateRequest);
    }
    else {
        table = $(tableName).DataTable();
        UpdateStudentsGradeValueInDataTableAndDB();
    }

}

//Assessment Max Score On Change - Don't call wait function
function UpdateStudentAssessmentScoreOnMaxScoreChanges(studentAssessmentScoreUpdateRequest) {
    if (checkScoreEntered == true) {
        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/UpdateStudentAssessmentScore";
        params['data'] = JSON.stringify(studentAssessmentScoreUpdateRequest);
        params['requestType'] = "POST";
        params['successCallbackFunction'] = UpdateAssessmentScoreCallBack;

        //doAjax(params);
        activeAjaxConnections++;
        doAjaxSequencial(params);

    }
    else {
        $("body").mLoading("hide");
    }
}

function UpdateStudentAssessmentScore(studentAssessmentScoreUpdateRequest) {
    
    if (checkScoreEntered == true) {
        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/UpdateStudentAssessmentScore";
        params['data'] = JSON.stringify(studentAssessmentScoreUpdateRequest);
        params['requestType'] = "POST";
        params['successCallbackFunction'] = UpdateAssessmentScoreCallBack;
       
        //doAjax(params);
        activeAjaxConnections++;
        setTimeout(function () { doAjaxSequencial(params); }, 4000);

    }
}

function UpdateAssessmentScoreCallBack(errorMessage) {
    $("body").mLoading("hide");
    if (errorMessage == "InValidScore") {
        //Throw Error Message
        OpenErrorInfoDialog("พบข้อมูลที่กรอกไม่ถูกต้อง ทำให้ไม่สามารถใช้งานต่อได้ กรุณาติดต่อบริษัทเพื่อทำการแก้ไข");
    }
}

//Assessment Score On Change Event Update the individual Student Score
function UpdateAssessmentScoreInDb(newScore, sID, sPlaneID, nGradeId, assessmentId, rowNumber, selectedColIndex, assessmentTypeName) {

    var beforeAfterMidTermScoreTotal = table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data();   // $(".BeforeAfterMidTermTotalScore").html();
    var beforeMidTermScoreTotal = table.cell(rowNumber - 1, scoreBeforeMidTermColumnIndex).data(); 
    var afterMidTermScoreTotal = table.cell(rowNumber - 1, scoreAfterMidTermColumnIndex).data(); 
    var midTermScoreTotal = table.cell(rowNumber - 1, scoreMidTermColumnIndex).data();
    var finalTermScoreTotal = table.cell(rowNumber - 1, scoreFinalTermColumnIndex).data();

    var beforeAfterMidTermScoreTotalForDB = table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data();
    var beforeMidTermScoreTotalForDB = table.cell(rowNumber - 1, scoreBeforeMidTermColumnIndex).data();
    var afterMidTermScoreTotalForDB = table.cell(rowNumber - 1, scoreAfterMidTermColumnIndex).data();
    var midTermScoreTotalForDB = table.cell(rowNumber - 1, scoreMidTermColumnIndex).data();
    var finalTermScoreTotalForDB = table.cell(rowNumber - 1, scoreFinalTermColumnIndex).data();

    var quizScoreFor100Percent = 0;
    var getBeforeQuiz100 = 0;
    var getAfterQuiz100 = 0;
    var midTermScoreFor100Percent = 0;
    var finalTermScorefor100Percent = 0;
    var totalGrade = "";
    var totalScore = 0;
    //var requestCount = 0;
    var behaviourScoreTotal = table.cell(rowNumber - 1, getBehaviorTotalColumnIndex).data();
    var behaviourLabelScore = table.cell(rowNumber - 1, getBehaviorLabelColumnIndex).data();
    var getReadWriteTotal = table.cell(rowNumber - 1, getReadWriteTotalColumnIndex).data();
    var getReadWrite = table.cell(rowNumber - 1, getReadWriteColumnIndex).data();
    var getSamattanaTotal = table.cell(rowNumber - 1, getSamattanaTotalColumnIndex).data();
    var getSamattana = table.cell(rowNumber - 1, getSamattanaColumnIndex).data();

    var beforeAfterMidTermMaxScoreTotal = $(".BeforeAfterMidTermMaxScoreTotal").html();
    var midTermMaxScoreTotal = $(".MidTermMaxScoreTotal").html();
    var finalTermMaxScoreTotal = $(".FinalTermMaxScoreTotal").html();

    //Based on the Assessment Type Update- Update other field values
    if (assessmentTypeName == "Before Mid Term") {
        beforeMidTermScoreTotal = GetSumOfStudentScore(newScore, selectedColIndex, "Before Mid Term", rowNumber);
        beforeMidTermScoreTotalForDB = beforeMidTermScoreTotal;

        beforeMidTermScoreTotal = (beforeMidTermScoreTotal == "") ? 0 : Number(beforeMidTermScoreTotal);
        afterMidTermScoreTotal = (afterMidTermScoreTotal == "") ? 0 : Number(afterMidTermScoreTotal);

        beforeAfterMidTermScoreTotal = beforeMidTermScoreTotal + afterMidTermScoreTotal;

        if (beforeMidTermScoreTotalForDB == "" && afterMidTermScoreTotalForDB == "") {
            table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data("").draw();
        }
        else {
            table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data(beforeAfterMidTermScoreTotal).draw();
        }
        table.cell(rowNumber - 1, scoreBeforeMidTermColumnIndex).data(beforeMidTermScoreTotalForDB).draw();

        //Change Background color
        HighLightTotalScore(beforeAfterMidTermScoreTotal, "BeforeAfterMidTermTotalScore", beforeAfterMidTermMaxScoreTotal, rowNumber);
        
    }
    else if (assessmentTypeName == "After Mid Term") {
        afterMidTermScoreTotal = GetSumOfStudentScore(newScore, selectedColIndex, "After Mid Term", rowNumber);

        afterMidTermScoreTotalForDB = afterMidTermScoreTotal;

        beforeMidTermScoreTotal = (beforeMidTermScoreTotal == "") ? 0 : Number(beforeMidTermScoreTotal);
        afterMidTermScoreTotal = (afterMidTermScoreTotal == "") ? 0 : Number(afterMidTermScoreTotal);

        beforeAfterMidTermScoreTotal = beforeMidTermScoreTotal + afterMidTermScoreTotal;

        if (beforeMidTermScoreTotalForDB == "" && afterMidTermScoreTotalForDB == "") {
            table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data("").draw();
        }
        else {
            table.cell(rowNumber - 1, beforeAfterMidTermTotalScoreColumnIndex).data(beforeAfterMidTermScoreTotal).draw();
        }
        table.cell(rowNumber - 1, scoreAfterMidTermColumnIndex).data(afterMidTermScoreTotalForDB).draw();

        //Change Background color
        HighLightTotalScore(beforeAfterMidTermScoreTotal, "BeforeAfterMidTermTotalScore", beforeAfterMidTermMaxScoreTotal, rowNumber);
        
    }
    else if (assessmentTypeName == "Mid Term") {
        midTermScoreTotal = GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);

        table.cell(rowNumber - 1, scoreMidTermColumnIndex).data(midTermScoreTotal).draw();

        //Change Background color
        HighLightTotalScore(midTermScoreTotal, "ScoreMidTerm", midTermMaxScoreTotal, rowNumber);

    }
    else if (assessmentTypeName == "Final") {
        finalTermScoreTotal = GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);

        table.cell(rowNumber - 1, scoreFinalTermColumnIndex).data(finalTermScoreTotal).draw();

        //Change Background color
        HighLightTotalScore(finalTermScoreTotal, "ScoreFinalTerm", finalTermMaxScoreTotal, rowNumber);
    }
    else if (assessmentTypeName == "Characteristics") {
        behaviourScoreTotal = GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);

        table.cell(rowNumber - 1, getBehaviorTotalColumnIndex).data(behaviourScoreTotal).draw();

        behaviourLabelScore = CalculateBehaviourScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);

        table.cell(rowNumber - 1, getBehaviorLabelColumnIndex).data(behaviourLabelScore).draw();
    }
    else if (assessmentTypeName == "ReadWrite") {
        getReadWriteTotal = GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);
        table.cell(rowNumber - 1, getReadWriteTotalColumnIndex).data(getReadWriteTotal).draw();
        
        if (response.Assessments.length > 70 && $("#chkGradeAutoReadScore").is(':checked')) {
            getReadWrite = CalculateBehaviourScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);
        }
       
        table.cell(rowNumber - 1, getReadWriteColumnIndex).data(getReadWrite).draw();
    }
    else if (assessmentTypeName == "Samattana") {
        getSamattanaTotal = GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);
        getSamattana = CalculateBehaviourScore(newScore, selectedColIndex, assessmentTypeName, rowNumber);

        table.cell(rowNumber - 1, getSamattanaTotalColumnIndex).data(getSamattanaTotal).draw();
        table.cell(rowNumber - 1, getSamattanaColumnIndex).data(getSamattana).draw();
    }

    beforeAfterMidTermScoreTotalForDB = (beforeAfterMidTermScoreTotal == "" || isNaN(beforeAfterMidTermScoreTotal)) ? "" : beforeAfterMidTermScoreTotal;
    midTermScoreTotalForDB = (midTermScoreTotal == "" || isNaN(midTermScoreTotal)) ? "" : midTermScoreTotal;
    finalTermScoreTotalForDB = (finalTermScoreTotal == "" || isNaN(finalTermScoreTotal)) ? "" : finalTermScoreTotal;

    beforeAfterMidTermScoreTotal = (beforeAfterMidTermScoreTotal == "" || isNaN(beforeAfterMidTermScoreTotal)) ? 0 : Number(beforeAfterMidTermScoreTotal);
    midTermScoreTotal = (midTermScoreTotal == "" || isNaN(midTermScoreTotal)) ? 0 : Number(midTermScoreTotal);
    finalTermScoreTotal = (finalTermScoreTotal == "" || isNaN(finalTermScoreTotal)) ? 0 : Number(finalTermScoreTotal);

    totalScore = beforeAfterMidTermScoreTotal + midTermScoreTotal + finalTermScoreTotal;

    if (totalScore != '' && totalScore != 'undefined' && totalScore % 1 != 0) {
        totalScore = totalScore.toFixed(2);
    } 

    table.cell(rowNumber - 1, TotalScoreColumnIndex).data(totalScore).draw();
    
    HighLightTotalScore(totalScore, "TotalScore", $(".AssessmentMaxTotalScore").html(), rowNumber);

    var calculatedFields = DynamicCalculation(rowNumber, beforeAfterMidTermScoreTotal, beforeMidTermScoreTotal, afterMidTermScoreTotal, midTermScoreTotal, finalTermScoreTotal);
    //readingsore = calculatedFields.readingsore;
    totalGrade = calculatedFields.totalGrade;
    totalScoreFor100Percent = calculatedFields.totalScoreFor100Percent;
    quizScoreFor100Percent = calculatedFields.quizScoreFor100Percent;
    getBeforeQuiz100 = calculatedFields.getBeforeQuiz100;
    getAfterQuiz100 = calculatedFields.getAfterQuiz100;
    midTermScoreFor100Percent = calculatedFields.midTermScoreFor100Percent;
    finalTermScorefor100Percent = calculatedFields.finalTermScorefor100Percent;
    //var getSamattana = table.cell(rowNumber - 1, getSamattanaColumnIndex).data();

    //-----------------------------------
    //Calculate ReadWrite Score
    getReadWrite = ReadWriteScoreAutoCalculation(rowNumber, getReadWrite);
    //---------------------------------------
    var studentAssessmentScoreUpdateRequest = [];
    studentAssessmentScoreUpdateRequest.push({
        sID: sID, sPlaneID: sPlaneID, nGradeId: nGradeId, AssessmentId: assessmentId, Score: newScore,
        GetReadWrite: getReadWrite, scoreBeforeAfterMidTerm: beforeAfterMidTermScoreTotalForDB, ScoreMidTerm: midTermScoreTotalForDB, ScoreFinalTerm: finalTermScoreTotalForDB, GetScore100: Number.isNaN(totalScoreFor100Percent) ? 0 : totalScoreFor100Percent, GetGradeLabel: totalGrade,
        GetQuiz100: Number.isNaN(quizScoreFor100Percent) ? 0 : quizScoreFor100Percent, GetBeforeQuiz100: getBeforeQuiz100, GetAfterQuiz100: getAfterQuiz100, GetMid100: midTermScoreFor100Percent, GetFinal100: finalTermScorefor100Percent, GetBehaviorTotal: behaviourScoreTotal, GetReadWriteTotal: getReadWriteTotal, GetSamattanaTotal: getSamattanaTotal,
        GetBehaviorLabel: behaviourLabelScore, GetSamattana: getSamattana, SubmitPeriod: response.GradeDTO.PeriodNow,
        ScoreBeforeMidTerm: beforeMidTermScoreTotalForDB, ScoreAfterMidTerm: afterMidTermScoreTotalForDB, MethodName: "AssessmentScoreOnChange",
        RequestOrder: requestCount++,
        IsNewAssessmentCreated: IsNewAssessmentCreated,
        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
    });

    var doAjax_params = $.extend({}, doAjax_params_default);
    doAjax_params['url'] = "../../api/AssessmentScore/UpdateStudentAssessmentScore";
    doAjax_params['data'] = JSON.stringify(studentAssessmentScoreUpdateRequest);
    doAjax_params['successCallbackFunction'] = UpdateAssessmentScoreCallBack;
    doAjax_params['requestType'] = "POST";
     
    //if (requestCount == 1) {
    //    doAjax(doAjax_params);
    //}
    //else {
          
        activeAjaxConnections++;
        //doAjaxSequencial(doAjax_params);
    setTimeout(function () { doAjaxSequencial(doAjax_params); }, 4000);
    //}
}

function ReadWriteScoreAutoCalculation(rowNumber, getReadWrite) {
    var totalScoreFor100Percentage;
    //if (LevelName != null && LevelName != "" && LevelName != "ปวช." && LevelName != "ปวส." && LevelName != "มัธยมศึกษา") {

    totalScoreFor100Percentage = (getUrlParameter("term") == "2") ? table.cell(rowNumber - 1, Term2TotalFor100PercentageColumnIndex).data() : table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data();
    //}
    //else {
    //    totalScoreFor100Percentage = table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data();
    //}
    
    if (!$("#chkGradeAutoReadScore").is(':checked')) {
        if (Number(totalScoreFor100Percentage) >= 80) { getReadWrite = "3"; }
        else if (Number(totalScoreFor100Percentage) >= 60) { getReadWrite = "2"; }
        else if (Number(totalScoreFor100Percentage) >= 50) { getReadWrite = "1"; }
        else { getReadWrite = "0"; }
        table.cell(rowNumber - 1, getReadWriteColumnIndex).data(getReadWrite).draw();
    }
   
    return getReadWrite;
}

function HighLightTotalScore(scoreTotal, controlClassName, maxScoreTotal, rowNumber) {
    if (scoreTotal != "" && maxScoreTotal != "" && $("#ddlPassingCutOffPercentage").val() > -1) {
        var cutOfMark = (parseFloat(maxScoreTotal) * parseFloat($("#ddlPassingCutOffPercentage").val())) / 100;
        if (scoreTotal < cutOfMark) {
            $("." + controlClassName +":eq(" + rowNumber + ")").addClass("alert-warning");
        }
        else {
            $("." + controlClassName +":eq(" + rowNumber + ")").removeClass("alert-warning");
        }
    }
}

function DynamicCalculation(rowNumber, beforeAfterMidTermScoreTotal, beforeMidTermScoreTotal, afterMidTermScoreTotal, midTermScoreTotal, finalTermScoreTotal) {

    var checkAtleaseOneScoreEntered = false;
    if ((beforeAfterMidTermScoreTotal != null && beforeAfterMidTermScoreTotal != '') || (midTermScoreTotal != null && midTermScoreTotal != '') || (finalTermScoreTotal != null && finalTermScoreTotal != '')) {
        checkAtleaseOneScoreEntered = true;
        checkScoreEntered = true;
    }
    if (isNaN(beforeAfterMidTermScoreTotal) || beforeAfterMidTermScoreTotal == null || beforeAfterMidTermScoreTotal == 'undefined' || beforeAfterMidTermScoreTotal == '')
        beforeAfterMidTermScoreTotal = 0;

    if (isNaN(midTermScoreTotal) || midTermScoreTotal == null || midTermScoreTotal == 'undefined' || midTermScoreTotal == '')
        midTermScoreTotal = 0;

    if (isNaN(finalTermScoreTotal) || finalTermScoreTotal == null || finalTermScoreTotal == 'undefined' || finalTermScoreTotal == '')
        finalTermScoreTotal = 0;


    var totalGrade = "";
    var term1AndTerm2TotalGrade = "";
    //var readingsore = "";
    //var readWriteScore = "";
    var quizScoreFor100Percent = 0;
    var getBeforeQuiz100 = 0;
    var getAfterQuiz100 = 0;
    var midTermScoreFor100Percent = 0;
    var finalTermScorefor100Percent = 0;

    var decimalValue = ($("#chkGradeDicimal").is(':checked')) ? 2 : decimalValue;
    var beforeAfterMidTermMaxScoreTotal = $("#hdnBeforeAfterMidTermMaxScoreTotal").val();
    var beforeMidTermMaxScoreTotal = $("#hdnBeforeMidTermMaxScoreTotal").val();
    var afterMidTermMaxScoreTotal = $("#hdnAfterMidTermMaxScoreTotal").val();

    var midTermMaxScoreTotal = $("#hdnMidTermMaxScoreTotal").val();
    var finalTermMaxScoreTotal = $("#hdnFinalTermMaxScoreTotal").val();


    //drop down % Value
    var ddlScorePercentage = parseInt(GetFRatioQuiz());
    var ddlMidTermPercentage = parseInt($("#ddlMidTermPercentage").val());
    var ddlFinalTermPercentage = parseInt($("#ddlFinalTermPercentage").val());

    //var ddlBeforeMidTermPercentage = (afterMidTermScoreTotal == null || afterMidTermScoreTotal == 'undefined' || afterMidTermScoreTotal == '' || afterMidTermScoreTotal == 0) ? ddlScorePercentage : ddlScorePercentage / 2;

    //var ddlAfterMidTermPercentage = ddlScorePercentage / 2;
    var ddlBeforeMidTermPercentage = parseInt($("#ddlBeforeMidTermPercentage").val());
    var ddlAfterMidTermPercentage = parseInt($("#ddlAfterMidTermPercentage").val());

    //Total 100 % Calculation
    if (Number(beforeAfterMidTermMaxScoreTotal) != 0)
        quizScoreFor100Percent = Number(((beforeAfterMidTermScoreTotal) * parseInt(ddlScorePercentage)) / beforeAfterMidTermMaxScoreTotal).toFixed(decimalValue);

    if (beforeMidTermMaxScoreTotal != null && beforeMidTermMaxScoreTotal != 'undefined' && Number(beforeMidTermMaxScoreTotal) != 0 && beforeMidTermScoreTotal != null && beforeMidTermScoreTotal != 'undefined') {
        getBeforeQuiz100 = Number(((beforeMidTermScoreTotal) * parseInt(ddlBeforeMidTermPercentage)) / beforeMidTermMaxScoreTotal).toFixed(decimalValue);
    }

    if (afterMidTermMaxScoreTotal != null && afterMidTermMaxScoreTotal != 'undefined' && Number(afterMidTermMaxScoreTotal) != 0 && afterMidTermScoreTotal != null && afterMidTermScoreTotal != 'undefined') {
        getAfterQuiz100 = Number(((afterMidTermScoreTotal) * parseInt(ddlAfterMidTermPercentage)) / afterMidTermMaxScoreTotal).toFixed(decimalValue);
    }
    

    if (midTermMaxScoreTotal != 0 && midTermMaxScoreTotal != "")
        midTermScoreFor100Percent = Number((midTermScoreTotal * ddlMidTermPercentage) / midTermMaxScoreTotal).toFixed(decimalValue);

    if (finalTermMaxScoreTotal != 0 && finalTermMaxScoreTotal != "")
        finalTermScorefor100Percent = Number((finalTermScoreTotal * ddlFinalTermPercentage) / finalTermMaxScoreTotal).toFixed(decimalValue);

    //console.log("beforeAfterMidTermScoreTotal " + beforeAfterMidTermScoreTotal);
    //console.log("ddlScorePercentage " + ddlScorePercentage);
    //console.log("beforeAfterMidTermMaxScoreTotal " + beforeAfterMidTermMaxScoreTotal);

    //console.log("midTermScoreTotal " + midTermScoreTotal);
    //console.log("ddlMidTermPercentage " + ddlMidTermPercentage);
    //console.log("midTermMaxScoreTotal " + midTermMaxScoreTotal);

    //console.log("finalTermScoreTotal " + finalTermScoreTotal);
    //console.log("ddlFinalTermPercentage " + ddlFinalTermPercentage);
    //console.log("finalTermMaxScoreTotal " + finalTermMaxScoreTotal);
    //console.log("quizScoreFor100Percent " + Number(quizScoreFor100Percent));
    //console.log("getBeforeQuiz100 " + Number(getBeforeQuiz100));
    //console.log("getAfterQuiz100 " + Number(getAfterQuiz100));
    //console.log("midTermScoreFor100Percent " + Number(midTermScoreFor100Percent));
    //console.log("finalTermScorefor100Percent " + Number(finalTermScorefor100Percent));
    //console.log("totalScoreFor100Percent " + Number(totalScoreFor100Percent));

    var totalScoreFor100Percent = 0;
    if (Number(getUrlParameter("year")) >= 2563) {
        //console.log(">2563")
        totalScoreFor100Percent = Number(Number(getBeforeQuiz100) + Number(getAfterQuiz100) + Number(midTermScoreFor100Percent) + Number(finalTermScorefor100Percent)).toFixed(decimalValue);
    }
    else {
        //console.log("<2563")
        totalScoreFor100Percent = Number(Number(quizScoreFor100Percent) + Number(midTermScoreFor100Percent) + Number(finalTermScorefor100Percent)).toFixed(decimalValue);
    }


    totalGrade = GetGradeLabel(totalScoreFor100Percent, rowNumber);

    //Update the value in the DataTable
    if (totalScoreFor100Percent != '' && totalScoreFor100Percent != 'undefined' && totalScoreFor100Percent % 1 != 0) {
        totalScoreFor100Percent = parseFloat(totalScoreFor100Percent).toFixed(2);
    } 

    var otherTerm = 0;
    var TotalFor100Percentage;
    var Term2TotalFor100ercentage;
    var term1AndTerm2ScoreTotal;
    //Update the Total 100 % Value based on selected Term
    if (getUrlParameter("term") == "2" && checkAtleaseOneScoreEntered == true) {
        //console.log("Term2");
        table.cell(rowNumber - 1, Term2TotalFor100PercentageColumnIndex).data(totalScoreFor100Percent).draw();
        table.cell(rowNumber - 1, Term2TotalFor50PercentageColumnIndex).data((totalScoreFor100Percent != "") ? (parseFloat(totalScoreFor100Percent) / 2).toFixed(decimalValue) : "").draw();
        TotalFor100Percentage = table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data();
        Term2TotalFor100ercentage = table.cell(rowNumber - 1, Term2TotalFor100PercentageColumnIndex).data();
        if (TotalFor100Percentage != "" && TotalFor100Percentage != 'undefined' && TotalFor100Percentage != null) {
            term1AndTerm2ScoreTotal = ((parseFloat(totalScoreFor100Percent) + parseFloat(TotalFor100Percentage)) / 2).toFixed(decimalValue);
        }
        else {
            term1AndTerm2ScoreTotal = totalScoreFor100Percent;
        }

        term1AndTerm2TotalGrade = GetGradeLabel(term1AndTerm2ScoreTotal, rowNumber);
    }
    else if (checkAtleaseOneScoreEntered == true) {
        table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data(totalScoreFor100Percent).draw();
        //var totalFor50Percentage = (totalScoreFor100Percent != "") ? (parseFloat(totalScoreFor100Percent) / 2).toFixed(decimalValue) : "";
        table.cell(rowNumber - 1, TotalFor50PercentageColumnIndex).data((totalScoreFor100Percent != "") ? (parseFloat(totalScoreFor100Percent) / 2).toFixed(decimalValue) : "").draw();
        TotalFor100Percentage = table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data();
        Term2TotalFor100ercentage = table.cell(rowNumber - 1, Term2TotalFor100PercentageColumnIndex).data();
        if (Term2TotalFor100ercentage != "" && Term2TotalFor100ercentage != 'undefined' && Term2TotalFor100ercentage != null) {
            term1AndTerm2ScoreTotal = ((parseFloat(totalScoreFor100Percent) + parseFloat(Term2TotalFor100ercentage)) / 2).toFixed(decimalValue);
        }
        else {
            term1AndTerm2ScoreTotal = totalScoreFor100Percent;
        }
        term1AndTerm2TotalGrade = GetGradeLabel(term1AndTerm2ScoreTotal, rowNumber);
    }
    else {
        table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data("").draw();
        table.cell(rowNumber - 1, TotalFor50PercentageColumnIndex).data("").draw();
    }

   
    //Calculate the Average of Term1 and Term2
    if ((TotalFor100Percentage == "" && (Term2TotalFor100ercentage == "" || Term2TotalFor100ercentage == null)) || (typeof TotalFor100Percentage == "undefined" && typeof Term2TotalFor100ercentage == "undefined")) {
        term1AndTerm2ScoreTotal = "";
        term1AndTerm2TotalGrade = "";
    }


    //Calculate the Grade based on Term1 and Term 2 Total
    if (term1AndTerm2ScoreTotal == "NaN") { term1AndTerm2ScoreTotal = "0"; term1AndTerm2TotalGrade = "0"; }
    else if (term1AndTerm2ScoreTotal == "") { term1AndTerm2ScoreTotal = ""; term1AndTerm2TotalGrade = ""; }
    else { term1AndTerm2TotalGrade = GetGradeLabel(term1AndTerm2ScoreTotal, rowNumber); term1AndTerm2ScoreTotal = parseFloat(term1AndTerm2ScoreTotal).toFixed(decimalValue) }

    table.cell(rowNumber - 1, Term1AndTerm2ScoreTotalColumnIndex).data(term1AndTerm2ScoreTotal).draw();
    table.cell(rowNumber - 1, getGradeLabelColumnIndex).data(totalGrade).draw();
    table.cell(rowNumber - 1, Term1AndTerm2GradeLabelColumnIndex).data(term1AndTerm2TotalGrade).draw();

    return { totalGrade: totalGrade, totalScoreFor100Percent: totalScoreFor100Percent, quizScoreFor100Percent: quizScoreFor100Percent, getBeforeQuiz100: getBeforeQuiz100, getAfterQuiz100: getAfterQuiz100, midTermScoreFor100Percent: midTermScoreFor100Percent, finalTermScorefor100Percent: finalTermScorefor100Percent, Term1AndTerm2ScoreTotal: term1AndTerm2ScoreTotal, Term1AndTerm2TotalGrade: term1AndTerm2TotalGrade};
}

function GetGradeLabel(getScore100, rowNumber) {
    var getSpecial = table.cell(rowNumber - 1, getSpecialColumnIndex).data();
    var getGradeLabel = "";
    if (getSpecial == "-1" || getSpecial == "undefined") {
        getScore100 = (getScore100 == "") ? "0" : getScore100;
        if (Number(getScore100) > 79.99) { getGradeLabel = "4.0"; }
        else if (Number(getScore100) > 74.99) { getGradeLabel = "3.5"; }
        else if (Number(getScore100) > 69.99) { getGradeLabel = "3.0"; }
        else if (Number(getScore100) > 64.99) { getGradeLabel = "2.5"; }
        else if (Number(getScore100) > 59.99) { getGradeLabel = "2.0"; }
        else if (Number(getScore100) > 54.99) { getGradeLabel = "1.5"; }
        else if (Number(getScore100) > 49.99) { getGradeLabel = "1.0"; }
        else if (Number(getScore100) > 39.99) { getGradeLabel = "0"; }
        else { getGradeLabel = "0"; }
    }
    else if (getSpecial == "1") { getGradeLabel = "ร"  }
    else if (getSpecial == "2") { getGradeLabel = "มส" }
    else if (getSpecial == "3") { getGradeLabel = "มก" }
    else if (getSpecial == "4") { getGradeLabel = "ผ"  }
    else if (getSpecial == "5") { getGradeLabel = "มผ" }
    else if (getSpecial == "6") { getGradeLabel = "อื่นๆ" }
    else if (getSpecial == "7") { getGradeLabel = "ขร" }
    else if (getSpecial == "8") { getGradeLabel = "ขส" }
    else if (getSpecial == "9") { getGradeLabel = "ท"}
    else if (getSpecial == "10") { getGradeLabel = "ดีเยี่ยม"}
    else if (getSpecial == "11") { getGradeLabel = "ดี" }
    else if (getSpecial == "12") { getGradeLabel = "พอใช้" }
    else if (getSpecial == "13") { getGradeLabel = "ปรับปรุง" }
    return getGradeLabel;
}

function UpdateCurricularValueInDb(newValue, sID, nGradeId, rowNumber, curricularName) {


    var request = {
        sID: sID, nGradeId: nGradeId,
        GetReadWrite: "",
        IsGetReadWrite: false,
        GetSpecial: "",
        IsGetSpecial: false,
        GetBehaviorLabel: "",
        IsGetBehaviorLabel: false,
        GetBehaviorTotal: "",
        IsGetBehaviorTotal: false,
        GetSamattana: "",
        IsGetSamattana: false,
        GetGradeLabel: ""
    };

    request.GetGradeLabel = table.cell(rowNumber - 1, getGradeLabelColumnIndex).data();
    request.GetSpecial = table.cell(rowNumber - 1, getSpecialColumnIndex).data();
    if (curricularName == "GetBehaviorLabel") {
        table.cell(rowNumber - 1, getBehaviorLabelColumnIndex).data(newValue).draw();
        request.GetBehaviorLabel = newValue;
        request.IsGetBehaviorLabel = true;
        checkScoreEntered = true;
    }
    else if (curricularName == "GetReadWrite") {
        table.cell(rowNumber - 1, getReadWriteColumnIndex).data(newValue).draw();
        request.GetReadWrite = newValue;
        request.IsGetReadWrite = true;
        checkScoreEntered = true;
    }
    else if (curricularName == "GetSamattana") {
        table.cell(rowNumber - 1, getSamattanaColumnIndex).data(newValue).draw();
        request.GetSamattana = newValue;
        request.IsGetSamattana = true;
        checkScoreEntered = true;
    }
    else if (curricularName == "GetSpecial") {
        table.cell(rowNumber - 1, getSpecialColumnIndex).data(newValue).draw();
        //When Update the Other Assessment Calculate the grade label and update in the DB.
        if (newValue != "-1") {
            request.GetGradeLabel = GetGradeLabel(0, rowNumber);
        }
        else {
           var score100 = (getUrlParameter("term") == "2") ? table.cell(rowNumber - 1, Term2TotalFor100PercentageColumnIndex).data() : table.cell(rowNumber - 1, TotalFor100PercentageColumnIndex).data();
            request.GetGradeLabel = GetGradeLabel(score100, rowNumber);
        }
        table.cell(rowNumber - 1, getGradeLabelColumnIndex).data(request.GetGradeLabel).draw();
        request.GetSpecial = newValue;
        request.IsGetSpecial = true;
        checkScoreEntered = true;
    }
    request.IsCurriculamUpdate = true;
    request.SubmitPeriod = response.GradeDTO.PeriodNow;
    request.MethodName = "UpdateCurricularValueInDb";
    request.IsNewAssessmentCreated = IsNewAssessmentCreated;
    request.IsRequestForCurrentAcademicYear = IsRequestForCurrentAcademicYear;
    var studentAssessmentScoreUpdateRequest = [];
    studentAssessmentScoreUpdateRequest.push(request);

   
    UpdateStudentAssessmentScore(studentAssessmentScoreUpdateRequest);

}

function NextAssessmentClick(control) {
    var assessmentTypeName = $(control).data('id');

    var selectedGroupColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == assessmentTypeName;
    });

    $.each(selectedGroupColumns, function (index, Value) {
        table.columns(visbleColumnList).visible(false);

        if (Value.Columns.length > 10) {
            table.columns(Value.Columns.slice(10)).visible(true);
            visbleColumnList = Value.Columns.slice(10)
        }
    });

    GetAssmentTypeHeaderText(assessmentTypeName, 2);

    $('#PreviouseAssessment' + assessmentTypeName.replace(/ /g, "")).removeClass("hidden");
    $('#PreviouseAssessment' + assessmentTypeName.replace(/ /g, "")).show();
    $(control).hide();
}

function PreviouseAssessmentClick(control) {
    var assessmentTypeName = $(control).data('id');

    var selectedGroupColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == assessmentTypeName;
    });

    $.each(selectedGroupColumns, function (index, Value) {
        if (Value.Columns.length > 10) {
            table.columns(visbleColumnList).visible(false);
        }

        table.columns(Value.Columns.slice(0, 10)).visible(true);
        visbleColumnList = Value.Columns.slice(0, 10);

    });

    GetAssmentTypeHeaderText(assessmentTypeName, 1);

    $('#NextAssessment' + assessmentTypeName.replace(/ /g, "")).removeClass("hidden");
    $('#NextAssessment' + assessmentTypeName.replace(/ /g, "")).show();
    $(control).hide();
}

function ToggleTabColumn(assessmentTypeName) {
    var selectedAssessmentTypeName = assessmentTypeName;
    var selectedGroupColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == selectedAssessmentTypeName;
    });

    if (selectedGroupColumns != null) {
        //Disable
        if (visbleColumnList.length == 0) {
            var otherGroupColumns = $.grep(toggleGroupColumns, function (n) {
                return n.AssessmentTypeName != selectedAssessmentTypeName && n.AssessmentTypeName !== "";
            });

            var hideColumnList = [];
            if (otherGroupColumns != null) {
                $.each(otherGroupColumns, function (index, Value) {
                    hideColumnList = $.merge(hideColumnList, Value.Columns)
                });

                table.columns(hideColumnList).visible(false);
            }
        }
        else {
            table.columns(visbleColumnList).visible(false);
        }

        //Enable
        $.each(selectedGroupColumns, function (index, Value) {
            //console.log(assessmentTypeName);
            if (assessmentTypeName == "ReadWrite" || assessmentTypeName == "Samattana") {
                visbleColumnList = Value.Columns.slice(0, 5);
                table.columns(Value.Columns.slice(0, 5)).visible(true);

                if (Value.Columns.length > 5) {
                    table.columns(Value.Columns.slice(5)).visible(false);
                }
            }
            else {
                visbleColumnList = Value.Columns.slice(0, 10);
                table.columns(Value.Columns.slice(0, 10)).visible(true);
            }
            //console.log("Enable Column List" + Value.Columns);
            
            if (Value.Columns.length > 10) {
                table.columns(Value.Columns.slice(10)).visible(false);
            }
        });

        $('#NextAssessment').removeClass("hidden");
        $('#NextAssessment').show();
        $('#PreviouseAssessment').hide();

        //table.rows().invalidate().render();
    }
    //});
}

function GetAssmentTypeHeaderText(assessmentTypeName, pageId) {
    //console.log(assessmentTypeName);
    var pageNumberText = (pageId == 1) ? " 1 - 10" : " 11 - 20";

   
    var assmentTypeHeaderText = "";
    switch (assessmentTypeName) {

        case "Before Mid Term":
            assmentTypeHeaderText = languageContent[0].BeforeMidTermTypeHeaderText + pageNumberText;
            $("#BeforeMidTermType").html(assmentTypeHeaderText);
            break;
        case "Mid Term":
            assmentTypeHeaderText = languageContent[0].MidTermTypeHeaderText + pageNumberText;
            $("#MidTermType").html(assmentTypeHeaderText);
            break;
        case "After Mid Term":
            assmentTypeHeaderText = languageContent[0].AfterMidTermTypeHeaderText + pageNumberText;
            $("#AfterMidTermType").html(assmentTypeHeaderText);
            break;
        case "Final":
            assmentTypeHeaderText = languageContent[0].FinalHeaderText + pageNumberText;
            $("#FinalType").html(assmentTypeHeaderText);
            break;
        case "Characteristics":
            assmentTypeHeaderText = languageContent[0].BehaviourTypeHeaderText + pageNumberText;
            $("#CharacteristicsType").html(assmentTypeHeaderText);
            break;
        case "ReadWrite":
            pageNumberText = (pageId == 1) ? " 1 - 5" : "";
            //console.log(pageNumberText);
            assmentTypeHeaderText = languageContent[0].ReadWriteTypeHeaderText + pageNumberText;
            $("#ReadWriteType").html(assmentTypeHeaderText);
            break;
        case "Samattana":
            pageNumberText = (pageId == 1) ? " 1 - 5" : "";
            //console.log(pageNumberText);
            assmentTypeHeaderText = languageContent[0].SamattanaTypeHeaderText + pageNumberText;
            $("#SamattanaType").html(assmentTypeHeaderText);
            break;
    }

    return assmentTypeHeaderText;
}

function GetHeaderContent(htmlTemplate, response) {
    var assessmentTypeHeaders = "";
    if (response.AssessmentTypeHeaders != "undefined") {

        $.each(response.AssessmentTypeHeaders, function (index, val) {

            assessmentTypeHeaders += " <th colspan='" + val.ToTalAssessment + "' style='height:auto;text-align:center'>";
            if (val.ToTalAssessment > 10) {
                assessmentTypeHeaders += "<span id='PreviouseAssessment" + val.AssessmentTypeName.replace(/ /g, "") + "' onclick='PreviouseAssessmentClick(this)' data-id='" + val.AssessmentTypeName + "' class='glyphicon glyphicon-chevron-left hidden' style='font-size:80%;float:left;margin:5px;cursor: pointer;' />"
            }

            assessmentTypeHeaders += "<label id='" + val.AssessmentTypeName.replace(/ /g, "") + "Type' >" + GetAssmentTypeHeaderText(val.AssessmentTypeName, 1) + "</label>"

            if (val.ToTalAssessment > 10) {
                assessmentTypeHeaders += " <span onclick='NextAssessmentClick(this)' data-id='" + val.AssessmentTypeName + "' class='glyphicon glyphicon-chevron-right' id='NextAssessment" + val.AssessmentTypeName.replace(/ /g, "") + "' style='font-size:80%;float:right;margin:5px;cursor: pointer;' />"
            }

            assessmentTypeHeaders += "</th> ";
        });
    };

    var AssessmentNameHeaderText = "";
    var AssessmentMaxScoreHeaderText = "";
    if (response.Assessments != "undefined") {
        var assessmentColumnIndex = 5;
        $.each(response.Assessments, function (index, val) {
            //val.IsLocked = true;
            var assessmentTypeDescriptionClass = val.AssessmentTypeDescription.replace(/ /g, "");
            var headerText = '<th class="rotate ' + assessmentTypeDescriptionClass + 'Type AssessmentNameHeaderText"><div class="rotate">';
            if (!val.IsLocked && !(assessmentTypeDescriptionClass == "Characteristics" || assessmentTypeDescriptionClass == "ReadWrite" || assessmentTypeDescriptionClass == "Samattana")) {
                headerText += ' <span class="fa fa-edit gly-rotate-90 OpenAssessmentNameDialog" data-id="' + val.AssessmentId + '" id="' + val.AssessmentId + '" style="cursor: pointer; font-size: 70%; color: white;" data-toggle="modal" data-target="#AssessmentNameDialog" /> ';
              
            }

            headerText += '<span name="' + val.AssessmentId + '" data-pt-gravity="top -45 50" Enabled="false" style="padding-left:5px">' + val.AssessmentName + '</span></div >';

            if (!val.IsLocked && (assessmentTypeDescriptionClass == "Characteristics" || assessmentTypeDescriptionClass == "ReadWrite" || assessmentTypeDescriptionClass == "Samattana")) {
                headerText += '<div style="margin-left:14px;margin-bottom:4px"><span class="fa fa-edit gly-rotate-90 OpenAssessmentNameDialog" data-id="' + val.AssessmentId + '" id="' + val.AssessmentId + '" style="cursor: pointer; font-size: 70%; color: white;" data-toggle="modal" data-target="#AssessmentNameDialog" /><span class="numberCircleNew OpenAssessmentDefaultValueDialog" onclick="OpenAssessmentDefaultValueDialogClick(' + val.AssessmentId + ',\'' + assessmentTypeDescriptionClass + '\')" id="Default' + val.AssessmentId + '" data-toggle="modal" data-target="#AssessmentDefaultValueDialog" ><span>3</span></span></div>'

            }

            headerText += '</th >';
            AssessmentNameHeaderText += headerText;

            AssessmentMaxScoreHeaderText += '<th>';
            //console.log(val.IsLocked);
            if (!val.IsLocked) {
                AssessmentMaxScoreHeaderText += '<input type="text" oncopy="return false" onpaste="return false" onFocus=(this.oldValue=this.value) maxlength="5" id="Max' + val.AssessmentId + '" value="' + val.AssessmentMaxScore + '" onkeydown="OnKeyDownAssessmentMaxScoreCheck(event, this,\'Max' + val.AssessmentId + '\',\'' + response.column[assessmentColumnIndex + 1].data + '\',\'' + response.column[assessmentColumnIndex - 1].data +'\')" onchange="AssessmentMaxScoreOnChange(this,' + val.AssessmentId + ',' + assessmentColumnIndex + ',\'' + val.AssessmentTypeDescription + '\',' + val.NGradeId + ',this.oldValue)" onkeypress="return isNumberKey(event, this,\'' + val.AssessmentTypeDescription +'\')" class="' + assessmentTypeDescriptionClass + ' AssessmentMaxScore AssessmentScoreBox" />';
            }
            else {
                AssessmentMaxScoreHeaderText += '<label class="AssessmentScoreBox">' + val.AssessmentMaxScore + '</label>';
            }

            AssessmentMaxScoreHeaderText += '</th >';

            assessmentColumnIndex++;
            console.log("GetHeaderContent");
            //If total is wrong trigger the specific control manually
            if (maxBeforeMidTermTotalFirstControlId == "" && response.IsMaxBeforeMidTermTotalIssue == true && val.AssessmentTypeDescription == "Before Mid Term" && val.AssessmentMaxScore != "") {
                console.log("IsMaxBeforeMidTermTotalIssue" + response.IsMaxBeforeMidTermTotalIssue);
                maxBeforeMidTermTotalFirstControlId ="#Max" + val.AssessmentId;
            }

            if (maxAfterMidTermTotalFirstControlId == "" && response.IsMaxAfterMidTermTotalIssue == true && val.AssessmentTypeDescription == "After Mid Term" && val.AssessmentMaxScore != "") {

                console.log("IsMaxAfterMidTermTotalIssue" + response.IsMaxAfterMidTermTotalIssue);
                maxAfterMidTermTotalFirstControlId = "#Max" + val.AssessmentId;
            }

            if (maxMidTermFirstControlId == "" && response.IsMaxMidTermTotalIssue == true && val.AssessmentTypeDescription == "Mid Term" && val.AssessmentMaxScore != "") {
                console.log("IsMaxMidTermTotalIssue" + response.IsMaxMidTermTotalIssue);
                maxMidTermFirstControlId = "#Max" + val.AssessmentId;
            }

            if (maxFinalTermFirstControlId == "" && response.IsMaxFinalTermTotalIssue == true && val.AssessmentTypeDescription == "Final" && val.AssessmentMaxScore != "") {
                console.log("IsMaxFinalTermTotalIssue" + response.IsMaxFinalTermTotalIssue);
                maxFinalTermFirstControlId = "#Max" + val.AssessmentId;
            }

        });
    }


    var headerText = htmlTemplate.replace("{{AssessmentTypeHeaderText}}", assessmentTypeHeaders);
    headerText = headerText.replace("{{AssessmentNameHeaderText}}", AssessmentNameHeaderText);
    headerText = headerText.replace("{{AssessmentMaxScoreHeaderText}}", AssessmentMaxScoreHeaderText);
    headerText = headerText.replace("{{TabColSpan}}", response.column.length);
    var beforeAfterMidTermMaxTotal = (response.GradeDTO.MaxGradeTotal == null || response.GradeDTO.MaxGradeTotal == "") ? 0 : response.GradeDTO.MaxGradeTotal; //GetSumOfMaxScore(1);
    headerText = headerText.replace("{{AssessmentMaxBeforeAfterMidTerm}}", beforeAfterMidTermMaxTotal);
    headerText = headerText.replace("{{AssessmentMaxBeforeAfterMidTerm}}", beforeAfterMidTermMaxTotal);  //Hidden Field

    $("#hdnBeforeAfterMidTermMaxScoreTotal").val(beforeAfterMidTermMaxTotal);
    $("#hdnBeforeMidTermMaxScoreTotal").val(response.GradeDTO.MaxBeforeMidTermTotal);
    $("#hdnAfterMidTermMaxScoreTotal").val(response.GradeDTO.MaxAfterMidTermTotal);

    var midTermMaxTotal = (response.GradeDTO.MaxMidTerm == null || response.GradeDTO.MaxMidTerm == "") ? 0 : response.GradeDTO.MaxMidTerm;
    headerText = headerText.replace("{{AssessmentMaxMidTerm}}", midTermMaxTotal);
    $("#hdnMidTermMaxScoreTotal").val(midTermMaxTotal);

    var finalTermMaxTotal = (response.GradeDTO.MaxFinalTerm == null || response.GradeDTO.MaxFinalTerm == "") ? 0 : response.GradeDTO.MaxFinalTerm;
    headerText = headerText.replace("{{AssessmentMaxFinalTerm}}", finalTermMaxTotal);
    $("#hdnFinalTermMaxScoreTotal").val(finalTermMaxTotal);


    var maxTotalScore = ConvertToFloat(beforeAfterMidTermMaxTotal) + ConvertToFloat(midTermMaxTotal) + ConvertToFloat(finalTermMaxTotal);

    if (maxTotalScore != '' && maxTotalScore != 'undefined' && maxTotalScore % 1 != 0) {
        maxTotalScore = maxTotalScore.toFixed(2);
    } 
        

    headerText = headerText.replace("{{AssessmentMaxTotalScore}}", maxTotalScore);
    headerText = headerText.replace("{{SlNOHeaderText}}", languageContent[0].SlNOHeaderText);
    headerText = headerText.replace("{{StudentCodeHeaderText}}", languageContent[0].StudentCodeHeaderText);
    headerText = headerText.replace("{{StudentNameHeaderText}}", languageContent[0].StudentNameHeaderText);
    headerText = headerText.replace("{{TestScoreHeaderText}}", languageContent[0].TestScoreHeaderText);
    headerText = headerText.replace("{{SumOfAllScoresHeaderText}}", languageContent[0].SumOfAllScoresHeaderText);
    headerText = headerText.replace("{{Total100PerHeaderText}}", languageContent[0].Total100PerHeaderText);
    headerText = headerText.replace("{{Term2Total100PerHeaderText}}", languageContent[0].Term2Total100PerHeaderText);

    headerText = headerText.replace("{{Total50PerHeaderText}}", languageContent[0].Total50PerHeaderText);
    headerText = headerText.replace("{{Term2Total50PerHeaderText}}", languageContent[0].Term2Total50PerHeaderText);
    headerText = headerText.replace("{{Term1AndTerm2ScoreTotalHeaderText}}", languageContent[0].Term1AndTerm2ScoreTotalHeaderText);

    
    //headerText = headerText.replace("{{Term2AndAverageHeaderText}}", Term2AndAverageHeaderText);
    //headerText = headerText.replace("{{Term2Total100PerHeaderText}}", languageContent[0].Term2Total100PerHeaderText);
    //headerText = headerText.replace("{{Term1AndTerm2ScoreTotalHeaderText}}", languageContent[0].Term1AndTerm2ScoreTotalHeaderText);
    headerText = headerText.replace("{{GradeHeaderText}}", languageContent[0].GradeHeaderText);
    headerText = headerText.replace("{{GradeHeaderText}}", languageContent[0].GradeHeaderText);
    headerText = headerText.replace("{{DesirableFeatureHeaderText}}", languageContent[0].DesirableFeatureHeaderText);
    headerText = headerText.replace("{{ReadWriteHeaderText}}", languageContent[0].ReadWriteHeaderText);
    headerText = headerText.replace("{{PerformanceScoreHeaderText}}", languageContent[0].PerformanceScoreHeaderText);
    headerText = headerText.replace("{{OtherAssessmentsHeaderText}}", languageContent[0].OtherAssessmentsHeaderText);
    headerText = headerText.replace("{{BeforeAfterMidTermHeaderText}}", languageContent[0].BeforeAfterMidTermHeaderText);
    headerText = headerText.replace("{{MidTermHeaderText}}", languageContent[0].MidTermHeaderText);
    headerText = headerText.replace("{{FinalHeaderText}}", languageContent[0].FinalHeaderText);
    headerText = headerText.replace("{{FullScoreHeaderText}}", languageContent[0].FullScoreHeaderText);
    headerText = headerText.replace("{{AssessmentMaxBehaviourTotalScore}}", languageContent[0].GetBehaviorTotal);


    return headerText;
}

function GetCoursesForImportGrade(assessmentTypeName) {

    $(".modal-body #AssessmentTypeId").val(assessmentTypeName);
   
    //if ($("#ddlCourseList > option").length == 0) {
        $("body").mLoading();
        var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/AssessmentScore/GetCoursesForImportGrade?nTermSubLevel2=" + nTermSubLevel2 + "&nTerm=" + nTerm + "&nTSubLevel=" + getUrlParameter("idlv") + "&sPlaneID="+ getUrlParameter("id");
        params['requestType'] = "GET";
        params['successCallbackFunction'] = BindCoursesForImportScore;
        doAjax(params);
        $("body").mLoading("hide");
    //}
}

var BindCoursesForImportScore = function BindCoursesForImportScore(courses) {
    //console.log(courses)

    $("#ddlCourseList").empty();
    $("#ddlCourseList option").remove();
    $("#ddlCourseList").append('<option value="-1">เลือกวิชา</option>');
    $(courses).each(function (index, value) {
        $("#ddlCourseList").append('<option value="' + value.SPlaneId + '">' + value.CourseName + '</option>')
    })

}

function ImportStudentCurricularMarks() {
   
    if ($("#ddlCourseList").val() != "-1") {
        var AssessmentTypeId = 0;

        if ($(".modal-body #AssessmentTypeId").val() == "GetBehaviorLabel") {
            AssessmentTypeId = 5;
        } else if ($(".modal-body #AssessmentTypeId").val() == "GetReadWrite") {
            AssessmentTypeId = 6;
        } else if ($(".modal-body #AssessmentTypeId").val() == "GetSamattana") {
            AssessmentTypeId = 7;
        }

        var commonRequest = {
            nGradeId: nGradeId, AssessmentTypeId: AssessmentTypeId, nTermSubLevel2: nTermSubLevel2, nTerm: nTerm,
            sPlaneID: $("#ddlCourseList").val(),
            IsNewAssessmentCreated: IsNewAssessmentCreated,
            IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
        };

        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/ImportStudentCurricularMarks";
        params['data'] = JSON.stringify(commonRequest);
        params['requestType'] = "POST";
        doAjax(params);
        $("body").mLoading();
        $('#TabBeforeMidTerm a').trigger('click');
        GetTableContent();
    }
}

//var bindImportedAssessmentsScore = function BindImportedAssessmentsScore(result) {

//      $(response.Assessments).each(function (index, value) {

//          if (value.AssessmentTypeDescription == $(".modal-body #AssessmentTypeId").val()) {

//            var assessments = $.grep(result, function (n) {
//                return n.AssessmentTypeDescription == value.AssessmentTypeDescription;
//            });
  
//            value.AssessmentName = assessments[0].AssessmentName;
//            value.AssessmentMaxScore = assessments[0].Score;
//        }
//    });
//}

function BindData(response) {
    //console.log(response);
    console.log("response.IsGradeDetailAvailable" + response.IsGradeDetailAvailable);
    IsGradeDetailAvailable = response.IsGradeDetailAvailable;
    IsUserAllowedToEditRatio = response.IsUserAllowedToEditRatio;
    nGradeId = response.NGradeId;
    console.log(nGradeId);
    console.log(response.NTerm);
    nTermSubLevel2 = response.nTermSubLevel2;
    nTerm = response.NTerm;
    //$("#ddlScorePercentage").val((response.FRatioQuiz == null) ? "0" : response.FRatioQuiz);
    $("#ddlBeforeMidTermPercentage").val((response.FRatioBeforeMidTerm == null) ? "0" : response.FRatioBeforeMidTerm);
    $("#ddlAfterMidTermPercentage").val((response.FRatioAfterMidTerm == null) ? "0" : response.FRatioAfterMidTerm);
    $("#ddlMidTermPercentage").val((response.FRatioMidTerm == null) ? "0" : response.FRatioMidTerm);
    $("#ddlFinalTermPercentage").val((response.FRatioLateTerm == null) ? "0" : response.FRatioLateTerm);
    $("#ddlPassingCutOffPercentage").val((response.FRatioQuizPass == null) ? "0" : response.FRatioQuizPass);
    $("#CourseName").html(response.CourseCode + " " + response.CourseName);
    $("#HomeRoomTeacher").html(response.HomeTeacherName);
    $("#SubmitPeriodText").html(response.SubmitPeriodText);
    $("#lblSubmitPeriodText").html((response.SubmitPeriodText != "") ? languageContent[0].lblSubmitPeriod : "");

    if (!IsUserAllowedToEditRatio) {
        $('#ddlBeforeMidTermPercentage').prop('disabled', true);
        $('#ddlAfterMidTermPercentage').prop('disabled', true);
        $('#ddlMidTermPercentage').prop('disabled', true);
        $('#ddlFinalTermPercentage').prop('disabled', true);
        $('#ddlPassingCutOffPercentage').prop('disabled', true);
    }

    GetCurrentDateTime(response.dUpdate);

    $("#Year").html(getUrlParameter("term") + "/" + getUrlParameter("year"));

    var teachersName = "";
    $.each(response.ClassTeacherDTOs, function (index, Value) {
        teachersName += (index + 1) + ". " + Value.TeacherFullName + ", ";
    });
    $("#ClassTeachersName").html(teachersName);
    $("#SubLevel").html((languageCode != "EN") ? response.SubLevel + " / " + response.nTSubLevel2 : response.SubLevelEN + " / " + response.nTSubLevel2);

    $("#chkGradeDicimal").prop("checked", (response.GradeDicimal == "1") ? true : false);
    if (LevelName != null && LevelName != "" && LevelName != "ปวช." && LevelName != "ปวส." && LevelName != "มัธยมศึกษา") {
        $("#chkShowFullScore100Percentage").prop("checked", (response.GradeShowFullScore == true) ? true : false);
    }
    else {
        $("#chkShowFullScore100Percentage").prop("checked", (response.GradeShowFullScore == true) ? true : false);
        $("#chkShowFullScore100Percentage").prop("disabled", true);
    }
    $("#chkGradeAutoReadScore").prop("checked", (response.GradeAutoReadScore == "1") ? true : false);
    $("#chkGradeAddCheewat").prop("checked", (response.GradeAddCheewat == "1") ? true : false);
    $("#chkGradeCloseBehaviorReadWrite").prop("checked", (response.GradeCloseBehaviorReadWrite == "1") ? true : false);
    $("#chkGradeAddBehavior").prop("checked", (response.GradeAddBehavior == "1") ? true : false);
    $("#chkGradeAddSamattana").prop("checked", (response.GradeAddSamattana == "1") ? true : false);
    $("#chkGradeAutoBehaviorScore").prop("checked", (response.GradeAutoBehaviorScore == "1") ? true : false);
    $("#chkOptionMid").prop("checked", (response.OptionMid == "1") ? true : false);
    $("#chkOptionFinal").prop("checked", (response.OptionFinal == "1") ? true : false);
    $("#chkCloseGradeTab").prop("checked", (response.GradeCloseGrade == "1") ? true : false);
    $("#AssessmentSharingToRoomList").hide();
    $("#AssessmentSharingSelectedRoomList").hide();
    $("#IsAssessmentSharedFromOtherRoom").hide();
    if (!response.IsAssessmentSharedFromOtherRoom) { // If assessment not shared from other rooms then show the room list for sharing assessment to other rooms
        $("#chkGradeShareData").prop("checked", (response.GradeShareData == "1") ? true : false);
        if (response.GradeShareData == "1") {
            $("#AssessmentSharingToRoomList").show();
            $("#AssessmentSharingSelectedRoomList").show();
            $("#lblAssessmentSharingTo").html(languageContent[0].lblAssessmentSharingTo);
            UpdateRoomListForSharingAssessment(response.RoomListForSharingAssessment);
            roomListForSharingAssessment = response.RoomListForSharingAssessment;
        }
    }
    else if (response.IsAssessmentSharedFromOtherRoom) { //Just show the labels if already assessment shared from other
        $("#IsAssessmentSharedFromOtherRoom").show();
        $("#lblAssessmentSharedFromOtherRoomHeader").html(languageContent[0].lblAssessmentSharedFromOtherRoomHeader);
        $("#lblAssessmentSharedFromOtherRoomName").html(response.AssessmentSharedFromOtherRoomName);
    }
    

    $("#chkGradeShareData").prop("disabled", response.IsAssessmentSharedFromOtherRoom);

    //Disable Sharing Assessment to Other rooms for previouse year
    if (!IsRequestForCurrentAcademicYear) {
        $("#chkGradeShareData").prop("disabled", false);
        $("#AssessmentSharingToRoomList").hide();
        $("#AssessmentSharingSelectedRoomList").hide();
        $("#IsAssessmentSharedFromOtherRoom").hide();
    }

    $("#chkGradeCloseSamattana").prop("checked", (response.GradeCloseSamattana == "1") ? true : false);

    $("#lblLastUpdated").html(languageContent[0].lblLastUpdated);
    $("#lblCourseName").html(languageContent[0].lblCourseName);
    $("#lblSubLevel").html(languageContent[0].lblSubLevel);
    $("#lblYearHeader").html(languageContent[0].lblYearHeader);
    $("#lblHomeRoomTeacher").html(languageContent[0].lblHomeRoomTeacher);
    $("#lblClassTeacher").html(languageContent[0].lblClassTeacher);
    
    $("#btnManage").html(languageContent[0].btnManage);

    //Tab
    $("#lblTabBeforeMidTerm").html(languageContent[0].lblTabBeforeMidTerm);
    $("#lblTabMidTerm").html(languageContent[0].lblTabMidTerm);
    $("#lblTabAfterMidTerm").html(languageContent[0].lblTabAfterMidTerm);
    $("#lblTabFinal").html(languageContent[0].lblTabFinal);
    $("#lblTabBehavior").html(languageContent[0].lblTabBehavior);
    $("#lblTabReadWrite").html(languageContent[0].lblTabReadWrite);
    $("#lblTabSamattana").html(languageContent[0].lblTabSamattana);
    $("#lnkSetting").html(languageContent[0].lnkSetting);
    $("#lnkImportFromExcel").html(languageContent[0].lnkImportFromExcel);
    $("#lnkPrint").html(languageContent[0].lnkPrint);
    $("#lnkLog").html(languageContent[0].lnkLog);
    $("#lblScoreRatioTitle").html(languageContent[0].lblScoreRatioTitle);
    $("#lblScorePercentage").html(languageContent[0].lblScorePercentage);
    $("#lblBeforeMidTermPercentage").html(languageContent[0].lblBeforeMidTermPercentage);
    $("#lblAfterMidTermPercentage").html(languageContent[0].lblAfterMidTermPercentage);
    $("#lblMidTermPercentage").html(languageContent[0].lblMidTermPercentage);
    $("#lblFinalTermPercentage").html(languageContent[0].lblFinalTermPercentage);
    $("#lblPassingCutOffPercentage").html(languageContent[0].lblPassingCutOffPercentage);
    $("#lblEditForm").html(languageContent[0].lblEditForm);
    $("#lblGradeDicimal").html(languageContent[0].lblGradeDicimal);
    $("#lblShowFullScore100Percentage").html(languageContent[0].lblShowFullScore100Percentage);
    
    $("#lblGradeAutoReadScore").html(languageContent[0].lblGradeAutoReadScore);
    $("#lblGradeAddBehavior").html(languageContent[0].lblGradeAddBehavior);
    $("#lblGradeAddSamattana").html(languageContent[0].lblGradeAddSamattana);
    $("#lblGradeAutoBehaviorScore").html(languageContent[0].lblGradeAutoBehaviorScore);
    $("#lblGradeAddCheewat").html(languageContent[0].lblGradeAddCheewat);
    $("#lblOptionMid").html(languageContent[0].lblOptionMid);
    $("#lblOptionFinal").html(languageContent[0].lblOptionFinal);
    $("#lblGradeCloseBehaviorReadWrite").html(languageContent[0].lblGradeCloseBehaviorReadWrite);
    $("#lblCloseGradeTab").html(languageContent[0].lblCloseGradeTab);
    $("#lblGradeCloseSamattana").html(languageContent[0].lblGradeCloseSamattana);
    $("#lblGradeShareData").html(languageContent[0].lblGradeShareData);

    $("#btnCancel").val(languageContent[0].btnCancel);
    $("#AssessmentNameDialogHeader").text(languageContent[0].AssessmentNameDialogHeader);
    $("#btnClose").text(languageContent[0].btnClose);
    $("#btnImportClose").text(languageContent[0].btnClose);
    $("#btnImportConfirm").text(languageContent[0].btnImportConfirm);
    $("#btnConfirmationDialogYes").html(languageContent[0].Yes);
    $("#btnConfirmationDialogClose").html(languageContent[0].btnClose);
    $("#Confirm .modal-title").html(languageContent[0].DeleteConfirmationDialogHeaderText);
    $("#OpenPrintLinks .modal-title").html(languageContent[0].lnkPrint);
    $("#btnFinalSubmit").val(languageContent[0].btnFinalSubmit);
    $("#btnDeleteSubmit").val(languageContent[0].btnDeleteSubmit);
    $("#btnDraftSave").val(languageContent[0].btnDraftSave);
    
}

function UpdateRoomListForSharingAssessment(roomList) {
    //$("#RoomListForSharingAssessment option").remove();
    $("#RoomListForSharingAssessmentMultiple option").remove();
    if (roomList != null && roomList != undefined) {
        $(roomList).each(function (index, value) {
            var selected = (value.IsGradeInfoSharedFromOther == true) ? "selected" : "";
            var disabled = (value.IsRoomHaveScore == true) ? "disabled" : "";
            //$("#RoomListForSharingAssessment").append('<option value="' + value.nTermSubLevel2 + '">' + value.RoomFullName + '</option>')
            var roomFullName = value.RoomFullName;
            if (disabled == "disabled"  && selected == "") {
                roomFullName = roomFullName + " -- (ไม่สามารถใช้งานร่วมได้ เนื่องจากทำการบันทึกคะแนนแล้ว)";
            }
            $("#RoomListForSharingAssessmentMultiple").append('<option value="' + value.nTermSubLevel2 + '" ' + selected + ' ' + disabled + '>' + roomFullName + '</option>')
        })
        $('#RoomListForSharingAssessmentMultiple').trigger("chosen:updated");
    }
}

function OnKeyDownAssessmentMaxScoreCheck(e, control, controlName, rightControlName, leftControlName) {
    var keyPressed = e.which;

    if (keyPressed == 39) {
        // Right Arrow
        var rightControl = $("#Max" + rightControlName)
        if (rightControl != undefined)
            rightControl.focus();
    } else if (keyPressed == 37) {
        // Left Arrow
        //console.log("Left Arrow");
        var leftControl = $("#Max" + leftControlName)
        if (leftControl != undefined)
            leftControl.focus();
    } 
}

function OnKeyDownCheck(e, control, controlName, rowNumber, rightControlName, leftControlName) {
    //console.log("OnKeyDownCheck");
    var keyPressed = 0;
    if (e === undefined) {
        keyPressed = $("#hdnAssessmentScoreBoxInputKey").val();
    }
    else {
        $("#hdnAssessmentScoreBoxInputKey").val(e.which);
        keyPressed = e.which;
    }
    var resignedStudent = $.grep(studentResignedRows, function (n) {
        return n < rowNumber;
    });

    if (resignedStudent.length > 0) {
        rowNumber = rowNumber - resignedStudent.length;
    }
    if (keyPressed == 39) {
        // Right Arrow
        var rightControl = $("input[name='" + rightControlName + "']")[parseInt(rowNumber - 1)];
        if (rightControl != undefined)
            rightControl.focus();
        //console.log("Right");
    } else if (keyPressed == 37) {
        // Left Arrow
        //console.log("Left Arrow");
        var leftControl = $("input[name='" + leftControlName + "']")[parseInt(rowNumber - 1)];
        if (leftControl != undefined)
            leftControl.focus();
    } else if (keyPressed == 38) {
        // Up Arrow
        var previouseRow = parseInt(rowNumber - 2);
        var upControl = $("input[name='" + controlName + "']")[previouseRow];
        if (upControl != undefined)
            upControl.focus();
    } else if (keyPressed == 40) {
        // Down Arrow
        var downControl = $("input[name='" + controlName + "']")[rowNumber];
        if (downControl != undefined)
            downControl.focus();
    }

}

function isNumberKey(e, control, assessmentTypeName) {
    var $this = $(control);
    var text = $(control).val();
    var controlName = $(control).attr('name');
    var keyPressed = 0;
   

    if (($(control).attr('name') != 'undefined' || $(control).attr('name') != null) && ((controlName == "GetSamattana" || controlName == "GetBehaviorLabel" || controlName == "GetReadWrite" || assessmentTypeName == "Characteristics") && event.which == 46)) {
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

function PageContentTranslate(lan) {
    languageCode = lan;

    languageContent = $.grep(languageContentSource, function (n) {
        return n.LanguageCode == languageCode;
    });

    if (visbleColumnList.length > 0)
        table.columns(visbleColumnList).visible(false);

    GetDefaultAssessmentColumnList();
    CreatePageContent(response, htmlTemplate);

    //$("body").mLoading();
    //$('#TabBeforeMidTerm a').trigger('click');
    //GetTableContent();
}

function OpenDeleteConfirmationDialog(columnName) {
    $("#hdnDeleteColumnName").val(columnName);

    $("#Confirm .modal-title").html(languageContent[0].DeleteConfirmationDialogHeaderText);
}
var DeleteBehaviourScores = function DeleteBehaviourScores(data) {
    console.log("DeleteBehaviourScores");
}

function ClearValue() {
    var columnName = $("#hdnDeleteColumnName").val();
    var columnIndex = [];
    if (columnName == "GetBehaviorLabel") {
        columnIndex.push(getBehaviorLabelColumnIndex);

        var behaviourTypeColumns = $.grep(toggleGroupColumns, function (n) {
            return n.AssessmentTypeName == "Characteristics";
        });

        $.each(behaviourTypeColumns[0].Columns, function (i, v) {
            columnIndex.push(v);
            console.log(response.column[v].data);
            $("#Max" + response.column[v].data).val("");
        });
       

        var request = {
            nGradeId: nGradeId,
            IsNewAssessmentCreated: IsNewAssessmentCreated,
            IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
        };

        var params = $.extend({}, doAjax_params_default);
        params['url'] = "../../api/AssessmentScore/DeleteBehaviourScores";
        params['data'] = JSON.stringify(request);
        params['requestType'] = "POST";
        doAjax(params);
        
    }
    if (columnName == "GetReadWrite") {
        columnIndex.push(columnIndex = getReadWriteColumnIndex);
    }
    if (columnName == "GetSamattana") {
        columnIndex.push(getSamattanaColumnIndex);
    }

    if (columnIndex.length > 0) {
        $.each(columnIndex, function (i, v) {
            table.column(v).data().each(function (value, index) {
               
                table.cell(index, v).data("").draw();
            });
        });
        UpdateStudentsGradeValueInDataTableAndDB();
    }
}

function GetSumOfMaxScore(type) {

    //var data_str = $("div#JsonAssessments").attr("data-json");
    var assessments = response.Assessments; //JSON.parse(decodeURIComponent(data_str));
    //if (type == "Before Mid Term") {
    //    assessments = $.grep(assessments, function (n) {
    //        return n.AssessmentTypeDescription == "Before Mid Term" || n.AssessmentTypeDescription == "After Mid Term";
    //    });
    //}
    //else {
        assessments = $.grep(assessments, function (n) {
            return n.AssessmentTypeDescription == type;
        });
    //}

    var maxScoreTotal = 0
    jQuery.each(assessments, function (i, val) {
        maxScoreTotal += (val.AssessmentMaxScore != "") ? parseFloat(val.AssessmentMaxScore) : 0;
    });

    if (maxScoreTotal != '' && maxScoreTotal != 'undefined' && maxScoreTotal % 1 != 0) {
        maxScoreTotal = maxScoreTotal.toFixed(2);
    }

    return maxScoreTotal;
}

function GetSumOfStudentScore(newScore, selectedColIndex, assessmentTypeName, rowNumber) {

    var assessmentColumns;
   
    assessmentColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == assessmentTypeName;
    });
   
    var totalScore = 0;
    var IsScoreEntered = "";
    $.each(assessmentColumns, function (index, Value) {
        $.each(Value.Columns, function (colIndex, colIndexValue) {
            if (selectedColIndex != colIndexValue) {
                var score = table.cell(rowNumber - 1, colIndexValue).data();
                if (score != "" && score != null) {
                    totalScore += parseFloat(score);
                    IsScoreEntered = "Yes";
                }
            }
            else {
                //Update the DataTable with new Value // TO-DO : Update in the response object
                table.cell(rowNumber - 1, colIndexValue).data(newScore).draw();
            }
        });
    });

    //Add New Value
    if (newScore != "") {
        IsScoreEntered = "Yes";
        totalScore += parseFloat(newScore);
    }

    if (IsScoreEntered == "") {
        totalScore = "";
    }

    totalScore = (isNaN(totalScore)) ? "" : totalScore;

    if (totalScore != '' && totalScore != 'undefined' && totalScore % 1 != 0) {
        totalScore = totalScore.toFixed(2);
    }
    return totalScore;
}

//Calculate the total for Verify Total 
function GetSumOfStudentScoreForCorrection(assessmentTypeName, rowIndex) {
    var assessmentColumns;

    assessmentColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == assessmentTypeName;
    });

    var totalScore = 0;
    $.each(assessmentColumns, function (index, Value) {
        $.each(Value.Columns, function (colIndex, colIndexValue) {
            var score = table.cell(rowIndex, colIndexValue).data();
            if (score != "" && score != null) {
                totalScore += parseFloat(score);
            }
        });
    });

    if (totalScore != '' && totalScore != 'undefined' && totalScore % 1 != 0) {
        totalScore = totalScore.toFixed(2);
    }
    return totalScore;
}

function CalculateBehaviourScore(newScore, selectedColIndex, assessmentTypeName, rowNumber) {
    var assessmentColumns = $.grep(toggleGroupColumns, function (n) {
        return n.AssessmentTypeName == assessmentTypeName;
    });

    var totalScore = 0;

    var behaviourMarks = [];
    $.each(assessmentColumns, function (index, Value) {
        $.each(Value.Columns, function (colIndex, colIndexValue) {
            if (selectedColIndex != colIndexValue) {
                var score = table.cell(rowNumber - 1, colIndexValue).data();
               
                var assessment = $.grep(response.Assessments, function (n) {
                    return n.AssessmentId == response.column[colIndexValue].data;
                });
                var maxScore = (assessment.length > 0) ? assessment[0].AssessmentMaxScore : "";

                if (maxScore != "" && maxScore != 'undefined') {
                    behaviourMarks.push((score != "" && score != null) ? parseFloat(score) : 0);
                }
            }
            else {
                //Update the DataTable with new Value // TO-DO : Update in the response object
                table.cell(rowNumber - 1, colIndexValue).data(newScore).draw();
            }
        });
    });

    //Add New Value
    if (newScore != "") {
        behaviourMarks.push(parseFloat(newScore));
    }


    // totalScore = CalculateBehaviourScore(behaviourMarks);
   
    if (behaviourMarks.length == 0)
        return null;

    var modeMap = {},
        maxEl = behaviourMarks[0],
        maxCount = 1;

    if (behaviourMarks.length > 1) {
        for (var i = 0; i < behaviourMarks.length; i++) {
            var el = behaviourMarks[i];
          
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
        maxEl = behaviourMarks[0];
    }
    totalScore = maxEl;
    //console.log(totalScore);
    if (totalScore == "4")
        totalScore = "0";

    totalScore = (isNaN(totalScore)) ? 0 : totalScore;

    if (totalScore != '' && totalScore != 'undefined' && totalScore % 1 != 0) {
        totalScore = totalScore.toFixed(2);
    }
    return totalScore;
}

//function RoomListForSharingAssessmentOnChange() {

//    //$("#txtSelectedRoomforSharing").val("");
//    $('#RoomListForSharingAssessmentMultiple :selected').each(function () {
//        if ($("#txtSelectedRoomforSharing").val() == "") {
//            $("#txtSelectedRoomforSharing").val($(this).text());
//        }
//        else {
//            $("#txtSelectedRoomforSharing").val($("#txtSelectedRoomforSharing").val() + "; " + $(this).text());
//        }
//    });
//}

function btnShareAssessmentOnClick() {
    //$("body").mLoading();
    var selectedRoom = [];

    var removeList = [];
    var newList = [];

    var oldSharedRoomValue = $.grep(roomListForSharingAssessment, function (n) {
        return (n.IsGradeInfoSharedFromOther == true || n.IsRoomHaveScore == true)
    });

    $('#RoomListForSharingAssessmentMultiple :selected').each(function () {
        selectedRoom.push($(this).val());
        var sharedRoom = parseInt($(this).val());
        var checkexist = $.grep(oldSharedRoomValue, function (n) {
            return n.nTermSubLevel2 == sharedRoom
        });
        if (checkexist.length == 0) {
            newList.push(sharedRoom);
        }
    });

    $(oldSharedRoomValue).each(function (index,value) {
        var sharedRoom = value.nTermSubLevel2;
        var sharedRoomnGradeId = value.nGradeId;
        var checkexist = $.grep(selectedRoom, function (n) {
            return n == sharedRoom
        });
        if (checkexist.length == 0) {
            removeList.push({ nTermSubLevel2: sharedRoom, nGradeId: sharedRoomnGradeId });
        }
    });
   
    if (newList.length == 0 && removeList.length == 0)  {
        $("body").mLoading('hide');
        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>กรุณาเลือกห้อง</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',

                },
            },
        });
    }
    else {
        $("body").mLoading('hide');
        $.confirm({
            title: '<h2>คำเตือน !</h>',
            content: '<h2>โปรดยืนยันสำหรับการประเมินร่วมกัน</h>',
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> ยกเลิก',
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> หุ้น',
                    action: function () {
                        console.log("Action Clicked");
                        $("body").mLoading();
                        var commonRequest = {
                            nGradeId: nGradeId, nTSubLevel: getUrlParameter("idlv"), sPlaneID: getUrlParameter("id"), nTermSubLevel2: getUrlParameter("idlv2"), sTerm: getUrlParameter("term"), NumberYear: getUrlParameter("year"), NTerm: nTerm, SelectedRoomsForShareGradeInfo: newList, RevertBackGradeSharedInfo: removeList, IsNewAssessmentCreated: IsNewAssessmentCreated,
                            IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear};

                        $.ajax({
                            ContentType: "application/json; charset=utf-8",
                            url: "../../api/AssessmentScore/ShareAssessmentToOtherRooms",
                            type: "Post",
                            data: commonRequest,
                            success: function (roomList) {
                                $("body").mLoading('hide');
                                $.confirm({
                                    title: false,
                                    content: '<img src="../../images/checked.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">แบ่งปันสำเร็จ</h1>',
                                    theme: 'material',
                                    type: 'blue',
                                    buttons: {
                                        "<span style=\"font-size: 20px;\">ปิด</span>": {
                                            btnClass: 'btn-primary',
                                            keys: ['enter', 'shift'],
                                            action: function () {
                                                GetRoomListForSharingAssessments();
                                            }
                                        }
                                    }
                                });
                            }
                        });
                    }
                }
            },
        });
    }
}

function btnFinalSubmitOnClick() {
    $.confirm({
        title: '<h2>คำเตือน !</h>',
        content: '<h2>เมื่อกดยืนยันหลังจากการบันทึกครั้งสุดท้ายแล้วท่านจะไม่สามารถแก้ไขคะแนนในช่องที่กรอกไปแล้วได้อีก</h>',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> ยกเลิก',
               
            },
            confirm: {
                label: '<i class="fa fa-check"></i> บันทึก',
                action: function () {
                    console.log("Action Clicked");
                    var shouldBeLocked = [];
                    table.columns(assessmentColumn).data().each(function (value, index) {
                        var checkValueNotEmpty = $.grep(value, function (n) {
                            return n != "" && n != null;
                        });
                        if (checkValueNotEmpty.length > 0) {
                            shouldBeLocked.push(response.column[assessmentColumn[index]].data);
                        }
                    });

                    var gradeLockList = {
                        nGradeId: nGradeId, AssessmentIds: shouldBeLocked, SubmitPeriod: "-1",
                        sPlaneID: getUrlParameter("id"), nTermSubLevel2: getUrlParameter("idlv2"), sTerm: getUrlParameter("term"), NumberYear: getUrlParameter("year"),
                        IsNewAssessmentCreated: IsNewAssessmentCreated,
                        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
                    };

                    $.ajax({
                        url: "../../api/AssessmentScore/LockStudentScore",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify(gradeLockList),
                        success: function (response) {
                            CreateLog("", "", "FinalSubmit", "");
                            RedirectToMainPage();
                           
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            ShowPageError();
                        }
                    });

                    //var params = $.extend({}, doAjax_params_default);
                    //params['url'] = "../../api/AssessmentScore/LockStudentScore";
                    //params['data'] = JSON.stringify(gradeLockList);
                    //params['requestType'] = "POST";
                    //doAjax(params);

                   
                   
                }
            }
        },
    });

}

function btnDeleteSubmitOnClick() {
    $.confirm({
        title: '<h2>คำเตือน !</h>',
        content: '<h2>หากกดยืนยัน คะแนนจะถูกลบและไม่สามารถเรียกคืนข้อมูลได้ โปรดตรวจสอบ ก่อนกดยืนยัน</h>',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> ยกเลิก',

            },
            confirm: {
                label: '<i class="fa fa-check"></i> บันทึก',
                action: function () {
                    console.log("Action Clicked");
                    console.log(response.GradeDTO.GradeRole);

                    var commonRequest = {
                        nGradeId: nGradeId, NumberYear: getUrlParameter("year"), IsNewAssessmentCreated: IsNewAssessmentCreated,
                        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear };

                    $.ajax({
                        url: "../../api/AssessmentScore/DeleteStudentScore",
                        type: "POST",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        data: JSON.stringify(commonRequest),
                        success: function (response) {
                           
                            CreateLog("", "", "DeleteSubmit", "");
                            RedirectToMainPage();

                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                           
                            ShowPageError();
                        }
                    });
                }
            }
        },
    });

}

function GradeDetailsImportFromExcel(evt) {
    console.log("handleFileSelect");

    $.confirm({
        title: '<h2>คำเตือน !</h>',
        content: '<h2>หากมีคะเเนนเดิมอยู่เเล้ว คะเเนนใน excel จะไปทับช่องคะเเนนเดิมดังนั้น ตรวจสอบในเเน่ใจก่อนทำการอัปโหลด</h>',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> ยกเลิก',
            },
            confirm: {
                label: '<i class="fa fa-check"></i> บันทึก',
                action: function () {
                    console.log("Action Clicked");
                    var files = evt.target.files; // FileList object
                    if (files.length > 0) {
                        IsImportFromExcel = true;
                        $("body").mLoading();
                        var xl2json = new ExcelToJSON();
                        xl2json.parseExcel(files[0]);
                    }
                }
            }
        },
    });
   
}

function ReloadDataTable() {
    $("body").mLoading();
    $('#TabBeforeMidTerm a').trigger('click');
    //UpdateStudentsGradeValueInDataTableAndDB();
    //("body").mLoading('hide');
    visbleColumnList = [];
    GetTableContent();
    //UpdateStudentsGradeValueInDataTableAndDB();
}

var ExcelToJSON = function () {

    this.parseExcel = function (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });
            var check = 0;
            var gradeImportFromExcelRequest;
            var gradeDetailsImportFromExcelRequests = [];
            var isvalidRatio = true;
            workbook.SheetNames.forEach(function (sheetName) {
                // Here is your object
                //alert(sheetName);
                var XL_row_object = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheetName]);
                var json_object = JSON.stringify(XL_row_object);
                //console.log("json_object" + json_object);
                var data = JSON.parse(json_object);

                //Get Not Locked Assessment
                var assessments = $.grep(response.Assessments, function (n) {
                    return n.IsLocked == false;
                });
                
                if (sheetName == "สำหรับdev1") {
                    check = 1;

                    var assessmentMaxScore = {};
                    var assessmentNames = {};
                    var column = "maxGrade" + 1;
                    //console.log("data[0].maxGrade1" + data[0]["maxGrade1"]);

                    for (var start = 1; start <= 20; start++) {

                        //Don't import the data if assessment is already locked
                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxGrade" + start;
                        });

                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxGrade" + start] = (data[0]["maxGrade" + start] != 'undefined' && $.isNumeric(data[0]["maxGrade" + start])) ? data[0]["maxGrade" + start] : "";
                            assessmentNames["nameGrade" + start] = (data[0]["nameGrade" + start] != 'undefined' && assessmentMaxScore["maxGrade" + start] != "") ? data[0]["nameGrade" + start] : "";
                        }

                        assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxCheewat" + start;
                        });


                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxCheewat" + start] = (data[0]["maxCheewat" + start] != 'undefined' && $.isNumeric(data[0]["maxCheewat" + start])) ? data[0]["maxCheewat" + start] : "";
                            assessmentNames["nameCheewat" + start] = (data[0]["nameCheewat" + start] != 'undefined' && assessmentMaxScore["maxCheewat" + start] != "") ? data[0]["nameCheewat" + start] : "";
                        }
                    }

                    for (var start = 1; start <= 10; start++) {

                        //Don't import the data if assessment is already locked
                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxMid" + start;
                        });

                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxMid" + start] = (data[0]["maxMid" + start] != 'undefined' && $.isNumeric(data[0]["maxMid" + start])) ? data[0]["maxMid" + start] : "";
                            assessmentNames["nameMid" + start] = (data[0]["nameMid" + start] != 'undefined' && assessmentMaxScore["maxMid" + start] != "") ? data[0]["nameMid" + start] : "";
                        }

                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxFinal" + start;
                        });
                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxFinal" + start] = (data[0]["maxFinal" + start] != 'undefined' && $.isNumeric(data[0]["maxFinal" + start])) ? data[0]["maxFinal" + start] : "";
                            assessmentNames["nameFinal" + start] = (data[0]["nameFinal" + start] != 'undefined' && assessmentMaxScore["maxFinal" + start] != "") ? data[0]["nameFinal" + start] : "";
                        }

                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxBehavior" + start;
                        });

                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxBehavior" + start] = (data[0]["maxBehavior" + start] != 'undefined' && $.isNumeric(data[0]["maxBehavior" + start])) ? data[0]["maxBehavior" + start] : "";
                            assessmentNames["nameBehavior" + start] = (data[0]["nameBehavior" + start] != 'undefined' && assessmentMaxScore["maxBehavior" + start] != "") ? data[0]["nameBehavior" + start] : "";
                        }

                    }

                    for (var start = 1; start <= 5; start++) {
                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxReadWrite" + start;
                        });

                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxReadWrite" + start] = (data[0]["maxReadWrite" + start] != 'undefined' && $.isNumeric(data[0]["maxReadWrite" + start])) ? data[0]["maxReadWrite" + start] : "";
                            assessmentNames["nameReadWrite" + start] = (data[0]["nameReadWrite" + start] != 'undefined' && assessmentMaxScore["maxReadWrite" + start] != "") ? data[0]["nameReadWrite" + start] : "";
                        }

                        var assessment = $.grep(assessments, function (n) {
                            return n.MaxScoreIdentifier == "maxSamattana" + start;
                        });

                        if (assessment != undefined && assessment != null && assessment.length > 0) {
                            assessmentMaxScore["maxSamattana" + start] = (data[0]["maxSamattana" + start] != 'undefined' && $.isNumeric(data[0]["maxSamattana" + start])) ? data[0]["maxSamattana" + start] : "";
                            assessmentNames["nameSamattana" + start] = (data[0]["nameSamattana" + start] != 'undefined' && assessmentMaxScore["maxSamattana" + start] != "") ? data[0]["nameSamattana" + start] : "";
                        }
                    }

                    var fRatioBeforeMidTerm = (data[0].fRatioBeforeMidTerm % 5 == 0 && data[0].fRatioBeforeMidTerm != 'undefined' && data[0].fRatioBeforeMidTerm != "") ? data[0].fRatioBeforeMidTerm : "-1";
                    var fRatioAfterMidTerm = (data[0].fRatioAfterMidTerm % 5 == 0 && data[0].fRatioAfterMidTerm != 'undefined' && data[0].fRatioAfterMidTerm != "") ? data[0].fRatioAfterMidTerm : "-1";
                    var fRatioQuiz = 0;
                    if (fRatioBeforeMidTerm != "-1" && fRatioAfterMidTerm != "-1") {
                        fRatioQuiz = Number(fRatioBeforeMidTerm) + Number(fRatioAfterMidTerm);
                    }
                    else if (fRatioBeforeMidTerm != "-1" && fRatioAfterMidTerm == "-1") {
                        fRatioQuiz = Number(fRatioBeforeMidTerm);
                    }
                    var fRatioMidTermExcel = (data[0].fRatioMidTerm % 5 == 0 && data[0].fRatioMidTerm != 'undefined' && data[0].fRatioMidTerm != "") ? data[0].fRatioMidTerm : "-1";
                    var fRatioLateTermExcel = (data[0].fRatioLateTerm % 5 == 0 && data[0].fRatioLateTerm != 'undefined' && data[0].fRatioLateTerm != "") ? data[0].fRatioLateTerm : "-1";
                    if ($("#ddlBeforeMidTermPercentage").val() == "-1" && $("#ddlAfterMidTermPercentage").val() == "-1" && $("#ddlMidTermPercentage").val() == "-1" && $("#ddlFinalTermPercentage").val() == "-1") {
                        isvalidRatio = false;
                    }
                    else if ($("#ddlBeforeMidTermPercentage").val() != fRatioBeforeMidTerm && $("#ddlBeforeMidTermPercentage").val() != "-1") {
                        isvalidRatio = false;
                    }
                    else if ($("#ddlAfterMidTermPercentage").val() != fRatioAfterMidTerm && $("#ddlAfterMidTermPercentage").val() != "-1") {
                        isvalidRatio = false;
                    }
                    else if ($("#ddlMidTermPercentage").val() != fRatioMidTermExcel && $("#ddlMidTermPercentage").val() != "-1") {
                        isvalidRatio = false;
                    }
                    else if ($("#ddlFinalTermPercentage").val() != fRatioLateTermExcel && $("#ddlFinalTermPercentage").val() != "-1") {
                        isvalidRatio = false;
                    }
                    

                    //console.log($("#ddlBeforeMidTermPercentage").val());
                    //console.log(fRatioBeforeMidTerm);
                    //console.log($("#ddlAfterMidTermPercentage").val());
                    //console.log(fRatioAfterMidTerm);
                    //console.log($("#ddlMidTermPercentage").val());
                    //console.log(fRatioMidTermExcel);
                    //console.log($("#ddlFinalTermPercentage").val());
                    //console.log(fRatioLateTermExcel);
                    //console.log(isvalidRatio);
                    console.log("IsRequestForCurrentAcademicYear" + IsRequestForCurrentAcademicYear);
                    gradeImportFromExcelRequest = {
                        NGradeId: nGradeId,
                        //FRatioQuiz: (data[0].fRatioQuiz % 5 == 0 && data[0].fRatioQuiz != 'undefined' && data[0].fRatioQuiz != "") ? data[0].fRatioQuiz : fRatioQuiz,
                        //FRatioBeforeMidTerm: (data[0].fRatioBeforeMidTerm % 5 == 0 && data[0].fRatioBeforeMidTerm != 'undefined' && data[0].fRatioBeforeMidTerm != "") ? data[0].fRatioBeforeMidTerm : $("#ddlBeforeMidTermPercentage").val(),
                        //FRatioAfterMidTerm: (data[0].fRatioAfterMidTerm % 5 == 0 && data[0].fRatioAfterMidTerm != 'undefined' && data[0].fRatioAfterMidTerm != "") ? data[0].fRatioAfterMidTerm : $("#ddlAfterMidTermPercentage").val(),
                        //FRatioMidTerm: (data[0].fRatioMidTerm % 5 == 0 && data[0].fRatioMidTerm != 'undefined' && data[0].fRatioMidTerm != "") ? data[0].fRatioMidTerm : $("#ddlMidTermPercentage").val(),
                        //FRatioLateTerm: (data[0].fRatioLateTerm % 5 == 0 && data[0].fRatioLateTerm != 'undefined' && data[0].fRatioLateTerm != "") ? data[0].fRatioLateTerm : $("#ddlFinalTermPercentage").val(),
                        //FRatioQuizPass: (data[0].fRatioQuizPass % 5 == 0 && data[0].fRatioQuizPass != 'undefined' && data[0].fRatioQuizPass != "") ? data[0].fRatioQuizPass : $("#ddlPassingCutOffPercentage").val(),

                        MaxMidTerm: (data[0].maxmidtotal != 'undefined') ? data[0].maxmidtotal : "",
                        MaxFinalTerm: (data[0].maxfinaltotal != 'undefined') ? data[0].maxfinaltotal : "",

                        MaxGradeTotal: data[0].maxgradetotal,

                       
                        MaxBeforeMidTermTotal: (data[0].maxBeforeMidTermTotal != null && data[0].maxBeforeMidTermTotal != 'undefined')? data[0].maxBeforeMidTermTotal : null,
                        MaxAfterMidTermTotal: (data[0].maxAfterMidTermTotal != null && data[0].maxAfterMidTermTotal != 'undefined') ? data[0].maxAfterMidTermTotal : null, 
                        //MaxBehaviorTotal: "",

                        //GradeDicimal: $("#chkGradeDicimal").is(':checked') ? "1" : "0",
                        //GradeAutoReadScore: $("#chkGradeAutoReadScore").is(':checked') ? "1" : "0",
                        //GradeCloseBehaviorReadWrite: $("#chkGradeCloseBehaviorReadWrite").is(':checked') ? "1" : "0",
                        //GradeAddCheewat: $("#chkGradeAddCheewat").is(':checked') ? "1" : "0",
                        //GradeAddBehavior: $("#chkGradeAddBehavior").is(':checked') ? "1" : "0",
                        //GradeAutoBehaviorScore: $("#chkGradeAutoBehaviorScore").is(':checked') ? "1" : "0",
                        //OptionMid: $("#chkOptionMid").is(':checked') ? "1" : "0",
                        //OptionFinal: $("#chkOptionFinal").is(':checked') ? "1" : "0",
                        //GradeCloseGrade: $("#chkCloseGradeTab").is(':checked') ? "1" : "0",
                        //GradeShareData: $("#chkGradeShareData").is(':checked') ? "1" : "0",
                        //GradeCloseSamattana: $("#chkGradeCloseSamattana").is(':checked') ? "1" : "0",

                        AssessmentMaxScore: assessmentMaxScore,
                        AssessmentNames: assessmentNames,
                        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
                    };
                }
                if (check > 0 && sheetName == "สำหรับdev2") {
                    var message1 = '<h2><p class="centertext">';
                    var count = 0;
                    var notfound = 0;
                    var midcount = 0;
                    var finalcount = 0;
                    var behavecount = 0;
                    var cwcount = 0;
                    var readcount = 0;

                    var idlist = [];
                    //Get Student List
                    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                        var data = this.data();
                        idlist.push({ sStudentId: data["sStudentId"], sID: data["sID"], nStudentStatus: data["nStudentStatus"], rowIndex : rowIdx  });
                    });
                    var IsValidScore = true;
                    for (var x = 0; x < data.length; x++) {
                        if (data[x].stdSID != 0) {
                            IsValidScore = true;
                            var sStudentId = data[x].stdSID;

                            //Check Valid Student or Not
                            var IsMatching = $.grep(idlist, function (n) {
                                return n.sStudentId == sStudentId && n.nStudentStatus != 1 && n.nStudentStatus != 2 && n.nStudentStatus != 3 && n.nStudentStatus != 5 && n.nStudentStatus != 6 && n.nStudentStatus != 7;
                            });
                            if (IsMatching.length > 0) {
                                var tStudentAssessmentScores = [];
                                console.log("Starting");

                                //Assessment Should not be locked
                                //Score Should not be more than Assessment Max Score
                                //Score Should not be empty
                                for (var start = 1; start <= 20; start++) {


                                    //Before MidTerm Score
                                    var assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxGrade" + start;
                                    });
                                    var score = 0;
                                    var maxScore = 0;
                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreGrade" + start] != null && data[x]["scoreGrade" + start] != 'undefined' && data[x]["scoreGrade" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreGrade" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxGrade" + start]);
                                        console.log("scoreGrade" + start);
                                        console.log(score);
                                        console.log(maxScore);
                                        if (IsValidScore && score > maxScore) {
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreGrade" + start, data[x]["scoreGrade" + start], IsMatching[0].sID));
                                        }
                                    }

                                    //After Mid Term Score
                                    assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxCheewat" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreCheewat" + start] != null && data[x]["scoreCheewat" + start] != 'undefined' && data[x]["scoreCheewat" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreCheewat" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxCheewat" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreCheewat" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreCheewat" + start, data[x]["scoreCheewat" + start], IsMatching[0].sID));
                                            //studentScore["scoreCheewat" + start] = data[x]["scoreCheewat" + start];
                                            if (Number(data[x]["scoreCheewat" + start]) > 0) cwcount++;
                                        }
                                    }
                                   

                                }

                                //Assessment Should not be locked
                                //Score Should not be more than Assessment Max Score
                                //Score Should not be empty
                                for (var start = 1; start <= 10; start++) {

                                    //Character Score
                                    var assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxBehavior" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreBehavior" + start] != null && data[x]["scoreBehavior" + start] != 'undefined' && data[x]["scoreBehavior" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreBehavior" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxBehavior" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreBehavior" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreBehavior" + start, data[x]["scoreBehavior" + start], IsMatching[0].sID));
                                            //studentScore["scoreBehavior" + start] = data[x]["scoreBehavior" + start];
                                            behavecount++;
                                        }
                                    }

                                    //Mid Term Score
                                    assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxMid" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreMid" + start] != null && data[x]["scoreMid" + start] != 'undefined' && data[x]["scoreMid" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreMid" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxMid" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreMid" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreMid" + start, data[x]["scoreMid" + start], IsMatching[0].sID));
                                            //studentScore["scoreMid" + start] = data[x]["scoreMid" + start];
                                            midcount++;
                                        }
                                    }

                                    //Final Term Score
                                    assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxFinal" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreFinal" + start] != null && data[x]["scoreFinal" + start] != 'undefined' && data[x]["scoreFinal" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreFinal" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxFinal" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreFinal" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreFinal" + start, data[x]["scoreFinal" + start], IsMatching[0].sID));
                                            //studentScore["scoreFinal" + start] = data[x]["scoreFinal" + start];
                                            finalcount++;
                                        }
                                    }
                                }

                                //Read & Write
                                //Samattana
                                for (var start = 1; start <= 10; start++) {
                                    //Read & Write
                                    var assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxReadWrite" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreReadWrite" + start] != null && data[x]["scoreReadWrite" + start] != 'undefined' && data[x]["scoreReadWrite" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreReadWrite" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxReadWrite" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreReadWrite" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreReadWrite" + start, data[x]["scoreReadWrite" + start], IsMatching[0].sID));
                                            //studentScore["scoreBehavior" + start] = data[x]["scoreBehavior" + start];
                                            behavecount++;
                                        }
                                    }

                                    //Samattana
                                    var assessment = $.grep(assessments, function (n) {
                                        return n.MaxScoreIdentifier == "maxSamattana" + start;
                                    });

                                    if (assessment != undefined && assessment != null && assessment.length > 0 && data[x]["scoreSamattana" + start] != null && data[x]["scoreSamattana" + start] != 'undefined' && data[x]["scoreSamattana" + start] != "") {

                                        score = ConvertToFloat(data[x]["scoreSamattana" + start]);
                                        maxScore = ConvertToFloat(gradeImportFromExcelRequest.AssessmentMaxScore["maxSamattana" + start]);

                                        if (IsValidScore && score > maxScore) {
                                            console.log("scoreSamattana" + start);
                                            console.log(score);
                                            console.log(maxScore);
                                            IsValidScore = false;
                                        }
                                        else {
                                            tStudentAssessmentScores.push(CreateStudentAssessmentScore(assessment, "scoreSamattana" + start, data[x]["scoreSamattana" + start], IsMatching[0].sID));
                                            //studentScore["scoreBehavior" + start] = data[x]["scoreBehavior" + start];
                                            behavecount++;
                                        }
                                    }
                                }
                                var readWrite;
                                if (data[x].getReadWrite != null) { readWrite = data[x].getReadWrite; readcount++; } else readWrite = "";

                                if (IsValidScore) {
                                    count = count + 1;
                                    var gradeDetailsImportFromExcelRequest = {
                                        NGradeId: nGradeId,
                                        sID: IsMatching[0].sID,
                                        StudentAssessmentScores: tStudentAssessmentScores,
                                        //StudentScore: studentScore,
                                        scoreMidTerm: (data[x].scoreMidTermSUM != null && data[x].scoreMidTermSUM != 'undefined' && data[x].scoreMidTermSUM != "") ? data[x].scoreMidTermSUM : "",
                                        scoreFinalTerm: (data[x].scoreFinalTermSUM != null && data[x].scoreFinalTermSUM != 'undefined' && data[x].scoreFinalTermSUM != "") ? data[x].scoreFinalTermSUM : "",
                                        getSamattana: (data[x].scoreSamatana != null && data[x].scoreSamatana != 'undefined') ? data[x].scoreSamatana : "",
                                        getReadWrite: readWrite,
                                        getBehaviorLabel: (data[x].scoreBahaviorSUM != null && data[x].scoreBahaviorSUM != 'undefined') ? data[x].scoreBahaviorSUM : "",
                                        IsRequestForCurrentAcademicYear: IsRequestForCurrentAcademicYear
                                    };
                                    console.log(gradeDetailsImportFromExcelRequest);
                                    gradeDetailsImportFromExcelRequests.push(gradeDetailsImportFromExcelRequest);
                                    //console.log("gradeDetailsImportFromExcelRequest" + JSON.stringify(gradeDetailsImportFromExcelRequest));
                                }
                                else {
                                   
                                    //console.log(tStudentAssessmentScores);
                                    message1 = message1 + sStudentId + ' คะแนนไม่ถูกต้อง <br>';
                                    notfound = notfound + 1;
                                }
                            }
                            else {
                                message1 = message1 + sStudentId + '<br>';
                                notfound = notfound + 1;
                            }
                        }
                    }

                    if (readcount > 0)
                        gradeImportFromExcelRequest.GradeAutoReadScore = 1;
                    if (cwcount > 0)
                        gradeImportFromExcelRequest.GradeAddCheewat = 1;
                    if (midcount > 0)
                        gradeImportFromExcelRequest.OptionMid = 1;
                    if (finalcount > 0)
                        gradeImportFromExcelRequest.OptionFinal = 1;

                    if (behavecount > 0) {
                        gradeImportFromExcelRequest.GradeAddBehavior = 1;
                        gradeImportFromExcelRequest.GradeAutoBehaviorScore = 1;
                    }

                    //gradeImportFromExcelRequest.IsRequestForCurrentAcademicYear = IsRequestForCurrentAcademicYear;
                    console.log("IsRequestForCurrentAcademicYear" + IsRequestForCurrentAcademicYear);
                    message1 = message1 + "</p></h>";
                    var error1 = '<h2><p class="centertext">ไม่พบรหัสนักเรียนต่อไปนี้ในระบบ</p></h>';
                    var done1 = '<h2><p class="centertext">นำเข้าคะแนนเสร็จสิ้น<br>จำนวนนักเรียนที่นำเข้าคะแนนสำเร็จ : ' + count + ' คน </p>';
                    if (notfound == 0)
                        error1 = "";
                    if (count == 0)
                        done1 = "<h2>";
                    console.log(isvalidRatio);
                    if (isvalidRatio) {
                       
                        setTimeout(function () {
                            setTimeout(function () {
                                //$("body").mLoading("hide"); // Remove this (Temp)

                                CreateLog("", "", "นำเข้าจาก Excel", "");
                                console.log("gradeImportFromExcelRequest");
                                console.log(gradeImportFromExcelRequest);
                                console.log(gradeDetailsImportFromExcelRequests);
                                //console.log(JSON.stringify(gradeDetailsImportFromExcelRequests));

                                $.ajax({
                                    url: "../../api/AssessmentScore/GradeDetailsImportFromExcel",
                                    crossDomain: true,
                                    type: "POST",
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    data: JSON.stringify(gradeDetailsImportFromExcelRequests),
                                    success: function (response) {
                                        console.log("Success");
                                        $.ajax({
                                            url: "../../api/AssessmentScore/GradeImportFromExcel",
                                            crossDomain: true,
                                            type: "POST",
                                            contentType: 'application/json; charset=utf-8',
                                            dataType: 'json',
                                            data: JSON.stringify(gradeImportFromExcelRequest),
                                            success: function (response1) {
                                                console.log("Success1");
                                                $("body").mLoading("hide");

                                                $.confirm({
                                                    title: false,
                                                    content: done1 + error1 + message1,
                                                    theme: 'material',
                                                    type: 'blue',
                                                    buttons: {
                                                        "<span style=\"font-size: 20px;\">ปิด</span>": {
                                                            btnClass: 'btn-primary',
                                                            keys: ['enter', 'shift'],
                                                            action: function () {
                                                                if (count > 0) {
                                                                    console.log("Button Close Clicked"); ReloadDataTable();
                                                                }
                                                            }
                                                        }
                                                    }
                                                });
                                            }
                                        });


                                    }
                                });

                            }, 1000);
                        }, 2000);
                    }
                    else {  //Invalid Ratio in the Excel
                        $("body").mLoading("hide");
                        $.confirm({
                            title: false,
                            content: "Excel มีการตั้งค่าสัดส่วนคะแนนไม่ตรงกับในระบบหรือมีค่าว่าง กรุณาตรวจสอบไฟล์ Excel และลองใหม่อีกครั้ง",
                            theme: 'material',
                            type: 'blue',
                            buttons: {
                                "<span style=\"font-size: 20px;\">ปิด</span>": {
                                    btnClass: 'btn-primary',
                                    keys: ['enter', 'shift'],
                                    action: function () {
                                        //if (count > 0) {
                                        //    console.log("Button Close Clicked"); ReloadDataTable();
                                        //}
                                    }
                                }
                            }
                        });
                    }
                }
            });
            
            if (check == 0) { //Alert for Incorrect Excel Form

                $.confirm({
                    title: false,
                    content: '<h2><p class="centertext">กรุณาใช้แบบฟอร์มที่กำหนดให้เท่านั้น</p></h>',
                    theme: 'material',
                    type: 'blue',
                    buttons: {
                        "<span style=\"font-size: 20px;\">ปิด</span>": {
                            btnClass: 'btn-primary',
                            keys: ['enter', 'shift'],
                            action: function () {
                                //console.log("Button Close Clicked"); ReloadDataTable();
                            }
                        }
                    }
                });
            }
        };


        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    };
};

function CreateStudentAssessmentScore(assessment, scoreIdentifier, score, sId) {
   

    var tstudentAssessmentScore = {

        NYear: assessment[0].NYear,
        NTerm: assessment[0].NTerm,
        IsActive: true,
        NGradeId: assessment[0].NGradeId,
        Score: score,
        ScoreIdentifier: scoreIdentifier,
        SchoolId: assessment[0].SchoolId,
        NTermSubLevel2: assessment[0].NTermSubLevel2,
        NTSubLevel: assessment[0].NTSubLevel,
        SId: sId,
        SPlaneId: assessment[0].SPlaneId,
        AssessmentId: assessment[0].AssessmentId,
    };
    return tstudentAssessmentScore;
}

function ConvertToFloat(value) {
    return (value == "" || value == null || value == 'undefined') ? 0 : parseFloat(value);
}

function print(id) {

    var full = window.location.href;
    var half = full.split('?');
    var urlPath = "";
    if (!IsNewAssessmentCreated && !IsRequestForCurrentAcademicYear) {
        urlPath = window.location.origin + "/grade/AssessmentScoreHistoryExport.aspx?"
    }
    else {
        urlPath = window.location.origin + "/grade/AssessmentScoreExport.aspx?"
    }


    var loc = "";
    //$("body").mLoading();
    if (id == "1") {
        loc = urlPath + half[1] + "&print=1";
        document.getElementById('list1').src = loc;
    }
    if (id == "2") {
        loc = urlPath + half[1] + "&print=2";
        document.getElementById('list2').src = loc;
    }
    if (id == "3") {
        loc = urlPath + half[1] + "&print=3";
        document.getElementById('list3').src = loc;
    }
    if (id == "4") {
        loc = urlPath + half[1] + "&print=4";
        document.getElementById('list4').src = loc;
    }
    if (id == "5") {
        loc = urlPath + half[1] + "&print=5";
        document.getElementById('list5').src = loc;
    }
    if (id == "6") {
        loc = urlPath + half[1] + "&print=6";
        document.getElementById('list6').src = loc;
    }
    if (id == "7") {
        loc = urlPath + half[1] + "&print=7";
        document.getElementById('list7').src = loc;
    }
    if (id == "8") {
        loc = urlPath + half[1] + "&print=8";
        document.getElementById('list8').src = loc;
    }
    if (id == "9") {
        loc = urlPath + half[1] + "&print=9";
        document.getElementById('list9').src = loc;
    }
    if (id == "10") {
        loc = urlPath + half[1] + "&print=10";
        document.getElementById('list10').src = loc;
    }
}

function CreateLog(OldValue, NewValue, Action, sStudentId) {
    var gradeLogRequest = { nGradeId: nGradeId, sPlaneID: getUrlParameter("id"), nTermSubLevel2: getUrlParameter("idlv2"), sTerm: getUrlParameter("term"), NumberYear: getUrlParameter("year"), SubLevel: response.GradeDTO.SubLevel, nTSubLevel2: response.GradeDTO.nTSubLevel2, CourseName: response.GradeDTO.CourseName, OldValue: OldValue, NewValue: NewValue, Action: Action, sStudentId: sStudentId };

    if (Action == "AssessmentNameOnChange" || Action == "AssessmentMaxScoreOnChange" || Action == "AssessmentScoreOnChange") {
        var tab = $("#myTab li.active");
        var assessmentType = $(tab).attr('id');
        if (assessmentType == "TabBeforeMidTerm") {
            gradeLogRequest.AssessmentType = "คะแนนเก็บ";
        }
        else if (assessmentType == "TabMidTerm") {

            gradeLogRequest.AssessmentType = "กลางภาค";
        }
        else if (assessmentType == "TabAfterMidTerm") {

            gradeLogRequest.AssessmentType = "คะแนนเก็บหลังกลางภาค";
        }
        else if (assessmentType == "TabFinal") {

            gradeLogRequest.AssessmentType = "ปลายภาค";
        }
        else if (assessmentType == "TabBehavior") {

            gradeLogRequest.AssessmentType = "คุณลักษณะฯ";
        }
        else if (assessmentType == "TabReadWrite") {
            gradeLogRequest.AssessmentType = "อ่าน คิด วิเคราะห์และเขียน";
        }
        else if (assessmentType == "TabSamattana") {
            gradeLogRequest.AssessmentType = "สมรรถนะ";
        }
    }

    
    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/AssessmentScore/CreateGradeLog";
    params['data'] = JSON.stringify(gradeLogRequest);
    params['requestType'] = "POST";
  
    if (Action == "DraftSave" || Action == "FinalSubmit" || Action == "DeleteSubmit") { params['stopCountingActiveAjaxConnections'] = true; }
    doAjax(params);
}

function btnDraftSaveOnClick() {
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
                    CreateLog("", "", "DraftSave", "");
                    setTimeout(function () { RedirectToMainPage(); },1000);
                }
            }
        }
    });
}

function btnCancelOnClick() {
    RedirectToMainPage();
}

function RedirectToMainPage() {
    //console.log("activeAjaxConnections" + activeAjaxConnections);
    if (activeAjaxConnections <= 0) {
        $("body").mLoading();      
        if (getUrlParameter("mode") == "3") {
            window.location.href = "../../grade/BP5cover-chalerm.aspx?idlv=" + getUrlParameter("idlv") + "&idlv2=" + getUrlParameter("idlv2") + "&year=" + getUrlParameter("year") + "&term=" + getUrlParameter("term") + "&id=" + getUrlParameter("id") + "&mode=" + getUrlParameter("mode") + "&PlanCourseId=" + getUrlParameter("PlanCourseId");
        }
        else {
            window.location.href = "../../grade/GradeRoomList.aspx?idlv=" + getUrlParameter("idlv") + "&idlv2=" + getUrlParameter("idlv2") + "&year=" + getUrlParameter("year") + "&term=" + getUrlParameter("term") + "&id=" + getUrlParameter("id") + " &mode=" + getUrlParameter("mode");
        }
    }
    else {
        OpenErrorInfoDialog("กำลังทำการบันทึกข้อมูล กรุณาลองใหม่อีกครั้ง"); 
    }
}

function OpenErrorInfoDialog(message) {
    $.confirm({
        title: false,
        content: '<img src="../../images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">' + message + '</h1>',
        theme: 'material',
        type: 'red',
        buttons: {
            "<span style=\"font-size: 20px;\">ปิด</span>": {
                btnClass: 'btn-primary closeButton',
                keys: ['enter', 'shift'],
                id: "btnOk",
                action: function () {
                }
            }
        }
    });
}

function unloadPage() {
    console.log(activeAjaxConnections);
    if (activeAjaxConnections > 0) {
        return "You have unsaved changes on this page. Do you want to leave this page and discard your changes or stay on this page?";
    }
}

window.onbeforeunload = unloadPage;

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
    //console.log(url);
    //console.log(stopCountingActiveAjaxConnections);
    //console.log(requestCount);
    //console.log($("input[name='__RequestVerificationToken']").val());

    $.ajax({
        url: url,
        crossDomain: false,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        headers: {
            '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function (jqXHR, settings) {
            if (!stopCountingActiveAjaxConnections)
                activeAjaxConnections++;
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
            if (!stopCountingActiveAjaxConnections) {
                activeAjaxConnections--;
            }
           
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}

function doAjaxSequencial(doAjax_params) {

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
    //console.log(url);
    //console.log(url);
    //console.log(stopCountingActiveAjaxConnections);
    $.ajax({
        url: url,
        crossDomain: false,
        async: false,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        headers: {
            '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val()
        },
        beforeSend: function (jqXHR, settings) {
          
            if (typeof beforeSendCallbackFunction === "function") {

                beforeSendCallbackFunction();
            }
        },
        success: function (data, textStatus, jqXHR) {
            console.log("doAjaxSequencial Success");
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Error");
            console.log("Ajax Error" + errorThrown);
            if (errorThrown == "Request Timeout") {
                ShowPageSessionTimeOutAlert();
            }
            else {
                ShowPageError();
            }

        },
        complete: function (jqXHR, textStatus) {
            activeAjaxConnections--;
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}

