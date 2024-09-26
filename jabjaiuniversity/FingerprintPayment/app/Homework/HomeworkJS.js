var datalevel = [];
$(function () {
    jQuery.validator.addMethod("greaterThan", function (value, element, params) {
        if (value !== "") value = value.split('/')[1] + "/" + value.split('/')[0] + "/" + value.split('/')[2];
        if ($(params).val() !== "") params = $(params).val().split('/')[1] + "/" + $(params).val().split('/')[0] + "/" + $(params).val().split('/')[2];
        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) >= new Date(params);
        }

        return isNaN(value) && isNaN(params)
            || (Number(value) > Number(params));
    }, 'Must be greater than {0}.');

    $.get("/App_Logic/dataJSONArray.ashx?mode=listplane4homework", "", function (msg) {
        var dll = $('select[id*=ddlPlane]');
        $('select[id*=ddlPlane] option').remove();
        dll.append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));
        $.each(msg, function (index) {
            dll.append($("<option></option>")
                .attr("value", msg[index].planeid)
                .text(msg[index].planename));
        });
    });

    $("#aspnetForm").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
            ctl00$MainContent$ddlPlane: {
                required: true
            },
            dayNotification: { required: true },
            dayStart: { required: true },
            dayEnd: {
                required: true,
                greaterThan: "#dayStart"
            },
            homeworkdetail: { required: true }
        },
        // Specify validation error messages
        messages: {
            ctl00$MainContent$ddlPlane: { required: "กรุณาเลือกชื่อวิชา" },
            dayNotification: "กรุณากรอกวันที่แจ้งเตือน",
            dayStart: "กรุณากรอกวันที่เริ่มต้นส่งงาน",
            dayEnd: { required: "กรุณากรอกวันที่สิ้นสุดการส่งงาน", greaterThan: "กรุณากรอกวันที่สิ้นสุดมากกว่าวันที่เริ่มต้นส่งงาน" },
            homeworkdetail: "กรุณากรอกรายละเอียดการบ้าน"
        },
        tooltip_options: {
            ctl00$MainContent$ddlPlane: { placement: 'right', html: true },
            dayNotification: { placement: 'right', html: true },
            dayStart: { placement: 'right', html: true },
            dayEnd: { placement: 'right', html: true },
            homeworkdetail: { placement: 'right', html: true }
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            if ($("input[name=optradio]:checked").val() === "0" && ArrayLevel === "") {
                alert("กรุณาเลือกรายชื่อนักเรียน");
                return false;
            } else if ($("input[name=optradio]:checked").val() === "1" && ArrayUser === "") {
                alert("กรุณาเลือกรายชื่อนักเรียน");
                return false;
            }
            var files = $("#my-file-selector")[0].files;

            ///create a new FormData object
            var formData = new FormData(); //var formData = new FormData($('form')[0]);
            ///get the file and append it to the FormData object
            formData.append(files[0].name, files[0]);
            formData.append('mode', "homework");
            formData.append('planeid', $("#ctl00_MainContent_ddlPlane").val());
            formData.append('dayNotification', $("#dayNotification").val());
            formData.append('dayStart', $("#dayStart").val());
            formData.append('dayEnd', $("#dayEnd").val());
            formData.append('homeworkdetail', $("#homeworkdetail").val());
            formData.append('SendData', $("input[name=optradio]:checked").val());
            formData.append('ArrayLevel', ArrayLevel);
            formData.append('ArrayUser', ArrayUser);

            $.ajax({
                type: 'post',
                url: '/App_Logic/insertDataJSON.ashx',
                data: formData,
                success: function () {
                    window.location.href = "/homeworklist.aspx"
                },
                xhrFields: {
                    onprogress: function (progress) {
                        // calculate upload progress
                        var percentage = Math.floor((progress.total / progress.totalSize) * 100);
                        // log upload progress to console
                        console.log('progress', percentage);
                        if (percentage === 100) {
                            console.log('DONE!');
                        }
                    }
                },
                processData: false,
                contentType: false
            });
        }
    });

    $("select[name=selectlevel]").change(function () {
        var level = parseInt($(this).val());
        var dataSublevel = [];
        $("input[name=sname]").val("");
        $("input[name=iduser]").val("");
        dataSublevel = $.grep(datalevel, function (v) {
            return v.SublevelId === level;
        });

        var SubLVID = $('select[name=selectlevel]').val();
        var sSubLV = $('select[name=selectlevel]').text();
        $('select[name=selectsublevel] option').remove();
        $('select[name=selectsublevel]')
                        .append($("<option></option>")
                        .attr("value", "")
                        .text("ทั้งหมด"));

        $.each(dataSublevel, function (index) {
            var datalevel2 = dataSublevel[index].Level2;
            $.each(datalevel2, function (indexlevel2) {
                $('select[name=selectsublevel]')
               .append($("<option></option>")
               .attr("value", datalevel2[indexlevel2].Sublevel2Id)
               .text(datalevel2[indexlevel2].Sublevel2Name));
            });
        });
    });

    $("select[id*=ddlPlane]").change(function () {
        tabtimetype('myTabContent', 'tablelevel4homework&plane=' + $("select[id*=ddlPlane]").val());
        var SubLVID = $('select[name=selectlevel]').val();
        var sSubLV = $('select[name=selectlevel]').text();
        $('select[name=selectsublevel] option').remove();
        $('select[name=selectsublevel]')
                        .append($("<option></option>")
                        .attr("value", "")
                        .text("ทั้งหมด"));

        $('select[name=selectlevel] option').remove();
        $('select[name=selectlevel]')
                        .append($("<option></option>")
                        .attr("value", "")
                        .text("ทั้งหมด"));

        $.get("/App_Logic/dataJSONArray.ashx?mode=sublevel", "", function (msg) {
            datalevel = msg;
            $.each(msg, function (index) {
                $('select[name=selectlevel]')
                    .append($("<option></option>")
                    .attr("value", msg[index].SublevelId)
                    .text(msg[index].SublevelName));
            });
        });
        getliststudent();
    });

    $("select[name=selectsublevel]").change(function () {
        $("input[name=sname]").val("");
        $("input[name=iduser]").val("");
        getliststudent();
    });

    tabtimetype('myTabContent', '');
    var dateToday = new Date();
    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", minDate: dateToday });
    AutocompleteUser();
})