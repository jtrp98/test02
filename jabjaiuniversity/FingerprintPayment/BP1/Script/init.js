var yearId = 0;
var CurriculumId = 1;

var _displayOne = function () {
    let $fn = this;

    $fn.GetCurriculumByYear = function (yearID) {
        yearId = yearID;
        $("#table-coures tbody tr").remove();
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });

        PageMethods.CheckYearHaveActiveTerm(yearId, function (response) {
            if (response) {
                PageMethods.GetCurriculumByYear(yearId, function (response) {
                    $.each(response, function (e, s) {
                        var curriculam = "<tr><td class=\"text - center\">" + (e + 1) +
                            "</td><td class=\"text - center\">" + s.CurriculumName +
                            "</td><td class=\"text - center\">" + s.PlanCount + "</td>" +
                            "<td class=\"text-center\"><div class=\"btn btn-success\" onclick=\"GetPlansByCurriculumId('" + s.CurriculumId + "');\">สร้างแผน</div>" +
                            "<div style=\"margin-left:5px\" class=\"btn btn-primary\" onclick=\"$displayTwo.showDialog(" + s.CurriculumId + ",'" + s.CurriculumName + "')\">แก้ไข</div>";

                        if (s.PlanCount <= 0) {
                            curriculam += "<div style=\"margin-left:5px\" class=\"btn btn-danger\" onclick=\"DeleteCurriculum('" + s.CurriculumId + "');\">ลบ</div>";
                        }

                        curriculam += "</td></tr >";
                        $("#table-coures tbody").append(curriculam);
                    });
                    ShowDisplay(2);
                    $.unblockUI();
                });
            }
            else {
                $.unblockUI();
                popupError("ไม่พบภาคเรียนในปีการศึกษาที่ท่านเลือก กรุณาทำการเพิ่มภาคเรียนในเมนูตั้งค่าปีการศึกษา");
            }
        });
    };

    $fn.CopyPlanFromAnotherYear = function (yearID, numberYear) {
        yearId = yearID;
       
        $("#CopyPlanFromAnotherYearModal").modal({ backdrop: 'static', keyboard: false });
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>', baseZ: 2000 });

        $.ajax({
            url: "../api/common/GetYears",
            crossDomain: true,
            type: "GET",
            contentType: 'application/json; charset=utf-8',
            //dataType: 'json',
            //data: JSON.stringify(gradeDetailsImportFromExcelRequests),
            success: function (response) {
                $("#hdnCopyPlanToYear").val(yearId);
                $("#CopyPlanToYear").html(numberYear);
                $.unblockUI();
                $("#ddlCopyPlanFromAnotherYear").empty();
                $("#ddlCopyPlanFromAnotherYear option").remove();
                $("#ddlCopyPlanFromAnotherYear").append('<option value="">เลือกปี</option>');
                $(response).each(function (index, value) {
                    if (value.NYear != yearID) {
                        $("#ddlCopyPlanFromAnotherYear").append('<option value="' + value.NYear + '">' + value.NumberYear + '</option>')
                    }
                })
                
                
            }
        });

       
       
        //$("#ddlCopyPlanFromAnotherYear option[value='" + yearID + "']").remove();
       
       
        //PageMethods.CheckYearHaveActiveTerm(yearId, function (response) {
        //    if (response) {
        //        PageMethods.GetCurriculumByYear(yearId, function (response) {
        //            var planCount = 0;
        //            $.each(response, function (e, s) {
        //                planCount += s.PlanCount;
        //                //$("#table-coures tbody").append("<tr><td class=\"text - center\">" + (e + 1) +
        //                //    "</td><td class=\"text - center\">" + s.CurriculumName +
        //                //    "</td><td class=\"text - center\">" + s.PlanCount + "</td>" +
        //                //    "<td class=\"text-center\"><div class=\"btn btn-success\" onclick=\"GetPlansByCurriculumId('" + s.CurriculumId + "');\">สร้างแผน</div><div style=\"margin-left:5px\" class=\"btn btn-primary\" onclick=\"$displayTwo.showDialog(" + s.CurriculumId + ",'" + s.CurriculumName + "')\">แก้ไข</div><div style=\"margin-left:5px\" class=\"btn btn-danger\" onclick=\"DeleteCurriculum('" + s.CurriculumId + "');\">ลบ</div></td>" +
        //                //    "</tr > ");
        //            });
        //            if (planCount == 0) {
        //                $.unblockUI();
        //            }
        //            else {
        //                $.unblockUI();
        //            }
        //            console.log(planCount);
        //            //ShowDisplay(2);
        //            $.unblockUI();
        //        });
        //    }
        //    else {
        //        $.unblockUI();
        //        popupError("ไม่พบภาคเรียนในปีการศึกษาที่ท่านเลือก กรุณาทำการเพิ่มภาคเรียนในเมนูตั้งค่าปีการศึกษา");
        //    }
        //});
    };

   
};

var _displayTwo = function () {
    let $fn = this;

    $fn.showDialog = function (CurriculumId, name) {
        $.confirm({
            title: '',
            content: '' +
                '<form action="" class="formName" style="font-size:24px;">' +
                '<div class="form-group">' +
                '<h2>เพิ่มหลักสูตร</h2>' +
                '<div class="col-md-4">หลักสูตร</div>' +
                '<div class="col-md-8"><input type="text" class="form-control" id="name" style=\"font-size:20px;\" required value="' + name + '" /></div>' +
                '</div>' +
                '<input type="hidden" id="CurriculumId" value="' + CurriculumId + '" />' +
                '</form>',
            theme: 'bootstrap',
            buttons: {
                formSubmit: {
                    text: '<span style=\"font-size: 20px;\" >บันทึก</span>',
                    btnClass: 'pull-left btn btn-blue',
                    action: function () {
                        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
                        var name = this.$content.find('input[id=name]').val();
                        let curriculumId = this.$content.find('input[id=CurriculumId]').val();
                        var data = {
                            NYear: yearId, CurriculumName: name,
                            CurriculumId: curriculumId
                        };

                        if (name !== "") {

                            PageMethods.AddOrUpdateCurriculum(data, function (e) {
                                //getStudentData(controlId);
                                //getStudentData(controlId + 1);
                                popupError("บันทึกข้อมูลหลักสูตรเรียบร้อยแล้ว");
                                $displayOne.GetCurriculumByYear(yearId);
                                $.unblockUI();
                            });
                        } else {
                            $.unblockUI();
                            $.alert('กรุณากรอกชื่อหลักสูตร ');
                            return false;
                        }
                    }
                },
                "<span style=\"font-size: 20px;\" >ยกเลิก</span>": function () {
                }
            }, onContentReady: function () {

            }
        });
    };
};

