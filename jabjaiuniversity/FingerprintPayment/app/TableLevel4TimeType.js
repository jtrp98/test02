$(function () {
    tabtimetype('000', 'divmain', 'myTabContent');
})

function getlevel(sid, _index, divsub) {
    var strHtml = "";
    $.ajax({
        type: "POST",
        url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listlevel",
        cache: false,
        contentType: 'application/json;',
        success: function (objlevel, status) {
            var tabHTML = "";
            var checked = "checked";
            if (_index === 0) {
                tabHTML += '<div class="tab-pane fade in active" id="tab' + sid + '" style="background:white;">';
            }
            else {
                tabHTML += '<div class="tab-pane fade" id="tab' + sid + '" style="background:white;">';
            }
            tabHTML += '<table class="table table-striped" style="margin-bottom:0px;"><tr class="table-tab"><th style="width:70%">ระดับช่วงชั้น</th><th style="width:15%"></th></tr></table>';
            tabHTML += '<div style="overflow-y: scroll; height: 300px;"><table class="table table-striped table-select-class">';
            $.each(objlevel, function (index) {
                var strsublevel = getsublevel(objlevel[index].nTLevel);
                if (strsublevel === undefined) {
                    strsublevel = "";
                }
                tabHTML += '<tr class="level-name"><td id="tdlevel' + objlevel[index].nTLevel + '" >'
                tabHTML += '<div id="tdlevel' + objlevel[index].nTLevel + '" onclick="ShowSubLevel(' + objlevel[index].nTLevel + ')" ><span class="glyphicon glyphicon-chevron-right"></span> ' + objlevel[index].LevelName + '</div><br/><br/>'
                tabHTML += '</td><td><div class="checkbox-container"><input type="checkbox" '
                tabHTML += 'value="' + objlevel[index].nTLevel + '" termsub="yes" class="form-control termsub" id="txtlv' + objlevel[index].nTLevel + '" '
                tabHTML += ' ' + checked + ' onclick=Allsublevel(' + objlevel[index].nTLevel + ',$(this).is(":checked"));  />' + strsublevel
                tabHTML += '</div></td>'
                tabHTML += '</td></tr>';
            })
            tabHTML += "</table></div></div>";
            $('#' + divsub).html($('#' + divsub).html() + tabHTML);
        }
    });
}

function getsublevel(sid) {
    var strHtml = "";
    $.ajax({
        type: "POST",
        url: "/App_Logic/dataGeneric.ashx?ID=" + sid + "&mode=listsublevel&nhol=" + _nhol,
        cache: false,
        contentType: 'application/json;',
        success: function (objlevel, status) {
            var tabHTML = "";
            var checked = false;
            tabHTML += '<table class="table table-striped" style="display:none;" id="tdsublevel' + sid + '">';
            $.each(objlevel, function (index) {
                if (objlevel[index].nHoliday !== null || objlevel[index].nAll === "1") {
                    checked = " checked";
                }
                else {
                    checked = "";
                    $('input[id*=txtlv' + sid + ']').attr("checked", false);
                }
                tabHTML += '<tr><td style="width:70%">' + objlevel[index].SubLevel + '</td>'
                tabHTML += '<td><input nHol="' + objlevel[index].nTSubLevel + '" type="checkbox" '
                tabHTML += 'value="' + objlevel[index].nTSubLevel + '" termsub="yes" class="form-control termsub" id="txtsublv' + sid + '_' + objlevel[index].nTSubLevel + '" '
                tabHTML += ' ' + checked + ' onclick=GetListtime($(this).val(),$(this).is(":checked"),' + sid + '); /></td>'
                tabHTML += '</td></tr>';
            })
            tabHTML += "</table>";
            $('#tdlevel' + sid).html($('#tdlevel' + sid).html() + tabHTML);
        }
    });
}

function tabtimetype(nHol, divmain, divsub) {
    _nhol = nHol;
    $('input[id*=txtListtime]').val("");
    $('#divmain').html("");
    $('#myTabContent').html("");
    $('#modalSet').html("");
    $('#modalsub').html("");
    var strHtml = "";
    $.ajaxSetup({ cache: false });
    $.ajax({
        type: "POST",
        url: "/App_Logic/dataGeneric.ashx?ID=&mode=tabtimetype",
        cache: false,
        contentType: 'application/json;',
        success: function (objjson, status) {
            strHtml = '<ul id="myTab" class="nav nav-tabs">';
            var tabHTML = "";
            $.each(objjson, function (index) {
                strHtml += ' <li ';
                if (index === 0) {
                    strHtml += ' class="active"';
                }
                strHtml += '><a href="#tab' + objjson[index].nTimeType + '" data-toggle="tab">' + objjson[index].sTimeType + '</a></li>';
                getlevel(objjson[index].nTimeType, index, divsub)
            })
            strHtml += '</ul>';
            $('#' + divmain).html(strHtml);
        }
    });
}

function ShowSubLevel(level) {
    if ($('table[id*=tdsublevel' + level + ']').css("display") === "none") {
        $('table[id*=tdsublevel' + level + ']').css("display", "");
        $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
        $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
    } else {
        $('table[id*=tdsublevel' + level + ']').css("display", "none");
        $('i[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
        $('i[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
    }
}
