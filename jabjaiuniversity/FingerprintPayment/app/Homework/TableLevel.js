var ArrayLevel = "";

function tabtimetype(divsub, mode) {
    $('input[id*=txtListtime]').val("");
    $('#divmain').html("");
    $('#myTabContent').html("");
    $('#modalSet').html("");
    $('#modalsub').html("");
    $('#' + divsub).html("");
    var tabHTML = "";
    $.ajaxSetup({ cache: false });
    if (mode === null || mode === "") {
        tabHTML += '<table class="table table-striped" style="margin-bottom:0px;"><tr class="table-tab"><th style="width:70%">ระดับช่วงชั้น</th><th style="width:15%"></th></tr></table>';
        tabHTML += '<div style="overflow-y: scroll; height: 300px;"><table class="table table-striped table-select-class">';
        tabHTML += "</table></div></div>";
        $('#' + divsub).html(tabHTML);
    } else {
        $.ajax({
            type: "POST",
            url: "/App_Logic/dataJSONArray.ashx?mode=" + mode,
            cache: false,
            contentType: 'application/json;',
            success: function (objjson, status) {
                tabHTML += '<table class="table table-striped" style="margin-bottom:0px;"><tr class="table-tab"><th style="width:70%">ระดับช่วงชั้น</th><th style="width:15%"></th></tr></table>';
                tabHTML += '<div style="overflow-y: scroll; height: 300px;"><table class="table table-striped table-select-class">';
                $.each(objjson, function (index) {
                    var Sublevel2 = objjson[index].Sublevel2;

                    tabHTML += '<tr class="level-name"><td id="tdlevel' + objjson[index].SublevelId + '" >'
                    tabHTML += '<div id="tdlevel' + objjson[index].SublevelId + '" onclick="ShowSubLevel(' + objjson[index].SublevelId + ')" ><span class="glyphicon glyphicon-chevron-right"></span> ' + objjson[index].SublevelName + '</div><br/>'
                    if (Sublevel2.length > 0) {
                        tabHTML += '<table class="table table-striped" style="display:none;" id="tdsublevel' + objjson[index].SublevelId + '">';
                        $.each(Sublevel2, function (indexlevel2) {
                            tabHTML += '<tr><td style="width:70%">' + Sublevel2[indexlevel2].level2Name + '</td>'
                            tabHTML += '<td><input nHol="' + Sublevel2[indexlevel2].level2Id + '" type="checkbox" '
                            tabHTML += 'value="' + Sublevel2[indexlevel2].level2Id + '" termsub="yes" class="form-control termsub" id="txtsublv' + objjson[index].SublevelId + '_' + Sublevel2[indexlevel2].level2Id + '" '
                            tabHTML += ' onclick=GetListtime($(this).val(),$(this).is(":checked"),' + objjson[index].SublevelId + '); /></td>'
                            tabHTML += '</td></tr>';
                        });
                        tabHTML += "</table>";
                    }

                    tabHTML += '</td><td><div class="checkbox-container"><input type="checkbox" '
                    tabHTML += 'value="' + objjson[index].SublevelId + '" termsub="yes" class="form-control termsub" id="txtlv' + objjson[index].SublevelId + '" '
                    tabHTML += ' onclick=Allsublevel(' + objjson[index].SublevelId + ',$(this).is(":checked"));  />'
                    tabHTML += '</div></td>'
                    tabHTML += '</td></tr>';
                });
                tabHTML += "</table></div></div>";
                $('#' + divsub).html(tabHTML);
            }
        });
    }
}

function ShowSubLevel(level) {
    if ($('table[id=tdsublevel' + level + ']').css("display") === "none") {
        $('table[id=tdsublevel' + level + ']').css("display", "");
        $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
        $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
    } else {
        $('table[id=tdsublevel' + level + ']').css("display", "none");
        $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
        $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
    }
}

function GetListtime(val, addtime, lv) {
    if (addtime) {
        ArrayLevel += (ArrayLevel === "" ? "" : ",") + val;
    }
    else {
        var _replace = ArrayLevel;
        var _str = "";
        $.each(_replace.split(","), function (e, s) {
            if (s !== "" && val !== s) {
                _str += (_str === "" ? "" : ",") + s;
            }
        });
        ArrayLevel = _str;
    }
    var _ch = true;
    $('input[id*=txtsublv' + lv + '_]').each(function (e, subs) {
        if ($(subs).is(":checked") === false) {
            _ch = false;
        }
    });
    $('input[id=txtlv' + lv + ']').prop("checked", _ch)
}

function Allsublevel(val, addtime) {
    var _replace = ArrayLevel;
    var _str = "";
    var _ch = true;
    $.each(_replace.split(','), function (e, s) {
        $('input[id*=txtsublv' + val + '_]').each(function (e, subs) {
            if (s !== "" && $(subs).val() === s) {
                _ch = false;
            }
        });
        if (_ch && s !== "") {
            _str += s + ",";
        }
        _ch = true;
    });
    if (_str === NaN) _str = "";
    if (addtime) {
        $('input[id*=txtsublv' + val + '_]').each(function (e, subs) {
            _str += (_str === "" ? "" : ",") + $(subs).val();
        });
    }
    ArrayLevel = _str;
    $('input[id*=txtsublv' + val + '_]').prop("checked", addtime);
}
