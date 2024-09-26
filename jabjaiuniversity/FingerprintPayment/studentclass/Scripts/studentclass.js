var functionTab1 = function () {
    let MoveCount = 0;
    let Tab1 = $("#tab1");
    let studentNumber = $(Tab1).find(".student-number");
    let $select_year = $(Tab1).find(".select-year");

    let $select_term = $(Tab1).find(".select-term");

    let $select_class = $(Tab1).find(".select-class");
    let $select_room = $(Tab1).find(".select-room ");

    $select_year.val("");
    $select_class.val("");
    $select_room.val("");
    let Dataupdate = [];

    $(tab1).find("button").click(function () {
        //console.log($("select[multiple][group-data=2] option").length);
        if (parseInt(studentNumber.html()) !== $("select[group-data=2][multiple] option").length) MoveCount += 1;
        studentNumber.html($("select[group-data=2][multiple] option").length);
        Dataupdate = [];
        for (i = 1; i <= 2; i++) {
            var TermId = $(".select-term[group-data=" + i + "]").val();
            var RoomId = $(".select-room[group-data=" + i + "]").val();
            var StudentId = [];
            $.each($("select[group-data=" + i + "][multiple] option"), function (e, s) {
                StudentId.push($(s).val());
            });
            Dataupdate.push({ ClassroomId: RoomId, TermId: TermId, StudentId: StudentId });
        }
    });

    $($select_term[0]).change(function () {
        $($select_term[1]).val($($select_term[0]).val());
    });

    $(tab1).find(".btn-success").click(function () {
        if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลชั้นเรียน");
        } else if ($($select_term[0]).val() === "" || $($select_term[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลภาคการศึกษา");
        } else {
            if (MoveCount > 0) {
                eventDataBases.updateData(Dataupdate);
                MoveCount = 0;
            }
            else {
                popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลที่ต้องการทำการย้าย");
            }
        }
    });

    $($select_year[0]).change(function () {
        $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $(Tab1).find(".select-year[group-data=1]").val(), function (reuslt) {
            $(Tab1).find(".select-term[group-data] option").remove();
            $(Tab1).find(".select-term[group-data]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกภาคการศึกษา"));
            $.each(reuslt, function (e, s) {
                $(Tab1).find(".select-term[group-data]")
                    .append($("<option></option>")
                        .attr("value", s.nTerm)
                        .text(s.sTerm));
            });
        });
        $(Tab1).find(".select-year[group-data=2]").val($(Tab1).find(".select-year[group-data=1]").val());
    });

    $select_class.change(function () {
        $.get("/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + $(".select-class[group-data=1]").val(), function (reuslt) {
            $(Tab1).find(".select-room[group-data] option").remove();
            $(Tab1).find(".select-room[group-data]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกห้องเรียน"));
            $.each(reuslt, function (e, s) {
                $(Tab1).find(".select-room[group-data]")
                    .append($("<option></option>")
                        .attr("value", s.nTermSubLevel2)
                        .text(s.nTSubLevel2));
            });
        });

        $(Tab1).find(".select-class[group-data=2]").val($(Tab1).find(".select-class[group-data=1]").val());
    });

    $($select_room).change(function () {
        if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
            $('.jp-multiselect-1').find("button").attr("disabled", true);
        } else {
            $('.jp-multiselect-1').find("button").attr("disabled", false);
        }
    });

    $($select_room[0]).change(function () {

        if (MoveCount > 0 && $($select_room[1]).val() !== "") {
            $.confirm({
                title: '<h3>แจ้งเตือน</h3>',
                content: '<h3>ท่านต้องการบันทึกข้อมูลหรือไม่</h3>',
                theme: 'bootstrap',
                buttons: {
                    "<span style=\"font-size: 20px;\">ตกลง</span>": function () {
                        eventDataBases.updateData(Dataupdate);
                        eventDataBases.getStudentData(1);
                        MoveCount = 0;
                    }, "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                        eventDataBases.getStudentData($($select_room[0]).attr("group-data"));
                        eventDataBases.getStudentData($($select_room[1]).attr("group-data"));
                        MoveCount = 0;
                    }
                }
            });
        } else {
            eventDataBases.getStudentData($(this).attr("group-data"));
        }
    });

    $($select_room[1]).change(function () {
        var TermId = $(".select-term[group-data=2]").val();
        var RoomId = $(".select-room[group-data=2]").val();
        var StudentId = [];
        var data = { ClassroomId: RoomId, TermId: TermId, StudentId: null };

        Tab1.find("select[group-data=2][multiple]").find("option").remove();
        PageMethods.getDataHistory(data, function (result) {
            var data = $.parseJSON(result);
            StundetCount = data.length;
            $.each(data, function (e, s) {
                Tab1.find("select[group-data=2][multiple]")
                    .append($("<option></option>")
                        .attr("value", s.studentId)
                        .text(s.studentName));
            });
            studentNumber.html(data.length);
        });
    });
};

