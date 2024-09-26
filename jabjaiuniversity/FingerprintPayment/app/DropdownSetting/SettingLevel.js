function SettingLevel() {
    $.get("/App_Logic/dataJSONArray.ashx?mode=sublevel", "", function (msg) {
        datalevel = msg;
        $('select[name=selectlevel]')
               .append($("<option></option>")
               .attr("value", "0")
               .text("- ทั้งหมด -"));
        $.each(msg, function (index) {
            $('select[name=selectlevel]')
                .append($("<option></option>")
                .attr("value", msg[index].SublevelId)
                .text(msg[index].SublevelName));
        })
    }).done(function () {
        getListSubLV2();
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

        $.each(dataSublevel, function (index) {
            var datalevel2 = dataSublevel[index].Level2;
            $('select[name=selectsublevel]')
                .append($("<option></option>")
                .attr("value", "0")
                .text("- ทั้งหมด -"));
            $.each(datalevel2, function (indexlevel2) {
                $('select[name=selectsublevel]')
               .append($("<option></option>")
               .attr("value", datalevel2[indexlevel2].Sublevel2Id)
               .text(datalevel2[indexlevel2].Sublevel2Name));
            });
        })
    });
}

function getListSubLV2() {
    var SubLVID = $('select[name=selectlevel]').val();
    var sSubLV = $('select[name=selectlevel]').text();
    $('select[name=selectsublevel] option').remove();
    $.ajax({
        url: "/App_Logic/dataJSONArray.ashx?mode=listsublevel2&SubLevel=" + SubLVID,
        success: function (msg) {
            $('select[name=selectsublevel]')
             .append($("<option></option>")
             .attr("value", "0")
             .text("- ทั้งหมด -"));
            $.each(msg, function (index) {
                $('select[name=selectsublevel]')
                    .append($("<option></option>")
                        .attr("value", msg[index].values)
                        .text(msg[index].text));
            });
        }
    }).done(function () {
        getliststudent();
    });
}

