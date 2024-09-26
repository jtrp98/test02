$(document).ready(function () {

    GetData();

});


function GetData() {

    var href = window.location.href;
    var sID = href.split('/DiplomaPrint.aspx?');
    sID = sID[1];
    console.log(sID);

    $.ajax({
        url: "/Diploma/LogicDiplomaPrint.ashx?mode=GetData&sID="
            + sID,
        dataType: "json",
        success: function (objjson) {

            console.log(objjson);
            $('#stdName').text(objjson.fullName);
            $('#stdBd').text(objjson.day);
            $('#stdBm').text(objjson.month);
            $('#stdBy').text(objjson.year);

            $('#schoolName').text(objjson.schoolName);
            $('#schoolDistrict').text(objjson.schoolDistrict);
            $('#schoolProvince').text(objjson.schoolProvince);

            $('#dayGrad').text(objjson.dayGrad);
            $('#monthGrad').text(objjson.monthGrad);
            $('#yearGrad').text(objjson.yearGrad);
        }
    });
}