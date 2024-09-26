$(document).ready(function(){
    $("select[id$='v4ddlsactionid_update']").change(function() {
        var _stumbonid = $("select[id$='v4ddltumbonid']").attr("value");
        var _syear = $("select[id$='v4ddlsyear']").attr("value");
        var _sactionid = $("select[id$='v4ddlsactionid_update']").attr("value");
        var _splanid = $("select[id$='v4ddlsplanid']").attr("value");
        
        var str =xor_encrypt(_syear +'&'+ _stumbonid +'&'+ _sactionid +"&"+ _splanid,7);
        $.ajax({
                            type: "POST",
                            url: "UpdateBudget3.ashx",
                            cache : false,
                            dataType: "html",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                       var strresp = new Array(3);
                                       strresp=response.split('$');
                                       $("input[id$='v4txtnpaidperiod1']").val(strresp[0]);
                                       $("input[id$='v4txtnpaidperiod2']").val(strresp[1]);
                                       $("input[id$='v4txtnpaidperiod3']").val(strresp[2]);
                                      }
               });   

    });
    $("select[id$='v4ddlsplanid']").change(function() {
        $("select[id$='v4ddlsactionid']").html("");
        var _id = $("select[id$='v4ddlsplanid']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='v4ddlsactionid']").fadeOut();
        $('#loadingActionplan').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "JsonActionPlan.ashx",
                            cache : false,
                            dataType: "json",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $.each(response, function() {                        
                                            $("select[id$='v4ddlsactionid']").append($("<option></option>").val(this['svalue']).html(this['stxt']));
                                         });
                                         $('#loadingActionplan').fadeOut();
                                         $("select[id$='v4ddlsactionid']").fadeIn();
                                      }
               });   

    });
    $("select[id$='v4ddlspaygroupid']").change(function() {
        $("select[id$='v4ddltpayment']").html("");
        var _id = $("select[id$='v4ddlspaygroupid']").attr("value");
        _id = (_id != '') ? _id : "";
        $("select[id$='v4ddltpayment']").fadeOut();
        $('#loadingPayment').fadeIn();
        var str =xor_encrypt(_id,7);
        $.ajax({
                            type: "POST",
                            url: "JsonPayment.ashx",
                            cache : false,
                            dataType: "json",
                            data: {strQueryString : encodeURI(str)},
                            success:  function(response) {
                                         $.each(response, function() {                        
                                            $("select[id$='v4ddltpayment']").append($("<option></option>").val(this['svalue']).html(this['stxt']));
                                         });
                                         $('#loadingPayment').fadeOut();
                                         $("select[id$='v4ddltpayment']").fadeIn();
                                      }
               });   

    });
});