var _displayThree = function () {
    let $fn = this;
    $fn.planeData = {};

    $fn.RanderTable = function () {
        console.log("Render Table");
        console.log($fn.planeData);
        $(".select2").select2();
        $("#table-course-plan tbody tr").remove();
        var term1nTerm = "";
        var term2nTerm = "";
        var term3nTerm = "";
        var totalTermCount = 0;
        $.each($fn.planeData, function (e, s) {
            //console.log(s);
            term1nTerm = "";
            term2nTerm = "";
            term3nTerm = "";
            //console.log("term3nTerm" + term3nTerm);
            let term = "";
            if (s.PlanCourseTermDTOs != undefined) {
                totalTermCount = s.PlanCourseTermDTOs.length;
                //console.log(totalTermCount);
                //console.log(s.PlanCourseTermDTOs);
                
                $.each(s.PlanCourseTermDTOs, function (e1, s1) {
                    if (s1.IsActive === true) {
                        term += (term === "" ? "" : ", ") + s1.TermNumber.toString();

                        if (s1.TermNumber.toString() == "1") {
                            term1nTerm = s1.NTerm;
                        }
                        else if (s1.TermNumber.toString() == "2") {
                            term2nTerm = s1.NTerm;
                        }
                        else if (s1.TermNumber.toString() != "1" && s1.TermNumber.toString() != "2")
                        {
                            term3nTerm = s1.NTerm;
                        }
                    }
                });
            }

            //console.log("term3nTerm-2" + term3nTerm);
            let teacherNames = "";
            if (s.PlanCourseTeacherDTOs != undefined) {
                $.each(s.PlanCourseTeacherDTOs, function (e, s) {
                    teacherNames += (teacherNames === "" ? "" : ", ") + s.TeacherFullName;
                })
            }
            //console.log("s.CourseStatus" + s.CourseStatus);
            //console.log("s.CourseStatus === 0" + s.CourseStatus === 0);
            $("#table-course-plan tbody").append("<tr><td class=\"text-center\">" + (e + 1) + "</td>" +
                //"<td class=\"text-center\">" + s.SPlaneId + "</td>" +
                "<td class=\"text-center\">" + s.CourseCode + "</td>" +
                "<td class=\"text-center\">" + s.NCredit + "</td>" +
                "<td>" + s.CourseName + "</td>" +
                "<td>" +
                teacherNames

                + "</td>" +
                "<td class=\"text-center\">" + term + "</td>" +
                (s.CourseStatus === 0 ? "<td class=\"text-center\"><i style=\"cursor: pointer;\" id=\"CourseStatus\" onclick=\'$displayThree.switchStatus($(this).attr(\"plane-data\"),\"CourseStatus\");\' class=\"" + (s.CourseStatus === 0 ? "fa fa-toggle-off" : "fa fa-toggle-on") + "\" plane-data='" + e + "'></i></td>" : "<td class=\"text-center \">" + "<i style=\"cursor: pointer;\" id=\"CourseStatus\" onclick=\'$displayThree.switchStatus($(this).attr(\"plane-data\"),\"CourseStatus\");\' class=\"" + (s.CourseStatus === 0 ? "fa fa-toggle-off" : "fa fa-toggle-on") + "\" plane-data='" + e + "'></i> </td>") +
                "<td class=\"text-center\">" +
                //"<i style=\"cursor: pointer;\" onclick='$displayThree.switchStatus(" + e + ");' class=\"" + (s.CourseStatus === 0 ? "fa fa-toggle-off" : "fa fa-toggle-on") + "\" plane-data='" + s.SPlaneId + "'></i> " +
                "<div class=\"dropdown\">\
                        <button class=\"btn btn-info dropdown-toggle\" type=\"button\" data-toggle=\"dropdown\">\
                            จัดการข้อมูล<span class=\"caret\"></span>\
                        </button>\
                        <ul class=\"dropdown-menu\">\
                            <li><a tabindex=\"-1\" style=\"font-size: 24px; cursor: pointer;\" onclick='$displayThree.getEditData(\"" + e + "\"," + s.PlanCourseId + "," + s.NYear + ");' UseSubmitBehavior='false'>แก้ไขโครงสร้างวิชาเรียน</a></li>\
                            <li><a tabindex=\"-1\" style=\"font-size: 24px; cursor: pointer;\" onclick='$displayThree.ShowModalForStudentClub(\"" + e + "\"," + s.PlanCourseId + "," + s.NYear + ",\"" + term1nTerm + "\",\"" + term2nTerm + "\",\"" + term3nTerm + "\",\"" + s.CourseCode + "\",\"" + s.CourseName + "\"," + totalTermCount + ");' UseSubmitBehavior='false'>ลงทะเบียนรายบุคคล</a></li>\
                            <li><a tabindex=\"-1\" style=\"font-size: 24px; cursor: pointer;\" onclick='$displayThree.EditAdjustedYearAndTerm(\"" + e + "\"," + s.PlanCourseId + "," + s.NYear + ");' UseSubmitBehavior='false'>ปรับแสดงผล รบ.1</a></li>\
                        </ul>\
                    </div>"+
                // <li><a tabindex=\"-1\" style=\"font-size: 24px; cursor: pointer;\" onclick='$displayThree.ShowModalForSwitchStatus(\"" + e + "\");' UseSubmitBehavior='false'>ตั้งค่าการเปิดการใช้งาน</a></li>\
                //"<i style=\"cursor: pointer;\" onclick='$displayThree.switchStatus(" + e + ");' class=\"" + (s.CourseStatus === 0 ? "fa fa-toggle-off" : "fa fa-toggle-on") + "\" plane-data='" + s.SPlaneId + "'></i> " +
                //"<div class=\"btn btn-success\" onclick='$displayThree.getEditData(\"" + e + "\"," + s.PlanCourseId + "," + s.NYear + ");' UseSubmitBehavior='false'>แก้ไข</div>" +
                //"<div style =\"margin-left:5px\" class=\"btn btn-primary\" onclick='$displayThree.EditAdjustedYearAndTerm(\"" + e + "\"," + s.PlanCourseId + "," + s.NYear + ");' UseSubmitBehavior='false'>ปรับ รบ.1</div>"  +
                "</td>" +
                "</tr>");
        });
    };

    $fn.ShowModalForStudentClub = function (id, planCourseId, nYear, term1nTerm, term2nTerm, term3nTerm, courseCode, courseName, totalTermCount) {
        $("#PlanCourseId").val(planCourseId);
        $("#term1nTerm").val(term1nTerm);
        $("#term2nTerm").val(term2nTerm);
        $("#term3nTerm").val(term3nTerm);
        $("#totalTermCount").val(totalTermCount);
        $("#StudentClubModalCourseCode").val(courseCode);
        $("#StudentClubModalCourseName").val(courseName);
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>', baseZ: 2000 });
        PageMethods.GetPlanCourseRoomDetail($("#select-sublevel2").val(), function (response) {
            //console.log(response);

            $("#StudentRoom option").remove();
            $.each(response, function (index) {
                $('select[id*=StudentRoom]')
                    .append($("<option></option>")
                        .attr("value", response[index].nTermSubLevel2)
                        .text(response[index].nTSubLevel2));
            });

            PageMethods.GetPlanCourseStudentDetail($("#select-sublevel").val(), $("#StudentRoom").val(), planCourseId, term1nTerm, term2nTerm, term3nTerm, totalTermCount, function (responseStudentDetails) {

                GetPlanCourseStudentDetailOnSuccess(responseStudentDetails);
            });
        });


        //PageMethods.GetStudentsForSubjectMapping(data["SPlaneId"], data["PlanId"], data["PlanCourseTermDTOs"], function (response) {

        //});

        $("#RowsIndex").val(id);
        $("#ModalForStudentClub").modal({ backdrop: 'static', keyboard: false });

    };

    $fn.ShowModalForSwitchStatus = function (id) {
        $("#RowsIndex").val(id);
        $("#ModalForSwitchStatus").modal({ backdrop: 'static', keyboard: false });
        $("#ModalForEditActualData").on('shown.bs.modal', function (ef) {
            //$(".select2").select2();
        });

        let _planeData = $fn.planeData.filter(function (e) {
            return e.RowsIndex == id;
        });

        if (_planeData.length > 0) {
            $("#CourseStatus").removeClass("fa fa-toggle-off");
            $("#CourseStatus").removeClass("fa fa-toggle-on");
            $("#CourseStatus").attr("plane-data", id);
            if (_planeData[0].CourseStatus == 1) {
                $("#CourseStatus").addClass("fa fa-toggle-on");
            } else {
                $("#CourseStatus").addClass("fa fa-toggle-off");
            }

            $("#IsActiveForGrade").removeClass("fa fa-toggle-off");
            $("#IsActiveForGrade").removeClass("fa fa-toggle-on");
            $("#IsActiveForGrade").attr("plane-data", id);
            if (_planeData[0].IsActiveForGrade == 1) {
                $("#IsActiveForGrade").addClass("fa fa-toggle-on");
            } else {
                $("#IsActiveForGrade").addClass("fa fa-toggle-off");
            }
        }
    };

    $fn.switchStatus = function (id, Columns) {
        $("#RowsIndex").val(id);
        //console.log("switchStatus");
        //console.log(id);
        //console.log(Columns);
        $.each($fn.planeData, function (index, data) {
            if (data["RowsIndex"] == id) {
                if (data[Columns] == 1) {
                    //$.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
                    PageMethods.ValidatePlanCourseDeActivation(data["SPlaneId"], data["PlanId"], data["PlanCourseTermDTOs"], function (response) {
                        switch (response.ErrorCode) {
                            case 500:
                                $.alert("ไม่สามารถปิดการใช้งาน เพราะมันถูกใช้ไปแล้วในตารางเวลา / เกรด");
                                $.unblockUI();
                                break;
                            case 200:

                                if (data[Columns] = 1) {
                                    $("#" + Columns).removeClass("fa fa-toggle-on");
                                    $("#" + Columns).addClass("fa fa-toggle-off");
                                }
                                else {
                                    $("#" + Columns).removeClass("fa fa-toggle-off");
                                    $("#" + Columns).addClass("fa fa-toggle-on");
                                }

                                data[Columns] = data[Columns] == 0 ? 1 : 0;
                                data["IsModified"] = true;

                                if (Columns === "CourseStatus") {
                                    $fn.RanderTable();
                                }
                                $.unblockUI();
                                break;
                        }
                    });
                }
                else if (data[Columns] == "" || data[Columns] == 0) {

                    data[Columns] = 1;
                    data["IsModified"] = true;
                    if (Columns === "CourseStatus") {
                        $fn.RanderTable();
                    }

                    $("#" + Columns).removeClass("fa fa-toggle-off");
                    //$("#" + Columns).removeClass("fa fa-toggle-on");
                    $("#" + Columns).addClass("fa fa-toggle-on");
                }
                //data["CourseStatus"] = data["CourseStatus"] === 0 ? 1 : 0;
                //data["IsModified"] = true;
            }
        });

    };

    $fn.switchStatusall = function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>', baseZ: 2000 });
        //console.log("switchStatus");
        //console.log(id);
        //console.log(Columns);
        var setCourseStatus = true;
        var className = $("#AllCourseStatus").attr("class");
        if (className == "fa fa-toggle-on fa-toggle-onall")
        {
            setCourseStatus = false;
            //$("#AllCourseStatus").removeClass("fa fa-toggle-on fa-toggle-onall");
            //$("#AllCourseStatus").addClass("fa fa-toggle-off");
        }
        else
        {
            //$("#AllCourseStatus").removeClass("fa fa-toggle-off");
            //$("#AllCourseStatus").addClass("fa fa-toggle-on fa-toggle-onall");
        }
        console.log("setCourseStatus" + setCourseStatus);
        var stopExecuting = false;
        var Columns = "CourseStatus";
        var subjectCount = $fn.planeData.length;
        console.log("subjectCount" + subjectCount);


        let postData = {
            //NYear: yearId,
            //CurriculumId: CurriculumId, PlanName: $("#PlanName").val(),
            //NTSubLevel: $("#select-sublevel").val(), PlanId: $("#PlanId").val(),
            EducationSubLevelIds: $("#select-sublevel2").val(),
        };

        let data = [];

        $.each($fn.planeData, function (e, s) {
            if (s["CourseStatus"] == 1 && setCourseStatus == false) {
                data.push({
                    PlanId: s["PlanId"], PlanCourseId: s["PlanCourseId"],
                    SPlaneID: s["SPlaneId"], CourseName: s["CourseName"],
                    PlanCourseTermDTOs: s["PlanCourseTermDTOs"] === null ? [] : s["PlanCourseTermDTOs"],
                    CourseCode: s["CourseCode"],
                    EducationSubLevelIds: $("#select-sublevel2").val(),
                });
            }
        });
        console.log("data" + data);
        postData["PlanCourseDTOs"] = data;
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        
        PageMethods.ValidatePlanCoursesDeActivation(postData, function (response) {
            switch (response.ErrorCode) {
                case 500:
                    $.alert(response.ErrorMessage.replace(",", "<br/>") + " วิชาดังกล่าวไม่สามารถ ไม่สามารถปิดการใช้งานได้ เนื่องจากถูกใช้งานในตารางเวลา / เกรด");
                    $.unblockUI();
                    break;
                case 200:
                    console.log("200")
                    if (data.length != 0) {
                        $.each($fn.planeData, function (index, data) {

                            if (data["CourseStatus"] == 1 && setCourseStatus == false) {
                                data["CourseStatus"] = (setCourseStatus) ? 1 : 0;
                                data["IsModified"] = true;
                            }
                            else if ((data["CourseStatus"] == 0 && setCourseStatus == true) || (data["CourseStatus"] == "" || data["CourseStatus"] == 0)) {

                                data["CourseStatus"] = (setCourseStatus) ? 1 : 0;
                                data["IsModified"] = true;
                            }
                        });
                    }
                    else if (data.length == 0 && setCourseStatus == true) {
                        $.each($fn.planeData, function (index, data) {
                            if ((data["CourseStatus"] == 0) || (data["CourseStatus"] == "")) {
                                data["CourseStatus"] = (setCourseStatus) ? 1 : 0;
                                data["IsModified"] = true;
                            }
                        });
                    }
                    
                    if (className == "fa fa-toggle-on fa-toggle-onall") {
                        $("#AllCourseStatus").removeClass("fa fa-toggle-on fa-toggle-onall");
                        $("#AllCourseStatus").addClass("fa fa-toggle-off");
                    }
                    else {
                        $("#AllCourseStatus").removeClass("fa fa-toggle-off");
                        $("#AllCourseStatus").addClass("fa fa-toggle-on fa-toggle-onall");
                    }
                    $fn.RanderTable();
                    $.unblockUI();
                    break;
            }
        });
        
    };
    $.ajax({
        url: "/App_Logic/modalJSON.aspx?mode=teacher",
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName + ' ' + objjson[index].sLastname,
                    value: objjson[index].sEmp,
                    code: objjson[index].sPhone,
                };
                availableValueEmployees[index] = newObject;
            });
        }
    }).done(function () {
        let selectTeachar = $($('select[name*=classchoice2]')[0]);
        addOptionTeacher(selectTeachar);
    });

    $fn.getEditData = function (id, planCourseId, nYear) {
        let data = getObjects($fn.planeData, "RowsIndex", id);
        $("#RowsIndex").val(id);
      
        for (e = $("[name*=classchoice2]").length; e > 0; e--) {
            $($("[name*=classchoice2]")[e]).parents(".group-teacher").remove();
        }

        var gradeScoreRatioUpdatedRequest = { nYear: yearId, nTSubLevel: $("#select-sublevel").val(), nTermSubLevel2s: $("#select-sublevel2").val(), sPlaneId: data[0].SPlaneId, PlanId: $("#PlanId").val(), PlanCourseId: data[0].PlanCourseId };

        $.ajax({
            url: "../api/AssessmentScore/CheckGradeScoreRatioUpdated",
            ContentType: "application/json; charset=utf-8",
            type: "Post",
            data: gradeScoreRatioUpdatedRequest,
            success: function (isScoreEntered) {
                console.log(isScoreEntered);
               
                if (isScoreEntered) {
                    $("#model-data").find("[id=IsUserAllowedToEditRatio]").attr('disabled', 'disabled');

                    //Score Is Entered In the score entry page for any Term But Ratio not updated in the plan then don't disable the ratio dropdown boxes.
                    if ($("#model-data").find("[id=IsUserAllowedToEditRatio]").val() == "ไม่" && ($('#RatioBeforeMidTerm option:selected').val() == "-1") && ($('#RatioAfterMidTerm option:selected').val() == "-1") && ($('#RatioMidTerm option:selected').val() == "-1") && ($('#RatioLateTerm option:selected').val() == "-1")) {

                        $('#RatioBeforeMidTerm').prop("disabled", false);
                        $('#RatioAfterMidTerm').prop("disabled", false);
                        $('#RatioMidTerm').prop("disabled", false);
                        $('#RatioLateTerm').prop("disabled", false);
                        $('#RatioQuizPass').prop("disabled", false);
                    }
                    else {

                        $('#RatioBeforeMidTerm').prop("disabled", true);
                        $('#RatioAfterMidTerm').prop("disabled", true);
                        $('#RatioMidTerm').prop("disabled", true);
                        $('#RatioLateTerm').prop("disabled", true);
                        $('#RatioQuizPass').prop("disabled", true);
                    }
                }
                else {
                    $("#model-data").find("[id=IsUserAllowedToEditRatio]").removeAttr('disabled');

                    if ($("#model-data").find("[id=IsUserAllowedToEditRatio]").val() == "ใช่") {
                        $('#RatioBeforeMidTerm').prop("disabled", true);
                        $('#RatioAfterMidTerm').prop("disabled", true);
                        $('#RatioMidTerm').prop("disabled", true);
                        $('#RatioLateTerm').prop("disabled", true);
                        $('#RatioQuizPass').prop("disabled", true);
                    }
                    else {
                        $('#RatioBeforeMidTerm').prop("disabled", false);
                        $('#RatioAfterMidTerm').prop("disabled", false);
                        $('#RatioMidTerm').prop("disabled", false);
                        $('#RatioLateTerm').prop("disabled", false);
                        $('#RatioQuizPass').prop("disabled", false);
                    }
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log("Ajax Error" + errorThrown)
                console.log("jqXHR" + jqXHR);
                console.log("textStatus" + textStatus);

            },
        });
        var isPlanCourseTeacherExists = false;
        $.map(data[0], function (values, key) {

            if (values !== null) {
                if (key === "PlanCourseTeacherDTOs") {
                    if (values.length > 0) {
                        isPlanCourseTeacherExists = true;
                        $.each(values, function (e, s) {
                            if (e > 0) {
                                addRows(e, s.SEmp);
                            }
                            else {
                                $($("[name*=classchoice2]")[e]).val(s.SEmp.toString()).trigger('change');
                            }
                        });
                    }
                    else {
                        $($("[name*=classchoice2]")[e]).val("").trigger('change');
                    }
                } else {
                    //console.log(key);
                    if (key == "IsUserAllowedToEditRatio") {
                        if (values.toString() == "false" || values.toString() == "ไม่") {
                            $("#model-data").find("[id=" + key + "]").val("ไม่");
                        }
                        else {
                            $("#model-data").find("[id=" + key + "]").val("ใช่");
                        }
                    }
                    else {
                        $("#model-data").find("[id=" + key + "]").val(values.toString());
                    }
                }
            }
            else if (key == "RatioBeforeMidTerm" || key == "RatioAfterMidTerm" || key == "RatioMidTerm" || key == "RatioLateTerm" || key == "RatioQuizPass") {
                values = (values == "" || values == null || values == 'undefined') ? "-1" : values;
                $("#model-data").find("[id=" + key + "]").val(values.toString());
            }
        });

        //Teacher selection is retain when open another subject, for avoid this problem we need to set the default value.
        if (isPlanCourseTeacherExists == false && planCourseId == 0) {
            $($("[name*=classchoice2]")[0]).val("").trigger('change');
        }

        $("#Term").html("");
        $.each(data[0]["PlanCourseTermDTOs"], function (e, s) {
            $("#Term").append("<div class=\"checkbox\"><label><input type=\"checkbox\" value=\"" + s.NTerm + "\" data-name=\"" + s.TermNumber + "\"  name=\"TermId\" " + (s.IsActive ? "checked" : "") + " /> " + s.TermNumber + "</label></div>");
        });

        $("#model-data").modal({ backdrop: 'static', keyboard: false });
        $("#model-data").on('shown.bs.modal', function (ef) {
            $(".select2").select2();
        });
    };

    $fn.RatioOnChange = function (control, oldValue) {
        console.log(oldValue);
        console.log($(control).val());
        var ratioBeforeMidTerm = ($('#RatioBeforeMidTerm option:selected').val() != "-1") ? Number($('#RatioBeforeMidTerm option:selected').val()) : 0;
        var ratioAfterMidTerm = ($('#RatioAfterMidTerm option:selected').val() != "-1") ? Number($('#RatioAfterMidTerm option:selected').val()) : 0;
        var ratioMidTerm = ($('#RatioMidTerm option:selected').val() != "-1") ? Number($('#RatioMidTerm option:selected').val()) : 0;
        var ratioLateTerm = ($('#RatioLateTerm option:selected').val() != "-1") ? Number($('#RatioLateTerm option:selected').val()) : 0;

        if ((ratioBeforeMidTerm + ratioAfterMidTerm + ratioMidTerm + ratioLateTerm) > 100) {
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            popupError("เปอร์เซ็นต์ไม่ควรเกิน 100");
            $(control).val(oldValue);
        }
        else {
            let postData = {
                NYear: yearId,
                CurriculumId: CurriculumId, PlanName: $("#PlanName").val(),
                NTSubLevel: $("#select-sublevel").val(), PlanId: $("#PlanId").val(),
                EducationSubLevelIds: $("#select-sublevel2").val(),
            };
            let rowsIndex = $("#RowsIndex").val();
            let data = getObjects($fn.planeData, "RowsIndex", rowsIndex);
            console.log(data);
            console.log(postData);
            console.log(data[0].PlanCourseId);
            console.log(data[0].SPlaneId);

            var gradeScoreRatioUpdatedRequest = { nYear: yearId, nTSubLevel: $("#select-sublevel").val(), nTermSubLevel2s: $("#select-sublevel2").val(), sPlaneId: data[0].SPlaneId, PlanId: $("#PlanId").val(), PlanCourseId: data[0].PlanCourseId };

            $.ajax({
                url: "../api/AssessmentScore/CheckGradeScoreRatioUpdated",
                ContentType: "application/json; charset=utf-8",
                type: "Post",
                data: gradeScoreRatioUpdatedRequest,
                success: function (response) {
                    console.log(response);
                    if (response) {
                        popupError("ไม่สามารถเปลี่ยนสัดส่วนคะแนนวิชานี้ได้ เนื่องจากวิชานี้ได้ทำการตั้งสัดส่วนในหน้าบันทึกคะแนนแล้ว กรุณาทำการแก้ไขที่หน้าบันทึกคะแนน");
                        $(control).val(oldValue);
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log("Ajax Error" + errorThrown)
                    console.log("jqXHR" + jqXHR);
                    console.log("textStatus" + textStatus);

                },
            });
        }

    }

    $fn.IsUserAllowedToEditRatioOnChange = function (control, oldValue) {
        console.log(oldValue);
        console.log($(control).val());

        if ($(control).val() == "ใช่") {
            $('#RatioBeforeMidTerm').prop("disabled", true);
            $('#RatioAfterMidTerm').prop("disabled", true);
            $('#RatioMidTerm').prop("disabled", true);
            $('#RatioLateTerm').prop("disabled", true);
            $('#RatioQuizPass').prop("disabled", true);
        }
        else {
            $('#RatioBeforeMidTerm').prop("disabled", false);
            $('#RatioAfterMidTerm').prop("disabled", false);
            $('#RatioMidTerm').prop("disabled", false);
            $('#RatioLateTerm').prop("disabled", false);
            $('#RatioQuizPass').prop("disabled", false);
        }
    }

    $fn.AdjustedYearOnChange = function (control) {
        $("#btnAdjustedDataSave").attr("disabled", true);
        let rowsIndex = $("#RowsIndex").val();
        let data = getObjects($fn.planeData, "RowsIndex", rowsIndex);

        var nYear = $('#AdjustedYear option:selected').val();

        var numberYear = $('#AdjustedYear option:selected').text();

        if (data[0].AdjustednYear == nYear) {
            $("#AdjustedTerm").html("");
            $.each(data[0]["PlanCourseAdjustedTermDTOs"], function (e, s) {
                $("#AdjustedTerm").append("<div class=\"checkbox\"><label><input type=\"checkbox\" value=\"" + s.NTerm + "\" data-name=\"" + s.TermNumber + "\"  name=\"AdjustedTermId\" " + (s.IsActive ? "checked" : "") + " /> " + s.TermNumber + "</label></div>");
            });
            $("#btnAdjustedDataSave").attr("disabled", false);
        }
        else {
            if (nYear != "") {
                $.ajax({
                    url: "../api/common/TermByYearSeqOrder/?numberYear=" + numberYear,
                    success: function (response) {
                        $("#AdjustedTerm").html("");
                        $.each(response, function (e, s) {
                            $("#AdjustedTerm").append("<div class=\"checkbox\"><label><input type=\"checkbox\" value=\"" + s.nTerm + "\" data-name=\"" + s.sTerm + "\"  name=\"AdjustedTermId\" /> " + s.sTerm + "</label></div>");
                        });
                        $("#btnAdjustedDataSave").attr("disabled", false);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        console.log("Ajax Error" + errorThrown)
                        console.log("jqXHR" + jqXHR);
                        console.log("textStatus" + textStatus);

                    },
                });
            }
            else {
                $("#AdjustedTerm").html("");
                $("#btnAdjustedDataSave").attr("disabled", false);
            }
        }
    };


    $fn.EditAdjustedYearAndTerm = function (id, planCourseId, nYear) {
        $("#btnAdjustedDataSave").attr("disabled", false);
        let data = getObjects($fn.planeData, "RowsIndex", id);
        $("#RowsIndex").val(id);
        $('#AdjustedYear').val(data[0].AdjustednYear);
        $("#ActualCourseName").val(data[0].CourseName);
        $("#ActualCourseCode").val(data[0].CourseCode);
        if (data[0].RB1DisplayOrder == 999) {
            $('#RB1DisplayOrder').val(1);
        }
        else {
            $('#RB1DisplayOrder').val(data[0].RB1DisplayOrder);
        }

        $("#AdjustedTerm").html("");
        $.each(data[0]["PlanCourseAdjustedTermDTOs"], function (e, s) {
            $("#AdjustedTerm").append("<div class=\"checkbox\"><label><input type=\"checkbox\" value=\"" + s.NTerm + "\" data-name=\"" + s.TermNumber + "\"  name=\"AdjustedTermId\" " + (s.IsActive ? "checked" : "") + " /> " + s.TermNumber + "</label></div>");
        });

        $("#ModalForEditActualData").modal({ backdrop: 'static', keyboard: false });
        $("#ModalForEditActualData").on('shown.bs.modal', function (ef) {
            //$(".select2").select2();
        });
    };

    $fn.CreateControlEvent = function () {
        $("#select-sublevel").change(function () {
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            $("#table-course-plan tbody tr").remove();
            PageMethods.GetCoursesForCreatePlan($("#select-sublevel").val(), yearId, function (response) {
                $fn.planeData = response;
                $.each($fn.planeData, function (e, s) {
                    $fn.planeData[e]["RowsIndex"] = e;
                });
                $fn.RanderTable();
                $.unblockUI();
            });
        });

        $("#addRows").click(function () {
            addRows();
        });

        $("#btnModalSave").click(function () {
            let RowsIndex = $("#RowsIndex").val();
            console.log("btnModalSave");
            console.log($fn.planeData);
            var isTermSelected = false;
            $.each($("[name=TermId]"), function (e1, s1) {
                if (!isTermSelected)
                    isTermSelected = $(s1).prop("checked");
            });
            console.log("isTermSelected" + isTermSelected);
            if (isTermSelected) {
                $.each($fn.planeData, function (e, s) {
                    if (s["RowsIndex"] == RowsIndex) {
                        s["IsModified"] = true;
                        $.each($("#model-data").find("input,select"), function (e1, s1) {
                            console.log($(s1).attr("id"));
                            if ($(s1).attr("id") != undefined) {
                                s[$(s1).attr("id")] = $(s1).val();
                            }
                        });

                        s["PlanCourseTeacherDTOs"] = [];

                        var selectedTeachers = $("[name*=classchoice2]").find(':selected');

                        $.each(selectedTeachers, function (e1, s1) {
                            if (s1.value !== "" && s1.value != null) {
                                s["PlanCourseTeacherDTOs"].push({ SEmp: s1.value, TeacherFullName: s1.text });
                            }
                        });
                        console.log(s["PlanCourseTeacherDTOs"]);

                        s["PlanCourseTermDTOs"] = [];
                        $.each($("[name=TermId]"), function (e1, s1) {
                            s["PlanCourseTermDTOs"].push({ PlanCourseId: s["PlanCourseId"], IsActive: $(s1).prop("checked"), TermNumber: $(s1).attr("data-name"), NTerm: $(s1).val() });
                        });
                    }
                });
                console.log("After Updated btnModalSave");

                $fn.RanderTable();
                $("#model-data").modal("hide");
            }
            else {

                $.alert("กรุณาเลือกระยะ");
                $.unblockUI();
            }
        });

        $("#btnAdjustedDataSave").click(function () {
            let RowsIndex = $("#RowsIndex").val();
            $.each($fn.planeData, function (e, s) {
                if (s["RowsIndex"] == RowsIndex) {
                    s["IsModified"] = true;
                    s["AdjustednYear"] = $('#AdjustedYear option:selected').val();
                    s["RB1DisplayOrder"] = $('#RB1DisplayOrder').val();
                    s["PlanCourseAdjustedTermDTOs"] = [];
                    if ($("#AdjustedYear").val() != "") {
                        $.each($("[name=AdjustedTermId]"), function (e1, s1) {
                            s["PlanCourseAdjustedTermDTOs"].push({ PlanCourseId: s["PlanCourseId"], IsActive: $(s1).prop("checked"), TermNumber: $(s1).attr("data-name"), NTerm: $(s1).val() });
                        });
                    }
                }
            });

            $fn.RanderTable();
            $("#ModalForEditActualData").modal("hide");
        });

        $("#btnStudentClubDataSave").click(function () {
            //let RowsIndex = $("#RowsIndex").val();
            //$.each($fn.planeData, function (e, s) {
            //    if (s["RowsIndex"] == RowsIndex) {
            //        s["IsModified"] = true;
            //        s["AdjustednYear"] = $('#AdjustedYear option:selected').val();
            //        s["RB1DisplayOrder"] = $('#RB1DisplayOrder').val();
            //        s["PlanCourseAdjustedTermDTOs"] = [];
            //        if ($("#AdjustedYear").val() != "") {
            //            $.each($("[name=AdjustedTermId]"), function (e1, s1) {
            //                s["PlanCourseAdjustedTermDTOs"].push({ PlanCourseId: s["PlanCourseId"], IsActive: $(s1).prop("checked"), TermNumber: $(s1).attr("data-name"), NTerm: $(s1).val() });
            //            });
            //        }
            //    }
            //});

            //$fn.RanderTable();

            var rowCount = 0;
            var planCourseStudentRequest = [];
            table.rows().every(function (rowIdx, tableLoop, rowLoop) {
                var data = this.data();
                $("#select-sublevel").val(), $("#StudentRoom").val()
                if ($($("input[name='chkStudentCode[]']")[rowCount]).is(":checked") && $($("input[name='chkTerm1[]']")[rowCount]).is(":checked")) {
                    planCourseStudentRequest.push({ PlanCourseId: $("#PlanCourseId").val(), sID: data.sID, nTerm: $("#term1nTerm").val(), IsActive: true, nTermSubLevel2: $("#StudentRoom").val(), nTSubLevel: $("#select-sublevel").val() })
                }
                if ($($("input[name='chkStudentCode[]']")[rowCount]).is(":checked") && $($("input[name='chkTerm2[]']")[rowCount]).is(":checked")) {
                    planCourseStudentRequest.push({ PlanCourseId: $("#PlanCourseId").val(), sID: data.sID, nTerm: $("#term2nTerm").val(), IsActive: true, nTermSubLevel2: $("#StudentRoom").val(), nTSubLevel: $("#select-sublevel").val() })
                }

                if ($($("input[name='chkStudentCode[]']")[rowCount]).is(":checked") && $($("input[name='chkTerm3[]']")[rowCount]).is(":checked")) {
                    planCourseStudentRequest.push({ PlanCourseId: $("#PlanCourseId").val(), sID: data.sID, nTerm: $("#term3nTerm").val(), IsActive: true, nTermSubLevel2: $("#StudentRoom").val(), nTSubLevel: $("#select-sublevel").val() })
                }

                rowCount++;
            });

            $("#ModalForStudentClub").modal("hide");
            PageMethods.AddOrUpdatePlanCourseStudentDetails(planCourseStudentRequest, $("#PlanCourseId").val(), $("#StudentRoom").val(), $("#select-sublevel").val(), function (response) {
                switch (response.ErrorCode) {
                    case 200:
                        $.alert("บันทึกสำเร็จ");
                        $.unblockUI();
                        break;
                    case 400:
                        $.alert("ไม่อัปเดต");
                        $.unblockUI();
                        break;
                }
            });

        });

        $("#table-course-plan tbody").find(".fa.fa-toggle-off ,.fa.fa-toggle-on").click(function () {
            let id = $(this).attr("plane-data");
            $.each($fn.planeData, function (index, data) {
                if (data["RowsIndex"] === id) {
                    data["courseStatus"] = (data["courseStatus"] === 0 || data["courseStatus"] == "") ? 1 : 0;
                    data["IsModified"] = true;
                }
            });
            $fn.RanderTable();
        });

        $("#btnSubmit").click(function () {
            //if ($("#select-sublevel2").val() === null) {
            //    $.alert("กรุณาเลือกห้องเรียน");
            //    return;
            //}

            //console.log($fn.planeData);
            let postData = {
                NYear: yearId,
                CurriculumId: CurriculumId, PlanName: $("#PlanName").val(),
                NTSubLevel: $("#select-sublevel").val(), PlanId: $("#PlanId").val(),
                EducationSubLevelIds: $("#select-sublevel2").val(),
            };

            let data = [];

            $.each($fn.planeData, function (e, s) {
                if (s["IsModified"] == true) {
                    data.push({
                        IsModified: s["IsModified"],
                        PlanId: s["PlanId"], PlanCourseId: s["PlanCourseId"],
                        SPlaneID: s["SPlaneId"], CourseName: s["CourseName"],
                        NCredit: s["NCredit"],
                        NYear: yearId,
                        AdjustednYear: s["AdjustednYear"],
                        RB1DisplayOrder: s["RB1DisplayOrder"],
                        CourseType: s["CourseType"], CourseTotalHour: s["CourseTotalHour"],
                        CourseStatus: s["CourseStatus"], CourseHour: s["CourseHour"],
                        CourseGroup: s["CourseGroup"], CourseCode: s["CourseCode"],
                        PlanCourseTeacherDTOs: s["PlanCourseTeacherDTOs"] === null ? [] : s["PlanCourseTeacherDTOs"],
                        PlanCourseTermDTOs: s["PlanCourseTermDTOs"] === null ? [] : s["PlanCourseTermDTOs"],
                        PlanCourseAdjustedTermDTOs: s["PlanCourseAdjustedTermDTOs"] === null ? [] : s["PlanCourseAdjustedTermDTOs"],
                        IsActive: s["IsActive"], IsActiveForGrade: s["IsActiveForGrade"],
                        IsUserAllowedToEditRatio: (s["IsUserAllowedToEditRatio"] == "ใช่") ? true : false,
                        RatioAfterMidTerm: s["RatioAfterMidTerm"],
                        RatioBeforeMidTerm: s["RatioBeforeMidTerm"],
                        RatioMidTerm: s["RatioMidTerm"],
                        RatioLateTerm: s["RatioLateTerm"],
                        RatioQuizPass: s["RatioQuizPass"]
                    });
                }
            });

            postData["PlanCourseDTOs"] = data;
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            PageMethods.AddOrUpdatePlanDetails(postData, function (response) {
                switch (response.ErrorCode) {
                    case 50:
                        $.alert("กรุณาเข้าห้องเรียน");
                        $.unblockUI();
                        break;
                    case 100:
                        $.alert("กรุณาใส่ชื่อแผน");
                        $.unblockUI();
                        break;
                    case 200:
                        GetPlansByCurriculumId(CurriculumId);
                        $("#PlanId").val("0");
                        break;
                    case 400:
                        $.alert("ห้องเรียน " + response.ErrorMessage + " ได้มีการจัดแผนในปีการศึกษานี้แล้ว");
                        $.unblockUI();
                        break;
                    case 300:
                        $.alert("คุณไม่สามารถลบห้องได้เนื่องจากมันถูกแมปกับตารางเวลา / เกรดแล้ว");
                        $.unblockUI();
                        break;
                    case 500:
                        $.alert("ชื่อแผน(" + response.ErrorMessage + ") มีอยู่แล้ว");
                        $.unblockUI();
                        break;
                }
            });
        });

        
    };
};

var $displayOne = new _displayOne();
var $displayTwo = new _displayTwo();
var $displayThree = new _displayThree();
$(function () {
    $displayThree.CreateControlEvent();
    $(".select2").select2();

    $("#select-sublevel").change(function () {
        var SubLVID = $('#select-sublevel option:selected').val();
        var sSubLV = $('#select-sublevel2 option:selected').text();
        $("#select-sublevel2 option").remove();

        $.ajax({
            url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
            success: function (msg) {
                $.each(msg, function (index) {
                    $('select[id*=select-sublevel2]')
                        .append($("<option></option>")
                            .attr("value", msg[index].nTermSubLevel2)
                            .text(msg[index].nTSubLevel2));
                });
            }
        });
    });

    $("#btnCopyPlanFromAnotherYear").click(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>', baseZ: 2000 });
        var copyPlanTonYear = $("#hdnCopyPlanToYear").val();

        //console.log("After Updated btnModalSave");

        //$fn.RanderTable();
        //$("#model-data").modal("hide");
        var copyPlanFromnYear = $("#ddlCopyPlanFromAnotherYear").val();
        if (copyPlanFromnYear == "") {
            $.unblockUI();
            popupError("โปรดเลือกปี");  // Please select year
        }
        else {
            //Check Year have Term - If not throw error
            PageMethods.CheckYearHaveActiveTerm(copyPlanTonYear, function (response) {
                if (response) {
                    //Check Source Year have plan - If not throw error
                    PageMethods.GetCurriculumByYear(copyPlanFromnYear, function (response) {
                        var planCount = 0;
                        $.each(response, function (e, s) {
                            planCount += s.PlanCount;
                        });

                        if (planCount == 0) {
                            $.unblockUI();
                            popupError("ไม่มีแผนที่จะลอกเลียนแบบ"); // There are no plan to copy
                        }
                        else {
                            planCount = 0;
                            //Check destination year have plan already - If plan exist throw error
                            PageMethods.GetCurriculumByYear(copyPlanTonYear, function (response) {

                                $.each(response, function (e, s) {
                                    planCount += s.PlanCount;
                                });

                                if (planCount > 0) {
                                    $.unblockUI();
                                    popupError("มีแผนอยู่แล้ว"); //Already Plan Exist. 
                                }
                                else {  // Copy Plan from source to destination
                                    PageMethods.CopyPlanFromAnotherYear(copyPlanFromnYear, copyPlanTonYear, function (response) {
                                        if (response == "") {
                                            $.unblockUI();
                                            popupError("บันทึกรายการสำเร็จ");

                                        }
                                        else {
                                            $.unblockUI();
                                            popupError(response);
                                        }
                                    });
                                }

                            });
                            
                        }
                      
                    });
                }
                else {
                    $.unblockUI();
                    popupError("ไม่พบภาคเรียนในปีการศึกษาที่ท่านเลือก กรุณาทำการเพิ่มภาคเรียนในเมนูตั้งค่าปีการศึกษา");
                }
            });
        }
    });

});

