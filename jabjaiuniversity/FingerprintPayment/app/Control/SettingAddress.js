function settingprovince(provincecontorl) {
    var province = $('select[name*=' + provincecontorl + ']');
    $('select[name*=' + provincecontorl + '] option').remove();
    $.get("/App_Logic/dataJSONArray.ashx?mode=getprovince&sid=0", "", function (result) {
        $.each(result, function (index) {
            if (index === 0) {
                province.empty().append('<option selected="selected" value=\'' + result[index].PROVINCE_ID + '\'>' + result[index].PROVINCE_NAME + '</option>');
            } else {
                province.append($("<option></option>").val(result[index].PROVINCE_ID).html(result[index].PROVINCE_NAME));
            }
            //province.append($("<option></option>")
            //    .attr("value", result[index].PROVINCE_ID)
            //    .text(result[index].PROVINCE_NAME));
        });
    })
}

function settingaumper(provincecontorl, aumpercontrol) {
    var province_id = $('select[name*=' + provincecontorl + '] option:selected').val();
    var aumper = $('select[name*=' + aumpercontrol + ']');
    $('select[name*=' + aumpercontrol + '] option').remove();
    $.get("/App_Logic/dataJSONArray.ashx?mode=getaumper&sid=" + province_id, "", function (result) {
        $.each(result, function (index) {
            aumper.append($("<option></option>")
                .attr("value", result[index].AMPHUR_ID)
                .text(result[index].AMPHUR_NAME));
        });
    })
}

function settingtumbon(aumpercontrol, tumboncontrol) {
    var aumper_id = $('select[name*=' + aumpercontrol + '] option:selected').val();
    var tumbon = $('select[name*=' + tumboncontrol + ']');
    $('select[name*=' + tumboncontrol + '] option').remove();
    $.get("/App_Logic/dataJSONArray.ashx?mode=gettumbon&sid=" + aumper_id, "", function (result) {
        $.each(result, function (index) {
            tumbon.append($("<option></option>")
                .attr("value", result[index].DISTRICT_ID)
                .text(result[index].DISTRICT_NAME));
        });
    })
}