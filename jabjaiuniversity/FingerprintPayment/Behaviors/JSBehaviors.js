
var availableValue = [];

$(function () {

    $('.datepicker').datepicker({ dateFormat: "dd/mm/yy", navigationAsDateFormat: true, showOtherMonths: true });

    getlistStd();
    //getlistemp();

    console.log(availableValue);

    $('#ctl00_MainContent_txtSearch').autocomplete({
        width: 300,
        max: 10,
        delay: 100,
        minLength: 1,
        autoFocus: true,
        cacheLength: 1,
        scroll: true,
        highlight: false,
        //source: function (request, response) {
        //    var results = $.ui.autocomplete.filter(availableValueplane, request.term);
        //    response(results.slice(0, 10));
        //},
        source: lightwell,
        select: function (event, ui) {
            event.preventDefault();
            $("#ctl00_MainContent_txtSearch").val(ui.item.label);
            //$("#txtId").val(ui.item.value);
            //$("#txtCode").val(ui.item.code);
            $code = ui.item.code;
        },
        focus: function (event, ui) {
            event.preventDefault();
            //$("#txtId").val("");
            //$("#txtCode").val("");
        }
    });

});


function getlistStd() {
    $.ajax({
        url: "/App_Logic/autoCompleteName.ashx?mode=GetStdList",
        dataType: "json",
        success: function (objjson) {
            //console.log(objjson);
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].Name,
                    value: objjson[index].StdId,
                    code: objjson[index].Code
                };
                availableValue.push(newObject);
            });
        }
    });
}


//function getlistemp() {
//    availableValue = [];
//    $.ajax({
//        url: "/App_Logic/autoCompleteName.ashx?mode=GetEmpList",
//        dataType: "json",
//        success: function (objjson) {
//            //console.log(objjson);
//            $.each(objjson, function (index) {
//                var newObject = {
//                    label: objjson[index].Name,
//                    value: objjson[index].EmpId,
//                    code: objjson[index].Phone
//                };
//                availableValue.push(newObject);
//            });
//        }
//    });
//}


function lightwell(request, response) {
    function hasMatch(s) {
        if (s === null) s = "";
        return s.toLowerCase().indexOf(request.term.toLowerCase()) !== -1;
    }
    var i, l, obj, matches = [];

    if (request.term === "") {
        response([]);
        return;
    }

    for (i = 0, l = availableValue.length; i < l; i++) {
        obj = availableValue[i];
        if (hasMatch(obj.label) || hasMatch(obj.code)) {
            matches.push(obj);
        }
    }
    response(matches.slice(0, 10));
}