var availableValueEmployees = [];
$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop(function () { $.unblockUI(); });

function ClosePopUp() {
    console.log("close");
    //$(".select2").val(null).trigger('change');
}
function DeleteCurriculum(curriculumId) {

    $.confirm({
        title: '<h2>กรุณายืนยัน !</h>',
        content: '<h2>คุณแน่ใจหรือว่าต้องการลบ</h>',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> ยกเลิก',
            },
            confirm: {
                label: '<i class="fa fa-check"></i> บันทึก',
                action: function () {
                    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
                    PageMethods.DeleteCurriculum(curriculumId, function (response) {
                        $.unblockUI();
                        if (response) {

                            $displayOne.GetCurriculumByYear(yearId);
                        }
                        else {
                            popupError("ไม่สามารถลบสิ่งนี้ ตรวจสอบหลักสูตรมีแผน");
                        }
                    });
                }
            }
        },
    });


}

function popupError(message) {
    $.confirm({
        title: '<h3>แจ้งเตือน</h3>',
        content: '<h3>' + message + '</h3>',
        theme: 'bootstrap',
        buttons: {
            "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                $.unblockUI();
            }
        }
    });
}

function addOptionTeacher(select) {
    //$(select).find('option').remove();
    select
        .append($("<option></option>")
            .attr("value", "")
            .text("เลือกอาจารย์ผู้สอน"));
    $.each(availableValueEmployees, function (index, data) {
        select
            .append($("<option></option>")
                .attr("value", data.value)
                .text(data.label));
    });
}

