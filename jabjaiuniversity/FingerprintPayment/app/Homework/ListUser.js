var availableListUser = [];
var ArrayUser = "";
var ArrayEmp = "";
$(function () {
    if ($("input[name*=optradio]") !== []) {

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

        $("input[name*=optradio]").click(function () {
            if ($("input[name=optradio]:checked").val() === "0") {
                $('.send_type_0').removeClass("hidden");
                $('.send_type_1').addClass("hidden");
            }
            else {
                $('.send_type_1').removeClass("hidden");
                $('.send_type_0').addClass("hidden");
            }

            $("select[name=selecttype]").val("0");
        });
    } else {
        $("input[name*=optradio]").click(function () {
            if ($("input[name=optradio]:checked").val() === "0") {
                $('.send_type_0').removeClass("hidden");
                $('.send_type_1').addClass("hidden");
            }
            else {
                $('.send_type_1').removeClass("hidden");
                $('.send_type_0').addClass("hidden");
            }
            $("select[name=selecttype]").val("0");
        });
    }

    $("select[name*=selecttype]").change(function () {
        if ($("select[name=selecttype]").val() === "0") {
            $('.user_type_1').removeClass("hidden");
        } else {
            $('.user_type_1').addClass("hidden");
        }
    });

    $("#btnaddlistuser").click(function () {
        var name = $('input[name=sname]').val();
        var type = $("select[name=selecttype] option:selected").text();
        var userid = $('input[name=iduser]').val();
        if (type === "") type = "นักเรียน";
        var bCheck = CheckUser(userid, type);

        if ($('input[name=sname]').val() === "" || $('input[name=iduser]').val() === "") {
            $('input[name=iduser]').val("");
            $('input[name=sname]').val("");
            return;
        }
        else if (bCheck) {
            $('input[name=iduser]').val("");
            $('input[name=sname]').val("");
            return;
        }

        if (type === "พนักงาน") {
            $("input[id*=txtUserType1]").val($("input[id*=txtUserType1]").val() + "," + userid);
            ArrayEmp += (ArrayEmp === "" ? "" : ",") + userid + ",";
        } else {
            $("input[id*=txtUserType0]").val($("input[id*=txtUserType0]").val() + "," + userid);
            ArrayUser += (ArrayUser === "" ? "" : ",") + userid;
        }

        var newObject = {
            name: name,
            type: type,
            userid: userid,
        };
        availableListUser.push(newObject);
        $("#tablelistuser tbody").html("");
        $.each(availableListUser, function (index) {
            $("#tablelistuser tbody").append(`<tr><td>` + availableListUser[index].name + `<td>` + availableListUser[index].type
                + `<td><span class="glyphicon glyphicon-remove" style="color:red" onclick="RemoveUser('` + availableListUser[index].userid + `','')"></span>`).appendTo
        });
        $('input[name=iduser]').val("");
        $('input[name=sname]').val("");
    });
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
    $('select[name=selectsublevel]')
        .append($("<option></option>")
            .attr("value", "")
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

function RemoveUser(userid, type) {
    availableListUser = availableListUser.filter(function (el) {
        return el.userid !== userid;
    });

    $("#tablelistuser tbody").html("");
    ArrayUser = "";
    $.each(availableListUser, function (index) {
        ArrayUser += availableListUser[index].userid + ",";
        $("#tablelistuser tbody").append(`<tr><td>` + availableListUser[index].name + `<td>`
            + availableListUser[index].type
            + `<td><span class="glyphicon glyphicon-remove" style="color:red" onclick="RemoveUser('` + availableListUser[index].userid + `','')"></span>`).appendTo
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
            var type = $("select[name=selecttype]");
            if (type.length === 0 || type.val() === "0") {
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
}

function CheckUser(userid, type) {
    var bCheck = false;
    jQuery.grep(availableListUser, function (n, i) {
        if (n.userid === userid && n.type === type) bCheck = true;
    });
    return bCheck;
}
