var doAjax_params_default = {
    'url': null,
    'requestType': "GET",
    'contentType': 'application/json; charset=utf-8',
    'dataType': 'json',
    'data': {},
    'beforeSendCallbackFunction': null,
    'successCallbackFunction': null,
    'completeCallbackFunction': null,
    'errorCallBackFunction': null,
};

$(document).ready(function () {
    //console.log("ready!");
    GetStudentGraduation();
});


function GetStudentGraduation() {

    var params = $.extend({}, doAjax_params_default);

    params['url'] = "../api/Graduation/GetStudentGraduation?nGradeId=" + "0" + "&assessmentTypeId=" + "0";
    params['requestType'] = "GET";
    params['successCallbackFunction'] = getStudentGraduation;
    doAjax(params);
}
var getStudentGraduation = function GetStudentGraduation(response) {
    console.log(response);
    console.log(response.length);
    console.log(s);
   
    //response.sid
    //$.each(response.FamilyProfileDTOs, function (index, Value) {
    //    console.log(Value.FatherFullName);
    //});

}

function doAjax(doAjax_params) {

    var url = doAjax_params['url'];
    var requestType = doAjax_params['requestType'];
    var contentType = doAjax_params['contentType'];
    var dataType = doAjax_params['dataType'];
    var data = doAjax_params['data'];
    var beforeSendCallbackFunction = doAjax_params['beforeSendCallbackFunction'];
    var successCallbackFunction = doAjax_params['successCallbackFunction'];
    var completeCallbackFunction = doAjax_params['completeCallbackFunction'];
    var errorCallBackFunction = doAjax_params['errorCallBackFunction'];

    //make sure that url ends with '/'
    /*if(!url.endsWith("/")){
     url = url + "/";
    }*/

    $.ajax({
        url: url,
        crossDomain: true,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {
            if (typeof beforeSendCallbackFunction === "function") {

                beforeSendCallbackFunction();
            }
        },
        success: function (data, textStatus, jqXHR) {
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Ajax Error" + errorThrown)
            if (typeof errorCallBackFunction === "function") {
                errorCallBackFunction(errorThrown);
            }

        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}