var availableValuestudent = [];
var availableValueEmployees = [];
var availableListUser = [];
function j_infosell(msg) {

    showModal("แจ้งผลการดำเนินการ", msg, function () {
    });
}

function getlevel(divsub) {
    var tabHTML = "";
    $.ajax({
        type: "POST",
        url: "/App_Logic/dataJSon.ashx?mode=getlevel",
        cache: false,
        contentType: 'application/json;',
        success: function (objlevel, status) {
            tabHTML += '<table class="table table-striped" style="margin-bottom:0px;"><tr class="table-tab"><th style="width:70%">ระดับช่วงชั้น</th><th style="width:15%"></th></tr></table>';
            tabHTML += '<div style="overflow-y: scroll; height: 300px;"><table class="table table-striped table-select-class">';
            $.each(objlevel, function (index) {

                tabHTML += '<tr class="level-name"><td>'
                tabHTML += '<div id="tdlevel' + objlevel[index].levelid + '" onclick="ShowSubLevel(' + objlevel[index].levelid + ')" ><span id="tdlevel' + objlevel[index].levelid + '" class="glyphicon glyphicon-chevron-right"></span> ' + objlevel[index].levelname + '</div>'
                tabHTML += '</td><td><div class="checkbox-container"><input type="checkbox" level="' + objlevel[index].levelid + '" '
                tabHTML += 'value="' + objlevel[index].levelid + '" termsub="yes" class="form-control termsub" id="txtlv' + objlevel[index].levelid + '" '
                tabHTML += ' onclick=checkedlevel(' + objlevel[index].levelid + ',$(this).is(":checked"));  />'

                var objsublevel = objlevel[index].sublevel;
                $.each(objsublevel, function (indexsublevel) {
                    var objsublevel2 = objsublevel[indexsublevel].sublevel2;

                    tabHTML += '<tr style="display:none;" id="rowlevel' + objlevel[index].levelid + '"><td style="padding-left:5em" '
                        + 'onclick="ShowSubLevel2(' + objsublevel[indexsublevel].sublevelid + ')"><span id="tdsublevel'
                        + objsublevel[indexsublevel].sublevelid + '" class="glyphicon glyphicon-chevron-right"></span>'
                        + objsublevel[indexsublevel].sublevelname + '</td>'
                    tabHTML += '<td><input sublevel="' + objsublevel[indexsublevel].sublevelid + '" type="checkbox" '
                    tabHTML += 'value="' + objsublevel[indexsublevel].sublevelid + '" termsub="yes" class="form-control termsub level' + objlevel[index].levelid + '"'
                    tabHTML += ' onclick=checkedsublevel(' + objsublevel[indexsublevel].sublevelid + ',' + objlevel[index].levelid + ',$(this).is(":checked")); /></td>'

                    $.each(objsublevel2, function (indexsublevel2) {
                        tabHTML += '<tr style="display:none;" id="rowsublevel' + objsublevel[indexsublevel].sublevelid + '"><td style="padding-left:10em">' + objsublevel2[indexsublevel2].sublevel2name + '</td>'
                        tabHTML += '<td><input nHol="' + objsublevel2[indexsublevel2].sublevel2id + '" type="checkbox" '
                        tabHTML += 'value="' + objsublevel2[indexsublevel2].sublevel2id + '" termsub="yes" class="form-control termsub level' + objlevel[index].levelid
                            + ' sublevel' + objsublevel[indexsublevel].sublevelid + '" id="sublevel2"'
                        tabHTML += ' onclick=checkedsublevel2(' + objsublevel[indexsublevel].sublevelid + ',' + objlevel[index].levelid + ',$(this).is(":checked")); /></td>'
                        tabHTML += '</td></tr>';
                    })

                    tabHTML += '</td></tr>';
                });

                tabHTML += '</div></td>'
                tabHTML += '</td></tr>';

            })
            tabHTML += "</table></div></div>";
            $('#' + divsub).html(tabHTML);
        }
    });
}

