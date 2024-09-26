/// <reference path="~/Scripts/jquery-3.1.1.js" />

var availableValueProduct = [];
$(function () {
    $("select[name*=ddlcType]").change(function () {
        $("input[name=txtSearch]").val("");
        $("input[name=productid]").val("");
        getlistproduct();
    });
    getlistproduct();
    AutocompleteProduct();

    $("#exportfile").click(function () {
        var dt = new Date();
        $('#dailySales').tableExport({ type: 'excel', escape: 'false', sheets: 'รายงานสรุปยอดขาย', fileName: "รายงานสรุปยอดขาย_" + dt.toLocaleDateString() + dt.getHours() + dt.getMinutes() + dt.getSeconds() });
    });

    $("#sell_type").change(function () {
        if ($(this).val() === "0") {
            $(".type_sell").show();
            $("select[id*=ddlSearch]").val("0");
            $(".type_row_0").addClass("hidden");
            $(".type_row_1").addClass("hidden");
            $(".type_row_2").addClass("hidden");
        }
        else if ($(this).val() === "2") {
            $("select[id*=ddlSearch]").val("0");
            $(".type_sell").hide();
            $(".type_row_0").addClass("hidden");
            $(".type_row_1").addClass("hidden");
            $(".type_row_2").addClass("hidden");
        }
        else {
            $(".type_sell").hide();
            $("select[name=selecttype]").val("1");
            $("select[id*=ddlSearch]").val("0");
            $(".type_row_0").addClass("hidden");
            $(".type_row_1").addClass("hidden");
            $(".type_row_2").removeClass("hidden");
        }
    });
})

function getlistproduct() {
    var typeproduct = $('select[id*=ddlcType]').val();
    availableValueProduct = [];
    $.ajax({
        url: "/App_Logic/dataJSon.ashx?mode=productlist&id=" + typeproduct,
        dataType: "json",
        success: function (objjson) {
            $.each(objjson, function (index) {
                var newObject = {
                    label: objjson[index].label,
                    value: objjson[index].value
                };
                availableValueProduct[index] = newObject;
            });
        }
    });
}

function AutocompleteProduct() {
    $('input[name*=txtSearch]').autocomplete({
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
            results = $.ui.autocomplete.filter(availableValueProduct, request.term);
            response(results.slice(0, 10));
        },
        select: function (event, ui) {
            event.preventDefault();
            $("input[name*=txtSearch]").val(ui.item.label);
            $("input[name=productid]").val(ui.item.value);
        },
        focus: function (event, ui) {
            event.preventDefault();
            $("input[name=productid]").val("");
        }
    });
}

