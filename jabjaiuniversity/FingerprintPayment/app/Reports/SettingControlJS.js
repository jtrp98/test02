
var availableValuestudent = [];
var availableValueEmployees = [];
var availableListUser = [];
var ArrayUser = "";
var ArrayEmp = "";

$(function () {
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
    $("select[id*=ddlSearch]").change(function () {
        switch ($(this).val()) {
            case "0":
                $(".type_row_0").addClass("hidden");
                $(".type_row_1").addClass("hidden");
                $(".type_row_2").addClass("hidden");
                break;
            case "1":
                $(".type_row_0").removeClass("hidden");
                $(".type_row_1").removeClass("hidden");
                $(".type_row_2").removeClass("hidden");
                break;
            case "2":
                $(".type_row_0").addClass("hidden");
                $(".type_row_1").addClass("hidden");
                $(".type_row_2").removeClass("hidden");
                break;
        }
    });

    $("select[name=selecttype]").change(function () {
        switch ($(this).val()) {
            case "0":
                $(".type_row_1").removeClass("hidden");
                $("input[name=sname]").val("");
                $("input[name=iduser]").val("");
                break;
            case "1":
                $(".type_row_1").addClass("hidden");
                $("input[name=sname]").val("");
                $("input[name=iduser]").val("");
                break;
        }
    });

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
    });;

    $.ajax({
        url: "/App_Logic/modalJSON.aspx?mode=teacher",
        dataType: "json",
        success: function (objjson) {
            availableValueEmployees = [];
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName + ' ' + objjson[index].sLastname,
                    value: objjson[index].sEmp
                };
                availableValueEmployees[index] = newObject;
            });
            //console.log(objjson);
        }
    });

    $("select[name=selectsublevel]").change(function () {
        $("input[name=sname]").val("");
        $("input[name=iduser]").val("");
        getliststudent();
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
        getliststudent();
    });
    AutocompleteUser();
    $("").datepicker({});
})

function getliststudent() {
    var nsublevel = $('select[name=selectsublevel]').val();
    var nelevel = $('select[name=selectlevel]').val()
    availableValuestudent = [];
    $.ajax({
        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" + nelevel + "&nsublevel=" + nsublevel,
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].sName,
                    value: objjson[index].sID
                };
                availableValuestudent[index] = newObject;
            });
        }
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

function AutocompleteUser() {
    $('input[name=sname]').autocomplete({
        width: 300,
        max: 10,
        delay: 100,
        minLength: 1,
        autoFocus: true,
        cacheLength: 1,
        scroll: true,
        highlight: false,
        source: function (request, response) {
            var results;
            if ($("select[name*=selecttype]").val() === "0") {
                results = $.ui.autocomplete.filter(availableValuestudent, request.term);
            } else if ($("select[name*=selecttype]").val() === "1" || $("select[id*=ddlSearch]").val() === "2") {
                results = $.ui.autocomplete.filter(availableValueEmployees, request.term);
            }
            response(results.slice(0, 10));
        },
        select: function (event, ui) {
            event.preventDefault();
            $("input[name=sname]").val(ui.item.label);
            $("input[name=iduser]").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("input[name=iduser]").val("");
        }
    });
}