function ShowSubLevel(level) {
    if ($('tr[id=rowlevel' + level + ']').css("display") == "none") {
        $('tr[id=rowlevel' + level + ']').css("display", "");
        $('span[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-right");
        $('span[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-down");
    } else {
        $('tr[id=rowlevel' + level + ']').css("display", "none");
        $('span[id=tdlevel' + level + ']').removeClass("glyphicon glyphicon-chevron-down");
        $('span[id=tdlevel' + level + ']').addClass("glyphicon glyphicon-chevron-right");
    }
}

function ShowSubLevel2(sublevel) {
    if ($('tr[id=rowsublevel' + sublevel + ']').css("display") == "none") {
        $('tr[id=rowsublevel' + sublevel + ']').css("display", "");
        $('span[id=tdsublevel' + sublevel + ']').removeClass("glyphicon glyphicon-chevron-right");
        $('span[id=tdsublevel' + sublevel + ']').addClass("glyphicon glyphicon-chevron-down");
    } else {
        $('tr[id=rowsublevel' + sublevel + ']').css("display", "none");
        $('span[id=tdsublevel' + sublevel + ']').removeClass("glyphicon glyphicon-chevron-down");
        $('span[id=tdsublevel' + sublevel + ']').addClass("glyphicon glyphicon-chevron-right");
    }
}

function checkedlevel(val, addtime) {
    $('.level' + val).prop("checked", addtime);
    getvaluessublevel2();
}

function checkedsublevel(sublevel, level, addtime) {
    $('.sublevel' + sublevel).prop("checked", addtime);
    $('input[sublevel=' + sublevel + ']').prop("checked", $('.sublevel' + sublevel).length == $('.sublevel' + sublevel + ':checked').length);
    $('input[level=' + level + ']').prop("checked", $('.level' + level).length == $('.level' + level + ':checked').length);
    getvaluessublevel2();
}

function checkedsublevel2(sublevel, level, addtime) {
    $('input[sublevel=' + sublevel + ']').prop("checked", $('.sublevel' + sublevel).length == $('.sublevel' + sublevel + ':checked').length);
    $('input[level=' + level + ']').prop("checked", $('.level' + level).length == $('.level' + level + ':checked').length);
    getvaluessublevel2();
}

var Strsublevel2 = "";
function getvaluessublevel2() {
    Strsublevel2 = "";
    $('input[id*=txtLv]').val("");
    $.each($("input[id=sublevel2]:checked"), function () {
        Strsublevel2 += (Strsublevel2 === "" ? "" : ",") + $(this).val();
    });
    $('input[id*=txtLv]').val(Strsublevel2);
}

$(function () {
    $('.clockpicker').clockpicker();
    $("#datesection").hide();
    $("#timesection").hide();
    $("#lvSet").hide();
    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
    getlevel('myTabContent');

    $("#ddlType").change(function () {
        if ($(this).val() == "0") {
            $("#datesection").hide();
            $("#timesection").hide();
            $("#lvSet").hide();
        }
        else {
            $("#subtype").hide();
            $("#datesection").show();
            $("#timesection").show();
            $("#lvSet").show();
        }
    });

    $("input[name*=optradio]").click(function () {
        if ($("input[name*=optradio]:checked").val() == "0") {
            $('.send_type_0').removeClass("hidden");
            $('.send_type_1').addClass("hidden");
        }
        else {
            $('.send_type_1').removeClass("hidden");
            $('.send_type_0').addClass("hidden");
        }

        $("select[name=selecttype]").val("0");
    });

    $("select[name*=selecttype]").change(function () {
        if ($("select[name=selecttype]").val() == "0") {
            $('.user_type_1').removeClass("hidden");
        } else {
            $('.user_type_1').addClass("hidden");
        }
    });

    $("select[name=selectlevel]").change(function () {
        getListSubLV2();
        getliststudent();
    });

    $("select[name=selectsublevel]").change(function () {
        getliststudent();
    });

    $('select[name=selectlevel] option').remove();
    $('select[name=selectlevel]')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));

    $('select[name=selectsublevel]')
        .append($("<option></option>")
            .attr("value", "")
            .text("ทั้งหมด"));

    $.ajax({
        url: "/App_Logic/dataJSONArray.ashx?mode=listsublevel",
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[name=selectlevel]')
                    .append($("<option></option>")
                        .attr("value", msg[index].values)
                        .text(msg[index].text));
            });
        }
    });

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
            console.log(objjson);
        }
    });

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
            var type = $("select[name=selecttype]").val();
            var results;
            if (type === "0") {
                results = $.ui.autocomplete.filter(availableValuestudent, request.term);
            } else {
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

    $("#btnaddlistuser").click(function () {
        var name = $('input[name=sname]').val();
        var type = $("select[name=selecttype] option:selected").text();
        var userid = $('input[name=iduser]').val();
        var bCheck = CheckUser(userid, type);

        if ($('input[name=sname]').val() === "") {
            return;
        }
        else if (bCheck) {
            $('input[name=iduser]').val("");
            $('input[name=sname]').val("");
            return;
        }

        if (type == "พนักงาน") {
            $("input[id*=txtUserType1]").val($("input[id*=txtUserType1]").val() + "," + userid);
        } else {
            $("input[id*=txtUserType0]").val($("input[id*=txtUserType0]").val() + "," + userid);
        }

        var newObject = {
            name: name,
            type: type,
            userid: userid
        };
        availableListUser.push(newObject);
        $("#tablelistuser tbody").html("");
        $.each(availableListUser, function (index) {
            $("#tablelistuser tbody").append("<tr><td>" + availableListUser[index].name + "<td>" + availableListUser[index].type + "<td style='display: none;'>" + availableListUser[index].userid).appendTo
        });
        $('input[name=iduser]').val("");
        $('input[name=sname]').val("");
    });

    $("select[id*=ddlSendType]").change(function () {
        if ($(this).val() === "3") {
            $(".container").removeClass("hidden");
        } else {
            $(".container").addClass("hidden");
        }
    })
});