function removeRows(e) {
    $("#addRows").show();
    let rowsIndex = $(e).parents(".group-teacher").attr('rows-data');
    $(e).parents(".group-teacher").remove();
    for (i = rowsIndex - 1; i < $("[name*=classchoice2]").length; i++) {
        let selectIndex = $("[name*=classchoice2]")[i];
        let div = $(selectIndex).parents(".group-teacher");
        div.find('label').html("ครูผู้สอนคนที่ " + (i + 1));
        div.attr('rows-data', i + 1);
    }
}

function addRows(Index, Values) {
    if ($("[name*=classchoice2]").length === 24) $("#addRows").hide();
    var divTeachar = `<div class="row group-teacher" rows-data="` + $("[name*=classchoice2]").length + `" >
                            <div class="col-md-4">
                                <label class="pull-right">ครูผู้สอนคนที่ `+ ($("[name*=classchoice2]").length + 1) + `</label>
                            </div>
                            <div class="col-md-6">
                                <select class="form-control js-example-basic-multiple11 select2" name="classchoice2[]">
                                </select>
                            </div>
                            <div class="col-md-2">
                                <span onclick="removeRows(this)" style="cursor:pointer;">
                                    <i class="fa fa-remove"></i> Remove
                                </span>
                            </div>
                        </div>`;

    $("#model-data .teacher").append(divTeachar);
    selectTeachar = $($('select[name*=classchoice2]')[$("[name*=classchoice2]").length - 1]);
    addOptionTeacher(selectTeachar);
    $(".select2").select2();
    //$(selectTeachar).select2();
    $($("[name*=classchoice2]")[Index]).val(Values).trigger('change');
}

