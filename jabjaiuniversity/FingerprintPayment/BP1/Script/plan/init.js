var temp = new TemplateTable();
var pageSize = 20, pageNumber = 1;
var sortName = "", Order = true;
var wording = "", LevelId = "";

$(function () {
    listData();
    $("#aspnetForm").submit(function (e) {
        e.preventDefault();
    });

    $("#aspnetForm").validate({
        messages: {
            courseCode: "กรอก รหัสวิชา",
            SubjectName: "กรุณากรอก ชื่อวิชา",
            nTSubLevel: "กรุณาเลือกระดับชั้น",
            courseType: "กรุณาเลือกกลุ่มสาระการเรียนรู้",
            courseGroup: "กรุณาเลือกประเภทรายวิชา",
            nCredit: "กรุณาเลือกหน่วยกิต",
            courseHour: "กรุณากรอก จำนวนชั่วโมงเรียน/สัปดาห์",
            courseTotalHour: "กรุณากรอก จำนวนชั่วโมงเรียน/ปี"
        },
        tooltip_options: {
            courseCode: { placement: 'right' },
            SubjectName: { placement: 'right' },
            nTSubLevel: { placement: 'right' },
            courseType: { placement: 'right' },
            Fd_CustTaxID: { placement: 'right' },
            courseGroup: { placement: 'right' },
            nCredit: { placement: 'right' },
            courseHour: { placement: 'right' },
            courseTotalHour: { placement: 'right' }
        }
    });

    $("#btnAddPlan").click(function () {
        $("#model-data").find("input").val("");
        $("#model-data").find("select").val("");
        $("#model-data").find("input").tooltip('hide');
        $("#model-data").find("select").tooltip('hide');
        $("#model-data").modal({ backdrop: 'static', keyboard: false });
    });
    $("#btnModalSave").click(function () {
        if ($("form").valid()) {
            $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
            var data = {};
            $.map($("form").serializeArray(), function (s, key) {
                data[s['name']] = s['value'];
            });

            PageMethods.updateData(data, function (response) {
                console.log(response);
                $("#model-data").modal("hide");
                listData();
                $.unblockUI();
            });
        }
    });

    $("#select-level").change(function () {
        LevelId = $(this).val();
        pageNumber = 1;
        listData();
    });

    $("#pageNumber").change(function () {
        pageNumber = parseInt($(this).val());
        listData();
        document.documentElement.scrollTo(0, 0);
    });

    $("#pageSize").change(function () {
        pageNumber = 1;
        pageSize = parseInt($(this).val());
        listData();
        document.documentElement.scrollTo(0, 0);
    });

    $("#txtSearch").keypress(function (e) {
        if (e.keyCode === 13) {
            e.preventDefault();
            Search();
        }
    });

    $(".data-order").click(function () {
        Order = $(this).attr("data-order");
        $("th.data-order i").removeClass("fa fa-sort-asc");
        $("th.data-order i").removeClass("fa fa-sort-desc");
        if (sortName !== $(this).attr("data-sort") || Order === "false" || Order === "") {
            Order = true;
            $(this).find("i").addClass("fa fa-sort-desc");
        } else {
            Order = false;
            $(this).find("i").addClass("fa fa-sort-asc");
        }
        sortName = $(this).attr("data-sort");
        $(this).attr("data-order", Order);
        listData();
    });


});

function getData(id) {
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    PageMethods.getData(id, function (response) {
        $.map(response, function (v, k) {
            let e = $("#model-data").find("[id=" + k + "]");
            e.val(v);
        });
        $("#model-data").modal({ backdrop: 'static', keyboard: false });
        $.unblockUI();
    });
}

function deleteData(id) {
    $.confirm({
        title: '',
        content: "ท่านต้องการลบข้อมูลวิชาเรียน นี้หรือไม่",
        theme: 'bootstrap',
        buttons: {
            formSubmit: {
                text: '<span style=\"font-size: 20px;\" >ตกลง</span>',
                btnClass: 'pull-left btn btn-blue',
                action: function () {
                    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
                    PageMethods.deleteData(id, function (response) {
                        listData();
                        $.unblockUI();
                    });
                }
            },
            "<span style=\"font-size: 20px;\" >ยกเลิก</span>": function () {
            }
        }
    });
}

function changePage(stepPage) {
    if (stepPage === 1) pageNumber += 1;
    else pageNumber -= 1;
    listData();
    document.documentElement.scrollTo(0, 0);
}

function Search() {
    wording = $("#txtSearch").val();
    pageNumber = 1;
    listData();
}

function listData() {
    $("#target tr").remove();
    var data = {
        "wording": wording, "pageSize": pageSize, "pageNumber": pageNumber,
        sortName: sortName, sorting: Order, levelId: LevelId
    };
    $.blockUI({ message: '<h1>กำลังดำเนินการกรุณารอสักครู่</h1>' });
    PageMethods.listData(data, function (response) {

        temp.PageSetting({ 'pageSize': pageSize, 'pageNumber': pageNumber });
        temp.TemplateSetting({ template_id: "#tmpl-mustache", target_name: "#target" });
        temp.RenderRows(response);
        $.unblockUI();
    });
}