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
    'stopCountingActiveAjaxConnections': false
};

$(document).ready(function () {

    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/Common/GetYears";
    params['requestType'] = "GET";
    params['successCallbackFunction'] = BindYear;
    doAjax(params);

   
});

function onSelectedYear() {
    $.get("<%=Page.ResolveUrl("~/api/exam / GetTermByYear")%>?year=" + $("#ddlYear").val(), function (data) {

        $("#ddlTerm").empty();

        $("<option></option>", {
            value: "",
            text: "ทั้งหมด"
        }).appendTo("#ddlTerm");

        $.each(data, function (index, item) {

            $("<option></option>", {
                value: item.value,
                text: item.text
            }).appendTo("#ddlTerm");
        });

        $("#ddlTerm").selectpicker("refresh");
    });

    var params = $.extend({}, doAjax_params_default);
    params['url'] = "../../api/Common/GetCumulativeTermsByYear?=" + $("#ddlYear").val();
    params['requestType'] = "GET";
    params['successCallbackFunction'] = BindTerms;
    doAjax(params);
}
function BindTerm(response) {

}
function BindYear(response) {
    $("#ddlYear").empty();
    $("#ddlYear option").remove();
    $("#ddlYear").append('<option value="">เลือกปี</option>');
    $(response).each(function (index, value) {
        $("#ddlYear").append('<option value="' + value.NYear + '">' + value.NumberYear + '</option>');
    });
}

function isNumberKey(e, control) {
    var $this = $(control);
    var text = $(control).val();
    var controlName = $(control).attr('name');
    var keyPressed = 0;


    if (($(control).attr('name') != 'undefined' || $(control).attr('name') != null) && ((controlName == "GetSamattana" || controlName == "GetBehaviorLabel" || controlName == "GetReadWrite") && event.which == 46)) {
        event.preventDefault();
    }
    else if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
        ((event.which < 48 || event.which > 57) &&
            (event.which != 0 && event.which != 8))) {
        event.preventDefault();
    }
    else if ((event.which == 46) && (text.indexOf('.') == -1)) {
        setTimeout(function () {

            if ($(control).val().substring($(control).val().indexOf('.')).length > 3) {
                $(control).val($(control).val().substring(0, $(control).val().indexOf('.') + 3));
            }
        }, 1);
    }
    else if ((text.indexOf('.') != -1) &&
        (text.substring(text.indexOf('.')).length > 2) &&
        (event.which != 0 && event.which != 8) &&
        ($(control)[0].selectionStart >= text.length - 2)) {
        event.preventDefault();
    }
    else if (event.ctrlKey == true && (event.which == '118' || event.which == '86')) {
        event.preventDefault();
    }
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
    var stopCountingActiveAjaxConnections = doAjax_params['stopCountingActiveAjaxConnections'];
    //make sure that url ends with '/'
    /*if(!url.endsWith("/")){
     url = url + "/";
    }*/

    $.ajax({
        url: url,
        crossDomain: false,
        type: requestType,
        contentType: contentType,
        dataType: dataType,
        data: data,
        beforeSend: function (jqXHR, settings) {

            //requestCount++;
            if (typeof beforeSendCallbackFunction === "function") {

                beforeSendCallbackFunction();
            }
        },
        success: function (data, textStatus, jqXHR) {
            console.log("Success");
            if (typeof successCallbackFunction === "function") {
                successCallbackFunction(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log("Ajax Error" + errorThrown);
            if (errorThrown == "Request Timeout") {
                ShowPageSessionTimeOutAlert();
            }
            else {
                ShowPageError();
            }

        },
        complete: function (jqXHR, textStatus) {
            if (typeof completeCallbackFunction === "function") {
                completeCallbackFunction();
            }
        }
    });
}