function ShowDisplay(DisplayIndex) {

    //if (DisplayIndex == 2) {
    //    $displayOne.GetCurriculumByYear(yearId);
    //}
    $("div[id*=display_]").hide();
    $("div[id*=display_" + DisplayIndex + "]").show();
    window.scrollTo(0, 0);


}
function StudentRoomOnChange() {
    console.log("StudentRoomOnChange");
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>', baseZ: 2000 });
    PageMethods.GetPlanCourseStudentDetail($("#select-sublevel").val(), $("#StudentRoom").val(), $("#PlanCourseId").val(), $("#term1nTerm").val(), $("#term2nTerm").val(), $("#term3nTerm").val(), $("#totalTermCount").val(), function (responseStudentDetails) {

        GetPlanCourseStudentDetailOnSuccess(responseStudentDetails);
    });
}
var table = $('#StudentClub').DataTable();
function GetPlanCourseStudentDetailOnSuccess(response) {
    $("#btnStudentClubDataSave").prop("disabled", true);

    table.destroy();
    table.clear().draw();
    table = $('#StudentClub').DataTable({
        bFilter: false,
        bSort: false,
        bPaginate: false,
        "sDom": 'Rlfrtlip',
        'data': response,
        'columns': [
            { 'data': 'RowNumber' },
            { 'data': 'sStudentID' },
            { 'data': 'Term1Active' },
            { 'data': 'Term2IsActive' },
            { 'data': 'Term3IsActive' },
        ],

        'columnDefs': [{
            'targets': 0,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-left',
            'width': '5%',
            'render': function (data, type, full, meta) {
                //var checked = (meta.settings.aoData[meta.row]._aData.Term1Active || meta.settings.aoData[meta.row]._aData.Term2IsActive) ? "Checked" : "";
                //return '<input type="checkbox" class="chkStudentCode" ' + checked + ' OnChange="StudentCodeOnChange(this,' + meta.row + ')" name="chkStudentCode[]" value="'
                //    + $('<div/>').text(data).html() + '">  ' + meta.settings.aoData[meta.row]._aData.sStudentID + " " + meta.settings.aoData[meta.row]._aData.StudentName
                return data;
            }
        }, {
            'targets': 1,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-left',
            'width': '75%',
            'render': function (data, type, full, meta) {
                var checked = (meta.settings.aoData[meta.row]._aData.Term1Active || meta.settings.aoData[meta.row]._aData.Term2IsActive || meta.settings.aoData[meta.row]._aData.Term3IsActive) ? "Checked" : "";
                return '<input type="checkbox" class="chkStudentCode" ' + checked + ' OnChange="StudentCodeOnChange(this,' + meta.row + ')" name="chkStudentCode[]" value="'
                    + $('<div/>').text(data).html() + '">  ' + meta.settings.aoData[meta.row]._aData.sStudentID + " " + meta.settings.aoData[meta.row]._aData.StudentName
            }
        },
        {
            'targets': 2,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            'render': function (data, type, full, meta) {
                var checked = (data) ? "Checked" : "";
                if ((meta.settings.aoData[meta.row]._aData.StudentTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.StudentTerm == "เทอม 1" ||
                    (meta.settings.aoData[meta.row]._aData.StudentTerm != null && meta.settings.aoData[meta.row]._aData.StudentTerm.indexOf('1') > -1)) && (meta.settings.aoData[meta.row]._aData.SubjectTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.SubjectTerm == "เทอม 1" || meta.settings.aoData[meta.row]._aData.SubjectTerm.indexOf('1') > -1)) {
                    return '<input type="checkbox" class="chkTerm1" name="chkTerm1[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                        + "" + '">';
                }
                else return '<input type="checkbox" class="chkTerm1" name="chkTerm1[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                    + "" + '" disabled>';
            }
        },
        {
            'targets': 3,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            'render': function (data, type, full, meta) {
                var checked = (data) ? "Checked" : "";


                if ((meta.settings.aoData[meta.row]._aData.StudentTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.StudentTerm == "เทอม 2" || (meta.settings.aoData[meta.row]._aData.StudentTerm != null && meta.settings.aoData[meta.row]._aData.StudentTerm.indexOf('2') > -1)) && (meta.settings.aoData[meta.row]._aData.SubjectTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.SubjectTerm == "เทอม 2" || meta.settings.aoData[meta.row]._aData.SubjectTerm.indexOf('2') > -1)) {
                    return '<input type="checkbox" class="chkTerm2" name="chkTerm2[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                        + "" + '">';
                }
                else return '<input type="checkbox" class="chkTerm2" name="chkTerm2[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                    + "" + '" disabled>';
            }
        },
        {
            'targets': 4,
            'searchable': false,
            'orderable': false,
            'className': 'dt-body-center',
            'visible': ($("#totalTermCount").val() == "3")?true:false,
            'render': function (data, type, full, meta) {
                var checked = (data) ? "Checked" : "";


                if ((meta.settings.aoData[meta.row]._aData.StudentTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.StudentTerm == "เทอม 3" || (meta.settings.aoData[meta.row]._aData.StudentTerm != null && meta.settings.aoData[meta.row]._aData.StudentTerm.indexOf('3') > -1)) && (meta.settings.aoData[meta.row]._aData.SubjectTerm == "ทั้งสองเทอม" || meta.settings.aoData[meta.row]._aData.SubjectTerm == "เทอม 3" || meta.settings.aoData[meta.row]._aData.SubjectTerm.indexOf('3') > -1)) {
                    return '<input type="checkbox" class="chkTerm3" name="chkTerm3[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                        + "" + '">';
                }
                else return '<input type="checkbox" class="chkTerm3" name="chkTerm3[]" ' + checked + ' OnChange="StudentClubChkTermOnChange(this,' + meta.row + ')" value="'
                    + "" + '" disabled>';
            }
        },
        ],

        fnInitComplete: function () {
            $.unblockUI();
            $("#btnStudentClubDataSave").prop("disabled", false);
        }

    });

    $('#StudentCodeSelectAll').on('click', function () {
        // Check/uncheck all checkboxes in the table
        var rows = table.rows({ 'search': 'applied' }).nodes();
        $("input[name='chkStudentCode[]']", rows).prop('checked', this.checked);

        if (!$("input[name='chkTerm1[]']", rows).prop('disabled')) {
            $("input[name='chkTerm1[]']", rows).prop('checked', this.checked);
        }

        if (!$("input[name='chkTerm2[]']", rows).prop('disabled')) {
            $("input[name='chkTerm2[]']", rows).prop('checked', this.checked);
        }

        if (!$("input[name='chkTerm3[]']", rows).prop('disabled')) {
            if ($("input[name='chkTerm3[]']", rows) != undefined && $("#totalTermCount").val() == "3") {
                $("input[name='chkTerm3[]']", rows).prop('checked', this.checked);
            }
        }
    });
};