var functionTab2 = function () {
    let MoveCount = 0;
    let Tab2 = $("#tab2");
    let studentNumber = Tab2.find(".student-number");
    let $select_year = Tab2.find(".select-year");

    let $select_term = Tab2.find(".select-term");

    let $select_class = Tab2.find(".select-class");
    let $select_room = Tab2.find(".select-room");
    let leftData = {}, rightData = {};

    $select_year.val("");
    $select_class.val("");
    $select_room.val("");
    let Dataupdate = {};

    $(Tab2).find("button").click(function () {
        //console.log($("select[multiple][group-data=4] option").length);
        studentNumber.html($("select[group-data=4][multiple] option").length);
        var TermId = Tab2.find('[id=termId]').val();
        var RoomId = $(".select-room[group-data=4]").val();
        var StudentId = [];

        $.each($("select[group-data=4][multiple] option"), function (e, s) {
            StudentId.push($(s).val());
        });

        Dataupdate["ClassroomId"] = RoomId;
        Dataupdate["TermId"] = TermId;
        Dataupdate["StudentId"] = StudentId;
        MoveCount += 1;
    });

    $(Tab2).find(".btn-success").click(function () {
        if ($(".select-class[group-data=4]").val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลชั้นเรียน");
        } else {
            if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
                popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลชั้นเรียน");
            } else if ($($select_term[0]).val() === "" || $($select_term[1]).val() === "") {
                popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลภาคการศึกษา");
            } else {
                if (MoveCount > 0) {
                    eventDataBases.updateTerm(Dataupdate);
                    MoveCount = 0;
                } else {
                    popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลที่ต้องการเลื่อนชั้น");
                }
            }
        }
    });

    $select_year.change(function () {
        GetHistoryUpClass({ leftData: true, rightData: true });
        if (Dataupdate !== {}) {
            var StudentId = [];
            $.each($("select[group-data=4][multiple] option"), function (e, s) {
                StudentId.push($(s).val());
            });
            rightData["StudentId"] = StudentId;
            //eventDataBases.updateTerm(rightData);
            MoveCount = 0;
        }
        eventDataBases.getTermData($(this).attr("group-data"));
    });

    $select_term.change(function () {
        Dataupdate['ClassroomId'] = $(".select-room[group-data=3]").val();
        Dataupdate['TermId'] = $(".select-term[group-data=3]").val();
        Dataupdate['StudentId'] = [];
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        PageMethods.getNextTerm(Dataupdate, function (result) {
            //var data = $.parseJSON(result);
            console.log(result);
            if (result !== null) {
                Tab2.find('[id=text-term]').html(result.sTerm);
                Tab2.find('[id=termId]').val(result.nTerm);
                Tab2.find('[id=text-year]').html(result.nYear);
                GetHistoryUpClass({ leftData: true, rightData: true });
            }
            else {
                popupError("กรุณาเพิ่มข้อมูลปีการศึกษา");
                $.unblockUI();
            }
        }, function () {
            $.unblockUI();
        });
    });

    $select_class.change(function () {
        eventDataBases.getSublevelData($(this).attr("group-data"));
    });

    $($select_room[0]).change(function () {
        leftData["ClassroomId"] = $(".select-room[group-data=3]").val();
        GetHistoryUpClass({ leftData: true, rightData: false });
    });

    $($select_room).change(function () {
        if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
            $('.jp-multiselect-2').find("button").attr("disabled", true);
        } else {
            $('.jp-multiselect-2').find("button").attr("disabled", false);
        }
    });

    $($select_room[1]).change(function () {
        if (MoveCount > 0 && $(".select-class[group-data=4]").val() !== "") {
            CheckUpdata();
            leftData["ClassroomId"] = $(".select-room[group-data=3]").val();
            rightData["ClassroomId"] = $(".select-room[group-data=4]").val();
            //GetHistoryUpClass({ leftData: false, rightData: true });

        } else {
            leftData["ClassroomId"] = $(".select-room[group-data=3]").val();
            rightData["ClassroomId"] = $(".select-room[group-data=4]").val();
            GetHistoryUpClass({ leftData: false, rightData: true });
        }
    });

    function GetHistoryUpClass(option) {
        rightData["TermId"] = Tab2.find('[id=termId]').val();
        leftData["TermId"] = $(".select-term[group-data=3]").val();
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        PageMethods.GetHistoryUpClass(leftData, rightData, function (result) {
            var data = $.parseJSON(result);
            console.log(data);
            if (option.leftData) {
                $("select[group-data=3][multiple] option").remove();
                $.each(data.leftData, function (e, s) {
                    $("select[group-data=3][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });
            }
            if (option.rightData) {
                $("select[group-data=4][multiple] option").remove();
                $.each(data.rightData, function (e, s) {
                    $("select[group-data=4][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });
                studentNumber.html(data.rightData.length);
            }
            $.unblockUI();
            return data;
        }, function () {
            $.unblockUI();
        });
    }

    function CheckUpdata() {
        $.confirm({
            title: '<h3>แจ้งเตือน</h3>',
            content: '<h3>ท่านต้องการบันทึกข้อมูลหรือไม่</h3>',
            theme: 'bootstrap',
            buttons: {
                "<span style=\"font-size: 20px;\">ตกลง</span>": function () {
                    eventDataBases.updateData(Dataupdate);
                    Dataupdate = {};
                    GetHistoryUpClass();
                    MoveCount = 0;
                }, "<span style=\"font-size: 20px;\">ปิด</span>": function () {
                    GetHistoryUpClass();
                    MoveCount = 0;
                }
            }
        });
    }
};

var functionTab3 = function () {
    let MoveCount = 0;
    let $fn = $("#tab3");
    let studentNumber = $fn.find(".student-number");
    let $select_year = $fn.find(".select-year");

    let $select_term = $fn.find(".select-term");

    let $select_class = $fn.find(".select-class");
    let $select_room = $fn.find(".select-room");

    $select_year.val("");
    $select_class.val("");
    $select_room.val("");

    $fn.find("button").click(function () {
        console.log($("select[multiple][group-data=6] option").length);
        studentNumber.html($("select[group-data=6][multiple] option").length);
        MoveCount += 1;
    });

    $fn.find(".btn-success").click(function () {
        if (parseInt($(".select-year[group-data=5]").val()) === "" && parseInt($(".select-term[group-data=5]").val()) === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาตรวจข้อมูลอีกครั้ง");
        } else if ($("#dayGraduate").val() === "") {
            popupError("กรุณากรอกข้อมูลวันที่จบการศึกษา");
        } else if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลชั้นเรียน");
        } else if ($($select_term[0]).val() === "" || $($select_term[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลภาคการศึกษา");
        }
        else if (MoveCount > 0) {
            var TermId = $(".select-term[group-data=5]").val();
            var RoomId = $(".select-room[group-data=5]").val();
            var StudentId = [];
            var data = {};
            $.each($("select[group-data=6][multiple] option"), function (e, s) {
                StudentId.push($(s).val());
            });
            data['ClassroomId'] = RoomId;
            data['TermId'] = TermId;
            data['StudentId'] = StudentId;
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            //let dayGraduate = $("#dayGraduate").val().split("/");
            let dayGraduate = $("#dayGraduate").val();
            let dayProfessionalStandard = null;
            if ($(".select-class[group-data=5] option:selected").attr("dayProfessionalStandard") === "true") {
                let _date = $("#dayProfessionalStandard").val().split("/");
                if (_date.length == 3) {
                    dayProfessionalStandard = _date[1] + "/" + _date[0] + "/" + _date[2];
                }
            }

            PageMethods.updateGraduate(data, dayGraduate, dayProfessionalStandard, function (result) {
                $.unblockUI();
                popupError("บันทึกจบเสร็จเรียบร้อยแล้ว");
                MoveCount = 0;
            });
        }
    });

    $($select_year).change(function () {
        $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $fn.find(".select-year[group-data=5]").val(), function (reuslt) {
            $fn.find(".select-term[group-data] option").remove();
            $fn.find(".select-term[group-data]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกภาคการศึกษา"));
            $.each(reuslt, function (e, s) {
                $fn.find(".select-term[group-data]")
                    .append($("<option></option>")
                        .attr("value", s.nTerm)
                        .text(s.sTerm));
            });
        });
        $fn.find(".select-year[group-data=6]").val($fn.find(".select-year[group-data=5]").val());
    });

    $($select_term).change(function () {
        $($select_term[1]).val($(this).val());
    });

    $select_class.change(function () {
        $fn.find("select[group-data][multiple]").find("option").remove();
        eventDataBases.getSublevelData($(this).attr("group-data"));
        $($select_class[1]).val($(this).val());
        if ($(this).find("option:selected").attr("dayProfessionalStandard") === "true") {
            $("[target=dayProfessionalStandard]").show();
        } else {
            $("[target=dayProfessionalStandard]").hide();
        }
    });

    $($select_room).change(function () {
        var groupId = $(this).attr("group-data");
        var TermId = $(".select-term[group-data=5]").val();
        var RoomId = $(".select-room[group-data=5]").val();
        var StudentId = [];
        var data = { ClassroomId: RoomId, TermId: TermId, StudentId: null };

        $fn.find("select[group-data][multiple]").find("option").remove();
        if (RoomId !== null) {
            PageMethods.historyGraduate(data, function (result) {
                var data = $.parseJSON(result);
                console.log(data);

                $.each(data.leftData, function (e, s) {
                    $fn.find("select[group-data=5][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });

                $.each(data.rightData, function (e, s) {
                    $fn.find("select[group-data=6][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });

                studentNumber.html(data.rightData.length);
            });
        }
    });
};

var functionTab4 = function () {
    let MoveCount = 0;
    let $fn = $("#tab4");
    let studentNumber = $fn.find(".student-number");
    let $select_year = $fn.find(".select-year");

    let $select_term = $fn.find(".select-term");

    let $select_class = $fn.find(".select-class");
    let $select_room = $fn.find(".select-room");

    $select_year.val("");
    $select_class.val("");
    $select_room.val("");

    $fn.find("button").click(function () {
        console.log($("select[multiple][group-data=6] option").length);
        studentNumber.html($("select[group-data=6][multiple] option").length);
        MoveCount += 1;
    });

    $fn.find(".btn-success").click(function () {
        if (parseInt($(".select-year[group-data=5]").val()) === "" && parseInt($(".select-term[group-data=5]").val()) === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาตรวจข้อมูลอีกครั้ง");
        } else if ($("#dayGraduate").val() === "") {
            popupError("กรุณากรอกข้อมูลวันที่จบการศึกษา");
        } else if ($($select_room[0]).val() === "" || $($select_room[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลชั้นเรียน");
        } else if ($($select_term[0]).val() === "" || $($select_term[1]).val() === "") {
            popupError("ไม่สามารถบันทึกช้อมูลได้กรุณาเลือกข้อมูลภาคการศึกษา");
        }
        else if (MoveCount > 0) {
            var TermId = $(".select-term[group-data=5]").val();
            var RoomId = $(".select-room[group-data=5]").val();
            var StudentId = [];
            var data = {};
            $.each($("select[group-data=6][multiple] option"), function (e, s) {
                StudentId.push($(s).val());
            });
            data['ClassroomId'] = RoomId;
            data['TermId'] = TermId;
            data['StudentId'] = StudentId;
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            //let dayGraduate = $("#dayGraduate").val().split("/");
            let dayGraduate = $("#dayGraduate").val();
            let dayProfessionalStandard = $("#dayProfessionalStandard").val().split("/");
            PageMethods.updateGraduate(data, dayGraduate, function (result) {
                $.unblockUI();
                popupError("บันทึกจบเสร็จเรียบร้อยแล้ว");
                MoveCount = 0;
            });
        }
    });

    $($select_year).change(function () {
        $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $fn.find(".select-year[group-data=5]").val(), function (reuslt) {
            $fn.find(".select-term[group-data] option").remove();
            $fn.find(".select-term[group-data]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกภาคการศึกษา"));
            $.each(reuslt, function (e, s) {
                $fn.find(".select-term[group-data]")
                    .append($("<option></option>")
                        .attr("value", s.nTerm)
                        .text(s.sTerm));
            });
        });
        $fn.find(".select-year[group-data=6]").val($fn.find(".select-year[group-data=5]").val());
    });

    $($select_term).change(function () {
        $($select_term[1]).val($(this).val());
    });

    $select_class.change(function () {
        $fn.find("select[group-data][multiple]").find("option").remove();
        eventDataBases.getSublevelData($(this).attr("group-data"));
        $($select_class[1]).val($(this).val());
    });

    $($select_room).change(function () {
        var groupId = $(this).attr("group-data");
        var TermId = $(".select-term[group-data=5]").val();
        var RoomId = $(".select-room[group-data=5]").val();
        var StudentId = [];
        var data = { ClassroomId: RoomId, TermId: TermId, StudentId: null };

        $fn.find("select[group-data][multiple]").find("option").remove();
        if (RoomId !== null) {
            PageMethods.historyGraduate(data, function (result) {
                var data = $.parseJSON(result);
                console.log(data);

                $.each(data.leftData, function (e, s) {
                    $fn.find("select[group-data=5][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });

                $.each(data.rightData, function (e, s) {
                    $fn.find("select[group-data=6][multiple]")
                        .append($("<option></option>")
                            .attr("value", s.studentId)
                            .text(s.studentName));
                });

                studentNumber.html(data.rightData.length);
            });
        }
    });
};

var EventDataBases = function () {
    this.getSublevelData = function (controlId) {
        $.get("/App_Logic/dataGeneric.ashx?mode=listsublevel2&nhol=" + $(".select-class[group-data=" + controlId + "]").val(), function (reuslt) {
            $(".select-room[group-data=" + controlId + "] option").remove();
            $(".select-room[group-data=" + controlId + "]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกห้องเรียน"));
            $.each(reuslt, function (e, s) {
                $(".select-room[group-data=" + controlId + "]")
                    .append($("<option></option>")
                        .attr("value", s.nTermSubLevel2)
                        .text(s.nTSubLevel2));
            });
        });
    };

    this.getTermData = function (controlId) {
        $.get("/App_Logic/dataGeneric.ashx?mode=listterm&id=" + $(".select-year[group-data=" + controlId + "]").val(), function (reuslt) {
            $(".select-term[group-data=" + controlId + "] option").remove();
            $(".select-term[group-data=" + controlId + "]")
                .append($("<option></option>")
                    .attr("value", "")
                    .text("เลือกภาคการศึกษา"));
            $.each(reuslt, function (e, s) {
                $(".select-term[group-data=" + controlId + "]")
                    .append($("<option></option>")
                        .attr("value", s.nTerm)
                        .text(s.sTerm));
            });
        });
    };

    this.getStudentData = function (GroupId) {
        var TermId = $(".select-term[group-data=" + GroupId + "]").val();
        var RoomId = $(".select-room[group-data=" + GroupId + "]").val();
        var StudentId = [];
        var data = { ClassroomId: RoomId, TermId: TermId, StudentId: null };

        $("select[group-data=" + GroupId + "][multiple]").find("option").remove();
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        PageMethods.getDataHistory(data, function (result) {
            var data = $.parseJSON(result);
            $.each(data, function (e, s) {
                $("select[group-data=" + GroupId + "][multiple]")
                    .append($("<option></option>")
                        .attr("value", s.studentId)
                        .text(s.studentName));
            });
            $.unblockUI();
        }, function () {
            $.unblockUI();
        });
    };

    this.updateData = function (data) {
        $.confirm({
            title: '',
            content: '' +
                '<form action="" class="formName">' +
                '<div class="form-group">' +
                '<h3>วันที่ย้ายห้อง</h3>' +
                '<input type="text" placeholder="DD/MM/YYYY" class="name form-control datepicker col-md-8" style=\"font-size:20px;\" required />' +
                '</div>' +
                '</form>',
            theme: 'bootstrap',
            buttons: {
                formSubmit: {
                    text: '<span style=\"font-size: 20px;\" >บันทึก</span>',
                    btnClass: 'pull-left btn btn-blue',
                    action: function () {
                        var name = this.$content.find('.name').val().split("/");
                        var note = this.$content.find('textarea').val();
                        if (name !== "") {
                            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
                            PageMethods.CheckScoreEntered(data[0], data[1], name[1] + "/" + name[0] + "/" + name[2], function (e) {
                                if (e.StatusCode === "200") {
                                    updateDataHistory(data[0], data[1], name[1] + "/" + name[0] + "/" + name[2]);
                                } else if (e.StatusCode === "401") {
                                    var message = "นักเรียนคนนี้มีคะแนนที่กรอกไปแล้วในห้องปัจจุบัน หากทำการย้ายห้องระหว่างเทอม ระบบจะย้ายคะแนนตามไปในห้องใหม่ด้วยอัติโนมัติ โดยมีเงื่อนไขดังนี้ </br>" +
                                        "1. ระบบสามารถย้ายคะแนนไปห้องใหม่ได้ในกรณีที่มีรหัสวิชาเรียนเหมือนกันเท่านั้น รหัสวิชาที่ไม่พบในห้องใหม่ คะแนนจะถูกลบทิ้ง </br>" +
                                        "2. คะแนนจะถูกย้ายมาตามช่องที่กรอกไว้ <br> 3. สัดส่วนคะแนนและคะแนนเต็ม จะไม่ถูกย้ายมาด้วย";

                                    var courseCode = "";
                                    $(e.Result).each(function () {

                                        courseCode += "</br>" + this;

                                    });

                                    message = "<br>" + message + "</br>" + courseCode;

                                    $.confirm({
                                        title: '<h2>คำเตือน !</h>',
                                        content: '<h2>' + message + '</h>',
                                        boxWidth: '500px',
                                        useBootstrap: false,
                                        buttons: {
                                            cancel: {
                                                label: '<i class="fa fa-times"></i> ยกเลิก',
                                            },
                                            confirm: {
                                                label: '<i class="fa fa-check"></i> ใช่',
                                                action: function () {
                                                    updateDataHistory(data[0], data[1], name[1] + "/" + name[0] + "/" + name[2]);
                                                }
                                            }
                                        },
                                    });
                                } 
                                $.unblockUI();
                            });

                            //updateDataHistory(data[0], data[1], name[1] + "/" + name[0] + "/" + name[2]);
                            //PageMethods.updateDataHistory(data[0], data[1], name[1] + "/" + name[0] + "/" + name[2], note, function (e) {
                            //    //getStudentData(controlId);
                            //    //getStudentData(controlId + 1);
                            //    popupError("บันทึกการย้ายห้องเรียบร้อยแล้ว");
                            //    $.unblockUI();
                            //});
                        } else {
                            $.alert('กรุณากรอกวันที่ย้ายห้อง ');
                            return false;
                        }
                    }
                },
                "<span style=\"font-size: 20px;\" >ยกเลิก</span>": function () {
                    $.unblockUI();
                }
            }, onContentReady: function () {
                // bind to events
                $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    // if the user submits the form by pressing enter in the field.
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click'); // reference the button and click it
                });
            }
        });
    };

    function updateDataHistory(leftData, rightData, dateChange) {
        PageMethods.updateDataHistory(leftData, rightData, dateChange, function (e) {
            //getStudentData(controlId);
            //getStudentData(controlId + 1);
            console.log(e);
            if (e.StatusCode === "200") {
                popupError("บันทึกการย้ายห้องเรียบร้อยแล้ว");
            } else if (e.StatusCode === "401") {
                popupError(e.Message);
            } else {
                popupError("เกิดข้อผิดพลาดในระบบ");
                console.log(e.SystemErrorMessage);
            }
            //popupError("บันทึกการย้ายห้องเรียบร้อยแล้ว");
            $.unblockUI();
        });
    }

    this.updateTerm = function (data) {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        PageMethods.updateTerm(data, function (e) {
            //getStudentData(controlId);
            //getStudentData(controlId + 1);

            if (e.StatusCode === "200") {
                popupError("บันทึกเลื่อนชั้นเรียบร้อยแล้ว");
            } else if (e.StatusCode === "401") {
                //popupError("ท่านต้องมีเทอมอย่างน้อย 2 เทอมจึงจะสามารถทำการเลื่อนชั้นได้");
                popupError(e.Message);
            } else {
                popupError("เกิดข้อผิดพลาดในระบบ");
                console.log(e.SystemErrorMessage);
            }
            $.unblockUI();
        });
    };

    this.getStudentUpClass = function (data, GroupId) {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
        PageMethods.getStudentData(data, function (result) {
            var data = $.parseJSON(result);
            $("select[group-data=" + GroupId + "][multiple] option").remove();
            $.each(data, function (e, s) {
                $("select[group-data=" + GroupId + "][multiple]")
                    .append($("<option></option>")
                        .attr("value", s.studentId)
                        .text(s.studentName));
            });
            $.unblockUI();

            return data.length;
        }, function () {
            $.unblockUI();
        });
    };

};

$(document)
    .ajaxStart(function () {
        $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    })
    .ajaxStop(function () { $.unblockUI(); });

var eventDataBases = new EventDataBases();
$(function () {
    $(".jp-multiselect-1").jQueryMultiSelection();
    $(".jp-multiselect-2").jQueryMultiSelection();
    $(".jp-multiselect-3").jQueryMultiSelection();
    //$(".datepicker").datepicker();
    $('.datepicker').datetimepicker({
        keepOpen: false,
        debug: false,
        format: 'DD/MM/YYYY-BE',
        locale: 'th',
        icons: {
            time: "fa fa-clock-o",
            date: "fa fa-calendar",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: 'fa fa-chevron-left',
            next: 'fa fa-chevron-right',
            today: 'fa fa-screenshot',
            clear: 'fa fa-trash',
            close: 'fa fa-remove'
        }
    });
    $(".datepicker").attr('maxlength', '10');
    $('.jp-multiselect-1').find("button").attr("disabled", true);
    $('.jp-multiselect-2').find("button").attr("disabled", true);
    functionTab1();
    functionTab2();
    functionTab3();
});

function popupError(message) {
    $.confirm({
        title: '<h3>แจ้งเตือน</h3>',
        content: '<h3>' + message + '</h3>',
        theme: 'bootstrap',
        buttons: {
            "<span style=\"font-size: 20px;\">ปิด</span>": function () {
            }
        }
    });
}
