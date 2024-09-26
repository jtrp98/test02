$(function () {
    $("#filter").filterControl({ level2: true });
    //$("#filter").append(Levelfilter());
    $.get("/App_Logic/dataJSONArray.ashx?mode=sublevel", "", function (msg) {
        datalevel = msg;
        $('select[name=selectlevel] option').remove();
        $.each(msg, function (index) {
            $('select[name=selectlevel]')
                .append($("<option></option>")
                .attr("value", msg[index].SublevelId)
                .text(msg[index].SublevelName));
        })
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
        })
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
        var Html = "";
        var termid = $('select[id=term] option:selected').val();
        var Level = $("select[name=selectsublevel]").val();
        $.get("/App_Logic/dataJSONArray.ashx?mode=liststudent&sublevel2=" + Level, "", function (Result) {
            Html += '<div class="row table table-striped plans-room-table"><div class="col-lg-1 col-md-1 col-sm-1"></div><div class="col-lg-4 col-md-4 col-sm-4" style="background:#286090; color: white;">ระดับชั้น</div><div class="col-lg-7 col-md-7 col-sm-7" style="background:#286090; color: white;">&nbsp;</div></div>';
            $.each(Result, function (Index) {
                Html += '<div class="row">' +
                    '<div class="col-lg-1 col-md-1 col-sm-1"></div>' +
                    '<div class="col-lg-4 col-md-4 col-sm-4">' + Result[Index].studentname + '</div>' +
                    '<div class="col-lg-7 col-md-7 col-sm-7 center">' +
                    '<a href="/grade/grade-register.aspx?id=' + Result[Index].studentid + '&termid=' + termid + '" class="btn btn-primary btn-link btn-plans-room">จัดการผลการเรียน</a>&nbsp;' +
                    '<a href="/grade/grade-detail.aspx?id=' + Result[Index].studentid + '&termid=' + termid + '" class="btn btn-info btn-link btn-plans-room">ดูการผลการเรียน</a> ' +
                    '</div></div>';
            });
            $("#listroom").html(Html);
        });
    })
})

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
}