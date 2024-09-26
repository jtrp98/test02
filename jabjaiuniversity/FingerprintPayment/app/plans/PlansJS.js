
$(function () {
    $("#filter").filterControl({ level2: false });
    //$("#filter").append(Levelfilter());
    $.get("/App_Logic/dataJSONArray.ashx?mode=sublevel", "", function (msg) {
        datalevel = msg;
        $('select[name=selectlevel] option').remove();
        $.each(msg, function (index) {
            $('select[name=selectlevel]')
                .append($("<option></option>")
                    .attr("value", msg[index].SublevelId)
                    .text(msg[index].SublevelName));
        });
    }).done(function () {
        getListSubLV2();
    });

    $.get("/App_Logic/dataJSONArray.ashx?mode=year", "", function (msg) {
        datalevel = msg;
        $('select[id=year] option').remove();
        $.each(msg, function (index) {
            $('select[id=year]')
                .append($("<option></option>")
                    .attr("value", msg[index].values)
                    .text(msg[index].text));
        });
    }).done(function () {
        getTerm();
    });

    $("select[id=year]").change(function () {
        getTerm();
    });

    $("select[name=selectlevel]").change(function () {
        getListSubLV2();
    });

    $("#btnSearch").click(function () {
        $("body").mLoading();
        var Html = "";
        var termid = $('select[id=term] option:selected').val();
        var Level = $("select[name=selectlevel]").val();
        $.get("/App_Logic/dataJSONArray.ashx?mode=listroom&sublevel=" + Level, "", function (Result) {
            //Html += '<div class="row table table-striped plans-room-table"><div class="col-lg-1 col-md-1 col-sm-1"></div><div class="col-lg-4 col-md-4 col-sm-4" style="background:#286090; color: white;">ระดับชั้น</div><div class="col-lg-7 col-md-7 col-sm-7" style="background:#286090; color: white;">&nbsp;</div></div>';
            //$.each(Result, function (Index) {
            //    Html += '<div class="row">' +
            //        '<div class="col-lg-1 col-md-1 col-sm-1"></div>' +
            //        '<div class="col-lg-4 col-md-4 col-sm-4">' + Result[Index].lvname + '</div>' +
            //        '<div class="col-lg-7 col-md-7 col-sm-7 center">' +
            //        '<a href="plans-schedule.aspx?id=' + Result[Index].lv2id + '&amp;idterm=' + termid + '" class="btn btn-primary btn-link btn-plans-room btnpermission">จัดตารางเรียน</a>&nbsp;' +
            //        '<a href="plans-scheduledetail.aspx?id=' + Result[Index].lv2id + '&amp;idterm=' + termid + '" class="btn btn-info btn-link btn-plans-room"> ดูตารางเรียน</a> ' +
            //        '</div></div>';
            //});
            //$("#listroom").html(Html);

            $(this).removeClass("disabled");
            $(".employeeslist-container").hide();
            var template = $('#template').html();
            var data = [];
            Mustache.parse(template);   // optional, speeds up future uses
            var rendered = Mustache.render(template, { "listroom": Result, termid: termid });
            $('#listroom').html(rendered);
            $("body").mLoading("hide");
        });
    });
});

function getListSubLV2() {
    var SubLVID = $('select[name=selectlevel]').val();
    var sSubLV = $('select[name=selectlevel]').text();
    $('select[name=selectsublevel] option').remove();

    $('select[name=selectsublevel]')
          .append($("<option></option>")
          .attr("value", "")
          .text("ทั้งหมด"));

    $.ajax({
        url: "/App_Logic/dataJSONArray.ashx?mode=listsublevel2&SubLevel=" + SubLVID,
        success: function (msg) {

            $.each(msg, function (index) {
                $('select[name=selectsublevel]')
                    .append($("<option></option>")
                        .attr("value", msg[index].values)
                        .text(msg[index].text));
            });
        }
    }).done(function () {
    });
}

function getTerm() {
    var year = $('select[id=year]').val();
    $('select[id=term] option').remove();
    $.ajax({
        url: "/App_Logic/dataJSONArray.ashx?mode=term&year=" + year,
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[id=term]')
                    .append($("<option></option>")
                        .attr("value", msg[index].text)
                        .text(msg[index].values));
            });
        }
    }).done(function () {
    });
};

function CheckPlanCourseExist(control, nTermSubLevel2, nTerm, link) {
    console.log(nTermSubLevel2);
    console.log(nTerm);
    PageMethods.ValidatePlanCourseDeActivation(nTermSubLevel2, nTerm, function (response) {
       
        if (response == true) {
            console.log(response);

            //$(control).attr('target', '_blank').attr('href', link).trigger('click');

            window.open(
                link,
                '_blank' // <- This is what makes it open in a new window.
            );

            return false;
        }
        else {
            console.log(response);
            
            $.confirm({
                title: false,
                content: '<img src="../../images/alert_inbox.png" style="height: 100px;width: 100px;" class="center-block" /><br/><h1 class="text-center">' + 'ยังไม่มีการระบุแผนในชั้นเรียนนี้ กรุณาทำการตั้งค่าแผนจากเมนูจัดการแผน/หลักสูตร ก่อนใช้งาน' + '</h1>',
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
            return false;
        }
    });
    return false;
}