function StudentClubChkTermOnChange(control, rowIndex) {

    if ($($("input[name='chkTerm2[]']")[rowIndex]).is(":checked") || $($("input[name='chkTerm1[]']")[rowIndex]).is(":checked") || $($("input[name='chkTerm3[]']")[rowIndex]).is(":checked")) {
        var arrChkBox = document.getElementsByName("chkStudentCode[]");
        $(arrChkBox[rowIndex]).prop('checked', true);
    }
    else {
        var arrChkBox = document.getElementsByName("chkStudentCode[]");
        $(arrChkBox[rowIndex]).prop('checked', false);
    }
}


function StudentCodeOnChange(control, rowIndex) {
    if ($(control).is(':checked')) {
        console.log($(control).is(':checked'));
       
        var arrChkBox = document.getElementsByName("chkTerm1[]");
        if (!$(arrChkBox[rowIndex]).prop('disabled')) {
            $(arrChkBox[rowIndex]).prop('checked', true);
        }

        arrChkBox = document.getElementsByName("chkTerm2[]");
        if (!$(arrChkBox[rowIndex]).prop('disabled')) {
            $(arrChkBox[rowIndex]).prop('checked', true);
        }


        if ($("#totalTermCount").val() == "3") {
            arrChkBox = document.getElementsByName("chkTerm3[]");
            if (!$(arrChkBox[rowIndex]).prop('disabled')) {
                $(arrChkBox[rowIndex]).prop('checked', true);
            }
        }
    }
    else {
        var arrChkBox = document.getElementsByName("chkTerm1[]");
        $(arrChkBox[rowIndex]).prop('checked', false);

        arrChkBox = document.getElementsByName("chkTerm2[]");
        $(arrChkBox[rowIndex]).prop('checked', false);

        if ($("#totalTermCount").val() == "3") {
            arrChkBox = document.getElementsByName("chkTerm3[]");
            $(arrChkBox[rowIndex]).prop('checked', false);
        }

        $('#StudentCodeSelectAll').prop('checked', false);
    }
}