function getListSubLV2() {
    var SubLVID = $('select[name=selectlevel]').val();
    var sSubLV = $('select[name=selectlevel]').text();
    $('select[name=selectsublevel] option').remove();
    $('select[name=selectsublevel]')
        .append($("<option></option>")
            .attr("value", " ")
            .text("ทั้งหมด"));
    $.ajax({
        url: "/App_Logic/dataJSONArray.ashx?mode=listsublevel2&SubLevel=" + SubLVID,
        success: function (msg) {
            $.each(msg, function (index) {
                $('select[name=selectsublevel]')
                    .append($("<option></option>")
                        .attr("value", msg[index].values)
                        .text(msg[index].text));
            });
        }
    });
}
function CheckUser(userid, type) {
    var bCheck = false;
    jQuery.grep(availableListUser, function (n, i) {
        if (n.userid === userid && n.type === type) bCheck = true;
    });
    return bCheck;
}
function getliststudent() {
    availableValuestudent = [];
    $.ajax({
        url: "/App_Logic/dataGenericListData.ashx?mode=liststudent&nelevel=" +
            $('select[name=selectlevel]').val() + "&nsublevel=" + $('select[name=selectsublevel]').val(),
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

function validate() {
    var checked = true;
    if ($('select[id*=ddlType] option:selected').val() === "1") {
        if ($("input[id*=dateSMS]").val() === "") checked = false;
        if ($("input[id*=monstop1]").val() === "") checked = false;

    }
    if ($("input[id*=optradio]:checked").val() === "0") {
        if ($("select[id*=ddlSendType] option:selected").val() === "3") {
            if ($("input[id*=txtLv]").val() === "") checked = false;
        }
    }
    else {
        if ($("input[id*=txtUserType1]").val() === "" && $("input[id*=txtUserType0]").val() === "") checked = false;
    }
    if ($("textarea[id*=txtDesp]").val() === "") checked = false;
    if (checked === false) j_infosell('<span>กรุณากรอกข้อมูลให้ครบถ้วน</span>');
    else $('body').mLoading({ text: '<br/>ระบบกำลังการส่งอาจใช้เวลานาน<br/>ประมาณ 1 - 2 นาทีกรุณารอชักครู่', html: true });
    return checked;
}