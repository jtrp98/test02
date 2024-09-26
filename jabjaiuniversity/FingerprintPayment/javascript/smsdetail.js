$(function () {
    var smsid = getUrlParameter("id");
    $.get("/App_Logic/dataJSONArray.ashx", { mode: "smddetail", smsid: smsid }, function (JsonOBJ) {
        $("#type").html(JsonOBJ.type);
        $("#texttype").html(JsonOBJ.texttype);
        $("#daysend").html(JsonOBJ.daysend);
        $("#timesend").html(JsonOBJ.timesend);
        $("#text").html(JsonOBJ.text);
        $("#useradd").html(JsonOBJ.useradd);
        var lUser = JsonOBJ.user;
        if (JsonOBJ.files != null)
        {
            $.each(JsonOBJ.files, function (fileindex) {
                var file = JsonOBJ.files[fileindex].filesname;
                switch (JsonOBJ.files[fileindex].contenttype)
                {
                    case "application/pdf":
                        $("#filerows").append("<tr><td><a target=\"_blank\" href=\"" + file + "\" ><i class=\"fa fa-file-pdf-o\"></i></a></td></tr>");
                        break;
                    case "application/msword": // WORD 2003
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": // WORD 2007
                        $("#filerows").append("<tr><td><a target=\"_blank\" href=\"" + file + "\" ><i class=\"fa fa-file-word-o\"></i></a></td></tr>");
                        break;
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": //EXECL 2007
                    case "application/vnd.ms-excel": //EXECL 2003
                        $("#filerows").append("<tr><td><a target=\"_blank\" href=\"" + file + "\" ><i class=\"fa fa-file-excel-o\"></i></a></td></tr>");
                        break;
                    case "image/png":
                    case "image/jpeg":
                    case "image/jpg":
                        $("#filerows").append("<tr><td><a target=\"_blank\" href=\"" + file + "\" ><img src=\"" + file + "\" style=\"height: 200px; width: 100px\"/></a></td></tr>");
                        break;
                }
            });
        }

        $.each(lUser, function (indexUser) {
            $("#tablelistuser tbody").append("<tr><td>" + (indexUser + 1) + "<td>" + lUser[indexUser].name
                + "<td>" + lUser[indexUser].type + "<td>" + (lUser[indexUser].status === false ? "ไม่ได้อ่าน" : "อ่านแล้ว"))
        });
    });
});