function GetPlansByCurriculumId(id) {
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    $("#table-plan tbody tr").remove();
    PageMethods.GetPlansByCurriculumId(id, function (response) {
        $.each(response, function (e, s) {
            var educationLevels = (s.EducationLevels != "undefinded" && s.EducationLevels != null) ? s.EducationLevels.join(",   ") : "";
            $("#table-plan tbody").append(`"<tr><td class="center">` + (e + 1) + `</td>
                            <td>`+ s.PlanName + `</td>
                            <td style="word-wrap: break-word;">` + educationLevels + `</td>
                            <td style="width:auto">
                                <div class="dropdown">
                                        <button class="btn btn-primary dropdown-toggle success" type="button" data-toggle="dropdown">
                                            จัดการ/แก้ไข <span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu" style="font-size:24px;">
                                            <li><a onclick="(GetPlanDetailsByPlanId(`+ s.PlanId + `))">แก้ไข</a></li>
                                            <li><a onclick="(DeletePlanByPlanId(`+ s.PlanId + `,` + id + `))">ลบ</a></li>
                                        </ul>
                                 </div>
                            </td></tr>`);
        });
        CurriculumId = id;
        ShowDisplay(3);
        $.unblockUI();
    });
}

function createPlan() {
    $("#table-course-plan tbody tr").remove();
    $("#PlanName").val("");
    $("#select-sublevel").val("");
    $("#select-sublevel").attr("disabled", false);
    ShowDisplay(4);
    $("#select-sublevel2").val(null);
    $("#PlanId").val("0");
    $(".select2").select2();
}