function gentablesum() {
    $("body").mLoading();
    $("#dailySales").html();
    var thead = $("#dailySales thead");
    var tbody = $("#dailySales tbody");
    thead.html("");
    tbody.html("");
    var search = "";
    $("#divprint").html("");
    var day = "";
    var dt = new Date();
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

    if ($("#txtdStart").val() !== "" && $("#txtdEnd").val() !== "") {
        day = $("#txtdStart").val() + " ถึงวันที่ " + $("#txtdEnd").val();
    }
    else if ($("#txtdStart").val()) {
        day = $("#txtdStart").val();
    }
    else {
        day = dt.toLocaleDateString();
    }
    search += "&productid=" + ($("input[name=productid]").val() === "" ? "0" : $("input[name=productid]").val());
    search += "&customer_type=" + $("select[name=selecttype]").val();
    search += "&CustomerId=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&user_type=" + $("select[name*=ddlSearch]").val();
    search += "&EmployeesId=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&DayStart=" + $("input[id*=txtdStart]").val();
    search += "&DayEnd=" + $("input[id*=txtdEnd]").val();
    search += "&sell_type=" + $("#sell_type").val();
    search += "&level_id=" + $("select[name=selectlevel]").val();
    search += "&level2_id=" + $("select[name=selectsublevel]").val();
    search += "&product_type=" + $("select[id*=ddlcType]").val();
    search += "&product_name=" + $("input[id*=txtSearch]").val();

    $.get("/App_Logic/ReportsJSON.ashx?mode=reportsummoney" + search, "", function (result) {
        if (result.length === 0) {
            $("#divprint").html("<center><h1>ไม่มีข้อมูล</h1></center>");
        }
        else {
            var headerreport = '<tr><th colspan="5" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
            headerreport += '<tr><th colspan="5" style="text-align: center; font-size: 24px; font-weight: bolder;border:0px;">รายงานสรุปยอดขาย';
            headerreport += '<tr><th colspan="5" style="text-align: center; font-size: 24px; font-weight: bolder;border:0px;">ประจำวันที่ ' + day;
            headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;">ออกรายงานวันที่ :&nbsp;' + dt.toLocaleDateString();
            headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;">เวลา :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
            headerreport += '<tr><th colspan="5" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;"><br>';
            thead.append(headerreport);

            var headertable = "<tr style='background-color:#337ab7;color:#fff;'>";
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ลำดับ';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">วันที่';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ราคาต้นทุน';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ราคาขาย';
            headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">กำไร';

            tbody.append(headertable);
            var price = 0, lucre = 0, cost = 0;

            $.each(result, function (index) {
                var rowstable = "<tr class='active'>";
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + (index + 1);
                rowstable += "<td style='text-align:center;font-size: 20px;cursor:pointer;color:#337ab7;' onclick=gentabledetail('" + result[index].dsell + "')>" + result[index].dsell;
                rowstable += "<td style='text-align:center; font-size: 20px;'>" + result[index].cost;
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + result[index].total;
                rowstable += "<td style='text-align:center;font-size: 20px;'>" + result[index].lucre;

                price += result[index].total;
                cost += result[index].cost;
                lucre += result[index].lucre;
                tbody.append(rowstable);
            });

            tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=4>ยอดขายรวม</td><td style='text-align:right;font-size: 20px;'colspan=1>" + price + " บาท");
            tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=4>ต้นทุน</td><td style='text-align:right;font-size: 20px;'colspan=1>" + cost.toFixed(2) + " บาท");
            tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=4>กำไร</td><td style='text-align:right;font-size: 20px;'colspan=1>" + lucre.toFixed(2) + " บาท");
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
    $("#divprint").html("");
    var search = "";
    var dt = new Date();
    search += "&productid=" + ($("input[name=productid]").val() === "" ? "0" : $("input[name=productid]").val());
    search += "&customer_type=" + $("select[name=selecttype]").val();
    search += "&CustomerId=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&user_type=" + $("select[name*=ddlSearch]").val();
    search += "&EmployeesId=" + ($("input[name=iduser]").val() === "" ? "0" : $("input[name=iduser]").val());
    search += "&DayEnd=" + $("input[id*=txtdEnd]").val();
    search += "&sell_type=" + $("#sell_type").val();
    search += "&level_id=" + $("select[name=selectlevel]").val();
    search += "&level2_id=" + $("select[name=selectsublevel]").val();
    search += "&product_type=" + $("select[id*=ddlcType]").val();
    search += "&product_name=" + $("input[id*=txtSearch]").val();
    search += "&DayStart=" + day;


    $.get("/App_Logic/ReportsJSON.ashx?mode=reportsbuyitem" + search, "", function (result) {
        if (result.length === 0) {
            $("#divprint").html("<center><h1>ไม่มีข้อมูล</h1></center>");
        }
        else {
            var headerreport = '<tr><th colspan="11" style="text-align: center; font-size: 26px; font-weight: bolder;border:0px;" id="school">' + $("input[id*=hdfschoolname]").val();
            headerreport += '<tr><th colspan="11" style="text-align: center; font-size: 24px; font-weight: bolder;border:0px;">รายงานสรุปยอดขาย';
            headerreport += '<tr><th colspan="11" style="text-align: center; font-size: 24px; font-weight: bolder;border:0px;">ประจำวันที่ ' + day;
            headerreport += '<tr><th colspan="11" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;">ออกรายงานวันที่ :&nbsp;' + dt.toLocaleDateString();
            headerreport += '<tr><th colspan="11" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;">เวลา :&nbsp;' + dt.getHours() + `:` + dt.getMinutes() + `:` + dt.getSeconds();
            headerreport += '<tr><th colspan="11" style="text-align: right; font-size: 20px; font-weight: bolder;border:0px;"><br>';
            thead.append(headerreport);
            $.each(result, function (indexday) {
                var selldata = result[indexday].selldata
                var headertable = " <tr><td colspan='11' style='text-align:right;'>" + result[indexday].dsell;
                headertable += "<tr style='background-color:#337ab7;color:#fff;'>";
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ลำดับ';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">เวลา';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ชื่อพนักงาน';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ชื่อผู้ซื้อสินค้า';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ประภท';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">รหัสบาร์โค๊ด';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ชื่อสินค้า';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ราคาต้นทุน';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ราคาขาย';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">จำนวน(หน่วย)';
                headertable += '<td id="headder" style="text-align: center; font-size: 20px; font-weight: bolder;">ยอดรวม (บาท)'

                tbody.append(headertable);

                var sum = 0, lucre = 0, cost = 0;
                $.each(selldata, function (indexsell) {
                    var rowstable = "<tr class='active'>";
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + (indexsell + 1);
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + selldata[indexsell].tsell;
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + selldata[indexsell].employeesnamm;
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + selldata[indexsell].customername;
                    rowstable += "<td style='text-align:center; font-size: 20px;'>" + selldata[indexsell].producttype;
                    rowstable += "<td style='text-align:center; font-size: 20px;'>" + selldata[indexsell].sbarcode;
                    rowstable += "<td style='text-align:center; font-size: 20px;'>" + selldata[indexsell].product;
                    rowstable += "<td style='text-align:center; font-size: 20px;'>" + selldata[indexsell].cost;
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + selldata[indexsell].price;
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + selldata[indexsell].amount;
                    rowstable += "<td style='text-align:center;font-size: 20px;'>" + (selldata[indexsell].price * selldata[indexsell].amount);;
                    sum += (selldata[indexsell].price * selldata[indexsell].amount);
                    cost += (selldata[indexsell].cost * selldata[indexsell].amount);
                    lucre += (selldata[indexsell].price * selldata[indexsell].amount) - (selldata[indexsell].cost * selldata[indexsell].amount);
                    tbody.append(rowstable);
                });

                tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=10>ยอดขายรวม</td><td style='text-align:right;font-size: 20px;'colspan=1>" + sum.toFixed(2) + " บาท");
                tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=10>ต้นทุน</td><td style='text-align:right;font-size: 20px;'colspan=1>" + cost.toFixed(2) + " บาท");
                tbody.append("<tr style='text-align:left;' class='active'><td style='text-align:right;font-size: 20px;font-weight: bolder;' colspan=10>กำไร</td><td style='text-align:right;font-size: 20px;'colspan=1>" + lucre.toFixed(2) + " บาท");
            });
        }
        $("body").mLoading('hide');
    });
}