
$(function () {
    $(document).load(function () {
        $("body").mLoading();
    });
    $("#filter").html(Controlfilter());
    $('.datepicker').datepicker({});

    $("#exportfile").click(function () {
        var dt = new Date();
        $('#dailySales').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปยอดเติมเงิน', fileName: "รายงานสรุปยอดเติมเงิน_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
    });

    $("#topup_type").change(function () {
        if ($(this).val() === "0") {
            $(".topup_type").show();
            $("select[id*=ddlSearch]").val("0");
            $(".type_row_0").addClass("hidden");
            $(".type_row_1").addClass("hidden");
            $(".type_row_2").addClass("hidden");
        }
        else if ($(this).val() === "2") {
            $("select[id*=ddlSearch]").val("0");
            $(".topup_type").hide();
            $(".type_row_0").addClass("hidden");
            $(".type_row_1").addClass("hidden");
            $(".type_row_2").addClass("hidden");
        }
        else {
            $(".topup_type").hide();
            $("select[id*=ddlSearch]").val("0");
            $(".type_row_0").removeClass("hidden");
            $(".type_row_1").removeClass("hidden");
            $(".type_row_2").removeClass("hidden");
        }
    });
})

function genreportsummoney4topup() {
    $("body").mLoading();
    $("#dailySales").html();
    var thead = $("#dailySales thead");
    var tbody = $("#dailySales tbody");
    thead.html("");
    tbody.html("");
    var search = "";
    $("#divprint").html("");

    var from = $("#txtdStart").val().split("/");
    var f = new Date(from[2], from[1] - 1, from[0]);
    from = $("#txtdEnd").val().split("/");
    var f2 = new Date(from[2], from[1] - 1, from[0]);
    if (f > f2) {
        $("body").mLoading('hide');
        $('#modalAlert #modal-header').text("ข้อความจากระบบ");
        $('#modalAlert #modal-content').html("รูปแบบการค้นหาวันที่ไม่ถูกต้อง");
        $("#modalAlert #modal-confirm ").hide()
        $("#modalAlert").modal();
        return;
    }

    var day = "";
    var dt = new Date();
    if ($("#txtdStart").val() !== "" && $("#txtdEnd").val() !== "") {
        day = $("#txtdStart").val() + " ถึงวันที่ " + $("#txtdEnd").val();
    }
    else if ($("#txtdStart").val()) {
        day = $("#txtdStart").val();
    }
    else {
        day = dt.toLocaleDateString();
    }

    search += "&user_type=" + $("select[id*=ddlSearch]").val();
    search += "&customer_type=" + $("select[name=selecttype]").val();
    search += "&customer_id=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&employees_id=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&daystart=" + $("input[id*=txtdStart]").val();
    search += "&dayend=" + $("input[id*=txtdEnd]").val();
    search += "&level_id=" + $("#selectlevel").val();
    search += "&level2_id=" + $("#selectsublevel").val();
    search += "&topup_type=" + $("#topup_type").val();

    $.get("/App_Logic/ReportsJSON.ashx?mode=reportsummoney4topup" + search, "", function () {
    }).done(function (result) {
        if (result.length === 0) {
            $("#divprint").html("<center><h1>ไม่มีข้อมูล</h1></center>");
        }
        else {
            var headerreport = '<tr><th colspan="3" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
            headerreport += '<tr><th colspan="3" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;">รายงานสรุปยอดเติมเงิน';
            headerreport += '<tr><th colspan="3" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;">ประจำวันที่ ' + day;
            headerreport += '<tr><th colspan="3" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;">ออกรายงานวันที่ :&nbsp;' + dt.toLocaleDateString();
            headerreport += '<tr><th colspan="3" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;">เวลา :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
            headerreport += '<tr><th colspan="3" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><br>';
            thead.append(headerreport);

            var headertable = "<tr style='background-color:#337ab7;color:#fff;'>";
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ลำดับ';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">วันที่';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ยอดเติม';

            tbody.append(headertable);
            var money = 0;

            $.each(result, function (index) {
                var rowstable = "<tr class='active'>";
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + (index + 1);
                rowstable += "<td style='text-align:center;font-size: 20px;cursor:pointer;color:#337ab7;' onclick=gentabledetail('" + result[index].daytopup + "')>" + result[index].daytopup;
                rowstable += "<td style='text-align:center; font-size: 20px;'>" + result[index].money;

                money += result[index].money;
                tbody.append(rowstable);
            });

            tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;' colspan=2>ยอดรวม</td><td style='text-align:right;font-size: 20px;'colspan=1>" + money + " บาท");
        }
        $("body").mLoading('hide');
    });

}

function gentabledetail(day) {
    $("body").mLoading();
    $("#dailySales").html();
    var thead = $("#dailySales thead");
    var tbody = $("#dailySales tbody");
    thead.html("");
    tbody.html("");
    var search = "";
    var dt = new Date();

    search += "&user_type=" + $("select[id*=ddlSearch]").val();
    search += "&customer_type=" + $("select[name=selecttype]").val();
    search += "&customer_id=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&employees_id=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&level_id=" + $("#selectlevel").val();
    search += "&level2_id=" + $("#selectsublevel").val();
    search += "&DayStart=" + day;
    search += "&topup_type=" + $("#topup_type").val();

    $.get("/App_Logic/ReportsJSON.ashx?mode=reportdetailmoney4topup" + search, "", function (result) {
        var headerreport = '<tr><th colspan="5" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
        headerreport += '<tr><th colspan="5" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;">รายงานสรุปยอดเติมเงิน';
        headerreport += '<tr><th colspan="5" style="text-align: center; font-size: 22px; font-weight: bolder;border:0px;">ประจำวันที่ ' + day;
        headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;">ออกรายงานวันที่ :&nbsp;' + dt.toLocaleDateString();
        headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;">เวลา :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
        headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 22px; font-weight: bolder;border:0px;"><br>';
        thead.append(headerreport);
        $.each(result, function (indexday) {
            var topupdate = result[indexday].topupdate
            var headertable = "<tr style='background-color:#337ab7;color:#fff;'>";
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ลำดับ';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ชื่อพนักงาน';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ชื่อผู้เติม';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">เวลา';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder">ยอดเติม(บาท)'

            tbody.append(headertable);

            var sum = 0, lucre = 0;
            $.each(topupdate, function (indexsell) {
                var rowstable = "<tr class='active'>";
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + (indexsell + 1);
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + topupdate[indexsell].employeesnamm;
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + topupdate[indexsell].customername;
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + topupdate[indexsell].timetoup;
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + topupdate[indexsell].money;
                sum += topupdate[indexsell].money;
                tbody.append(rowstable);
            });
            tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;' colspan=4>ยอดรวม</td><td style='text-align:right;font-size: 20px;'colspan=1>" + sum + " บาท");
        });
        $("body").mLoading('hide');
    });
}