function DeletePlanByPlanId(planId, curriculumId) {
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });

    PageMethods.DeletePlanByPlanId(planId, function (response) {

        if (response) {
            GetPlansByCurriculumId(curriculumId);
            $.unblockUI();
        }
        else {
            $.unblockUI();
            popupError("ปิดการใช้งานหลักสูตรทั้งหมดและลบแผน");
        }
    });
}

function GetPlanDetailsByPlanId(id) {
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    $("#table-course-plan tbody tr").remove();

    PageMethods.GetPlanDetailsByPlanId(id, function (response) {
        $("#PlanName").val(response.PlanName);
        $("#PlanId").val(response.PlanId);
        $("#select-sublevel").val(response.NTSubLevel);
        $("#select-sublevel").attr("disabled", "disabled");
        SetdataSublevel2(response.EducationSubLevelIds);

        $displayThree.planeData = response.PlanCourseDTOs;
        $.each($displayThree.planeData, function (e, s) {
            $displayThree.planeData[e]["RowsIndex"] = e;
        });

        $displayThree.RanderTable();
        $.unblockUI();
        ShowDisplay(4);
        $(".select2").select2();
    });
}

function SetdataSublevel2(ClassId) {

    var SubLVID = $('#select-sublevel option:selected').val();
    var sSubLV = $('#select-sublevel2 option:selected').text();
    $("#select-sublevel2 option").remove();
    $.ajax({
        url: "/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + SubLVID,
        success: function (msg) {

            $.each(msg, function (index) {
                $('select[id*=select-sublevel2]')
                    .append($("<option></option>")
                        .attr("value", msg[index].nTermSubLevel2)
                        .text(msg[index].nTSubLevel2));
            });
            $("#select-sublevel2").val(ClassId);
        }
    });
}

